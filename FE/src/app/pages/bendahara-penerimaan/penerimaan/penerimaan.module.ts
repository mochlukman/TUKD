import { NgModule } from '@angular/core';
import { PenerimaanRoutingModule } from './penerimaan-routing.module';
import { MainTanpaPenetapanComponent } from './main-tanpa-penetapan/main-tanpa-penetapan.component';
import { MainPenetapanComponent } from './main-penetapan/main-penetapan.component';
import { TanpaPenetapanComponent } from './main-tanpa-penetapan/tanpa-penetapan/tanpa-penetapan.component';
import { TanpaPenetapanFormComponent } from './main-tanpa-penetapan/tanpa-penetapan/tanpa-penetapan-form/tanpa-penetapan-form.component';
import { TanpaPenetapanMainRincianComponent } from './main-tanpa-penetapan/tanpa-penetapan/tanpa-penetapan-main-rincian/tanpa-penetapan-main-rincian.component';
import { TanpaPenetapanRincianFormComponent } from './main-tanpa-penetapan/tanpa-penetapan/tanpa-penetapan-main-rincian/tanpa-penetapan-rincian-form/tanpa-penetapan-rincian-form.component';
import { CoreModule } from 'src/app/core/core.module';
import { SharedModule } from 'src/app/shared/shared.module';
import { TanpaPenetapanRincianComponent } from './main-tanpa-penetapan/tanpa-penetapan/tanpa-penetapan-main-rincian/tanpa-penetapan-rincian/tanpa-penetapan-rincian.component';
import { PenetapanComponent } from './main-penetapan/penetapan/penetapan.component';
import { PenetapanFormComponent } from './main-penetapan/penetapan/penetapan-form/penetapan-form.component';
import { PenetapanMainRincianComponent } from './main-penetapan/penetapan/penetapan-main-rincian/penetapan-main-rincian.component';
import { PenetapanRincianComponent } from './main-penetapan/penetapan/penetapan-main-rincian/penetapan-rincian/penetapan-rincian.component';
import { PenetapanRincianFormComponent } from './main-penetapan/penetapan/penetapan-main-rincian/penetapan-rincian-form/penetapan-rincian-form.component';


@NgModule({
  declarations: [
    MainTanpaPenetapanComponent, 
    MainPenetapanComponent, 
    TanpaPenetapanComponent, 
    TanpaPenetapanFormComponent, 
    TanpaPenetapanMainRincianComponent, 
    TanpaPenetapanRincianFormComponent,
    TanpaPenetapanRincianComponent,
    PenetapanComponent,
    PenetapanFormComponent,
    PenetapanMainRincianComponent,
    PenetapanRincianComponent,
    PenetapanRincianFormComponent
  ],
  imports: [
    CoreModule,
    SharedModule,
    PenerimaanRoutingModule
  ]
})
export class PenerimaanModule { }
