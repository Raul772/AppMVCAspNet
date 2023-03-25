using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

builder.Services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Latest);


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}

app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

//  DEFINI��O DE ROTAS
//  PARA MAIS ROTAS, CHAMA-SE NOVAMENTE O M�TODO
app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action}/{id?}",

    //  DEFAULTS CONT�M OS VALORES PADR�O PARA PAR�METROS
    defaults: new {controller = "home", action = "Index"}
    );



app.UseMvc();
