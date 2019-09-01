using System;
using System.Collections.Generic;
using System.Text;

namespace Modelos
{
    public class Endereco
    {
        public string Rua { get; set; }
        public int? Numero { get; set; }
        public int? Complemento { get; set; }
        public int CEP { get; set; }
    }
}
