using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using PontoEletronico.DadosSql;
using PontoEletronico.Interfaces;
using PontoEletronico.Models;
using PontoEletronico.Models.Classes;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Security.Claims;

namespace PontoEletronico.Controllers
{

	public class PerfilController : Controller
	{
		private readonly ILogger<PerfilController> _logger;
		private readonly AppDbContext _dbContext;
		private readonly IWebHostEnvironment _webHostEnvironment;
		private JornadaHoras _horas;
#region Propriedade
		private string caminho;
		
		#endregion
		public PerfilController(ILogger<PerfilController> ilogger, AppDbContext context, IWebHostEnvironment hostEnvironment)
		{
			_logger = ilogger;
			_dbContext = context;
			_webHostEnvironment = hostEnvironment;
			caminho = hostEnvironment.WebRootPath;
		}
		[HttpGet]
		public IActionResult Index()
		{
			if (User.Identity.IsAuthenticated)
			{
				var nomeUsuario = User.Identity.Name;
				var usuario = _dbContext.Usuarios.FirstOrDefault(u => u.Nome == nomeUsuario);
				var idUsuario = usuario.Id;
				DadosHoras(idUsuario);
				return View(usuario);
			}
			else
			{
				return View();
			}
		}

		public IActionResult DadosHoras(int id)
        {
			_horas = new();
			Conexao conexao = new();

			var cmd = conexao.ComandoSql("spCarregaDadosHoras");

			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("@idUsuario", id);

			SqlDataReader leitor = cmd.ExecuteReader();
			if (leitor.Read())
			{
				ViewBag.Dia = leitor.GetInt32(0);
				ViewBag.Semana = leitor.GetInt32(1);
				ViewBag.Mes = leitor.GetInt32(2);
				ViewBag.Extra = leitor.GetInt32(4);
				ViewBag.Total = leitor.GetInt32(3);
			}
			return RedirectToAction("Index");
		}

		[HttpPost]
		public IActionResult Entrada(string email, string senha)
		{
			Conexao conexao = new();

			try
			{
				conexao.StringConexao();
				conexao.StringConexao().OpenAsync();
				var cmd = conexao.ComandoSql("spUsuarios");
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.Parameters.AddWithValue("@email", email);
				cmd.Parameters.AddWithValue("@senha", senha);

				SqlDataReader leitorDeDados = cmd.ExecuteReader();
				
				return View();
			}

			catch (Exception ex)
			{
				conexao.StringConexao().Close();
				throw new Exception("Ops! houve um erro na execução do comando SQL" + ex);
			}
			finally
			{
				conexao.StringConexao().Dispose();
				conexao.StringConexao().Close();
			}
		}

		[HttpPost]
		public IActionResult Index(IFormFile foto, int usuario)
		{
			if (foto != null)
			{
				string pastaServidor = caminho + "/Imagens/";
				string nomeArquivo = Guid.NewGuid().ToString() + "_" + foto.FileName;

				SalvaImagemNoDoServidor(foto, pastaServidor, nomeArquivo);
				PersisteImagemDePerfilNoBanco(usuario, nomeArquivo);
			}

			return RedirectToAction("Index");
		}

		private static void SalvaImagemNoDoServidor(IFormFile foto, string pastaServidor, string nomeArquivo)
		{
			if (!Directory.Exists(pastaServidor))
			{
				Directory.CreateDirectory(pastaServidor);
			}
			using (var arquivoStream = System.IO.File.Create(pastaServidor + nomeArquivo))
			{
				foto.CopyToAsync(arquivoStream);
			}
			return;
		}

		private void PersisteImagemDePerfilNoBanco(int usuario, string nomeArquivo)
		{
			using (var contexto = _dbContext)
			{
				var atualizar = contexto.Usuarios.FirstOrDefault(img => img.Id == usuario);
				if (atualizar != null)
				{
					atualizar.Foto = nomeArquivo;
					contexto.SaveChanges();
				}
			}
			return;
		}
	}
}
