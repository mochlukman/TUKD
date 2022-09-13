import { Component, Input, OnChanges, OnDestroy, OnInit, SimpleChanges, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Subscription } from 'rxjs';
import { IDaftunit } from 'src/app/core/interface/idaftunit';
import { IDisplayGlobal } from 'src/app/core/interface/iglobal';
import { ISpm } from 'src/app/core/interface/ispm';
import { ITokenClaim } from 'src/app/core/interface/itoken-claim';
import { AuthenticationService } from 'src/app/core/services/auth.service';
import { DaftunitService } from 'src/app/core/services/daftunit.service';
import { SpmService } from 'src/app/core/services/spm.service';
import { StattrsService } from 'src/app/core/services/stattrs.service';
import { NotifService } from 'src/app/core/_commonServices/notif.service';
import { LookDaftunitComponent } from 'src/app/shared/lookups/look-daftunit/look-daftunit.component';
import { LookTrackingDataComponent } from 'src/app/shared/lookups/look-tracking-data/look-tracking-data.component';
import { SpmUpFormComponent } from '../spm-up-form/spm-up-form.component';
import { ReportModalComponent } from 'src/app/shared/modals/report-modal/report-modal.component';
import { ReportService } from 'src/app/core/services/report.service';

@Component({
  selector: 'app-spm-up-pembuatan',
  templateUrl: './spm-up-pembuatan.component.html',
  styleUrls: ['./spm-up-pembuatan.component.scss']
})
export class SpmUpPembuatanComponent implements OnInit, OnDestroy, OnChanges {
  @Input() tabIndex: number = 0;
  uiUnit: IDisplayGlobal;
  unitSelected: IDaftunit;
  userInfo: ITokenClaim;
  loading: boolean;
  loading_rek: boolean;
  listdata: ISpm[] = [];
  dataSelected: ISpm = null;
  @ViewChild(LookDaftunitComponent, {static: true}) Daftunit : LookDaftunitComponent;
  @ViewChild(SpmUpFormComponent, {static: true}) Form : SpmUpFormComponent;
  @ViewChild(LookTrackingDataComponent, {static: true}) Tracking : LookTrackingDataComponent;
  @ViewChild(ReportModalComponent,{static: true}) showTanggal: ReportModalComponent;
  @ViewChild('dt',{static:false}) dt: any;
  spmSelected: ISpm;
  formFilter: FormGroup;
  initialForm: any;
  constructor(
    private auth: AuthenticationService,
    private service: SpmService,
    private notif: NotifService,
    private stattrsService: StattrsService,
    private unitService: DaftunitService,
    private fb: FormBuilder,
    private reportService: ReportService
  ) {
    this.formFilter = this.fb.group({
      idunit: [0, [Validators.required, Validators.min(0)]],
      kdstatus : '21',
      idxkode : 6,
    });
    this.initialForm = this.formFilter.value;
    this.userInfo = this.auth.getTokenInfo();
    this.uiUnit = {kode: '', nama: ''};
  }
  ngOnChanges(changes: SimpleChanges): void {
    if(this.tabIndex == 0){
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
    } else {
      this.ngOnDestroy();
    }
  }

