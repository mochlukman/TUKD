import { Component, OnDestroy, OnInit } from '@angular/core';
import { StsService } from 'src/app/core/services/sts.service';

@Component({
  selector: 'app-main-sts',
  templateUrl: './main-sts.component.html',
  styleUrls: ['./main-sts.component.scss']
})
export class MainStsComponent implements OnInit, OnDestroy {
  tabIndex: number = 0;
  constructor(
    private service: StsService
  ) {
    
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
