import { Component, OnDestroy, OnInit } from '@angular/core';
import { CanActiveGuardService } from 'src/app/core/_commonServices/can-active-guard.service';

@Component({
  selector: 'app-spp-gu',
  templateUrl: './spp-gu.component.html',
  styleUrls: ['./spp-gu.component.scss']
})
export class SppGuComponent implements OnInit, OnDestroy {
  title: string = '';
  tabIndex: number = 0;
  constructor(
    private active : CanActiveGuardService
  ) {
    this.active.titleComponent$.subscribe(resp => this.title = resp);
    this.tabIndex = 0;
  }
  ngOnInit() {
  }
  onChangeTab(e: any){
    this.tabIndex = e.index;
	}
  ngOnDestroy(): void{
    this.tabIndex = 0;
  }
}
