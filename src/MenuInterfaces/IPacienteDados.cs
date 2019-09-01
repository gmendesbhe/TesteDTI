using System;
using System.Collections.Generic;
using System.Text;

namespace InterfacesConsole
{
    public interface IPacienteDados
    {
        Guid Salvar(Apresentacao.Paciente paciente);
        IEnumerable<Apresentacao.Paciente> Listar();
        Apresentacao.Paciente Listar(Guid guid);
    }
}
