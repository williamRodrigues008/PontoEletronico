using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dados
{
	internal class ConexaoComBanco
	{
		public AddDbContext<AppDbContext> Conexao { get; set; }
	}
}
