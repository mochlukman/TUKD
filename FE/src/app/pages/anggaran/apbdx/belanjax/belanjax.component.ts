import { Component, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { LazyLoadEvent } from 'primeng/api';
import { IDaftunit } from 'src/app/core/interface/idaftunit';
import { IDisplayGlobal, ILookupTree } from 'src/app/core/interface/iglobal';
import { IRkar } from 'src/app/core/interface/irkar';
import { ITokenClaim } from 'src/app/core/interface/itoken-claim';
import { InputRupiahPipe } from 'src/app/core/pipe/input-rupiah.pipe';
import { AuthenticationService } from 'src/app/core/services/auth.service';
import { DaftunitService } from 'src/app/core/services/daftunit.service';
import { RkarService } from 'src/app/core/services/rkar.service';
import { CanActiveGuardService } from 'src/app/core/_commonServices/can-active-guard.service';
import { NotifService } from 'src/app/core/_commonServices/notif.service';
import { LookDaftunitComponent } from 'src/app/shared/lookups/look-daftunit/look-daftunit.component';
import { LookMkegiatanByKegunitComponent } from 'src/app/shared/lookups/look-mkegiatan-by-kegunit/look-mkegiatan-by-kegunit.component';
import { RkarFormxComponent } from './rkar-formx/rkar-formx.component';
import { RkardanaxComponent } from './rkardanax/rkardanax.component';

@Component({
  selector: 'app-belanjax',
  templateUrl: './belanjax.component.html',
  styleUrls: ['./belanjax.component.scss'],
  providers: [InputRupiahPipe]
})
export class BelanjaxComponent implements OnInit, OnDestroy {
  title: string = '';
  uiUnit: IDisplayGlobal;
  uiKeg: IDisplayGlobal;
  unitSelected: IDaftunit;
  kegSelected: ILookupTree;
  userInfo: ITokenClaim;
  loading: boolean;
  listdata: IRkar[] = [];
  totalRecords: number;
  totalNilai: number = 0;
  dataSelected: IRkar = null;
  formFilter: FormGroup;
  initialForm: any;
  nilaiInit: number;
  @ViewChild(LookDaftunitComponent, { static: true }) Daftunit: LookDaftunitComponent;
  @ViewChild(RkarFormxComponent, { static: true }) Form: RkarFormxComponent;
  @ViewChild(LookMkegiatanByKegunitComponent, {static: true}) Kegiatan : LookMkegiatanByKegunitComponent;
  @ViewChild('dt', { static: false }) dt: any;
  @ViewChild(RkardanaxComponent, {static: true}) SumberDana : RkardanaxComponent;
  isvalid: boolean = false;
  constructor(
    private auth: AuthenticationService,
    private service: RkarService,
    private notif: NotifService,
    private canActive: CanActiveGuardService,
    private fb: FormBuilder,
    private unitService: DaftunitService
  ) {
    this.canActive.titleComponent$.subscribe(resp => this.title = resp);
    this.userInfo = this.auth.getTokenInfo();
    this.uiUnit = { kode: '', nama: '' };
    this.uiKeg = {kode: '', nama: ''};
    this.formFilter = this.fb.group({
      idunit: [0, [Validators.required, Validators.min(1)]],
      kdtahap: '341',
      idkeg: [0, [Validators.required, Validators.min(1)]],
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
  lookKegiatan(){
    if(this.unitSelected){
      this.Kegiatan.title = 'Pilih Kegiatan';
      this.Kegiatan._idunit = this.formFilter.value.idunit;
      this.Kegiatan._kdtahap = this.formFilter.value.kdtahap;
      this.Kegiatan._type = 'subkegiatan';
      this.Kegiatan.get();
      this.Kegiatan.showThis = true;
    } else {
      this.notif.warning('Pilih SKPD');
    }
    
  }
  callBackKegiatan(e: ILookupTree){
    this.kegSelected = e;
    let split_label = this.kegSelected.label.split('-');
    this.uiKeg = {kode: split_label[0], nama: split_label[1]};
    this.formFilter.patchValue({
      idkeg: this.kegSelected.data_id
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
      this.service._idkeg = this.formFilter.value.idkeg;
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
      kdtahap: this.formFilter.value.kdtahap,
      idkeg: this.formFilter.value.idkeg
    });
    this.Form.showThis = true;
  }
  onRowEditInit(e: IRkar) {
    this.nilaiInit = e.nilai;
  }
  onRowEditSave(e: IRkar) {
    this.service.put(e).subscribe(resp => {
      if (resp.ok) {
        if (this.dt) this.dt.reset();
      }
      this.notif.success('Nilai Berhasil Diubah');
    }, error => {
      this.listdata.map(m => {
        if(m.idrkar == e.idrkar){
          m.nilai = this.nilaiInit;
        }
      });
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
  onRowEditCancel(e: IRkar, index: number) {
    this.listdata.map(m => {
      if(m.idrkar == e.idrkar){
        m.nilai = this.nilaiInit;
      }
    });
  }
  delete(e: IRkar) {
    this.notif.confir({
      message: `${e.idrekNavigation.kdper.trim()} - ${e.idrekNavigation.nmper.trim()} Akan Dihapus ?`,
      accept: () => {
        this.service.delete(e.idrkar).subscribe(
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
  dataKlick(e: IRkar) {
    this.dataSelected = e;
  }
  callbackChild(e: any){
    if(typeof e === 'object' && e !== null){
      this.listdata.map(m => {
        if(m.idrkar == e.idrka){
          m.nilai = e.nilai;
        }
      });
      this.subTotal();
      this.totalNilai = e.grandTotalChild;
    }
  }
  getSumberDana(e: IRkar){
    this.SumberDana.rkaSelected = e;
    this.SumberDana.title = 'Sumber Dana';
    this.SumberDana.isvalid = this.isvalid;
    this.SumberDana.showThis = true;
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
