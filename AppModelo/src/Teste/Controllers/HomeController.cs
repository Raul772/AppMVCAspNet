using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Teste.Extensions;
using Teste.Models;

namespace Teste.Controllers
{
    //  Autenticação do Usuário
    [Authorize]
    public class HomeController : Controller
    {

//      Injeção de Dependências -------------------------------

        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        //      -------------------------------------------------------

        //  Anulando o Authentication 
        [AllowAnonymous]
        public IActionResult Index()
        {
            return View();
        }

        //  Autorização por Roles
        [Authorize(Roles = "Admin")]
        public IActionResult Privacy()
        {
            return View();
        }

        //  Autorização por Claims
        [Authorize(Policy = "PodeExcluir")]
        public IActionResult Secret()
        {
            return View();
        }

        //  Autorização por Claims
        [Authorize(Policy = "PodeLer")]
        public IActionResult Secret2()
        {
            return View("Secret");
        }

        // Autorização Customizada => Teste.Extensions
        [AthorizeClaim("Produtos", "Ler", "Editar")]
        public IActionResult ClaimsCustom()
        {
            return View("Secret");
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}