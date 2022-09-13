import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { PenyetoranRoutingModule } from './penyetoran-routing.module';
import { MainPendapatanComponent } from './main-pendapatan/main-pendapatan.component';
import { CoreModule } from 'src/app/core/core.module';
import { SharedModule } from 'src/app/shared/shared.module';
import { DPengajuanComponent } from './main-pendapatan/dpengajuan/dpengajuan.component';
import { DPengajuanFormComponent } from './main-pendapatan/dpengajuan/dpengajuan-form/dpengajuan-form.component';
import { DPengajuanMainRincianComponent } from './main-pendapatan/dpengajuan/dpengajuan-main-rincian/dpengajuan-main-rincian.component';
import { DPengajuanTbpComponent } from './main-pendapatan/dpengajuan/dpengajuan-main-rincian/dpengajuan-tbp/dpengajuan-tbp.component';
import { DPengajuanRekeningComponent } from './main-pendapatan/dpengajuan/dpengajuan-main-rincian/dpengajuan-rekening/dpengajuan-rekening.component';
import { DPengajuanRekeningFormComponent } from './main-pendapatan/dpengajuan/dpengajuan-main-rincian/dpengajuan-rekening-form/dpengajuan-rekening-form.component';
import { MainPembiayaanComponent } from './main-pembiayaan/main-pembiayaan.component';
import { BPengajuanComponent } from './main-pembiayaan/bpengajuan/bpengajuan.component';
import { BPengajuanFormComponent } from './main-pembiayaan/bpengajuan/bpengajuan-form/bpengajuan-form.component';
import { BPengajuanMainRincianComponent } from './main-pembiayaan/bpengajuan/bpengajuan-main-rincian/bpengajuan-main-rincian.component';
import { BpengajuanRekeningComponent } from './main-pembiayaan/bpengajuan/bpengajuan-main-rincian/bpengajuan-rekening/bpengajuan-rekening.component';
import { BpengajuanRekeningFormComponent } from './main-pembiayaan/bpengajuan/bpengajuan-main-rincian/bpengajuan-rekening-form/bpengajuan-rekening-form.component';
import { MainStsComponent } from './main-sts/main-sts.component';
import { StsPengajuanComponent } from './main-sts/sts-pengajuan/sts-pengajuan.component';
import { StsPengajuanFormComponent } from './main-sts/sts-pengajuan/sts-pengajuan-form/sts-pengajuan-form.component';
import { StsPengajuanMainRincianComponent } from './main-sts/sts-pengajuan/sts-pengajuan-main-rincian/sts-pengajuan-main-rincian.component';
import { StsPengajuanRekeningComponent } from './main-sts/sts-pengajuan/sts-pengajuan-main-rincian/sts-pengajuan-rekening/sts-pengajuan-rekening.component';
import { StsPengajuanRekeningFormComponent } from './main-sts/sts-pengajuan/sts-pengajuan-main-rincian/sts-pengajuan-rekening-form/sts-pengajuan-rekening-form.component';


@NgModule({
  declarations: [
    MainPendapatanComponent,
    DPengajuanComponent,
    DPengajuanFormComponent,
    DPengajuanMainRincianComponent,
    DPengajuanTbpComponent,
    DPengajuanRekeningComponent,
    DPengajuanRekeningFormComponent,
    MainPembiayaanComponent,
    BPengajuanComponent,
    BPengajuanFormComponent,
    BPengajuanMainRincianComponent,
    BpengajuanRekeningComponent,
    BpengajuanRekeningFormComponent,
    MainStsComponent,
    StsPengajuanComponent,
    StsPengajuanFormComponent,
    StsPengajuanMainRincianComponent,
    StsPengajuanRekeningComponent,
    StsPengajuanRekeningFormComponent
  ],
  imports: [
    CoreModule,
    SharedModule,
    PenyetoranRoutingModule
  ]
})
export class PenyetoranModule { }
