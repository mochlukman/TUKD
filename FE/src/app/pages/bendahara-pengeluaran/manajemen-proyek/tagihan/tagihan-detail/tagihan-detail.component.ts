import { Component, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { iif, Subscription } from 'rxjs';
import { ITagihan, ITagihandet } from 'src/app/core/interface/itagihan';
import { ITokenClaim } from 'src/app/core/interface/itoken-claim';
import { AuthenticationService } from 'src/app/core/services/auth.service';
import { TagihanService } from 'src/app/core/services/tagihan.service';
import { TagihandetService } from 'src/app/core/services/tagihandet.service';
import { NotifService } from 'src/app/core/_commonServices/notif.service';
import { FormTagihanDetailComponent } from './form-tagihan-detail/form-tagihan-detail.component';

@Component({
  selector: 'app-tagihan-detail',
  templateUrl: './tagihan-detail.component.html',
  styleUrls: ['./tagihan-detail.component.scss']
})
export class TagihanDetailComponent implements OnInit, OnDestroy {
  loading: boolean;
  listdata: ITagihandet[] = [];
  dataSelected : ITagihandet = null;
  userInfo: ITokenClaim;
  tagihanSelected: ITagihan;
  sub_tagihan: Subscription;
  @ViewChild('dt',{static:false}) dt: any;
  @ViewChild(FormTagihanDetailComponent,{static: true}) Form : FormTagihanDetailComponent;
  constructor(
    private tagihanService: TagihanService,
    private service: TagihandetService,
    private notif: NotifService,
    private auth: AuthenticationService
  ) {
    this.userInfo = this.auth.getTokenInfo();
    this.sub_tagihan = this.tagihanService._tagihanSelected.subscribe(resp => {
      this.tagihanSelected = resp;
      if(this.tagihanSelected){
        this.get();
      } else {
        this.dataSelected = null;
        this.listdata = [];
      }
    });
  }
  ngOnInit() {
  }
  callback(e: any){
    if(e.added){
      this.listdata.push(e.data);
      if(this.dt) this.dt.reset();
    } else if(e.edited){
      let index = this.listdata.findIndex(f => f.idtagihandet === e.data.idtagihandet);
      this.listdata = this.listdata.filter(f => f.idtagihandet != e.data.idtagihandet);
      this.listdata.splice(index, 0, e.data);
      if(this.dt) this.dt.resetPageOnSort = false;
    }
    this.dt.reset();
  }
  get(){
    if(this.tagihanSelected){
      if(this.dt) this.dt.reset();
      this.loading = true;
      this.listdata = [];
      this.service.gets(this.tagihanSelected.idtagihan)
        .subscribe(resp => {
          if(resp.length){
            this.listdata = [...resp]
          } else {
            this.notif.info('Data Rekening Tidak Tersedia');
          }
          this.loading = false;
        }, error => {
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
  totalNilai(){
    let total = 0;
		if(this.listdata.length > 0){
			this.listdata.forEach(f => total += f.nilai);
		}
		return total;
  }
  rekKlik(e: ITagihandet){
    this.dataSelected = e;
  }
  add(){
    this.Form.title = 'Tambah';
    this.Form.mode = 'add';
    this.Form.tagihanSelected = this.tagihanSelected;
    this.Form.forms.patchValue({
      idtagihan: this.tagihanSelected.idtagihan
    });
    this.Form.showThis = true;
  }
  update(e: ITagihandet){
    this.Form.title = 'Ubah Nilai';
    this.Form.mode = 'edit';
    this.Form.forms.patchValue({
      idtagihandet : e.idtagihandet,
      idtagihan : e.idtagihan,
      idrek : e.idrek,
      nilai : e.nilai
    });
    if(e.rekening){
      this.Form.uiRek = {kode: e.rekening.kdper, nama: e.rekening.nmper};
    }
    this.Form.showThis = true;
  }
  delete(e: ITagihandet){
    this.notif.confir({
			message: `${e.rekening.kdper} - ${e.rekening.nmper} Akan Dihapus`,
			accept: () => {
				this.service.delete(e.idtagihandet).subscribe(
					(resp) => {
						if (resp.ok) {
              this.listdata = this.listdata.filter(f => f.idtagihandet !== e.idtagihandet);
              this.listdata.sort((a,b) =>  (a.idrek > b.idrek ? 1 : -1));
              this.notif.success('Data berhasil dihapus');
              this.dt.reset();
						}
					}, (error) => {
            if(Array.isArray(error.error.error)){
              for(var i = 0; i < error.error.error.length; i++){
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
  ngOnDestroy() : void {
    this.dataSelected = null;
    this.listdata = [];
    this.sub_tagihan.unsubscribe();
  }
}
