import { IDisplayGlobal } from 'src/app/core/interface/iglobal';
import { ReportService } from 'src/app/core/services/report.service';
import { TahapService } from 'src/app/core/services/tahap.service';
import { MenuItem, SelectItem } from 'primeng/api';
import { IReportParameter } from 'src/app/core/interface/ireport-parameter';
import { Component, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { ITokenClaim } from 'src/app/core/interface/itoken-claim';
import { AuthenticationService } from 'src/app/core/services/auth.service';
import { NotifService } from 'src/app/core/_commonServices/notif.service';
import { ReportModalComponent } from 'src/app/shared/modals/report-modal/report-modal.component';
import { LookDaftunitComponent } from 'src/app/shared/lookups/look-daftunit/look-daftunit.component';
import { IDaftunit } from 'src/app/core/interface/idaftunit';

@Component({
  selector: 'app-lpj-penerimaan',
  templateUrl: './lpj-penerimaan.component.html',
  styleUrls: ['./lpj-penerimaan.component.scss']
})
export class LpjPenerimaanComponent implements OnInit , OnDestroy {
  loading_post: boolean;
  uiUnit: IDisplayGlobal;
  unitSelected: IDaftunit;
  uiBend: IDisplayGlobal;
  listBulan: SelectItem[];
  bulanSelected:any;
  userInfo: ITokenClaim;
  loading: boolean;
  itemPrints: MenuItem[];
	parameterReport: IReportParameter;
	typeDocument: number;
  @ViewChild(LookDaftunitComponent, {static: true}) Daftunit : LookDaftunitComponent;
  
	@ViewChild(ReportModalComponent,{static: true}) showTanggal: ReportModalComponent;
  constructor(
  
      private authService: AuthenticationService,
      private notif: NotifService,
      private reportService: ReportService,
      private service: TahapService

  ) { 
    this.userInfo=this.authService.getTokenInfo();
      this.uiUnit={kode: '', nama: '' };
      this.uiBend = {kode: '', nama: ''};
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
    this.getBulan();
  }
  getBulan(){
    this.listBulan = [
      {label: 'Pilih Bulan' , value: null},
      {label: 'Januari' , value: '1'},
      {label: 'Februari' , value: '2'},
      {label: 'Maret' , value: '3'},
      {label: 'April' , value: '4'},
      {label: 'Mei' , value: '5'},
      {label: 'Juni' , value: '6'},
      {label: 'Juli' , value: '7'},
      {label: 'Agustus' , value: '8'},
      {label: 'September' , value: '9'},
      {label: 'Oktober' , value: '10'},
      {label: 'Nopember' , value: '11'},
      {label: 'Desember' , value: '12'}
    ];
  }
  lookDaftunit(){
    this.Daftunit.title = 'Pilih SKPD';
    this.Daftunit.gets('3,4');  // this.userInfo.Idunit);  //userunit
    this.Daftunit.showThis = true;

  }
  callBackDaftunit(e: IDaftunit){
    this.unitSelected = e;
    this.uiUnit = { kode: this.unitSelected.kdunit, nama: this.unitSelected.nmunit };
  }

  

  
  callBackTanggal(e: any){
    if(e.cetak){
			if (!this.unitSelected) {
				this.notif.warning('SKPD harus dipilih');
			} else 
       {
				this.loading_post = true;
				this.parameterReport = {
					Type: this.typeDocument,
					FileName: 'lpjtr.rpt',
					Params: {
            '@idunit': this.unitSelected.idunit,
            '@bulan': this.bulanSelected,
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
    this.bulanSelected=undefined;
	}

}
