using System;
using System.Collections.Generic;

namespace Tienda;

public partial class Producto
{
    public int Id { get; set; }

    public int? EstableciminetoId { get; set; }

    public string? Nombre { get; set; }

    public decimal? Precio { get; set; }

    public int? Stock { get; set; }

    public virtual Establecimiento? Establecimineto { get; set; }

    public virtual ICollection<Venta> Venta { get; set; } = new List<Venta>();
}
