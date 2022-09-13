import { Component, EventEmitter, OnInit, Output, ViewChild } from '@angular/core';
import { Message } from 'primeng/api';
import { IBpkpajak } from 'src/app/core/interface/ibpkpajak';
import { IBpkpajakdet } from 'src/app/core/interface/ibpkpajakdet';
import { IPajak } from 'src/app/core/interface/ipajak';
import { ITokenClaim } from 'src/app/core/interface/itoken-claim';
import { AuthenticationService } from 'src/app/core/services/auth.service';
import { BpkpajakdetService } from 'src/app/core/services/bpkpajakdet.service';
import { NotifService } from 'src/app/core/_commonServices/notif.service';
import { LookPajakCheckboxComponent } from 'src/app/shared/lookups/look-pajak-checkbox/look-pajak-checkbox.component';
import { PembuatanBpkPungutanPajakDetFormComponent } from './pembuatan-bpk-pungutan-pajak-det-form/pembuatan-bpk-pungutan-pajak-det-form.component';

@Component({
  selector: 'app-pembuatan-bpk-pungutan-pajak-det',
  templateUrl: './pembuatan-bpk-pungutan-pajak-det.component.html',
  styleUrls: ['./pembuatan-bpk-pungutan-pajak-det.component.scss']
})
export class PembuatanBpkPungutanPajakDetComponent implements OnInit {
  BpkpajakSelected: IBpkpajak;
  showThis: boolean;
  loading: boolean;
  title: string;
  mode: string;
  msg: Message[];
  indoKalender: any;
  userInfo: ITokenClaim;
  @Output() callback = new EventEmitter();
  listdata: IBpkpajakdet[] = [];
  @ViewChild('dt',{static:false}) dt: any;
  @ViewChild(LookPajakCheckboxComponent, {static: true}) Pajak: LookPajakCheckboxComponent;
  @ViewChild(PembuatanBpkPungutanPajakDetFormComponent, {static: true}) Form: PembuatanBpkPungutanPajakDetFormComponent;
  constructor(
    private service: BpkpajakdetService,
    private authService: AuthenticationService,
    private notif: NotifService
  ) { 
    this.userInfo = this.authService.getTokenInfo();
  }
  ngOnInit() {
  }
  gets(){
    if(this.BpkpajakSelected){
      this.loading = true;
      this.listdata = [];
      this.service._idbpkpajak = this.BpkpajakSelected.idbpkpajak;
      this.service.gets().subscribe(resp => {
        if(resp.length > 0){
          this.listdata = resp;
        } else {
          this.msg = [];
            
        }
        this.loading = false;
      }, error => {
        this.msg = [];
        this.loading = false;
        if (Array.isArray(error.error.error)) {
          for (var i = 0; i < error.error.error.length; i++) {
            this.msg.push({severity: 'error', summary: 'error', detail: error.error.error[i]});
          }
        } else {
          this.msg.push({severity: 'error', summary: 'error', detail: error.error});
        }
      });
    }
  }
  callbackPost(e: any){
    if(e.added){
      this.listdata.push(...e.data);
      if(this.dt) this.dt.reset();
    } else if(e.edited){
      this.listdata.splice(this.listdata.findIndex(i => i.idbpkpajakdet === e.data.idbpkpajakdet), 1, e.data);
      this.msg = [];
      this.msg.push({severity: 'success', summary: 'Berhasil', detail: 'Data Berhasil Diubah'});
      setTimeout(() => {
        this.msg = [];
      }, 3000);
      this.callback.emit({
        idbpkpajak: this.BpkpajakSelected.idbpkpajak,
        nilai: this.listdata.map(m => m.nilai).reduce((prev, next) => prev + next) // jumlahkan Nilai
      });
    }
  }
  callbackForm(e: IPajak[]){
    if(e.length > 0){
      let idpajaks = e.map(m => m.idpajak);
      let postBody = {
        idbpkpajak : this.BpkpajakSelected.idbpkpajak,
        idpajak: idpajaks
      };
      this.loading = true;
      this.service.post(postBody).subscribe(resp => {
        if(resp.ok){
          this.callbackPost({
            data: resp.body,
            added: true
          });
          this.msg = [];
          this.msg.push({severity: 'success', summary: 'Berhasil', detail: 'Data Berhasil Ditambahakan'});
          setTimeout(() => {
            this.msg = [];
          }, 3000);
        }
        this.loading = false;
      }, error => {
        this.msg = [];
        this.loading = false;
        if (Array.isArray(error.error.error)) {
          for (var i = 0; i < error.error.error.length; i++) {
            this.msg.push({severity: 'error', summary: 'error', detail: error.error.error[i]});
          }
        } else {
          this.msg.push({severity: 'error', summary: 'error', detail: error.error});
        }
      });
    }
  }
  add(){
    this.Pajak.title = 'Pilih Pajak';
    this.Pajak._idbpkpajak = this.BpkpajakSelected.idbpkpajak;
    this.Pajak.gets();
    this.Pajak.showThis = true;
  }
  edit(e: IBpkpajakdet){
    this.Form.title = `Ubah Nilai ${e.idpajakNavigation.nmpajak}`;
    this.Form.mode = 'edit';
    this.Form.forms.patchValue({
      idbpkpajakdet : e.idbpkpajakdet,
      nilai : e.nilai
    });
    this.Form.showThis = true;
  }
  delete(e: IBpkpajakdet){
    this.notif.confir({
			message: ``,
			accept: () => {
        this.loading = true;
				this.service.delete(e.idbpkpajakdet).subscribe(
					(resp) => {
						if (resp.ok) {
              this.listdata = this.listdata.filter(f => f.idbpkpajakdet !== e.idbpkpajakdet);
              this.msg = [];
              this.msg.push({severity: 'success', summary: 'Berhasil', detail: 'Data Berhasil Dihapus'});
              if(this.dt) this.dt.reset();
              setTimeout(() => {
                this.msg = [];
              }, 3000);
              this.callback.emit({
                idbpkpajak: this.BpkpajakSelected.idbpkpajak,
                nilai: this.listdata.map(m => m.nilai).reduce((prev, next) => prev + next) // jumlahkan Nilai
              });
						}
            this.loading = false;
					}, (error) => {
            this.msg = [];
            this.loading = false;
            if (Array.isArray(error.error.error)) {
              for (var i = 0; i < error.error.error.length; i++) {
                this.msg.push({severity: 'error', summary: 'error', detail: error.error.error[i]});
              }
            } else {
              this.msg.push({severity: 'error', summary: 'error', detail: error.error});
            }
          });
			},
			reject: () => {
				return false;
			}
		});
  }
  onShow(){
    this.gets();
  }
  onHide(){
    this.listdata = [];
    this.msg = [];
    this.BpkpajakSelected = undefined;
    this.showThis = false;
  }
}
