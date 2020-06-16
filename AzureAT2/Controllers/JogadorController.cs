using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AzureAT.Dominio.Model.Entity;
using AzureAT.Infraestrutura.Data.Context;
using AzureAT.Dominio.Model.Interface.Servicos;
using System.IO;

namespace AzureAT2.Controllers
{
    public class JogadorController : Controller
    {
        private readonly IJogadorService _domainService;

        public JogadorController(IJogadorService domainService)
        {
            _domainService = domainService;
        }


        public async Task<IActionResult> Historico(string pesquisa)
        {
            if (pesquisa == null)
            {
                return View(null);
            }
            else
            {
                return View(await _domainService.GetHistoricoAsync(pesquisa));
            }
        }


        // GET: Amigo
        public async Task<IActionResult> Index()
        {
            return View(await _domainService.GetAllAsync());
        }

        // GET: Amigo/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var amigoEntity = await _domainService.GetByIdAsync(id.Value);

            if (amigoEntity == null)
            {
                return NotFound();
            }

            return View(amigoEntity);
        }

        // GET: Amigo/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Amigo/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(JogadorEntity jogadorEntity)
        {
            if (ModelState.IsValid)
            {
                var image = jogadorEntity.ImageUri;

                
                await _domainService.InsertAsync(jogadorEntity);

                return RedirectToAction(nameof(Index));
            }
            return View(jogadorEntity);
        }

        // GET: Amigo/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var amigoEntity = await _domainService.GetByIdAsync(id.Value);
            if (amigoEntity == null)
            {
                return NotFound();
            }
            return View(amigoEntity);
        }

        // POST: Amigo/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, JogadorEntity jogadorEntity)
        {
            if (id != jogadorEntity.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var file = Request.Form.Files.SingleOrDefault();

                    await _domainService.UpdateAsync(jogadorEntity, file?.OpenReadStream());
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AmigoEntityExists(jogadorEntity.Id))
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

        // GET: Amigo/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var amigoEntity = await _domainService.GetByIdAsync(id.Value);
            if (amigoEntity == null)
            {
                return NotFound();
            }

            return View(amigoEntity);
        }

        // POST: Amigo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var amigoEntity = await _domainService.GetByIdAsync(id);
            await _domainService.DeleteAsync(amigoEntity);

            return RedirectToAction(nameof(Index));
        }

        private bool AmigoEntityExists(int id)
        {
            return _domainService.GetByIdAsync(id) != null;
        }

    }
}
