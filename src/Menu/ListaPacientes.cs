using Dados;
using InterfacesConsole;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Apresentacao;

namespace Menu
{
    public class ListaPacientes : ITelaConsole
    {
        private IPacienteDados _pacienteDados;
        private List<Paciente> _listaPacientes;

        public ListaPacientes(IPacienteDados pacienteDados)
        {
            _pacienteDados = pacienteDados;
        }
        public void Renderizar()
        {
            _listaPacientes = _pacienteDados.Listar().ToList();
            for (int i = 0; i < _listaPacientes.Count; i++)
            {
                var paciente = _listaPacientes[i];
                Console.WriteLine($"{i + 1} - {paciente.Nome} - {paciente.DataNascimento} - {paciente.Telefones}");
            }
        }

        public ITelaConsole TratarInput(string linha)
        {
            if (int.TryParse(linha, out int opcao) && opcao - 1 >= 0 && opcao - 1 < _listaPacientes.Count)
            {
                return new CadastroPaciente(_pacienteDados, _listaPacientes[opcao - 1].Id);
            }
            return null;
        }
    }
}
