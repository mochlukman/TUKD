import { Component, EventEmitter, OnInit, Output, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Message, SelectItem } from 'primeng/api';
import { IDisplayGlobal } from 'src/app/core/interface/iglobal';
import { IMkegiatan } from 'src/app/core/interface/imkegiatan';
import { Isifatkeg } from 'src/app/core/interface/isifatkeg';
import { InputRupiahPipe } from 'src/app/core/pipe/input-rupiah.pipe';
import { KegunitService } from 'src/app/core/services/kegunit.service';
import { SifatkegService } from 'src/app/core/services/sifatkeg.service';
import { NotifService } from 'src/app/core/_commonServices/notif.service';
import { LookMkegiatanComponent } from 'src/app/shared/lookups/look-mkegiatan/look-mkegiatan.component';

@Component({
  selector: 'app-keg-unit-form',
  templateUrl: './keg-unit-form.component.html',
  styleUrls: ['./keg-unit-form.component.scss'],
  providers: [ InputRupiahPipe ]
})
export class KegUnitFormComponent implements OnInit {
  private Idprgrm: number;
  set _idprgrm(e: number){
    this.Idprgrm = e;
  }
  private Idpgrmunit: number;
  set _idpgrmunit(e: number){ // ini diisi kalau lookup yang diingikan != kegiatan yang ada di kegunit / untuk input ke kegunit
    this.Idpgrmunit = e;
  }
  loading_post: boolean;
  uiKeg: IDisplayGlobal;
  kegSelected: IMkegiatan;
  showThis: boolean;
  title: string;
  mode: string;
  msg: Message[];
  forms: FormGroup;
  initialForm: any;
  listSifat: SelectItem[] = [];
  sifatSelected: any;
  @Output() callBack = new EventEmitter();
  @ViewChild(LookMkegiatanComponent, {static: true}) Mkegiatan : LookMkegiatanComponent;
  constructor(
    private service: KegunitService,
    private fb: FormBuilder,
    private sifat: SifatkegService,
    private notif: NotifService
  ) {
    this.forms = this.fb.group({
      idkegunit : 0,
      idkeg : [0, [Validators.required, Validators.min(1)]],
      idpgrmunit : [0, [Validators.required, Validators.min(1)]],
      idsifatkeg : [0, [Validators.required, Validators.min(1)]],
      target : '',
      sasaran : '',
      satuan: '',
      pagu: 0,
      ketkeg: ''

    });
    this.initialForm = this.forms.value;
    this.uiKeg = {kode: '', nama: ''};
  }
  ngOnInit(){
  }
  lookMkegiatan(){
    this.Mkegiatan.title = 'Pilih Kegiatan';
    this.Mkegiatan._idprgrm = this.Idprgrm;
    this.Mkegiatan._idpgrmunit = this.Idpgrmunit;
    this.Mkegiatan._levelkeg = 1;
    this.Mkegiatan._type = 'H';
    this.Mkegiatan.showThis = true;
  }
  callbackKegiatan(e: any){
    this.kegSelected = e;
    this.uiKeg = {kode: this.kegSelected.nukeg, nama: this.kegSelected.nmkegunit};
    this.forms.patchValue({
      idkeg: this.kegSelected.idkeg
    });
  }
  getSifat(){
    this.listSifat = [
      {label: 'Pilih', value: null}
    ];
    this.sifat.gets()
      .subscribe(resp => {
        let tempSifat: Isifatkeg[] = [];
        if(resp.length > 0 ){
          tempSifat = resp;
          tempSifat.forEach(e => {
            this.listSifat.push({label: `${e.kdsifat} : ${e.nmsifat}`, value: e.idsifatkeg})
          });
        }
        if(this.mode === 'edit'){
          this.sifatSelected = this.listSifat.find(w => w.value == this.forms.value.idsifatkeg).value;
        }
      });
  }
  changeSifat(e: any){
    if(e.value){
      this.forms.patchValue({
        idsifatkeg:  e.value
      });
    } else {
      this.forms.patchValue({
        idsifatkeg: 0,
      });
      this.msg = [];
      this.msg.push({severity: 'warn', summary: 'Peringatan', detail: 'Pilih Sifat Kegiatan'});
    }
  }
  simpan(){
    if(this.forms.valid){
      this.loading_post = true;
      this.forms.patchValue({
        tglvalid: this.forms.value.tglvalid !== null ? new Date(this.forms.value.tglvalid).toLocaleDateString() : null
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
    this.getSifat();
  }
  onHide(){
    this.uiKeg = {kode: '', nama: ''};
    this.forms.reset(this.initialForm);
    this.showThis = false;
    this.msg = [];
    this.loading_post = false;
    this.kegSelected = null;
    this.sifatSelected = null;
  }
}