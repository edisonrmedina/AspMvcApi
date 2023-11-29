using salesSystem.bll.Servicios.Contratos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using salesSystem.bll.Servicios.Contratos;
using salessystem.dal.repositories.contracts;
using salesSystem.dto;
using salesSystem.model;

namespace salesSystem.bll.Servicios
{
  public class RolService : IRolService
  {
    private readonly IgenericRepository<Rol> _rolRepositorio;
    private readonly IMapper _mapper;

    public RolService(IgenericRepository<Rol> rolRepositorio, IMapper mapper)
    {
      _rolRepositorio = rolRepositorio;
      _mapper = mapper;
    }

    public async Task<List<RolDTO>> lista()
    {
      try
      {
        var listRol = await _rolRepositorio.consultar();
        return _mapper.Map<List<RolDTO>>(listRol.ToList());

      }
      catch (Exception ex)
      {
        throw;
      }
    }

  }
}
