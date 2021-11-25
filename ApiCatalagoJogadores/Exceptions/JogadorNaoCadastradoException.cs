using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCatalagoJogadores.Exceptions
{
    public class JogadorNaoCadastradoException : Exception
    {
        public JogadorNaoCadastradoException()
            :base("Este jogador não está cadastrado.")
        { }
    }
}
