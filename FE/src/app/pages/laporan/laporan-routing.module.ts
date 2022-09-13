import { RkaPerubahanPembiayaanComponent } from './rka-perubahan/rka-perubahan-pembiayaan/rka-perubahan-pembiayaan.component';
import { RkaPerubahanRincianComponent } from './rka-perubahan/rka-perubahan-rincian/rka-perubahan-rincian.component';
import { RkaPerubahanBelanjaComponent } from './rka-perubahan/rka-perubahan-belanja/rka-perubahan-belanja.component';
import { RkaPerubahanPendapatanComponent } from './rka-perubahan/rka-perubahan-pendapatan/rka-perubahan-pendapatan.component';
import { RkaPerubahanRingkasanComponent } from './rka-perubahan/rka-perubahan-ringkasan/rka-perubahan-ringkasan.component';
import { BkubendpenerimaanKasComponent } from './bku-penerimaan/bkubendpenerimaan-kas/bkubendpenerimaan-kas.component';
import { SpjfungPengeluaranComponent } from './spj-pengeluaran/spjfung-pengeluaran/spjfung-pengeluaran.component';
import { RekonPenerimaanComponent } from './lpj-penerimaan/rekon-penerimaan/rekon-penerimaan.component';
import { LapPenerimaanComponent } from './lpj-penerimaan/lap-penerimaan/lap-penerimaan.component';
import { LpjPenerimaanComponent } from './lpj-penerimaan/lpj-penerimaan/lpj-penerimaan.component';
import { BkubendPenerimaanComponent } from './bku-penerimaan/bkubend-penerimaan/bkubend-penerimaan.component';
import { StsComponent } from './bukti-penerimaan/sts/sts.component';
import { TbpComponent } from './bukti-penerimaan/tbp/tbp.component';
import { Registersp2dBudComponent } from './sp2d/registersp2d-bud/registersp2d-bud.component';
import { Registersp2dTolakComponent } from './sp2d/registersp2d-tolak/registersp2d-tolak.component';
import { Sp2dTolakComponent } from './sp2d/sp2d-tolak/sp2d-tolak.component';
import { RegspmTolakComponent } from './spm/regspm-tolak/regspm-tolak.component';
import { SpmTolakComponent } from './spm/spm-tolak/spm-tolak.component';
import { SpmLsComponent } from './spm/spm-ls/spm-ls.component';
import { SpmUpComponent } from './spm/spm-up/spm-up.component';

