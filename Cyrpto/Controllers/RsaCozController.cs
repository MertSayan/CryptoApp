using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using System.Text;

namespace Cyrpto.Controllers
{
    public class RsaCozController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(string encryptedText, string base64PrivateKey)
        {
            ViewBag.EncryptedText = encryptedText;
            ViewBag.PrivateKey = base64PrivateKey;

            if (string.IsNullOrEmpty(encryptedText) || string.IsNullOrEmpty(base64PrivateKey))
            {
                ViewBag.Error = "Lütfen şifreli metin ve private key girin.";
                return View();
            }

            try
            {
                var privateKeyBytes = Convert.FromBase64String(base64PrivateKey);
                using var rsa = RSA.Create();
                rsa.ImportRSAPrivateKey(privateKeyBytes, out _);

                var encryptedBytes = Convert.FromBase64String(encryptedText);
                var decryptedBytes = rsa.Decrypt(encryptedBytes, RSAEncryptionPadding.Pkcs1);
                ViewBag.DecryptedText = Encoding.UTF8.GetString(decryptedBytes);
            }
            catch
            {
                ViewBag.Error = "Çözme sırasında hata oluştu. Private key geçerli mi veya şifreli metin bozuk mu kontrol edin.";
            }

            return View();
        }
    }
}
