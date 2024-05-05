using Microsoft.AspNetCore.Mvc;
using MyPortfolio.Models;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace MyPortfolio.Controllers
{
    public class PortfolioController : Controller
    {
        private readonly HttpClient _httpClient;

        public PortfolioController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
            _httpClient.BaseAddress = new Uri("http://localhost:7101/");
        }

        public IActionResult Index()
        {
            return View();
        }

        //[HttpGet]
        //public IActionResult Contact()
        //{
        //    return View();
        //}

        [HttpPost]
        public async Task<IActionResult> Contact(ContactMessage message)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    
                    HttpResponseMessage response = await _httpClient.PostAsJsonAsync("api/SendEmail/", message);
                    if (response.IsSuccessStatusCode)
                    {
                        ViewBag.SuccessMessage = "Your message has been sent successfully!";
                    }
                    else
                    {
                        ViewBag.ErrorMessage = "Failed to send message. Please try again later.";
                    }
                }
                catch (Exception ex)
                {
                    ViewBag.ErrorMessage = $"An error occurred: {ex.Message}";
                }
            }
            else
            {
                ViewBag.ErrorMessage = "Please provide valid information in all fields.";
            }

            return RedirectToAction("Index");
        }
    }
}
