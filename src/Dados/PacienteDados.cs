using InterfacesConsole;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Dados
{
    public class PacienteDados: IPacienteDados
    {
        private List<Modelos.Paciente> _pacientes;
        private ValidadorPaciente _validador;

        public PacienteDados(ValidadorPaciente validador)
        {
            _pacientes = new List<Modelos.Paciente>();
            _validador = validador;
        }

        public Guid Salvar(Apresentacao.Paciente paciente)
        {
            var dataNascimento = _validador.ObterDataNascimento(paciente.DataNascimento);
            var telefones = _validador.ObterTelefones(paciente.Telefones);
            Guid guidPadrao = Guid.Empty;
            if (paciente.Id == guidPadrao)
            {
                return AdicionarPaciente(paciente, dataNascimento, telefones);
            }
            else
            {
                return AtualizarPaciente(paciente, dataNascimento, telefones);
            }
        }

        private Guid AtualizarPaciente(Apresentacao.Paciente paciente, DateTime dataNascimento, IEnumerable<long> telefones)
        {
            var pacienteSalvo = _pacientes.Where(p => p.Id == paciente.Id).First();

            if (pacienteSalvo is null)
            {
                throw new InvalidOperationException("Paciente não encontrado");
            }

            pacienteSalvo.Nome = paciente.Nome;
            pacienteSalvo.Email = paciente.Email;
            pacienteSalvo.Telefones = telefones;
            pacienteSalvo.DataNascimento = dataNascimento;
            pacienteSalvo.Endereco = paciente.Endereco;
            return pacienteSalvo.Id;
        }

        internal Modelos.Paciente ListarModelo(Guid id)
        {
            return _pacientes.Where(p => p.Id == id).FirstOrDefault();
        }

        private Guid AdicionarPaciente(Apresentacao.Paciente paciente, DateTime dataNascimento, IEnumerable<long> telefones)
        {
            Guid guid = Guid.NewGuid();
            _pacientes.Add(
                new Modelos.Paciente()
                {
                    Id = guid,
                    Nome = paciente.Nome,
                    Email = paciente.Email,
                    Telefones = telefones,
                    DataNascimento = dataNascimento,
                    Endereco = paciente.Endereco
                }
                );
            return guid;
        }

        public IEnumerable<Apresentacao.Paciente> Listar()
        {
            return _pacientes.Select(p => new Apresentacao.Paciente(p.Id)
            {
                DataNascimento = p.DataNascimento.ToString("dd/MM/yyyy"),
                Email = p.Email,
                Endereco = p.Endereco,
                Nome = p.Nome,
                Telefones = string.Join("; ", p.Telefones)
            });
        }

        public Apresentacao.Paciente Listar(Guid guid)
        {
            return _pacientes.Where(p => p.Id == guid).Select(p => new Apresentacao.Paciente(p.Id)
            {
                DataNascimento = p.DataNascimento.ToString("dd/MM/yyyy"),
                Email = p.Email,
                Endereco = p.Endereco,
                Nome = p.Nome,
                Telefones = string.Join("; ", p.Telefones)
            }).FirstOrDefault();
        }

    }
}
