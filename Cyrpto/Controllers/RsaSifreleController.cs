using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using System.Text;

namespace Cyrpto.Controllers
{
    public class RsaSifreleController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(string plainText, string base64PublicKey)
        {
            ViewBag.PlainText = plainText;
            ViewBag.PublicKey = base64PublicKey;

            if (string.IsNullOrEmpty(plainText) || string.IsNullOrEmpty(base64PublicKey))
            {
                ViewBag.Error = "Lütfen metin ve public key girin.";
                return View();
            }

            try
            {
                var publicKeyBytes = Convert.FromBase64String(base64PublicKey);
                using var rsa = RSA.Create();
                rsa.ImportRSAPublicKey(publicKeyBytes, out _);

                var dataToEncrypt = Encoding.UTF8.GetBytes(plainText);
                var encryptedBytes = rsa.Encrypt(dataToEncrypt, RSAEncryptionPadding.Pkcs1);
                ViewBag.EncryptedText = Convert.ToBase64String(encryptedBytes);
            }
            catch
            {
                ViewBag.Error = "Şifreleme sırasında hata oluştu. Public key geçerli mi kontrol edin.";
            }

            return View();
        }
    }
}
