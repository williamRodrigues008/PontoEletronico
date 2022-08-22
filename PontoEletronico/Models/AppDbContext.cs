using Microsoft.EntityFrameworkCore;
using PontoEletronico.Interfaces;
using PontoEletronico.Models.Classes;

namespace PontoEletronico.Models
{
	public class AppDbContext : DbContext
	{
		public AppDbContext(DbContextOptions <AppDbContext> options) 
			: base(options)
		{
		}
		public DbSet<Usuarios> Usuarios { get; set; }
		public DbSet<Registros> Registros { get; set; }
	}
}
