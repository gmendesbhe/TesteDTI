using System;
using System.Collections.Generic;
using System.Text;

namespace InterfacesConsole
{
    public interface IAlimentoDados
    {
        IEnumerable<IEnumerable<Apresentacao.Alimento>> ListarCombinacoes(double maximoValorCalorico);
    }
}
