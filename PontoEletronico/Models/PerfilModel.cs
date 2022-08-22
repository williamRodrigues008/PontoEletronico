using PontoEletronico.Models.Classes;

namespace PontoEletronico.Models
{
	public class PerfilModel
	{
		public IEnumerable<Usuarios> Usuario { get; set; }

		public int Id { get; set; }
		public string Nome { get; set; }

		public string Email { get; set; }

		public string Telefone { get; set; }

		public IFormFile Foto { get; set; }
		public string FotoDoBanco { get; set; }

	}

}
