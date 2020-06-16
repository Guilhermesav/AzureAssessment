using System;
using System.Collections.Generic;
using System.Text;
using Azure.Storage.Queues;
using AzureAT.Dominio.Model.Interface.Infraestrutura;

namespace AzureAT.Infraestrtura.Services.Queue
{
    public class QueueMessage : IQueueMessage
    {
        private readonly QueueServiceClient _queueServiceClient;
        private const string _queueName = "queue-image-insert";

        public QueueMessage(string storageAccount)
        {
            _queueServiceClient = new QueueServiceClient(storageAccount);
        } 

        public async System.Threading.Tasks.Task SendAsync(string messageText)
        {
            var queueClient = _queueServiceClient.GetQueueClient(_queueName);

            await queueClient.CreateIfNotExistsAsync();

            await queueClient.SendMessageAsync(messageText);
        }
    }
}

