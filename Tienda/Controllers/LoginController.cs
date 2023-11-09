using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Tienda.Models;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;

namespace Tienda.Controllers;

[ApiController]
[Route("[controller]")]
public class LoginController : ControllerBase
{
    private readonly ILogger<LoginController> _logger;//por defecto
    //userManager => Realiza el CRUD de los usuarios
    private readonly SignInManager<IdentityUser> _signInManager;//usuarios
    private readonly UserManager<IdentityUser> _userManager;//usuarios
    public LoginController(
        ILogger<LoginController> logger, 
        SignInManager<IdentityUser> signInManager,
        UserManager<IdentityUser> userManager)
    {
        _logger = logger;
        _signInManager = signInManager;
        _userManager = userManager;
    }

    [HttpPost]//añadir registro
    public async Task<ActionResult> Login(LoginModel loginModel)
    {    
    var authResult = await _signInManager.PasswordSignInAsync(loginModel.Username, loginModel.Password, isPersistent: true, lockoutOnFailure: true);
    
    if (authResult == SignInResult.TwoFactorRequired)
    {
      return BadRequest();
    }

    if (authResult == SignInResult.Success)
    {
      var user = await _userManager.FindByNameAsync(loginModel.Username);
      //habría que generar el token (PEROOOOO como lo estamos haciendo con cookies, se generea automagicamente sola)
      return Ok(new {
        permissions = "Admin", 
        userData = new {
          userName = loginModel.Username,
          id = user?.Id}
      }); //En esta respuesta ok 200 se verá como viene la cookie ya en el inspeccionar y los datos del usuario
    }

    if (authResult == SignInResult.LockedOut)
    {
      return BadRequest();
    }    

    return BadRequest();
    }

    [Authorize]//si esta autorizado (tiene cookie creada)
    [HttpPost("Logout")]//añadir registro
    public async Task<ActionResult> Logout()
    {      
        await _signInManager.SignOutAsync(); 
        return Ok();   
    }


}


