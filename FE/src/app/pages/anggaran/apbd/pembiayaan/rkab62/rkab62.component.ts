import { Component, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { LazyLoadEvent } from 'primeng/api';
import { Subscription } from 'rxjs';
import { IDaftunit } from 'src/app/core/interface/idaftunit';
import { IDisplayGlobal } from 'src/app/core/interface/iglobal';
import { IRkab } from 'src/app/core/interface/irkab';
import { ITokenClaim } from 'src/app/core/interface/itoken-claim';
import { InputRupiahPipe } from 'src/app/core/pipe/input-rupiah.pipe';
import { AuthenticationService } from 'src/app/core/services/auth.service';
import { DaftunitService } from 'src/app/core/services/daftunit.service';
import { RkabService } from 'src/app/core/services/rkab.service';
import { NotifService } from 'src/app/core/_commonServices/notif.service';
import { LookDaftunitComponent } from 'src/app/shared/lookups/look-daftunit/look-daftunit.component';
import { Rkab62FormComponent } from './rkab62-form/rkab62-form.component';
import { ReportModalComponent } from 'src/app/shared/modals/report-modal/report-modal.component';
import { ReportService } from 'src/app/core/services/report.service';

@Component({
  selector: 'app-rkab62',
  templateUrl: './rkab62.component.html',
  styleUrls: ['./rkab62.component.scss'],
  providers: [InputRupiahPipe]
})
export class Rkab62Component implements OnInit, OnDestroy {
  uiUnit: IDisplayGlobal;
  unitSelected: IDaftunit;
  userInfo: ITokenClaim;
  loading: boolean;
  listdata: IRkab[] = [];
  totalRecords: number;
  totalNilai: number = 0;
  dataSelected: IRkab = null;
  formFilter: FormGroup;
  initialForm: any;
  nilaiInit: number;
  @ViewChild(LookDaftunitComponent, { static: true }) Daftunit: LookDaftunitComponent;
  @ViewChild(Rkab62FormComponent, { static: true }) Form: Rkab62FormComponent;
  @ViewChild(ReportModalComponent,{static: true}) showTanggal: ReportModalComponent;
  @ViewChild('dt', { static: false }) dt: any;
  indexTab : number = 0;
  isvalid: boolean = false;
  constructor(
    private reportService: ReportService,
    private auth: AuthenticationService,
    private service: RkabService,
    private notif: NotifService,
    private fb: FormBuilder,
    private unitService: DaftunitService
  ) {
    this.userInfo = this.auth.getTokenInfo();
    this.uiUnit = { kode: '', nama: '' };
    this.formFilter = this.fb.group({
      idunit: [0, [Validators.required, Validators.min(1)]],
      kdtahap: '321',
      trkr: 2 // pengeluaran
    });
    this.service._tabIndex.subscribe(resp => {
      this.indexTab = resp;
      if(this.indexTab == 1){
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
    });
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
    if (this.formFilter.valid && this.indexTab == 1) {
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
      this.service._trkr = this.formFilter.value.trkr;
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
  onRowEditInit(e: IRkab) {
    this.nilaiInit = e.nilai;
  }
  onRowEditSave(e: IRkab) {
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
  onRowEditCancel(e: IRkab, index: number) {
    this.listdata.map(m => {
      if(m.idrkab == e.idrkab){
        m.nilai = this.nilaiInit;
      }
    });
  }
  delete(e: IRkab) {
    this.notif.confir({
      message: `${e.idrekNavigation.kdper.trim()} - ${e.idrekNavigation.nmper.trim()} Akan Dihapus ?`,
      accept: () => {
        this.service.delete(e.idrkab).subscribe(
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
  dataKlick(e: IRkab) {
    this.dataSelected = e;
  }
  callbackChild(e: any){
    if(typeof e === 'object' && e !== null){
      this.listdata.map(m => {
        if(m.idrkab == e.idrka){
          m.nilai = e.nilai;
        }
      });
      this.subTotal();
      this.totalNilai = e.grandTotalChild;
    }
  }

  print(e: IRkab){

    this.showTanggal.useTgl = true;
		this.showTanggal.useHal = true;
		this.showTanggal.showThis = true;
  }
  callBackTanggal(e: any){
    if (!this.unitSelected) {
      this.notif.warning('SKPD harus dipilih');
    } 
    else {
    let parameterReport = {
      Type: 1,
      FileName: 'RKA3.rpt',
      Params: {
        '@kdtahap': '321',
        '@idunit': this.unitSelected.idunit,
        '@tanggal': e.TGL,
        '@hal':e.halaman
            }
    };
      this.reportService.execPrint(parameterReport).subscribe((resp) => {
        this.reportService.extractData(resp, 1, `laporan_${this.unitSelected.kdunit}RKA_Pembiayaan`);
      });
    }
	}
  
  ngOnDestroy(): void {
    this.listdata = [];
    this.uiUnit = { kode: '', nama: '' };
    this.unitSelected = null;
    this.dataSelected = null;
    this.totalRecords = 0;
    this.totalNilai = 0;
    this.indexTab = 0;
    this.isvalid = false;
  }
}
