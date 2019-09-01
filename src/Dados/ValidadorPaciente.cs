using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Linq;

namespace Dados
{
    public class ValidadorPaciente
    {
        private readonly Regex regexTelefones = new Regex(@"^(\d{2})?(\d{8,9})$",RegexOptions.Compiled);
        private readonly Regex regexCaracteresEspeciais = new Regex(@"\D", RegexOptions.Compiled);

        public DateTime ObterDataNascimento(string dataNascimento)
        {
            if (DateTime.TryParseExact(dataNascimento,"dd/MM/yyyy",null,System.Globalization.DateTimeStyles.AllowWhiteSpaces,out DateTime resultado))
            {
                return resultado;
            }
            else
            {
                throw new ArgumentException("Data de nascimento deve estar no formato \"dd/MM/yyyy\"");
            }
        }

        public IEnumerable<long> ObterTelefones(string telefones)
        {
            var numerosTelefone = telefones.Split(';').Select(n => regexCaracteresEspeciais.Replace(n, string.Empty));
            var telefonesInvalidos = numerosTelefone.Where(n => !regexTelefones.IsMatch(n));
            if (telefonesInvalidos.Any())
            {
                throw new ArgumentException($"Os telefones {string.Join("; ", telefonesInvalidos)} devem ter entre 8 e 11 dígitos");
            }
            return numerosTelefone.Select(n => long.Parse(n));
        }
    }
}
