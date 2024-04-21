using Microsoft.AspNetCore.Mvc;
using MyPortfolio.Models;

namespace MyPortfolio.Controllers
{
    public class PortfolioController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Contact(ContactMessage message)
        {
            if (ModelState.IsValid)
            {
                // Azure Function App'e HTTP isteği gönderme
                // Mail gönderme işlemi burada yapılacak
                // Örnek kod:
                // HttpClient client = new HttpClient();
                // HttpResponseMessage response = await client.PostAsJsonAsync("Azure Function URL", message);
                // response.EnsureSuccessStatusCode(); // Başarılı bir yanıt alınmadığında hata yönetimi
                ViewBag.SuccessMessage = "Your message has been sent successfully!";
            }
            return View("Index");
        }
    }
}
