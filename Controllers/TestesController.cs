using MeuSiteMVC.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MeuSiteMVC.Controllers
{
    [Route("minha-conta")]
    public class TestesController : Controller
    {
        // GET: TestesController
        public IActionResult Index()
        {
            return View();
        }

        // GET: TestesController/Details/5
        [HttpGet("Detalhes")]
        public ICollection<Carrinho> Details([FromBody] ICollection<Carrinho> carrinho)
        {
            return carrinho;
        }

        // GET: TestesController/Create
        [HttpGet("novo")]
        public IActionResult Create()
        {
            return Content("ok");
        }

        // POST: TestesController/Create
        [HttpPost("novo")]
        [ValidateAntiForgeryToken]
        public IActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: TestesController/Edit/5
        public IActionResult Edit(int id)
        {
            return View();
        }

        // POST: TestesController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: TestesController/Delete/5
        public IActionResult Delete(int id)
        {
            return View();
        }

        // POST: TestesController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
