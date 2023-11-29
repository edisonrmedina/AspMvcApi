using Azure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using salesSystem.api.utilidad;
using salesSystem.bll.Servicios.Contratos;
using salesSystem.dto;


namespace salesSystem.api.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class RolController : ControllerBase
  {
    private readonly IRolService _rolService;

    public RolController(IRolService rolService)
    {
      _rolService = rolService;
    }

    [HttpGet]
    [Route("Lista")]
    public async Task<ActionResult> Lista()
    {
      var rsp = new utilidad.Response<List<RolDTO>>();

      try
      {
        rsp.status = true;
        rsp.Value = await _rolService.lista();

      }catch (Exception ex)
      {
        rsp.status=false;
        rsp.msg = ex.Message;
      }
      return Ok(rsp);
    }

  }
}
