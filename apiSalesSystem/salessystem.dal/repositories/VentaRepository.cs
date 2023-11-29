using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using salesSystem.dal.dbContext;
using salessystem.dal.repositories.contracts;
using salesSystem.model;
using salessystem.dal.repositories;

namespace salesSystem.dal.repositories
{
  public class VentaRepository : GenericRepository<Venta>, IVentaRepository
  {
    private readonly DbventaContext _dbContext;

    public VentaRepository(DbventaContext dbContext) : base(dbContext)
    {
      _dbContext = dbContext;
    }

    /// <summary>
    /// Registra una venta en la base de datos, actualizando el stock de productos vendidos
    /// y generando un número de documento único para la venta.
    /// </summary>
    /// <param name="modeloVenta">La venta que se va a registrar.</param>
    /// <returns>La venta registrada con su número de documento asignado.</returns>
    public async Task<Venta> Registrar(Venta modeloVenta)
    {
      // Crear una instancia para almacenar la venta generada.
      Venta ventaGenerada = new Venta();

      // Crear una transacción para garantizar la atomicidad de la operación.
      using (var transaction = _dbContext.Database.BeginTransaction())
      {
        try
        {
          // Actualizar el stock de los productos vendidos.
          foreach (DetalleVenta dv in modeloVenta.DetalleVenta)
          {
            // Encontrar el producto en la base de datos.
            Producto producto_encontrado = _dbContext.Productos
                .Where(p => p.IdProducto == dv.IdProducto)
                .First();

            // Actualizar el stock del producto.
            producto_encontrado.Stock -= dv.Cantidad;
            _dbContext.Productos.Update(producto_encontrado);
          }

          // Guardar los cambios en la base de datos.
          await _dbContext.SaveChangesAsync();

          // Generar el número de documento único.
          NumeroDocumento correlativo = _dbContext.NumeroDocumentos.First();
          correlativo.UltimoNumero += 1;
          correlativo.FechaRegistro = DateTime.Now;
          _dbContext.Update(correlativo);
          await _dbContext.SaveChangesAsync();

          // Formatear el número de documento en el formato deseado (0001).
          int cantDigito = 4;
          string ceros = string.Concat(Enumerable.Repeat("0", cantDigito));
          string numeroVenta = ceros + correlativo.UltimoNumero.ToString();
          numeroVenta = numeroVenta.Substring(numeroVenta.Length - cantDigito, cantDigito);

          // Asignar el número de documento a la venta.
          modeloVenta.NumeroDocumento = numeroVenta;

          // Agregar la venta a la base de datos.
          await _dbContext.Venta.AddAsync(modeloVenta);
          await _dbContext.SaveChangesAsync();

          // Actualizar la instancia de venta generada.
          ventaGenerada = modeloVenta;

          // Confirmar la transacción.
          transaction.Commit();
        }
        catch (Exception)
        {
          // En caso de error, realizar un rollback de la transacción.
          transaction.Rollback();
          throw;
        }
      }

      // Devolver la venta registrada con su número de documento asignado.
      return ventaGenerada;
    }


  }
}
