import { Component, Input, OnChanges, OnDestroy, OnInit, SimpleChanges } from '@angular/core';
import { ITbp } from 'src/app/core/interface/itbp';
import { ITokenClaim } from 'src/app/core/interface/itoken-claim';
import { AuthenticationService } from 'src/app/core/services/auth.service';
import { TbpdettService } from 'src/app/core/services/tbpdett.service';

@Component({
  selector: 'app-pelbank-bp-to-bpp-detail',
  templateUrl: './pelbank-bp-to-bpp-detail.component.html',
  styleUrls: ['./pelbank-bp-to-bpp-detail.component.scss']
})
export class PelbankBpToBppDetailComponent implements OnInit, OnDestroy, OnChanges {
  tabIndex: number = 0;
  @Input() TbpSelected : ITbp;
  userInfo: ITokenClaim
  constructor(
    private service: TbpdettService,
    private authService: AuthenticationService
  ) {
    this.userInfo = this.authService.getTokenInfo();
  }
  ngOnChanges(changes: SimpleChanges): void {

  }
  ngOnInit() {
  }
  onChangeTab(e: any){
		this.service.setTabIndex(e.index);
	}
  ngOnDestroy(): void {
    this.tabIndex = 0;
    this.service.setTabIndex(0);
    this.TbpSelected = null;
  }
}
