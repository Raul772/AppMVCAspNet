
using Microsoft.AspNetCore.Mvc;
using RL.ModelApp.Controllers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews(options => options.EnableEndpointRouting = false);
builder.Services.AddRazorPages();

var app = builder.Build();

// Definição de Rotas
app.MapControllerRoute(
        name: "Default",
        pattern: "{controller}/{action}/{id?}",
        defaults: new {controller = "Home", action = "Index"}
    );

app.Run();
