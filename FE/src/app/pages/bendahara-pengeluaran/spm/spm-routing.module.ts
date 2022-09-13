import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { CanActiveGuardService } from 'src/app/core/_commonServices/can-active-guard.service';
import { SpmGuComponent } from './spm-gu/spm-gu.component';
import { SpmLsNonpegawaiComponent } from './spm-ls-nonpegawai/spm-ls-nonpegawai.component';
import { SpmLsPegawaiComponent } from './spm-ls-pegawai/spm-ls-pegawai.component';
import { SpmLsUangmukaComponent } from './spm-ls-uangmuka/spm-ls-uangmuka.component';
import { SpmPembiayaanComponent } from './spm-pembiayaan/spm-pembiayaan.component';
import { SpmTuComponent } from './spm-tu/spm-tu.component';
import { SpmUpComponent } from './spm-up/spm-up.component';


const routes: Routes = [
  {
    path: 'spm-up',
    component: SpmUpComponent,
    canActivate: [CanActiveGuardService],
    data: {setTitle: 'SPM - UP'}
  },
  {
    path: 'spm-gu',
    component: SpmGuComponent,
    canActivate: [CanActiveGuardService],
    data: {setTitle: 'SPM - GU'}
  },
  {
    path: 'spm-tu',
    component: SpmTuComponent,
    canActivate: [CanActiveGuardService],
    data: {setTitle: 'SPM - TU'}
  },
  {
    path: 'spm-ls-uang-muka',
    component: SpmLsUangmukaComponent,
    canActivate: [CanActiveGuardService],
    data: {setTitle: 'SPM - LS Uang Muka'}
  },
  {
    path: 'spm-ls-pegawai',
    component: SpmLsPegawaiComponent,
    canActivate: [CanActiveGuardService],
    data: {setTitle: 'SPM - LS Gaji dan Tunjangan'}
  },
  {
    path: 'spm-ls-non-pegawai',
    component: SpmLsNonpegawaiComponent,
    canActivate: [CanActiveGuardService],
    data: {setTitle: 'SPM - LS Barang dan Jasa'}
  },
  {
    path: 'spm-pembiayaan',
    component: SpmPembiayaanComponent,
    canActivate: [CanActiveGuardService],
    data: {setTitle: 'SPM - Pembiayaan'}
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class SpmRoutingModule { }
