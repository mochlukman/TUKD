import { NgModule } from '@angular/core';
import { LaporanRoutingModule } from './laporan-routing.module';
import { CoreModule } from 'src/app/core/core.module';
import { PersetujuanDpaComponent } from './dpa-skpd/persetujuan-dpa/persetujuan-dpa.component';
import { DpaskpdComponent } from './dpa-skpd/dpaskpd/dpaskpd.component';
import { SharedModule } from 'src/app/shared/shared.module';
import { DpaRingkasanComponent } from './dpa-skpd/dpa-ringkasan/dpa-ringkasan.component';
import { DpaPendapatanComponent } from './dpa-skpd/dpa-pendapatan/dpa-pendapatan.component';
import { DpaBelanjaComponent } from './dpa-skpd/dpa-belanja/dpa-belanja.component';
import { DpaRincianComponent } from './dpa-skpd/dpa-rincian/dpa-rincian.component';
import { DpaPembiayaanComponent } from './dpa-skpd/dpa-pembiayaan/dpa-pembiayaan.component';
import { AngkasSkpdComponent } from './anggarankas/angkas-skpd/angkas-skpd.component';
import { AngkasPemdaComponent } from './anggarankas/angkas-pemda/angkas-pemda.component';
import { SpdDokComponent } from './spd/spd-dok/spd-dok.component';
import { SpdLampComponent } from './spd/spd-lamp/spd-lamp.component';
import { SPPUPComponent } from './SPP/spp-up/spp-up.component';
import { SPPGUComponent } from './SPP/spp-gu/spp-gu.component';
import { SPPTUComponent } from './SPP/spp-tu/spp-tu.component';
import { SppLsgjComponent } from './SPP/spp-lsgj/spp-lsgj.component';
import { SppLsbjComponent } from './SPP/spp-lsbj/spp-lsbj.component';
import { SppLsp3Component } from './SPP/spp-lsp3/spp-lsp3.component';
import { SppTurencanaComponent } from './SPP/spp-turencana/spp-turencana.component';
import { SpmUpComponent } from './spm/spm-up/spm-up.component';
import { SpmGuComponent } from './spm/spm-gu/spm-gu.component';
import { SpmTuComponent } from './spm/spm-tu/spm-tu.component';
import { SpmLsComponent } from './spm/spm-ls/spm-ls.component';
import { SpmTolakComponent } from './spm/spm-tolak/spm-tolak.component';
import { RegspmTolakComponent } from './spm/regspm-tolak/regspm-tolak.component';
import { Sp2dComponent } from './sp2d/sp2d/sp2d.component';
import { Sp2dTolakComponent } from './sp2d/sp2d-tolak/sp2d-tolak.component';
import { Registersp2dTolakComponent } from './sp2d/registersp2d-tolak/registersp2d-tolak.component';
import { Registersp2dBudComponent } from './sp2d/registersp2d-bud/registersp2d-bud.component';
import { RegistersppSpmSp2dComponent } from './sp2d/registerspp-spm-sp2d/registerspp-spm-sp2d.component';
import { BkubendPengeluaranComponent } from './bku/bkubend-pengeluaran/bkubend-pengeluaran.component';
import { BkubendBankComponent } from './bku/bkubend-bank/bkubend-bank.component';
import { BkubendKasComponent } from './bku/bkubend-kas/bkubend-kas.component';
import { BkubendPajakComponent } from './bku/bkubend-pajak/bkubend-pajak.component';
import { BkubendPanjarComponent } from './bku/bkubend-panjar/bkubend-panjar.component';
import { BkubendSubrincianComponent } from './bku/bkubend-subrincian/bkubend-subrincian.component';
import { KartukendaliSubkegiatanComponent } from './bku/kartukendali-subkegiatan/kartukendali-subkegiatan.component';
import { TbpComponent } from './bukti-penerimaan/tbp/tbp.component';
import { StsComponent } from './bukti-penerimaan/sts/sts.component';
import { RegisterstsComponent } from './bukti-penerimaan/registersts/registersts.component';
import { BkubendPenerimaanComponent } from './bku-penerimaan/bkubend-penerimaan/bkubend-penerimaan.component';
import { BkubendpenerimaanSubrinciComponent } from './bku-penerimaan/bkubendpenerimaan-subrinci/bkubendpenerimaan-subrinci.component';
import { BkuBudComponent } from './bud/bku-bud/bku-bud.component';
import { LpkhComponent } from './bud/lpkh/lpkh.component';
import { SpTuComponent } from './bud/sp-tu/sp-tu.component';
import { LpjPenerimaanComponent } from './lpj-penerimaan/lpj-penerimaan/lpj-penerimaan.component';
import { LapPenerimaanComponent } from './lpj-penerimaan/lap-penerimaan/lap-penerimaan.component';
import { RekonPenerimaanComponent } from './lpj-penerimaan/rekon-penerimaan/rekon-penerimaan.component';
import { SpjfungPengeluaranComponent } from './spj-pengeluaran/spjfung-pengeluaran/spjfung-pengeluaran.component';
import { SpjadmPengeluaranComponent } from './spj-pengeluaran/spjadm-pengeluaran/spjadm-pengeluaran.component';
import { LpjUpComponent } from './lpj-pengeluaran/lpj-up/lpj-up.component';
import { LpjTuComponent } from './lpj-pengeluaran/lpj-tu/lpj-tu.component';
import { LaptutupkasComponent } from './lpj-pengeluaran/laptutupkas/laptutupkas.component';
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
import { BkubendpenerimaanKasComponent } from './bku-penerimaan/bkubendpenerimaan-kas/bkubendpenerimaan-kas.component';
import { RkaPerubahanPendapatanComponent } from './rka-perubahan/rka-perubahan-pendapatan/rka-perubahan-pendapatan.component';
import { RkaPerubahanBelanjaComponent } from './rka-perubahan/rka-perubahan-belanja/rka-perubahan-belanja.component';
import { RkaPerubahanPembiayaanComponent } from './rka-perubahan/rka-perubahan-pembiayaan/rka-perubahan-pembiayaan.component';
import { RkaPerubahanRincianComponent } from './rka-perubahan/rka-perubahan-rincian/rka-perubahan-rincian.component';
import { RkaPerubahanRingkasanComponent } from './rka-perubahan/rka-perubahan-ringkasan/rka-perubahan-ringkasan.component';



