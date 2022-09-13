import { NgModule } from '@angular/core';

import { PergeseranRoutingModule } from './pergeseran-routing.module';
import { CoreModule } from 'src/app/core/core.module';
import { SharedModule } from 'src/app/shared/shared.module';
import { PergeseranComponent } from './pergeseran/pergeseran.component';
import { PergeseranUangComponent } from './pergeseran/pergeseran-uang/pergeseran-uang.component';
import { PergeseranUangFormComponent } from './pergeseran/pergeseran-uang/pergeseran-uang-form/pergeseran-uang-form.component';
import { PergeseranUangDetailComponent } from './pergeseran/pergeseran-uang/pergeseran-uang-detail/pergeseran-uang-detail.component';
import { PergeseranUangDetailRincianComponent } from './pergeseran/pergeseran-uang/pergeseran-uang-detail/pergeseran-uang-detail-rincian/pergeseran-uang-detail-rincian.component';
import { PergeseranUangDetailRincianFormComponent } from './pergeseran/pergeseran-uang/pergeseran-uang-detail/pergeseran-uang-detail-rincian-form/pergeseran-uang-detail-rincian-form.component';


@NgModule({
  declarations: [
    PergeseranComponent,
    PergeseranUangComponent,
    PergeseranUangFormComponent,
    PergeseranUangDetailComponent,
    PergeseranUangDetailRincianComponent,
    PergeseranUangDetailRincianFormComponent
  ],
  imports: [
    CoreModule,
    SharedModule,
    PergeseranRoutingModule
  ]
})
export class PergeseranModule { }
