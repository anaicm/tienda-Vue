using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Tienda.Models;

namespace Tienda.Controllers;
[Authorize]
[ApiController]
[Route("[controller]")]
public class EstablecimientosController : ControllerBase
{
    private readonly ILogger<EstablecimientosController> _logger;//por defecto
    //userManager => Realiza el CRUD de los usuarios
    private readonly TiendaContext _tiendaContext;//usuarios

    public EstablecimientosController(
        ILogger<EstablecimientosController> logger,
        TiendaContext tiendaContext)
    {
        _logger = logger;
        _tiendaContext = tiendaContext;
    }
    //--------------------------------------------------------------------------------------------------------------------------------------
    [HttpGet]// traer todos los usuarios
    public IActionResult Get()
    {
        return Ok(_tiendaContext.Establecimientos);//devuelve los usuarios de la tabla 'aspnet_user' 
    }
    //-----------------------------------------------------------------------------------------------------
    [HttpPut]
    public async Task<IActionResult> Update(Establecimiento tienda)
    {
        var establecimientoDb = _tiendaContext.Establecimientos.Find(tienda.Id);//busca la venta en la BD y coge su id
        if (establecimientoDb != null)//si existe la venta 
        {
            establecimientoDb.Nombre = tienda.Nombre;//actualiza la cantidad
            establecimientoDb.Direccion = tienda.Direccion;//actualiza la fecha
            establecimientoDb.Telefono = tienda.Telefono;//actualiza el telefono
                                                          // Guarda los cambios en la base de datos
            await _tiendaContext.SaveChangesAsync();//guarda los cambios
            return Ok();
        }
        return BadRequest();
    }
    //--------------------------------------------------------------------------------------------------
    [HttpPost]//añadir registro
    public ActionResult Add(Establecimiento tienda)
    {
        _tiendaContext.Establecimientos.Add(tienda);//añade una fila nueva con el nuevo registro en la tabla Libro
        _tiendaContext.SaveChanges();//guarda los cambios
        return Ok(tienda);//devuelve un ok del elemento
    }
    //----------------------------------------------------------------------------------------------    
    [HttpDelete("{id}")]//Eliminar registro
    public IActionResult Delete(int id)
    {
        var itemToDelete = _tiendaContext.Establecimientos.FirstOrDefault(w => w.Id == id);
        if (itemToDelete == null)
        {
            return NotFound();
        }
        _tiendaContext.Establecimientos.Remove(itemToDelete);
        _tiendaContext.SaveChanges();//guarda los cambios
        return NoContent();//devuelve un ok del elemento

    }
//---------Obtener los establecimientos según el id de usuario--------------------------------------
[HttpGet("{idUsuario}")]
public IActionResult GetEstablecimientosPorUsuarioId(string idUsuario)
{
  //Obtener los establecimientos del usuario
  var establecimientos = _tiendaContext.Establecimientos
    .Where(e => e.Usuario_id != null && e.Usuario_id == idUsuario)
    .ToList();

  //Devolver los establecimientos
  return Ok(establecimientos);
}
}