import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { CanActiveGuardService } from 'src/app/core/_commonServices/can-active-guard.service';
import { AngkasPemdaComponent } from './anggarankas/angkas-pemda/angkas-pemda.component';
import { AngkasSkpdComponent } from './anggarankas/angkas-skpd/angkas-skpd.component';
import { DpaBelanjaComponent } from './dpa-skpd/dpa-belanja/dpa-belanja.component';
import { DpaPembiayaanComponent } from './dpa-skpd/dpa-pembiayaan/dpa-pembiayaan.component';
import { DpaPendapatanComponent } from './dpa-skpd/dpa-pendapatan/dpa-pendapatan.component';
import { DpaRincianComponent } from './dpa-skpd/dpa-rincian/dpa-rincian.component';
import { DpaRingkasanComponent } from './dpa-skpd/dpa-ringkasan/dpa-ringkasan.component';
import { DpaskpdComponent } from './dpa-skpd/dpaskpd/dpaskpd.component';
import { PersetujuanDpaComponent } from './dpa-skpd/persetujuan-dpa/persetujuan-dpa.component';
import { SpdDokComponent } from './spd/spd-dok/spd-dok.component';
import { SpdLampComponent } from './spd/spd-lamp/spd-lamp.component';
import { SPPGUComponent } from './SPP/spp-gu/spp-gu.component';
import { SppLsbjComponent } from './SPP/spp-lsbj/spp-lsbj.component';
import { SppLsgjComponent } from './SPP/spp-lsgj/spp-lsgj.component';
import { SppLsp3Component } from './SPP/spp-lsp3/spp-lsp3.component';
import { SPPTUComponent } from './SPP/spp-tu/spp-tu.component';
import { SppTurencanaComponent } from './SPP/spp-turencana/spp-turencana.component';
import { SPPUPComponent } from './SPP/spp-up/spp-up.component';
import { SpmGuComponent } from './spm/spm-gu/spm-gu.component';
import { SpmTuComponent } from './spm/spm-tu/spm-tu.component';
import { Sp2dComponent } from './sp2d/sp2d/sp2d.component';
import { RegistersppSpmSp2dComponent } from './sp2d/registerspp-spm-sp2d/registerspp-spm-sp2d.component';
import { BkubendPengeluaranComponent } from './bku/bkubend-pengeluaran/bkubend-pengeluaran.component';
import { BkubendBankComponent } from './bku/bkubend-bank/bkubend-bank.component';
import { BkubendKasComponent } from './bku/bkubend-kas/bkubend-kas.component';
import { BkubendPajakComponent } from './bku/bkubend-pajak/bkubend-pajak.component';
import { BkubendPanjarComponent } from './bku/bkubend-panjar/bkubend-panjar.component';
import { BkubendSubrincianComponent } from './bku/bkubend-subrincian/bkubend-subrincian.component';
import { KartukendaliSubkegiatanComponent } from './bku/kartukendali-subkegiatan/kartukendali-subkegiatan.component';
import { RegisterstsComponent } from './bukti-penerimaan/registersts/registersts.component';
import { BkubendpenerimaanSubrinciComponent } from './bku-penerimaan/bkubendpenerimaan-subrinci/bkubendpenerimaan-subrinci.component';
import { BkuBudComponent } from './bud/bku-bud/bku-bud.component';
import { LookPhk3Component } from 'src/app/shared/lookups/look-phk3/look-phk3.component';
import { LpkhComponent } from './bud/lpkh/lpkh.component';
import { SpTuComponent } from './bud/sp-tu/sp-tu.component';
import { SpjadmPengeluaranComponent } from './spj-pengeluaran/spjadm-pengeluaran/spjadm-pengeluaran.component';
import { LaptutupkasComponent } from './lpj-pengeluaran/laptutupkas/laptutupkas.component';
import { LpjTuComponent } from './lpj-pengeluaran/lpj-tu/lpj-tu.component';
import { LpjUpComponent } from './lpj-pengeluaran/lpj-up/lpj-up.component';
import { NotakreditComponent } from './bukti-penerimaan/notakredit/notakredit.component';
import { NotapencairandanaComponent } from './pengajuanbelanja/notapencairandana/notapencairandana.component';
import { RkaRingkasanComponent } from './rka/rka-ringkasan/rka-ringkasan.component';
import { RkaPendapatanComponent } from './rka/rka-pendapatan/rka-pendapatan.component';
import { RkaBelanjaComponent } from './rka/rka-belanja/rka-belanja.component';
import { RkaRincianComponent } from './rka/rka-rincian/rka-rincian.component';
import { RkaPembiayaanComponent } from './rka/rka-pembiayaan/rka-pembiayaan.component';
import { ApbdringkasanComponent } from './apbd/apbdringkasan/apbdringkasan.component';
import { ApbdringkasanUrusComponent } from './apbd/apbdringkasan-urus/apbdringkasan-urus.component';
import { ApbdrincianComponent } from './apbd/apbdrincian/apbdrincian.component';
import { PenjabaranRingkasanComponent } from './apbd/penjabaran-ringkasan/penjabaran-ringkasan.component';
import { PenjabaranRincianComponent } from './apbd/penjabaran-rincian/penjabaran-rincian.component';
import { BkubendpenerimaanBankComponent } from './bku-penerimaan/bkubendpenerimaan-bank/bkubendpenerimaan-bank.component';


