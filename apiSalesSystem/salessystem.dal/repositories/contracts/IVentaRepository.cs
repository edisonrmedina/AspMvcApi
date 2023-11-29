using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using salesSystem.model;

namespace salessystem.dal.repositories.contracts
{
  
  public interface IVentaRepository:IgenericRepository<Venta>
  {
    Task<Venta> Registrar(Venta modeloVenta);
  }
}
