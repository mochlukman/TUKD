import { Component, OnDestroy, OnInit } from '@angular/core';
import { SppService } from 'src/app/core/services/spp.service';
import { CanActiveGuardService } from 'src/app/core/_commonServices/can-active-guard.service';

@Component({
  selector: 'app-spp-ls-uangmuka',
  templateUrl: './spp-ls-uangmuka.component.html',
  styleUrls: ['./spp-ls-uangmuka.component.scss']
})
export class SppLsUangmukaComponent implements OnInit, OnDestroy {
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
