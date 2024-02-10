using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
//using AspNetCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Trabalho_Pratico_23_24.Data;
using Trabalho_Pratico_23_24.Models;

namespace Trabalho_Pratico_23_24.Controllers
{
    [Authorize(Roles = "Gestor,Funcionario")]
    public class CheckInsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public CheckInsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: CheckIns
        public async Task<IActionResult> Index()
        {
              return _context.CheckIns != null ? 
                          View(await _context.CheckIns.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.CheckIns'  is null.");
        }

        // GET: CheckIns/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.CheckIns == null)
            {
                return NotFound();
            }

            var checkIn = await _context.CheckIns
                .FirstOrDefaultAsync(m => m.Id == id);
            if (checkIn == null)
            {
                return NotFound();
            }

            return View(checkIn);
        }

        // GET: CheckIns/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CheckIns/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int id, [Bind("HoraEntrada,TemEquipamentosOpcionais,EquipamentoOpcionais,TemDanos,Danos,Observacoes")] CheckIn checkIn)
        {
            
            
            if (ModelState.IsValid)
            {
                var arrendamento = await _context.Arrendamentos.FirstOrDefaultAsync(a => a.Id == id);
                if(arrendamento == null)
                {
                    Console.WriteLine("\n \n\n rrendamento is null " + id);
                    return View(checkIn);
                }
                arrendamento.PorConfirmar = false;
                arrendamento.Aceite = true;
                
                var user = await _userManager.GetUserAsync(User);
                checkIn.Entregador = user;

                
                _context.Add(checkIn);
                
                
                arrendamento.CheckIn = checkIn;
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            
            
           



            return View(checkIn);
        }

        // GET: CheckIns/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.CheckIns == null)
            {
                return NotFound();
            }

            var checkIn = await _context.CheckIns.FindAsync(id);
            if (checkIn == null)
            {
                return NotFound();
            }
            return View(checkIn);
        }

        // POST: CheckIns/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,HoraEntrada,TemEquipamentosOpcionais,EquipamentoOpcionais,TemDanos,Danos,Observacoes")] CheckIn checkIn)
        {
            if (id != checkIn.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(checkIn);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CheckInExists(checkIn.Id))
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
            return View(checkIn);
        }

        // GET: CheckIns/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.CheckIns == null)
            {
                return NotFound();
            }

            var checkIn = await _context.CheckIns
                .FirstOrDefaultAsync(m => m.Id == id);
            if (checkIn == null)
            {
                return NotFound();
            }

            return View(checkIn);
        }

        // POST: CheckIns/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.CheckIns == null)
            {
                return Problem("Entity set 'ApplicationDbContext.CheckIns'  is null.");
            }
            var checkIn = await _context.CheckIns.FindAsync(id);
            if (checkIn != null)
            {
                _context.CheckIns.Remove(checkIn);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CheckInExists(int id)
        {
          return (_context.CheckIns?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
