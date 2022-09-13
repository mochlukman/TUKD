import { LookSppComponent } from 'src/app/shared/lookups/look-spp/look-spp.component';
import { ISpp } from 'src/app/core/interface/ispp';
import { ReportService } from 'src/app/core/services/report.service';
import { MenuItem, SelectItem } from 'primeng/api';
import { IReportParameter } from 'src/app/core/interface/ireport-parameter';
import { Component, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { IDaftunit } from 'src/app/core/interface/idaftunit';
import { IDisplayGlobal } from 'src/app/core/interface/iglobal';
import { ITokenClaim } from 'src/app/core/interface/itoken-claim';
import { AuthenticationService } from 'src/app/core/services/auth.service';
import { NotifService } from 'src/app/core/_commonServices/notif.service';
import { LookDaftunitComponent } from 'src/app/shared/lookups/look-daftunit/look-daftunit.component';
import { ReportModalComponent } from 'src/app/shared/modals/report-modal/report-modal.component';
import { LookPegawaiComponent } from 'src/app/shared/lookups/look-pegawai/look-pegawai.component';
import { IPegawai } from 'src/app/core/interface/ipegawai';

@Component({
  selector: 'app-spp-lsgj',
  templateUrl: './spp-lsgj.component.html',
  styleUrls: ['./spp-lsgj.component.scss']
})
export class SppLsgjComponent implements OnInit, OnDestroy {
  loading_post: boolean;
  uiUnit: IDisplayGlobal;
  uiSpp: IDisplayGlobal;
  uiPeg: IDisplayGlobal;
  pegawaiSelected: IPegawai;
  unitSelected: IDaftunit;
  sppSelected: ISpp;
  userInfo: ITokenClaim;
  loading: boolean;
  itemPrints: MenuItem[];
	parameterReport: IReportParameter;
	typeDocument: number;
	@ViewChild(LookDaftunitComponent, {static: true}) Daftunit : LookDaftunitComponent;
  @ViewChild(LookSppComponent,{static: true}) Spp : LookSppComponent;
  @ViewChild(LookPegawaiComponent, {static: true}) Pegawai : LookPegawaiComponent;
	@ViewChild(ReportModalComponent,{static: true}) showTanggal: ReportModalComponent;
  constructor(
  
      private authService: AuthenticationService,
      private notif: NotifService,
      private reportService: ReportService,

  ) { 
    this.userInfo=this.authService.getTokenInfo();
      this.uiUnit={kode: '', nama: '' };
      this.uiSpp={kode: '', nama: '' };
      this.uiPeg = {kode: '', nama: ''};
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

  LookSpp(){
    if(this.unitSelected){
    this.Spp.title='Pilih SPP';
    this.Spp._idunit = this.unitSelected.idunit;
    this.Spp._idxkode = 4;
    this.Spp._kdstatus = '24';
    this.Spp.gets();
    this.Spp.showThis=true;
      } else {
        this.notif.warning('Pilih Unit Organisasi');
      }
    }
  callBackSpp(e: ISpp){
    this.sppSelected = e;
    this.uiSpp = {kode: e.nospp, nama: e.tglspp !== null ? new Date(e.tglspp).toLocaleDateString() : ''}
  }
  lookPegawai(){
    if(this.unitSelected){
      this.Pegawai.title = 'Pilih Pegawai';
      this.Pegawai.gets(this.unitSelected.idunit);
      this.Pegawai.showThis = true;
    } else {
      this.notif.warning('Pilih Unit Organisasi');
    } 
  }
  callbackPegawai(e: IPegawai){
    this.pegawaiSelected = e;
    this.uiPeg = {kode: e.nip != '' ? e.nip.trim() : '', nama: e.nama};
  }
  lookDaftunit(){
    this.Daftunit.title = 'Pilih SKPD';
    this.Daftunit.gets('3,4');  // this.userInfo.Idunit);  //userunit
    this.Daftunit.showThis = true;

    this.uiSpp = { kode: '', nama: '' };
    this.uiPeg = { kode: '', nama: '' };
    this.sppSelected=undefined;
    this.pegawaiSelected=undefined;
  }
  callBackDaftunit(e: IDaftunit){
    this.unitSelected = e;
    this.uiUnit = { kode: this.unitSelected.kdunit, nama: this.unitSelected.nmunit };
  }
  
  callBackTanggal(e: any){
		if(e.cetak){
			if (!this.unitSelected) {
				this.notif.warning('SKPD harus dipilih');
			} else if (!this.sppSelected) {
				this.notif.warning('SPP harus dipilih');
			}
      else  if (!this.pegawaiSelected) {
				this.notif.warning('PPTK harus dipilih');
			} 
      else {
				this.loading_post = true;
				this.parameterReport = {
					Type: this.typeDocument,
					FileName: 'spp-ls.rpt',
					Params: {
						
						'@idunit': this.unitSelected.idunit,
            '@idspp' : this.sppSelected.idspp,
            '@idpeg' : this.pegawaiSelected.idpeg,
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
    this.uiSpp = { kode: '', nama: '' };
		this.sppSelected = null;	
    this.uiPeg = { kode: '', nama: '' };
		this.pegawaiSelected = null;	
	}
}
