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
    public class LibroesController : Controller
    {
        private readonly ConstruTechContext _context;

        public LibroesController(ConstruTechContext context)
        {
            _context = context;
        }

        // GET: Libroes
        public async Task<IActionResult> Index()
        {
              return _context.Libros != null ? 
                          View(await _context.Libros.ToListAsync()) :
                          Problem("Entity set 'ConstruTechContext.Libros'  is null.");
        }

        // GET: Libroes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Libros == null)
            {
                return NotFound();
            }

            var libro = await _context.Libros
                .FirstOrDefaultAsync(m => m.Id == id);
            if (libro == null)
            {
                return NotFound();
            }

            return View(libro);
        }

        // GET: Libroes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Libroes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Titulo,Autor,Genero,Fechapublicacion")] Libro libro)
        {
            if (ModelState.IsValid)
            {
                _context.Add(libro);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(libro);
        }

        // GET: Libroes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Libros == null)
            {
                return NotFound();
            }

            var libro = await _context.Libros.FindAsync(id);
            if (libro == null)
            {
                return NotFound();
            }
            return View(libro);
        }

        // POST: Libroes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Titulo,Autor,Genero,Fechapublicacion")] Libro libro)
        {
            if (id != libro.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(libro);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LibroExists(libro.Id))
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
            return View(libro);
        }

        // GET: Libroes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Libros == null)
            {
                return NotFound();
            }

            var libro = await _context.Libros
                .FirstOrDefaultAsync(m => m.Id == id);
            if (libro == null)
            {
                return NotFound();
            }

            return View(libro);
        }

        // POST: Libroes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Libros == null)
            {
                return Problem("Entity set 'ConstruTechContext.Libros'  is null.");
            }
            var libro = await _context.Libros.FindAsync(id);
            if (libro != null)
            {
                _context.Libros.Remove(libro);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LibroExists(int id)
        {
          return (_context.Libros?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
