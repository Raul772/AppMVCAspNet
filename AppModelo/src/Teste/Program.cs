using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Teste.Areas.Identity.Data;


var builder = WebApplication.CreateBuilder(args);


//  Gerado Por Identity Scaffolding ----------------------------------

var connectionString = builder.Configuration.GetConnectionString("TesteContextConnection") ?? throw new InvalidOperationException("Connection string 'TesteContextConnection' not found.");

builder.Services.AddDbContext<TesteContext>(options => options
    .UseSqlServer(connectionString));

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<IdentityRole>() //    Para trabalhar com Roles do ASP.NET Identity
    .AddEntityFrameworkStores<TesteContext>();

// --------------------------------------------------------------------



// Add services to the container.
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

//  Utilização de ASP.NET Identity para autenticação e autorização.
app.UseAuthentication();
app.UseAuthorization();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

//  Mapear as rotas das Razor Pages
app.MapRazorPages();

app.Run();
