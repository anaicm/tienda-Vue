using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Tienda;



[Authorize]
[ApiController]
[Route("[controller]")]
public class ProductosController : ControllerBase
{
    private readonly ILogger<ProductosController> _logger;//por defecto
    //userManager => Realiza el CRUD de los usuarios
    private readonly TiendaContext _tiendaContext;//usuarios

    public ProductosController(
        ILogger<ProductosController> logger,
        TiendaContext tiendaContext)
    {
        _logger = logger;
        _tiendaContext = tiendaContext;
    }
    //--------------------------------------------------------------------------------------------------------------------------------------
    [HttpGet]// traer todos los usuarios
    public IActionResult Get()
    {
        return Ok(_tiendaContext.Productos);//devuelve los usuarios de la tabla 'aspnet_user' 
    }
    //-----------------------------------------------------------------------------------------------------
    [HttpPut]
    public async Task<IActionResult> Update(Producto producto)
    {
        var productoDb = _tiendaContext.Productos.Find(producto.Id);//busca la venta en la BD y coge su id
        if (productoDb != null)//si existe la venta 
        {
            productoDb.Nombre = producto.Nombre;//actualiza la cantidad
            productoDb.Precio = producto.Precio;//actualiza la fecha
            productoDb.Stock = producto.Stock;//actualiza el telefono
                                              // Guarda los cambios en la base de datos
            await _tiendaContext.SaveChangesAsync();//guarda los cambios
            return Ok();
        }
        return BadRequest();
    }
    //--------------------------------------------------------------------------------------------------
    [HttpPost]//añadir registro
    public ActionResult Add(Producto producto)
    {
        _tiendaContext.Productos.Add(producto);//añade una fila nueva con el nuevo registro en la tabla Libro
        _tiendaContext.SaveChanges();//guarda los cambios
        return Ok(producto);//devuelve un ok del elemento
    }
    //----------------------------------------------------------------------------------------------    
    [HttpDelete("{id}")]//Eliminar registro
    public IActionResult Delete(int id)
    {
        var itemToDelete = _tiendaContext.Productos.Find(id);
        if (itemToDelete == null)
        {
            return NotFound();
        }
        _tiendaContext.Productos.Remove(itemToDelete);
        _tiendaContext.SaveChanges();//guarda los cambios
        return NoContent();//devuelve un ok del elemento

    }

}

