import { Component, EventEmitter, Input, OnInit, Output, SimpleChanges, ViewChild } from '@angular/core';
import { LazyLoadEvent } from 'primeng/api';
import { IRkadetr } from 'src/app/core/interface/irkadetr';
import { IRkar } from 'src/app/core/interface/irkar';
import { ITokenClaim } from 'src/app/core/interface/itoken-claim';
import { AuthenticationService } from 'src/app/core/services/auth.service';
import { RkadetrService } from 'src/app/core/services/rkadetr.service';
import { NotifService } from 'src/app/core/_commonServices/notif.service';
import { RkadetrFormxComponent } from './rkadetr-formx/rkadetr-formx.component';

@Component({
  selector: 'app-rka-rpenjabaranx',
  templateUrl: './rka-rpenjabaranx.component.html',
  styleUrls: ['./rka-rpenjabaranx.component.scss']
})
export class RkaRpenjabaranxComponent implements OnInit {
  loading: boolean;
  listdata: IRkadetr[] = [];
  userInfo: ITokenClaim;
  totalRecords: number;
  @Input() rkaSelected: IRkar;
  @ViewChild('dt', { static: false }) dt: any;
  @ViewChild(RkadetrFormxComponent, { static: true }) Form: RkadetrFormxComponent;
  @Output() callbackChild = new EventEmitter();
  totalNilai: number = 0;
  @Input() isvalid: boolean = false;
  constructor(
    private service: RkadetrService,
    private notif: NotifService,
    private auth: AuthenticationService
  ) {
    this.userInfo = this.auth.getTokenInfo();
  }
  ngOnChanges(changes: SimpleChanges): void {
    if (changes.rkaSelected) {
      if (changes.rkaSelected.firstChange && changes.rkaSelected.currentValue) {
        if (this.dt) this.dt.reset();
      } else {
        if (changes.rkaSelected.currentValue != changes.rkaSelected.previousValue) {
          if (this.dt) this.dt.reset();
        }
      }
    }
  }

  ngOnInit() {
  }
  callback(e: any) {
    if (e.added || e.edited) {
      this.callbackChild.emit(e.data);
      if (this.dt) this.dt.reset();
    }
    this.subTotal();
  }
  gets(event: LazyLoadEvent) {
    if (this.rkaSelected) {
      if (this.loading) this.loading = true;
      this.listdata = [];
      this.totalNilai = 0;
      this.service._start = event.first;
      this.service._rows = event.rows;
      this.service._globalFilter = event.globalFilter;
      this.service._sortField = event.sortField;
      this.service._sortOrder = event.sortOrder;
      this.service._idrkar = this.rkaSelected.idrkar;
      this.service.paging().subscribe(resp => {
        if (resp.totalrecords > 0) {
          this.totalRecords = resp.totalrecords;
          this.listdata = resp.data;
          if (resp.optionalResult) {
            this.totalNilai = resp.optionalResult.totalNilai;
          }
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
  add() {
    this.Form.title = 'Tambah Data';
    this.Form.mode = 'add';
    this.Form.forms.patchValue({
      idrkar: this.rkaSelected.idrkar,
    });
    this.Form.showThis = true;
  }
  addSub(e: IRkadetr) {
    this.Form.title = 'Tambah Sub Data';
    this.Form.mode = 'add';
    this.Form.forms.patchValue({
      idrkar: this.rkaSelected.idrkar,
      kdjabar: `${e.kdjabar.trim()}xx.`,
      type: 'D',
      idrkadetrduk: e.idrkadetr
    });
    this.Form.showThis = true;
  }
  edit(e: IRkadetr) {
    this.Form.title = 'Ubah Data';
    this.Form.mode = 'edit';
    this.Form.forms.patchValue({
      idrkadetr: e.idrkadetr,
      idrkar: e.idrkar,
      kdjabar: e.kdjabar,
      uraian: e.uraian,
      satuan: e.satuan,
      tarif: e.tarif,
      ekspresi: e.ekspresi,
      idstdharga: e.idstdharga,
      type: e.type.trim(),
      idrkadetrduk: e.idrkadetrduk,
    });
    this.Form.showThis = true;
  }
  delete(e: IRkadetr) { 
    this.notif.confir({
			message: `${e.uraian} Akan Dihapus ?`,
			accept: () => {
				this.service.delete(e.idrkadetr).subscribe(
					(resp) => {
						if (resp.ok) {
              this.callbackChild.emit(resp.body);
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
  subTotal(){
    let total = 0;
		if(this.listdata.length > 0){
			this.listdata.forEach(f => {
        if(f.type.trim() == 'D'){
          total += +f.subtotal;
        }
      });
		}
		return total;
  }
  ngOnDestroy(): void {
    this.totalNilai = 0;
  }
}