using Microsoft.AspNetCore.Mvc;

namespace Cyrpto.Controllers
{
    public class RsaController : Controller
    {
        private readonly ILogger<RsaController> _logger;

        public RsaController(ILogger<RsaController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

      
    }
}
