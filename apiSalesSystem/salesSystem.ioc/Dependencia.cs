using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using salesSystem.dal.dbContext;
using salessystem.dal.repositories;
using salessystem.dal.repositories.contracts;
using salesSystem.dal.repositories;
using salesSystem.utilities;
using System;
using salesSystem.bll.Servicios.Contratos;
using salesSystem.bll.Servicios;

namespace salesSystem.ioc
{
  public static class Dependencia
  {
    /// <summary>
    /// Configura la inyección de dependencias para servicios utilizados en el sistema de ventas.
    /// </summary>
    /// <param name="service">La colección de servicios de la aplicación.</param>
    /// <param name="configuration">La configuración de la aplicación, incluida la cadena de conexión a la base de datos.</param>
    public static void inyectarDependencias(this IServiceCollection service, IConfiguration configuration)
    {
      // Configurar el contexto de la base de datos para la aplicación.
      try
      {
        service.AddDbContext<DbventaContext>(options =>
        {
          string connectionString = configuration.GetConnectionString("sqlConnectionString");
          options.UseSqlServer(connectionString);
        });
      }
      catch (Exception ex)
      {
        // Agrega detalles del error al mensaje de error
        string errorMessage = $"Error initializing DbContext: {ex.Message}";
        throw new Exception(errorMessage, ex);
      }

      // Configurar un servicio transitorio para el repositorio genérico.
      service.AddTransient(typeof(IgenericRepository<>), typeof(GenericRepository<>));

      // Configurar un servicio de ámbito para el repositorio de ventas específico.
      service.AddScoped<IVentaRepository, VentaRepository>();

      //Tomamos la clase donde esta todos nuestros Mapeos
      service.AddAutoMapper(typeof(AutoMapperProfile));

      service.AddScoped<IRolService, RolService>();
      service.AddScoped<IUsuarioService, UsuarioService>();
      service.AddScoped<ICategoriaService, CategoriaService>();
      service.AddScoped<iProductService, ProductService>();
      service.AddScoped<IVentaService, VentaService>();
      service.AddScoped<IDashboard, DashboardService>();
      service.AddScoped<IMenuService, MenuService>();
       

    }
  }
}
