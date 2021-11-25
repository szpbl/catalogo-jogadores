using ApiCatalagoJogadores.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCatalagoJogadores.Repositories
{
    public class JogadorRepository : IJogadorRepository
    {
        private readonly static Dictionary<Guid, Jogador> jogadores = new()
        {
            {Guid.Parse("1b4adff5-e41b-4f4d-97af-12a8f0f9fb90"), new Jogador{Id = Guid.Parse("1b4adff5-e41b-4f4d-97af-12a8f0f9fb90"), Nome = "Cristiano Ronaldo", Idade = 36, Posicao = Posicao.ATA, Overall = 91, Time = "Manchester United", Valor = 38.7} },
            {Guid.Parse("744502a7-dedb-45bf-8aa1-294a46d23ebb"), new Jogador{Id = Guid.Parse("744502a7-dedb-45bf-8aa1-294a46d23ebb"), Nome = "Lionel Messi", Idade = 34, Posicao = Posicao.PD, Overall = 93, Time = "Paris Saint-Germain", Valor = 67.1} },
            {Guid.Parse("4fd6d8a9-badf-4ee0-b779-79a387c694ca"), new Jogador{Id = Guid.Parse("4fd6d8a9-badf-4ee0-b779-79a387c694ca"), Nome = "Mason Mount", Idade = 22, Posicao = Posicao.MEI, Overall = 83, Time = "Chelsea", Valor = 50.3} },
            {Guid.Parse("907b3737-2cf5-47ae-bab2-47c626a17387"), new Jogador{Id = Guid.Parse("907b3737-2cf5-47ae-bab2-47c626a17387"), Nome = "Vinícius Júnior", Idade = 20, Posicao = Posicao.PE, Overall = 80, Time = "Real Madrid", Valor = 40.0} },
            {Guid.Parse("7db7e1ff-4723-47e9-a2df-d6c3e3bfd36d"), new Jogador{Id = Guid.Parse("7db7e1ff-4723-47e9-a2df-d6c3e3bfd36d"), Nome = "Jan Oblak", Idade = 28, Posicao = Posicao.GOL, Overall = 91, Time = "Atlético de Madrid", Valor = 96.3} },
            {Guid.Parse("c4a123ba-b4f5-4d96-8ba8-8b56df8367f7"), new Jogador{Id = Guid.Parse("c4a123ba-b4f5-4d96-8ba8-8b56df8367f7"), Nome = "Erling Haaland", Idade = 20, Posicao = Posicao.ATA, Overall = 88, Time = "Borussia Dortmund", Valor = 123.4} },

        };
        public Task<List<Jogador>> Obter(int pagina, int quantidade)
        {
            return Task.FromResult(jogadores.Values.Skip((pagina - 1) * quantidade).Take(quantidade).ToList());
        }

        public Task<Jogador> Obter(Guid id)
        {
            if (!jogadores.ContainsKey(id))
                return null;

            return Task.FromResult(jogadores[id]);
        }

        public Task<List<Jogador>> Obter(string nome, string time)
        {
            return Task.FromResult(jogadores.Values.Where(jogador => jogador.Nome.Equals(nome) && jogador.Time.Equals(time)).ToList());
        }

        public Task Inserir(Jogador jogador)
        {
            jogadores.Add(jogador.Id, jogador);
            return Task.CompletedTask;
        }

        public Task Atualizar(Jogador jogador)
        {
            jogadores[jogador.Id] = jogador;
            return Task.CompletedTask;
        }

        public Task Remover(Guid id)
        {
            jogadores.Remove(id);
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            
        }
    }
}
