using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using SQLitePCL;
using Trabalho_Pratico_23_24.Data;
using Trabalho_Pratico_23_24.Models;

namespace Trabalho_Pratico_23_24.Controllers
{
    
    public class HabitacaoController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public HabitacaoController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Habitacao
        
        public async Task<IActionResult> Index(string disponivel, int? categoriaId, string ordenarPor)
        {


            ViewData["ListaDeCategorias"] = new SelectList(_context.Set<Categorias>(), "Id", "Nome");
            //Isto é para mostrar habitações a utilizadores não autenticados
            if (!User.Identity.IsAuthenticated || User.IsInRole("Cliente"))
            {
                var applicationDbContext = _context.Habitacao
                    .Where(a => a.Ativo == true)
                    .Include(h => h.Categoria)
                    .Include(s => s.Locadores);

                return View(await applicationDbContext.ToListAsync());
            }

       
            //Utilizador com autenticação

            var toReturn = new List<Habitacao>();

            switch (disponivel)
            {
                case "Ativos":
                    if (User.IsInRole("Gestor"))
                    {
                        var CurrentUser = await _userManager.GetUserAsync(User);


                        var gestorAsData = await _context.Gestores
                                   .FirstOrDefaultAsync(g => g.ApplicationUser == CurrentUser);

                        if (gestorAsData != null)
                        {
                            await _context.Entry(gestorAsData).Reference(g => g.Locador).LoadAsync();
                            toReturn = await _context.Habitacao
                                .Include(h => h.Categoria)
                                .Include(s => s.Locadores)
                                .Where(h => h.Locadores.Any(l => l.Id == gestorAsData.Locador.Id) && h.Ativo == true)
                             .ToListAsync();
                        }
                    }
                    if (User.IsInRole("Funcionario"))
                    {
                        var CurrentUser = await _userManager.GetUserAsync(User);


                        var funcionarioAsData = await _context.Funcionarios
                                   .FirstOrDefaultAsync(f => f.ApplicationUser == CurrentUser);

                        if (funcionarioAsData != null)
                        {
                            await _context.Entry(funcionarioAsData).Reference(g => g.Locador).LoadAsync();
                            toReturn = await _context.Habitacao
                                .Include(h => h.Categoria)
                                .Include(s => s.Locadores)
                                .Where(h => h.Locadores.Any(l => l.Id == funcionarioAsData.Locador.Id) && h.Ativo == true)
                             .ToListAsync();
                        }
                    }

                    if (User.IsInRole("Cliente"))
                    {
                        toReturn = await _context.Habitacao
                        .Include(h => h.Categoria)
                        .Include(s => s.Locadores)
                        .Where(h => h.Ativo == true).ToListAsync();

                    }

                    break;

                case "Inativos":
                    if (User.IsInRole("Gestor"))
                    {
                        var CurrentUser = await _userManager.GetUserAsync(User);


                        var gestorAsData = await _context.Gestores
                                   .FirstOrDefaultAsync(g => g.ApplicationUser == CurrentUser);

                        if (gestorAsData != null)
                        {
                            await _context.Entry(gestorAsData).Reference(g => g.Locador).LoadAsync();
                            toReturn = await _context.Habitacao
                                .Include(h => h.Categoria)
                                .Include(s => s.Locadores)
                                .Where(h => h.Locadores.Any(l => l.Id == gestorAsData.Locador.Id) && h.Ativo == false)
                             .ToListAsync();
                        }
                    }
                    if (User.IsInRole("Funcionario"))
                    {
                        var CurrentUser = await _userManager.GetUserAsync(User);


                        var funcionarioAsData = await _context.Funcionarios
                                   .FirstOrDefaultAsync(f => f.ApplicationUser == CurrentUser);

                        if (funcionarioAsData != null)
                        {
                            await _context.Entry(funcionarioAsData).Reference(g => g.Locador).LoadAsync();
                            toReturn = await _context.Habitacao
                                .Include(h => h.Categoria)
                                .Include(s => s.Locadores)
                                .Where(h => h.Locadores.Any(l => l.Id == funcionarioAsData.Locador.Id) && h.Ativo == false)
                             .ToListAsync();
                        }
                    }

                    if (User.IsInRole("Cliente"))
                    {
                        toReturn = await _context.Habitacao
                        .Include(h => h.Categoria)
                        .Include(s => s.Locadores)
                        .Where(h => h.Ativo == false).ToListAsync();

                    }

                    break;

                default:
                    if (User.IsInRole("Gestor"))
                    {
                        var CurrentUser = await _userManager.GetUserAsync(User);


                        var gestorAsData = await _context.Gestores
                                   .FirstOrDefaultAsync(g => g.ApplicationUser == CurrentUser);

                        if (gestorAsData != null)
                        {
                            await _context.Entry(gestorAsData).Reference(g => g.Locador).LoadAsync();
                            toReturn = await _context.Habitacao
                                .Include(h => h.Categoria)
                                .Include(s => s.Locadores)
                                .Where(h => h.Locadores.Any(l => l.Id == gestorAsData.Locador.Id))
                             .ToListAsync();
                        }
                    }
                    if (User.IsInRole("Funcionario"))
                    {
                        var CurrentUser = await _userManager.GetUserAsync(User);


                        var funcionarioAsData = await _context.Funcionarios
                                   .FirstOrDefaultAsync(f => f.ApplicationUser == CurrentUser);

                        if (funcionarioAsData != null)
                        {
                            await _context.Entry(funcionarioAsData).Reference(g => g.Locador).LoadAsync();
                            toReturn = await _context.Habitacao
                                .Include(h => h.Categoria)
                                .Include(s => s.Locadores)
                                .Where(h => h.Locadores.Any(l => l.Id == funcionarioAsData.Locador.Id))
                             .ToListAsync();
                        }
                    }

                    if (User.IsInRole("Cliente"))
                    {
                        toReturn = await _context.Habitacao
                        .Include(h => h.Categoria)
                        .Include(s => s.Locadores)
                        .ToListAsync();

                    }
                    break;


            }



            if (categoriaId.HasValue && categoriaId.Value > 0)
            {
                toReturn = toReturn.Where(h => h.CategoriaId == categoriaId.Value).ToList();
            }


            if (!string.IsNullOrEmpty(ordenarPor))
            {
                switch (ordenarPor)
                {
                    case "precoAsc":
                        toReturn = toReturn.OrderBy(h => h.CustoArrendamento).ToList();
                        break;

                    case "precoDesc":
                        toReturn = toReturn.OrderByDescending(h => h.CustoArrendamento).ToList();
                        break;
                }
            }


            return View(toReturn);

        }







