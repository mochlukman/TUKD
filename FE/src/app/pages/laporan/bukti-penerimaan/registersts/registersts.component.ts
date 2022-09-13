import { Component, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { IDaftunit } from 'src/app/core/interface/idaftunit';
import { IDisplayGlobal } from 'src/app/core/interface/iglobal';
import { IReportParameter } from 'src/app/core/interface/ireport-parameter';
import { ITokenClaim } from 'src/app/core/interface/itoken-claim';
import { indoKalender } from 'src/app/core/local';
import { AuthenticationService } from 'src/app/core/services/auth.service';
import { ReportService } from 'src/app/core/services/report.service';
import { NotifService } from 'src/app/core/_commonServices/notif.service';
import { MenuItem } from 'primeng/api';
import { LookDaftunitComponent } from 'src/app/shared/lookups/look-daftunit/look-daftunit.component';
import { ReportModalComponent } from 'src/app/shared/modals/report-modal/report-modal.component';
import { LookBendaharaComponent } from 'src/app/shared/lookups/look-bendahara/look-bendahara.component';
import { Ibend } from 'src/app/core/interface/ibend';

@Component({
  selector: 'app-registersts',
  templateUrl: './registersts.component.html',
  styleUrls: ['./registersts.component.scss']
})
export class RegisterstsComponent implements OnInit , OnDestroy {
  loading_post: boolean;
  uiUnit: IDisplayGlobal;
  unitSelected: IDaftunit;
  uiBend: IDisplayGlobal;
  bendSelected: Ibend;
  indoKalender: any;
  userInfo: ITokenClaim;
  loading: boolean;
  itemPrints: MenuItem[];
	parameterReport: IReportParameter;
	typeDocument: number;
	@ViewChild(LookDaftunitComponent, {static: true}) Daftunit : LookDaftunitComponent;
  @ViewChild(LookBendaharaComponent, {static: true}) Bendahara : LookBendaharaComponent;
	@ViewChild(ReportModalComponent,{static: true}) showTanggal: ReportModalComponent;
  tglAwal: string;
  tglAkhir: string;
  constructor(
      private authService: AuthenticationService,
      private notif: NotifService,
      private reportService: ReportService,
  ) { 
 
      this.userInfo=this.authService.getTokenInfo();
      this.uiUnit={kode: '', nama: '' };
      this.uiBend = {kode: '', nama: ''};
      this.indoKalender = indoKalender;
      this.itemPrints = [
        {
          label: 'PDF',
          icon: 'fa fa-file-pdf-o',
          command: () => {
            this.cetak(1);
          }
        },
        {
          label: 'WORD',
          icon: 'fa fa-file-word-o',
          command: () => {
            this.cetak(2);
          }
        },
        {
          label: 'EXCEL',
          icon: 'fa fa-file-excel-o',
          command: () => {
            this.cetak(3);
          }
        }
      ];

  }

  ngOnInit() {
    
  }

  lookDaftunit(){
    this.Daftunit.title = 'Pilih SKPD';
    this.Daftunit.gets('3,4');  // this.userInfo.Idunit);  //userunit
    this.Daftunit.showThis = true;
    this.bendSelected = null;
    this.uiBend = {kode: '', nama: ''};
  }
  callBackDaftunit(e: IDaftunit){
    this.unitSelected = e;
    this.uiUnit = { kode: this.unitSelected.kdunit, nama: this.unitSelected.nmunit };
  }
  lookBendahara(){
    if(this.unitSelected){
      this.Bendahara.title = 'Pilih Bendahara';
      this.Bendahara.gets(this.unitSelected.idunit,'01,11');
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
  }
  
  callBackTanggal(e: any){
		if(e.cetak){
			if (!this.unitSelected) {
				this.notif.warning('SKPD harus dipilih');
			} 
      else {
				this.loading_post = true;
				this.parameterReport = {
					Type: this.typeDocument,
					FileName: 'regsts.rpt',
					Params: {
						
						'@idunit': this.unitSelected.idunit,
            '@idbend': this.bendSelected.idbend,
            '@awal': (this.tglAwal !== null || this.tglAwal != '') ? new Date(this.tglAwal).toLocaleDateString('en-GB', { year: 'numeric', month: '2-digit', day: '2-digit' }) : '',
            '@akhir': (this.tglAkhir !== null || this.tglAkhir != '') ? new Date(this.tglAkhir).toLocaleDateString('en-GB', { year: 'numeric', month: '2-digit', day: '2-digit' }) : '',
						'@tanggal': e.TGL,
						'@hal':e.halaman
					}
				};
				this.reportService.execPrint(this.parameterReport).subscribe((resp) => {
					this.loading_post = false;
					this.reportService.extractData(resp, this.parameterReport.Type, this.parameterReport.FileName);
				});
			}
		}
	}
  cetak(type: number) {
		this.typeDocument = type;
		this.showTanggal.useTgl = true;
		this.showTanggal.useHal = true;
		this.showTanggal.showThis = true;
	}
  ngOnDestroy() {
		this.uiUnit = { kode: '', nama: '' };
		this.unitSelected = null;	
    this.tglAwal = '';
    this.tglAkhir = '';
	}

}
