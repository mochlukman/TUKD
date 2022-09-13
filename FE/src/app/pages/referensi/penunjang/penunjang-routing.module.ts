import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { CanActiveGuardService } from 'src/app/core/_commonServices/can-active-guard.service';
import { GolonganComponent } from './golongan/golongan.component';
import { IndikatorKinerjaComponent } from './indikator-kinerja/indikator-kinerja.component';
import { PegawaiComponent } from './pegawai/pegawai.component';


const routes: Routes = [
  {
    path: 'golongan',
    component: GolonganComponent,
    canActivate: [CanActiveGuardService],
    data: {setTitle: 'Golongan'}
  },
  {
    path: 'pegawai',
    component: PegawaiComponent,
    canActivate: [CanActiveGuardService],
    data: {setTitle: 'Pegawai'}
  },
  {
    path: 'indikator-kinerja',
    component: IndikatorKinerjaComponent,
    canActivate: [CanActiveGuardService],
    data: {setTitle: 'Indikator Kinerja'}
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class PenunjangRoutingModule { }
