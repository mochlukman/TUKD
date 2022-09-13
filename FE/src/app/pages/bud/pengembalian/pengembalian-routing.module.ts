import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { CanActiveGuardService } from 'src/app/core/_commonServices/can-active-guard.service';
import { PembiayaanComponent } from './pembiayaan/pembiayaan.component';
import { PendapatanComponent } from './pendapatan/pendapatan.component';


const routes: Routes = [
  {
    path:'pendapatan',
    component: PendapatanComponent,
    canActivate: [CanActiveGuardService],
    data: {setTitle: 'SP2D Pengembalian Pendapatan'}
  },
  {
    path:'penerimaan-pembiayaan',
    component: PembiayaanComponent,
    canActivate: [CanActiveGuardService],
    data: {setTitle: 'SP2D Penerimaan Pembiayaan'}
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class PengembalianRoutingModule { }
