using e2_client.Models;
using e2_client.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace e2_client.Controllers
{
    public class MonAnController : Controller
    {
        string uri = "https://localhost:7122/api/MonAns";
        HttpClient client = new HttpClient();
        public async Task<IActionResult> Index()
        {
            client.BaseAddress = new Uri(uri);
            string data = await client.GetStringAsync(uri);
            List<MonAn> articles = JsonConvert.DeserializeObject<List<MonAn>>(data);
            return View(articles);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(MonAnViewModel monAnView)
        {
            if (monAnView == null || monAnView.CongThucViewModels == null || !monAnView.CongThucViewModels.Any())
            {
                ModelState.AddModelError(string.Empty, "Thông tin món ăn hoặc công thức không hợp lệ.");
                return View(monAnView);
            }

            client.BaseAddress = new Uri(uri);
            var response = await client.PostAsJsonAsync("", monAnView);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }

            var errorContent = await response.Content.ReadAsStringAsync();
            ModelState.AddModelError(string.Empty, errorContent);
            return View(monAnView);
        }
    }
}