        //POST:
        [HttpPost]
        [ValidateAntiForgeryToken]

        //Index da Pesquisa
        public async Task<IActionResult> Index(string localizacao, string tipoHabitacao, DateTime dataInicio, DateTime dataFim, int periodoMinimo, int CategoriaId, int LocadorId, bool ordenarPorPreco, bool ordenarPorAvaliacao, string direcao)
        {

            ViewBag.Localizacao = localizacao;
            ViewBag.TipoHabitacao = tipoHabitacao;
            ViewBag.DataInicio = dataInicio.ToShortDateString(); //ToShortDateString para apenas mostrar a data sem a hora
            ViewBag.DataFim = dataFim.ToShortDateString();
            ViewBag.PeriodoMinimo = periodoMinimo;

            IQueryable<Habitacao> habitacoes = _context.Habitacao;
            IQueryable<Habitacao> result;


            if (CategoriaId > 0)
            {              
               
                if(LocadorId > 0)
                {
                    result = from s in habitacoes
                         .Include(s => s.Locadores)
                             where s.Localizacao.Contains(localizacao) &&
                                   s.Tipo.Contains(tipoHabitacao) &&
                                   s.DataInicioContrato >= dataInicio &&
                                   s.CategoriaId == CategoriaId &&
                                   s.DataFimContrato <= dataFim &&
                                   s.Ativo == true &&
                                   s.Locadores.Any(l => l.Id == LocadorId) &&
                                   s.PeriodoMinimoArrendamento >= periodoMinimo
                             select s;
                                        
                }
                else
                {
                    // linq query method
                    result = from s in habitacoes
                             .Include(s => s.Locadores)
                             where s.Localizacao.Contains(localizacao) &&
                                   s.Tipo.Contains(tipoHabitacao) &&
                                   s.DataInicioContrato >= dataInicio &&
                                   s.CategoriaId == CategoriaId &&
                                   s.DataFimContrato <= dataFim &&
                                   s.Ativo == true &&
                                   s.PeriodoMinimoArrendamento >= periodoMinimo
                             select s;
                }
                


            }
            else
            {
                // linq extension method
                // Caso o utilizador não tenha introduzido nenhuma filtro para categoria,
                //procura-se na bd sem esse parametro

                if (LocadorId > 0)
                {
                    result = habitacoes
                    .Include(s => s.Locadores)
                    .Where(s => s.Localizacao.Contains(localizacao) &&
                                               s.Tipo.Contains(tipoHabitacao) &&
                                               s.DataInicioContrato >= dataInicio &&
                                               s.DataFimContrato <= dataFim &&
                                               s.Ativo == true &&
                                               s.Locadores.Any(l => l.Id == LocadorId) &&
                                               s.PeriodoMinimoArrendamento >= periodoMinimo);
                }
                else
                {
                    result = habitacoes
                    .Include(s => s.Locadores)
                    .Where(s => s.Localizacao.Contains(localizacao) &&
                               s.Tipo.Contains(tipoHabitacao) &&
                               s.DataInicioContrato >= dataInicio &&
                               s.DataFimContrato <= dataFim &&
                               s.Ativo == true &&
                               s.PeriodoMinimoArrendamento >= periodoMinimo);
                }
                
            }

            // Aplicando ordenação por preço
            if (ordenarPorPreco)
            {
                if (direcao == "desc")
                {
                    result = result.OrderByDescending(s => s.CustoArrendamento);
                }
                else
                {
                    result = result.OrderBy(s => s.CustoArrendamento);
                }
            }

            // Aplicando ordenação por avaliação
            if (ordenarPorAvaliacao)
            {
                if (direcao == "desc")
                {
                    result = result.OrderByDescending(s => s.AvaliacaoLocador);
                }
                else
                {
                    result = result.OrderBy(s => s.AvaliacaoLocador);
                    
                }
            }


            var resultList = await result.ToListAsync();


            if (resultList.Count == 0)
                return View("ErroPesquisa");

            return View("PesquisaResult", resultList);

        }


