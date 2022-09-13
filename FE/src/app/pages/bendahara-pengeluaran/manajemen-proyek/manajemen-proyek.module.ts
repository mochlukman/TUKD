import { NgModule } from '@angular/core';

import { ManajemenProyekRoutingModule } from './manajemen-proyek-routing.module';
import { RekananComponent } from './rekanan/rekanan.component';
import { CoreModule } from 'src/app/core/core.module';
import { SharedModule } from 'src/app/shared/shared.module';
import { FormRekenanComponent } from './rekanan/form-rekenan/form-rekenan.component';
import { KontrakComponent } from './kontrak/kontrak.component';
import { FormKontrakComponent } from './kontrak/form-kontrak/form-kontrak.component';
import { TagihanComponent } from './tagihan/tagihan.component';
import { FormTagihanComponent } from './tagihan/form-tagihan/form-tagihan.component';
import { TagihanDetailComponent } from './tagihan/tagihan-detail/tagihan-detail.component';
import { FormTagihanDetailComponent } from './tagihan/tagihan-detail/form-tagihan-detail/form-tagihan-detail.component';
import { KontrakDetailComponent } from './kontrak/kontrak-detail/kontrak-detail.component';
import { FormKontrakDetailComponent } from './kontrak/kontrak-detail/form-kontrak-detail/form-kontrak-detail.component';
import { TagihanModalComponent } from './tagihan/tagihan-modal/tagihan-modal.component';
import { TagihanNonModalComponent } from './tagihan/tagihan-non-modal/tagihan-non-modal.component';
import { AdendumComponent } from './adendum/adendum.component';
import { FormAdendumComponent } from './adendum/form-adendum/form-adendum.component';


@NgModule({
  declarations: [
    RekananComponent,
    FormRekenanComponent,
    KontrakComponent,
    FormKontrakComponent,
    TagihanComponent,
    FormTagihanComponent,
    TagihanDetailComponent,
    FormTagihanDetailComponent,
    KontrakDetailComponent,
    FormKontrakDetailComponent,
    TagihanModalComponent,
    TagihanNonModalComponent,
    AdendumComponent,
    FormAdendumComponent
  ],
  imports: [
    CoreModule,
    SharedModule,
    ManajemenProyekRoutingModule,
  ]
})
export class ManajemenProyekModule { }
