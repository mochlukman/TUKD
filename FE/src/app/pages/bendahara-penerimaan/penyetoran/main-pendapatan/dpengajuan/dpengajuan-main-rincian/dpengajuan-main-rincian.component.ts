import { Component, Input, OnChanges, OnDestroy, OnInit, SimpleChanges } from '@angular/core';
import { ISts } from 'src/app/core/interface/ists';
import { ITokenClaim } from 'src/app/core/interface/itoken-claim';
import { AuthenticationService } from 'src/app/core/services/auth.service';

@Component({
  selector: 'app-dpengajuan-main-rincian',
  templateUrl: './dpengajuan-main-rincian.component.html',
  styleUrls: ['./dpengajuan-main-rincian.component.scss']
})
export class DPengajuanMainRincianComponent implements OnInit, OnChanges, OnDestroy {
  @Input() StsSelected : ISts;
  userInfo: ITokenClaim;
  group1: boolean;
  group2: boolean;
  constructor(
    private authService: AuthenticationService
  ) {
    this.group1 = false;
    this.group2 = false;
    this.userInfo = this.authService.getTokenInfo();
  }
  ngOnChanges(changes: SimpleChanges): void {
    if(changes.StsSelected){
      if(['16','60','63'].indexOf(changes.StsSelected.currentValue.kdstatus.trim()) > -1){
        this.group1 = true;
        this.group2 = false;
      }
      if(['61','64'].indexOf(changes.StsSelected.currentValue.kdstatus.trim()) > -1){
        this.group1 = false;
        this.group2 = true;
      }
    }
  }
  ngOnInit() {
  }
  ngOnDestroy(): void {
    this.StsSelected = null;
    this.group1 = true;
    this.group2 = false;
  }
}
