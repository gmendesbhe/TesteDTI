using InterfacesConsole;
using System;
using System.Collections.Generic;
using System.Text;
using Apresentacao;
using System.Linq;

namespace Menu
{
    public class CadastroConsulta : ITelaConsole
    {
        private const int DIETA = 7;
        private const int PACIENTE = 2;
        private Dado[] _dados = new Dado[]
        {
            new Dado(){Descricao="Data"},
            new Dado(){Descricao="Hora"},
            new Dado(){Descricao="Paciente"},
            new Dado(){Descricao="Peso"},
            new Dado(){Descricao="Indice de Massa Corporal"},
            new Dado(){Descricao="Sensação Física"},
            new Dado(){Descricao="Restrições Alimentares"},
            new Dado(){Descricao="Dieta"},
        };
        private Guid _guid;
        private bool _dadoCompleto;
        private bool _editando;
        private Dado _dadoAtual;
        private string _erroSalvar;
        private Paciente _pacienteSelecionado;
        private IEnumerable<Alimento> _dietaSelecionada;
        private IPacienteDados _pacienteDados;
        private IConsultaDados _consultaDados;
        private IAlimentoDados _alimentoDados;
        private List<Paciente> _pacientes;
        private bool _escolhendoPaciente;
        private bool _escolhendoDieta;
        private List<IEnumerable<Alimento>> _dietas;
        private double? _maximoCalorico;
        private bool _informarMaximoCalorico;

        public CadastroConsulta(IPacienteDados pacienteDados, IConsultaDados consultaDados, IAlimentoDados alimentoDados)
        {
            _pacienteDados = pacienteDados;
            _consultaDados = consultaDados;
            _alimentoDados = alimentoDados;
        }

        public CadastroConsulta(IPacienteDados pacienteDados, IConsultaDados consultaDados, IAlimentoDados alimentoDados, Guid idConsulta) : this(pacienteDados, consultaDados, alimentoDados)
        {
            _guid = idConsulta;
            var consulta = consultaDados.Listar(idConsulta);
            _dados[0].Valor = consulta.Data;
            _dados[1].Valor = consulta.Hora;
            _dados[2].Valor = consulta.Paciente.Nome;
            _pacienteSelecionado = consulta.Paciente;
            _dados[3].Valor = consulta.Peso;
            _dados[4].Valor = consulta.IndiceMassaCorporal;
            _dados[5].Valor = consulta.SensacaoPaciente;
            _dados[6].Valor = consulta.RestricoesAlimentares;
            _dietaSelecionada = consulta.Dieta;
            _dados[7].Valor = DescreverDietaSelecionada();

        }

        void ITelaConsole.Renderizar()
        {
            for (int i = 0; i < _dados.Length; i++)
            {
                Console.WriteLine($"{i + 1} - {_dados[i].Descricao} {_dados[i].Valor}");
            }
            Console.WriteLine();
            if (_dadoCompleto && !_editando)
            {
                Console.WriteLine("Deseja salvar o registro? Digite [S]IM para salvar");
            }
            if (_editando)
            {
                RenderizarEdicao();
            }
            if (!string.IsNullOrWhiteSpace(_erroSalvar))
            {
                Console.WriteLine(_erroSalvar);
                _erroSalvar = null;
            }
        }

        private void RenderizarEdicao()
        {
            Console.WriteLine(_dadoAtual.Descricao);
            if (_escolhendoPaciente)
            {
                RenderizarPacientes();
            }
            if (_escolhendoDieta)
            {
                if (!_maximoCalorico.HasValue)
                {
                    Console.WriteLine("Informe a meta de consumo calórico");
                }
                else
                {
                    RenderizarDietas();
                }
            }
        }

        private void RenderizarPacientes()
        {
            for (int i = 0; i < _pacientes.Count; i++)
            {
                var paciente = _pacientes[i];
                Console.WriteLine($"{i + 1} - {paciente.Nome} - {paciente.DataNascimento}");
            }
        }

