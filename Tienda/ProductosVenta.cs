using System;
using System.Collections.Generic;

namespace Tienda;

public partial class ProductosVenta
{
    public int? ProductoId { get; set; }

    public int? VentaId { get; set; }

    public virtual Producto? Producto { get; set; }

    public virtual Venta? Venta { get; set; }
}
