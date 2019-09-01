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
                ValorCalorico = 100,
                Grupo = 1
            },
            new Modelos.Alimento()
            {
                Descricao="Feijão",
                ValorCalorico = 236.44,
                Grupo = 1
            },
            new Modelos.Alimento()
            {
                Descricao="Quinoa",
                ValorCalorico = 312.33,
                Grupo = 1
            },
            new Modelos.Alimento()
            {
                Descricao="Frango",
                ValorCalorico = 150.43,
                Grupo = 2
            },
            new Modelos.Alimento()
            {
                Descricao="Boi",
                ValorCalorico = 500,
                Grupo = 2
            },
            new Modelos.Alimento()
            {
                Descricao="Porco",
                ValorCalorico = 407,
                Grupo = 2
            },
            new Modelos.Alimento()
            {
                Descricao="Alface",
                ValorCalorico = 50.8,
                Grupo = 3
            },
            new Modelos.Alimento()
            {
                Descricao="Tomate",
                ValorCalorico = 100,
                Grupo = 3
            },
            new Modelos.Alimento()
            {
                Descricao="Rúcula",
                ValorCalorico = 178,
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
