import { Component, Input, OnChanges, OnDestroy, OnInit, SimpleChanges, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { LazyLoadEvent } from 'primeng/api';
import { IDaftunit } from 'src/app/core/interface/idaftunit';
import { IDpa } from 'src/app/core/interface/idpa';
import { IDisplayGlobal } from 'src/app/core/interface/iglobal';
import { ITokenClaim } from 'src/app/core/interface/itoken-claim';
import { AuthenticationService } from 'src/app/core/services/auth.service';
import { DpaService } from 'src/app/core/services/dpa.service';
import { NotifService } from 'src/app/core/_commonServices/notif.service';
import { LookDaftunitComponent } from 'src/app/shared/lookups/look-daftunit/look-daftunit.component';
import { SkDpaPengesahanFormComponent } from './sk-dpa-pengesahan-form/sk-dpa-pengesahan-form.component';

@Component({
  selector: 'app-sk-dpa-pengesahan',
  templateUrl: './sk-dpa-pengesahan.component.html',
  styleUrls: ['./sk-dpa-pengesahan.component.scss']
})
export class SkDpaPengesahanComponent implements OnInit, OnChanges, OnDestroy {
  @Input() tabIndex: number = 0;
  uiUnit: IDisplayGlobal;
  unitSelected: IDaftunit;
  loading: boolean;
  listdata : IDpa [] = [];
  totalRecords: number;
  dataSelected: IDpa;
  userInfo: ITokenClaim;
  @ViewChild(LookDaftunitComponent, {static: true}) Daftunit : LookDaftunitComponent;
  @ViewChild(SkDpaPengesahanFormComponent, {static: true}) Form : SkDpaPengesahanFormComponent;
  @ViewChild('dt',{static:true}) dt: any;
  formFilter: FormGroup;
  initialForm: any;
  constructor(
    private service : DpaService,
    private notif: NotifService,
    private auth: AuthenticationService,
    private fb: FormBuilder
  ) {
    this.userInfo = this.auth.getTokenInfo();
    this.uiUnit = {kode: '', nama: ''};
    this.formFilter = this.fb.group({
      idunit: [0, [Validators.required, Validators.min(1)]],
      kdtahap: '341',
      idxkode: 2
    });
    this.initialForm = this.formFilter.value;
  }
  ngOnChanges(changes: SimpleChanges): void {
    if(this.tabIndex != 1){
      this.ngOnDestroy();
    }
  }
  lookDaftunit(){
    this.Daftunit.title = 'Pilih SKPD';
    this.Daftunit.gets('3,4');
    this.Daftunit.showThis = true;
  }
  callBackDaftunit(e: IDaftunit){
    this.unitSelected = e;
    this.uiUnit = {kode: this.unitSelected.kdunit, nama: this.unitSelected.nmunit};
    this.dataSelected = null;
    this.formFilter.patchValue({
      idunit: this.unitSelected.idunit
    });
    if(this.dt) this.dt.reset();
  }
  callback(e: any){
    if (e.added) {
      if (this.dt) this.dt.reset();
    } else if(e.edited){
      this.listdata.splice(this.listdata.findIndex(i => i.iddpa === e.data.iddpa), 1, e.data);
    }
    this.dataSelected = null;
  }
  ngOnInit() {
  }
  gets(event: LazyLoadEvent){
    if(this.formFilter.valid && this.tabIndex == 1){
      this.loading = true;
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
        this.loading = false;
      }, error => {
        this.loading = false;
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
  update(e: IDpa){
    this.Form.forms.patchValue({
      iddpa: e.iddpa,
      idunit: e.idunit,
      kdtahap: e.kdtahap,
      idxkode: e.idxkode,
      nodpa: e.nodpa,
      tgldpa: e.tgldpa !== null ? new Date(e.tgldpa) : null,
      keterangan: e.keterangan,
      nosah: e.nosah,
      tglsah: e.tglsah !== null ? new Date(e.tglsah) : null,
      sah: e.sah
    });
    this.Form.title = 'Pengesahan Data';
    this.Form.mode = 'edit';
    this.Form.showThis = true;
  }
  dataClick(e: IDpa){
    this.dataSelected = e;
	}
  delete(e: IDpa){
    let postBody = {
      iddpa: e.iddpa,
      idunit: e.idunit,
      tglsah: null,
      sah: null
    };
    this.notif.confir({
      message: `Batalkan Pengesahan ?`,
      accept: () => {
        this.service.pengesahan(postBody).subscribe(
          (resp) => {
            if (resp.ok) {
              this.callback({
                edited: true,
                data: resp.body
              });
              this.notif.success('Pengesahan Berhasil Dibatalkan');
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
  ngOnDestroy(): void {
    this.formFilter.reset(this.initialForm);
    this.uiUnit = {kode:'', nama: ''};
    this.unitSelected = null;
    this.listdata = [];
    this.dataSelected = null;
  }
}
