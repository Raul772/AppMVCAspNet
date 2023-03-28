
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

//  Adi��o do MVC no projeto
//  Atribuil�ao
builder.Services.AddControllersWithViews();
//  Adi��o do Razor Pages (equivalente ao services.AddMvc();)
builder.Services.AddRazorPages();

var app = builder.Build();

// Defini��o de Rotas
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
