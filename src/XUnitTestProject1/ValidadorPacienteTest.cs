using Dados;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Testes
{
    public class ValidadorPacienteTest
    {
        [Theory]
        [InlineData("1")]
        [InlineData("80974")]
        [InlineData("8763085340923578")]
        [InlineData("8327401875069")]
        public void CriticarTelefoneComPoucosDigitosOuMuitosDigitos(string telefone)
        {
            var validador = new ValidadorPaciente();

            Action acao = () => validador.ObterTelefones(telefone);

            Assert.Throws<ArgumentException>(acao);
        }

        [Theory]
        [InlineData("3133333333")]
        [InlineData("11933333333")]
        [InlineData("33333333")]
        [InlineData("933333333")]
        public void RetornarTelfoneCorretoComoLong(string telefone) {

            var validador = new ValidadorPaciente();

            var esperado = new List<long>(1) { long.Parse(telefone) };
            var resultado = validador.ObterTelefones(telefone);

            Assert.Equal(esperado, resultado);
        }

        [Fact]
        public void RemoveCaracteresNaoNumericosDoTelefone()
        {

            var validador = new ValidadorPaciente();

            var esperado = new List<long>(1) { 123456789 };
            var resultado = validador.ObterTelefones("1j2j34j5k67w89qqq");

            Assert.Equal(esperado, resultado);
        }

        [Theory]
        [InlineData("933333333")]
        [InlineData("22/22/2222")]
        [InlineData("11/1s/2ooo")]
        public void CriticarDatasMalFormatadasOuCaracteresEstranhos(string data)
        {

            var validador = new ValidadorPaciente();

            Action acao = ()=>validador.ObterDataNascimento(data);

            Assert.Throws<ArgumentException>(acao);
        }


    }
}
