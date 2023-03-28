
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

//  Adição do MVC no projeto
//  Atribuilçao
builder.Services.AddControllersWithViews();
//  Adição do Razor Pages (equivalente ao services.AddMvc();)
builder.Services.AddRazorPages();

var app = builder.Build();

// Definição de Rotas
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
