import { Component, EventEmitter, OnInit, Output, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Message } from 'primeng/api';
import { IDisplayGlobal } from 'src/app/core/interface/iglobal';
import { IKontrak } from 'src/app/core/interface/ikontrak';
import { IStattrs } from 'src/app/core/interface/istattrs';
import { indoKalender } from 'src/app/core/local';
import { TagihanService } from 'src/app/core/services/tagihan.service';
import { NotifService } from 'src/app/core/_commonServices/notif.service';
import { LookKontrakComponent } from 'src/app/shared/lookups/look-kontrak/look-kontrak.component';
import { LookStatusComponent } from 'src/app/shared/lookups/look-status/look-status.component';

@Component({
  selector: 'app-form-tagihan',
  templateUrl: './form-tagihan.component.html',
  styleUrls: ['./form-tagihan.component.scss']
})
export class FormTagihanComponent implements OnInit {
  loading_post: boolean;
  uiKontrak: IDisplayGlobal;
  kontrakSelected: IKontrak;
  showThis: boolean;
  title: string;
  mode: string;
  msg: Message[];
  forms: FormGroup;
  @Output() callBack = new EventEmitter();
  indoKalender: any;
  @ViewChild(LookKontrakComponent, {static: true}) Kontrak : LookKontrakComponent;
  constructor(
    private service: TagihanService,
    private fb: FormBuilder,
    private notif: NotifService
  ) {
    this.forms = this.fb.group({
      idtagihan : 0,
      idunit :  [0, Validators.required],
      idkeg :  [0, Validators.required],
      notagihan : ['', Validators.required],
      tgltagihan : null,
      idkontrak :  [0, Validators.required],
      uraiantagihan :  '',
      kdstatus :  '',
    });
    this.uiKontrak = {kode: '', nama: ''};
    this.indoKalender = indoKalender;
  }
  ngOnInit(){
  }
  lookKontrak(){
    this.Kontrak.title = 'Pilih Kontrak';
    this.Kontrak.gets(this.forms.value.idunit, this.forms.value.idkeg);
    this.Kontrak.showThis = true;
  }
  callbackKontrak(e: IKontrak){
    this.kontrakSelected = e;
    this.uiKontrak = {kode: e.nokontrak, nama: e.uraian};
    this.forms.patchValue({
      idkontrak: e.idkontrak
    });
  }
  simpan(){
    if(this.forms.valid){
      this.loading_post = true;
      this.forms.patchValue({
        tgltagihan: this.forms.value.tgltagihan !== null ? new Date(this.forms.value.tgltagihan).toLocaleDateString() : null
      });
      if(this.mode === 'add'){
        this.service.post(this.forms.value).subscribe((resp) => {
          if (resp.ok) {
            this.callBack.emit({
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
            this.callBack.emit({
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
  }
  onHide(){
    this.uiKontrak = {kode: '', nama: ''};
    this.forms.reset();
    this.forms.patchValue({
      idtagihan : 0,
      idunit : 0,
      idkeg : 0,
      notagihan : '',
      tgltagihan : null,
      idkontrak : 0,
      kdstatus: ''
    });
    this.showThis = false;
    this.msg = [];
    this.loading_post = false;
    this.kontrakSelected = null;
  }
}
