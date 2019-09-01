using System;
using System.Collections.Generic;

namespace Modelos
{
    public class Paciente
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Endereco { get; set; }
        public IEnumerable<long> Telefones { get; set; }
        public string Email { get; set; }
        public DateTime DataNascimento { get; set; }
    }
    
}
