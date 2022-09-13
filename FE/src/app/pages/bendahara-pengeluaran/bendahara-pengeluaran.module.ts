import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { BendaharaPengeluaranRoutingModule } from './bendahara-pengeluaran-routing.module';
import { CoreModule } from 'src/app/core/core.module';
import { SharedModule } from 'src/app/shared/shared.module';


@NgModule({
  declarations: [],
  imports: [
    CoreModule,
    SharedModule,
    BendaharaPengeluaranRoutingModule
  ]
})
export class BendaharaPengeluaranModule { }
