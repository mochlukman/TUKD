import { Component, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { LazyLoadEvent } from 'primeng/api';
import { IDaftunit } from 'src/app/core/interface/idaftunit';
import { IDisplayGlobal } from 'src/app/core/interface/iglobal';
import { IRkad } from 'src/app/core/interface/irkad';
import { ITokenClaim } from 'src/app/core/interface/itoken-claim';
import { InputRupiahPipe } from 'src/app/core/pipe/input-rupiah.pipe';
import { AuthenticationService } from 'src/app/core/services/auth.service';
import { DaftunitService } from 'src/app/core/services/daftunit.service';
import { RkadService } from 'src/app/core/services/rkad.service';
import { CanActiveGuardService } from 'src/app/core/_commonServices/can-active-guard.service';
import { NotifService } from 'src/app/core/_commonServices/notif.service';
import { LookDaftunitComponent } from 'src/app/shared/lookups/look-daftunit/look-daftunit.component';
import { RkadFormxComponent } from './rkad-formx/rkad-formx.component';

@Component({
  selector: 'app-pendapatanx',
  templateUrl: './pendapatanx.component.html',
  styleUrls: ['./pendapatanx.component.scss'],
  providers: [InputRupiahPipe]
})
export class PendapatanxComponent implements OnInit, OnDestroy {
  title: string = '';
  uiUnit: IDisplayGlobal;
  unitSelected: IDaftunit;
  userInfo: ITokenClaim;
  loading: boolean;
  listdata: IRkad[] = [];
  totalRecords: number;
  totalNilai: number = 0;
  dataSelected: IRkad = null;
  formFilter: FormGroup;
  initialForm: any;
  nilaiInit: number;
  @ViewChild(LookDaftunitComponent, { static: true }) Daftunit: LookDaftunitComponent;
  @ViewChild(RkadFormxComponent, { static: true }) Form: RkadFormxComponent;
  @ViewChild('dt', { static: false }) dt: any;
  isvalid: boolean = false;
  constructor(
    private auth: AuthenticationService,
    private service: RkadService,
    private notif: NotifService,
    private canActive: CanActiveGuardService,
    private fb: FormBuilder,
    private unitService: DaftunitService
  ) {
    this.canActive.titleComponent$.subscribe(resp => this.title = resp);
    this.userInfo = this.auth.getTokenInfo();
    this.uiUnit = { kode: '', nama: '' };
    this.formFilter = this.fb.group({
      idunit: [0, [Validators.required, Validators.min(1)]],
      kdtahap: '341'
    });
    if(this.userInfo.Idunit != 0){
      this.unitService.get(this.userInfo.Idunit).subscribe(resp => {
        this.callBackDaftunit(resp);
      },error => {
        this.loading = false;
        if(Array.isArray(error.error.error)){
          for(var i = 0; i < error.error.error.length; i++){
            this.notif.error(error.error.error[i]);
          }
        } else {
          this.notif.error(error.error);
        }
      });
    }
  }
  ngOnInit() {
  }
  lookDaftunit() {
    this.Daftunit.title = 'Pilih SKPD';
    this.Daftunit.gets('3,4');
    this.Daftunit.showThis = true;
  }
  callBackDaftunit(e: IDaftunit) {
    this.unitSelected = e;
    this.uiUnit = { kode: this.unitSelected.kdunit, nama: this.unitSelected.nmunit };
    this.formFilter.patchValue({
      idunit: this.unitSelected.idunit
    });
    this.dataSelected = null;
    if (this.dt) this.dt.reset();
  }
  gets(event: LazyLoadEvent) {
    if (this.formFilter.valid) {
      this.totalNilai = 0;
      this.loading = true;
      this.listdata = [];
      this.service._start = event.first;
      this.service._rows = event.rows;
      this.service._globalFilter = event.globalFilter;
      this.service._sortField = event.sortField;
      this.service._sortOrder = event.sortOrder;
      this.service._idunit = this.formFilter.value.idunit;
      this.service._kdtahap = this.formFilter.value.kdtahap;
      this.service.paging().subscribe(resp => {
        if (resp.totalrecords > 0) {
          this.totalRecords = resp.totalrecords;
          this.listdata = resp.data;
          if (resp.optionalResult) {
            this.totalNilai = resp.optionalResult.totalNilai;
          }
        } else {
          this.notif.info('Data Tidak Tersedia');
        }
        this.isvalid = resp.isvalid;
        this.loading = false;
      }, error => {
        this.loading = false;
        if (Array.isArray(error.error.error)) {
          for (var i = 0; i < error.error.error.length; i++) {
            this.notif.error(error.error.error[i]);
          }
        } else {
          this.notif.error(error.error);
        }
      });
    }
  }
  callback(e: any) {
    if (e.added) {
      if (this.dt) this.dt.reset();
    }
    this.dataSelected = null;
  }
  add() {
    this.Form.title = 'Tambah Data';
    this.Form.mode = 'add';
    this.Form.forms.patchValue({
      idunit: this.formFilter.value.idunit,
      kdtahap: this.formFilter.value.kdtahap
    });
    this.Form.showThis = true;
  }
  onRowEditInit(e: IRkad) {
    this.nilaiInit = e.nilai;
  }
  onRowEditSave(e: IRkad) {
    this.service.put(e).subscribe(resp => {
      if (resp.ok) {
        if (this.dt) this.dt.reset();
      }
      this.notif.success('Nilai Berhasil Diubah');
    }, error => {
      this.loading = false;
      if (Array.isArray(error.error.error)) {
        for (var i = 0; i < error.error.error.length; i++) {
          this.notif.error(error.error.error[i]);
        }
      } else {
        this.notif.error(error.error);
      }
    });
  }
  onRowEditCancel(e: IRkad, index: number) {
    this.listdata.map(m => {
      if(m.idrkad == e.idrkad){
        m.nilai = this.nilaiInit;
      }
    });
  }
  delete(e: IRkad) {
    this.notif.confir({
      message: `${e.idrekNavigation.kdper.trim()} - ${e.idrekNavigation.nmper.trim()} Akan Dihapus ?`,
      accept: () => {
        this.service.delete(e.idrkad).subscribe(
          (resp) => {
            if (resp.ok) {
              this.notif.success('Data berhasil dihapus');
              if (this.dt) this.dt.reset();
            }
          }, (error) => {
            if (Array.isArray(error.error.error)) {
              for (var i = 0; i < error.error.error.length; i++) {
                this.notif.error(error.error.error[i]);
              }
            } else {
              this.notif.error(error.error);
            }
          });
      },
      reject: () => {
        return false;
      }
    });
  }
  subTotal(){
    let total = 0;
		if(this.listdata.length > 0){
			this.listdata.forEach(f => total += +f.nilai);
		}
		return total;
  }
  dataKlick(e: IRkad) {
    this.dataSelected = e;
  }
  callbackChild(e: any){
    if(typeof e === 'object' && e !== null){
      this.listdata.map(m => {
        if(m.idrkad == e.idrka){
          m.nilai = e.nilai;
        }
      });
      this.subTotal();
      this.totalNilai = e.grandTotalChild;
    }
  }
  ngOnDestroy(): void {
    this.listdata = [];
    this.uiUnit = { kode: '', nama: '' };
    this.unitSelected = null;
    this.dataSelected = null;
    this.totalRecords = 0;
    this.totalNilai = 0;
    this.isvalid = false;
  }
}
