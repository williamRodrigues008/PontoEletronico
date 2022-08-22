using PontoEletronico.Models.Classes;

namespace PontoEletronico.Interfaces
{
	public interface IPerfil
	{
		public Task<IEnumerable<Usuarios>> ObterPerfil();
	}
}
