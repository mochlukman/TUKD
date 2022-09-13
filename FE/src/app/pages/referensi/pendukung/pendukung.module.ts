import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { PendukungRoutingModule } from './pendukung-routing.module';
import { CoreModule } from 'src/app/core/core.module';
import { SharedModule } from 'src/app/shared/shared.module';
import { UrusanComponent } from './urusan/urusan.component';
import { UnitsComponent } from './units/units.component';
import { AkunComponent } from './akun/akun.component';
import { AkunPendapatanComponent } from './akun/akun-pendapatan/akun-pendapatan.component';
import { AkunBelanjaComponent } from './akun/akun-belanja/akun-belanja.component';
import { AkunPembiayaanComponent } from './akun/akun-pembiayaan/akun-pembiayaan.component';
import { ProgramKegiatanComponent } from './program-kegiatan/program-kegiatan.component';
import { KegiatanListComponent } from './program-kegiatan/kegiatan-list/kegiatan-list.component';
import { UrusanFormComponent } from './urusan/urusan-form/urusan-form.component';
import { UnitsFormComponent } from './units/units-form/units-form.component';
import { ProgramFormComponent } from './program-kegiatan/program-form/program-form.component';
import { KegiatanFormComponent } from './program-kegiatan/kegiatan-list/kegiatan-form/kegiatan-form.component';
import { AkunPendapatanFormComponent } from './akun/akun-pendapatan/akun-pendapatan-form/akun-pendapatan-form.component';
import { AkunBelanjaFormComponent } from './akun/akun-belanja/akun-belanja-form/akun-belanja-form.component';
import { AkunPembiayaanFormComponent } from './akun/akun-pembiayaan/akun-pembiayaan-form/akun-pembiayaan-form.component';


@NgModule({
  declarations: [
    UrusanComponent,
    UnitsComponent,
    AkunComponent,
    AkunPendapatanComponent,
    AkunBelanjaComponent,
    AkunPembiayaanComponent,
    ProgramKegiatanComponent,
    KegiatanListComponent,
    UrusanFormComponent,
    UnitsFormComponent,
    ProgramFormComponent,
    KegiatanFormComponent,
    AkunPendapatanFormComponent,
    AkunBelanjaFormComponent,
    AkunPembiayaanFormComponent
  ],
  imports: [
    CoreModule,
    SharedModule,
    PendukungRoutingModule
  ]
})
export class PendukungModule { }
