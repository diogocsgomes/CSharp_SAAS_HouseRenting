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
using Trabalho_Pratico_23_24.Data;
using Trabalho_Pratico_23_24.Data.Migrations;
using Trabalho_Pratico_23_24.Models;

namespace Trabalho_Pratico_23_24.Controllers
{
    [Authorize(Roles ="Gestor,Funcionario,Cliente")]
    public class ArrendamentosController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private static int Id_habitacao; //VARIAVEL GLOBAL EXCECIONAL

        public ArrendamentosController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Arrendamentos
        public async Task<IActionResult> Index(string filtro, int categoriaId, string clienteId, int habitacaoId, DateTime dataInicio, DateTime dataFim)
        {
            ViewData["ListaDeCategorias"] = new SelectList(_context.Set<Categorias>(), "Id", "Nome");
            ViewData["ListaDeHabitacoes"] = new SelectList(_context.Set<Habitacao>(), "Id", "Id");
            var clientes = _userManager.GetUsersInRoleAsync("Cliente").Result;
            ViewBag.ListaDeClientes = new SelectList(clientes, "Id", "UserName");
            IQueryable<Arrendamento> arrendamentos = _context.Arrendamentos;
            IQueryable<Arrendamento> result = _context.Arrendamentos
            .Include(s => s.Cliente)
            .Include(s => s.Imovel)
                .ThenInclude(i => i.Categoria)
            .Include(s => s.Imovel)
                .ThenInclude(i => i.Locadores);

            var user = _userManager.GetUserAsync(User).Result;



            //else if (User.IsInRole("Funcionario")) //Ent é porque é funcionário
            //{

            //    var userAsData = _context.Funcionarios.FirstOrDefaultAsync(g => g.ApplicationUser == user).Result;
            //    await _context.Entry(userAsData).Reference(g => g.Locador).LoadAsync();



            //    await _context.Entry(userAsData).Reference(g => g.Locador).LoadAsync();
            //    return View(await _context.Arrendamentos
            //        .Include(a => a.Cliente)
            //                .Include(a => a.Imovel)
            //                    .ThenInclude(i => i.Categoria)
            //                .Include(a => a.Imovel)
            //                    .ThenInclude(i => i.Locadores)
            //        .Where(A => A.Imovel.Locadores.Any(l => l.Id == userAsData.Locador.Id))
            //        .ToListAsync());
            //}
            //else
            //{
            //    //Aqui é pq o user é cliente pois não tem acesso aos filtros
            //    result = arrendamentos
            //    .Include(s => s.Cliente)
            //    .Include(s => s.Imovel)
            //    .Include(s => s.Imovel.Locadores)
            //    .Include(s => s.Imovel.Categoria)
            //    .Where(a => a.Cliente.Id == user.Id);
            //}



            ////////////////////////////////////////////////////////////////////////////////////////////////
            //if (User.IsInRole("Gestor"))
            //{
            //    var userAsData = _context.Gestores.FirstOrDefaultAsync(g => g.ApplicationUser == user).Result;
            //    await _context.Entry(userAsData).Reference(g => g.Locador).LoadAsync();

                //// Verificar se a categoriaId foi fornecida
                //if (categoriaId > 0)
                //{
                //    result = arrendamentos
                //       .Include(s => s.Cliente)
                //       .Include(s => s.Imovel)
                //       .Include(s => s.Imovel.Locadores)
                //       .Include(s => s.Imovel.Categoria)
                //       .Where(A => A.Imovel.Locadores.Any(l => l.Id == userAsData.Locador.Id) && A.Imovel.CategoriaId == categoriaId);

                //}

                //// Verificar se o clienteId foi fornecido
                //if (!string.IsNullOrEmpty(clienteId))
                //{
                //    result = arrendamentos
                //      .Include(s => s.Cliente)
                //      .Include(s => s.Imovel)
                //      .Include(s => s.Imovel.Locadores)
                //      .Include(s => s.Imovel.Categoria)
                //      .Where(A => A.Imovel.Locadores.Any(l => l.Id == userAsData.Locador.Id) && A.Cliente.Id == clienteId);
                //}

                //// Verificar se o habitacaoId foi fornecido
                //if (habitacaoId > 0)
                //{
                //    result = arrendamentos
                //       .Include(s => s.Cliente)
                //       .Include(s => s.Imovel)
                //       .Include(s => s.Imovel.Locadores)
                //       .Include(s => s.Imovel.Categoria)
                //       .Where(A => A.Imovel.Locadores.Any(l => l.Id == userAsData.Locador.Id) && A.Imovel.Id == habitacaoId);
                //}

                //// Verificar se a dataInicio foi fornecida
                //if (dataInicio != DateTime.MinValue)
                //{
                //    result = arrendamentos
                //      .Include(s => s.Cliente)
                //      .Include(s => s.Imovel)
                //      .Include(s => s.Imovel.Locadores)
                //      .Include(s => s.Imovel.Categoria)
                //      .Where(A => A.Imovel.Locadores.Any(l => l.Id == userAsData.Locador.Id) && A.DatraEntrada == dataInicio);
                //}

                //// Verificar se a dataFim foi fornecida
                //if (dataFim != DateTime.MinValue)
                //{
                //    result = arrendamentos
                //     .Include(s => s.Cliente)
                //     .Include(s => s.Imovel)
                //     .Include(s => s.Imovel.Locadores)
                //     .Include(s => s.Imovel.Categoria)
                //     .Where(A => A.Imovel.Locadores.Any(l => l.Id == userAsData.Locador.Id) && A.DatraSaida == dataFim);
                //}

                /////////////////////////////////////////////

                //if (userAsData != null)
                //{
                //    await _context.Entry(userAsData).Reference(g => g.Locador).LoadAsync();

                //    if (categoriaId > 0)
                //    {
                //        result = result.Where(A => A.Imovel.Locadores.Any(l => l.Id == userAsData.Locador.Id) && A.Imovel.CategoriaId == categoriaId);
                //    }

                //    if (!string.IsNullOrEmpty(clienteId))
                //    {
                //        result = result.Where(A => A.Imovel.Locadores.Any(l => l.Id == userAsData.Locador.Id) && A.Cliente.Id == clienteId);
                //    }

                //    if (habitacaoId > 0)
                //    {
                //        result = result.Where(A => A.Imovel.Locadores.Any(l => l.Id == userAsData.Locador.Id) && A.Imovel.Id == habitacaoId);
                //    }

                //    if (dataInicio != DateTime.MinValue)
                //    {
                //        result = result.Where(A => A.Imovel.Locadores.Any(l => l.Id == userAsData.Locador.Id) && A.DatraEntrada == dataInicio);
                //    }

                //    if (dataFim != DateTime.MinValue)
                //    {
                //        result = result.Where(A => A.Imovel.Locadores.Any(l => l.Id == userAsData.Locador.Id) && A.DatraSaida == dataFim);
                //    }
                //}

                //var resultList = await result.ToListAsync();
                //return View(resultList);


                //result = arrendamentos
                //   .Include(s => s.Cliente)
                //   .Include(s => s.Imovel)
                //   .Include(s => s.Imovel.Locadores)
                //   .Include(s => s.Imovel.Categoria)
                //   .Where(A => A.Imovel.Locadores.Any(l => l.Id == userAsData.Locador.Id));



            



            //////////////////////////////////////////////////////////////////////////////////////////////////////////
            switch (filtro)
            {
                case "PorConfirmar":
                    if (User.IsInRole("Gestor"))
                    {
                        var userAsData = _context.Gestores.FirstOrDefaultAsync(g => g.ApplicationUser == user).Result;
                        await _context.Entry(userAsData).Reference(g => g.Locador).LoadAsync();



                        if (userAsData != null)
                        {
                            await _context.Entry(userAsData).Reference(g => g.Locador).LoadAsync();

                            result = result
                                .Where(A => A.Imovel.Locadores.Any(l => l.Id == userAsData.Locador.Id) && A.PorConfirmar == true);

                        }

                        //return View(await _context.Arrendamentos
                        //            .Include(a => a.Cliente)
                        //            .Include(a => a.Imovel)
                        //                .ThenInclude(i => i.Categoria)
                        //            .Include(a => a.Imovel)
                        //                .ThenInclude(i => i.Locadores)
                        //            .Where(A => A.Imovel.Locadores.Any(l => l.Id == userAsData.Locador.Id) && A.EntreguePorCliente == false)
                        //.ToListAsync());

                        result = arrendamentos
                            .Where(s => s.PorConfirmar == true && s.Imovel.Locadores.Any(l => l.Id == userAsData.Locador.Id));
                    }
                    else //Ent é porque é funcionário
                    {

                        var userAsData = _context.Funcionarios.FirstOrDefaultAsync(g => g.ApplicationUser == user).Result;
                        await _context.Entry(userAsData).Reference(g => g.Locador).LoadAsync();


                        //return View(await _context.Arrendamentos
                        //    .Include(a => a.Cliente)
                        //            .Include(a => a.Imovel)
                        //                .ThenInclude(i => i.Categoria)
                        //            .Include(a => a.Imovel)
                        //                .ThenInclude(i => i.Locadores)
                        //    .Where(A => A.Imovel.Locadores.Any(l => l.Id == userAsData.Locador.Id) && A.EntreguePorCliente == false)
                        //    .ToListAsync());

                        result = arrendamentos
                            .Where(s => s.PorConfirmar == true && s.Imovel.Locadores.Any(l => l.Id == userAsData.Locador.Id));
                    }

                    break;

                case "Entregue":
                    if (User.IsInRole("Gestor"))
                    {
                        var userAsData = _context.Gestores.FirstOrDefaultAsync(g => g.ApplicationUser == user).Result;
                        await _context.Entry(userAsData).Reference(g => g.Locador).LoadAsync();

                        //return View(await _context.Arrendamentos
                        //            .Include(a => a.Cliente)
                        //            .Include(a => a.Imovel)
                        //                .ThenInclude(i => i.Categoria)
                        //            .Include(a => a.Imovel)
                        //                .ThenInclude(i => i.Locadores)
                        //            .Where(A => A.Imovel.Locadores.Any(l => l.Id == userAsData.Locador.Id) && A.EntreguePorCliente == false)
                        //.ToListAsync());

                        result = arrendamentos
                            .Where(s => s.PorConfirmar == false && s.Imovel.Locadores.Any(l => l.Id == userAsData.Locador.Id));



                        //return View(await _context.Arrendamentos
                        //            .Include(a => a.Cliente)
                        //            .Include(a => a.Imovel)
                        //                .ThenInclude(i => i.Categoria)
                        //            .Include(a => a.Imovel)
                        //                .ThenInclude(i => i.Locadores)
                        //            .Where(A => A.Imovel.Locadores.Any(l => l.Id == userAsData.Locador.Id) && A.EntreguePorCliente == true)
                        //            .ToListAsync());

                       
                    }
                    else //Ent é porque é funcionário
                    {

                        var userAsData = _context.Funcionarios.FirstOrDefaultAsync(g => g.ApplicationUser == user).Result;
                        await _context.Entry(userAsData).Reference(g => g.Locador).LoadAsync();


                        //return View(await _context.Arrendamentos
                        //    .Include(a => a.Cliente)
                        //            .Include(a => a.Imovel)
                        //                .ThenInclude(i => i.Categoria)
                        //            .Include(a => a.Imovel)
                        //                .ThenInclude(i => i.Locadores)
                        //    .Where(A => A.Imovel.Locadores.Any(l => l.Id == userAsData.Locador.Id) && A.EntreguePorCliente == true)
                        //    .ToListAsync());


                        result = arrendamentos
                            .Where(s => s.PorConfirmar== false && s.Imovel.Locadores.Any(l => l.Id == userAsData.Locador.Id));

                    }

                    break;

                default:
                    if (User.IsInRole("Gestor"))
                    {
                        var userAsData = _context.Gestores.FirstOrDefaultAsync(g => g.ApplicationUser == user).Result;
                        await _context.Entry(userAsData).Reference(g => g.Locador).LoadAsync();


                        //return View(await _context.Arrendamentos
                        //            .Include(a => a.Cliente)
                        //            .Include(a => a.Imovel)
                        //                .ThenInclude(i => i.Categoria)
                        //            .Include(a => a.Imovel)
                        //                .ThenInclude(i => i.Locadores)
                        //            .Where(A => A.Imovel.Locadores.Any(l => l.Id == userAsData.Locador.Id) && A.EntreguePorCliente == false)
                        //.ToListAsync());

                        result = arrendamentos
                            .Where(A => A.Imovel.Locadores.Any(l => l.Id == userAsData.Locador.Id));



                    }
                    else if(User.IsInRole("Funcionario")) //Ent é porque é funcionário
                    {

                        var userAsData = _context.Funcionarios.FirstOrDefaultAsync(g => g.ApplicationUser == user).Result;
                        await _context.Entry(userAsData).Reference(g => g.Locador).LoadAsync();


                        //return View(await _context.Arrendamentos
                        //    .Include(a => a.Cliente)
                        //            .Include(a => a.Imovel)
                        //                .ThenInclude(i => i.Categoria)
                        //            .Include(a => a.Imovel)
                        //                .ThenInclude(i => i.Locadores)
                        //    .Where(A => A.Imovel.Locadores.Any(l => l.Id == userAsData.Locador.Id))
                        //    .ToListAsync());

                        result = arrendamentos
                            .Where(A => A.Imovel.Locadores.Any(l => l.Id == userAsData.Locador.Id));
                    }
                    else
                    {
                        //Aqui é pq o user é cliente pois não tem acesso aos filtros
                        result = arrendamentos
                            .Where(a => a.Cliente.Id == user.Id);

                        
                    }
                    break;
            }



            if (categoriaId > 0)
                result = result.Where(A => A.Imovel.CategoriaId == categoriaId);

            if (!string.IsNullOrEmpty(clienteId))
                result = result.Where(A => A.Cliente.Id == clienteId);
            Console.WriteLine("\n\n\n Cliente com id == " + clienteId);

            if (habitacaoId > 0)
                result = result.Where(A => A.Imovel.Id == habitacaoId);

            if (dataInicio != DateTime.MinValue)
            {
                result = result.Where(A => A.DatraEntrada >= dataInicio);
            }
            //Console.WriteLine("\n\n\nnão há data inicio: " + dataInicio);


            if (dataFim != DateTime.MinValue)
                result = result.Where(A => A.DatraSaida <= dataFim);

            result = result.Include(s => s.Cliente)
                        .Include(s => s.Imovel)
                        .Include(s => s.Imovel.Locadores)
                        .Include(s => s.Imovel.Categoria);

            var resultList = await result.ToListAsync();
            return View(resultList);



            //int categoriaId, int clienteId, int habitacaoId, DateTime dataInicio, DateTime dataFim










            //var resultList = await result.ToListAsync();
            //return View(resultList);





            //return arrendamentos != null ?
            //       View(await arrendamentos.ToListAsync()) :
            //       Problem("Entity set 'ApplicationDbContext.Arrendamentos' is null.");

            //return _context.Arrendamentos != null ?
            //              View(await _context.Arrendamentos.ToListAsync()) :
            //              Problem("Entity set 'ApplicationDbContext.Arrendamentos'  is null.");

            //switch (filtro)
            //{
            //    case "Entregues":
            //        if (User.IsInRole("Cliente"))
            //        {
            //            var user = _userManager.GetUserAsync(User).Result;
            //            return View(await _context.Arrendamentos.Where(A => A.Cliente == user).ToListAsync());
            //        }
            //        if (User.IsInRole("Gestor"))
            //        {
            //            var user = _userManager.GetUserAsync(User).Result;

            //            var userAsData = _context.Gestores.FirstOrDefaultAsync(g => g.ApplicationUser == user).Result;


            //            await _context.Entry(userAsData).Reference(g => g.Locador).LoadAsync();
            //            return View(await _context.Arrendamentos.Where(A =>
            //            A.Imovel.Locadores.Any(l => l.Id == userAsData.Locador.Id) && A.EntreguePorCliente == true).ToListAsync());
            //        }
            //        if (User.IsInRole("Funcionario"))
            //        {
            //            var user = _userManager.GetUserAsync(User).Result;

            //            var userAsData = _context.Funcionarios.FirstOrDefaultAsync(g => g.ApplicationUser == user).Result;


            //            await _context.Entry(userAsData).Reference(g => g.Locador).LoadAsync();
            //            return View(await _context.Arrendamentos.Where(A =>
            //            A.Imovel.Locadores.Any(l => l.Id == userAsData.Locador.Id) && A.EntreguePorCliente == true).ToListAsync());
            //        }
            //        break;

            //    default:

            //        if (User.IsInRole("Cliente"))
            //        {
            //            var user = _userManager.GetUserAsync(User).Result;
            //            return View(await _context.Arrendamentos.Where(A => A.Cliente == user).ToListAsync());
            //        }
            //        if (User.IsInRole("Gestor"))
            //        {
            //            var user = _userManager.GetUserAsync(User).Result;

            //            var userAsData = _context.Gestores.FirstOrDefaultAsync(g => g.ApplicationUser == user).Result;


            //            await _context.Entry(userAsData).Reference(g => g.Locador).LoadAsync();
            //            return View(await _context.Arrendamentos.Where(A =>
            //            A.Imovel.Locadores.Any(l => l.Id == userAsData.Locador.Id) && A.PorConfirmar == true).ToListAsync());
            //        }
            //        if (User.IsInRole("Funcionario"))
            //        {
            //            var user = _userManager.GetUserAsync(User).Result;

            //            var userAsData = _context.Funcionarios.FirstOrDefaultAsync(g => g.ApplicationUser == user).Result;


            //            await _context.Entry(userAsData).Reference(g => g.Locador).LoadAsync();
            //            return View(await _context.Arrendamentos.Where(A =>
            //            A.Imovel.Locadores.Any(l => l.Id == userAsData.Locador.Id) && A.PorConfirmar == true).ToListAsync());
            //        }
            //        break;

            //}


            //return _context.Arrendamentos != null ?
            //              View(await _context.Arrendamentos.ToListAsync()) :
            //              Problem("Entity set 'ApplicationDbContext.Arrendamentos'  is null.");
        }

