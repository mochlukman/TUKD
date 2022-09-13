import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { IPgrmunit } from '../interface/ipgrmunit';

@Injectable({
  providedIn: 'root'
})
export class PgrmunitService {
  private tabIndex = new BehaviorSubject(<number>0);
  public _tabIndex = this.tabIndex.asObservable();
  private Idpgrmunit: number;
  set _idpgrmunit(e: number){
    this.Idpgrmunit = e;
  }
  private Idunit: number;
  set _idunit(e:number){
    this.Idunit = e;
  }
  private Kdtahap: string;
  set _kdtahap(e: string){
    this.Kdtahap = e;
  }
  private Idpgrm: number;
  set _idpgrm(e: number){
    this.Idpgrm = e;
  }
  private Start: number;
  set _start(e: number){
    this.Start = e;
  }
  private Rows: number;
  set _rows(e: number){
    this.Rows = e;
  }
  private GlobalFilter: string = '';
  set _globalFilter(e: string){
    this.GlobalFilter = e;
  }
  private SortField: string = '';
  set _sortField(e: string){
    this.SortField = e;
  }
  private SortOrder: number;
  set _sortOrder(e: number){
    this.SortOrder = e;
  }
  constructor(private http: HttpClient) { }
  setTabIndex(e: number):void{
    this.tabIndex.next(e);
  }
  get(idpgrmunit: number){
    return this.http.get<IPgrmunit>(`${environment.url}Pgrmunit/${idpgrmunit}`).pipe(map(resp => <IPgrmunit>resp));
  }
  gets(): Observable<IPgrmunit[]>{
    const qp = new HttpParams()
      .set('Idunit', this.Idunit ? this.Idunit.toString() : "0")
      .set('Kdtahap', this.Kdtahap ? this.Kdtahap : "x")
      .set('Idpgrm', this.Idpgrm ? this.Idpgrm.toString() : "0");
    return this.http.get<IPgrmunit[]>(`${environment.url}Pgrmunit`, {params: qp}).pipe(map(resp => <IPgrmunit[]>resp));
  }
  paging(){
    const qp = new HttpParams()
      .set('Start', this.Start.toString())
      .set('Rows', this.Rows.toString())
      .set('GlobalFilter', this.GlobalFilter ? this.GlobalFilter : '')
      .set('SortField', this.SortField ? this.SortField : '')
      .set('SortOrder', this.SortOrder.toString())
      .set('Parameters.Idunit', this.Idunit ? this.Idunit.toString() : "0")
      .set('Parameters.Kdtahap', this.Kdtahap ? this.Kdtahap : "x");
    return this.http.get(`${environment.url}Pgrmunit/paging`, {params: qp}).pipe(map(resp => <any>resp));
  }
  post(postBody: any){
    return this.http.post(`${environment.url}Pgrmunit`, postBody, {observe: 'response'});
  }
  put(postBody: any){
    return this.http.put(`${environment.url}Pgrmunit`, postBody, {observe: 'response'});
  }
  delete(idpgrmunit: number){
    return this.http.request('DELETE', `${environment.url}Pgrmunit/${idpgrmunit}`, {observe: 'response'});
  }
}
