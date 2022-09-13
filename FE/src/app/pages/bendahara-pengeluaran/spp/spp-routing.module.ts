import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { CanActiveGuardService } from 'src/app/core/_commonServices/can-active-guard.service';
import { SppGuComponent } from './spp-gu/spp-gu.component';
import { SppLsNonpegawaiComponent } from './spp-ls-nonpegawai/spp-ls-nonpegawai.component';
import { SppLsPegawaiComponent } from './spp-ls-pegawai/spp-ls-pegawai.component';
import { SppLsUangmukaComponent } from './spp-ls-uangmuka/spp-ls-uangmuka.component';
import { SppPembiayaanComponent } from './spp-pembiayaan/spp-pembiayaan.component';
import { SppPengajuanTuComponent } from './spp-pengajuan-tu/spp-pengajuan-tu.component';
import { SppTuComponent } from './spp-tu/spp-tu.component';
import { SppUpComponent } from './spp-up/spp-up.component';


const routes: Routes = [
  {
    path: 'spp-up',
    component: SppUpComponent,
    canActivate: [CanActiveGuardService],
    data: {setTitle: 'Bendahara Pengeluaran  >  SPP  >  SPP - UP'}
  },
  {
    path: 'spp-gu',
    component: SppGuComponent,
    canActivate: [CanActiveGuardService],
    data: {setTitle: 'SPP - GU'}
  },
  {
    path: 'spp-ls-uang-muka',
    component: SppLsUangmukaComponent,
    canActivate: [CanActiveGuardService],
    data: {setTitle: 'SPP - LS Uang Muka'}
  },
  {
    path: 'spp-ls-pegawai',
    component: SppLsPegawaiComponent,
    canActivate: [CanActiveGuardService],
    data: {setTitle: 'SPP - LS Gaji dan Tunjangan'}
  },
  {
    path: 'spp-ls-non-pegawai',
    component: SppLsNonpegawaiComponent,
    canActivate: [CanActiveGuardService],
    data: {setTitle: 'SPP - LS Barang dan Jasa'}
  },
  {
    path: 'spp-pembiayaan',
    component: SppPembiayaanComponent,
    canActivate: [CanActiveGuardService],
    data: {setTitle: 'SPP - Pembiayaan'}
  },
  {
    path: 'pengajuan-tu',
    component: SppPengajuanTuComponent,
    canActivate: [CanActiveGuardService],
    data: {setTitle: 'SPP - Pengajuan TU'}
  },
  {
    path: 'spp-tu',
    component: SppTuComponent,
    canActivate: [CanActiveGuardService],
    data: {setTitle: 'SPP - TU'}
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class SppRoutingModule { }
