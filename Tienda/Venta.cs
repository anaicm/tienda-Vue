using System;
using System.Collections.Generic;

namespace Tienda;

public partial class Venta
{
    public int Id { get; set; }

    public int? TiendaId { get; set; }

    public int? ProductoId { get; set; }

    public string? UsuarioId { get; set; }

    public int? Cantidad { get; set; }

    public DateTime? Fecha { get; set; }

    public virtual Producto? Producto { get; set; }

    public virtual Establecimiento? Tienda { get; set; }

    public virtual AspNetUser? Usuario { get; set; }
}
