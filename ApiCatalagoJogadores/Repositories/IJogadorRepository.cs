using ApiCatalagoJogadores.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCatalagoJogadores.Repositories
{
    public interface IJogadorRepository : IDisposable
    {
        Task<List<Jogador>> Obter(int pagina, int quantidade);
        Task<Jogador> Obter(Guid id);
        Task<List<Jogador>> Obter(string nome, string time);
        Task Inserir(Jogador jogador);
        Task Atualizar(Jogador jogador);
        Task Remover(Guid id);

    }
}
