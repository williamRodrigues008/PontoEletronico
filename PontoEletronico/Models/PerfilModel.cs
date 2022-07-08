namespace PontoEletronico.Models
{
	public class PerfilModel
	{
		public string Nome { get; set; }
		public string Email { get; set; }
		public string Telefone { get; set; }
		public IFormFile Foto { get; set; }
	}
}
