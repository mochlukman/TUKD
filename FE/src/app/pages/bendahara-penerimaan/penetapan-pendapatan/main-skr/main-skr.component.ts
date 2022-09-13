import { Component, OnDestroy, OnInit } from '@angular/core';
import { SkpService } from 'src/app/core/services/skp.service';

@Component({
  selector: 'app-main-skr',
  templateUrl: './main-skr.component.html',
  styleUrls: ['./main-skr.component.scss']
})
export class MainSkrComponent implements OnInit, OnDestroy {
  tabIndex: number = 0;
  constructor(
    private service: SkpService
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
