using MeuSiteMVC.Data;
using MeuSiteMVC.Extensions;
using MeuSiteMVC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;

namespace MeuSiteMVC.Controllers
{
    [Authorize]
    [Route("alunos")]
    public class AlunosController : Controller
    {
        private readonly AppDbContext _context;
        private readonly ApiConfiguration ApiConfig;

        public AlunosController(AppDbContext context, IOptions<ApiConfiguration> apiConfiguration)
        {
            _context = context;
            ApiConfig = apiConfiguration.Value;
        }

        [Route("inicio")]
        [ClaimsAuthorize("Produtos", "VI")]
        public IActionResult Index(string searchString)
        {
            var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            //Usado para utilizar o appsettings.json para buscar as configurações de uma API 
            //Uso o IOptions<ApiConfiguration> apiConfiguration
            var apiDomain = ApiConfig.UserSecret;

            var alunos = from a in _context.Alunos
                         select a;

            // Adicionada a validação para o 'searchString'
            if (!string.IsNullOrEmpty(searchString) && searchString != "Joao")
            {
                alunos = alunos.Where(a => a.Nome.Contains(searchString));
            }

            ViewBag.CurrentFilter = searchString;

            return View(alunos.ToList());
        }

        [ClaimsAuthorize("Produtos", "AD")]
        [Route("criar-aluno")]
        public IActionResult Create()
        {
            return View();
        }

        [ClaimsAuthorize("Produtos", "AD")]
        [HttpPost("criar")]
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

        [ClaimsAuthorize("Produtos", "VI")]
        [Route("detalhes")]
        public async Task<IActionResult> Details(int id)
        {
            var aluno = await _context.Alunos.FirstOrDefaultAsync(m => m.Id == id);
            return View(aluno);
        }

        [ClaimsAuthorize("Produtos", "ED")]
        [Route("editar/{id}")]
        public async Task<IActionResult> Edit(int id)
        {
            var aluno = await _context.Alunos.FindAsync(id);
            return View(aluno);
        }

        [ClaimsAuthorize("Produtos", "ED")]
        [HttpPost]
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

        [ClaimsAuthorize("Produtos", "ED")]
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