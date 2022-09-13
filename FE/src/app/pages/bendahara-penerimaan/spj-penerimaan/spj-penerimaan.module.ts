import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { SPjPenerimaanRoutingModule } from './spj-penerimaan-routing.module';
import { CoreModule } from 'src/app/core/core.module';
import { SharedModule } from 'src/app/shared/shared.module';
import { SpjMainPageComponent } from './spj-main-page/spj-main-page.component';
import { SpjPengajuanComponent } from './spj-pengajuan/spj-pengajuan.component';
import { SpjPengajuanFormComponent } from './spj-pengajuan/spj-pengajuan-form/spj-pengajuan-form.component';
import { SpjPengajuanMainRincianComponent } from './spj-pengajuan/spj-pengajuan-main-rincian/spj-pengajuan-main-rincian.component';
import { SpjBkuPenerimaanComponent } from './spj-pengajuan/spj-pengajuan-main-rincian/spj-bku-penerimaan/spj-bku-penerimaan.component';
import { SpjBkuPenerimaanFormComponent } from './spj-pengajuan/spj-pengajuan-main-rincian/spj-bku-penerimaan-form/spj-bku-penerimaan-form.component';
import { SpjBkuPenyetoranFormComponent } from './spj-pengajuan/spj-pengajuan-main-rincian/spj-bku-penyetoran-form/spj-bku-penyetoran-form.component';
import { SpjBkuPenyetoranComponent } from './spj-pengajuan/spj-pengajuan-main-rincian/spj-bku-penyetoran/spj-bku-penyetoran.component';


@NgModule({
  declarations: [
  SpjMainPageComponent,
  SpjPengajuanComponent,
  SpjPengajuanFormComponent,
  SpjPengajuanMainRincianComponent,
  SpjBkuPenerimaanComponent,
  SpjBkuPenerimaanFormComponent,
  SpjBkuPenyetoranFormComponent,
  SpjBkuPenyetoranComponent
  ],
  imports: [
    CoreModule,
    SharedModule,
    SPjPenerimaanRoutingModule
  ]
})
export class SPjPenerimaanModule { }
