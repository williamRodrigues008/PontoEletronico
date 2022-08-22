using PontoEletronico.Interfaces;
using System.ComponentModel.DataAnnotations.Schema;

namespace PontoEletronico.Models.Classes
{
	[Table("Usuarios")]
    public class Usuarios
    {

        [Column("Id")]
		public int Id{ get; set; }

		[Column("Nome")]
		public string Nome { get; set; }

		[Column("Email")]
		public string Email { get; set; }


		[Column("Telefone")]
		public string? Telefone { get; set; }

		[Column("Foto")]
		public string? Foto { get; set; }
    }
}
