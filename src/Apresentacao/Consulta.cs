using System;
using System.Collections.Generic;
using System.Text;

namespace Apresentacao
{
    public class Consulta
    {
        public Consulta()
        {

        }

        public Consulta(Guid guid)
        {
            Id = guid;
        }
        public Guid Id { get; set; }
        public string Data { get; set; }
        public string Hora { get; set; }
        public string Peso { get; set; }
        public string IndiceMassaCorporal { get; set; }
        public string SensacaoPaciente { get; set; }
        public Paciente Paciente { get; set; }
        public string RestricoesAlimentares { get; set; }
        public IEnumerable<Alimento> Dieta { get; set; }
    }
}
