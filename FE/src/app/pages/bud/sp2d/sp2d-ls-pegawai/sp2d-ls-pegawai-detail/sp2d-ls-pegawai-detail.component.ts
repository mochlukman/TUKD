import { Component, Input, OnChanges, OnDestroy, OnInit, SimpleChanges } from '@angular/core';
import { ILookupTree } from 'src/app/core/interface/iglobal';
import { ISp2d } from 'src/app/core/interface/isp2d';
import { ITokenClaim } from 'src/app/core/interface/itoken-claim';
import { AuthenticationService } from 'src/app/core/services/auth.service';
import { SppdetrService } from 'src/app/core/services/sppdetr.service';

@Component({
  selector: 'app-sp2d-ls-pegawai-detail',
  templateUrl: './sp2d-ls-pegawai-detail.component.html',
  styleUrls: ['./sp2d-ls-pegawai-detail.component.scss']
})
export class Sp2dLsPegawaiDetailComponent implements OnInit, OnDestroy, OnChanges {
  tabIndex: number = 0;
  @Input() Sp2dSelected : ISp2d;
  @Input() KegSelected : ILookupTree;
  userInfo: ITokenClaim
  constructor(
    private service: SppdetrService,
    private authService: AuthenticationService
  ) {
    this.userInfo = this.authService.getTokenInfo();
  }
  ngOnChanges(changes: SimpleChanges): void {

  }
  ngOnInit() {
    this.tabIndex = 0;
  }
  onChangeTab(e: any){
    this.tabIndex = e.index;
	}
  ngOnDestroy(): void {
    this.tabIndex = 0;
    this.Sp2dSelected = null;
    this.KegSelected = null;
  }
}