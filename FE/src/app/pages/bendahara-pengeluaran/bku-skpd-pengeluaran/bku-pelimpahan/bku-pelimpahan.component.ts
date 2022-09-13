import { Component, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Subscription } from 'rxjs';
import { Ibend } from 'src/app/core/interface/ibend';
import { IBku } from 'src/app/core/interface/ibku';
import { IDaftunit } from 'src/app/core/interface/idaftunit';
import { IDisplayGlobal } from 'src/app/core/interface/iglobal';
import { ITokenClaim } from 'src/app/core/interface/itoken-claim';
import { indoKalender } from 'src/app/core/local';
import { AuthenticationService } from 'src/app/core/services/auth.service';
import { BkuMainPagesService } from 'src/app/core/services/bku-main-pages.service';
import { BkuService } from 'src/app/core/services/bku.service';
import { NotifService } from 'src/app/core/_commonServices/notif.service';
import { LookBendaharaComponent } from 'src/app/shared/lookups/look-bendahara/look-bendahara.component';
import { LookDaftunitComponent } from 'src/app/shared/lookups/look-daftunit/look-daftunit.component';
import { BkuPelimpahanFormComponent } from './bku-pelimpahan-form/bku-pelimpahan-form.component';

@Component({
  selector: 'app-bku-pelimpahan',
  templateUrl: './bku-pelimpahan.component.html',
  styleUrls: ['./bku-pelimpahan.component.scss']
})
export class BkuPelimpahanComponent implements OnInit, OnDestroy {
  uiUnit: IDisplayGlobal;
  unitSelected: IDaftunit;
  uiBend: IDisplayGlobal;
  bendSelected: Ibend;
  indexSubs : Subscription;
  userInfo: ITokenClaim;
  loading: boolean;
  indoKalender: any;
  formFilter: FormGroup;
  initialForm: any;
  listdata: IBku[] = [];
  dataSelected: IBku;
  @ViewChild(LookDaftunitComponent, {static: true}) Daftunit : LookDaftunitComponent;
  @ViewChild(LookBendaharaComponent, {static: true}) Bendahara : LookBendaharaComponent;
  @ViewChild('dt',{static:false}) dt: any;
  @ViewChild(BkuPelimpahanFormComponent, {static: true}) Form: BkuPelimpahanFormComponent;
  constructor(
    private mainService: BkuMainPagesService,
    private auth: AuthenticationService,
    private notif: NotifService,
    private fb: FormBuilder,
    private service: BkuService
  ) {
    this.formFilter = this.fb.group({
      idunit: [0, [Validators.required, Validators.min(1)]],
      idbend: [0, [Validators.required, Validators.min(1)]],
      jenis: ['tbp'],
      tgl1: [new Date(new Date().getFullYear() +'-01-01'), Validators.required],
      tgl2: [new Date(new Date().getFullYear() +'-12-31'), Validators.required],
    });
    this.initialForm = this.formFilter.value;
    this.indoKalender = indoKalender;
    this.userInfo = this.auth.getTokenInfo();
    this.uiUnit = {kode: '', nama: ''};
    this.uiBend = {kode: '', nama: ''};
    this.indexSubs = this.mainService._tabIndex.subscribe(resp => {
      if(resp === 1){}
    });
  }

  ngOnInit() {

  }
  lookDaftunit(){
    this.Daftunit.title = 'Pilih Unit Organisasi';
    this.Daftunit.gets('3,4');
    this.Daftunit.showThis = true;
  }
  callBackDaftunit(e: IDaftunit){
    this.unitSelected = e;
    this.uiUnit = {kode: this.unitSelected.kdunit, nama: this.unitSelected.nmunit};
    this.bendSelected = null;
    this.uiBend = {kode: '', nama: ''};
    this.formFilter.patchValue({
      idunit: this.unitSelected.idunit,
      idbend: 0
    });
  }
  lookBendahara(){
    if(this.unitSelected){
      this.Bendahara.title = 'Pilih Bendahara';
      this.Bendahara.gets(this.unitSelected.idunit,'02,12');
      this.Bendahara.showThis = true;
    } else {
      this.notif.warning('Pilih Unit');
    }
    
  }
  callBackBendahara(e: Ibend){
    this.bendSelected = e;
    this.uiBend = {
      kode: this.bendSelected.idpegNavigation.nip, 
      nama: this.bendSelected.idpegNavigation.nama + ',' + this.bendSelected.jnsbendNavigation.jnsbend.trim() + ' - ' + this.bendSelected.jnsbendNavigation.uraibend.trim()
    };
    this.formFilter.patchValue({
      idbend: this.bendSelected.idbend
    });
  }
  gets(){
    if(this.formFilter.valid){
      this.formFilter.patchValue({
        tgl1: this.formFilter.value.tgl1 != null ? new Date(this.formFilter.value.tgl1).toLocaleDateString() : null,
        tgl2: this.formFilter.value.tgl2 != null ? new Date(this.formFilter.value.tgl2).toLocaleDateString() : null
      });
      this.loading = true;
      this.listdata = [];
      this.service.gets(
        this.formFilter.value.idunit,
        this.formFilter.value.idbend,
        this.formFilter.value.jenis,
        this.formFilter.value.tgl1,
        this.formFilter.value.tgl2,
      ).subscribe(resp => {
        if(resp.length > 0){
          this.listdata = resp;
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
  }
  callback(e: any){
    if(e.added){
      this.listdata.push(e.data);
    }
  }
  add(){
    if(this.formFilter.valid){
      this.service.generateNoBku(this.formFilter.value.idunit, this.formFilter.value.idbend)
        .subscribe(resp => {
          if(resp.nobku){
            this.Form.forms.patchValue({
              idunit: this.formFilter.value.idunit,
              idbend: this.formFilter.value.idbend,
              nobku: resp.nobku,
              jenis: 'tbp'
            });
            this.Form.title = 'Tambah BKU Pelimpahan';
            this.Form.mode = 'add';
            this.Form.showThis = true;
          }
        }, error => {
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
  delete(e: IBku){
    this.notif.confir({
			message: `${e.nobkuskpd} Akan Dihapus ?`,
			accept: () => {
				this.service.delete(e.nobkuskpd, 'tbp').subscribe(
					(resp) => {
						if (resp.ok) {
              this.listdata = this.listdata.filter(f => f.nobkuskpd.trim() !== e.nobkuskpd.trim());
              this.notif.success('Data berhasil dihapus');
              this.dataSelected = null;
              if(this.dt) this.dt.reset();
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
    this.unitSelected = null;
    this.bendSelected = null;
    this.uiUnit = {kode: '', nama: ''};
    this.uiBend = {kode: '', nama: ''};
    this.formFilter.reset(this.initialForm);
    this.loading = false;
  }
}
