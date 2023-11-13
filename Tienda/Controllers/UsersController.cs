using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Tienda.Models;

namespace Tienda.Controllers;
[Authorize]
[ApiController]
[Route("[controller]")]
public class UsersController : ControllerBase
{
    private readonly ILogger<UsersController> _logger;//por defecto
    //userManager => Realiza el CRUD de los usuarios
    private readonly UserManager<IdentityUser> _userManager;//usuarios

    public UsersController(
        ILogger<UsersController> logger, 
        UserManager<IdentityUser> userManager)
    {
        _logger = logger;
        _userManager = userManager;
    }
//--------------------------------------------------------------------------------------------------------------------------------------
    [HttpGet]// traer todos los usuarios
    public IActionResult Get()
    {
        return Ok(_userManager.Users);//devuelve los usuarios de la tabla 'aspnet_user' 
    }
//--------------------------------------------------------------------------------------------------------------------------------------

    [HttpPost]//añadir registro
    public async Task<ActionResult> Add(AddUserModel userModel)
    {            
       // var authenticatedUserName = HttpContext.User.Identity.Name; => obtiene el nombre del usuario por la cookies
       // var user = _userContext.FindByNameAsync(authenticatedUserName); => Devuelve el usuario en el que coincide el nombre
        
        var user = new IdentityUser
        {
            UserName = userModel.Username,
            Email = userModel.Email
        };

        if(userModel.Password != null) {
            //CreateAsync() => Crea el usuario y realiza el hash a la password
            var result = await _userManager.CreateAsync(user, userModel.Password);//CreateAsync()=> comprueba las reglas de la contraseña 
            if (result.Succeeded)
            {
                return Ok(); // Devuelve un Ok del elemento
            }
            else
            {
                // Manejar errores aquí si es necesario
                return BadRequest(result.Errors);
            }
        }

        return BadRequest();    
    } 
//--------------------------------------------------------------------------------------------------------------------------------------
   [HttpPut("{id}")] // Actualizar usuario por ID
    public async Task<ActionResult> Update(string id, EditUserModel userModel)
{
    // Buscar el usuario por su ID
    var user = await _userManager.FindByIdAsync(id);

    if (user == null)
    {
        return NotFound(); // Usuario no encontrado
    }

    // Actualizar propiedades del usuario
    user.UserName = userModel.Username;
    user.Email = userModel.Email;
    // UpdateAsync() => Actualiza las propiedades del usuario en la base de datos
    var result = await _userManager.UpdateAsync(user);

    if (result.Succeeded)
    {
        return Ok(); // Devuelve un Ok del elemento
    }
    else
    {
        // Manejar errores aquí si es necesario
        return BadRequest(result.Errors);
    }
}
//---------------------------------eliminar usuario

}

