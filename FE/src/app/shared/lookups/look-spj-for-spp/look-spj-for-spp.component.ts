import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { Message } from 'primeng/api';
import { ISpjLookupForSpp } from 'src/app/core/interface/ispj';
import { SpjService } from 'src/app/core/services/spj.service';

@Component({
  selector: 'app-look-spj-for-spp',
  templateUrl: './look-spj-for-spp.component.html',
  styleUrls: ['./look-spj-for-spp.component.scss']
})
export class LookSpjForSppComponent implements OnInit {
  showThis: boolean;
  title: string;
  msgs: Message[];
  listdata: ISpjLookupForSpp[];
  loading: boolean;
	@Output() callBack = new EventEmitter();
  constructor(
    private service: SpjService
  ) {
  }

  ngOnInit() {
  }
  get(Idunit: number, Idspp: number, Kdstatus: string) {
    this.listdata = [];
    this.msgs = [];
		this.loading = true;
		this.service.getLookupForSpp(Idunit, Idspp, Kdstatus).subscribe((resp) => {
			if(resp.length > 0){
				this.listdata = resp;
			} else {
				this.msgs.push({ severity: 'info', summary: 'Info', detail: 'Data Tidak Tersedia' });
			}
			this.loading = false;
		},(error) => {
			this.loading = false;
			if(Array.isArray(error.error.error)){
				for(var i = 0; i < error.error.error; i++){
					this.msgs.push({severity: 'error', summary: 'Error', detail: error.error.error[i]});
				}
			} else {
				this.msgs.push({severity: 'error', summary: 'Error', detail: error.error});
			}
		});
	}
  pilih(e: ISpjLookupForSpp) {
    this.callBack.emit(e);
    this.onHide();
	}
  onHide(){
    this.msgs = [];
		this.showThis = false;
  }
}