using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Clinicamedica.Data;
using Clinicamedica.Models;
using Microsoft.AspNetCore.Authorization;

namespace Clinicamedica.Controllers
{
    [Authorize]
    public class TratamientoController : Controller
    {
        private readonly ClinicamedicaContext _context;

        public TratamientoController(ClinicamedicaContext context)
        {
            _context = context;
        }

        // GET: Tratamientoes
        public async Task<IActionResult> Index()
        {
            var clinicamedicaContext = _context.Tratamiento.Include(t => t.Cita);
            return View(await clinicamedicaContext.ToListAsync());
        }

        // GET: Tratamientoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tratamiento = await _context.Tratamiento
                .Include(t => t.Cita)
                .FirstOrDefaultAsync(m => m.TratamientoId == id);
            if (tratamiento == null)
            {
                return NotFound();
            }

            return View(tratamiento);
        }

        
        public IActionResult Create()
        {
            ViewData["CitaId"] = new SelectList(_context.Cita, "CitaId", "CitaId");
            return View();
        }

        
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TratamientoId,Descripcion,CitaId")] Tratamiento tratamiento)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tratamiento);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CitaId"] = new SelectList(_context.Cita, "CitaId", "CitaId", tratamiento.CitaId);
            return View(tratamiento);
        }

        // GET: Tratamientoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tratamiento = await _context.Tratamiento.FindAsync(id);
            if (tratamiento == null)
            {
                return NotFound();
            }
            ViewData["CitaId"] = new SelectList(_context.Cita, "CitaId", "CitaId", tratamiento.CitaId);
            return View(tratamiento);
        }

       
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TratamientoId,Descripcion,CitaId")] Tratamiento tratamiento)
        {
            if (id != tratamiento.TratamientoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tratamiento);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TratamientoExists(tratamiento.TratamientoId))
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
            ViewData["CitaId"] = new SelectList(_context.Cita, "CitaId", "CitaId", tratamiento.CitaId);
            return View(tratamiento);
        }

        // GET: Tratamientoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tratamiento = await _context.Tratamiento
                .Include(t => t.Cita)
                .FirstOrDefaultAsync(m => m.TratamientoId == id);
            if (tratamiento == null)
            {
                return NotFound();
            }

            return View(tratamiento);
        }

       
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tratamiento = await _context.Tratamiento.FindAsync(id);
            if (tratamiento != null)
            {
                _context.Tratamiento.Remove(tratamiento);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TratamientoExists(int id)
        {
            return _context.Tratamiento.Any(e => e.TratamientoId == id);
        }
    }
}
