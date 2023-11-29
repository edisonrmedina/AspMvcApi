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

namespace salesSystem.bll.Servicios.Contratos
{
  public interface ICategoriaService
  {

    Task<List<CategoriaDTO>> lista();

  }
}
