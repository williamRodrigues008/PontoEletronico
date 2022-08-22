using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using PontoEletronico.DadosSql;
using PontoEletronico.Models;
using System.Data;

namespace PontoEletronico.Controllers
{
	public class InicioController : Controller
	{
		private readonly AppDbContext _dbContext;

		public InicioController(AppDbContext dbContext)
		{
			_dbContext = dbContext;
		}

		public IActionResult Index()
		{
			return View();
		}
		public async Task<IActionResult> Sair()
		{
			if (User.Identity.IsAuthenticated)
			{
				await HttpContext.SignOutAsync();
			}
			return RedirectToAction("Index");
		}

		public IActionResult RegistrarEntrada()
		{
			if (User.Identity.IsAuthenticated)
			{
				Conexao conexao = new();

				var cmd = conexao.ComandoSql("spRegistraEntrada");
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.Parameters.AddWithValue("@idUsuario", RetornarIdDoUsuario());
				cmd.ExecuteNonQuery();
				ViewBag.Mensagem = "Entrada Registrada com sucesso !";
				return RedirectToAction("Index", "Inicio");
			}
			else
			{
				return RedirectToAction("Index", "Home");
			}
		}
		public IActionResult RegistrarPausa()
		{
			if (User.Identity.IsAuthenticated)
			{
				Conexao conexao = new();
				try
				{
					var cmd = conexao.ComandoSql("spRegistraPausaAlmoco");
					cmd.CommandType = CommandType.StoredProcedure;
					cmd.Parameters.AddWithValue("@idUsuario", RetornarIdDoUsuario());
					cmd.ExecuteNonQuery();
					ViewBag.Mensagem = "Pausa Registrada com sucesso !";
				}
				catch (Exception)
				{

					throw;
				}
				

				return RedirectToAction("Index", "Inicio");
			}
			else
			{
				return RedirectToAction("Index", "Home");
			}
		}

		public IActionResult RegistrarRetorno()
		{
			if (User.Identity.IsAuthenticated)
			{
				Conexao conexao = new();

				var cmd = conexao.ComandoSql("spRegistraRetornoAlmoco");
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.Parameters.AddWithValue("@idUsuario", RetornarIdDoUsuario());
				cmd.ExecuteNonQuery();
				return RedirectToAction("Index", "Perfil");
			}
			else
			{
				return RedirectToAction("Index", "Home");
			}
		}
		public IActionResult RegistrarSaida()
		{
			if (User.Identity.IsAuthenticated)
			{
				Conexao conexao = new();

				var cmd = conexao.ComandoSql("spRegistraSaida");
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.Parameters.AddWithValue("@idUsuario", RetornarIdDoUsuario());
				cmd.ExecuteNonQuery();
				return RedirectToAction("Index", "Perfil");
			}
			else
			{
				return RedirectToAction("Index", "Home");
			}
		}
		private int RetornarIdDoUsuario()
		{
				var nomeUsuario = User.Identity.Name;
				var usuario = _dbContext.Usuarios.FirstOrDefault(u => u.Nome == nomeUsuario);
				return usuario.Id;
		}
	}
}
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                     