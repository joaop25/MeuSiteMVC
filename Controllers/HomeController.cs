using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MeuSiteMVC.Models;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Localization;

namespace MeuSiteMVC.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IStringLocalizer<HomeController> _localizer;

    public HomeController(ILogger<HomeController> logger,
        IStringLocalizer<HomeController> localizer)
    {
        _logger = logger;
        _localizer = localizer;
    }


    [ResponseCache(Duration = 300, Location = ResponseCacheLocation.Any, NoStore = true)]
    public IActionResult Index()
    {
        ViewData["Message"] = _localizer["Seja bem vindo!"];

        if (Request.Cookies.TryGetValue("MeuCookie", out string? cookieValue))
        {
            ViewData["MeuCookie"] = cookieValue;
        }

        return View();
        //return RedirectToAction("Index", "Alunos");
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [HttpGet("home2/joao")]
    public IActionResult Detalhes(int id)
    {
        return Index();
    }

    [Route("teste")]
    public IActionResult Teste()
    {
        throw new Exception("ALGO ERRADO NÃO ESTAVA CERTO!");

        return View("Index");
    }

    [HttpPost]
    public IActionResult SetLanguage(string culture, string returnUrl)
    {
        if (!string.IsNullOrEmpty(culture))
        {
            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(
                    new RequestCulture(culture)),
                new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
            );
        }

        if (Url.IsLocalUrl(returnUrl))
        {
            return LocalRedirect(returnUrl);
        }
        else
        {
            return RedirectToAction("Index", "Home");
        }
    }

    [Route("erro/{id:length(3,3)}")]
    public IActionResult Errors(int id)
    {
        var modelErro = new ErrorViewModel();

        if (id == 500)
        {
            modelErro.Mensagem = "Ocorreu um erro! Tente novamente mais tarde ou contate nosso suporte.";
            modelErro.Titulo = "Ocorreu um erro!";
            modelErro.ErroCode = id;
        }
        else if (id == 404)
        {
            modelErro.Mensagem = "A página que está procurando não existe! <br />Em caso de dúvidas entre em contato com nosso suporte";
            modelErro.Titulo = "Ops! Página não encontrada.";
            modelErro.ErroCode = id;
        }
        else if (id == 403)
        {
            modelErro.Mensagem = "Você não tem permissão para fazer isto.";
            modelErro.Titulo = "Acesso Negado";
            modelErro.ErroCode = id;
        }
        else
        {
            return StatusCode(500);
        }

        return View("Error", modelErro);
    }

    [Route("cookies")]
    public IActionResult Cookie()
    {
        var cookieOptions = new CookieOptions
        {
            Expires = DateTime.Now.AddHours(1)
        };

        Response.Cookies.Append("MeuCookie", "Dados do Cookie", cookieOptions);

        return View();
    }
}
