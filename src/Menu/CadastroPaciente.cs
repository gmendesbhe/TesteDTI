using InterfacesConsole;
using Apresentacao;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Dados;

namespace Menu
{
    public class CadastroPaciente : ITelaConsole
    {
        
        private IPacienteDados _pacienteDados;
        private Dado _dadoAtual;
        private bool _dadoCompleto;
        private bool _editando;
        private string _erroSalvar;

        private Dado[] _dados = new Dado[5] {
            new Dado(){Descricao="Nome"},
            new Dado(){Descricao="Endereco"},
            new Dado(){Descricao="Telefones"},
            new Dado(){Descricao="Email"},
            new Dado(){Descricao="Data de nascimento"}
        };
        private Guid _guid;

        public CadastroPaciente(IPacienteDados pacienteDados)
        {
            _pacienteDados = pacienteDados;
        }
        public CadastroPaciente(IPacienteDados pacienteDados, Guid idParticipante):this(pacienteDados)
        {
            _guid = idParticipante;
            var paciente = pacienteDados.Listar(idParticipante);
            _dados[0].Valor = paciente.Nome;
            _dados[1].Valor = paciente.Endereco;
            _dados[2].Valor = paciente.Telefones;
            _dados[3].Valor = paciente.Email;
            _dados[4].Valor = paciente.DataNascimento;
        }
        void ITelaConsole.Renderizar()
        {
            for (int i = 0; i < _dados.Length; i++)
            {
                Console.WriteLine($"{i+1} - {_dados[i].Descricao} {_dados[i].Valor}");
            }
            if (_dadoCompleto && !_editando)
            {
                Console.WriteLine("Deseja salvar o registro? Digite [S]IM para salvar");
            }
            if (_editando)
            {
                Console.WriteLine(_dadoAtual.Descricao);
            }
            if (!string.IsNullOrWhiteSpace(_erroSalvar))
            {
                Console.WriteLine(_erroSalvar);
                _erroSalvar = null;
            }
        }

        public ITelaConsole TratarInput(string linha)
        {
            VerificarEdicao(linha);
            VerificarSalvar(linha);
            return null;
        }

        private void VerificarSalvar(string linha)
        {
            if (_dadoCompleto)
            {
                var linhaUpper = linha.ToUpperInvariant();
                if (linhaUpper == Utilitario.Constantes.S || linhaUpper == Utilitario.Constantes.SIM)
                {
                    try
                    {
                        _guid = _pacienteDados.Salvar(new Paciente(_guid)
                        {
                            DataNascimento = _dados[4].Valor,
                            Email = _dados[3].Valor,
                            Endereco = _dados[1].Valor,
                            Telefones = _dados[2].Valor,
                            Nome = _dados[0].Valor
                        });
                    }
                    catch (Exception ex) when (ex is ArgumentException || ex is InvalidOperationException)
                    {
                        _erroSalvar = ex.Message;
                    }
                }
            }
        }

        private void VerificarEdicao(string linha)
        {
            if (_editando)
            {
                _editando = false;
                _dadoAtual.Valor = linha;
            }
            else if (int.TryParse(linha, out int opcao) && opcao-1 < _dados.Length && opcao-1 >= 0)
            {
                _editando = true;
                _dadoAtual = _dados[opcao-1];
            }
            _dadoCompleto = !_dados.Any(d => string.IsNullOrEmpty(d.Valor));
        }
    }
}
