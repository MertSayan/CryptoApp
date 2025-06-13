using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;

namespace Cyrpto.Controllers
{
    public class KeyController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Generate()
        {
            using (var rsa = RSA.Create(2048))
            {
                string publicKey = Convert.ToBase64String(rsa.ExportRSAPublicKey());
                string privateKey = Convert.ToBase64String(rsa.ExportRSAPrivateKey());

                ViewBag.PublicKey = publicKey;
                ViewBag.PrivateKey = privateKey;
            }

            return View("Index");
        }
    }
}
