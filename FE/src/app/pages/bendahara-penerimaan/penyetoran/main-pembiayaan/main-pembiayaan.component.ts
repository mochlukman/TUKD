import { Component, OnDestroy, OnInit } from '@angular/core';
import { StsService } from 'src/app/core/services/sts.service';

@Component({
  selector: 'app-main-pembiayaan',
  templateUrl: './main-pembiayaan.component.html',
  styleUrls: ['./main-pembiayaan.component.scss']
})
export class MainPembiayaanComponent implements OnInit, OnDestroy {
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

