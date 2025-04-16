using MeuSiteMVC.Models;
using Microsoft.AspNetCore.Mvc;

namespace MeuSiteMVC.ViewComponents
{
    public class SaudacaoAlunoViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var aluno = new Aluno() { Nome = "Joao Pedro" };
            return View(aluno);
        }
    }
}
