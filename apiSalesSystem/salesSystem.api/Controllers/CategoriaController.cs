using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using salesSystem.api.utilidad;
using salesSystem.bll.Servicios;
using salesSystem.bll.Servicios.Contratos;
using salesSystem.dto;

namespace salesSystem.api.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class CategoriaController : ControllerBase
  {
    private readonly ICategoriaService _categoriaService;

    public CategoriaController(ICategoriaService categoriaService)
    {
      _categoriaService = categoriaService;
    }

    [HttpGet]
    [Route("lista")]
    public async Task<ActionResult> Lista()
    {
      var rsp = new utilidad.Response<List<CategoriaDTO>>();

      try
      {
        rsp.status = true;
        rsp.Value = await _categoriaService.lista();

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
