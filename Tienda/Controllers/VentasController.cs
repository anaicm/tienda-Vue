using System.IO.Compression;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Tienda.Models;

namespace Tienda.Controllers;
[Authorize]
[ApiController]
[Route("[controller]")]
public class VentasController : ControllerBase
{
    private readonly ILogger<UsersController> _logger;//por defecto
    //userManager => Realiza el CRUD de los usuarios
    private readonly TiendaContext _tiendaContext;//usuarios

    public VentasController(
        ILogger<UsersController> logger, 
        TiendaContext tiendaContext)
    {
        _logger = logger;
        _tiendaContext = tiendaContext;
    }
//--------------------------------------------------------------------------------------------------------------------------------------
    [HttpGet]// traer todos los usuarios
    public IActionResult Get()
    {
        return Ok(_tiendaContext.Ventas);//devuelve los usuarios de la tabla 'aspnet_user' 
    }
//-----------------------------------------------------------------------------------------------------
    [HttpPut]
public async Task<IActionResult> Update(Venta venta)
{
    var ventaDb = _tiendaContext.Ventas.Find(venta.Id);//busca la venta en la BD y coge su id
    if(ventaDb != null)//si existe la venta 
    {
        ventaDb.Cantidad = venta.Cantidad;//actualiza la cantidad
        ventaDb.Fecha = venta.Fecha;//actualiza la fecha
        // Guarda los cambios en la base de datos
        await _tiendaContext.SaveChangesAsync();//guarda los cambios
        return Ok();
    }
    return BadRequest();
}
//--------------------------------------------------------------------------------------------------
 [HttpPost]//añadir registro
    public ActionResult Add(Venta venta)
    {
        _tiendaContext.Ventas.Add(venta);//añade una fila nueva con el nuevo registro en la tabla Libro
        _tiendaContext.SaveChanges();//guarda los cambios
        return Ok(venta);//devuelve un ok del elemento
    }
//----------------------------------------------------------------------------------------------    
     [HttpDelete("{id}")]//Eliminar registro
    public IActionResult Delete(int id)
    {
        var itemToDelete = _tiendaContext.Ventas.FirstOrDefault(w => w.Id == id);
        if(itemToDelete == null)
        {
            return NotFound();
        }
        _tiendaContext.Ventas.Remove(itemToDelete);
        _tiendaContext.SaveChanges();//guarda los cambios
        return NoContent();//devuelve un ok del elemento
       
    }
//-----------ProductosVentas-----------------------------------
[HttpGet("{id}/producto-ventas")]
public IActionResult ProductoVentas(int id)
{
    var venta = _tiendaContext.Ventas.Find(id);
    if (venta == null)
    {
        return NotFound();
    }

    var productoVentas = _tiendaContext.ProductosVentas.Where(pv => pv.VentaId == id).ToList();

    return Ok(productoVentas);
}
}

