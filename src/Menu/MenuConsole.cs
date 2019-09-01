using InterfacesConsole;
using System;
using System.Collections.Generic;

namespace Menu
{
    public abstract class MenuConsole<TItem>
    {
        public Dictionary<int, MenuItem<TItem>> Itens { get; protected set; }
    }
}
