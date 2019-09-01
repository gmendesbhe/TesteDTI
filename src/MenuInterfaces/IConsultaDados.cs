using System;
using System.Collections.Generic;
using System.Text;

namespace InterfacesConsole
{
    public interface IConsultaDados
    {
        Guid Salvar(Apresentacao.Consulta consulta);
        Apresentacao.Consulta Listar(Guid idConsulta);
        IEnumerable<Apresentacao.Consulta> Listar();
    }
}
