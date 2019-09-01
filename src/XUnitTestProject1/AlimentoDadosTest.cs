using System;
using Xunit;
using System.Linq;

namespace XUnitTestProject1
{
    public class AlimentoDadosTest
    {
        [Theory]
        [InlineData(1000D)]
        [InlineData(300.2D)]
        [InlineData(500)]
        [InlineData(700.7D)]
        public void DeveTrazerListaComRegistrosValidos(double maximoValorcalorico)
        {
            var alimentoDados = new Dados.AlimentoDados();
            var combinacoes = alimentoDados.ListarCombinacoes(maximoValorcalorico);

            Assert.True(combinacoes.All(c => c.Any(a => a.Grupo == 1) && c.Any(a => a.Grupo == 2) && c.Any(a => a.Grupo == 3)));
            Assert.True(combinacoes.All(c => c.Sum(a => a.ValorCalorico) <= maximoValorcalorico));

        }
        [Theory]
        [InlineData(0D)]
        [InlineData(-3.2D)]
        public void DeveTrazerListaVazia(double maximoValorcalorico)
        {
            var alimentoDados = new Dados.AlimentoDados();
            var combinacoes = alimentoDados.ListarCombinacoes(maximoValorcalorico);

            Assert.True(combinacoes.Count()==0);
        }
    }
}
