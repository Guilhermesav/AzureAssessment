using Microsoft.Azure.Cosmos.Table;
using System;
using System.Collections.Generic;
using System.Text;

namespace AzureAT.Dominio.Model.Entity
{
    public class JogadorHistoricoEntity : TableEntity
    {
        public JogadorHistoricoEntity() { }

        public JogadorHistoricoEntity(JogadorEntity jogadorEntity)
        {
            PartitionKey = jogadorEntity.Id.ToString();
            RowKey = Guid.NewGuid().ToString();
            Nome = jogadorEntity.Nome;
            Time = jogadorEntity.Time;
            FreeAgent = jogadorEntity.FreeAgent;
        }


        public string Nome { get; set; }
        public string Time { get; set; }
        public bool FreeAgent { get; set; }
        
    }
}