@NgModule({
  imports: [ 
    CoreModule,
    SharedModule,
    LaporanRoutingModule ],
  declarations: [
    PersetujuanDpaComponent, 
    DpaskpdComponent, DpaRingkasanComponent, DpaPendapatanComponent, DpaBelanjaComponent, DpaRincianComponent, DpaPembiayaanComponent, AngkasSkpdComponent, AngkasPemdaComponent, SpdDokComponent, SpdLampComponent, SPPUPComponent, SPPGUComponent, SPPTUComponent, SppLsgjComponent, SppLsbjComponent, SppLsp3Component, SppTurencanaComponent, SpmUpComponent, SpmGuComponent, SpmTuComponent, SpmLsComponent, SpmTolakComponent, RegspmTolakComponent, Sp2dComponent, Sp2dTolakComponent, Registersp2dTolakComponent, Registersp2dBudComponent, RegistersppSpmSp2dComponent, BkubendPengeluaranComponent, BkubendBankComponent, BkubendKasComponent, BkubendPajakComponent, BkubendPanjarComponent, BkubendSubrincianComponent, KartukendaliSubkegiatanComponent, TbpComponent, StsComponent, RegisterstsComponent, BkubendPenerimaanComponent, BkubendpenerimaanSubrinciComponent, BkuBudComponent, LpkhComponent, SpTuComponent, LpjPenerimaanComponent, LapPenerimaanComponent, RekonPenerimaanComponent, SpjfungPengeluaranComponent, SpjadmPengeluaranComponent, LpjUpComponent, LpjTuComponent, LaptutupkasComponent, NotakreditComponent, NotapencairandanaComponent, RkaRingkasanComponent, RkaPendapatanComponent, RkaBelanjaComponent, RkaRincianComponent, RkaPembiayaanComponent, ApbdringkasanComponent, ApbdringkasanUrusComponent, ApbdrincianComponent, PenjabaranRingkasanComponent, PenjabaranRincianComponent, BkubendpenerimaanBankComponent, BkubendpenerimaanKasComponent, RkaPerubahanPendapatanComponent, RkaPerubahanBelanjaComponent, RkaPerubahanPembiayaanComponent, RkaPerubahanRincianComponent, RkaPerubahanRingkasanComponent
  ]
 
})
export class LaporanModule { }
