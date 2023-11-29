using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using salesSystem.api.utilidad;
using salesSystem.bll.Servicios;
using salesSystem.bll.Servicios.Contratos;
using salesSystem.dto;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace salesSystem.api.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class DashBoardController : ControllerBase
  {
    private readonly IDashboard _dashboardService;

    public DashBoardController(IDashboard dashboardService)
    {
      _dashboardService = dashboardService;
    }

    [HttpGet]
    [Route("Resumen")]
    public async Task<ActionResult> Resumen()
    {
      var rsp = new utilidad.Response<DashBoardDTO>();

      try
      {
        rsp.status = true;
        rsp.Value = await _dashboardService.Resumen();

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
