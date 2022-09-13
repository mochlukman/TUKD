import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { IKontrak } from '../interface/ikontrak';

@Injectable({
  providedIn: 'root'
})
export class KontrakService {
  private kontrakSelected = new BehaviorSubject(<IKontrak>null);
  public _kontrakSelected = this.kontrakSelected.asObservable();
  constructor(private http: HttpClient) { }
  setKontrakSelected(data: IKontrak):void{
    this.kontrakSelected.next(data);
  }
  gets(Idunit: number, Idkeg?:number): Observable<IKontrak[]>{
    const param = new HttpParams()
      .set('Idunit', Idunit.toString())
      .set('Idkeg', Idkeg ? Idkeg.toString() : '0');
    return this.http.get<IKontrak[]>(`${environment.url}Kontrak`, {params: param})
      .pipe(map(resp => <IKontrak[]>resp));
  }
  get(Idkontrak: number){
    return this.http.get<IKontrak>(`${environment.url}kontrak/${Idkontrak}`).pipe(map(resp => <IKontrak>resp));
  }
  post(paramBody: any){
    return this.http.post(`${environment.url}kontrak`, paramBody, {observe: 'response'});
  }
  put(paramBody: any){
    return this.http.put(`${environment.url}kontrak`, paramBody, {observe: 'response'});
  }
  delete(Idkontrak: number){
    return this.http.request('DELETE', `${environment.url}Kontrak/${Idkontrak}`,{observe: 'response'});
  }
}
