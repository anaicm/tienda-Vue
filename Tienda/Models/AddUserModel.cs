using System.ComponentModel.DataAnnotations;

namespace Tienda.Models;
// solo me quedo con los campos que me interesa de la tabla por defecto que me da identity para el añadir usuario
public class AddUserModel
{
  [Required]
  public string? Username { get; set; }
  
  [Required]
  public string? Email { get; set; }

  [Required]
  public string? Password { get; set; }
}