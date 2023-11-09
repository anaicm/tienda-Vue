using System.ComponentModel.DataAnnotations;

namespace Tienda.Models;
// solo me quedo con los campos que me interesa para el añadir usuario
public class LoginModel
{
  [Required]
  public string? Username { get; set; }
  [Required]
  public string? Password { get; set; }
}