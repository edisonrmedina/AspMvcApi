using AutoMapper;
using salessystem.dal.repositories.contracts;
using salesSystem.bll.Servicios.Contratos;
using salesSystem.dto;
using salesSystem.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace salesSystem.bll.Servicios
{
  public class CategoriaService:ICategoriaService
  {
    private readonly IgenericRepository<Categoria> _categoriaRepositorio;
    private readonly IMapper _mapper;

    public CategoriaService(IgenericRepository<Categoria> categoriaRepositorio, IMapper mapper)
    {
      _categoriaRepositorio = categoriaRepositorio;
      _mapper = mapper;
    }

    public async Task<List<CategoriaDTO>> lista()
    {
      var listaCategorias = await _categoriaRepositorio.consultar();
      return _mapper.Map<List<CategoriaDTO>>(listaCategorias.ToList());
    }
  }
}
