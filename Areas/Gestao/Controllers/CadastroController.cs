using Microsoft.AspNetCore.Mvc;

namespace MeuSiteMVC.Areas.Gestao.Controllers
{
    [Area("Gestao")]
    public class CadastroController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
