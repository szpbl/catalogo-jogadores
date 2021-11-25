using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCatalagoJogadores.Exceptions
{
    public class JogadorJaCadastradoException : Exception
    {
        public JogadorJaCadastradoException()
            : base("Este jogador já está cadastrado.")
        { }
    }
}