        // GET: Arrendamentos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Arrendamentos == null)
            {
                return NotFound();
            }

            var arrendamento = await _context.Arrendamentos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (arrendamento == null)
            {
                return NotFound();
            }

            return View(arrendamento);
        }

        // GET: Arrendamentos/Create
        [Authorize(Roles = "Cliente")]
        public  IActionResult Create(int habitacaoId)
        {
            var habitacao =  _context.Habitacao.Include(h => h.Locadores)
                .FirstOrDefaultAsync(h => h.Id == habitacaoId);

            if (habitacao != null && habitacao.Result != null && habitacao.Result.Locadores != null)
            {
                var lista = habitacao.Result.Locadores.ToList();
                ViewData["Locadores"] = new SelectList(lista, "Id", "Nome");
            }
            

            //var lista = _context.Habitacao.FirstOrDefaultAsync(h => h.Id == habitacaoId).Result.Locadores.ToList();
            //Console.WriteLine("\n\n\n o tamanho da lista " + lista.Count);

            //ViewData["Locadores"] = new SelectList(lista, "Id", "Nome");
            return  View();
        }

        // POST: Arrendamentos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[HttpGet]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Cliente")]
        public async Task<IActionResult> Create(int habitacaoId, [Bind("Id,DatraEntrada,DatraSaida,PorConfirmar,Aceite,EntreguePorCliente,RecebidoPeloLocador,LocadorId")] Arrendamento arrendamento)
        {
            if (ModelState.IsValid)
            {
                //arrendamento.Imovel
                // var IdHabitacao = Int32.Parse(TempData["habitacaoId"] as string);
                

                var habitacao = await _context.Habitacao.FirstOrDefaultAsync(h => h.Id == habitacaoId);
              
                var user = await _userManager.GetUserAsync(User);

                if(user == null || habitacao == null || habitacao.Ativo == false)
                {
                    return RedirectToAction(nameof(Index));
                }
               

                arrendamento.Imovel = habitacao;
                arrendamento.Cliente = user;
                arrendamento.PorConfirmar = true;
                
            

                _context.Add(arrendamento);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(arrendamento);
        }

        // GET: Arrendamentos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Arrendamentos == null)
            {
                return NotFound();
            }

            var arrendamento = await _context.Arrendamentos.FindAsync(id);
            if (arrendamento == null)
            {
                return NotFound();
            }
            return View(arrendamento);
        }

        // POST: Arrendamentos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,DatraEntrada,DatraSaida,PorConfirmar,Aceite,EntreguePorCliente,RecebidoPeloLocador")] Arrendamento arrendamento)
        {
            if (id != arrendamento.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(arrendamento);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ArrendamentoExists(arrendamento.Id))
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
            return View(arrendamento);
        }

        // GET: Arrendamentos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Arrendamentos == null)
            {
                return NotFound();
            }

            var arrendamento = await _context.Arrendamentos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (arrendamento == null)
            {
                return NotFound();
            }

            return View(arrendamento);
        }

        // POST: Arrendamentos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Arrendamentos == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Arrendamentos'  is null.");
            }
            var arrendamento = await _context.Arrendamentos.FindAsync(id);
            if (arrendamento != null)
            {
                _context.Arrendamentos.Remove(arrendamento);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ArrendamentoExists(int id)
        {
          return (_context.Arrendamentos?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        public async Task<IActionResult> RejeitarArrendamento(int id)
        {
            var arrendamento = await _context.Arrendamentos.FindAsync(id);
            arrendamento.PorConfirmar = false;
            arrendamento.Aceite = false;
            if (arrendamento != null)
            {
                _context.Arrendamentos.Remove(arrendamento);
            }
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }

        public async Task<IActionResult> ClienteEntrega(int id)
        {
            var arrendamento = await _context.Arrendamentos.FindAsync(id);
            arrendamento.EntreguePorCliente = true;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
