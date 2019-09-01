using Menu;
using InterfacesConsole;
using System;
using System.ComponentModel.Design;

namespace Injetor
{
    public static class Injetor
    {
        public static ServiceContainer Container()
        {
            var container = new ServiceContainer();

            Dados.PacienteDados pacienteDados = new Dados.PacienteDados(new Dados.ValidadorPaciente());
            container.AddService(typeof(IPacienteDados), pacienteDados);
            container.AddService(typeof(IConsultaDados), new Dados.ConsultaDados(new Dados.ValidadorConsulta(), pacienteDados));
            container.AddService(typeof(IAlimentoDados), new Dados.AlimentoDados());

            return container;
        }
        public static ITelaConsole ObterTelaInicial()
        {
            return new TelaInicial(Container());

        }
        
    }
}
