using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MeuSiteMVC.Models;
using Microsoft.Extensions.Localization;
using Microsoft.AspNetCore.Localization;

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

    public IActionResult Index()
    {
        ViewData["Message"] = _localizer["Seja bem vindo!"];
        ViewData["Horario"] = DateTime.Now;

        if (Request.Cookies.TryGetValue("MeuCookie", out string? cookieValue))
        {
            ViewData["MeuCookie"] = cookieValue;
        }
        return View();
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

    [HttpPost]
    public IActionResult SetLanguage(string culture, string returnUrl)
    {
        Response.Cookies.Append(
            CookieRequestCultureProvider.DefaultCookieName,
            CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
            new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
        );

        return LocalRedirect(returnUrl);
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


    [Route("teste")]
    public IActionResult Teste()
    {
        throw new Exception("ALGO ERRADO NÃO ESTAVA CERTO!");

        return View("Index");
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
}
