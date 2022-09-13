import { NgModule } from '@angular/core';
import { PanjarRoutingModule } from './panjar-routing.module';
import { PanjarMainComponent } from './panjar-main/panjar-main.component';
import { CoreModule } from 'src/app/core/core.module';
import { SharedModule } from 'src/app/shared/shared.module';
import { PanjarComponent } from './panjar-main/panjar/panjar.component';
import { PanjarFormComponent } from './panjar-main/panjar/panjar-form/panjar-form.component';
import { PanjarRincianComponent } from './panjar-main/panjar/panjar-rincian/panjar-rincian.component';
import { PanjarRincianFormComponent } from './panjar-main/panjar/panjar-rincian-form/panjar-rincian-form.component';
import { PanjarRincianKegiatanComponent } from './panjar-main/panjar/panjar-rincian-kegiatan/panjar-rincian-kegiatan.component';


@NgModule({
  declarations: [PanjarMainComponent, PanjarComponent, PanjarFormComponent, PanjarRincianComponent, PanjarRincianFormComponent, PanjarRincianKegiatanComponent],
  imports: [
    CoreModule,
    SharedModule,
    PanjarRoutingModule
  ]
})
export class PanjarModule { }
