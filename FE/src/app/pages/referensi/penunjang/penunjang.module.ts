import { NgModule } from '@angular/core';
import { PenunjangRoutingModule } from './penunjang-routing.module';
import { CoreModule } from 'src/app/core/core.module';
import { GolonganComponent } from './golongan/golongan.component';
import { IndikatorKinerjaComponent } from './indikator-kinerja/indikator-kinerja.component';
import { PegawaiComponent } from './pegawai/pegawai.component';
import { SharedModule } from 'src/app/shared/shared.module';
import { PegawaiFormComponent } from './pegawai/pegawai-form/pegawai-form.component';


@NgModule({
  declarations: [
    GolonganComponent,
    IndikatorKinerjaComponent,
    PegawaiComponent,
    PegawaiFormComponent
  ],
  imports: [
    CoreModule,
    SharedModule,
    PenunjangRoutingModule
  ]
})
export class PenunjangModule { }
