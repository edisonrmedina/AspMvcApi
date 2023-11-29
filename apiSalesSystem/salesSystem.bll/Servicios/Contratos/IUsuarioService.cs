using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using salesSystem.dto;


namespace salesSystem.bll.Servicios.Contratos
{
  public interface IUsuarioService
  {
    Task<List<UsuarioDTO>> lista();
    Task<SesionDTO> validarCredenciales(string correo, string clave);
    Task<UsuarioDTO> crear(UsuarioDTO usuarioModel);
    Task<bool> Editar(UsuarioDTO usuarioModel);
    Task<bool> Eliminar(int i);
  }
}
