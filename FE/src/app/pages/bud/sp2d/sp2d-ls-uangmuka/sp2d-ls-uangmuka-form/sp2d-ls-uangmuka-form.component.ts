import { Component, EventEmitter, OnInit, Output, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Message, SelectItem } from 'primeng/api';
import { Ibend } from 'src/app/core/interface/ibend';
import { IDisplayGlobal } from 'src/app/core/interface/iglobal';
import { ISpm } from 'src/app/core/interface/ispm';
import { ITokenClaim } from 'src/app/core/interface/itoken-claim';
import { indoKalender } from 'src/app/core/local';
import { AuthenticationService } from 'src/app/core/services/auth.service';
import { JabttdService } from 'src/app/core/services/jabttd.service';
import { Sp2dService } from 'src/app/core/services/sp2d.service';
import { StattrsService } from 'src/app/core/services/stattrs.service';
import { NotifService } from 'src/app/core/_commonServices/notif.service';
import { LookBendaharaComponent } from 'src/app/shared/lookups/look-bendahara/look-bendahara.component';
import { LookSpmComponent } from 'src/app/shared/lookups/look-spm/look-spm.component';

@Component({
  selector: 'app-sp2d-ls-uangmuka-form',
  templateUrl: './sp2d-ls-uangmuka-form.component.html',
  styleUrls: ['./sp2d-ls-uangmuka-form.component.scss']
})
export class Sp2dLsUangmukaFormComponent implements OnInit {
  loading_post: boolean;
  uiSpm: IDisplayGlobal;
  spmSelected: ISpm;
  uiSpd: IDisplayGlobal;
  uiBend: IDisplayGlobal;
  bendSelected: Ibend;
  showThis: boolean;
  title: string;
  mode: string;
  msg: Message[];
  forms: FormGroup;
  @Output() callback = new EventEmitter();
  indoKalender: any;
  listBulan: SelectItem[] = [];
  bulanSelected: any;
  listJabttd: SelectItem[] = [];
  jabttdSelected: any;
  nmstatus: string;
  userInfo: ITokenClaim;
  @ViewChild(LookSpmComponent,{static: true}) Spm : LookSpmComponent;
  @ViewChild(LookBendaharaComponent, {static: true}) Bendahara : LookBendaharaComponent;
  initialValue: any;
  isvalid: boolean = false;
  constructor(
    private service: Sp2dService,
    private fb: FormBuilder,
    private notif: NotifService,
    private authService: AuthenticationService,
    private stattrsService: StattrsService,
    private jabttdService: JabttdService
  ) {
    this.userInfo = this.authService.getTokenInfo();
    this.uiSpm = {kode: '', nama: ''};
    this.uiBend = {kode: '', nama: ''};
    this.forms = this.fb.group({
      idsp2d : 0,
      idspm : [0, [Validators.required, Validators.min(1)]],
      idspd : [0, [Validators.required, Validators.min(1)]],
      idunit : [0, [Validators.required, Validators.min(1)]],
      nosp2d : ['', Validators.required],
      kdstatus : ['', Validators.required],
      idbend : null,
      idxkode : [0, Validators.required],
      noreg : '',
      ketotor : '',
      keperluan : '',
      tglsp2d : null,
      nospd: '',
      tglspd: null,
      idttd: 0,
      idphk3 : null,
      idkontrak : null,
      nokontrak: '',
      nmphk3: '',
      nminst: ''
    });
    this.initialValue = this.forms.value;
    this.indoKalender = indoKalender;
  }

