using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using InterfacesConsole;
using Apresentacao;
using Modelos;

namespace Dados
{
    public class ConsultaDados : IConsultaDados
    {
        private List<Modelos.Consulta> _consultas;
        private ValidadorConsulta _validador;
        private PacienteDados _pacienteDados;

        public ConsultaDados(ValidadorConsulta validador, PacienteDados pacienteDados)
        {
            _consultas = new List<Modelos.Consulta>();
            _validador = validador;
            _pacienteDados = pacienteDados;
        }

        public Guid Salvar(Apresentacao.Consulta consulta)
        {
            var dataHora = _validador.ValidarDataHora(consulta.Data, consulta.Hora);
            var imc = _validador.ValidarImc(consulta.IndiceMassaCorporal);
            var peso = _validador.ValidarPeso(consulta.Peso);

            var dieta = consulta.Dieta?.Select(a => new Modelos.Alimento()
            {
                Descricao = a.Descricao,
                Grupo = a.Grupo,
                ValorCalorico = a.ValorCalorico
            });
            var paciente = _pacienteDados.ListarModelo(consulta.Paciente.Id);

            Guid guidPadrao = Guid.Empty;
            if (consulta.Id == guidPadrao)
            {
                return AdicionarConsulta(consulta, dataHora, imc, peso, dieta, paciente);
            }
            else
            {
                return AtualizarConsulta(consulta, dataHora, imc, peso, dieta, paciente);
            }

        }

        private Guid AtualizarConsulta(Apresentacao.Consulta consulta, DateTime dataHora, double imc, double peso, IEnumerable<Modelos.Alimento> dieta, Modelos.Paciente paciente)
        {
            var consultaSalva = _consultas.Where(c => c.Id == consulta.Id).FirstOrDefault();

            if (consultaSalva is null)
            {
                throw new InvalidOperationException("Consulta não encontrada");
            }

            consultaSalva.IndiceMassaCorporal = imc;
            consultaSalva.Peso =peso;
            consultaSalva.RestricoesAlimentares = consulta.RestricoesAlimentares;
            consultaSalva.SensacaoPaciente = consulta.SensacaoPaciente;
            consultaSalva.DataHora = dataHora;
            consultaSalva.Paciente = paciente;
            consultaSalva.Dieta = dieta;
            return consultaSalva.Id;
        }

        private Guid AdicionarConsulta(Apresentacao.Consulta consulta, DateTime dataHora, double imc, double peso, IEnumerable<Modelos.Alimento> dieta, Modelos.Paciente paciente)
        {
            var guid = Guid.NewGuid();

            _consultas.Add(
                            new Modelos.Consulta()
                            {
                                DataHora = dataHora,
                                Dieta = dieta,
                                Id = guid,
                                IndiceMassaCorporal = imc,
                                Paciente = paciente,
                                Peso = peso,
                                RestricoesAlimentares = consulta.RestricoesAlimentares,
                                SensacaoPaciente = consulta.SensacaoPaciente
                            }
                            );
            return guid;
        }

        public IEnumerable<Apresentacao.Consulta> Listar()
        {
            return _consultas.Select(p => new Apresentacao.Consulta(p.Id)
            {
                Data = p.DataHora.ToString("dd/MM/yyyy"),
                Hora = p.DataHora.ToString("HH:mm"),
                IndiceMassaCorporal = p.IndiceMassaCorporal.ToString(),
                Peso = p.Peso.ToString(),
                RestricoesAlimentares = p.RestricoesAlimentares,
                SensacaoPaciente = p.SensacaoPaciente,
                Paciente = new Apresentacao.Paciente(p.Paciente.Id) { Nome = p.Paciente.Nome }
            });
        }

        public Apresentacao.Consulta Listar(Guid guid)
        {
            return _consultas
                .Where(p => p.Id == guid)
                .Select(p => new Apresentacao.Consulta(p.Id)
                {
                    Data = p.DataHora.ToString("dd/MM/yyyy"),
                    Hora = p.DataHora.ToString("HH:mm"),
                    IndiceMassaCorporal = p.IndiceMassaCorporal.ToString(),
                    Peso = p.Peso.ToString(),
                    RestricoesAlimentares = p.RestricoesAlimentares,
                    SensacaoPaciente = p.SensacaoPaciente,
                    Dieta = p.Dieta?.Select(a => new Apresentacao.Alimento() { Descricao = a.Descricao, Grupo = a.Grupo, ValorCalorico = a.ValorCalorico }),
                    Paciente = new Apresentacao.Paciente(p.Paciente.Id) { Nome = p.Paciente.Nome }
                })
                .FirstOrDefault();
        }
    }
}
