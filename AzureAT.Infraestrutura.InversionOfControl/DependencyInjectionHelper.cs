using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using AzureAT.Infraestrutura.Data.Context;
using AzureAT.Dominio.Service.Services;
using AzureAT.Dominio.Model.Interface.Repositorios;
using AzureAT.Dominio.Model.Interface.Servicos;
using AzureAT.Dominio.Model.Interface.Infraestrutura;
using AzureAT.Infraestrutura.Data.Repositorios;
using AzureAT.Infraestrtura.Services.Queue;
using AzureAT.Infraestrtura.Services.Blob;

namespace AzureAT.Infraestrutura.InversionOfControl
{
    public class DependencyInjectionHelper
    {
        public static void Register(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<JogadorContext>(options =>
                    options.UseSqlServer(configuration.GetConnectionString("JogadorContext")));

            services.AddScoped<IJogadorRepository, JogadorRepository>();
            services.AddScoped<IJogadorService, JogadorService>();

            services.AddScoped<IBlobService, BlobService>(provider =>
            new BlobService(configuration.GetValue<string>("ConnectionStringStorageAccount")));

            services.AddScoped<IQueueMessage, QueueMessage>(provider =>
                new QueueMessage(configuration.GetValue<string>("ConnectionStringStorageAccount")));

             services.AddScoped<IJogadorHistoricoRepository, JogadorHistoricoRepository>(provider =>
               new JogadorHistoricoRepository(configuration.GetValue<string>("ConnectionStringStorageAccount")));
        }
    }
}
