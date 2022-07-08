using Microsoft.EntityFrameworkCore;
using PontoEletronico.Models.Classes;

namespace PontoEletronico.Models
{
	public class AppDbContext : DbContext
	{
		public AppDbContext(DbContextOptions <AppDbContext> options) 
			: base(options)
		{
		}
		public DbSet<Perfil> PerfilUsuario { get; set; }
	}
}
