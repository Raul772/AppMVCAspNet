
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.EntityFrameworkCore;
using RL.ModelApp.Data;


//  Criação do objeto Builder
var builder = WebApplication.CreateBuilder(args);


//  Setar a Connection String do Entity Framework para comunicação com Banco de Dados
//  A Conecction String está contida no arquivo appsettings.json e automaticamente atribuída ao objeto Builder
//  O atributo Configuration do Builder possui o método GetConnectionString
builder.Services.AddDbContext<MeuDbContext>(options => 
        options.UseSqlServer(builder.Configuration.GetConnectionString("MeuDbContext")));


//  Adição do MVC no projeto
//  Atribuilçao
builder.Services.AddControllersWithViews();
//  Adição do Razor Pages (equivalente ao services.AddMvc();)
builder.Services.AddRazorPages();


// Mudança de nome da pasta Areas para Modules
builder.Services.Configure<RazorViewEngineOptions>( options =>
{
    options.AreaViewLocationFormats.Clear();
    options.AreaViewLocationFormats.Add("/Modules/{2}/Views/{1}/{0}.cshtml");
    options.AreaViewLocationFormats.Add("/Modules/{2}/Views/Shared/{0}.cshtml");
    options.AreaViewLocationFormats.Add("/Views/Shared/{0}.cshtml");
});


//  Criação da Aplicação Web
var app = builder.Build();



//  ****************************************************************************

// Definição de Rotas

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

//  Defaults pode ser utilizado para setar valores padrão a partir de um objeto anonimo
//  Para mais rotas, chama-se mais vezes o método


//  Utilizado para permitir a utilização de bibliotecas locais no projeto
app.UseStaticFiles();

// Utiliza-se app.Run() ao invés de app.UseMvc()
app.Run();
