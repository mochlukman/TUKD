import { Component, OnDestroy, OnInit } from '@angular/core';
import { TbpService } from 'src/app/core/services/tbp.service';

@Component({
  selector: 'app-main-penetapan',
  templateUrl: './main-penetapan.component.html',
  styleUrls: ['./main-penetapan.component.scss']
})
export class MainPenetapanComponent implements OnInit, OnDestroy {
  tabIndex: number = 0;
  constructor(
    private service: TbpService
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

