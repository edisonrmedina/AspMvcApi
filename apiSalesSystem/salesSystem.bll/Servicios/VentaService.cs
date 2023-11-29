using AutoMapper;
using salessystem.dal.repositories.contracts;
using salesSystem.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using salesSystem.bll.Servicios.Contratos;
using salessystem.dal.repositories.contracts;
using salesSystem.dto;
using System.Globalization;
using Microsoft.EntityFrameworkCore;

namespace salesSystem.bll.Servicios
{
  public class VentaService :IVentaService
  {
    private IVentaRepository _ventaRepository;
    private readonly IgenericRepository<DetalleVenta> _DetalleVentaRepositorio;
    private readonly IMapper _mapper;

    public VentaService(IVentaRepository ventaRepository, IgenericRepository<DetalleVenta> _DetalleVentaRepositorio, IMapper mapper)
    {
      _ventaRepository = ventaRepository;
      _DetalleVentaRepositorio = _DetalleVentaRepositorio;
      _mapper = mapper;
    }

    public async Task<VentaDTO> Registrar(VentaDTO ventaModel)
    {
      try
      {
        // Implementa la lógica para obtener el historial de ventas
        // ...
        var ventaGenerada = await _ventaRepository.Registrar(_mapper.Map<Venta>(ventaModel));

        if (ventaGenerada.IdVenta == 0)
          throw new TaskCanceledException("No se pudo crear");


        // Puedes devolver la lista de ventas históricas o lo que consideres apropiado
        return _mapper.Map<VentaDTO>(ventaGenerada);
      }
      catch (Exception ex)
      {
        // Maneja las excepciones según tus necesidades
        throw;
      }
    }

    public async Task<List<VentaDTO>> Historical(string buscarPor, string numVenta, string fechaInicio, string fechaFin)
    {
      IQueryable<Venta> query = await _ventaRepository.consultar();
      var listaVentas = new List<Venta>();

      try
      {

        if(buscarPor == "fecha")
        {
          DateTime fecha_inicio = DateTime.ParseExact(fechaInicio, "dd/MM/yyyy", new CultureInfo("es-EC"));
          DateTime fecha_fin = DateTime.ParseExact(fechaFin, "dd/MM/yyyy", new CultureInfo("es-EC"));

          listaVentas = await query
                .Where(v => v.NumeroDocumento == numVenta)
                .Include(dv => dv.DetalleVenta)
                .ThenInclude(p => p.IdProductoNavigation)
                .ToListAsync();

        }
        else
        {

        }

      }catch(Exception ex)
      {
        throw;
      }

      List<VentaDTO> listaVentasDTO = _mapper.Map<List<VentaDTO>>(listaVentas);
      return listaVentasDTO;

    }

    public async Task<List<ReporteDTO>> Reporte(string fechaInicio, string fechaFin)
    {
      IQueryable<DetalleVenta> query = await _DetalleVentaRepositorio.consultar();
      var listaVentas = new List<DetalleVenta>();

      try
      {
        DateTime fecha_inicio = DateTime.ParseExact(fechaInicio, "dd/MM/yyyy", new CultureInfo("es-EC"));
        DateTime fecha_fin = DateTime.ParseExact(fechaFin, "dd/MM/yyyy", new CultureInfo("es-EC"));

        listaVentas = await query
          .Include(p => p.IdProductoNavigation)
          .Include(v => v.IdVentaNavigation)
          .Where(dv =>
            dv.IdVentaNavigation.FechaRegistro.Value.Date >= fecha_fin.Date &&
            dv.IdVentaNavigation.FechaRegistro.Value.Date <= fecha_fin.Date
          ).ToListAsync();

      }
      catch (Exception ex) {
        throw;
      }

      return _mapper.Map<List<ReporteDTO>>(listaVentas);


    }
  }
}
