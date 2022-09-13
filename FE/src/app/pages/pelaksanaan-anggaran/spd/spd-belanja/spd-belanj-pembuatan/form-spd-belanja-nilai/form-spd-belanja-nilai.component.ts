import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Message } from 'primeng/api';
import { InputRupiahPipe } from 'src/app/core/pipe/input-rupiah.pipe';
import { SpddetrService } from 'src/app/core/services/spddetr.service';
import { NotifService } from 'src/app/core/_commonServices/notif.service';

@Component({
  selector: 'app-form-spd-belanja-nilai',
  templateUrl: './form-spd-belanja-nilai.component.html',
  styleUrls: ['./form-spd-belanja-nilai.component.scss'],
  providers: [ InputRupiahPipe ]
})
export class FormSpdBelanjaNilaiComponent implements OnInit {
  loading_post: boolean;
  showThis: boolean;
  title: string;
  msg: Message[];
  forms: FormGroup;
  @Output() callback = new EventEmitter();
  constructor(
    private fb: FormBuilder,
    private notif: NotifService,
    private service: SpddetrService
  ) {
    this.forms = this.fb.group({
      idspddetr : [Validators.required],
      nilai : 0,
    });
  }
  ngOnInit(){
  }

  simpan(){
    if(this.forms.valid){
      this.loading_post = true;
      this.service.putNilai(this.forms.value).subscribe((resp) => {
        if (resp.ok) {
          this.callback.emit({
            put_nilai: true,
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
  onShow(){
  }
  onHide(){
    this.forms.reset();
    this.forms.patchValue({
      idspddetr : 0,
      nilai : 0
    });
    this.showThis = false;
    this.msg = [];
    this.loading_post = false;
  }
}
