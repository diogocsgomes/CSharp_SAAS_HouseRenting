using System;
using System.Collections.Generic;
using System.Drawing;
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
using Trabalho_Pratico_23_24.ViewModels;

namespace Trabalho_Pratico_23_24.Controllers
{
    [Authorize(Roles = "Gestor,Funcionario")]
    public class CheckOutsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        IWebHostEnvironment _webHostEnvironment;

        public CheckOutsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _userManager = userManager;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: CheckOuts
        public async Task<IActionResult> Index()
        {
            return _context.CheckOuts != null ?
                        View(await _context.CheckOuts.ToListAsync()) :
                        Problem("Entity set 'ApplicationDbContext.CheckOuts'  is null.");
        }

        // GET: CheckOuts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.CheckOuts == null)
            {
                return NotFound();
            }

            var checkOut = await _context.CheckOuts
                .FirstOrDefaultAsync(m => m.Id == id);
            if (checkOut == null)
            {
                return NotFound();
            }

            return View(checkOut);
        }

        // GET: CheckOuts/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CheckOuts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int id, [Bind("HoraSaida,TemEquipamentosOpcionais,EquipamentoOpcionais,TemDanos,Danos,DanosImagem,Observacoes")] CheckOutViewModel checkOut1)
        {
            if (ModelState.IsValid)
            {

                String filename = "";
                if (checkOut1.DanosImagem != null)
                {
                    String uploadfolder = Path.Combine(_webHostEnvironment.WebRootPath, "danos");
                    filename = Guid.NewGuid().ToString() + "_" + checkOut1.Danos + ".png";
                    String filepath = Path.Combine(uploadfolder, filename);
                    checkOut1.DanosImagem.CopyTo(new FileStream(filepath, FileMode.Create));
                }

                CheckOut o = new()
                {
                    Id = checkOut1.Id,
                    HoraSaida = checkOut1.HoraSaida,
                    TemEquipamentosOpcionais = checkOut1.TemEquipamentosOpcionais,
                    EquipamentoOpcionais = checkOut1.EquipamentoOpcionais,
                    TemDanos = checkOut1.TemDanos,
                    Danos = checkOut1.Danos,
                    Observacoes = checkOut1.Observacoes,
                    DanosPath = filename,
                    Recetor = checkOut1.Recetor,

                };

                var arrendamento = await _context.Arrendamentos.FirstOrDefaultAsync(a => a.Id == id);

                if (arrendamento == null)
                {
                    Console.WriteLine("\n \n\n rrendamento is null " + id);
                    return View(o);
                }

                var user = await _userManager.GetUserAsync(User);
                o.Recetor = user;
                arrendamento.CheckOut = o;
                arrendamento.RecebidoPeloLocador = true;

                _context.CheckOuts.Add(o);
                _context.Add(o);
                _context.SaveChanges();
                await _context.SaveChangesAsync();
                ViewBag.sucess = "Registo CheckOut Concluido";
                return RedirectToAction(nameof(Index));
            }
            return View(checkOut1);
        }

        // GET: CheckOuts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.CheckOuts == null)
            {
                return NotFound();
            }

            var checkOut = await _context.CheckOuts.FindAsync(id);
            if (checkOut == null)
            {
                return NotFound();
            }
            return View(checkOut);
        }

        // POST: CheckOuts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,HoraSaida,TemEquipamentosOpcionais,EquipamentoOpcionais,TemDanos,Danos,Observacoes")] CheckOut checkOut)
        {
            if (id != checkOut.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(checkOut);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CheckOutExists(checkOut.Id))
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
            return View(checkOut);
        }

        // GET: CheckOuts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.CheckOuts == null)
            {
                return NotFound();
            }

            var checkOut = await _context.CheckOuts
                .FirstOrDefaultAsync(m => m.Id == id);
            if (checkOut == null)
            {
                return NotFound();
            }

            return View(checkOut);
        }

        // POST: CheckOuts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.CheckOuts == null)
            {
                return Problem("Entity set 'ApplicationDbContext.CheckOuts'  is null.");
            }
            var checkOut = await _context.CheckOuts.FindAsync(id);
            if (checkOut != null)
            {
                _context.CheckOuts.Remove(checkOut);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CheckOutExists(int id)
        {
            return (_context.CheckOuts?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
