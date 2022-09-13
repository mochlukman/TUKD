import { Component, OnInit } from '@angular/core';
import { SpjtrMainPageService } from 'src/app/core/services/spjtr-main-page.service';

@Component({
  selector: 'app-spj-main-page',
  templateUrl: './spj-main-page.component.html',
  styleUrls: ['./spj-main-page.component.scss']
})
export class SpjMainPageComponent implements OnInit {
  tabIndex: number = 0;
  constructor(
    private service: SpjtrMainPageService
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