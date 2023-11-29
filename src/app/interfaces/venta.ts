import { DetalleVenta } from "./detalle-venta";

export interface Venta {
    idVenta:number,
    numeroDocumento:number,
    tipoPago:string,
    fechaRegistro:string,
    totalTexto:string,
    detalleVenta:DetalleVenta[]
}
