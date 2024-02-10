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
using Trabalho_Pratico_23_24.Models;

namespace Trabalho_Pratico_23_24.Controllers
{
    [Authorize(Roles = "Administrador")]
    public class LocadoresController : Controller
    {
        private readonly ApplicationDbContext _context;

        private UserManager<ApplicationUser> _userManager;


        public LocadoresController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;

        }

        // GET: Locadores
        public async Task<IActionResult> Index(string disponivel)
        {

            if(_context.Locadores == null)
                return Problem("Entity set 'ApplicationDbContext.Locadores'  is null.");

            if (disponivel == "true")
                return View(_context.Locadores.Where(c => c.Ativo == true));

            if (disponivel == "false")
                return View(_context.Locadores.Where(c => c.Ativo == false));


            return View(await _context.Locadores.ToListAsync());

            //return _context.Locadores != null ?
            //            View(await _context.Locadores.ToListAsync()) :
            //            Problem("Entity set 'ApplicationDbContext.Locadores'  is null.");
        }




        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Search([Bind("TextoAPesquisar")] PesquisaLocadorViewModel pesquisaLocador)
        {
            

            PesquisaLocadorViewModel pesquisa = new PesquisaLocadorViewModel()
            {
                TextoAPesquisar = pesquisaLocador.TextoAPesquisar,
                ListaDeLocadores = new List<Locador>(),
                NumResultados = 0


            };


           // Console.WriteLine("Recebi " + pesquisa.TextoAPesquisar);
            pesquisa.ListaDeLocadores = _context.Locadores.Where(p => p.Nome == pesquisa.TextoAPesquisar).ToList();
            pesquisa.NumResultados = pesquisa.ListaDeLocadores.Count;


            return View(pesquisa);

           

        }



            // GET: Locadores/Details/5
            public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Locadores == null)
            {
                return NotFound();
            }

            var locador = await _context.Locadores
                .FirstOrDefaultAsync(m => m.Id == id);
            if (locador == null)
            {
                return NotFound();
            }

            return View(locador);
        }

        // GET: Locadores/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Locadores/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome,N_Telefone,Email")] Locador locador)
        {
            if (ModelState.IsValid)
            {
                locador.Ativo = true;
                _context.Add(locador);
                await _context.SaveChangesAsync();

                Console.WriteLine("O ID do locador adicionado é " + locador.Id);

                string GestorEmail = "Locador" + locador.Id + "Gestor@gmail.com";
                string GestorUserName = "Gestor" + locador.Id;

                Console.WriteLine("\n \n \nO nome é: " + GestorUserName);

                var gestorUser = new ApplicationUser
                {
                    UserName = GestorEmail,
                    NormalizedUserName = GestorEmail.ToUpper(),
                    //     UserName = GestorEmail,
                    Email = GestorEmail,
                    PrimeiroNome = locador.Nome + "Gestor",
                    UltimoNome = "Local",
                    EmailConfirmed = true,
                    PhoneNumberConfirmed = true,
                    DataNascimento = DateTime.Today,
                    
                    //Locador = locador

                    //PasswordHash = "1234"

                };
                var user = await _userManager.FindByEmailAsync(gestorUser.Email);
                if (user == null)
                {

                    await _userManager.CreateAsync(gestorUser, "Ab%12345");

                    await _userManager.AddToRoleAsync(gestorUser, "Gestor");
                }
                
                await _context.SaveChangesAsync();


                var gestorData = new Gestor
                {
                    PrimeiroNome = "Primeiro" + locador.Nome ,
                    UltimoNome = "Gestor",
                    Email = GestorEmail,
                    DataNascimento = DateTime.Today,
                    N_telefone = -1,
                    ApplicationUser = gestorUser,
                    Locador = locador,
                    Ativo = true
                    

                };
                _context.Gestores.Add(gestorData);
                await _context.SaveChangesAsync();


                return RedirectToAction(nameof(Index));




            }
            return View(locador);
        }

        // GET: Locadores/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Locadores == null)
            {
                return NotFound();
            }

            var locador = await _context.Locadores.FindAsync(id);
            if (locador == null)
            {
                return NotFound();
            }
            return View(locador);
        }

        // POST: Locadores/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,N_Telefone,Email,Ativo")] Locador locador)
        {
            if (id != locador.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(locador);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LocadorExists(locador.Id))
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
            return View(locador);
        }

        // GET: Locadores/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Locadores == null)
            {
                return NotFound();
            }

            var locador = await _context.Locadores
                .FirstOrDefaultAsync(m => m.Id == id);
            if (locador == null)
            {
                return NotFound();
            }

            return View(locador);
        }

        // POST: Locadores/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Locadores == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Locadores'  is null.");
            }
           
            var locador = await _context.Locadores.FindAsync(id);
            
            
            if (locador != null && locador.Habitacoes.Count == 0)
            {
                var habitacoes = await _context.Habitacao
                    .Where(h => h.Locadores.Any(l => l == locador))
        .ToListAsync();

                if (habitacoes.Count > 0) // nao pode eleminar
                {
                    return RedirectToAction(nameof(Index));
                }

                var funcionariosAsData = await _context.Funcionarios.Where(f => f.Locador == locador)
                    .Include(f => f.ApplicationUser).ToListAsync();

                if(funcionariosAsData.Count > 0)
                {
                    var funcionariosAsUsers = funcionariosAsData.Where(f => f.ApplicationUser != null)
                        .Select(f => f.ApplicationUser).ToList();

                    if(funcionariosAsUsers.Count > 0)
                    {
                        foreach(var funcionarioAsUser in funcionariosAsUsers)
                        {
                            var result = await _userManager.DeleteAsync(funcionarioAsUser);
                            if (!result.Succeeded)
                            {
                                Console.WriteLine("\n\n\n Eleminação não efetuada");
                            }
                        }

                        foreach(var funcionarioAsData in funcionariosAsData)
                        {
                             _context.Funcionarios.Remove(funcionarioAsData);
                            
                        }


                    }
                }

                var gestoresAsData = await _context.Gestores.Where(g => g.Locador == locador)
                    .Include(g => g.ApplicationUser).ToListAsync();

                if(gestoresAsData.Count > 0)
                {
                    var gestoresAsUsers = gestoresAsData.Where(g => g.ApplicationUser != null)
                        .Select(g => g.ApplicationUser).ToList();
                    if(gestoresAsUsers.Count > 0)
                    {
                        foreach (var gestorAsUser in gestoresAsUsers)
                        {
                            var result = await _userManager.DeleteAsync(gestorAsUser);
                            if (!result.Succeeded)
                            {
                                Console.WriteLine("\n\n\n Eleminação não efetuada");
                            }
                        }

                        foreach (var gestorAsData in gestoresAsData)
                        {
                            _context.Gestores.Remove(gestorAsData);

                        }
                    }
                }


                

                _context.Locadores.Remove(locador);
            }

            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LocadorExists(int id)
        {
            return (_context.Locadores?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
