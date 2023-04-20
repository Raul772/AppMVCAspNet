using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Teste.Areas.Identity.Data;
using Teste.Config;
using KissLog.AspNetCore;
using KissLog.CloudListeners.Auth;
using KissLog.CloudListeners.RequestLogsListener;
using KissLog.Formatters;
using Teste.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Utilizando diferentes ambientes de execução
builder.Configuration
    .SetBasePath(builder.Environment.ContentRootPath)
    .AddJsonFile("appsettings.json", true, true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", true, true)
    .AddEnvironmentVariables();

if (builder.Environment.IsProduction())
{
    builder.Configuration.AddUserSecrets<Program>();
}

// Utilização do KissLog para geração dos Logs
builder.Services.AddLogging(provider =>
{
    provider.AddKissLog(options =>
            {
                options.Formatter = (FormatterArgs args) =>
                {
                    if (args.Exception == null)
                        return args.DefaultValue;

                    string exceptionStr = new ExceptionFormatter().Format(args.Exception, args.Logger);
                    return string.Join(Environment.NewLine, new[] { args.DefaultValue, exceptionStr });
                };
            });
});

builder.Services.AddHttpContextAccessor();


// -------------------------------------------------------------------

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

//  Registro de Policies através de Claims (Externalizado)
builder.Services.ConfigAuthorizations();

//  Configurações de injeção de dependência (Externalizado)
builder.Services.ResolveDependencies();


builder.Services.AddControllersWithViews(options =>
{
    options.Filters.Add<AuditoriaFilter>();
});



var app = builder.Build();


app.UseKissLogMiddleware(options => {
    options.Listeners.Add(
        new RequestLogsApiListener(
            new Application(
                builder.Configuration["KissLog.OrganizationId"],    //  "01c4a9ef-4d11-467f-9234-ad9d575a2bcb"
                builder.Configuration["KissLog.ApplicationId"]     //  "2d81cc7f-76bf-4ab6-a453-0d9cdcd69413"
            )
        )
    {
        ApiUrl = builder.Configuration["KissLog.ApiUrl"]    //  "https://api.kisslog.net"
    });
});


if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/error/500");
    app.UseStatusCodePagesWithRedirects("/error/{0}");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

//  Utilização de ASP.NET Identity para autenticação e autorização.
app.UseAuthentication();
app.UseAuthorization();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

//  Mapear as rotas das Razor Pages
app.MapRazorPages();

app.Run();
