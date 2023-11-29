using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using salesSystem.bll.Servicios.Contratos;
using salesSystem.dto;

namespace salesSystem.api.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class MenuController : ControllerBase
  {
    private readonly IMenuService _menuService;

    public MenuController(IMenuService menuService)
    {
      _menuService = menuService;
    }

    [HttpGet]
    [Route("lista")]
    public async Task<ActionResult> Lista(int idUsuario)
    {
      var rsp = new utilidad.Response<List<MenuDTO>>();

      try
      {
        rsp.status = true;
        rsp.Value = await _menuService.lista(idUsuario);

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
