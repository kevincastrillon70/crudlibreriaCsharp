using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using sistemaLibros.Models;

namespace sistemaLibros.Controllers
{
    public class PrestamoesController : Controller
    {
        private readonly ConstruTechContext _context;

        public PrestamoesController(ConstruTechContext context)
        {
            _context = context;
        }

        // GET: Prestamoes
        public async Task<IActionResult> Index()
        {
            var construTechContext = _context.Prestamos.Include(p => p.IdlibroNavigation).Include(p => p.IduserNavigation);
            return View(await construTechContext.ToListAsync());
        }

        // GET: Prestamoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Prestamos == null)
            {
                return NotFound();
            }

            var prestamo = await _context.Prestamos
                .Include(p => p.IdlibroNavigation)
                .Include(p => p.IduserNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (prestamo == null)
            {
                return NotFound();
            }

            return View(prestamo);
        }

        // GET: Prestamoes/Create
        public IActionResult Create()
        {
            ViewData["Idlibro"] = new SelectList(_context.Libros, "Id", "Id");
            ViewData["Iduser"] = new SelectList(_context.Clientes, "IdCli", "IdCli");
            return View();
        }

        // POST: Prestamoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Idlibro,Iduser,Fechainicio,Fechafin")] Prestamo prestamo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(prestamo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Idlibro"] = new SelectList(_context.Libros, "Id", "Id", prestamo.Idlibro);
            ViewData["Iduser"] = new SelectList(_context.Clientes, "IdCli", "IdCli", prestamo.Iduser);
            return View(prestamo);
        }

        // GET: Prestamoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Prestamos == null)
            {
                return NotFound();
            }

            var prestamo = await _context.Prestamos.FindAsync(id);
            if (prestamo == null)
            {
                return NotFound();
            }
            ViewData["Idlibro"] = new SelectList(_context.Libros, "Id", "Id", prestamo.Idlibro);
            ViewData["Iduser"] = new SelectList(_context.Clientes, "IdCli", "IdCli", prestamo.Iduser);
            return View(prestamo);
        }

        // POST: Prestamoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Idlibro,Iduser,Fechainicio,Fechafin")] Prestamo prestamo)
        {
            if (id != prestamo.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(prestamo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PrestamoExists(prestamo.Id))
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
            ViewData["Idlibro"] = new SelectList(_context.Libros, "Id", "Id", prestamo.Idlibro);
            ViewData["Iduser"] = new SelectList(_context.Clientes, "IdCli", "IdCli", prestamo.Iduser);
            return View(prestamo);
        }

        // GET: Prestamoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Prestamos == null)
            {
                return NotFound();
            }

            var prestamo = await _context.Prestamos
                .Include(p => p.IdlibroNavigation)
                .Include(p => p.IduserNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (prestamo == null)
            {
                return NotFound();
            }

            return View(prestamo);
        }

        // POST: Prestamoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Prestamos == null)
            {
                return Problem("Entity set 'ConstruTechContext.Prestamos'  is null.");
            }
            var prestamo = await _context.Prestamos.FindAsync(id);
            if (prestamo != null)
            {
                _context.Prestamos.Remove(prestamo);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PrestamoExists(int id)
        {
          return (_context.Prestamos?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
