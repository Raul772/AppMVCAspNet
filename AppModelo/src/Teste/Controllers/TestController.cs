using KissLog;
using Microsoft.AspNetCore.Mvc;


namespace Teste.Controllers
{
    public class TestController : Controller
    {
        // Utilização da Interface IKLogger para o KissLogger
        public readonly IKLogger _logger;

        public TestController(IKLogger logger) {
            _logger = logger;
        }


        public IActionResult Index()
        {
            _logger.Trace("Usuário acessou Teste");

            return View();
        }
    }
}
