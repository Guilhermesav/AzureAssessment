using AzureAT.Dominio.Model.Entity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace AzureAT.Dominio.Model.Interface.Servicos
{
    public interface IJogadorService
    {
        Task<IEnumerable<JogadorEntity>> GetAllAsync();
        Task<JogadorEntity> GetByIdAsync(int id);
        Task InsertAsync(JogadorEntity jogadorEntity);
        Task UpdateAsync(JogadorEntity jogadorEntity,string novaImagem);
        Task DeleteAsync(JogadorEntity jogadorEntity);

        Task<IEnumerable<JogadorHistoricoEntity>> GetHistoricoAsync(string id);
    }
}
