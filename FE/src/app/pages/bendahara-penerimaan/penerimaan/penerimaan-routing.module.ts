import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { CanActiveGuardService } from 'src/app/core/_commonServices/can-active-guard.service';
import { MainPenetapanComponent } from './main-penetapan/main-penetapan.component';
import { MainTanpaPenetapanComponent } from './main-tanpa-penetapan/main-tanpa-penetapan.component';


const routes: Routes = [
  {
    path: 'tanpa-penetapan',
    component: MainTanpaPenetapanComponent,
    canActivate: [CanActiveGuardService],
    data: {setTitle: 'TBP Tanpa Penetapan'}
  },
  {
    path: 'penetapan',
    component: MainPenetapanComponent,
    canActivate: [CanActiveGuardService],
    data: {setTitle: 'TBP Penetapan'}
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class PenerimaanRoutingModule { }
