using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FilmesMVC.Models.Enuns;

namespace FilmesMVC.Models
{
    public class FilmeViewModel
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;

        //FAZER DESCRIÇÃO

        public int AnoLancamento { get; set; }
        public decimal Bilheteria { get; set; }
        public TimeSpan Duracao { get; set; }
        public ClassificacaoEnum Classificacao { get; set; }
        public GeneroEnum Genero { get; set; }
    }
}