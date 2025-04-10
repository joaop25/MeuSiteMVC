using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MeuSiteMVC.Controllers
{
    [Route("minha-conta")]
    public class TestesController : Controller
    {
        // GET: TestesController
        public ActionResult Index()
        {
            return View();
        }

        // GET: TestesController/Details/5
        [HttpGet("Detalhes")]
        public ActionResult Details(int id)
        {
            return NotFound();
        }

        // GET: TestesController/Create
        [HttpGet("novo")]
        public ActionResult Create()
        {
            return Content("ok");
        }

        // POST: TestesController/Create
        [HttpPost("novo")]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
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
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: TestesController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
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
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: TestesController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
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
