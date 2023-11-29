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
  public class UsuarioController : ControllerBase
  {
    private readonly IUsuarioService _usuarioServicio;

    public UsuarioController(IUsuarioService usuarioServicio)
    {
      _usuarioServicio = usuarioServicio;
    }

    [HttpGet]
    [Route("lista")]
    public async Task<ActionResult> Lista()
    {
      var rsp = new utilidad.Response<List<UsuarioDTO>>();

      try
      {
        rsp.status = true;
        rsp.Value = await _usuarioServicio.lista();

      }
      catch (Exception ex)
      {
        rsp.status = false;
        rsp.msg = ex.Message;
      }
      return Ok(rsp);
    }

    [HttpPost]
    [Route("iniciarSesion")]
    public async Task<ActionResult> iniciarSesion([FromBody] LoginDTO login)
    {

      var rsp = new utilidad.Response<SesionDTO>();

      try
      {
        rsp.status = true;
        rsp.Value = await _usuarioServicio.validarCredenciales(login.Correo,login.Clave);

      }
      catch (Exception ex)
      {
        rsp.status = false;
        rsp.msg = ex.Message;
      }
      return Ok(rsp);

    }

    [HttpPost]
    [Route("Guardar")]
    public async Task<ActionResult> Guardar([FromBody] UsuarioDTO usuario)
    {

      var rsp = new utilidad.Response<UsuarioDTO>();

      try
      {
        rsp.status = true;
        rsp.Value = await _usuarioServicio.crear(usuario);

      }
      catch (Exception ex)
      {
        rsp.status = false;
        rsp.msg = ex.Message;
      }
      return Ok(rsp);

    }

    [HttpPut]
    [Route("Editar")]
    public async Task<ActionResult> Editar([FromBody] UsuarioDTO usuario)
    {

      var rsp = new utilidad.Response<bool>();

      try
      {
        rsp.status = true;
        rsp.Value = await _usuarioServicio.Editar(usuario);

      }
      catch (Exception ex)
      {
        rsp.status = false;
        rsp.msg = ex.Message;
      }
      return Ok(rsp);

    }

    [HttpDelete]
    [Route("Eliminar/{id:int}")]
    public async Task<ActionResult> Eliminar(int id)
    {

      var rsp = new utilidad.Response<bool>();

      try
      {
        rsp.status = true;
        rsp.Value = await _usuarioServicio.Eliminar(id);

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
