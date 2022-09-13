import { Component, OnDestroy, OnInit } from '@angular/core';
import { WebUserService } from 'src/app/core/services/web-user.service';

@Component({
  selector: 'app-kelola-pengguna',
  templateUrl: './kelola-pengguna.component.html',
  styleUrls: ['./kelola-pengguna.component.scss']
})
export class KelolaPenggunaComponent implements OnInit, OnDestroy {
  tabIndex: number = 0;
  constructor(
    private service: WebUserService
  ) { }

  ngOnInit() {
    this.service.setTabindex(this.tabIndex);
  }
  onChangeTab(e: any){
		this.service.setTabindex(e.index);
	}
  ngOnDestroy():void{
    this.tabIndex = 0;
  }
}
