using AzureAT.Dominio.Model.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AzureAT.Dominio.Model.Interface.Repositorios
{
    public interface IJogadorRepository
    {
        Task<IEnumerable<JogadorEntity>> GetAllAsync();
        Task<JogadorEntity> GetByIdAsync(int id);
        Task InsertAsync(JogadorEntity jogadorEntity);
        Task UpdateAsync(JogadorEntity jogadorEntity);
        Task DeleteAsync(JogadorEntity jogadorEntity);

    }
}
