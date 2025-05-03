using MeuSiteMVC.Data;
using MeuSiteMVC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace MeuSiteMVC.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("alunos")]
    public class AlunosController : Controller
    {
        private readonly AppDbContext _context;

        public AlunosController(AppDbContext context)
        {
            _context = context;
        }

        [Route("inicio")]
        [AllowAnonymous]
        public IActionResult Index(string searchString)
        {
            var alunos = from a in _context.Alunos
                         select a;

            if (!string.IsNullOrEmpty(searchString))
            {
                alunos = alunos.Where(a => a.Nome.Contains(searchString));
            }

            ViewBag.CurrentFilter = searchString;

            return View(alunos.ToList());
        }

        [Route("criar-aluno")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost("criar")]
       // [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome, DataNascimento, Email, EmailConfirmacao, Avaliacao,Ativo")] Aluno aluno)
        {
            if (ModelState.IsValid)
            {
                _context.Alunos.Add(aluno);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            return View(aluno);

           
        }

        [Route("detalhes")]
        public async Task<IActionResult> Details(int id)
        {
            var aluno = await _context.Alunos.FirstOrDefaultAsync(m => m.Id == id);
            return View(aluno);
        }

        [Route("editar/{id}")]
        public async Task<IActionResult> Edit(int id)
        {
            var aluno = await _context.Alunos.FindAsync(id);
            return View(aluno);
        }

        [HttpPost]
       // [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome, DataNascimento, Email, Avaliacao,Ativo")] Aluno aluno)
        {
            if (id != aluno.Id)
            {
                return NotFound();
            }

            ModelState.Remove("EmailConfirmacao");
            if (ModelState.IsValid)
            {
                _context.Update(aluno);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(aluno);

        }

        [Route("deletar/{id}")]
       // [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var aluno = await _context.Alunos.FindAsync(id);
            
            if(aluno == null) return Content("Aluno não encontrado para ser deletado!!");

            _context.Alunos.Remove(aluno);
            await _context.SaveChangesAsync();

            TempData["Mensagem"] = "Aluno deletado com sucesso!";
            return RedirectToAction(nameof(Index));
        }
    }
}