        private void RenderizarDietas()
        {
            for (int i = 0; i < _dietas.Count; i++)
            {
                var dieta = _dietas[i];
                Console.Write($"{i + 1} - ");
                foreach (var alimento in dieta)
                {
                    Console.Write($"{alimento.Descricao}; ");
                }
                Console.WriteLine($"Valor Calórico: { dieta.Sum(a => a.ValorCalorico)}");
            }
        }

        ITelaConsole ITelaConsole.TratarInput(string linha)
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
                        _guid = _consultaDados.Salvar(new Consulta(_guid)
                        {
                            Data = _dados[0].Valor,
                            Hora = _dados[1].Valor,
                            Peso = _dados[3].Valor,
                            IndiceMassaCorporal = _dados[4].Valor,
                            SensacaoPaciente = _dados[5].Valor,
                            RestricoesAlimentares = _dados[6].Valor,
                            Paciente = _pacienteSelecionado,
                            Dieta = _dietaSelecionada
                        });
                    }
                    catch (Exception ex) when (ex is ArgumentException || ex is InvalidOperationException)
                    {
                        _erroSalvar = ex.Message; ;
                    }
                }
            }
        }

        private void VerificarEdicao(string linha)
        {
            if (_editando)
            {
                EditarDado(linha);
            }
            else if (int.TryParse(linha, out int opcao) && opcao - 1 < _dados.Length && opcao - 1 >= 0)
            {
                _editando = true;

                _dadoAtual = _dados[opcao - 1];

                if (opcao - 1 == PACIENTE)
                {
                    _pacientes = _pacienteDados.Listar().ToList();
                    _escolhendoPaciente = true;
                }

                if (opcao - 1 == DIETA)
                {
                    _escolhendoDieta = true;
                    if (_maximoCalorico.HasValue)
                    {
                        _dietas = _alimentoDados.ListarCombinacoes(_maximoCalorico.Value).ToList();
                        _maximoCalorico = null;
                    }
                    else
                    {
                        _informarMaximoCalorico = true;
                    }
                }
            }
            _dadoCompleto = !_dados
                .Where(d => d.Descricao != "Sensação Física" && d.Descricao != "Dieta")
                .Any(d => string.IsNullOrEmpty(d.Valor));
        }

        private void EditarDado(string linha)
        {
            _editando = false;

            if (_escolhendoPaciente)
            {
                TratarSelecaoPaciente(linha);
            }
            else if (_escolhendoDieta)
            {
                TratarSelecaoDieta(linha);
            }
            else
            {
                _dadoAtual.Valor = linha;
            }
        }

        private void TratarSelecaoDieta(string linha)
        {
            if (string.IsNullOrWhiteSpace(linha))
            {
                CancelarEdicaoDieta();
                return;
            }
            if (_informarMaximoCalorico)
            {
                _maximoCalorico = Convert.ToDouble(linha);
                _dietas = _alimentoDados.ListarCombinacoes(_maximoCalorico.Value).ToList();
                _informarMaximoCalorico = false;
                _editando = true;
            }
            else if (int.TryParse(linha, out int opcao) && opcao - 1 < _dietas.Count && opcao - 1 >= 0)
            {
                _dietaSelecionada = _dietas[opcao - 1];
                _dadoAtual.Valor = DescreverDietaSelecionada();
                _escolhendoDieta = false;
            }
            else
            {
                _escolhendoDieta = false;
            }
        }

        private string DescreverDietaSelecionada()
        {
            if (_dietaSelecionada is null ||_dietaSelecionada.Count()==0)
            {
                return string.Empty;
            }
            var alimentos = string.Join("; ", _dietaSelecionada.Select(a => $"{a.Descricao}"));
            return string.Join("; ", alimentos, _dietaSelecionada.Sum(a => a.ValorCalorico));
        }

        private void CancelarEdicaoDieta()
        {
            _informarMaximoCalorico = false;
            _escolhendoDieta = false;
            _maximoCalorico = null;
        }

        private void TratarSelecaoPaciente(string linha)
        {
            if (int.TryParse(linha, out int opcao) && opcao - 1 < _pacientes.Count && opcao - 1 >= 0)
            {
                _pacienteSelecionado = _pacientes[opcao - 1];
                _dadoAtual.Valor = _pacienteSelecionado.Nome;
            }
            _escolhendoPaciente = false;
        }
    }
}
