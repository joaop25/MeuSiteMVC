using MeuSiteMVC.Data;
using MeuSiteMVC.Models;
using Microsoft.AspNetCore.Mvc;

namespace MeuSiteMVC.Controllers
{
    public class TesteEFController : Controller
    {
        public AppDbContext Db { get; set; }
        public TesteEFController(AppDbContext db)
        {
            Db = db;
        }

        public IActionResult Index()
        {
            var aluno = new Aluno()
            {
                Nome = "Joao",
                Email = "joao@gmail.com",
                DataNascimento = new DateTime(2000, 03, 25),
                Avaliacao = 5,
                Ativo = true
            };

            //Db.Add(aluno);
            //Db.SaveChanges();

            var result = Db.Alunos.Where(p => p.Nome.Contains("Jo")).FirstOrDefault();
            //result.Email = "joaop@gmail.com";

            //Db.Alunos.Update(result);
            //Db.SaveChanges();

            Db.Alunos.Remove(result);
            Db.SaveChanges();

            return Content("Teste");
        }
    }
}
