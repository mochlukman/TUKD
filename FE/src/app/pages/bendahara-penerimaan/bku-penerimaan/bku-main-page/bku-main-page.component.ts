import { Component, OnInit } from '@angular/core';
import { BkuPenerimaanMainPageService } from 'src/app/core/services/bku-penerimaan-main-page.service';

@Component({
  selector: 'app-bku-main-page',
  templateUrl: './bku-main-page.component.html',
  styleUrls: ['./bku-main-page.component.scss']
})
export class BkuMainPageComponent implements OnInit {
  tabIndex: number = 0;
  constructor(
    private service: BkuPenerimaanMainPageService
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
