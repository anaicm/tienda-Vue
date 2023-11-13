using System.ComponentModel.DataAnnotations;

namespace Tienda.Models;
public class EditUserModel
{
  [Required]
  public string? Username { get; set; }
  
  [Required]
  public string? Email { get; set; }
}