  ngOnInit(){
  }
  lookBendahara(){
    this.Bendahara.title = 'Pilih Bendahara';
    this.Bendahara.gets(this.forms.value.idunit);
    this.Bendahara.showThis = true;
  }
  callBackBendahara(e: Ibend){
    this.bendSelected = e;
    this.forms.patchValue({
      idbend: this.bendSelected.idbend
    });
    this.uiBend = {
      kode: this.bendSelected.idpegNavigation.nip, 
      nama: this.bendSelected.idpegNavigation.nama + ',' + this.bendSelected.jnsbendNavigation.jnsbend.trim() + ' - ' + this.bendSelected.jnsbendNavigation.uraibend.trim()
    };
  }
  getJabttd(){
    this.listJabttd = [
      {label: 'Pilih', value: null}
    ];
    this.jabttdService.gets('04.301')
      .subscribe(resp => {
        if(resp.length > 0 ){
          resp.forEach(e => {
            this.listJabttd.push({label: `${e.idpegNavigation.nip} -  ${e.idpegNavigation.nama}`, value: e.idttd})
          });
        }
        if(this.mode === 'edit'){
          if(this.forms.value.idttd != null && this.forms.value.idttd != 0){
            this.jabttdSelected = this.listJabttd.find(w => w.value == this.forms.value.idttd).value;
          }
        }
      })
  }
  changejabttd(e: any){
    this.forms.patchValue({
      idttd:  e.value
    });
  }
  lookSpm(){
    this.Spm.title = 'Pilih SPM';
    this.Spm._kdstatus = this.forms.value.kdstatus;
    this.Spm._idxkode = this.forms.value.idxkode;
    this.Spm._idunit = this.forms.value.idunit;
    this.Spm.gets();
    this.Spm.showThis = true;
  }
  callbackSpm(e: ISpm){
    this.spmSelected = e;
    this.uiSpm = {kode: e.nospm, nama: e.tglspm !== null ? new Date(e.tglspm).toLocaleDateString() : ''};
    this.forms.patchValue({
      idspm: this.spmSelected.idspm,
      idspd: this.spmSelected.idsppNavigation.idspdNavigation.idspd,
      nospd: this.spmSelected.idsppNavigation.idspdNavigation.nospd,
      tglspd: this.spmSelected.idsppNavigation.idspdNavigation.tglspd != null ? new Date(this.spmSelected.idsppNavigation.idspdNavigation.tglspd).toLocaleDateString() : null,
      ketotor: this.spmSelected.ketotor,
      keperluan: this.spmSelected.keperluan,
      nokontrak: this.spmSelected.idkontrakNavigation ? this.spmSelected.idkontrakNavigation.nokontrak : '',
      nmphk3: this.spmSelected.idphk3Navigation ? this.spmSelected.idphk3Navigation.nmphk3 : '',
      nminst: this.spmSelected.idphk3Navigation ? this.spmSelected.idphk3Navigation.nminst : '',
      idphk3: this.spmSelected.idphk3Navigation ? this.spmSelected.idphk3Navigation.idphk3 : '',
      idkontrak: this.spmSelected.idkontrakNavigation ? this.spmSelected.idkontrakNavigation.idkontrak : null
    });
  }
  simpan(){
    if(this.forms.valid){
      this.loading_post = true;
      this.forms.patchValue({
        tglsp2d: this.forms.value.tglsp2d !== null ? new Date(this.forms.value.tglsp2d).toLocaleDateString() : null
      });
      if(this.mode === 'add'){
        this.service.post(this.forms.value).subscribe((resp) => {
          if (resp.ok) {
            this.callback.emit({
              added: true,
              data: resp.body
            });
            this.onHide();
            this.notif.success('Input Data Berhasil');
          }
          this.loading_post = false;
        }, (error) => {
          if(Array.isArray(error.error.error)){
            this.msg = [];
            for(var i = 0; i < error.error.error.length; i++){
              this.msg.push({severity: 'error', summary: 'error', detail: error.error.error[i]});
            }
          } else {
            this.msg = [];
            this.msg.push({severity: 'error', summary: 'error', detail: error.error});
          }
          this.loading_post = false;
        });
      } else if(this.mode === 'edit'){
        this.service.put(this.forms.value).subscribe((resp) => {
          if (resp.ok) {
            this.callback.emit({
              edited: true,
              data: resp.body
            });
            this.onHide();
            this.notif.success('Input Data Berhasil');
          }
          this.loading_post = false;
        }, (error) => {
          if(Array.isArray(error.error.error)){
            this.msg = [];
            for(var i = 0; i < error.error.error.length; i++){
              this.msg.push({severity: 'error', summary: 'error', detail: error.error.error[i]});
            }
          } else {
            this.msg = [];
            this.msg.push({severity: 'error', summary: 'error', detail: error.error});
          }
          this.loading_post = false;
        });
      }
    }
  }
  onShow(){
    this.getJabttd();
    if(this.mode == 'edit'){
      this.stattrsService.get(this.forms.value.kdstatus.trim())
        .subscribe(resp => this.nmstatus = resp.lblstatus.trim());
    }
  }
  onHide(){
    this.forms.reset(this.initialValue);
    this.showThis = false;
    this.msg = [];
    this.uiSpm = {kode: '', nama: ''};
    this.uiSpd = {kode: '', nama: ''};
    this.uiBend = {kode: '', nama: ''};
    this.spmSelected = null;
    this.bendSelected = null;
    this.loading_post = false;
    this.nmstatus = '';
    this.bulanSelected = null;
    this.listBulan = [];
    this.jabttdSelected = null;
    this.isvalid = false;
  }
}
