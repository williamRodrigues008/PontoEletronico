using System.ComponentModel.DataAnnotations.Schema;

namespace PontoEletronico.Models.Classes
{
	[Table("Registros")]
	public class Registros
	{
		[Column("IdRegistro")]
		public int Id { get; set; }

		[Column("Entrada")]
		public DateTime Entrada { get; set; }

		[Column("PausaAlmoco")]
		public DateTime PausaAlmoco { get; set; }

		[Column("RetornoAlmoco")]
		public DateTime RetornoAlmoco { get; set; }

		[Column("Saida")]
		public DateTime Saida { get; set; }

		[Column("IdUsuario")]
		public int IdUsuario { get; set; }
	}
}
