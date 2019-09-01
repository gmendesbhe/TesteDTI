using Dados;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Testes
{
    public class ValidadorConsultaTest
    {

        [Fact]
        public void CriticaDataMenorQueAtual()
        {

            var validador = new ValidadorConsulta();

            var dataAtual = new DateTime(2019, 08, 01);

            Action acao = () => validador.ValidarDataHora("22/07/2019", "11:11");

            Assert.Throws<ArgumentException>(acao);
        }

        [Theory]
        [InlineData("933333333", "11111")]
        [InlineData("22/22/2222", "11:11")]
        [InlineData("11/1s/2ooo", "10:50")]
        [InlineData("11/11/2000", "27:50")]
        [InlineData("11/11/2000", "10:70")]
        public void CriticarDataHoraMalFormatadasOuCaracteresEstranhos(string data, string hora)
        {

            var validador = new ValidadorConsulta();

            Action acao = () => validador.ValidarDataHora(data, hora);

            Assert.Throws<ArgumentException>(acao);
        }

        [Theory]
        [InlineData("-1,4")]
        [InlineData("0")]
        public void CriticarImcZeradoOuNegativo(string imc)
        {
            var validador = new ValidadorConsulta();
            Action acao = () => validador.ValidarImc(imc);

            Assert.Throws<ArgumentException>(acao);
        }

        [Theory]
        [InlineData("-1,A")]
        [InlineData("rrr")]
        public void CriticarImcMalFormatado(string imc)
        {
            var validador = new ValidadorConsulta();
            Action acao = () => validador.ValidarImc(imc);

            Assert.Throws<ArgumentException>(acao);
        }

        [Theory]
        [InlineData("-1,4")]
        [InlineData("0")]
        public void CriticarPesoZeradoOuNegativo(string peso)
        {
            var validador = new ValidadorConsulta();
            Action acao = () => validador.ValidarPeso(peso);

            Assert.Throws<ArgumentException>(acao);
        }

        [Theory]
        [InlineData("-1,A")]
        [InlineData("rrr")]
        public void CriticarPesoMalFormatado(string peso)
        {
            var validador = new ValidadorConsulta();
            Action acao = () => validador.ValidarPeso(peso);

            Assert.Throws<ArgumentException>(acao);
        }

        [Fact]
        public void ObterPesoCorretoPadraoBr()
        {
            var validador = new ValidadorConsulta();
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("pt-BR");
            var esperado = 55.7D;
            var atual = validador.ValidarPeso("55,7");

            Assert.Equal(esperado, atual);
        }

        [Fact]
        public void ObterImcCorretoPadraoBr()
        {
            var validador = new ValidadorConsulta();

            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("pt-BR");
            var esperado = 13.3D;
            var atual = validador.ValidarImc("13,3");

            Assert.Equal(esperado, atual);
        }
    }
}
