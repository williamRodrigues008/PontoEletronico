using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using PontoEletronico.DadosSql;
using PontoEletronico.Models;
using PontoEletronico.Models.Classes;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Security.Claims;

namespace PontoEletronico.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;

		public HomeController(ILogger<HomeController> logger)
		{
			_logger = logger;
		}

		public IActionResult Index()
		{
			return View();
		}
		public async Task<IActionResult> Entrar(string email, string senha)
		{
			Conexao conexao = new();
			var jornada = new JornadaHoras();
			var cmd = conexao.ComandoSql("spUsuarios");
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("@email", email);
			cmd.Parameters.AddWithValue("@senha", senha);

			SqlDataReader leitor = cmd.ExecuteReader();

			if (await leitor.ReadAsync())
			{
				int idUsuario = leitor.GetInt32(0);
				string nomeUsuario = leitor.GetString(1);

				List<Claim> acessos = new List<Claim>
				{
					new Claim(ClaimTypes.NameIdentifier, idUsuario.ToString()),
					new Claim(ClaimTypes.Name, nomeUsuario)

				};

				var identidade = new ClaimsIdentity(acessos, "Identity.Login");
				var usuarioPrincipal = new ClaimsPrincipal(new[] { identidade });

				await HttpContext.SignInAsync(usuarioPrincipal, new AuthenticationProperties
				{
					IsPersistent = true,
					ExpiresUtc = DateTime.Now.AddHours(1),
				});
				return RedirectToAction("Index", "Perfil");
			}
			else
			{
				return Json(new { Msg = "Credenciais incorretas" });
			}
		}

		public IActionResult Privacy()
		{
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}