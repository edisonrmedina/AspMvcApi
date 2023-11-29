using salesSystem.dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace salesSystem.bll.Servicios.Contratos
{
  public interface IDashboard
  {
    Task<DashBoardDTO> Resumen();
  }
}