        // GET: Habitacao/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Habitacao == null)
            {
                return NotFound();
            }

            var habitacao = await _context.Habitacao
                .Include(h => h.Categoria)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (habitacao == null)
            {
                return NotFound();
            }

            return View(habitacao);
        }

        // GET: Habitacao/Create
        
        public IActionResult Create()
        {
            ViewData["CategoriaId"] = new SelectList(_context.Set<Categorias>(), "Id", "Nome");
            return View();
        }

        // POST: Habitacao/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Localizacao,Tipo,CustoArrendamento,DataInicioContrato,DataFimContrato,PeriodoMinimoArrendamento,PeriodoMaximoArrendamento,AvaliacaoLocador,CategoriaId")] Habitacao habitacao)
        {
            if (ModelState.IsValid)
            {

                if(User.IsInRole("Gestor"))
                {
                    var CurrentUser = await _userManager.GetUserAsync(User);


                    var gestorAsData = await _context.Gestores
                               .FirstOrDefaultAsync(g => g.ApplicationUser == CurrentUser);

                    if (gestorAsData != null)
                    {
                        await _context.Entry(gestorAsData).Reference(g => g.Locador).LoadAsync();
                        habitacao.Locadores.Add(gestorAsData.Locador);
                    }
                }
                if (User.IsInRole("Funcionario"))
                {
                    var CurrentUser = await _userManager.GetUserAsync(User);


                    var funcionarioAsData = await _context.Funcionarios
                               .FirstOrDefaultAsync(f => f.ApplicationUser == CurrentUser);

                    if (funcionarioAsData != null)
                    {
                        await _context.Entry(funcionarioAsData).Reference(f => f.Locador).LoadAsync();
                        habitacao.Locadores.Add(funcionarioAsData.Locador);
                    }
                }
                habitacao.Ativo = true;
                _context.Add(habitacao);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
			
			ViewData["CategoriaId"] = new SelectList(_context.Categorias, "Id", "Nome", habitacao.CategoriaId);
			return View(habitacao);
        }

        // GET: Habitacao/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Habitacao == null)
            {
                return NotFound();
            }

            var habitacao = await _context.Habitacao.FindAsync(id);
            if (habitacao == null)
            {
                return NotFound();
            }
            ViewData["CategoriaId"] = new SelectList(_context.Set<Categorias>(), "Id", "Nome", habitacao.CategoriaId);
            return View(habitacao);
        }

        // POST: Habitacao/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Localizacao,Tipo,CustoArrendamento,DataInicioContrato,DataFimContrato,PeriodoMinimoArrendamento,PeriodoMaximoArrendamento,AvaliacaoLocador,CategoriaId,Ativo")] Habitacao habitacao)
        {
            if (id != habitacao.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(habitacao);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HabitacaoExists(habitacao.Id))
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
            ViewData["CategoriaId"] = new SelectList(_context.Set<Categorias>(), "Id", "", habitacao.CategoriaId);
            return View(habitacao);
        }

        // GET: Habitacao/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Habitacao == null)
            {
                return NotFound();
            }

            var habitacao = await _context.Habitacao
                .Include(h => h.Categoria)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (habitacao == null)
            {
                return NotFound();
            }

            return View(habitacao);
        }

        // POST: Habitacao/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Habitacao == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Habitacao'  is null.");
            }
            var habitacao = await _context.Habitacao.FindAsync(id);
            if (habitacao != null)
            {
                _context.Habitacao.Remove(habitacao);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HabitacaoExists(int id)
        {
          return (_context.Habitacao?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        // GET: Habitacao
        public async Task<IActionResult> ListToAssociate()
        {
            var toReturn = new List<Habitacao>();
            if (User.IsInRole("Gestor"))
            {
                var CurrentUser = await _userManager.GetUserAsync(User);


                var gestorAsData = await _context.Gestores
                           .FirstOrDefaultAsync(g => g.ApplicationUser == CurrentUser);

                if (gestorAsData != null)
                {
                    await _context.Entry(gestorAsData).Reference(g => g.Locador).LoadAsync();
                    toReturn = await _context.Habitacao
                        .Include(s => s.Categoria)
                        .Where(h => !h.Locadores.Any(l => l.Id == gestorAsData.Locador.Id))
                     .ToListAsync();
                }
            }
            if (User.IsInRole("Funcionario"))
            {
                var CurrentUser = await _userManager.GetUserAsync(User);


                var FuncionarioAsData = await _context.Funcionarios
                           .FirstOrDefaultAsync(f => f.ApplicationUser == CurrentUser);

                if (FuncionarioAsData != null)
                {
                    await _context.Entry(FuncionarioAsData).Reference(g => g.Locador).LoadAsync();
                    toReturn = await _context.Habitacao
                        .Include(s => s.Categoria)
                        .Where(h => !h.Locadores.Any(l => l.Id == FuncionarioAsData.Locador.Id))
                     .ToListAsync();
                }
            }
            return View(toReturn);
            //var applicationDbContext = _context.Habitacao.Include(h => h.Categoria);
            //return View(await applicationDbContext.ToListAsync());
        }

        public async Task<IActionResult> Associate(int id)
        {
            var habitacao = await _context.Habitacao.FindAsync(id);
            if(habitacao != null)
            {
                if (User.IsInRole("Gestor"))
                {
                    var CurrentUser = await _userManager.GetUserAsync(User);


                    var gestorAsData = await _context.Gestores
                               .FirstOrDefaultAsync(g => g.ApplicationUser == CurrentUser);

                    if (gestorAsData != null)
                    {
                        await _context.Entry(gestorAsData).Reference(g => g.Locador).LoadAsync();
                        habitacao.Locadores.Add(gestorAsData.Locador);
                    }
                }
                if (User.IsInRole("Funcionario"))
                {
                    var CurrentUser = await _userManager.GetUserAsync(User);


                    var funcionarioAsData = await _context.Funcionarios
                               .FirstOrDefaultAsync(f => f.ApplicationUser == CurrentUser);

                    if (funcionarioAsData != null)
                    {
                        await _context.Entry(funcionarioAsData).Reference(f => f.Locador).LoadAsync();
                        habitacao.Locadores.Add(funcionarioAsData.Locador);
                    }
                }
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }

    }
}
