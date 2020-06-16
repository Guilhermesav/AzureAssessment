using AzureAT.Dominio.Model.Entity;
using AzureAT.Dominio.Model.Interface.Repositorios;
using Microsoft.Azure.Cosmos.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureAT.Infraestrutura.Data.Repositorios
{
    public class JogadorHistoricoRepository : IJogadorHistoricoRepository
    {
        private readonly CloudStorageAccount _cloudStorageAccount;
        private const string _table = "jogador";

        public JogadorHistoricoRepository(string storageAccount)
        {
            _cloudStorageAccount = CloudStorageAccount.Parse(storageAccount);
        }

        public async Task<IEnumerable<JogadorHistoricoEntity>> GetByPartitionKeyAsync(string partitionKey)
        {
            CloudTableClient tableClient = _cloudStorageAccount.CreateCloudTableClient(new TableClientConfiguration());

            CloudTable table = tableClient.GetTableReference(_table);

            var linqQuery = table.CreateQuery<JogadorHistoricoEntity>().Where(x => x.PartitionKey == partitionKey
                                                               );
            return await Task.Run(() => linqQuery.ToList());
        }

        public async Task InsertAsync(JogadorHistoricoEntity jogadorHistoricoEntity)
        {
            CloudTableClient tableClient = _cloudStorageAccount.CreateCloudTableClient(new TableClientConfiguration());

            CloudTable table = tableClient.GetTableReference(_table);

            table.CreateIfNotExists();

            var insertOperation = TableOperation.InsertOrReplace(jogadorHistoricoEntity);

            _ = await table.ExecuteAsync(insertOperation);
        }
    }
}

