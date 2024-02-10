using System;
using System.Collections.Generic;
using System.Linq;
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
    [Authorize(Roles = "Administrador, Gestor")]
    public class GestoresController : Controller 
    {
        private readonly ApplicationDbContext _context;
        private UserManager<ApplicationUser> _userManager;
        public GestoresController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Gestores
        //[Authorize(Roles = "Administrador, Gestor")]
        public async Task<IActionResult> Index()
        {

            if (_context.Funcionarios != null)
            {
                if (User.IsInRole("Gestor"))
                {
                    var CurrentUser = await _userManager.GetUserAsync(User);
                    var gestor = await _context.Gestores
                              .FirstOrDefaultAsync(g => g.ApplicationUser == CurrentUser);

                    if (gestor != null)
                    {
                        await _context.Entry(gestor).Reference(g => g.Locador).LoadAsync();

                    }
                    var toReturn = _context.Gestores.Where(g => g.Locador == gestor.Locador).ToList();
                    return View(toReturn);
                }else if(User.IsInRole("Administrador")){
                    var toReturn = _context.Gestores;
                    return View(toReturn);
                }

            }

            
            return Problem("Entity set 'ApplicationDbContext.Gestores'  is null.");

            //return _context.Gestores != null ? 
            //            View(await _context.Gestores.ToListAsync()) :
            //            Problem("Entity set 'ApplicationDbContext.Gestores'  is null.");
        }

        // GET: Gestores/Details/5

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Gestores == null)
            {
                return NotFound();
            }

            var gestor = await _context.Gestores
                .FirstOrDefaultAsync(m => m.gestorId == id);
            if (gestor == null)
            {
                return NotFound();
            }

            return View(gestor);
        }

        // GET: Gestores/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Gestores/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("gestorId,PrimeiroNome,UltimoNome,Email,DataNascimento,N_telefone")] Gestor gestor)
        {
            if (ModelState.IsValid)
            {

                var emailCheck = await _userManager.FindByEmailAsync(gestor.Email);
                if(emailCheck != null)
                {
                    // O email já está a ser usado por outro usuário
                    ModelState.AddModelError("Email", "O email já está em uso.");

                    // Retornar a mesma view com o modelo e a mensagem de erro
                    return View(nameof(Create), gestor);
                }

                var CurrentUser = await _userManager.GetUserAsync(User);
                

                var gestorAsData = await _context.Gestores
                           .FirstOrDefaultAsync(g => g.ApplicationUser == CurrentUser);

                if (gestorAsData != null)
                {
                    await _context.Entry(gestorAsData).Reference(g => g.Locador).LoadAsync();
                    gestor.Locador = gestorAsData.Locador;
                }

                gestor.Ativo = true;
                _context.Add(gestor);

                var UserGestor = new ApplicationUser
                {
                    Email = gestor.Email,
                    NormalizedEmail = gestor.Email.ToUpper(),
                    UserName = gestor.Email,
                    NormalizedUserName = gestor.Email.ToUpper(),
                    DataNascimento = gestor.DataNascimento,
                    PhoneNumber = gestor.N_telefone.ToString(),
                    NIF = gestor.gestorId * 10,
                    EmailConfirmed = true

                };
                var user = await _userManager.FindByEmailAsync(UserGestor.Email);
                if (user == null)
                {

                    await _userManager.CreateAsync(UserGestor, "Ab%12345");

                    await _userManager.AddToRoleAsync(UserGestor, "Gestor");
                }

                // await _context.SaveChangesAsync();

                gestor.ApplicationUser = UserGestor;

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(gestor);
        }

        // GET: Gestores/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {

            

            if (id == null || _context.Gestores == null)
            {
                return NotFound();
            }

            var currentUser = await _userManager.GetUserAsync(User);
            var gestor = await _context.Gestores.FindAsync(id);
            if (gestor == null)
            {
                return NotFound();
            }
            if(gestor.ApplicationUser == currentUser)
            {
                return RedirectToAction(nameof(Index));
            }
            return View(gestor);
        }

        // POST: Gestores/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("gestorId,PrimeiroNome,UltimoNome,Email,DataNascimento,N_telefone,Ativo")] Gestor gestor)
        {
            if (id != gestor.gestorId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(gestor);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GestorExists(gestor.gestorId))
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
            return View(gestor);
        }

        // GET: Gestores/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Gestores == null)
            {
                return NotFound();
            }


            var gestor = await _context.Gestores
                .FirstOrDefaultAsync(m => m.gestorId == id);
            if (gestor == null)
            {
                return NotFound();
            }

            var currentUser = await _userManager.GetUserAsync(User);
 
            if (gestor.ApplicationUser == currentUser)
            {
                return RedirectToAction(nameof(Index));
            }
            return View(gestor);
        }

        // POST: Gestores/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Gestores == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Gestores'  is null.");
            }
            var gestor = await _context.Gestores.FindAsync(id);

            if (gestor != null)
            {
                var gestorAsUser = await _userManager.FindByEmailAsync(gestor.Email);
                var currentUser = await _userManager.GetUserAsync(User);
                if(currentUser.Id == gestorAsUser.Id)
                {
                    return RedirectToAction(nameof(Index));
                }

                if (gestorAsUser != null)
                {
                    var rolesForUser = await _userManager.GetRolesAsync(gestorAsUser);
                    if (rolesForUser.Count > 0)
                    {
                        foreach (var role in rolesForUser.ToList())
                        {
                            var result = await _userManager.RemoveFromRoleAsync(gestorAsUser, role);

                        }
                    }

                    await _userManager.DeleteAsync(gestorAsUser);
                    _context.Gestores.Remove(gestor);
                }

            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

            //if (_context.Gestores == null)
            //{
            //    return Problem("Entity set 'ApplicationDbContext.Gestores'  is null.");
            //}
            //var gestor = await _context.Gestores.FindAsync(id);
            //if (gestor != null)
            //{
            //    _context.Gestores.Remove(gestor);
            //}

            //await _context.SaveChangesAsync();
            //return RedirectToAction(nameof(Index));
        }

        private bool GestorExists(int id)
        {
          return (_context.Gestores?.Any(e => e.gestorId == id)).GetValueOrDefault();
        }
    }
}
