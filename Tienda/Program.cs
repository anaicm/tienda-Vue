using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.Cookies;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//Añadir contextos (acceso a base de datos)
builder.Services.AddDbContext<UsersContext>
(options=>options.UseSqlServer
(@"Data Source=PORTATIL\SQLEXPRESS;Initial Catalog=Tienda;User Id=sa;Password=rootadmin;Encrypt=false"));
//---------------------------------------------------------------------------
//En program.css 1-. primero se pone los servicios 
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//--distinto dominio habilitar las cors-----------------------------------------------------------
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder
            .WithOrigins("http://localhost:8080")  // Reemplaza con el origen correcto de tu frontend
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials(); // Habilita las credenciales
    });
});
// Añadir identity al proyecto usando el usersontext el user y rol por defecto.
// Añade authentication a la api con los usuarios de la tabla ASPNET_USERS (que son los mapeados de IdentityUser)
builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
{
    //restricciones para la contraseña
    options.Password.RequiredLength = 0;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireDigit = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireLowercase = false;
})
  .AddEntityFrameworkStores<UsersContext>()
  .AddDefaultTokenProviders();

//Añado la posibilidad de authencation con Cookies
builder.Services.Configure<CookieAuthenticationOptions>(IdentityConstants.ApplicationScheme, options =>
{
  var expireTimeSpanString = "100000";
  if (TimeSpan.TryParse(expireTimeSpanString, out TimeSpan expireTimeSpan))
  {
    options.ExpireTimeSpan = expireTimeSpan;
  }
  // Do not redirect on authentication fail, just set 401
  options.Events.OnRedirectToLogin = context =>
  {
    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
    return Task.CompletedTask;
  };

  options.AccessDeniedPath = "/index.html";
});

//2-. Segundo la app 

var app = builder.Build();
//--añadir para que no bloquee en el front
app.UseCors("AllowAll");
//-------------------------------------------------------------------------------

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
