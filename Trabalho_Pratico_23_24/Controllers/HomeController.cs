using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using Trabalho_Pratico_23_24.Data;
using Trabalho_Pratico_23_24.Models;

namespace Trabalho_Pratico_23_24.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
		private readonly ApplicationDbContext _context;

		public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
		{
            _logger = logger;
			_context = context;
		}
		public async Task<IActionResult> Index()
		{
			var habitacoes = await _context.Habitacao.ToListAsync();
			return View(habitacoes);
		}

		public IActionResult Privacy()
		{
			return View();
		}
		public IActionResult SobreNos()
		{
			return View();
		}

		public IActionResult Pesquisar()
		{
            ViewData["ListaCategorias"] = new SelectList(_context.Categorias.OrderBy(c => c.Nome).ToList(), "Id", "Nome");
            ViewData["ListaLocadores"] = new SelectList(_context.Locadores.OrderBy(c => c.Nome).ToList(), "Id", "Nome");

            return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}