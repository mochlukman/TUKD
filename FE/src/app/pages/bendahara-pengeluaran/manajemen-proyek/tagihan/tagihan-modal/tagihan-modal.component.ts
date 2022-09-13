import { Component, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { Subscription } from 'rxjs';
import { IDaftunit } from 'src/app/core/interface/idaftunit';
import { IDisplayGlobal, ILookupTree } from 'src/app/core/interface/iglobal';
import { ITagihan } from 'src/app/core/interface/itagihan';
import { ITokenClaim } from 'src/app/core/interface/itoken-claim';
import { AuthenticationService } from 'src/app/core/services/auth.service';
import { TagihanService } from 'src/app/core/services/tagihan.service';
import { NotifService } from 'src/app/core/_commonServices/notif.service';
import { LookDaftunitDpaComponent } from 'src/app/shared/lookups/look-daftunit-dpa/look-daftunit-dpa.component';
import { LookDpakegiatanComponent } from 'src/app/shared/lookups/look-dpakegiatan/look-dpakegiatan.component';
import { FormTagihanComponent } from '../form-tagihan/form-tagihan.component';

@Component({
  selector: 'app-tagihan-modal',
  templateUrl: './tagihan-modal.component.html',
  styleUrls: ['./tagihan-modal.component.scss']
})
export class TagihanModalComponent implements OnInit, OnDestroy {
  uiUnit: IDisplayGlobal;
  unitSelected: IDaftunit;
  uiKeg: IDisplayGlobal;
  kegSelected: ILookupTree;
  userInfo: ITokenClaim;
  loading: boolean;
  listdata: ITagihan[] = [];
  dataSelected: ITagihan = null;
  @ViewChild(LookDaftunitDpaComponent, {static: true}) Daftunit : LookDaftunitDpaComponent;
  @ViewChild(LookDpakegiatanComponent, {static: true}) Kegiatan : LookDpakegiatanComponent;
  @ViewChild('dt',{static:false}) dt: any;
  @ViewChild(FormTagihanComponent, {static: true}) Form: FormTagihanComponent;
  sub_tagihan: Subscription;
  indexSubs : Subscription;
  kdstatus: string = '71';
  constructor(
    private auth: AuthenticationService,
    private service: TagihanService,
    private notif: NotifService
  ) {
    this.userInfo = this.auth.getTokenInfo();
    this.uiUnit = {kode: '', nama: ''};
    this.uiKeg = {kode: '', nama: ''};
    this.sub_tagihan = this.service._tagihanSelected.subscribe(resp => this.dataSelected = resp);
    this.indexSubs = this.service._tabIndex.subscribe(resp => {
      if(resp === 0){
        this.uiUnit = { kode: '', nama: '' };
		    this.uiKeg = { kode: '', nama: '' };
        this.dataSelected = null;
        this.unitSelected = null;
        this.kegSelected = null;
        this.listdata = [];
      }
    });
  }
  ngOnInit() {
  }
  lookDaftunit(){
    this.Daftunit.title = 'Pilih SKPD';
    this.Daftunit.gets('3,4');
    this.Daftunit.showThis = true;
  }
  callBackDaftunit(e: IDaftunit){
    this.unitSelected = e;
    this.uiUnit = {kode: this.unitSelected.kdunit, nama: this.unitSelected.nmunit};
    this.uiKeg = {kode: '', nama: ''};
    this.kegSelected = null;
    this.listdata = [];
    this.service.setTagihanSelected(null);
  }
  lookKegiatan(){
    if(this.unitSelected.hasOwnProperty('idunit')){
      this.Kegiatan.title = 'Pilih Kegiatan';
      this.Kegiatan.get(this.unitSelected.idunit, '321');
      this.Kegiatan.showThis = true;
    }
  }
  callBackKegiatan(e: ILookupTree){
    this.kegSelected = e;
    let split_label = this.kegSelected.label.split('-');
    this.uiKeg = {kode: split_label[0], nama: split_label[1]};
    this.service.setTagihanSelected(null);
    this.getDatas();
  }
  callback(e: any){
    if(e.added){
      this.listdata.push(e.data);
      if(this.dt) this.dt.reset();
    } else if(e.edited){
      let index = this.listdata.findIndex(f => f.idtagihan === e.data.idtagihan);
      this.listdata = this.listdata.filter(f => f.idtagihan != e.data.idtagihan);
      this.listdata.splice(index, 0, e.data);
      if(this.dt) this.dt.resetPageOnSort = false;
    }
  }
  getDatas(){
    if(this.unitSelected){
      if(this.dt) this.dt.reset();
      this.loading = true;
      this.service.gets(this.unitSelected.idunit, this.kegSelected.data_id, this.kdstatus)
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
    } else {
      this.notif.warning('Pilih SKPD');
    }
  }
  add(){
    if(this.unitSelected.hasOwnProperty('idunit') && this.kegSelected){
      this.Form.forms.patchValue({
        idunit: this.unitSelected.idunit,
        idkeg: this.kegSelected.data_id,
        kdstatus: this.kdstatus
      });
      this.Form.title = 'Tambah';
      this.Form.mode = 'add';
      this.Form.showThis = true;
    }
  }
  update(e: ITagihan){
    this.Form.forms.patchValue({
      idtagihan : e.idtagihan,
      idunit :  e.idunit,
      idkeg :  e.idkeg,
      notagihan : e.notagihan,
      tgltagihan : e.tgltagihan != null ? new Date(e.tgltagihan) : new Date(),
      idkontrak :  e.idkontrak,
      uraiantagihan :  e.uraiantagihan,
      kdstatus :  e.kdstatus,
    });
    if(e.idkontrakNavigation){
      this.Form.kontrakSelected = e.idkontrakNavigation;
      this.Form.uiKontrak = {kode: e.idkontrakNavigation.nokontrak, nama: e.idkontrakNavigation.uraian};
    }
    this.Form.title = 'Ubah';
    this.Form.mode = 'edit';
    this.Form.showThis = true;
  }
  dataKlick(e: ITagihan){
		this.service.setTagihanSelected(e);
	}
  delete(e: ITagihan){
    this.notif.confir({
			message: `${e.notagihan} Akan Dihapus`,
			accept: () => {
				this.service.delete(e.idtagihan).subscribe(
					(resp) => {
						if (resp.ok) {
              this.listdata = this.listdata.filter(f => f.idtagihan !== e.idtagihan);
              this.listdata.sort((a,b) =>  (a.notagihan.trim() > b.notagihan.trim() ? 1 : -1));
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
		this.uiUnit = { kode: '', nama: '' };
		this.uiKeg = { kode: '', nama: '' };
		this.unitSelected = null;
    this.kegSelected = null
		this.service.setTagihanSelected(null);
    this.sub_tagihan.unsubscribe();
    this.indexSubs.unsubscribe();
  }
}
