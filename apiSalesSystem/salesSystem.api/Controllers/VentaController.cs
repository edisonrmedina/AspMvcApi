using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using salesSystem.bll.Servicios;
using salesSystem.bll.Servicios.Contratos;
using salesSystem.dto;
using salesSystem.model;

namespace salesSystem.api.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class VentaController : ControllerBase
  {
    private readonly IVentaService _ventaService;

    public VentaController(IVentaService ventaService)
    {
      _ventaService = ventaService;
    }

    [HttpPost]
    [Route("Registar")]
    public async Task<ActionResult> Registrar([FromBody] VentaDTO venta)
    {
      var rsp = new utilidad.Response<VentaDTO>();

      try
      {
        rsp.status = true;
        rsp.Value = await _ventaService.Registrar(venta);
      }
      catch (Exception ex)
      {
        rsp.status = false;
        rsp.msg = ex.Message;
      }

      return Ok(rsp);
    }

    [HttpGet]
    [Route("Historial")]
    public async Task<ActionResult> Historial(string buscarPor, string? numVenta, string? fechaInicio, string? fechaFin)
    {
      var rsp = new utilidad.Response<List<VentaDTO>>();

      numVenta = numVenta is null ? "" : numVenta;
      fechaInicio = fechaInicio is null ? "" : fechaInicio;
      fechaFin = fechaFin is null ? "" : fechaFin;

      try
      {
        rsp.status = true;
        rsp.Value = await _ventaService.Historical(buscarPor, numVenta,fechaInicio, fechaFin);
      }
      catch (Exception ex)
      {
        rsp.status = false;
        rsp.msg = ex.Message;
      }

      return Ok(rsp);
    }

    [HttpGet]
    [Route("Reporte")]
    public async Task<ActionResult> Reporte(string fechaInicio, string fechaFin)
    {
      var rsp = new utilidad.Response<List<ReporteDTO>>();

      fechaInicio = fechaInicio is null ? "" : fechaInicio;
      fechaFin = fechaFin is null ? "" : fechaFin;

      try
      {
        rsp.status = true;
        rsp.Value = await _ventaService.Reporte(fechaInicio, fechaFin);
      }
      catch (Exception ex)
      {
        rsp.status = false;
        rsp.msg = ex.Message;
      }

      return Ok(rsp);
    }

  }
}
