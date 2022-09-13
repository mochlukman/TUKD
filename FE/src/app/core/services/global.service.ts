import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
@Injectable({
  providedIn: 'root'
})
export class GlobalService {

  constructor(private http: HttpClient) { }
  backupDatabase(){
    return this.http.get(`${environment.url}BackupDatabase`,{observe: 'response', responseType: 'blob' as 'json' });
  }
}
