using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using salesSystem.dal.dbContext;
using salessystem.dal.repositories.contracts;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;


namespace salessystem.dal.repositories
{
  public class GenericRepository<Tmodel> : IgenericRepository<Tmodel> where Tmodel : class
  {
    private readonly DbventaContext _dbContext;

    public GenericRepository(DbventaContext dbContext)
    {
      _dbContext = dbContext;
    }

    public async Task<IQueryable<Tmodel>> consultar(Expression<Func<Tmodel, bool>> filtro = null)
    {
      try
      {
        IQueryable<Tmodel> queryModelo = _dbContext.Set<Tmodel>();

        if (filtro != null)
        {
          queryModelo = queryModelo.Where(filtro);
        }

        return queryModelo.AsNoTracking(); // No siempre es necesario AsQueryable si ya es IQueryable
      }
      catch (Exception ex)
      {
        // Registra o maneja la excepción de alguna manera
        Console.WriteLine($"Error en ConsultarAsync: {ex}");
        throw;
      }
    }

    public async Task<Tmodel> obtener(Expression<Func<Tmodel, bool>> filtro)
    {
      try
      {
        Tmodel modelo = await _dbContext.Set<Tmodel>().FirstOrDefaultAsync(filtro);
        return modelo;
      }
      catch
      {
        throw;
      }
    }

    public async Task<Tmodel> crear(Tmodel modelo)
    {
      try
      {
        _dbContext.Set<Tmodel>().Add(modelo);
        await _dbContext.SaveChangesAsync();
        return modelo;
      }
      catch
      {
        throw;
      }

    }

    public async Task<bool> editar(Tmodel modelo)
    {
      try
      {
        _dbContext.Set<Tmodel>().Update(modelo);
        await _dbContext.SaveChangesAsync();
        return true;
      }
      catch
      {
        throw;
      }

    }

    public async Task<bool> eliminar(Tmodel modelo)
    {
      try
      {
        _dbContext.Set<Tmodel>().Remove(modelo);
        await _dbContext.SaveChangesAsync();
        return true;
      }
      catch
      {
        throw;
      }
    }

    
  }
}
