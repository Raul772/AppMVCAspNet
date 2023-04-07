
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.EntityFrameworkCore;
using RL.ModelApp.Data;


//  Cria��o do objeto Builder
var builder = WebApplication.CreateBuilder(args);


//  Setar a Connection String do Entity Framework para comunica��o com Banco de Dados
//  A Conecction String est� contida no arquivo appsettings.json e automaticamente atribu�da ao objeto Builder
//  O atributo Configuration do Builder possui o m�todo GetConnectionString
builder.Services.AddDbContext<MeuDbContext>(options => 
        options.UseSqlServer(builder.Configuration.GetConnectionString("MeuDbContext")));


//  Adi��o do MVC no projeto
//  Atribuil�ao
builder.Services.AddControllersWithViews();
//  Adi��o do Razor Pages (equivalente ao services.AddMvc();)
builder.Services.AddRazorPages();


// Mudan�a de nome da pasta Areas para Modules
builder.Services.Configure<RazorViewEngineOptions>( options =>
{
    options.AreaViewLocationFormats.Clear();
    options.AreaViewLocationFormats.Add("/Modules/{2}/Views/{1}/{0}.cshtml");
    options.AreaViewLocationFormats.Add("/Modules/{2}/Views/Shared/{0}.cshtml");
    options.AreaViewLocationFormats.Add("/Views/Shared/{0}.cshtml");
});


//  Cria��o da Aplica��o Web
var app = builder.Build();



//  ****************************************************************************

// Defini��o de Rotas

app.MapAreaControllerRoute(
        name: "ProdutosArea",
        areaName: "Produtos",
        pattern: "Produtos/{controller}/{action}/{id?}",
        defaults: new { controller = "Cadastro", action = "Index"}
    );

//app.MapControllerRoute(
//        /* name: */ "Areas",
//        /* pattern: */ "{area:exists}/{controller}/{action}/{id?}",
//        /* defaults: */ new { controller = "Home", action = "Index"}
//    );


app.MapControllerRoute(
        /* name: */ "Default",
        /* pattern: */ "{controller}/{action}/{id?}",
        /* defaults: */ new {controller = "Home", action = "Index"}
    );

//  Defaults pode ser utilizado para setar valores padr�o a partir de um objeto anonimo
//  Para mais rotas, chama-se mais vezes o m�todo


//  Utilizado para permitir a utiliza��o de bibliotecas locais no projeto
app.UseStaticFiles();

// Utiliza-se app.Run() ao inv�s de app.UseMvc()
app.Run();
