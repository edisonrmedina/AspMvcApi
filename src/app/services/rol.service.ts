import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { envionment } from '../../environments/environment';
import { ResponseApi } from '../interfaces/response-api';

@Injectable({
  providedIn: 'root'
})
export class RolService {
  private api = envionment.endpoint + 'rol/';

  constructor(private http:HttpClient) { }

  lista(): Observable<ResponseApi>{
    return this.http.get<ResponseApi>(this.api+'lista'); 
  }
}
