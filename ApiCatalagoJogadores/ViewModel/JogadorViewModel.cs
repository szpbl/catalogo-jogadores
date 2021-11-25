using ApiCatalagoJogadores.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCatalagoJogadores.ViewModel
{
    public class JogadorViewModel
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public int Idade { get; set; }
        public int Overall { get; set; }
        public Posicao Posicao { get; set; }
        public string Time { get; set; } 
        public double Valor { get; set; }
    }


}
