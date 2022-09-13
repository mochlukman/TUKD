import { Component, EventEmitter, OnInit, Output, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Message } from 'primeng/api';
import { IDisplayGlobal } from 'src/app/core/interface/iglobal';
import { IStdharga } from 'src/app/core/interface/istdharga';
import { InputRupiahPipe } from 'src/app/core/pipe/input-rupiah.pipe';
import { RkadetrService } from 'src/app/core/services/rkadetr.service';
import { StdhargaService } from 'src/app/core/services/stdharga.service';
import { NotifService } from 'src/app/core/_commonServices/notif.service';
import { LookStdhargaComponent } from 'src/app/shared/lookups/look-stdharga/look-stdharga.component';

@Component({
  selector: 'app-rkadetr-formx',
  templateUrl: './rkadetr-formx.component.html',
  styleUrls: ['./rkadetr-formx.component.scss'],
  providers: [InputRupiahPipe]
})
export class RkadetrFormxComponent implements OnInit {
  uiStdharga: IDisplayGlobal;
  stdhargaSelected: IStdharga;
  loading_post: boolean;
  showThis: boolean;
  title: string;
  mode: string;
  msg: Message[];
  forms: FormGroup;
  initialForm: any;
  isHeader: boolean;
  @Output() callback = new EventEmitter();
  @ViewChild(LookStdhargaComponent, {static: true}) Stdharga : LookStdhargaComponent;
  constructor(
    private service: RkadetrService,
    private fb: FormBuilder,
    private notif: NotifService,
    private stdhargaService: StdhargaService
  ) {
    this.uiStdharga = {kode: '', nama: ''};
    this.forms = this.fb.group({
      idrkadetr: 0,
      idrkar: [0, [Validators.required, Validators.min(1)]],
      kdjabar: ['', Validators.required],
      idstdharga: 0,
      uraian: '',
      satuan: '',
      tarif: 0,
      ekspresi: '',
      type: '',
      idrkadetrduk: 0
    });
    this.initialForm = this.forms.value;
  }
  ngOnInit() {
  }
  lookStdHarga(){
    this.Stdharga.title = 'Pilih Standar Harga';
    this.Stdharga.showThis = true;
  }
  callbackStdharga(e: IStdharga){
    this.forms.patchValue({
      idstdharga: e.idstdharga
    });
    this.uiStdharga = {kode: e.nostd.trim(), nama: e.nmstd.trim()};
    this.stdhargaSelected = e;
  }
  typeChange(e: string) {
    if (e == 'H') {
      this.isHeader = true;
      this.forms.patchValue({
        satuan: '',
        tarif: 0,
        ekspresi: '',
      });
    } else {
      this.isHeader = false;
    }
  }
  simpan() {
    if (this.forms.valid) {
      this.loading_post = true;
      this.forms.get('type').enable();
      if (this.mode === 'add') {
        if(this.forms.value.idrkadetrduk == null){
          this.forms.patchValue({
            idrkadetrduk: 0
          });
        }
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
          if (Array.isArray(error.error.error)) {
            this.msg = [];
            for (var i = 0; i < error.error.error.length; i++) {
              this.msg.push({ severity: 'error', summary: 'error', detail: error.error.error[i] });
            }
          } else {
            this.msg = [];
            this.msg.push({ severity: 'error', summary: 'error', detail: error.error });
          }
          this.loading_post = false;
        });
      } else if (this.mode === 'edit') {
        if(this.forms.value.idrkadetrduk == null){
          this.forms.patchValue({
            idrkadetrduk: 0
          });
        }
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
          if (Array.isArray(error.error.error)) {
            this.msg = [];
            for (var i = 0; i < error.error.error.length; i++) {
              this.msg.push({ severity: 'error', summary: 'error', detail: error.error.error[i] });
            }
          } else {
            this.msg = [];
            this.msg.push({ severity: 'error', summary: 'error', detail: error.error });
          }
          this.loading_post = false;
        });
      }
    }
  }
  onShow() {
    if(this.forms.value.type == 'H'){
      this.isHeader = true;
    }
    if(this.mode == 'edit'){
      this.forms.get('type').disable();
      if(this.forms.value.idstdharga){
        this.loading_post = true;
        this.stdhargaService.get(this.forms.value.idstdharga).subscribe(resp => {
          if(resp.idstdharga){
            this.forms.patchValue({
              idstdharga: resp.idstdharga
            });
            this.uiStdharga = {kode: resp.nostd.trim(), nama: resp.nmstd.trim()};
            this.stdhargaSelected = resp;
          }
          this.loading_post = false;
        }, (error) => {
          if (Array.isArray(error.error.error)) {
            this.msg = [];
            for (var i = 0; i < error.error.error.length; i++) {
              this.msg.push({ severity: 'error', summary: 'error', detail: error.error.error[i] });
            }
          } else {
            this.msg = [];
            this.msg.push({ severity: 'error', summary: 'error', detail: error.error });
          }
          this.loading_post = false;
        });
      }
    }
  }
  onHide() {
    this.forms.reset(this.initialForm);
    this.msg = [];
    this.loading_post = false;
    this.isHeader = false;
    this.forms.get('type').enable();
    this.uiStdharga = {kode: '', nama: ''};
    this.showThis = false;
  }
}