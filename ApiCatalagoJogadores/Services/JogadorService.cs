using ApiCatalagoJogadores.Entities;
using ApiCatalagoJogadores.Exceptions;
using ApiCatalagoJogadores.InputModel;
using ApiCatalagoJogadores.Repositories;
using ApiCatalagoJogadores.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCatalagoJogadores.Services
{
    public class JogadorService : IJogadorService
    {
        private readonly IJogadorRepository _jogadorRepository;

        public JogadorService(IJogadorRepository jogadorRepository)
        {
            _jogadorRepository = jogadorRepository;
        }

        public async Task<List<JogadorViewModel>> Obter(int pagina, int quantidade)
        {
            var jogadores = await _jogadorRepository.Obter(pagina, quantidade);

            return jogadores.Select(jogador => new JogadorViewModel
            {
                Id = jogador.Id,
                Nome = jogador.Nome,
                Idade = jogador.Idade,
                Overall = jogador.Overall,
                Posicao = jogador.Posicao,
                Time = jogador.Time,
                Valor = jogador.Valor
            }).ToList();
        } 

        public async Task<JogadorViewModel> Obter(Guid id)
        {
            var jogador = await _jogadorRepository.Obter(id);

            if (jogador == null)
                return null;

            return new JogadorViewModel
            {
                Id = jogador.Id,
                Nome = jogador.Nome,
                Idade = jogador.Idade,
                Overall = jogador.Overall,
                Posicao = jogador.Posicao,
                Time = jogador.Time,
                Valor = jogador.Valor
            };
        }

        public async Task<JogadorViewModel> Inserir(JogadorInputModel jogador)
        {
            var entidadeJogador = await _jogadorRepository.Obter(jogador.Nome, jogador.Time);

            if (entidadeJogador.Count > 0)
                throw new JogadorJaCadastradoException();
            

            var jogadorInsert = new Jogador
            {
                Id = Guid.NewGuid(),
                Nome = jogador.Nome,
                Idade = jogador.Idade,
                Overall = jogador.Overall,
                Posicao = jogador.Posicao,
                Time = jogador.Time,
                Valor = jogador.Valor
            };

            await _jogadorRepository.Inserir(jogadorInsert);

            return new JogadorViewModel
            {
                Id = jogadorInsert.Id,
                Nome = jogador.Nome,
                Idade = jogador.Idade,
                Overall = jogador.Overall,
                Posicao = jogador.Posicao,
                Time = jogador.Time,
                Valor = jogador.Valor

            };
        }
        public async Task Atualizar(Guid id, JogadorInputModel jogador)
        {
            var entidadeJogador = await _jogadorRepository.Obter(id);

            if (entidadeJogador == null)
                throw new JogadorNaoCadastradoException();

            entidadeJogador.Nome = jogador.Nome;
            entidadeJogador.Idade = jogador.Idade;
            entidadeJogador.Overall = jogador.Overall;
            entidadeJogador.Posicao = jogador.Posicao;
            entidadeJogador.Time = jogador.Time;
            entidadeJogador.Valor = jogador.Valor;

            await _jogadorRepository.Atualizar(entidadeJogador);
        }

        public async Task Atualizar(Guid id, int overall)
        {
            var entidadeJogador = await _jogadorRepository.Obter(id);

            if (entidadeJogador == null)
                throw new JogadorNaoCadastradoException();

            entidadeJogador.Overall = overall;

            await _jogadorRepository.Atualizar(entidadeJogador);
        }

        public async Task Remover(Guid id)
        {
            var jogador = await _jogadorRepository.Obter(id);

            if (jogador == null)
                throw new JogadorNaoCadastradoException();

            await _jogadorRepository.Remover(id);
        }

        public void Dispose()
        {
            _jogadorRepository?.Dispose();
        }

    }



}
