import { Component, EventEmitter, OnInit, Output, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Message } from 'primeng/api';
import { IDisplayGlobal } from 'src/app/core/interface/iglobal';
import { indoKalender } from 'src/app/core/local';
import { BkuPenerimaanService } from 'src/app/core/services/bku-penerimaan.service';
import { NotifService } from 'src/app/core/_commonServices/notif.service';
import { LookPelimpahanComponent } from 'src/app/shared/lookups/look-pelimpahan/look-pelimpahan.component';

@Component({
  selector: 'app-bku-penerimaan-form',
  templateUrl: './bku-penerimaan-form.component.html',
  styleUrls: ['./bku-penerimaan-form.component.scss']
})
export class BkuPenerimaanFormComponent implements OnInit {
  showThis: boolean;
  loading_post: boolean;
  title: string;
  mode: string;
  forms: FormGroup;
  msg: Message[];
  indoKalender: any;
  initialForm: any;
  uiRef: IDisplayGlobal;
  refSelected: any;
  @Output() callback = new EventEmitter();
  @ViewChild(LookPelimpahanComponent,{static: true}) ref: LookPelimpahanComponent;
  constructor(
    private service: BkuPenerimaanService,
    private notif: NotifService,
    private fb: FormBuilder
  ) { 
    this.indoKalender = indoKalender;
    this.forms = this.fb.group({
      idbku : 0,
      nobku : ['', Validators.required],
      idunit : [0, [Validators.required, Validators.min(1)]],
      tglbku : null,
      idref : [0, [Validators.required, Validators.min(1)]],
      uraian : [''],
      tglvalid : null,
      idbend : [0, [Validators.required, Validators.min(1)]],
      jenis : ['', Validators.required]
    });
    this.initialForm = this.forms.value;
    this.uiRef = {kode: '', nama: ''};
  }
  ngOnInit() {
  }
  lookRef(){
    this.ref.title = 'Pilih TBP';
    this.ref.getForbku(this.forms.value.idunit, this.forms.value.idbend);
    this.ref.showThis = true;
  }
  callbackRef(e: any){
    this.refSelected = e;
    this.uiRef = {kode: e.notbp, nama: e.uraitbp};
    this.forms.patchValue({
      idref: e.idtbp
    });
  }
  simpan(){
    if(this.forms.valid){
      this.loading_post = true;
      this.forms.patchValue({
        tglbku: this.forms.value.tglbku !== null ? new Date(this.forms.value.tglbku).toLocaleDateString() : null
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
      }
    }
  }
  onShow(){}
  onHide(){
    this.uiRef = {kode: '', nama: ''};
    this.forms.reset(this.initialForm);
    this.showThis = false;
  }
}
