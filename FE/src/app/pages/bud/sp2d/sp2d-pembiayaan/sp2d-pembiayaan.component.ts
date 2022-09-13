import { Component, OnDestroy, OnInit } from '@angular/core';
import { Sp2dService } from 'src/app/core/services/sp2d.service';
import { CanActiveGuardService } from 'src/app/core/_commonServices/can-active-guard.service';

@Component({
  selector: 'app-sp2d-pembiayaan',
  templateUrl: './sp2d-pembiayaan.component.html',
  styleUrls: ['./sp2d-pembiayaan.component.scss']
})
export class Sp2dPembiayaanComponent implements OnInit, OnDestroy {
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
