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
    public class FacturasController : Controller
    {
        private readonly ClinicamedicaContext _context;

        public FacturasController(ClinicamedicaContext context)
        {
            _context = context;
        }

        // GET: Facturas
        public async Task<IActionResult> Index()
        {
            var clinicamedicaContext = _context.Factura.Include(f => f.Paciente).Include(f => f.Tratamiento);
            return View(await clinicamedicaContext.ToListAsync());
        }

        // GET: Facturas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var factura = await _context.Factura
                .Include(f => f.Paciente)
                .Include(f => f.Tratamiento)
                .FirstOrDefaultAsync(m => m.FacturaId == id);
            if (factura == null)
            {
                return NotFound();
            }

            return View(factura);
        }

        // GET: Facturas/Create
        public IActionResult Create()
        {
            ViewData["PacienteId"] = new SelectList(_context.Paciente, "PacienteId", "PacienteId");
            ViewData["TratamientoId"] = new SelectList(_context.Set<Tratamiento>(), "TratamientoId", "TratamientoId");
            return View();
        }

        // POST: Facturas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FacturaId,PacienteId,TratamientoId,Monto,FechaPago")] Factura factura)
        {
            if (ModelState.IsValid)
            {
                _context.Add(factura);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PacienteId"] = new SelectList(_context.Paciente, "PacienteId", "PacienteId", factura.PacienteId);
            ViewData["TratamientoId"] = new SelectList(_context.Set<Tratamiento>(), "TratamientoId", "TratamientoId", factura.TratamientoId);
            return View(factura);
        }

        // GET: Facturas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var factura = await _context.Factura.FindAsync(id);
            if (factura == null)
            {
                return NotFound();
            }
            ViewData["PacienteId"] = new SelectList(_context.Paciente, "PacienteId", "PacienteId", factura.PacienteId);
            ViewData["TratamientoId"] = new SelectList(_context.Set<Tratamiento>(), "TratamientoId", "TratamientoId", factura.TratamientoId);
            return View(factura);
        }

        // POST: Facturas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("FacturaId,PacienteId,TratamientoId,Monto,FechaPago")] Factura factura)
        {
            if (id != factura.FacturaId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(factura);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FacturaExists(factura.FacturaId))
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
            ViewData["PacienteId"] = new SelectList(_context.Paciente, "PacienteId", "PacienteId", factura.PacienteId);
            ViewData["TratamientoId"] = new SelectList(_context.Set<Tratamiento>(), "TratamientoId", "TratamientoId", factura.TratamientoId);
            return View(factura);
        }

        // GET: Facturas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var factura = await _context.Factura
                .Include(f => f.Paciente)
                .Include(f => f.Tratamiento)
                .FirstOrDefaultAsync(m => m.FacturaId == id);
            if (factura == null)
            {
                return NotFound();
            }

            return View(factura);
        }

        // POST: Facturas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var factura = await _context.Factura.FindAsync(id);
            if (factura != null)
            {
                _context.Factura.Remove(factura);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FacturaExists(int id)
        {
            return _context.Factura.Any(e => e.FacturaId == id);
        }
    }
}
