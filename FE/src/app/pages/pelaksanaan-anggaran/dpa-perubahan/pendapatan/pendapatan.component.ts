import { Component, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { LazyLoadEvent } from 'primeng/api';
import { IDaftunit } from 'src/app/core/interface/idaftunit';
import { IDpa } from 'src/app/core/interface/idpa';
import { IDpad } from 'src/app/core/interface/idpad';
import { IDisplayGlobal } from 'src/app/core/interface/iglobal';
import { IRkad } from 'src/app/core/interface/irkad';
import { ITokenClaim } from 'src/app/core/interface/itoken-claim';
import { AuthenticationService } from 'src/app/core/services/auth.service';
import { DpaService } from 'src/app/core/services/dpa.service';
import { DpadService } from 'src/app/core/services/dpad.service';
import { CanActiveGuardService } from 'src/app/core/_commonServices/can-active-guard.service';
import { NotifService } from 'src/app/core/_commonServices/notif.service';
import { LookDaftunitDpaComponent } from 'src/app/shared/lookups/look-daftunit-dpa/look-daftunit-dpa.component';

@Component({
  selector: 'app-pendapatan',
  templateUrl: './pendapatan.component.html',
  styleUrls: ['./pendapatan.component.scss']
})
export class PendapatanComponent implements OnInit, OnDestroy {
  title: string = '';
  tabIndex: number = 0;
  uiUnit: IDisplayGlobal;
  unitSelected: IDaftunit;
  userInfo: ITokenClaim;
  loading_sk: boolean;
  totalRecords: number;
  loading_rek: boolean;
  listdata: IDpa[] = [];
  listrek: IDpad[] = [];
  skSelected: IDpa = null;
  rekSelected: IDpad = null;
  @ViewChild(LookDaftunitDpaComponent, {static: true}) Daftunit : LookDaftunitDpaComponent;
  @ViewChild('dt',{static:true}) dt: any;
  @ViewChild('dtrek',{static:true}) dtrek: any;
  formFilter: FormGroup;
  initialForm: any;
  nilaiInit: number;
  constructor(
    private auth: AuthenticationService,
    private service: DpaService,
    private service_rek: DpadService,
    private notif: NotifService,
    private fb: FormBuilder,
    private active: CanActiveGuardService
  ) {
    this.active.titleComponent$.subscribe(resp => this.title = resp);
    this.userInfo = this.auth.getTokenInfo();
    this.uiUnit = {kode: '', nama: ''};
    this.formFilter = this.fb.group({
      idunit: [0, [Validators.required, Validators.min(1)]],
      kdtahap: '341',
      idxkode: 2
    });
    this.initialForm = this.formFilter.value;
    this.service_rek._nilaiRek.subscribe(resp => {
      if(this.rekSelected){
        if(this.listrek.length > 0){
          this.listrek.map(m => {
            if(m.iddpad == this.rekSelected.iddpad){
							m.nilai = resp;
						}
						this.dtrek.reset();
          });
        }
			}
    });
  }
  ngOnInit() {
    this.tabIndex = 0;
  }
  lookDaftunit(){
    this.Daftunit.title = 'Pilih SKPD';
    this.Daftunit.gets('3,4');
    this.Daftunit.showThis = true;
  }
  callBackDaftunit(e: IDaftunit){
    this.unitSelected = e;
    this.uiUnit = {kode: this.unitSelected.kdunit, nama: this.unitSelected.nmunit};
    this.skSelected = null;
    this.rekSelected = null;
    this.formFilter.patchValue({
      idunit: this.unitSelected.idunit
    });
    if(this.dt) this.dt.reset();
  }
  gets(event: LazyLoadEvent){
    if(this.formFilter.valid){
      this.loading_sk = true;
      this.service._start = event.first;
      this.service._rows = event.rows;
      this.service._globalFilter = event.globalFilter;
      this.service._sortField = event.sortField;
      this.service._sortOrder = event.sortOrder;
      this.service._idunit = this.formFilter.value.idunit;
      this.service._kdtahap = this.formFilter.value.kdtahap;
      this.service.paging().subscribe(resp => {
        this.totalRecords = resp.totalrecords;
        this.listdata = resp.data;
        this.loading_sk = false;
      }, error => {
        this.loading_sk = false;
        if(Array.isArray(error.error.error)){
          for(var i = 0; i < error.error.error.length; i++){
            this.notif.error(error.error.error[i]);
          }
        } else {
          this.notif.error(error.error);
        }
      });
    }
  }
  skKlick(e: IDpa){
		this.skSelected = e;
    this.getRek();
	}
  getRek(){
    if(this.skSelected){
      if(this.dtrek) this.dtrek.reset();
      this.loading_rek = true;
      this.listrek = [];
      this.service_rek._iddpa = this.skSelected.iddpa;
      this.service_rek._kdtahap = this.skSelected.kdtahap;
      this.service_rek.gets()
        .subscribe(resp => {
          if(resp.length){
            this.listrek = [...resp]
          } else {
            this.notif.info('Data Rekening Tidak Tersedia');
          }
          this.loading_rek = false;
        }, error => {
          this.loading_rek = false;
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
		if(this.listrek.length > 0){
			this.listrek.forEach(f => total += f.nilai);
		}
		return total;
  }
  onRowEditInit(e: IDpad) {
    this.nilaiInit = e.nilai;
  }
  onRowEditSave(e: IDpad) {
    this.service_rek.put(e).subscribe(resp => {
      if (resp.ok) {
        this.getRek();
      }
      this.notif.success('Nilai Berhasil Diubah');
    }, error => {
      this.loading_rek = false;
      if (Array.isArray(error.error.error)) {
        for (var i = 0; i < error.error.error.length; i++) {
          this.notif.error(error.error.error[i]);
        }
      } else {
        this.notif.error(error.error);
      }
    });
  }
  onRowEditCancel(e: IDpad, index: number) {
    this.listrek.map(m => {
      if(m.iddpad == e.iddpad){
        m.nilai = this.nilaiInit;
      }
    });
  }
  delete(e: IDpad) {
    this.notif.confir({
      message: `${e.idrekNavigation.kdper.trim()} - ${e.idrekNavigation.nmper.trim()} Akan Dihapus ?`,
      accept: () => {
        this.service_rek.delete(e.iddpad).subscribe(
          (resp) => {
            if (resp.ok) {
              this.notif.success('Data berhasil dihapus');
              if (this.dt) this.dt.reset();
            }
          }, (error) => {
            if (Array.isArray(error.error.error)) {
              for (var i = 0; i < error.error.error.length; i++) {
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
  rekKlik(e: IDpad){
    this.rekSelected = e;
  }
  onChangeTab(e: any){
    this.tabIndex = e.index;
	}
  ngOnDestroy(): void {
    this.formFilter.reset(this.initialForm);
    this.listdata = [];
		this.uiUnit = { kode: '', nama: '' };
		this.unitSelected = null;
		this.skSelected = null;
    this.rekSelected = null;
  }
}

