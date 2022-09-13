import { Component, EventEmitter, Input, OnChanges, OnDestroy, OnInit, Output, SimpleChanges } from '@angular/core';
import { IBpkpajakstr } from 'src/app/core/interface/ibpkpajakstr';

@Component({
  selector: 'app-penyetoran-pajak-rincian',
  templateUrl: './penyetoran-pajak-rincian.component.html',
  styleUrls: ['./penyetoran-pajak-rincian.component.scss']
})
export class PenyetoranPajakRincianComponent implements OnInit, OnChanges, OnDestroy {
  @Input() bpkpajakstrSelected: IBpkpajakstr;
  tabIndex: number = 0;
  @Output() callbackdet = new EventEmitter();
  constructor() { }
  ngOnInit() {
    this.tabIndex = 0;
  }
  ngOnChanges(changes: SimpleChanges): void {
    
  }
  callback(e: any){
    this.callbackdet.emit(e);
  }
  onChangeTab(e: any){
		this.tabIndex = e.index;
	}
  ngOnDestroy(): void {
    this.tabIndex = 0;
  }
}
