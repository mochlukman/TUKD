import { Component, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { IBkbkas } from 'src/app/core/interface/ibkbkas';
import { IDaftunit } from 'src/app/core/interface/idaftunit';
import { IDisplayGlobal } from 'src/app/core/interface/iglobal';
import { ITokenClaim } from 'src/app/core/interface/itoken-claim';
import { AuthenticationService } from 'src/app/core/services/auth.service';
import { BkbkasService } from 'src/app/core/services/bkbkas.service';
import { NotifService } from 'src/app/core/_commonServices/notif.service';
import { LookDaftunitComponent } from 'src/app/shared/lookups/look-daftunit/look-daftunit.component';
import { FormRekkasdaComponent } from './form-rekkasda/form-rekkasda.component';

@Component({
  selector: 'app-rekkasda',
  templateUrl: './rekkasda.component.html',
  styleUrls: ['./rekkasda.component.scss']
})
export class RekkasdaComponent implements OnInit, OnDestroy {
  userInfo: ITokenClaim;
  loading: boolean;
  listdata: IBkbkas[] = [];
  cols: any[] = [];
  dataSelected: IBkbkas = null;
  @ViewChild('dt',{static:true}) dt: any;
  @ViewChild(FormRekkasdaComponent, {static: true}) Form: FormRekkasdaComponent;
  constructor(
    private auth: AuthenticationService,
    private service: BkbkasService,
    private notif: NotifService
  ) {
    this.userInfo = this.auth.getTokenInfo();
    this.cols = [
      {field: 'nobbantu'},
      {field: 'nmbkas'},
      {field: 'idbankNavigation.akbank'},
      {field: 'norek'},
      {field: 'salod'},
      {field: 'idrekNavigation.nmrek'},
      {field: 'pilih'}
		];
  }
  ngOnInit() {
    this.getDatas();
  }
  callback(e: any){
    if(e.added){
      this.listdata.push(e.data);
    } else if(e.edited){
      let index = this.listdata.findIndex(f => f.nobbantu.trim() === e.data.nobbantu.trim());
      this.listdata = this.listdata.filter(f => f.nobbantu.trim() != e.data.nobbantu.trim());
      this.listdata.splice(index, 0, e.data);
    }
    this.dataSelected = null;
  }
  getDatas(){
    if(this.dt) this.dt.reset();
      this.loading = true;
      this.service.gets()
        .subscribe(resp => {
          this.listdata = [];
          if(resp.length > 0){
            this.listdata = [...resp];
          } else {
            this.notif.info('Data Tidak Tersedia');
          }
          this.loading = false;
        }, (error) => {
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
  add(){
    this.Form.title = 'Tambah';
    this.Form.mode = 'add';
    this.Form.showThis = true;
  }
  update(e: IBkbkas){
    this.Form.forms.patchValue({
      nobbantu : e.nobbantu.trim(),
      idunit : e.idunit,
      idrek : e.idrek,
      idbank : e.idbank,
      nmbkas : e.nmbkas,
      norek : (e.norek != '' || e.norek != null) ? e.norek.trim() : '',
      saldo : e.saldo,
    });
    if(e.idrekNavigation){
      this.Form.uiRek = {kode: e.idrekNavigation.kdper.trim() , nama: e.idrekNavigation.nmper};
    }
    this.Form.title = 'Ubah';
    this.Form.mode = 'edit';
    this.Form.showThis = true;
  }
  dataKlick(e: IBkbkas){
		this.dataSelected = e;
	}
  delete(e: IBkbkas){
    this.notif.confir({
			message: ``,
			accept: () => {
				this.service.delete(e.nobbantu.trim()).subscribe(
					(resp) => {
						if (resp.ok) {
              this.listdata = this.listdata.filter(f => f.nobbantu.trim() !== e.nobbantu.trim());
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
  ngOnDestroy(): void {
    this.listdata = [];
		this.dataSelected = null;
  }
}