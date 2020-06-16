using AzureAT.Dominio.Model.Entity;
using AzureAT.Dominio.Model.Interface.Repositorios;
using AzureAT.Infraestrutura.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AzureAT.Infraestrutura.Data.Repositorios
{
    public class JogadorRepository : IJogadorRepository
    {
        private readonly JogadorContext _context;

        public JogadorRepository(JogadorContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<JogadorEntity>> GetAllAsync()
        {
            return await _context.JogadorEntity.ToListAsync();
        }

        public async Task<JogadorEntity> GetByIdAsync(int id)
        {
            return await _context.JogadorEntity.SingleOrDefaultAsync(m => m.Id == id);
        }

        public async Task InsertAsync(JogadorEntity jogadorEntity)
        {
            _context.Add(jogadorEntity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(JogadorEntity jogadorEntity)
        {
            _context.Update(jogadorEntity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(JogadorEntity jogadorEntity)
        {
            _context.Remove(jogadorEntity);
            await _context.SaveChangesAsync();
        }
    }
}
