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
using Microsoft.EntityFrameworkCore;

namespace salesSystem.bll.Servicios
{
  public class UsuarioService:IUsuarioService 
  {
    private readonly IgenericRepository<Usuario> _usuarioRepositorio;
    private readonly IMapper _mapper;

    public UsuarioService(IgenericRepository<Usuario> usuarioRepositorio, IMapper mapper)
    {
      _usuarioRepositorio = usuarioRepositorio;
      _mapper = mapper;
    }

    public async Task<List<UsuarioDTO>> lista()
    {
      try
      {
        var queryUsuario = await _usuarioRepositorio.consultar();
        var listaUsuarios = queryUsuario.Include(rol => rol.IdRolNavigation).AsNoTracking().ToList();

        return _mapper.Map<List<UsuarioDTO>>(listaUsuarios.ToList());

      }
      catch (Exception ex)
      {
        throw;
      }
    }

    public async Task<SesionDTO> validarCredenciales(string correo, string clave)
    {
      try
      {
        var queryUsuario = await _usuarioRepositorio.consultar(u =>
          u.Correo == correo &&
          u.Clave == clave
        );
        var userFinded = queryUsuario.FirstOrDefault();

        if ( userFinded == null )
        {
          throw new TaskCanceledException("Usuario no existe");
        }

        Usuario userFind = queryUsuario.Include(rol => rol.IdRolNavigation).First();

        return _mapper.Map<SesionDTO>(userFind);

      }
      catch (Exception ex)
      {
        throw;
      }
    }

    public async Task<UsuarioDTO> crear(UsuarioDTO usuarioModel)
    {
      try
      {
        var usuarioCreado = await _usuarioRepositorio.crear(_mapper.Map<Usuario>(usuarioModel));

        if (usuarioCreado.IdUsuario == 0)
          throw new TaskCanceledException("Usuario no se creo");

        var query = await _usuarioRepositorio.consultar(u => u.IdUsuario == usuarioCreado.IdUsuario);

        usuarioCreado = query.Include(rol => rol.IdRolNavigation).First();

        return _mapper.Map<UsuarioDTO>(usuarioCreado);

      }catch (Exception ex)
      {
        throw;
      }
    }

    public async Task<bool> Editar(UsuarioDTO usuarioModel)
    {
      try
      {
        var usuario = _mapper.Map<Usuario>(usuarioModel);
        var usuarioExistente = await _usuarioRepositorio.obtener(u => u.IdUsuario == usuario.IdUsuario);

        if (usuarioExistente == null)
        {
          throw new TaskCanceledException("Usuario no encontrado");
        }

        // Actualizar propiedades del usuarioExistente con los valores proporcionados en usuarioModel
        // Asegúrate de manejar todas las propiedades que deseas actualizar
        usuarioExistente.NombreCompleto = usuario.NombreCompleto;
        usuarioExistente.Correo = usuario.Correo;
        usuarioExistente.IdRol = usuario.IdRol;
        usuarioExistente.Clave = usuario.Clave;
        usuarioExistente.EsActivo = usuario.EsActivo;

        var usuarioActualizado = await _usuarioRepositorio.editar(usuarioExistente);

        if (!usuarioActualizado)
          throw new TaskCanceledException("No se pudo editar el usuario");

        return usuarioActualizado;
      }
      catch (Exception ex)
      {
        throw;
      }
    }

    public async Task<bool> Eliminar(int id)
    {
      try
      {
        var usuarioExistente = await _usuarioRepositorio.obtener(u => u.IdUsuario == id);

        if (usuarioExistente == null)
        {
          throw new TaskCanceledException("Usuario no encontrado");
        }

        var eliminado = await _usuarioRepositorio.eliminar(usuarioExistente);

        // eliminado es un booleano que indica si la eliminación fue exitosa
        if (!eliminado)
          throw new TaskCanceledException("No se pudo eliminar");

        return eliminado;

      }
      catch (Exception ex)
      {
        throw;
      }
    }

  }
}
