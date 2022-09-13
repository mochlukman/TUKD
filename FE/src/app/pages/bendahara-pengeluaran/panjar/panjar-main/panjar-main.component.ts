import { Component, OnDestroy, OnInit } from '@angular/core';
import { PanjarService } from 'src/app/core/services/panjar.service';
import { CanActiveGuardService } from 'src/app/core/_commonServices/can-active-guard.service';

@Component({
  selector: 'app-panjar-main',
  templateUrl: './panjar-main.component.html',
  styleUrls: ['./panjar-main.component.scss']
})
export class PanjarMainComponent implements OnInit, OnDestroy {
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