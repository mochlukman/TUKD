import { Component, OnInit } from '@angular/core';
import { KinkegService } from 'src/app/core/services/kinkeg.service';
import { CanActiveGuardService } from 'src/app/core/_commonServices/can-active-guard.service';

@Component({
  selector: 'app-kinerja',
  templateUrl: './kinerja.component.html',
  styleUrls: ['./kinerja.component.scss']
})
export class KinerjaComponent implements OnInit {
  tabIndex: number = 0;
  title: string = '';
  constructor(
    private service: KinkegService,
    private canActiveGuard: CanActiveGuardService
    ) {
      this.canActiveGuard.titleComponent$.subscribe(resp => this.title = resp);
  }
  ngOnInit() {
    this.service.setTabIndex(this.tabIndex);
  }
  onChangeTab(e: any){
		this.service.setTabIndex(e.index);
	}
  ngOnDestroy(): void{
    this.tabIndex = 0;
    this.service.setTabIndex(this.tabIndex);
  }
}
