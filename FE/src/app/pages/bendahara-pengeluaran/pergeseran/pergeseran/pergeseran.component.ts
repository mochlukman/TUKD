import { Component, OnDestroy, OnInit } from '@angular/core';
import { BkbankService } from 'src/app/core/services/bkbank.service';
import { CanActiveGuardService } from 'src/app/core/_commonServices/can-active-guard.service';

@Component({
  selector: 'app-pergeseran',
  templateUrl: './pergeseran.component.html',
  styleUrls: ['./pergeseran.component.scss']
})
export class PergeseranComponent implements OnInit, OnDestroy {
  title: string = '';
  tabIndex: number = 0;
  constructor(
    private active: CanActiveGuardService
  ) {
    this.active.titleComponent$.subscribe(resp => this.title = resp);
  }
  ngOnInit() {
    this.tabIndex = 0;
  }
  onChangeTab(e: any){
		this.tabIndex = e.index;
	}
  ngOnDestroy(): void{
    this.tabIndex = 0;
  }
}
