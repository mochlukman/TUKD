import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { CanActiveGuardService } from 'src/app/core/_commonServices/can-active-guard.service';
import { AkunComponent } from './akun/akun.component';
import { ProgramKegiatanComponent } from './program-kegiatan/program-kegiatan.component';
import { UnitsComponent } from './units/units.component';
import { UrusanComponent } from './urusan/urusan.component';


const routes: Routes = [
  {
    path: 'urusan',
    component: UrusanComponent,
    canActivate: [CanActiveGuardService],
    data: {setTitle: 'Urusan'}
  },
  {
    path: 'unit-organisasi',
    component: UnitsComponent,
    canActivate: [CanActiveGuardService],
    data: {setTitle: 'Unit Organisasi'}
  },
  {
    path: 'program-kegiatan',
    component: ProgramKegiatanComponent,
    canActivate: [CanActiveGuardService],
    data: {setTitle: 'Program Kegiatan'}
  },
  {
    path: 'akun',
    component: AkunComponent,
    canActivate: [CanActiveGuardService],
    data: {setTitle: 'Akun'}
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class PendukungRoutingModule { }
