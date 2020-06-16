using AzureAT.Dominio.Model.Entity;
using AzureAT.Dominio.Model.Interface.Infraestrutura;
using AzureAT.Dominio.Model.Interface.Repositorios;
using AzureAT.Dominio.Model.Interface.Servicos;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace AzureAT.Dominio.Service.Services
{
    public class JogadorService : IJogadorService
    {
        private readonly IJogadorRepository _repository;
        private readonly IQueueMessage _queueService;
        private readonly IBlobService _blobService;
        private readonly IJogadorHistoricoRepository _historicoRepository;

        public JogadorService(IJogadorRepository repository,IBlobService blobService, IQueueMessage queueService, IJogadorHistoricoRepository historicoRepository)
        {
            _repository = repository;
            _queueService = queueService;
            _blobService = blobService;
            _historicoRepository = historicoRepository;
        }

        public async Task<IEnumerable<JogadorHistoricoEntity>> GetHistoricoAsync(string id)
        {
            return await _historicoRepository.GetByPartitionKeyAsync(id);
        }

        public async Task<IEnumerable<JogadorEntity>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<JogadorEntity> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task InsertAsync(JogadorEntity jogadorEntity)
        {
            await _repository.InsertAsync(jogadorEntity);

            var message = new
            {
                ImageURI = jogadorEntity.ImageUri,
                Id = $"{jogadorEntity.Id}",
            };

            var jsonMessage = JsonConvert.SerializeObject(message);
            var bytesJsonMessage = UTF8Encoding.UTF8.GetBytes(jsonMessage);
            string jsonMessageBase64 = Convert.ToBase64String(bytesJsonMessage);

            await _queueService.SendAsync(jsonMessageBase64);   

            await _historicoRepository.InsertAsync(new JogadorHistoricoEntity(jogadorEntity));
        }

        public async Task UpdateAsync(JogadorEntity jogadorEntity,Stream stream)
        {
            if (stream != null)
            {
                await _blobService.DeleteAsync(jogadorEntity.ImageUri);

                var newUri = await _blobService.UploadAsync(stream);
                jogadorEntity.ImageUri = newUri;
            }
            await _repository.UpdateAsync(jogadorEntity);

            await _historicoRepository.InsertAsync(new JogadorHistoricoEntity(jogadorEntity));
        }

        public async Task DeleteAsync(JogadorEntity jogadorEntity)
        {
            await _repository.DeleteAsync(jogadorEntity);
        }

    }
}

