import { Component, OnDestroy, OnInit } from '@angular/core';
import { ITokenClaim } from 'src/app/core/interface/itoken-claim';
import { AuthenticationService } from 'src/app/core/services/auth.service';
import { CanActiveGuardService } from 'src/app/core/_commonServices/can-active-guard.service';

@Component({
  selector: 'app-akun',
  templateUrl: './akun.component.html',
  styleUrls: ['./akun.component.scss']
})
export class AkunComponent implements OnInit, OnDestroy {
  title: string = '';
  tabIndex: number = 0;
  userInfo: ITokenClaim;
  constructor(
    private active: CanActiveGuardService,
    private authService: AuthenticationService
  ) {
    this.active.titleComponent$.subscribe(resp => this.title = resp);
    this.userInfo = this.authService.getTokenInfo();
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
