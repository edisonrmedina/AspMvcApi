import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { envionment } from '../../environments/environment';
import { ResponseApi } from '../interfaces/response-api';
import { Venta } from '../interfaces/venta';
@Injectable({
  providedIn: 'root'
})
export class VentaService {

  private api = envionment.endpoint + 'venta/';

  constructor(private http:HttpClient) { }

  registrar(request:Venta): Observable<ResponseApi>{
    return this.http.post<ResponseApi>(this.api+'registrar',request); 
  }

  historial(buscarPor:string,numeroVenta: string,fechaInicio:string,fechaFin:string): Observable<ResponseApi>{
    return this.http.get<ResponseApi>(`${this.api}historial?buscarPor=${buscarPor}&numeroVenta=${numeroVenta}&fechaInicio=${fechaInicio}&fechaFin=${fechaFin}`); 
  }

  reporte(fechaInicio:string,fechaFin:string): Observable<ResponseApi>{
    return this.http.get<ResponseApi>(`${this.api}reporte?fechaInicio=${fechaInicio}&fechaFin=${fechaFin}`); 
  }

}
