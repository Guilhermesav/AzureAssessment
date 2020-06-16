using AzureAT.Dominio.Model.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AzureAT.Dominio.Model.Interface.Repositorios
{
    public interface IJogadorHistoricoRepository
    {
        Task<IEnumerable<JogadorHistoricoEntity>> GetByPartitionKeyAsync(string partitionKey);
        Task InsertAsync(JogadorHistoricoEntity jogadorHistoricoEntity);

    }
}
