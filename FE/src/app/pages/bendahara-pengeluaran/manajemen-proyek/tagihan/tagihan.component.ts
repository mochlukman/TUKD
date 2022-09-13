import { Component, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { TagihanService } from 'src/app/core/services/tagihan.service';
import { CanActiveGuardService } from 'src/app/core/_commonServices/can-active-guard.service';

@Component({
  selector: 'app-tagihan',
  templateUrl: './tagihan.component.html',
  styleUrls: ['./tagihan.component.scss']
})
export class TagihanComponent implements OnInit, OnDestroy {
  title: string = '';
  tabIndex: number = 0;
  constructor(
    private service: TagihanService,
    private active: CanActiveGuardService
  ) {
    this.active.titleComponent$.subscribe(resp => this.title = resp);    
  }
  ngOnInit() {
    this.service.setTabIndex(this.tabIndex);
  }
  onChangeTab(e: any){
		this.service.setTabIndex(e.index);
    this.service.setTagihanSelected(null);
	}
  ngOnDestroy(): void{
    this.tabIndex = 0;
    this.service.setTabIndex(this.tabIndex);
    this.service.setTagihanSelected(null);
  }
}