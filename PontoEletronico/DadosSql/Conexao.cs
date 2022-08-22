using System.Data.SqlClient;

namespace PontoEletronico.DadosSql
{
	public class Conexao
	{
		public SqlConnection StringConexao()
		{
			SqlConnection conexao = new SqlConnection(@"Data Source=DESKTOP-48ULVVB\NASERVER;Initial Catalog=Ponto;Integrated Security=True");
			return conexao;
		}

		public SqlCommand ComandoSql(string procedure)
		{
			SqlConnection conexao = StringConexao();
			conexao.Open();
			return new SqlCommand(procedure, conexao);
		}


	}
}
