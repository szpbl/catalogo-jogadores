using ApiCatalagoJogadores.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCatalagoJogadores.InputModel
{
    public class JogadorInputModel
    {
        [Required]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "O nome do jogador deve conter entre 3 e 100 caracteres.")]
        public string Nome { get; set; }
        [Required]
        [Range(15, 45, ErrorMessage = "A idade do jogador deve estar entre 15 e 45 anos.")]
        public int Idade { get; set; }
        [Required]
        [Range(1, 99, ErrorMessage = "O overral do jogador deve estar entre 1 e 99.")]
        public int Overall { get; set; }
        [Required]
        public Posicao Posicao { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "O nome do time deve conter entre 3 e 100 caracteres.")]
        public string Time { get; set; }
        [Required]
        [Range(1,1500000000, ErrorMessage = "O valor do jogador deve ser entre 1 e 1.500.000.000 da moeda escolhida.")]
        public double Valor { get; set; }
    }
}
