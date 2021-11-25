using ApiCatalagoJogadores.Exceptions;
using ApiCatalagoJogadores.InputModel;
using ApiCatalagoJogadores.Services;
using ApiCatalagoJogadores.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCatalagoJogadores.Controllers.V1
{
    [Route("api/V1/[controller]")]
    [ApiController]
    public class JogadoresController : ControllerBase
    {
        private readonly IJogadorService _jogadorService;

        public JogadoresController(IJogadorService jogadorService)
        {
            _jogadorService = jogadorService;
        }
        /// <summary>
        /// Buscar todos os jogadores de forma paginada
        /// </summary>
        /// <remarks>
        /// Não é possível retornar os jogadores sem paginação
        /// </remarks>
        /// <param name="pagina">Indica qual página está sendo consultada. Mínimo 1.</param>
        /// <param name="quantidade">Indica a quantidade de registros por página. Mínimo 1 e máximo 50.</param>
        /// <response code="200">Retorna a lista de jogadores</response>
        /// <response code="204">Caso a lista esteja vazia</response>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<JogadorViewModel>>> Obter([FromQuery, Range(1, int.MaxValue)] int pagina = 1, [FromQuery, Range(1, 50)] int quantidade = 5)
        {
            var jogadores = await _jogadorService.Obter(pagina, quantidade);

            if (jogadores.Count == 0)
                return NoContent();

            return Ok(jogadores);

        }

        /// <summary>
        /// Busca o jogador de acordo com o Id informado
        /// </summary>
        /// <param name="idJogador">Id do jogador buscado</param>
        /// <response code="200">Retorna o jogador filtrado</response>
        /// <response code="204">Caso não haja jogador com o id pesquisado</response>
        [HttpGet("{idJogador:guid}")]
        public async Task<ActionResult<JogadorViewModel>> Obter([FromRoute] Guid idJogador)
        {
            var jogador = await _jogadorService.Obter(idJogador);

            if (jogador == null)
                return NoContent();

            return Ok(jogador);

        }

        /// <summary>
        /// Insere novo jogador
        /// </summary>
        /// <param name="jogadorImputModel">Dados do jogador a ser inserido</param>
        /// <response code="200">Caso o jogador seja inserido com sucesso</response>
        /// <response code="422">Caso já exista um jogador com o mesmo nome no time</response>
        [HttpPost]
        public async Task<ActionResult<JogadorViewModel>> InserirJogador([FromBody] JogadorInputModel jogadorImputModel)
        {
            try
            {
                var jogador = await _jogadorService.Inserir(jogadorImputModel);
                return Ok(jogador);
            }
            catch (JogadorJaCadastradoException)
            {
                return UnprocessableEntity("O jogador já foi cadastrado.");
            }
        }

        /// <summary>
        /// Atualiza jogador já registrado
        /// </summary>
        /// <param name="idJogador">Id do jogador a ser atualizado</param>
        /// <param name="jogadorInputModel">Dados a serem inseridos no jogador</param>
        /// <response code="200">Caso o jogador seja atualizado com sucesso</response>
        /// <response code="404">Caso não exista um jogador com o Id informado</response>   
        [HttpPut("{idJogador:guid}")]
        public async Task<ActionResult> AtualizarJogador([FromRoute] Guid idJogador, [FromBody] JogadorInputModel jogadorInputModel)
        {
            try
            {
                await _jogadorService.Atualizar(idJogador, jogadorInputModel);

                return Ok();
            }
            catch (JogadorNaoCadastradoException)
            {
                return NotFound("O jogador não está cadastrado.");
            }
        }


        /// <summary>
        /// Atualiza o Overall de um jogador
        /// </summary>
        /// <param name="idJogador">Id do jogador a ser atualizado</param>
        /// <param name="overall">Novo Overall do jogador</param>
        /// <response code="200">Caso o jogador seja atualizado com sucesso</response>
        /// <response code="404">Caso não exista um jogador com o Id informado</response>  
        [HttpPatch("{idJogador:guid}/overall/{overall:int}")]
        public async Task<ActionResult> AtualizarJogador([FromRoute] Guid idJogador, [FromRoute] int overall)
        {
            try
            {
                await _jogadorService.Atualizar(idJogador, overall);

                return Ok();
            }
            catch (JogadorNaoCadastradoException)
            {
                return NotFound("O jogador não está cadastrado.");
            }
        }

        /// <summary>
        /// Remove jogador da lista
        /// </summary>
        /// <param name="idJogador">Id do jogador a ser removido</param>
        /// <response code="200">Caso o jogador seja removido com sucesso</response>
        /// <response code="404">Caso não exista um jogador com o Id informado</response>  
        [HttpDelete("{idJogador:guid}")]
        public async Task<ActionResult> ApagarJogador([FromRoute] Guid idJogador)
        {
            try
            {
                await _jogadorService.Remover(idJogador);

                return Ok();
            } 
            catch (JogadorNaoCadastradoException)
            {
                return NotFound("O jogador não está cadastrado");
            }
        }
    }
}
