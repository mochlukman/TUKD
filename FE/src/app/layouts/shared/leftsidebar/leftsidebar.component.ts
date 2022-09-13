import { Component, OnInit, Input } from '@angular/core';
import { Router } from '@angular/router';

import { AuthenticationService } from '../../../core/services/auth.service';
import { SIDEBAR_WIDTH_CONDENSED } from '../../layout.model';
import { ITokenClaim } from 'src/app/core/interface/itoken-claim';
import Swal from 'sweetalert2';
@Component({
  selector: 'app-leftsidebar',
  templateUrl: './leftsidebar.component.html',
  styleUrls: ['./leftsidebar.component.scss'],

})
export class LeftsidebarComponent implements OnInit {

  @Input() sidebarType: string;
  userInfo: ITokenClaim;
  constructor(
    private router: Router, 
    private authenticationService: AuthenticationService) {
      this.userInfo = this.authenticationService.getTokenInfo();
  }

  ngOnInit() {
  }

  /**
   * Is sidebar condensed?
   */
  isSidebarCondensed() {
    return this.sidebarType === SIDEBAR_WIDTH_CONDENSED;
  }

  /**
   * Logout the user
   */
  logout() {
    Swal({
			title: '',
			text: 'Keluar Aplikasi ?',
			type: 'warning',
			showCancelButton: true,
			heightAuto: false,
			confirmButtonText: 'Ya',
			cancelButtonText: 'Batal'
		}).then((result) => {
			if (result.value) {
				this.authenticationService.logout();
        this.router.navigate(['/account/login'], { queryParams: { returnUrl: '/' } });
			} else if (result.dismiss === Swal.DismissReason.cancel) {
			}
		});
  }
}
