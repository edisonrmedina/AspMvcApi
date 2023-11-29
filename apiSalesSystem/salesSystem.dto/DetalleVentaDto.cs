using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace salesSystem.dto
{
  public class DetalleVentaDto
  {
    public int? IdProducto { get; set; }

    public string? DescripcionProductos { get; set; }
    public int? Cantidad { get; set; }

    public string? PrecioTexto { get; set; }

    public decimal? TotalTexto { get; set; }
  }
}
