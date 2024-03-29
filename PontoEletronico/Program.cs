using Microsoft.AspNetCore;
using Microsoft.EntityFrameworkCore;
using PontoEletronico.Interfaces;
using PontoEletronico.Models;
using PontoEletronico.Models.Classes;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddAuthentication("Identity.Login")
				.AddCookie("Identity.Login", config =>
				{
					config.Cookie.Name = "Identity.Login";
					config.LoginPath = "/Perfil";
					config.AccessDeniedPath = "/Perfil";
					config.ExpireTimeSpan = TimeSpan.FromHours(1);
				});
builder.Services.AddDbContext<AppDbContext>(opts =>
				opts?.UseSqlServer(builder.Configuration.GetConnectionString("StringConexao")));


builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Home/Error");
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}



app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Inicio}/{action=Index}/{id?}");

app.Run();
