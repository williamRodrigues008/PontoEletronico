using System.ComponentModel.DataAnnotations;

namespace PontoEletronico.Models.Classes
{
	public class Perfil
	{
		public int Id{ get; set; }

		[Required(ErrorMessage = "Por favor, informe um nome")]
		public string Nome { get; set; }

		[Required(ErrorMessage = "Por favor, informe um e-mail")]
		public string Email { get; set; }

		[Required(ErrorMessage = "Por favor, informe um nome")]
		public string Telefone { get; set; }

		public string Foto { get; set; }
	}
}
