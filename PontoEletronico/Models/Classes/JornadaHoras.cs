using PontoEletronico.DadosSql;
using System.Data;
using System.Data.SqlClient;

namespace PontoEletronico.Models.Classes
{
    public class JornadaHoras
	{

		#region Propriedades
		private readonly AppDbContext contexto;

		public int Dia { get; set; }
		public int Semana { get; set; }
		public int Mes { get; set; }
		public int Extra { get; set; }
		public int Total { get; set; }
		#endregion

		public JornadaHoras()
		{
		}

		
	}
}
