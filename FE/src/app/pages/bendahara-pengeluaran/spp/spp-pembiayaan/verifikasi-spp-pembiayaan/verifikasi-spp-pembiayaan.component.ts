import { Component, Input, OnChanges, OnDestroy, OnInit, SimpleChanges, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { IDaftunit } from 'src/app/core/interface/idaftunit';
import { IDisplayGlobal } from 'src/app/core/interface/iglobal';
import { ISpp } from 'src/app/core/interface/ispp';
import { ITokenClaim } from 'src/app/core/interface/itoken-claim';
import { AuthenticationService } from 'src/app/core/services/auth.service';
import { DaftunitService } from 'src/app/core/services/daftunit.service';
import { SppService } from 'src/app/core/services/spp.service';
import { StattrsService } from 'src/app/core/services/stattrs.service';
import { NotifService } from 'src/app/core/_commonServices/notif.service';
import { LookDaftunitComponent } from 'src/app/shared/lookups/look-daftunit/look-daftunit.component';
import { LookDpakegiatanComponent } from 'src/app/shared/lookups/look-dpakegiatan/look-dpakegiatan.component';
import { VerifikasiSppPembiayaanCheckComponent } from './verifikasi-spp-pembiayaan-check/verifikasi-spp-pembiayaan-check.component';
import { VerifikasiSppPembiayaanFormComponent } from './verifikasi-spp-pembiayaan-form/verifikasi-spp-pembiayaan-form.component';

@Component({
  selector: 'app-verifikasi-spp-pembiayaan',
  templateUrl: './verifikasi-spp-pembiayaan.component.html',
  styleUrls: ['./verifikasi-spp-pembiayaan.component.scss']
})
export class VerifikasiSppPembiayaanComponent implements OnInit, OnChanges, OnDestroy {
  @Input() tabIndex: number = 0;
  uiUnit: IDisplayGlobal;
  unitSelected: IDaftunit;
  userInfo: ITokenClaim;
  loading: boolean;
  loading_rek: boolean;
  listdata: ISpp[] = [];
  dataSelected: ISpp = null;
  @ViewChild(LookDaftunitComponent, {static: true}) Daftunit : LookDaftunitComponent;
  @ViewChild(LookDpakegiatanComponent, {static: true}) Kegiatan : LookDpakegiatanComponent;
  @ViewChild(VerifikasiSppPembiayaanFormComponent, {static: true}) Form : VerifikasiSppPembiayaanFormComponent;
  @ViewChild(VerifikasiSppPembiayaanCheckComponent, {static: true}) Checks: VerifikasiSppPembiayaanCheckComponent;
  @ViewChild('dt',{static:false}) dt: any;
  formFilter: FormGroup;
  initialForm: any;
  constructor(
    private auth: AuthenticationService,
    private service: SppService,
    private notif: NotifService,
    private stattrsService: StattrsService,
    private fb: FormBuilder,
    private unitService: DaftunitService
  ) {
    this.userInfo = this.auth.getTokenInfo();
    this.uiUnit = {kode: '', nama: ''};
    this.formFilter = this.fb.group({
      idunit: [0, [Validators.required, Validators.min(0)]],
      kdstatus: '24',
      idxkode: 5
    });
    this.initialForm = this.formFilter.value;
  }
  ngOnChanges(changes: SimpleChanges): void {
    if(this.tabIndex == 1){
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
    if(this.formFilter.valid && this.tabIndex == 1){
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
  update(e: ISpp){
    this.Form.forms.patchValue({
      idspp : e.idspp,
      idunit : e.idunit,
      nospp : e.nospp,
      kdstatus : e.kdstatus,
      idbulan : e.idbulan,
      idbend : e.idbend,
      idspd : e.idspd,
      idphk3 : e.idphk3,
      idxkode : e.idxkode,
      noreg : e.noreg,
      ketotor : e.ketotor,
      idkontrak : e.idkontrak,
      keperluan : e.keperluan,
      tglspp : e.tglspp !== null ? new Date(e.tglspp) : new Date(),
      status : e.status,
      nilaiup: e.nilaiup,
      tglvalid : e.tglvalid !== null ? new Date(e.tglvalid) : new Date(),
      valid: e.valid,
      validasi: e.validasi
    });
    if(e.idspdNavigation){
      this.Form.uiSpd = {kode: e.idspdNavigation.nospd, nama: e.idspdNavigation.tglspd !== null ? new Date(e.idspdNavigation.tglspd).toLocaleDateString() : ''};
      this.Form.spdSelected = e.idspdNavigation;
    }
    if(e.idbendNavigation){
      this.Form.uiBend = {
        kode: e.idbendNavigation.idpegNavigation.nip, 
        nama: e.idbendNavigation.idpegNavigation.nama + ',' + e.idbendNavigation.jnsbendNavigation.jnsbend.trim() + ' - ' + e.idbendNavigation.jnsbendNavigation.uraibend.trim()
      }
      this.Form.bendSelected = e.idbendNavigation;
    }
    this.Form.title = 'Pengesahan Data';
    this.Form.mode = 'edit';
    this.Form.isstatus = e.status;
    this.Form.showThis = true;
  }
  delete(e: ISpp){
    let postBody = {
      idspp: e.idspp,
      nospp: e.nospp,
      kdstatus: e.kdstatus,
      tglspp: e.tglspp,
      tglvalid: null,
      verifikasi: '',
      valid: null,
      validasi: ''
    };
    this.notif.confir({
      message: `Batalkan Pengesahan ?`,
      accept: () => {
        this.service.pengesahan(postBody).subscribe(
          (resp) => {
            if (resp.ok) {
              this.callback({
                edited: true,
                data: resp.body
              });
              this.notif.success('Pengesahan Berhasil Dibatalkan');
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
  dataKlick(e: ISpp){
    this.dataSelected = e;
	}
  lookCheck(e: ISpp){
    this.Checks.title = 'Check List Dokumen';
    this.Checks._idspp = e.idspp;
    this.Checks._idxkode = e.idxkode;
    this.Checks.showThis = true;
  }
  ngOnDestroy(): void{
    this.listdata = [];
		this.uiUnit = { kode: '', nama: '' };
		this.unitSelected = null;
		this.dataSelected = null;
    if(this.formFilter) this.formFilter.reset(this.initialForm);
  }
}