  ngOnInit() {
  }
  lookDaftunit(){
    this.Daftunit.title = 'Pilih Unit Organisasi';
    this.Daftunit.gets('3,4');
    this.Daftunit.showThis = true;
  }
  callBackDaftunit(e: IDaftunit){
    this.unitSelected = e;
    this.uiUnit = {kode: this.unitSelected.kdunit, nama: this.unitSelected.nmunit};
    this.formFilter.patchValue({
      idunit: this.unitSelected.idunit
    });
    this.dataSelected = null;
    this.get();
  }
  get(){
    if(this.formFilter.valid && this.tabIndex == 0){
      if(this.dt) this.dt.reset();
      this.loading = true;
      this.listdata = [];
      this.service._idunit = this.formFilter.value.idunit;
      this.service._kdstatus = this.formFilter.value.kdstatus;
      this.service._idxkode = this.formFilter.value.idxkode;
      this.service.gets()
        .subscribe(resp => {
          if(resp.length > 0){
            this.listdata = [...resp];
          } else {
            this.notif.info('Data Tidak Tersedia');
          }
          this.loading = false;
        }, (error) => {
          this.loading = false;
          if(Array.isArray(error.error.error)){
            for(let i = 0; i < error.error.error.length; i++){
              this.notif.error(error.error.error[i]);
            }
          } else {
            this.notif.error(error.error);
          }
        });
    }
  }
  callback(e: any){
    if(e.added){
      this.listdata.push(e.data);
      if(this.dt) this.dt.reset();
    } else if(e.edited){
      let index = this.listdata.findIndex(f => f.idspp === e.data.idspp);
      this.listdata = this.listdata.filter(f => f.idspp != e.data.idspp);
      this.listdata.splice(index, 0, e.data);
      if(this.dt) this.dt.resetPageOnSort = false;
    }
  }
  add(){
    if(this.formFilter.valid){
      this.service.getNoreg(this.formFilter.value.idunit)
        .subscribe(resp => {
          if(resp.noreg){
            let noreg =  resp.noreg;
            this.stattrsService.get(this.formFilter.value.kdstatus)
            .subscribe(resp => {
              this.Form.forms.patchValue({
                idunit : this.formFilter.value.idunit,
                kdstatus : this.formFilter.value.kdstatus,
                idxkode : this.formFilter.value.idxkode,
                nospm: `${noreg}/SPM-${resp.lblstatus}/${this.unitSelected.kdunit}/${this.userInfo.NmTahun}`,
                noreg: noreg
              });
              this.Form.nmstatus = resp.lblstatus.trim();
              this.Form.title = 'Tambah';
              this.Form.mode = 'add';
              this.Form.showThis = true;
            },(error) => {
              if(Array.isArray(error.error.error)){
                for(var i = 0; i < error.error.error.length; i++){
                  this.notif.error(error.error.error[i]);
                }
              } else {
                this.notif.error(error.error);
              }
            });
          }
        }, (error) => {
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
  update(e: ISpm){
    this.Form.forms.patchValue({
      idspm : e.idspm,
      idspp : e.idspp,
      idspd : e.idspd,
      idunit : e.idunit,
      nospm : e.nospm,
      kdstatus : e.kdstatus,
      idbend : e.idbend,
      idxkode : e.idxkode,
      noreg : e.noreg,
      ketotor : e.ketotor,
      keperluan : e.keperluan,
      tglspm : e.tglspm != null ? new Date(e.tglspm) : null,
    });
    if(e.idsppNavigation){
      this.Form.uiSpp = {kode: e.idsppNavigation.nospp, nama: e.idsppNavigation.tglspp !== null ? new Date(e.idsppNavigation.tglspp).toLocaleDateString() : ''};
      this.Form.sppSelected = e.idsppNavigation;
      this.Form.forms.patchValue({
        nilaiup: new Intl.NumberFormat('ID').format(e.idsppNavigation.nilaiup)
      });
      if(e.idsppNavigation.idspdNavigation){
        this.Form.forms.patchValue({
          nospd: e.idsppNavigation.idspdNavigation.nospd,
          tglspd: e.idsppNavigation.idspdNavigation.tglspd != null ? new Date(e.idsppNavigation.idspdNavigation.tglspd).toLocaleDateString() : '',
        });
      }
    }
    if(e.idbendNavigation){
      this.Form.bendSelected = e.idbendNavigation;
      this.Form.uiBend = {
        kode: e.idbendNavigation.idpegNavigation.nip, 
        nama: e.idbendNavigation.idpegNavigation.nama + ',' + e.idbendNavigation.jnsbendNavigation.jnsbend.trim() + ' - ' + e.idbendNavigation.jnsbendNavigation.uraibend.trim()
      };
    }
    this.Form.title = 'Ubah';
    this.Form.mode = 'edit';
    this.Form.isvalid = e.valid;
    this.Form.showThis = true;
  }
  print(e: ISpm){
    this.spmSelected = e;
    this.showTanggal.useTgl = true;
		this.showTanggal.useHal = true;
		this.showTanggal.showThis = true;
  }
  callBackTanggal(e: any){
    let parameterReport = {
      Type: 1,
      FileName: 'spm.rpt',
      Params: {
        '@idspm': this.spmSelected.idspm,
        '@tanggal': e.TGL,
        '@hal':e.halaman
      }
    };
    this.reportService.execPrint(parameterReport).subscribe((resp) => {
      this.reportService.extractData(resp, 1, `laporan_${this.spmSelected.nospm}`);
    });
	}
  delete(e: ISpm){
    this.notif.confir({
			message: `${e.nospm} Akan Dihapus ?`,
			accept: () => {
				this.service.delete(e.idspm).subscribe(
					(resp) => {
						if (resp.ok) {
              this.listdata = this.listdata.filter(f => f.idspm !== e.idspm);
              this.notif.success('Data berhasil dihapus');
              if(this.dt) this.dt.reset();
						}
					}, (error) => {
            if(Array.isArray(error.error.error)){
              for(var i = 0; i < error.error.error.length; i++){
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
  dataKlick(e: ISpm){
		this.dataSelected = e;
	}
  lookTracking(e: ISpm){
    this.Tracking.title = `Status Data SPM : ${e.nospm}`;
    this.Tracking._typedata = 'spm';
    this.Tracking._iddata = e.idspm;
    this.Tracking.showThis = true;
  }
  ngOnDestroy(): void{
    this.formFilter.reset(this.initialForm);
    this.listdata = [];
		this.uiUnit = { kode: '', nama: '' };
		this.unitSelected = null;
		this.dataSelected = null;
  }
}