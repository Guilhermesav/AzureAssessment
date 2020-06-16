using AzureAT.Dominio.Model.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore.SqlServer;

namespace AzureAT.Infraestrutura.Data.Context
{
    public class JogadorContext : DbContext
    {
       
        public JogadorContext(DbContextOptions<JogadorContext> options)
            : base(options)
        {
          
        }

        public DbSet<JogadorEntity> JogadorEntity { get; set; }
    }
}
