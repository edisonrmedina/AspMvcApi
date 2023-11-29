using AutoMapper;
using salessystem.dal.repositories.contracts;
using salesSystem.bll.Servicios.Contratos;
using salesSystem.dto;
using salesSystem.model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace salesSystem.bll.Servicios
{
  public class DashboardService:IDashboard
  {
    private IVentaRepository _ventaRepository;
    private readonly IgenericRepository<Producto> _productoRepositorio;
    private readonly IMapper _mapper;

    public DashboardService(IVentaRepository ventaRepository, IgenericRepository<Producto> productoRepositorio, IMapper mapper)
    {
      _ventaRepository = ventaRepository;
      _productoRepositorio = productoRepositorio;
      _mapper = mapper;
    }

    //Metodos para resumen

    private IQueryable<Venta> retornarVentas(IQueryable<Venta> tablaventas,int restarCantidadDias)
    {
      //de hoy hace n dias atras

      DateTime? ultimaFecha = tablaventas.OrderByDescending(v => v.FechaRegistro).Select(v => v.FechaRegistro).First();
      ultimaFecha = ultimaFecha.Value.AddDays(restarCantidadDias);

      return tablaventas.Where
        (
          v => v.FechaRegistro.Value >= ultimaFecha.Value.Date
        );

    }

    private async Task<int> totalVentasUltimaSemana()
    {
      int total = 0;
      IQueryable<Venta> _ventaQuery = await _ventaRepository.consultar();

      if(_ventaQuery.Count() > 0)
      {
        var tablaVenta = retornarVentas(_ventaQuery, 7);
        total = tablaVenta.Count();
      }

      return total;
    }

    private async Task<string> totalIngresoUltimaSemana()
    {
      decimal total = 0;
      IQueryable<Venta> _ventaQuery = await _ventaRepository.consultar();

      if (_ventaQuery.Count() > 0)
      {
        var tablaVenta = retornarVentas( _ventaQuery, 7);
        total = tablaVenta.Select(v => v.Total).Sum(v => v.Value);

        
      }

      return Convert.ToString(total, new CultureInfo("es-Ec"));

    }

    private async Task<int> totalProductos()
    {
      IQueryable<Producto> _productoQuery = await _productoRepositorio.consultar();
      int total = _productoQuery.Count();
      return total;
    }

    private async Task<Dictionary<string,int>> ventasUltimaSemana()
    {
      Dictionary<string, int> result = new Dictionary<string, int>();
      IQueryable<Venta> _ventaQuery = await _ventaRepository.consultar();
      if (_ventaQuery.Count() > 0)
      {
          var tablaVenta = retornarVentas(_ventaQuery, 7);
          result = tablaVenta.GroupBy(v => v.FechaRegistro.Value.Date).OrderBy(g => g.Key)
            .Select(dv => new {fecha = dv.Key.ToString("dd/mm/yyyy"),total = dv.Count() })
            .ToDictionary(keySelector: r => r.fecha,elementSelector:r => r.total );
      }
      return result;
    }

    public async Task<DashBoardDTO> Resumen()
    {
      DashBoardDTO dashBoard = new DashBoardDTO();
      try
      {
        dashBoard.TotalVentas = await totalVentasUltimaSemana();
        dashBoard.TotalIngresos = await totalIngresoUltimaSemana();
        dashBoard.TotalProductos = await totalProductos();

        List<VentaSemanaDTO> listaVentas = new List<VentaSemanaDTO>();

        foreach (KeyValuePair<string, int> item in await ventasUltimaSemana())
          {
          listaVentas.Add(new VentaSemanaDTO()
          {
            Fecha = item.Key,
            total = item.Value
          });

          }
          dashBoard.ventaUltimaSemana = listaVentas;
      } catch (Exception ex)
      {
        throw;
      }

      return dashBoard;
    }
  }
}
