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
 
  public class MenuService:IMenuService
  {
    private readonly IgenericRepository<Usuario>? _usuarioRepositorio;
    private readonly IgenericRepository<MenuRol> _menuRolRepositorio;
    private readonly IgenericRepository<Menu> _menuRepositorio;
    private readonly IMapper _mapper;

    public MenuService(IgenericRepository<Usuario>? usuarioRepositorio, IgenericRepository<MenuRol> menuRolRepositorio, IgenericRepository<Menu> menuRepositorio, IMapper mapper)
    {
      _usuarioRepositorio = usuarioRepositorio;
      _menuRolRepositorio = menuRolRepositorio;
      _menuRepositorio = menuRepositorio;
      _mapper = mapper;
    }

    public async Task<List<MenuDTO>> lista(int idUsuario)
    {
      IQueryable<Usuario> tbUsuarios = await _usuarioRepositorio.consultar(u => u.IdUsuario == idUsuario);
      IQueryable<MenuRol> tbMenuRol = await _menuRolRepositorio.consultar();
      IQueryable<Menu> tbMenu = await _menuRepositorio.consultar();

      try
      {
        IQueryable<Menu> tbResultado = (from u in tbUsuarios
                                           join mr in tbMenuRol on u.IdRol equals mr.IdRol
                                           join m in tbMenu on mr.IdMenu equals m.IdMenu
                                           select m).AsQueryable();

        var listaMenus = tbResultado.ToList();

        return _mapper.Map<List<MenuDTO>>(listaMenus);
      }
      catch (Exception ex)
      {
        throw;
      }
    }
  }
}
