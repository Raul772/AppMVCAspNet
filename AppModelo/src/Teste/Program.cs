using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Teste.Areas.Identity.Data;
using Teste.Config;

var builder = WebApplication.CreateBuilder(args);

// Utilizando diferentes ambientes de execu��o
builder.Configuration
    .SetBasePath(builder.Environment.ContentRootPath)
    .AddJsonFile("appsettings.json", true, true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", true, true)
    .AddEnvironmentVariables();

if (builder.Environment.IsProduction())
{
    builder.Configuration.AddUserSecrets<Program>();
}


#region IdentityScaffolding
//  Gerado Por Identity Scaffolding ----------------------------------

var connectionString = builder.Configuration.GetConnectionString("TesteContextConnection") ?? throw new InvalidOperationException("Connection string 'TesteContextConnection' not found.");

builder.Services.AddDbContext<TesteContext>(options => options
    .UseSqlServer(connectionString));

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<IdentityRole>() //    Adicionado Manualmente para trabalhar com Roles do ASP.NET Identity
    .AddEntityFrameworkStores<TesteContext>();

// --------------------------------------------------------------------

#endregion

//  Registro de Policies atrav�s de Claims (Externalizado)
builder.Services.ConfigAuthorizations();

//  Configura��es de inje��o de depend�ncia (Externalizado)
builder.Services.ResolveDependencies();


builder.Services.AddControllersWithViews();
var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

//  Utiliza��o de ASP.NET Identity para autentica��o e autoriza��o.
app.UseAuthentication();
app.UseAuthorization();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

//  Mapear as rotas das Razor Pages
app.MapRazorPages();

app.Run();
