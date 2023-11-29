import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { envionment } from '../../environments/environment';
import { Login } from '../interfaces/login';
import { ResponseApi } from '../interfaces/response-api';
import { Usuario } from '../interfaces/usuario';

@Injectable({
  providedIn: 'root'
})
export class UsuarioService {
  private api = envionment.endpoint + 'usuario/';

  constructor(private http:HttpClient) { }

  lista(): Observable<ResponseApi>{
    return this.http.get<ResponseApi>(this.api+'lista'); 
  }
  iniciarSesion(request: Login): Observable<ResponseApi> {
    return this.http.post<ResponseApi>(this.api+'iniciarSesion', request); 
  }
  
  guardar(request:Usuario): Observable<ResponseApi>{
    return this.http.post<ResponseApi>(this.api+'guardar',request); 
  }

  editar(request:Usuario): Observable<ResponseApi>{
    return this.http.put<ResponseApi>(this.api+'editar',request); 
  }

  eliminar(id:number): Observable<ResponseApi>{
    return this.http.delete<ResponseApi>(this.api+'Eliminar/'+id); 
  }

}
