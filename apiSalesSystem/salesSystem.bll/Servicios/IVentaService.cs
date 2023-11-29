using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using salesSystem.dto;

namespace salesSystem.bll.Servicios
{
  public interface IVentaService
  {
    Task<VentaDTO> Registrar(VentaDTO ventaModel);
    Task<List<VentaDTO>> Historical(string buscarPor , string numVenta , string fechaInicio, string fechaFin);
    Task<List<ReporteDTO>> Reporte(string fechaInicio , string fechaFin);

  }
}
