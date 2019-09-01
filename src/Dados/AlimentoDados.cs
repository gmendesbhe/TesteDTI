using System;
using System.Collections.Generic;
using System.Linq;
using Utilitario;
using System.Text;
using InterfacesConsole;

namespace Dados
{
    public class AlimentoDados:IAlimentoDados
    {
        private List<Modelos.Alimento> _alimentos = new List<Modelos.Alimento>()
        {
            new Modelos.Alimento()
            {
                Descricao="Arroz",
                ValorCalorico = 1,
                Grupo = 1
            },
            new Modelos.Alimento()
            {
                Descricao="Feijão",
                ValorCalorico = 2,
                Grupo = 1
            },
            new Modelos.Alimento()
            {
                Descricao="Quinoa",
                ValorCalorico = 3,
                Grupo = 1
            },
            new Modelos.Alimento()
            {
                Descricao="Frango",
                ValorCalorico = 1,
                Grupo = 2
            },
            new Modelos.Alimento()
            {
                Descricao="Boi",
                ValorCalorico = 1,
                Grupo = 2
            },
            new Modelos.Alimento()
            {
                Descricao="Porco",
                ValorCalorico = 1,
                Grupo = 2
            },
            new Modelos.Alimento()
            {
                Descricao="Alface",
                ValorCalorico = 1,
                Grupo = 3
            },
            new Modelos.Alimento()
            {
                Descricao="Tomate",
                ValorCalorico = 1,
                Grupo = 3
            },
            new Modelos.Alimento()
            {
                Descricao="Rúcula",
                ValorCalorico = 1,
                Grupo = 3
            },
        };

        public IEnumerable<IEnumerable<Apresentacao.Alimento>> ListarCombinacoes(double maximoValorCalorico)
        {
            return _alimentos
                .Select(a=>new Apresentacao.Alimento()
                {
                    Descricao = a.Descricao,
                    Grupo = a.Grupo,
                    ValorCalorico = a.ValorCalorico
                })
                .GroupBy(a => a.Grupo)
                .Select(g => g.AsEnumerable())
                .ProdutoCartesiano()
                .Where(g => g.Sum(a => a.ValorCalorico) <= maximoValorCalorico)
                ;
        }
    }
}
