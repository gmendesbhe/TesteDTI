using InterfacesConsole;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;

namespace Menu
{
    public class TelaInicial : MenuConsole<ITelaConsole>, ITelaConsole
    {
        public TelaInicial(ServiceContainer container)
        {
            Itens = new Dictionary<int, MenuItem<ITelaConsole>>() {
                {1,new MenuItem<ITelaConsole>(){Descricao = "1 - Cadastrar Pacientes", AcaoSelecionado = () => new CadastroPaciente(container.GetService(typeof(IPacienteDados))as IPacienteDados)} },
                {2,new MenuItem<ITelaConsole>(){Descricao = "2 - Listar Pacientes", AcaoSelecionado = () => new ListaPacientes(container.GetService(typeof(IPacienteDados))as IPacienteDados)} },
                {3,new MenuItem<ITelaConsole>(){Descricao = "3 - Cadastrar Consulta", AcaoSelecionado = () => new CadastroConsulta(container.GetService(typeof(IPacienteDados))as IPacienteDados, container.GetService(typeof(IConsultaDados))as IConsultaDados, container.GetService(typeof(IAlimentoDados))as IAlimentoDados)} },
                {4,new MenuItem<ITelaConsole>(){Descricao = "4 - listar Consultas", AcaoSelecionado = () => new ListaConsultas(container.GetService(typeof(IPacienteDados))as IPacienteDados, container.GetService(typeof(IConsultaDados))as IConsultaDados, container.GetService(typeof(IAlimentoDados))as IAlimentoDados)} }
            };
        }

        public ITelaConsole TratarInput(string linha)
        {
            if (int.TryParse(linha, out int opcao) && Itens.TryGetValue(opcao, out MenuItem<ITelaConsole> item))
            {
                return item.AcaoSelecionado();
            }
            return null;
        }

        void ITelaConsole.Renderizar()
        {
            foreach (var kvp in Itens)
            {
                Console.WriteLine($"{kvp.Value.Descricao}");
            }
        }
    }
}
