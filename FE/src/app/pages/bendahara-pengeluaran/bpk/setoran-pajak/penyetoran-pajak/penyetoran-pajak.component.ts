import { Component, Input, OnChanges, OnDestroy, OnInit, SimpleChanges, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { IBkpajak } from 'src/app/core/interface/ibkpajak';
import { IBpkpajakstr } from 'src/app/core/interface/ibpkpajakstr';
import { IDaftunit } from 'src/app/core/interface/idaftunit';
import { IDisplayGlobal } from 'src/app/core/interface/iglobal';
import { ITokenClaim } from 'src/app/core/interface/itoken-claim';
import { AuthenticationService } from 'src/app/core/services/auth.service';
import { BpkpajakstrService } from 'src/app/core/services/bpkpajakstr.service';
import { NotifService } from 'src/app/core/_commonServices/notif.service';
import { LookDaftunitComponent } from 'src/app/shared/lookups/look-daftunit/look-daftunit.component';
import { PenyetoranPajakFormComponent } from './penyetoran-pajak-form/penyetoran-pajak-form.component';

@Component({
  selector: 'app-penyetoran-pajak',
  templateUrl: './penyetoran-pajak.component.html',
  styleUrls: ['./penyetoran-pajak.component.scss']
})
export class PenyetoranPajakComponent implements OnInit, OnChanges, OnDestroy {
  @Input() tabIndex: number = 0;
  uiUnit: IDisplayGlobal;
  unitSelected: IDaftunit;
  userInfo: ITokenClaim;
  loading: boolean;
  listdata: IBpkpajakstr[] = [];
  dataSelected: IBpkpajakstr = null;
  @ViewChild(LookDaftunitComponent, { static: true }) Daftunit: LookDaftunitComponent;
  @ViewChild(PenyetoranPajakFormComponent, {static: true}) Form: PenyetoranPajakFormComponent;
  @ViewChild('dt', { static: false }) dt: any;
  formFilter: FormGroup;
  initialForm: any;
  constructor(
    private auth: AuthenticationService,
    private service: BpkpajakstrService,
    private notif: NotifService,
    private fb: FormBuilder
  ) {
    this.userInfo = this.auth.getTokenInfo();
    this.uiUnit = {kode : '', nama : ''};
    this.formFilter = this.fb.group({
      idunit: [0, [Validators.required, Validators.min(1)]],
      kdstatus: ['36', Validators.required],
    });
    this.initialForm = this.formFilter.value;
  }
  ngOnChanges(changes: SimpleChanges): void {
    if(this.tabIndex != 0){
      this.ngOnDestroy();
    }
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
    this.formFilter.patchValue({
      idunit: this.unitSelected.idunit
    });
    this.listdata = [];
    this.dataSelected = null;
    this.gets();
  }
  callback(e: any) {
    if(e.added){
      this.listdata.push(e.data);
    } else if(e.edited){
      this.listdata.splice(this.listdata.findIndex(i => i.idbpkpajakstr === e.data.idbpkpajakstr), 1, e.data);
    }
    this.dataSelected = null;
  }
  gets(){
    if(this.formFilter.valid && this.tabIndex == 0){
      this.loading = true;
      this.listdata = [];
      this.dataSelected = null;
      this.service._idunit = this.formFilter.value.idunit;
      this.service._kdstatus = this.formFilter.value.kdstatus;
      this.service.gets().subscribe(resp => {
        if(resp.length > 0){
          this.listdata = resp;
        } else {
          this.notif.info('Data Tidak Tersedia');
        }
        this.loading = false;
      }, error => {
        this.loading = false;
        if (Array.isArray(error.error.error)) {
          for (var i = 0; i < error.error.error.length; i++) {
            this.notif.error(error.error.error[i]);
          }
        } else {
          this.notif.error(error.error);
        }
      });
    }
  }
  callbackdet(e: any){
    this.listdata.map(m => {
      if(m.idbpkpajakstr == e.idbpkpajakstr){
        m.nilai = e.nilai;
      }
    });
  }
  add(){
    this.Form.title = 'Tambah Data';
    this.Form.mode = 'add';
    this.Form.forms.patchValue({
      idunit : this.formFilter.value.idunit
    });
    this.Form.showThis = true;
  }
  edit(e: IBpkpajakstr){
    this.Form.title = 'Ubah Data';
    this.Form.mode = 'edit';
    this.Form.forms.patchValue({
      idbpkpajakstr : e.idbpkpajakstr,
      idunit : e.idunit,
      uraian : e.uraian,
      nomor : e.nomor,
      tanggal :e.tanggal ? new Date(e.tanggal) : null,
      kdstatus : e.kdstatus.trim()
    });
    this.Form.showThis = true;
  }
  delete(e: IBpkpajakstr){
    this.notif.confir({
			message: ``,
			accept: () => {
				this.service.delete(e.idbpkpajakstr).subscribe(
					(resp) => {
						if (resp.ok) {
              this.listdata = this.listdata.filter(f => f.idbpkpajakstr !== e.idbpkpajakstr);
              this.notif.success('Data berhasil dihapus');
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
  dataClick(e: IBpkpajakstr){
    this.dataSelected = e;
  }
  ngOnDestroy(): void {
    this.formFilter.reset(this.initialForm);
    this.uiUnit = { kode: '', nama: '' };
    this.unitSelected = null;
    this.listdata = [];
    this.dataSelected = null;
  }
}
