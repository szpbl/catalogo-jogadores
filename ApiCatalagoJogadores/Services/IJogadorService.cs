using ApiCatalagoJogadores.InputModel;
using ApiCatalagoJogadores.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCatalagoJogadores.Services
{
    public interface IJogadorService : IDisposable
    {
        Task<List<JogadorViewModel>> Obter(int pagina, int quantidade);
        Task<JogadorViewModel> Obter(Guid id);
        Task<JogadorViewModel> Inserir(JogadorInputModel jogador);
        Task Atualizar(Guid id, JogadorInputModel jogador);
        Task Atualizar(Guid id, int overall);
        Task Remover(Guid id);

    }
}
