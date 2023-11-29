using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace salessystem.dal.repositories.contracts
{
  public interface IgenericRepository<Tmodel> where Tmodel : class
  {
    Task<Tmodel> obtener(Expression<Func<Tmodel, bool>> filtro);
    Task<Tmodel> crear(Tmodel modelo);
    Task<bool> editar(Tmodel modelo);

    /// ver si le puedes cambiar el modelo
    Task<bool> eliminar(Tmodel modelo);
    Task<IQueryable<Tmodel>> consultar(Expression<Func<Tmodel, bool>> filtro=null);
  }
}