const routes: Routes = [

// RKA-SKPD
	{
		path: 'anggaranskpd/rkaring',
		component: RkaRingkasanComponent,
		canActivate: [ CanActiveGuardService ],
		data: { setTitle: 'RKA-Ringkasan' }
	},
	{
		path: 'anggaranskpd/rkapendapatan',
		component: RkaPendapatanComponent,
		canActivate: [ CanActiveGuardService ],
		data: { setTitle: 'RKA-Pendapatan' }
	},
	{
		path: 'anggaranskpd/rkabelanja',
		component: RkaBelanjaComponent,
		canActivate: [ CanActiveGuardService ],
		data: { setTitle: 'RKA-Belanja' }
	},	
	{
		path: 'anggaranskpd/rkarincian',
		component: RkaRincianComponent,
		canActivate: [ CanActiveGuardService ],
		data: { setTitle: 'RKA-Rincian' }
	},
	{
		path: 'anggaranskpd/rkabiaya',
		component: RkaPembiayaanComponent,
		canActivate: [ CanActiveGuardService ],
		data: { setTitle: 'RKA-Pembiayaan' }
	},
// ABPD
	{
		path: 'apbd/ringkasan',
		component: ApbdringkasanComponent,
		canActivate: [ CanActiveGuardService ],
		data: { setTitle: 'ABPD Ringkasan' }
	},
	{
		path: 'apbd/ringkasan-urusan',
		component: ApbdringkasanUrusComponent,
		canActivate: [ CanActiveGuardService ],
		data: { setTitle: 'ABPD Ringkasan Urusan dan Organisasi' }
	},
	{
		path: 'apbd/rincian',
		component: ApbdrincianComponent,
		canActivate: [ CanActiveGuardService ],
		data: { setTitle: 'ABPD Rincian' }
	},
	{
		path: 'apbd/ringkasan-penjabaran',
		component: PenjabaranRingkasanComponent,
		canActivate: [ CanActiveGuardService ],
		data: { setTitle: 'Ringkasan Penjabaran APBD' }
	},
	{
		path: 'apbd/rincian-penjabaran',
		component: PenjabaranRincianComponent,
		canActivate: [ CanActiveGuardService ],
		data: { setTitle: 'Rincian Penjabaran APBD' }
	},
// DPA-SKPD
  	{
		path: 'anggaranskpd/dpaver',
		component: PersetujuanDpaComponent,
		canActivate: [ CanActiveGuardService ],
		data: { setTitle: 'Persetujuan Rekapitulasi DPA-SKPD' }
  	},
	{
		path: 'anggaranskpd/dpaskpd',
		component: DpaskpdComponent,
		canActivate: [ CanActiveGuardService ],
		data: { setTitle: 'DPA-SKPD' }
  	},
  	{
		  path: 'anggaranskpd/dparing',
		  component: DpaRingkasanComponent,
		  canActivate: [ CanActiveGuardService ],
		  data: { setTitle: 'Ringkasan DPA-SKPD' }
	},
	{
		path: 'anggaranskpd/dpadskpd',
		component: DpaPendapatanComponent,
		canActivate: [ CanActiveGuardService ],
		data: { setTitle: 'DPA Pendapatan SKPD' }
  	},
	{
		path: 'anggaranskpd/dparskpd',
		component: DpaBelanjaComponent,
		canActivate: [ CanActiveGuardService ],
		data: { setTitle: 'DPA Belanja SKPD' }
	},
	{
		path: 'anggaranskpd/dparincskpd',
		component: DpaRincianComponent,
		canActivate: [ CanActiveGuardService ],
		data: { setTitle: 'DPA Rincian Belanja SKPD' }
	},
	{
		path: 'anggaranskpd/dpabskpd',
		component: DpaPembiayaanComponent,
		canActivate: [ CanActiveGuardService ],
		data: { setTitle: 'DPA Pembiayaan SKPD' }
	},

// Anggaran kas
	{
		path: 'anggarankas/angkasskpd',
		component: AngkasSkpdComponent,
		canActivate: [ CanActiveGuardService ],
		data: { setTitle: 'Anggaran Kas SKPD' }
	},
	{
		path: 'anggarankas/angkaspemda',
		component: AngkasPemdaComponent,
		canActivate: [ CanActiveGuardService ],
		data: { setTitle: 'Anggaran Kas Pemda' }
	}
	,
// RKA-PERUBAHAN
{
	path: 'anggaranskpdp/rkapring',
	component: RkaPerubahanRingkasanComponent,
	canActivate: [ CanActiveGuardService ],
	data: { setTitle: 'RKA Perubahan-Ringkasan' }
},
{
	path: 'anggaranskpdp/rkappendapatan',
	component: RkaPerubahanPendapatanComponent,
	canActivate: [ CanActiveGuardService ],
	data: { setTitle: 'RKA Perubahan-Pendapatan' }
},
{
	path: 'anggaranskpdp/rkapbelanja',
	component: RkaPerubahanBelanjaComponent,
	canActivate: [ CanActiveGuardService ],
	data: { setTitle: 'RKA Perubahan-Belanja' }
},
{
	path: 'anggaranskpdp/rkaprincian',
	component: RkaPerubahanRincianComponent,
	canActivate: [ CanActiveGuardService ],
	data: { setTitle: 'RKA Perubahan-Rincian' }
},
{
	path: 'anggaranskpdp/rkapbiaya',
	component: RkaPerubahanPembiayaanComponent,
	canActivate: [ CanActiveGuardService ],
	data: { setTitle: 'RKA Perubahan-Pembiayaan' }
},
// SPD
	{
		path: 'spd/dokspd',
		component: SpdDokComponent,
		canActivate: [ CanActiveGuardService ],
		data: { setTitle: 'Dokumen SPD' }
	},
	{
		path: 'spd/lampspd',
		component: SpdLampComponent,
		canActivate: [ CanActiveGuardService ],
		data: { setTitle: 'Lampiran SPD' }
	},

// SPP	
	{
		path: 'permintaan-pembayaran/spp-up',
		component: SPPUPComponent,
		canActivate: [ CanActiveGuardService ],
		data: { setTitle: 'SPP-UP' }
	},
	{
		path: 'permintaan-pembayaran/spp-gu',
		component: SPPGUComponent,
		canActivate: [ CanActiveGuardService ],
		data: { setTitle: 'SPP-GU' }
	},
	{
		path: 'permintaan-pembayaran/spp-tu',
		component: SPPTUComponent,
		canActivate: [ CanActiveGuardService ],
		data: { setTitle: 'SPP-TU' }
	},
	{
		path: 'permintaan-pembayaran/spp-ls-gaji',
		component: SppLsgjComponent,
		canActivate: [ CanActiveGuardService ],
		data: { setTitle: 'SPP-LS Gaji dan Tunjangan' }
	},
	{
		path: 'permintaan-pembayaran/spp-ls-barang-jasa',
		component: SppLsbjComponent,
		canActivate: [ CanActiveGuardService ],
		data: { setTitle: 'SPP-LS Barang dan Jasa' }
	},
	{
		path: 'permintaan-pembayaran/spp-ls-pihak-ketiga',
		component: SppLsp3Component,
		canActivate: [ CanActiveGuardService ],
		data: { setTitle: 'SPP-LS Pihak ketiga dan lainnya' }
	},
	{
		path: 'permintaan-pembayaran/rencana-kebutuhan-tu',
		component: SppTurencanaComponent,
		canActivate: [ CanActiveGuardService ],
		data: { setTitle: 'Daftar Rencana Kebutuhan TU' }
	}
	//spm
	,
	{
		path: 'perintah-membayar/spm-up',
		component: SpmUpComponent,
		canActivate: [ CanActiveGuardService ],
		data: { setTitle: 'SPM UP' }
	},
	{
		path: 'perintah-membayar/spm-gu',
		component: SpmGuComponent,
		canActivate: [ CanActiveGuardService ],
		data: { setTitle: 'SPM GU' }
	},
	{
		path: 'perintah-membayar/spm-tu',
		component: SpmTuComponent,
		canActivate: [ CanActiveGuardService ],
		data: { setTitle: 'SPM TU' }
	},
	{
		path: 'perintah-membayar/spm-ls',
		component: SpmLsComponent,
		canActivate: [ CanActiveGuardService ],
		data: { setTitle: 'SPM LS' }
	},
	{
		path: 'perintah-membayar/surat-penolakan-spm',
		component: SpmTolakComponent,
		canActivate: [ CanActiveGuardService ],
		data: { setTitle: 'Surat Penolakan SPM' }
	},
	{
		path: 'perintah-membayar/register-penolakan-spm',
		component: RegspmTolakComponent,
		canActivate: [ CanActiveGuardService ],
		data: { setTitle: 'Register Surat Penolakan SPM' }
	}

	//SP2D
	,
	{
		path: 'pencairan-dana/sp2d',
		component: Sp2dComponent ,
		canActivate: [ CanActiveGuardService ],
		data: { setTitle: 'SP2D' }
	},
	{
		path: 'pencairan-dana/surat-penolakan-sp2d',
		component: Sp2dTolakComponent ,
		canActivate: [ CanActiveGuardService ],
		data: { setTitle: 'Surat Penolakan SP2D' }
	},
	{
		path: 'pencairan-dana/register-surat-penolakan-sp2d',
		component: Registersp2dTolakComponent ,
		canActivate: [ CanActiveGuardService ],
		data: { setTitle: 'Register Surat Penolakan SP2D' }
	},
	{
		path: 'pencairan-dana/register-sp2d-kuasa-bud',
		component: Registersp2dBudComponent ,
		canActivate: [ CanActiveGuardService ],
		data: { setTitle: 'Register SP2D Kuasa BUD' }
	}
	//Buku Bendahara Pengeluaran
	,
	{
		path: 'buku-bendahara-pengeluaran/register-spp-spm-sp2d',
		component: RegistersppSpmSp2dComponent ,
		canActivate: [ CanActiveGuardService ],
		data: { setTitle: 'Register SPP SPM SP2D' }
	},
	{
		path: 'buku-bendahara-pengeluaran/bku',
		component: BkubendPengeluaranComponent ,
		canActivate: [ CanActiveGuardService ],
		data: { setTitle: 'BKU' }
	},
	{
		path: 'buku-bendahara-pengeluaran/buku-pembantu-bank',
		component: BkubendBankComponent ,
		canActivate: [ CanActiveGuardService ],
		data: { setTitle: 'Buku Pembantu Bank' }
	},
	{
		path: 'buku-bendahara-pengeluaran/buku-pembantu-kas',
		component: BkubendKasComponent ,
		canActivate: [ CanActiveGuardService ],
		data: { setTitle: 'Buku Pembantu Kas' }
	},
	{
		path: 'buku-bendahara-pengeluaran/buku-pembantu-pajak',
		component: BkubendPajakComponent ,
		canActivate: [ CanActiveGuardService ],
		data: { setTitle: 'Buku Pembantu pajak' }
	},
	{
		path: 'buku-bendahara-pengeluaran/buku-pembantu-panjar',
		component: BkubendPanjarComponent ,
		canActivate: [ CanActiveGuardService ],
		data: { setTitle: 'Buku Pembantu Panjar' }
	},
	{
		path: 'buku-bendahara-pengeluaran/buku-pembantu-per-sub-rincian-objek',
		component: BkubendSubrincianComponent ,
		canActivate: [ CanActiveGuardService ],
		data: { setTitle: 'Buku Pembantu per Sub Rincian Objek' }
	},
	{
		path: 'buku-bendahara-pengeluaran/kartu-kendali-sub-kegiatan',
		component: KartukendaliSubkegiatanComponent ,
		canActivate: [ CanActiveGuardService ],
		data: { setTitle: 'Kartu Kendali sub Kegiatan' }
	}
	//Bukti Penerimaan
	,
	{
		path: 'bendaharapenerimaan/tbp',
		component: TbpComponent ,
		canActivate: [ CanActiveGuardService ],
		data: { setTitle: 'T B P' }
	},
	{
		path: 'bendaharapenerimaan/sts',
		component: StsComponent ,
		canActivate: [ CanActiveGuardService ],
		data: { setTitle: 'S T S' }
	},
	{
		path: 'bendaharapenerimaan/regsts',
		component: RegisterstsComponent ,
		canActivate: [ CanActiveGuardService ],
		data: { setTitle: 'S T S' }
	},
	{
		path: 'bendaharapenerimaan/nkb',
		component: NotakreditComponent ,
		canActivate: [ CanActiveGuardService ],
		data: { setTitle: 'Nota Kredit Bank' }
	}
	//BKU bend Penerimaan
	,
	{
		path: 'bendaharapenerimaan/bkutrskpd',
		component: BkubendPenerimaanComponent ,
		canActivate: [ CanActiveGuardService ],
		data: { setTitle: 'BKU Bendahara Penerimaan SKPD' }
	},
	{
		path: 'bendaharapenerimaan/bpsubrinctr',
		component: BkubendpenerimaanSubrinciComponent ,
		canActivate: [ CanActiveGuardService ],
		data: { setTitle: 'BKU Bendahara Penerimaan SKPD' }
	},
	{
		path: 'bendaharapenerimaan/bpbanktr',
		component: BkubendpenerimaanBankComponent ,
		canActivate: [ CanActiveGuardService ],
		data: { setTitle: 'Buku Pembantu BANK Penerimaan' }
	},
	{
		path: 'bendaharapenerimaan/bpkastunaitr',
		component: BkubendpenerimaanKasComponent ,
		canActivate: [ CanActiveGuardService ],
		data: { setTitle: 'Buku Pembantu Kas Tunai Penerimaan' }
	},
	//BUD
	{
		path: 'pembukuan-bud/bku-bud',
		component: BkuBudComponent ,
		canActivate: [ CanActiveGuardService ],
		data: { setTitle: 'BKU BUD' }
	},
	{
		path: 'pembukuan-bud/laporan-posisi-kas-harian',
		component: LpkhComponent ,
		canActivate: [ CanActiveGuardService ],
		data: { setTitle: 'Laporan Posisi Kas Harian' }
	},
	{
		path: 'pembukuan-bud/surat-persetujuan-tu',
		component: SpTuComponent ,
		canActivate: [ CanActiveGuardService ],
		data: { setTitle: 'Laporan Surat Persetujuan TU' }
	}
	//LPJ-PENERIMAAN
	,
	{
		path: 'lpj-penerimaan/lpjbendtr',
		component: LpjPenerimaanComponent ,
		canActivate: [ CanActiveGuardService ],
		data: { setTitle: 'LPJ Penerimaan' }
	},
	{
		path: 'lpj-penerimaan/lpjtrkr',
		component: LapPenerimaanComponent ,
		canActivate: [ CanActiveGuardService ],
		data: { setTitle: 'Laporan Penerimaan dan Penyetoran' }
	},
	{
		path: 'lpj-penerimaan/rekontr',
		component: RekonPenerimaanComponent ,
		canActivate: [ CanActiveGuardService ],
		data: { setTitle: 'Rekonsiliasi Penerimaan' }
	},
	{
		path: 'pertanggungjawaban-bendahara-pengeluaran/lpj-up',
		component: LpjUpComponent ,
		canActivate: [ CanActiveGuardService ],
		data: { setTitle: 'LPJ - UP' }
	},
	{
		path: 'pertanggungjawaban-bendahara-pengeluaran/lpj-tu',
		component: LpjTuComponent ,
		canActivate: [ CanActiveGuardService ],
		data: { setTitle: 'LPJ - TU' }
	},
	{
		path: 'pertanggungjawaban-bendahara-pengeluaran/laporan-penutupan-kas',
		component: LaptutupkasComponent ,
		canActivate: [ CanActiveGuardService ],
		data: { setTitle: 'Laporan Penutupan Kas' }
	},
	{
		path: 'pertanggungjawaban-bendahara-pengeluaran/spj-administratif',
		component: SpjadmPengeluaranComponent ,
		canActivate: [ CanActiveGuardService ],
		data: { setTitle: 'SPJ Administratif' }
	},
	{
		path: 'pertanggungjawaban-bendahara-pengeluaran/spj-fungsional',
		component: SpjfungPengeluaranComponent ,
		canActivate: [ CanActiveGuardService ],
		data: { setTitle: 'SPJ Fungsional' }
	},
	//pengajuan belanja
	{
		path: 'pengajuan-belanja/nota-pencairan-dana',
		component: NotapencairandanaComponent ,
		canActivate: [ CanActiveGuardService ],
		data: { setTitle: 'Nota Pencairan Dana' }
	}
	
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class LaporanRoutingModule { }
