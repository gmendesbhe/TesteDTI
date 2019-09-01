using System;
using System.Collections.Generic;
using System.Text;

namespace Menu
{
    public class MenuItem<T>
    {
        public string Descricao { get; set; }
        public Func<T> AcaoSelecionado { get; set; }
    }
}
