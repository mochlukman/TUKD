import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AuthGuard } from '../core/guards/auth.guard';
import { CanActiveGuardService } from '../core/_commonServices/can-active-guard.service';
import { NotfoundComponent } from './notfound/notfound.component';


const routes: Routes = [
  {
    path: '', 
    children: [
      {
				path: '',
				redirectTo: 'dashboard',
				pathMatch: 'full'
			},
      {
        path: 'dashboard',
				loadChildren: () => import('./home/home.module').then(m => m.HomeModule),
        canActivate: [AuthGuard]
      },
      {
        path: 'pengaturan',
        loadChildren: () => import('./pengaturan/pengaturan.module').then(m => m.PengaturanModule),
        canActivate: [AuthGuard]
      },
      {
        path: 'referensi',
        loadChildren: () => import('./referensi/referensi.module').then(m => m.ReferensiModule),
        canActivate: [AuthGuard]
      },
      {
        path: 'anggaran',
        loadChildren: () => import('./anggaran/anggaran.module').then(m => m.AnggaranModule),
        canActivate: [AuthGuard]
      },
      {
        path: 'pelaksanaan-anggaran',
        loadChildren: () => import('./pelaksanaan-anggaran/pelaksanaan-anggaran.module').then(m => m.PelaksanaanAnggaranModule),
        canActivate: [AuthGuard]
      },
      {
        path: 'bendahara-pengeluaran',
        loadChildren: () => import('./bendahara-pengeluaran/bendahara-pengeluaran.module').then(m => m.BendaharaPengeluaranModule),
        canActivate: [AuthGuard]
      },
      {
        path: 'bendahara-penerimaan',
        loadChildren: () => import('./bendahara-penerimaan/bendahara-penerimaan.module').then(m => m.BendaharaPenerimaanModule),
        canActivate: [AuthGuard]
      },
      {
        path: 'b-u-d',
        loadChildren: () => import('./bud/bud.module').then(m => m.BudModule),
        canActivate: [AuthGuard]
      },
      {
        path: 'memorial',
        loadChildren: () => import('./akuntansi/memorial/memorial.module').then(m => m.MemorialModule),
        canActivate: [AuthGuard]
      },
      {
        path: 'laporan',
        loadChildren: () => import('./laporan/laporan.module').then(m => m.LaporanModule),
        canActivate: [AuthGuard]
      },
			{
        path: '**',
				component: NotfoundComponent,
				canActivate: [ CanActiveGuardService ],
				data: {setTitle: 'Halaman Tidak Ditemukan'}
      },
    ]
  },
  
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class PagesRoutingModule { }
