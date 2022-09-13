import { Component, EventEmitter, OnInit, Output, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Message, SelectItem } from 'primeng/api';
import { IDaftunit } from 'src/app/core/interface/idaftunit';
import { IDisplayGlobal } from 'src/app/core/interface/iglobal';
import { IStattrs } from 'src/app/core/interface/istattrs';
import { ITokenClaim } from 'src/app/core/interface/itoken-claim';
import { indoKalender } from 'src/app/core/local';
import { AuthenticationService } from 'src/app/core/services/auth.service';
import { StattrsService } from 'src/app/core/services/stattrs.service';
import { TbpService } from 'src/app/core/services/tbp.service';
import { NotifService } from 'src/app/core/_commonServices/notif.service';
import { LookSkpComponent } from 'src/app/shared/lookups/look-skp/look-skp.component';
@Component({
  selector: 'app-penetapan-form',
  templateUrl: './penetapan-form.component.html',
  styleUrls: ['./penetapan-form.component.scss']
})
export class PenetapanFormComponent implements OnInit {
  uiSkp: IDisplayGlobal;
  skpSeleceted: any;
  showThis: boolean;
  loading_post: boolean;
  title: string;
  mode: string;
  forms: FormGroup
  msg: Message[];
  indoKalender: any;
  userInfo: ITokenClaim;
  initialForm: any;
  @Output() callback = new EventEmitter();
  listStattrs: SelectItem[] = [];
  stattrsSelected: any;
  unitSelected: IDaftunit;
  jenisBukti: string;
  @ViewChild(LookSkpComponent, {static: true}) Skp : LookSkpComponent;
  constructor(
    private service: TbpService,
    private fb: FormBuilder,
    private notif: NotifService,
    private authService: AuthenticationService,
    private stattrsService: StattrsService
  ) { 
    this.userInfo = this.authService.getTokenInfo();
    this.indoKalender = indoKalender;
    this.forms = this.fb.group({
      idtbp : 0,
      idunit : [0, [Validators.required, Validators.min(1)]],
      notbp : ['', Validators.required],
      kdstatus : ['', Validators.required],
      idbend1 : [0, [Validators.required, Validators.min(1)]],
      idxkode : 1,
      tgltbp : null,
      tglvalid: null,
      penyetor: '',
      alamat: '',
      uraitbp : ['', Validators.required],
      jabbend: '',
      idpeg: 0,
      idskp: 0
    });
    this.initialForm = this.forms.value;
    this.uiSkp = {kode: '', nama: ''};
  }
  getStattrs(){
    this.listStattrs = [
      {label: 'Pilih', value: null}
    ];
    this.stattrsService.getBylist('61,64')
      .subscribe(resp => {
        let tempjBukti: IStattrs[] = [];
        if(resp.length > 0 ){
          tempjBukti = resp;
          tempjBukti.forEach(e => {
            this.listStattrs.push({label: `${e.lblstatus}`, value: e.kdstatus})
          });
        }
        if(this.mode === 'edit'){
          if(this.forms.value.kdstatus != null && this.forms.value.kdstatus != ''){
            this.stattrsSelected = this.listStattrs.find(w => w.value == this.forms.value.kdstatus).value;
            if(tempjBukti.length > 0){
              let jbukti = tempjBukti.find(f => f.kdstatus.trim() == this.forms.value.kdstatus.trim());
              if(jbukti != null){
                this.jenisBukti =  `${jbukti.lblstatus}`;
              }
            }
            
          }
        }
      });
  }
  changeStattrs(e: any){
    if(e.value){
      let label = this.listStattrs.find(f => f.value == e.value);
      let label_split = label.label.split(':');
      this.forms.patchValue({
        kdstatus:  e.value,
        notbp: `XXXXX/TBP/${this.unitSelected.kdunit}/${this.forms.value.jabbend}/${this.userInfo.NmTahun}`,
      });
    } else {
      this.forms.patchValue({
        kdstatus: '',
        notbp: '',
      });
      this.msg = [];
      this.msg.push({severity: 'warn', summary: 'Peringatan', detail: 'Pilih Jenis Bukti'});
    }
  }
  lookSKP(){
    this.Skp.title = 'Pilih Skp/Skr';
    this.Skp.gets(this.forms.value.idunit, null, null, null, true);
    this.Skp.showThis = true;
  }
  callbackSkp(e: any){
    this.skpSeleceted = e;
    this.uiSkp = {kode: this.skpSeleceted.noskp, nama: this.skpSeleceted.uraiskp};
    this.forms.patchValue({
      idskp: this.skpSeleceted.idskp
    });
  }
  ngOnInit() {
  }
  simpan(){
    if(this.forms.valid){
      this.loading_post = true;
      this.forms.patchValue({
        tgltbp: this.forms.value.tgltbp !== null ? new Date(this.forms.value.tgltbp).toLocaleDateString() : null,
        tglvalid: this.forms.value.tglvalid !== null ? new Date(this.forms.value.tglvalid).toLocaleDateString() : null
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
    this.getStattrs();
  }
  onHide(){
    this.forms.reset(this.initialForm);
    this.msg = [];
    this.loading_post = false;
    this.stattrsSelected = null;
    this.unitSelected = null;
    this.showThis = false;
    this.jenisBukti = '';
    this.uiSkp = {kode: '', nama: ''};
    this.skpSeleceted = null;
  }
}
