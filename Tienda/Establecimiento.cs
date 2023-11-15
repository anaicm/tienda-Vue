using System;
using System.Collections.Generic;

namespace Tienda;

public partial class Establecimiento
{
    public int Id { get; set; }

    public string? Nombre { get; set; }

    public string? Direccion { get; set; }

    public string? Telefono { get; set; }

    public string? UsuarioId { get; set; }

    public virtual AspNetUser? Usuario { get; set; }

    public virtual ICollection<Venta> Venta { get; set; } = new List<Venta>();
}
