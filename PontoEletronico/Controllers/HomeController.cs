using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using PontoEletronico.Models;
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
		public async Task<IActionResult> Entrar(string usuario, string senha)
		{
			SqlConnection conexao = new SqlConnection(@"Data Source=DESKTOP-48ULVVB\NASERVER;Initial Catalog=Ponto;Integrated Security=True");

			await conexao.OpenAsync();
			string retornoDados = $"select * from Usuarios where usuario = '{usuario}' and senha = '{senha}'";
			SqlCommand comandoSql = new(retornoDados, conexao);

			SqlDataReader leitor = comandoSql.ExecuteReader();

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

				return RedirectToAction("Perfil", "Home");
				//return RedirectToAction("Index");
			}
			else
			{
				return Json(new { Msg = "Credenciais incorretas" });
			}
		}

		public IActionResult Perfil()
		{
			return View();
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