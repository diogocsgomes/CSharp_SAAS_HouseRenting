using System;
using System.Collections.Generic;
using System.Data;
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
    [Authorize(Roles = "Gestor")]
    public class FuncionariosController : Controller
    {
        private readonly ApplicationDbContext _context;
        private UserManager<ApplicationUser> _userManager;
        
        

        public FuncionariosController(ApplicationDbContext context,
            UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
            
            
        }

        // GET: Funcionarios
        //[Authorize(Roles = "Gestor, Funcionario, Administrador")]
        public async Task<IActionResult> Index()
        {
            if(_context.Funcionarios != null)
            {
                if (User.IsInRole("Gestor") || User.IsInRole("Funcionario")) { 
                    var CurrentUser = await _userManager.GetUserAsync(User);
                    var gestor = await _context.Gestores
                              .FirstOrDefaultAsync(g => g.ApplicationUser == CurrentUser);

                    if (gestor != null)
                    {
                        await _context.Entry(gestor).Reference(g => g.Locador).LoadAsync();
                    
                    }
                    var toReturn = _context.Funcionarios.Where(f => f.Locador == gestor.Locador).ToList();
                    return View(toReturn);
                }else if(User.IsInRole("Administrador"))
                {
                    var toReturn = _context.Funcionarios;
                    return View(toReturn);
                }
            }

            //return _context.Funcionarios != null ? 
            //            View(await _context.Funcionarios.ToListAsync()) :
            //            Problem("Entity set 'ApplicationDbContext.Funcionarios'  is null.");
            return Problem("Entity set 'ApplicationDbContext.Funcionarios'  is null.");
        }

        // GET: Funcionarios/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Funcionarios == null)
            {
                return NotFound();
            }

            var funcionario = await _context.Funcionarios
                .FirstOrDefaultAsync(m => m.FuncionarioId == id);
            if (funcionario == null)
            {
                return NotFound();
            }

            return View(funcionario);
        }

        // GET: Funcionarios/Create
        [Authorize(Roles = "Gestor")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Funcionarios/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Gestor")]
        public async Task<IActionResult> Create([Bind("FuncionarioId,Nome,Email,DataNascimento,N_telefone")] Funcionario funcionario)
        {
            if (ModelState.IsValid)
            {
                var emailCheck = await _userManager.FindByEmailAsync(funcionario.Email);
                if (emailCheck != null)
                {
                    // O email já está sendo usado por outro usuário
                    ModelState.AddModelError("Email", "O email já está em uso.");

                    // Retornar a mesma view com o modelo e a mensagem de erro
                    return View(nameof(Create), funcionario);
                }

                var CurrentUser = await _userManager.GetUserAsync(User);
                Console.WriteLine("\n \n \n" + CurrentUser.Email);
                
                var gestor = await _context.Gestores
                           .FirstOrDefaultAsync(g => g.ApplicationUser == CurrentUser);

                if (gestor != null)
                {
                    await _context.Entry(gestor).Reference(g => g.Locador).LoadAsync();
                    funcionario.Locador = gestor.Locador;
                }
                funcionario.Ativo = true;
                _context.Add(funcionario);
                
                await _context.SaveChangesAsync();

                var UserFuncionario = new ApplicationUser
                {
                    Email = funcionario.Email,
                    NormalizedEmail = funcionario.Email.ToUpper(),
                    UserName = funcionario.Email,
                    NormalizedUserName = funcionario.Email.ToUpper(),
                    DataNascimento = funcionario.DataNascimento,
                    PhoneNumber = funcionario.N_telefone.ToString(),
                    NIF = funcionario.FuncionarioId *10,
                    EmailConfirmed = true

                };
                var user = await _userManager.FindByEmailAsync(UserFuncionario.Email);
                if (user == null)
                {

                    await _userManager.CreateAsync(UserFuncionario, "Ab%12345");

                    await _userManager.AddToRoleAsync(UserFuncionario, "Funcionario");
                }
               
                // await _context.SaveChangesAsync();

                 funcionario.ApplicationUser = UserFuncionario;
                
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            return View(funcionario);
        }

        // GET: Funcionarios/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Funcionarios == null)
            {
                return NotFound();
            }

            var funcionario = await _context.Funcionarios.FindAsync(id);
            if (funcionario == null)
            {
                return NotFound();
            }
            return View(funcionario);
        }

        // POST: Funcionarios/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("FuncionarioId,Nome,Email,DataNascimento,N_telefone, Ativo")] Funcionario funcionario)
       
        {
            Console.WriteLine("\n \n \n Vim aqui :)) " + funcionario.Ativo);
            if (id != funcionario.FuncionarioId)
            {
                
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                Console.WriteLine("Tou aqui dentro");
                try
                {
                    _context.Update(funcionario);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FuncionarioExists(funcionario.FuncionarioId))
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
            return View(funcionario);
        }

        // GET: Funcionarios/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Funcionarios == null)
            {
                return NotFound();
            }

            var funcionario = await _context.Funcionarios
                .FirstOrDefaultAsync(m => m.FuncionarioId == id);
            if (funcionario == null)
            {
                return NotFound();
            }

            return View(funcionario);
        }

        // POST: Funcionarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Funcionarios == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Funcionarios'  is null.");
            }
            var funcionario = await _context.Funcionarios.FindAsync(id);
            
            if (funcionario != null)
            {
                var funcionarioAsUser = await _userManager.FindByEmailAsync(funcionario.Email);
                if(funcionarioAsUser != null)
                {
                    var rolesForUser = await _userManager.GetRolesAsync(funcionarioAsUser);
                    if (rolesForUser.Count > 0)
                    {
                        foreach (var role in rolesForUser.ToList())
                        {
                            var result = await _userManager.RemoveFromRoleAsync(funcionarioAsUser, role);
                            
                        }
                    }

                    await _userManager.DeleteAsync(funcionarioAsUser);
                    _context.Funcionarios.Remove(funcionario);
                }
                
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FuncionarioExists(int id)
        {
          return (_context.Funcionarios?.Any(e => e.FuncionarioId == id)).GetValueOrDefault();
        }
    }
}
