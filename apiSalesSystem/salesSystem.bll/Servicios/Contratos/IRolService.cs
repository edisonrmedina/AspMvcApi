using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using salesSystem.dto;

namespace salesSystem.bll.Servicios.Contratos
{
    public interface IRolService
    {

    Task<List<RolDTO>> lista();

  }
}
