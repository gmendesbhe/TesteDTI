using System;
using System.Collections.Generic;
using System.Text;

namespace Dados
{
    public class ValidadorConsulta
    {
        public DateTime ValidarDataHora(string data, string hora)
        {
            if (DateTime.TryParseExact(string.Join(' ', data, hora), "dd/MM/yyyy HH:mm", null, System.Globalization.DateTimeStyles.AllowWhiteSpaces, out DateTime resultado))
            {
                if (resultado<DateTime.Today)
                {
                    throw new ArgumentException("Data informada não pode ser anterior a hoje");
                }
                return resultado;
            }
            throw new ArgumentException("Data e Hora da consulta devem estar no formato \"dd / MM / yyyy HH: mm\"");
        }

        public double ValidarImc(string indiceMassaCorporal)
        {
            var numeroInvalido = new ArgumentException("Indice de massa corporal deve ser um número válido maior que zero");
            try
            {
                var imc = Convert.ToDouble(indiceMassaCorporal);
                if (imc>0)
                {
                    return imc;
                }
                throw numeroInvalido;
            }
            catch (FormatException)
            {
                throw numeroInvalido;
            }
        }

        public double ValidarPeso(string peso)
        {
            var numeroInvalido = new ArgumentException("Peso deve ser um número válido maior que zero");
            try
            {
                var imc = Convert.ToDouble(peso);
                if (imc > 0)
                {
                    return imc;
                }
                throw numeroInvalido;
            }
            catch (FormatException)
            {
                throw numeroInvalido;
            }
        }
    }
}
