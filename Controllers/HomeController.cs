using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MeuSiteMVC.Models;

namespace MeuSiteMVC.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return RedirectToAction("Index", "Alunos");
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
        throw new Exception("ALGO ERRADO N�O ESTAVA CERTO!");

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
            modelErro.Mensagem = "A p�gina que est� procurando n�o existe! <br />Em caso de d�vidas entre em contato com nosso suporte";
            modelErro.Titulo = "Ops! P�gina n�o encontrada.";
            modelErro.ErroCode = id;
        }
        else if (id == 403)
        {
            modelErro.Mensagem = "Voc� n�o tem permiss�o para fazer isto.";
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
