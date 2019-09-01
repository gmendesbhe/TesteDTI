using System;
using System.Collections.Generic;
using System.Text;

namespace Modelos
{
    public class Consulta
    {
        public Guid Id { get; set; }
        public DateTime DataHora { get; set; }
        public double Peso { get; set; }
        public double IndiceMassaCorporal { get; set; }
        public string SensacaoPaciente { get; set; }
        public Paciente Paciente { get; set; }
        public string RestricoesAlimentares { get; set; }
        public IEnumerable<Alimento> Dieta { get; set; }
    }
}
