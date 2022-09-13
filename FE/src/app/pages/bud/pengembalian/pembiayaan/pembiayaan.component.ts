import { Component, OnChanges, OnDestroy, OnInit } from '@angular/core';
import { CanActiveGuardService } from 'src/app/core/_commonServices/can-active-guard.service';

@Component({
  selector: 'app-pembiayaan',
  templateUrl: './pembiayaan.component.html',
  styleUrls: ['./pembiayaan.component.scss']
})
export class PembiayaanComponent implements OnInit, OnDestroy {
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
    this.tabIndex = 0;
  }
}
