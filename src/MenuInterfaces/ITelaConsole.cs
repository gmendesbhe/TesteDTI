using System;

namespace InterfacesConsole
{
    public interface ITelaConsole
    {
        void Renderizar();
        ITelaConsole TratarInput(string linha);

    }
}
