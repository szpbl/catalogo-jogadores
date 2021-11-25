using ApiCatalagoJogadores.Entities;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCatalagoJogadores.Repositories
{
    public class JogadorSqlServerRepository : IJogadorRepository
    {
        private readonly SqlConnection sqlConnection;

        public JogadorSqlServerRepository(IConfiguration configuration)
        {
            sqlConnection = new SqlConnection(configuration.GetConnectionString("Default"));
        }

        public async Task<List<Jogador>> Obter(int pagina, int quantidade)
        {
            var jogadores = new List<Jogador>();

            var comando = $"select * from Jogadores order by id offset {((pagina - 1) * quantidade)} rows fetch next {quantidade} rows only";

            await sqlConnection.OpenAsync();
            SqlCommand sqlCommand = new(comando, sqlConnection);
            SqlDataReader sqlDataReader = await sqlCommand.ExecuteReaderAsync();

            while (sqlDataReader.Read())
            {
                jogadores.Add(new Jogador
                {
                    Id = (Guid)sqlDataReader["Id"],
                    Nome = (string)sqlDataReader["Nome"],
                    Idade = (int)sqlDataReader["Idade"],
                    Posicao = (Posicao)sqlDataReader["Posicao"],
                    Overall = (int)sqlDataReader["Overall"],
                    Time = (string)sqlDataReader["Time"],
                    Valor = (double)sqlDataReader["Valor"]
                });
            }

            await sqlConnection.CloseAsync();

            return jogadores;
        }

        public async Task<Jogador> Obter(Guid id)
        {
            Jogador jogador = null;

            var comando = $"select * from Jogadores where Id = '{id}'";

            await sqlConnection.OpenAsync();
            SqlCommand sqlCommand = new(comando, sqlConnection);
            SqlDataReader sqlDataReader = await sqlCommand.ExecuteReaderAsync();

            while (sqlDataReader.Read())
            {
                jogador = new Jogador
                {
                    Id = (Guid)sqlDataReader["Id"],
                    Nome = (string)sqlDataReader["Nome"],
                    Idade = (int)sqlDataReader["Idade"],
                    Posicao = (Posicao)sqlDataReader["Posicao"],
                    Overall = (int)sqlDataReader["Overall"],
                    Time = (string)sqlDataReader["Time"],
                    Valor = (double)sqlDataReader["Valor"]
                };
            }

            await sqlConnection.CloseAsync();

            return jogador;
        }

        public async Task<List<Jogador>> Obter(string nome, string time)
        {
            var jogadores = new List<Jogador>();

            var comando = $"select * from Jogadores where Nome = '{nome}' and Time = '{time}'";

            await sqlConnection.OpenAsync();
            SqlCommand sqlCommand = new(comando, sqlConnection);
            SqlDataReader sqlDataReader = await sqlCommand.ExecuteReaderAsync();

            while (sqlDataReader.Read())
            {
                jogadores.Add(new Jogador
                {
                    Id = (Guid)sqlDataReader["Id"],
                    Nome = (string)sqlDataReader["Nome"],
                    Idade = (int)sqlDataReader["Idade"],
                    Posicao = (Posicao)sqlDataReader["Posicao"],
                    Overall = (int)sqlDataReader["Overall"],
                    Time = (string)sqlDataReader["Time"],
                    Valor = (double)sqlDataReader["Valor"]
                });
            }

            await sqlConnection.CloseAsync();

            return jogadores;
        }

        public async Task Inserir(Jogador jogador)
        {
            var comando = $"insert Jogadores (Id, Nome, Idade, Posicao, Overall, Time, Valor) values ('{jogador.Id}', '{jogador.Nome}', {jogador.Idade}, {(int)jogador.Posicao}, {jogador.Overall}, '{jogador.Time}', {jogador.Valor.ToString().Replace(",", ".")})";

            await sqlConnection.OpenAsync();
            SqlCommand sqlCommand = new(comando, sqlConnection);
            sqlCommand.ExecuteNonQuery();
            await sqlConnection.CloseAsync();
        }

        public async Task Atualizar(Jogador jogador)
        {
            var comando = $"update Jogadores set Nome = '{jogador.Nome}', Idade = {jogador.Idade}, Posicao = {(int)jogador.Posicao}, Overall = {jogador.Overall}, Time = '{jogador.Time}', Valor = {jogador.Valor.ToString().Replace(",", ".")}";

            await sqlConnection.OpenAsync();
            SqlCommand sqlCommand = new(comando, sqlConnection);
            sqlCommand.ExecuteNonQuery();
            await sqlConnection.CloseAsync();
        }

        public async Task Remover(Guid id)
        {
            var comando = $"delete from Jogadores where Id = '{id}'";

            await sqlConnection.OpenAsync();
            SqlCommand sqlCommand = new(comando, sqlConnection);
            await sqlCommand.ExecuteNonQueryAsync();
            await sqlConnection.CloseAsync();
        }

        public void Dispose()
        {
            sqlConnection?.Close();
            sqlConnection?.Dispose();
        }
    }
}
