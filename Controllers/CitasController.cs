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
    public class CitasController : Controller
    {
        private readonly ClinicamedicaContext _context;

        public CitasController(ClinicamedicaContext context)
        {
            _context = context;
        }

        // GET: Citas
        public async Task<IActionResult> Index()
        {
            var clinicamedicaContext = _context.Cita.Include(c => c.Medico).Include(c => c.Paciente);
            return View(await clinicamedicaContext.ToListAsync());
        }

        // GET: Citas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cita = await _context.Cita
                .Include(c => c.Medico)
                .Include(c => c.Paciente)
                .FirstOrDefaultAsync(m => m.CitaId == id);
            if (cita == null)
            {
                return NotFound();
            }

            return View(cita);
        }

        // GET: Citas/Create
        public IActionResult Create()
        {
            // Asegúrate de que estos datos estén correctamente poblados
            ViewData["MedicoId"] = new SelectList(_context.Set<Medico>(), "MedicoId", "Nombre");
            ViewData["PacienteId"] = new SelectList(_context.Paciente, "PacienteId", "Nombre");
            return View();
        }

        // POST: Citas/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CitaId,PacienteId,MedicoId,Fecha,Motivo")] Cita cita)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Add(cita);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException)
                {
                    // Maneja excepciones si es necesario
                    ModelState.AddModelError("", "No se pudo guardar la cita. Inténtalo de nuevo.");
                }
            }

            // Si el modelo no es válido, vuelve a cargar los datos de la vista
            ViewData["MedicoId"] = new SelectList(_context.Set<Medico>(), "MedicoId", "Nombre", cita.MedicoId);
            ViewData["PacienteId"] = new SelectList(_context.Paciente, "PacienteId", "Nombre", cita.PacienteId);
            return View(cita);
        }

        // GET: Citas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cita = await _context.Cita.FindAsync(id);
            if (cita == null)
            {
                return NotFound();
            }
            ViewData["MedicoId"] = new SelectList(_context.Set<Medico>(), "MedicoId", "Nombre", cita.MedicoId);
            ViewData["PacienteId"] = new SelectList(_context.Paciente, "PacienteId", "Nombre", cita.PacienteId);
            return View(cita);
        }

        // POST: Citas/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CitaId,PacienteId,MedicoId,Fecha,Motivo")] Cita cita)
        {
            if (id != cita.CitaId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cita);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CitaExists(cita.CitaId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        // Puedes registrar el error si es necesario
                        // _logger.LogError("Error en la actualización de la cita.");
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["MedicoId"] = new SelectList(_context.Set<Medico>(), "MedicoId", "Nombre", cita.MedicoId);
            ViewData["PacienteId"] = new SelectList(_context.Paciente, "PacienteId", "Nombre", cita.PacienteId);
            return View(cita);
        }

        // GET: Citas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cita = await _context.Cita
                .Include(c => c.Medico)
                .Include(c => c.Paciente)
                .FirstOrDefaultAsync(m => m.CitaId == id);
            if (cita == null)
            {
                return NotFound();
            }

            return View(cita);
        }

        // POST: Citas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cita = await _context.Cita.FindAsync(id);
            if (cita != null)
            {
                _context.Cita.Remove(cita);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        private bool CitaExists(int id)
        {
            return _context.Cita.Any(e => e.CitaId == id);
        }
    }
}
