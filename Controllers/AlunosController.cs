using MeuSiteMVC.Data;
using MeuSiteMVC.Extensions;
using MeuSiteMVC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Elfie.Diagnostics;
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
        private readonly ILogger<AlunosController> _logger;

        public AlunosController(AppDbContext context
            , IOptions<ApiConfiguration> apiConfiguration,
              ILogger<AlunosController> logger)
        {
            _context = context;
            ApiConfig = apiConfiguration.Value;
            _logger = logger;
        }

        [Route("inicio")]
        [ClaimsAuthorize("Produtos", "VI")]
        public IActionResult Index(string searchString)
        {
            //_logger.LogInformation("Acessou a tela de Alunos");
            //_logger.LogCritical("Critical");
            //_logger.LogWarning("Warning");
            _logger.LogError("Error");

            //throw new Exception("Teste");
            var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            //Usado para utilizar o appsettings.json para buscar as configurações de uma API 
            //Uso o IOptions<ApiConfiguration> apiConfiguration
            var apiDomain = ApiConfig.UserSecret;

            var alunos = from a in _context.Alunos
                         select a;

            if (!string.IsNullOrEmpty(searchString))
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
        public async Task<IActionResult> Create([Bind("Id,Nome, DataNascimento,ImagemUpload, Email, EmailConfirmacao, Avaliacao,Ativo")] Aluno aluno)
        {

            if (ModelState.IsValid)
            {
                var imgPrefixo = Guid.NewGuid() + "_";
                if (!await UploadArquivo(aluno.ImagemUpload, imgPrefixo))
                {
                    return View(aluno);
                }

                aluno.Imagem = imgPrefixo + aluno.ImagemUpload.FileName;

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
        [Route("editar-aluno/{id}")]
        public async Task<IActionResult> Edit(int id)
        {
            var aluno = await _context.Alunos.FindAsync(id);
            return View(aluno);
        }


        [ClaimsAuthorize("Produtos", "ED")]
        [HttpPost("editar-aluno/{id}")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome, DataNascimento,ImagemUpload, Email,EmailConfirmacao, Avaliacao,Ativo")] Aluno aluno)
        {
            if (id != aluno.Id)
            {
                return NotFound();
            }


            var alunoDb = await _context.Alunos.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id);

            if (ModelState.IsValid)
            {
                try
                {
                    aluno.Imagem = alunoDb.Imagem;

                    if (aluno.ImagemUpload != null)
                    {
                        var imgPrefixo = Guid.NewGuid() + "_";
                        if (!await UploadArquivo(aluno.ImagemUpload, imgPrefixo))
                        {
                            return View(aluno);
                        }

                        aluno.Imagem = imgPrefixo + aluno.ImagemUpload.FileName;
                    }

                    _context.Update(aluno);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!alunoExists(aluno.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(aluno);


        }

        [ClaimsAuthorize("Produtos", "ED")]
        [Route("deletar/{id}")]
        // [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            var aluno = _context.Alunos.Find(id);
            if (aluno == null)
            {
                return NotFound();
            }

            _context.Alunos.Remove(aluno);
            _context.SaveChanges();

            TempData["Mensagem"] = "Aluno excluído com sucesso!";
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> UploadArquivo(IFormFile arquivo, string imgPrefixo)
        {
            if (arquivo.Length <= 0) return false;

            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", imgPrefixo + arquivo.FileName);

            if (System.IO.File.Exists(path))
            {
                ModelState.AddModelError(string.Empty, "Já existe um arquivo com este nome!");
                return false;
            }

            using (var stream = new FileStream(path, FileMode.Create))
            {
                await arquivo.CopyToAsync(stream);
            }

            return true;
        }

        private bool alunoExists(int id)
        {
            return (_context.Alunos?.Any(e => e.Id == id)).GetValueOrDefault();
        }

    }
}
