import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { ISpjtr } from '../interface/ispjtr';

@Injectable({
  providedIn: 'root'
})
export class SpjtrService {
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
  gets(Idunit: number, Idbend: number, Kdstatus?: string) : Observable<ISpjtr[]>{
    const qp = new HttpParams()
    .set('Idunit', Idunit.toString())
    .set('Idbend', Idbend.toString())
    .set('Kdstatus', Kdstatus ? Kdstatus.toString() : '');
    return this.http.get<ISpjtr[]>(`${environment.url}Spjtr`,{params: qp}).pipe(map(resp => <ISpjtr[]>resp));
  }
  getPaging(Idunit: number, Idbend: number, Kdstatus?: string){
    const qp = new HttpParams()
    .set('Start', this.Start.toString())
    .set('Rows', this.Rows.toString())
    .set('GlobalFilter', this.GlobalFilter ? this.GlobalFilter : '')
    .set('SortField', this.SortField ? this.SortField : '')
    .set('SortOrder', this.SortOrder.toString())
    .set('Parameters.Idunit', Idunit.toString())
    .set('Parameters.Kdstatus', Kdstatus ? Kdstatus.toString() : '')
    .set('Parameters.Idbend', Idbend.toString());
    return this.http.get(`${environment.url}Spjtr/paging`,{params: qp}).pipe(map(resp => <any>resp));
  }
  post(paramBody: any){
    return this.http.post(`${environment.url}Spjtr`, paramBody, {observe:'response'});
  }
  put(paramBody: any){
    return this.http.put(`${environment.url}Spjtr`, paramBody, {observe:'response'});
  }
  delete(Idspjtr: number){
    return this.http.request('DELETE', `${environment.url}Spjtr/${Idspjtr}`, {observe: 'response'});
  }
}