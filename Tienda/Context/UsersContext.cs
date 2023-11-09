using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

//si no tiene parámetros las trablas son creadas por defecto
public class UsersContext: IdentityDbContext {
    public UsersContext(DbContextOptions<UsersContext>options):base(options){}
  
   

}