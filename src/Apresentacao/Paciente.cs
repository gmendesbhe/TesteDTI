using System;

namespace Apresentacao
{
    public class Paciente
    {
        

        public Paciente()
        {

        }

        public Paciente(Guid guid)
        {
            Id = guid;
        }

        public Guid Id { get; private set; }
        public string Nome { get; set; }
        public string Endereco { get; set; }
        public string Telefones { get; set; }
        public string Email { get; set; }
        public string DataNascimento { get; set; }
    }
}
