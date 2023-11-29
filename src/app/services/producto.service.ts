import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { envionment } from '../../environments/environment';
import { Producto } from '../interfaces/producto';
import { ResponseApi } from '../interfaces/response-api';
@Injectable({
  providedIn: 'root'
})
export class ProductoService {
  private api = envionment.endpoint + 'producto/';

  constructor(private http:HttpClient) { }
  lista(): Observable<ResponseApi>{
    return this.http.get<ResponseApi>(this.api+'lista'); 
  }
  
  guardar(request:Producto): Observable<ResponseApi>{
    return this.http.post<ResponseApi>(this.api+'guardar',request); 
  }

  editar(request:Producto): Observable<ResponseApi>{
    return this.http.put<ResponseApi>(this.api+'editar',request); 
  }

  eliminar(id:number): Observable<ResponseApi>{
    return this.http.delete<ResponseApi>(this.api+'Eliminar/'+id); 
  }

}
