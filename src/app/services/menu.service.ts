import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { envionment } from '../../environments/environment';
import { ResponseApi } from '../interfaces/response-api';

@Injectable({
  providedIn: 'root'
})
export class MenuService {

  private api = envionment.endpoint + 'menu/';

  constructor(private http:HttpClient) { }

  lista(idUsuario:number): Observable<ResponseApi>{
    return this.http.get<ResponseApi>(this.api+'lista?idUsuario='+idUsuario); 
  }

}
