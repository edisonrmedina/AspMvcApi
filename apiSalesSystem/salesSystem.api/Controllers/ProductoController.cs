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
  public class ProductoController : ControllerBase
  {
    private readonly iProductService _productService;

    public ProductoController(iProductService productService)
    {
      _productService = productService;
    }

    [HttpGet]
    [Route("Lista")]
    public async Task<ActionResult> Lista()
    {
      var rsp = new utilidad.Response<List<ProductoDTO>>();

      try
      {
        rsp.status = true;
        rsp.Value = await _productService.Listar();
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
    public async Task<ActionResult> Guardar([FromBody] ProductoDTO producto)
    {
      var rsp = new utilidad.Response<ProductoDTO>();

      try
      {
        rsp.status = true;
        rsp.Value = await _productService.Crear(producto);
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
    public async Task<ActionResult> Editar([FromBody] ProductoDTO producto)
    {
      var rsp = new utilidad.Response<bool>();

      try
      {
        rsp.status = true;
        rsp.Value = await _productService.Editar(producto);
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
        rsp.Value = await _productService.Eliminar(id);
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
