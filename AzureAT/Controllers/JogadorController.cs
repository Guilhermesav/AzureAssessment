using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AzureAT.Dominio.Model.Entity;
using AzureAT.Infraestrutura.Data.Context;

namespace AzureAT.Controllers
{
    public class JogadorController : Controller
    {
        private readonly JogadorContext _context;

        public JogadorController(JogadorContext context)
        {
            _context = context;
        }

        // GET: Jogador
        public async Task<IActionResult> Index()
        {
            return View(await _context.JogadorEntity.ToListAsync());
        }

        // GET: Jogador/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jogadorEntity = await _context.JogadorEntity
                .FirstOrDefaultAsync(m => m.Id == id);
            if (jogadorEntity == null)
            {
                return NotFound();
            }

            return View(jogadorEntity);
        }

        // GET: Jogador/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Jogador/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome,Time,FreeAgent,ImageUri")] JogadorEntity jogadorEntity)
        {
            if (ModelState.IsValid)
            {
                _context.Add(jogadorEntity);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(jogadorEntity);
        }

        // GET: Jogador/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jogadorEntity = await _context.JogadorEntity.FindAsync(id);
            if (jogadorEntity == null)
            {
                return NotFound();
            }
            return View(jogadorEntity);
        }

        // POST: Jogador/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,Time,FreeAgent,ImageUri")] JogadorEntity jogadorEntity)
        {
            if (id != jogadorEntity.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(jogadorEntity);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!JogadorEntityExists(jogadorEntity.Id))
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
            return View(jogadorEntity);
        }

        // GET: Jogador/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jogadorEntity = await _context.JogadorEntity
                .FirstOrDefaultAsync(m => m.Id == id);
            if (jogadorEntity == null)
            {
                return NotFound();
            }

            return View(jogadorEntity);
        }

        // POST: Jogador/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var jogadorEntity = await _context.JogadorEntity.FindAsync(id);
            _context.JogadorEntity.Remove(jogadorEntity);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool JogadorEntityExists(int id)
        {
            return _context.JogadorEntity.Any(e => e.Id == id);
        }
    }
}
