using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace salesSystem.dto
{
  public class ProductoDTO
  {
    public int IdProducto { get; set; }
    public string? Nombre { get; set; }
    public string? DescripcionCategoria { get; set; }
    public int? Stock { get; set; }
    //aca no olvidar hacer la coneccion y todo.
    public string? Precio { get; set; }
    public bool? EsActivo { get; set; }

  }
}
