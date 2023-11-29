import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { envionment } from '../../environments/environment';
import { ResponseApi } from '../interfaces/response-api';

@Injectable({
  providedIn: 'root'
})
export class DashboardService {
  private api = envionment.endpoint + 'Dashboard/';

  constructor(private http:HttpClient) { }

  resumen(): Observable<ResponseApi>{
    return this.http.get<ResponseApi>(this.api+'Resumen'); 
  }

}
