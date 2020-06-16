using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AzureAT.Dominio.Model.Entity
{
    [Table("Jogador")]
    public class JogadorEntity
    {
        public int Id { get; set; }

        public string Nome { get; set; }

        public string Time { get; set; }

        public bool FreeAgent { get; set; }

        [DisplayName("Foto")]
        public string ImageUri { get; set; }
    }
}
