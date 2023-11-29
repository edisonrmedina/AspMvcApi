using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using salesSystem.bll.Servicios.Contratos;
using salessystem.dal.repositories.contracts;
using salesSystem.dto;
using salesSystem.model;

namespace salesSystem.bll.Servicios
{
  public class ProductService : iProductService
  {
    private readonly IgenericRepository<Producto> _productoRepositorio;
    private readonly IMapper _mapper;

    public ProductService(IgenericRepository<Producto> productoRepositorio, IMapper mapper)
    {
      _productoRepositorio = productoRepositorio;
      _mapper = mapper;
    }

    public async Task<List<ProductoDTO>> Listar()
    {
      try
      {
        var queryProductos = await _productoRepositorio.consultar();
        var listaProductos = queryProductos.ToList();

        return _mapper.Map<List<ProductoDTO>>(listaProductos);
      }
      catch (Exception ex)
      {
        throw;
      }
    }

    public async Task<ProductoDTO> Crear(ProductoDTO productoModel)
    {
      try
      {
        var productoCreado = await _productoRepositorio.crear(_mapper.Map<Producto>(productoModel));

        if (productoCreado.IdProducto == 0)
          throw new TaskCanceledException("Producto no se creó");

        var query = await _productoRepositorio.consultar(p => p.IdProducto == productoCreado.IdProducto);
        productoCreado = query.First();

        return _mapper.Map<ProductoDTO>(productoCreado);
      }
      catch (Exception ex)
      {
        throw;
      }
    }

    public async Task<bool> Editar(ProductoDTO productoModel)
    {
      try
      {
        var product = _mapper.Map<Producto>(productoModel);
        var productoExistente = await _productoRepositorio.obtener(p => p.IdProducto == productoModel.IdProducto);

        if (productoExistente == null)
        {
          throw new TaskCanceledException("Producto no encontrado");
        }

        // Actualizar propiedades del productoExistente con los valores proporcionados en productoModel
        // Asegúrate de manejar todas las propiedades que deseas actualizar
        productoExistente.Nombre = product.Nombre;
        productoExistente.IdCategoria = product.IdCategoria;
        productoExistente.Stock = product.Stock;
        productoExistente.Precio = product.Precio;
        productoExistente.EsActivo = product.EsActivo;

        var productoActualizado = await _productoRepositorio.editar(productoExistente);

        if (!productoActualizado)
          throw new TaskCanceledException("No se pudo editar");

        // productoActualizado es un booleano que indica si la edición fue exitosa
        return productoActualizado;
      }
      catch (Exception ex)
      {
        throw;
      }
    }

    public async Task<bool> Eliminar(int id)
    {
      try
      {
        var productoExistente = await _productoRepositorio.obtener(p => p.IdProducto == id);

        if (productoExistente == null)
        {
          throw new TaskCanceledException("Producto no encontrado");
        }

        var eliminado = await _productoRepositorio.eliminar(productoExistente);

        if (!eliminado)
          throw new TaskCanceledException("No se pudo eliminar");

        // eliminado es un booleano que indica si la eliminación fue exitosa
        return eliminado;
      }
      catch (Exception ex)
      {
        throw;
      }
    }
  }
}
