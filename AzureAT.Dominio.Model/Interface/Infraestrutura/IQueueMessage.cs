using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AzureAT.Dominio.Model.Interface.Infraestrutura
{
    public interface IQueueMessage
    {
        Task SendAsync(string messageText);
    }
}
