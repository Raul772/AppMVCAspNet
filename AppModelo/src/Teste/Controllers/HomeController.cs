using KissLog;
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

        private readonly IKLogger _logger;

        public HomeController(IKLogger logger)
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
            throw new Exception("Error");
            return View("Secret");
        }

        // Autorização Customizada => Teste.Extensions
        [AthorizeClaim("Produtos", "Ler")]
        public IActionResult ClaimsCustom()
        {
            _logger.Trace("Acessou ClaimsCustom!");
            return View("Secret");
        }

        [Route("/error/{id:length(3,3)}")]
        public IActionResult Error(int id)
        {

            var modelErro = new ErrorViewModel();

            switch (id)
            {
                case 500:
                    modelErro.Mensagem = "Ocorreu um erro! Tente novamente mais tarde ou contate nosso suporte.";
                    modelErro.Titulo = "Ocorreu um erro!";
                    modelErro.ErroCode = id;
                    break;
                case 404:
                    modelErro.Mensagem = "A página que está procurando não existe! <br />Em caso de dúvidas entre com contato com nosso suporte.";
                    modelErro.Titulo = "Ops! Página não encontrada.";
                    modelErro.ErroCode = id;
                    break;
                case 403:
                    modelErro.Mensagem = "Você não tem permissão para fazer isto.";
                    modelErro.Titulo = "Acesso Negado!";
                    modelErro.ErroCode = id;
                    break;
                default:
                    return StatusCode(404);
            }
            return View("Error", modelErro);
        }
    }
}