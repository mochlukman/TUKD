import { Component, EventEmitter, OnInit, Output, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Message } from 'primeng/api';
import { IDaftphk3 } from 'src/app/core/interface/idaftphk3';
import { IDisplayGlobal } from 'src/app/core/interface/iglobal';
import { ITokenClaim } from 'src/app/core/interface/itoken-claim';
import { indoKalender } from 'src/app/core/local';
import { AuthenticationService } from 'src/app/core/services/auth.service';
import { SpmService } from 'src/app/core/services/spm.service';
import { StattrsService } from 'src/app/core/services/stattrs.service';
import { NotifService } from 'src/app/core/_commonServices/notif.service';
import { LookPhk3Component } from 'src/app/shared/lookups/look-phk3/look-phk3.component';

@Component({
  selector: 'app-pembiayaan-pengajuan-form',
  templateUrl: './pembiayaan-pengajuan-form.component.html',
  styleUrls: ['./pembiayaan-pengajuan-form.component.scss']
})
export class PembiayaanPengajuanFormComponent implements OnInit {
  loading_post: boolean;
  showThis: boolean;
  title: string;
  mode: string;
  msg: Message[];
  forms: FormGroup;
  @Output() callback = new EventEmitter();
  indoKalender: any;
  nmstatus: string;
  userInfo: ITokenClaim;
  validasi: boolean;
  uiPhk3: IDisplayGlobal;
  phk3Selected: IDaftphk3;
  @ViewChild(LookPhk3Component, {static: true}) Phk3 : LookPhk3Component;
  initialform: any;
  constructor(
    private service: SpmService,
    private fb: FormBuilder,
    private notif: NotifService,
    private authService: AuthenticationService,
    private stattrsService: StattrsService
  ) {
    this.userInfo = this.authService.getTokenInfo();
    this.forms = this.fb.group({
      idspm: 0,
      idunit : [0, [Validators.required, Validators.min(1)]],
      nospm : ['', Validators.required],
      kdstatus : ['', Validators.required],
      idbend : null,
      idphk3 : null,
      idxkode : [0, Validators.required],
      noreg : '',
      ketotor : '',
      idkontrak : null,
      keperluan : '',
      tglspm : null,
      nospd: '',
      tglspd: null,
      nokontrak: '',
      nmphk3: '',
      nminst: ''
    });
    this.indoKalender = indoKalender;
    this.uiPhk3 = {kode: '', nama: ''};
    this.initialform = this.forms.value;
  }

  ngOnInit(){
  }
  lookPhk3(){
    this.Phk3.title = 'Pilih Pihak Ketiga / Rekanan';
    this.Phk3.gets(this.forms.value.idunit);
    this.Phk3.showThis = true;
  }
  callbackPhk3(e: any){
    this.phk3Selected = e;
    this.uiPhk3 = {kode: this.phk3Selected.nmphk3, nama: this.phk3Selected.nminst};
    this.forms.patchValue({
      idphk3: this.phk3Selected.idphk3
    });
  }
  simpan(){
    if(this.forms.valid){
      this.loading_post = true;
      this.forms.patchValue({
        tglspm: this.forms.value.tglspm !== null ? new Date(this.forms.value.tglspm).toLocaleDateString() : null
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
    if(this.mode == 'edit'){
      this.stattrsService.get(this.forms.value.kdstatus.trim())
          .subscribe(resp => this.nmstatus = resp.lblstatus.trim());
    }
  }
  onHide(){
    this.forms.reset(this.initialform);
    this.showThis = false;
    this.msg = [];
    this.loading_post = false;
    this.nmstatus = '';
    this.validasi = false;
    this.uiPhk3 = {kode: '', nama: ''};
    this.phk3Selected = null;
  }
}