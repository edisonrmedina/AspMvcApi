using System;
using System.Collections.Generic;
using System.Globalization;
using AutoMapper;
using salesSystem.dto;
using salesSystem.model;

namespace salesSystem.utilities
{
  public class AutoMapperProfile : Profile
  {
    public AutoMapperProfile()
    {

      #region Rol
      CreateMap<Rol, RolDTO>().ReverseMap();
      #endregion
      
      #region Menu
      CreateMap<Menu, MenuDTO>().ReverseMap();
      #endregion

      
      #region Usuario
      // Mapeo de Usuario a UsuarioDTO
      CreateMap<Usuario, UsuarioDTO>()
          .ForMember(destino =>
              destino.RolDescription,
              opt => opt.MapFrom(origen => origen.IdRolNavigation.Nombre)
          )
          .ForMember(destino =>
              destino.EsActivo,
              opt => opt.MapFrom(origen => origen.EsActivo == true ? 1 : 0));

      // Mapeo de Usuario a SesionDTO
      CreateMap<Usuario, SesionDTO>()
          .ForMember(destino =>
              destino.RolDescription,
              opt => opt.MapFrom(origen => origen.IdRolNavigation.Nombre)
          );

      // Mapeo inverso de UsuarioDTO a Usuario
      CreateMap<UsuarioDTO, Usuario>()
          .ForMember(destino =>
              destino.IdRol, opt => opt.Ignore());

      CreateMap<UsuarioDTO, Usuario>()
          .ForMember(destino =>
              destino.EsActivo, opt => opt.MapFrom(origen => origen.EsActivo == 1 ? true : false));

      #endregion

      #region Categoria
      CreateMap<Categoria, CategoriaDTO>();
      CreateMap<CategoriaDTO, Categoria>();
      #endregion

      #region Producto
      // Mapeo de Producto a ProductoDTO
      CreateMap<Producto, ProductoDTO>()
          .ForMember(
              destino => destino.DescripcionCategoria,
              opt => opt.MapFrom(origen => origen.IdCategoriaNavigation.Nombre)
          )
          .ForMember(
              destino => destino.Precio, opt => opt.MapFrom(origen => Convert.ToString(origen.Precio.Value, new CultureInfo("es-EC"))
          ))
          .ForMember(destino =>
              destino.EsActivo,
              opt => opt.MapFrom(origen => origen.EsActivo == true ? 1 : 0));

      // Mapeo inverso de ProductoDTO a Producto
      CreateMap<ProductoDTO, Producto>()
    .ForPath(destino => destino.IdCategoriaNavigation.Nombre, opt => opt.MapFrom(origen => origen.DescripcionCategoria))
    .ForMember(destino => destino.Precio, opt => opt.MapFrom(origen => Convert.ToDecimal(origen.Precio, new CultureInfo("es-EC"))))
    .ForMember(destino => destino.EsActivo, opt => opt.MapFrom(origen => origen.EsActivo == true ? 1 : 0));


      #endregion

      #region Venta
      // Mapeo de Venta a VentaDTO
      CreateMap<Venta, VentaDTO>()
          .ForMember(
              destino => destino.TotalTexto,
              opt => opt.MapFrom(origen => Convert.ToString(origen.Total.Value, new CultureInfo("es-EC"))
          ))
          .ForMember(
              destino => destino.FechaRegistro,
              opt => opt.MapFrom(origen => origen.FechaRegistro.Value.ToString("dd/MM/yyyy"))
          );

      // Mapeo inverso de VentaDTO a Venta
      CreateMap<VentaDTO, Venta>()
          .ForMember(
              destino => destino.Total, opt => opt.MapFrom(origen => Convert.ToDecimal(origen.TotalTexto, new CultureInfo("es-EC"))
          ));

      #endregion

      #region Detalle Venta
      // Mapeo de DetalleVenta a DetalleVentaDto
      CreateMap<DetalleVenta, DetalleVentaDto>()
          .ForMember(
              destino => destino.DescripcionProductos,
              opt => opt.MapFrom(origen => origen.IdProductoNavigation.Nombre)
          )
          .ForMember(
              destino => destino.PrecioTexto,
              opt => opt.MapFrom(origen => Convert.ToString(origen.Precio.Value, new CultureInfo("es-EC")))
          ).ForMember(
              destino => destino.TotalTexto,
              opt => opt.MapFrom(origen => Convert.ToString(origen.Total.Value, new CultureInfo("es-EC")))
          );

      // Mapeo inverso de DetalleVentaDto a DetalleVenta
      CreateMap<DetalleVentaDto, DetalleVenta>()
    .ForPath(destino => destino.IdProductoNavigation.Nombre, opt => opt.MapFrom(origen => origen.DescripcionProductos))
    .ForMember(destino => destino.Precio, opt => opt.MapFrom(origen => Convert.ToDecimal(origen.PrecioTexto, new CultureInfo("es-EC"))))
    .ForMember(destino => destino.Total, opt => opt.MapFrom(origen => Convert.ToDecimal(origen.TotalTexto, new CultureInfo("es-EC"))));


      #endregion

      #region Reporte
      // Mapeo de DetalleVenta a ReporteDTO
      CreateMap<DetalleVenta, ReporteDTO>()
        .ForMember(
            destino => destino.FechaRegistro,
            opt => opt.MapFrom(origen => origen.IdProductoNavigation.FechaRegistro.Value.ToString("dd/MM/yyyy"))
         )
        .ForMember(
            destino => destino.NumeroDocumento,
            opt => opt.MapFrom(origen => origen.IdVentaNavigation.NumeroDocumento)
         )
        .ForMember(
            destino => destino.TipoPago,
            opt => opt.MapFrom(origen => origen.IdVentaNavigation.TipoPago)
         )
         .ForMember(
              destino => destino.TotalVenta,
              opt => opt.MapFrom(origen => Convert.ToString(origen.IdVentaNavigation.Total.Value, new CultureInfo("es-EC")))
          )
         .ForMember(
              destino => destino.Producto,
              opt => opt.MapFrom(origen => origen.IdProductoNavigation.Nombre)
          )
         .ForMember(
              destino => destino.Precio,
              opt => opt.MapFrom(origen => Convert.ToString(origen.IdProductoNavigation.Precio))
          )
         .ForMember(
              destino => destino.Total,
              opt => opt.MapFrom(origen => Convert.ToString(origen.Total))
          );

      #endregion

    }
  }
}
