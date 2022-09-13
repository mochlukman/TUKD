import { Component, OnInit } from '@angular/core';
import { RkaMainService } from 'src/app/core/services/rka-main.service';
import { CanActiveGuardService } from 'src/app/core/_commonServices/can-active-guard.service';

@Component({
  selector: 'app-main-pagex',
  templateUrl: './main-pagex.component.html',
  styleUrls: ['./main-pagex.component.scss']
})
export class MainPagexComponent implements OnInit {
  tabIndex: number = 0;
  title: string = '';
  constructor(
    private service: RkaMainService,
    private canActive: CanActiveGuardService
  ) {
    this.canActive.titleComponent$.subscribe(resp => this.title = resp);
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