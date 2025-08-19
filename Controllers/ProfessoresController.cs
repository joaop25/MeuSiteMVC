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
    [Route("professores")]
    public class ProfessoresController : Controller
    {
        private readonly AppDbContext _context;
        private readonly ApiConfiguration ApiConfig;

        public ProfessoresController(AppDbContext context
            ,IOptions<ApiConfiguration> apiConfiguration)
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

            var professores = from p in _context.Professores
                             select p;

            if (!string.IsNullOrEmpty(searchString))
            {
                professores = professores.Where(p => p.Nome.Contains(searchString));
            }

            ViewBag.CurrentFilter = searchString;

            return View(professores.ToList());
        }

        [ClaimsAuthorize("Produtos", "AD")]
        [Route("criar-professor")]
        public IActionResult Create()
        {
            return View();
        }

        [ClaimsAuthorize("Produtos", "AD")]
        [HttpPost("criar")]
        public async Task<IActionResult> Create([Bind("Id,Nome, DataNascimento, Email, EmailConfirmacao, Formacao, AnosExperiencia, Ativo")] Professor professor)
        {
            if (ModelState.IsValid)
            {
                _context.Professores.Add(professor);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            return View(professor);
        }

        [ClaimsAuthorize("Produtos", "VI")]
        [Route("detalhes")]
        public async Task<IActionResult> Details(int id)
        {
            var professor = await _context.Professores.FirstOrDefaultAsync(m => m.Id == id);
            return View(professor);
        }

        [ClaimsAuthorize("Produtos", "ED")]
        [Route("editar/{id}")]
        public async Task<IActionResult> Edit(int id)
        {
            var professor = await _context.Professores.FindAsync(id);
            return View(professor);
        }

        [ClaimsAuthorize("Produtos", "ED")]
        [HttpPost]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome, DataNascimento, Email, Formacao, AnosExperiencia, Ativo")] Professor professor)
        {
            if (id != professor.Id)
            {
                return NotFound();
            }

            ModelState.Remove("EmailConfirmacao");
            if (ModelState.IsValid)
            {
                _context.Update(professor);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(professor);
        }

        [ClaimsAuthorize("Produtos", "ED")]
        [Route("deletar/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var professor = await _context.Professores.FindAsync(id);
            
            if(professor == null) return Content("Professor não encontrado para ser deletado!!");

            _context.Professores.Remove(professor);
            await _context.SaveChangesAsync();

            TempData["Mensagem"] = "Professor deletado com sucesso!";
            return RedirectToAction(nameof(Index));
        }
    }
}