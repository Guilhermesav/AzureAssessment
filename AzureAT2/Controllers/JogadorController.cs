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


   
        public async Task<IActionResult> Index()
        {
            return View(await _domainService.GetAllAsync());
        }

     
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jogadorEntity = await _domainService.GetByIdAsync(id.Value);

            if (jogadorEntity == null)
            {
                return NotFound();
            }

            return View(jogadorEntity);
        }

        public IActionResult Create()
        {
            return View();
        }

       
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

       
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jogadorEntity = await _domainService.GetByIdAsync(id.Value);
            string imagemAntiga = jogadorEntity.ImageUri;

            if (jogadorEntity == null)
            {
                return NotFound();
            }
            return View(jogadorEntity);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, JogadorEntity jogadorEntity,string imagemAntiga)
        {
            if (id != jogadorEntity.Id)
            {
                return NotFound();
            }
                
            if (ModelState.IsValid)
            {
                try
                {
                    await _domainService.UpdateAsync(jogadorEntity,imagemAntiga);
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

            var jogadorEntity = await _domainService.GetByIdAsync(id.Value);
            if (jogadorEntity == null)
            {
                return NotFound();
            }

            return View(jogadorEntity);
        }

        // POST: Amigo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var jogadorEntity = await _domainService.GetByIdAsync(id);
            await _domainService.DeleteAsync(jogadorEntity);

            return RedirectToAction(nameof(Index));
        }

        private bool AmigoEntityExists(int id)
        {
            return _domainService.GetByIdAsync(id) != null;
        }

    }
}
