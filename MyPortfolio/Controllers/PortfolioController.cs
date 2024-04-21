using Microsoft.AspNetCore.Mvc;
using MyPortfolio.Models;
using System.Net.Http;

namespace MyPortfolio.Controllers
{
    public class PortfolioController : Controller
    {
        private readonly HttpClient _httpClient;

        public PortfolioController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Contact(ContactMessage message)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    HttpResponseMessage response = await _httpClient.PostAsJsonAsync("Azure_Function_URL", message);
                    if (response.IsSuccessStatusCode)
                    {
                        TempData["SuccessMessage"] = "Your message has been sent successfully!";
                    }
                    else
                    {
                        TempData["ErrorMessage"] = "Failed to send message. Please try again later.";
                    }
                }
                catch (Exception ex)
                {
                    TempData["ErrorMessage"] = $"An error occurred: {ex.Message}";
                }
            }
            else
            {
                TempData["ErrorMessage"] = "Please provide valid information in all fields.";
            }

            return RedirectToAction("Index");
        }
    }
}
}
