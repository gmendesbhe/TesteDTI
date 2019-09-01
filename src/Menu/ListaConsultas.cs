using Apresentacao;
using InterfacesConsole;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Menu
{
    public class ListaConsultas : ITelaConsole
    {
        private IConsultaDados _consultaDados;
        private IPacienteDados _pacienteDados;
        private IAlimentoDados _alimentoDados;
        private List<Consulta> _listaConsultas;

        public ListaConsultas( IPacienteDados pacienteDados, IConsultaDados consultaDados, IAlimentoDados alimentoDados)
        {
            _consultaDados = consultaDados;
            _pacienteDados = pacienteDados;
            _alimentoDados = alimentoDados;
        }
        public void Renderizar()
        {
            _listaConsultas = _consultaDados.Listar().ToList();
            for (int i = 0; i < _listaConsultas.Count; i++)
            {
                var paciente = _listaConsultas[i];
                Console.WriteLine($"{i + 1} - {paciente.Data} {paciente.Hora} - {paciente.Paciente.Nome}");
            }
        }

        public ITelaConsole TratarInput(string linha)
        {
            if (int.TryParse(linha, out int opcao) && opcao - 1 >= 0 && opcao - 1 < _listaConsultas.Count)
            {
                return new CadastroConsulta(_pacienteDados, _consultaDados, _alimentoDados, _listaConsultas[opcao - 1].Id);
            }
            return null;
        }
    }
}
