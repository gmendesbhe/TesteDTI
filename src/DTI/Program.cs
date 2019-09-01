using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using InterfacesConsole;

namespace DTI
{
    class Program
    {
        static void Main(string[] args)
        {
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("pt-BR");

            var stackTelas = new Stack<ITelaConsole>(5);
            stackTelas.Push(Injetor.Injetor.ObterTelaInicial());

            var telaAtual = stackTelas.Peek();

            while (stackTelas.TryPeek(out telaAtual))
            {

                Console.Clear();

                Console.WriteLine("A qualquer momento digite \"SAIR\" para sair do sistema; ou digite \"VOLTAR\" para voltar à tela anterior");
                Console.WriteLine();
                Console.WriteLine("Digite o número da opção que deseja acessar e pressione ENTER");
                Console.WriteLine();
                telaAtual.Renderizar();
                Console.WriteLine();

                var linha = Console.ReadLine();

                if (linha.ToUpperInvariant() == Utilitario.Constantes.SAIR)
                {
                    return;
                }

                if (linha.ToUpperInvariant() == Utilitario.Constantes.VOLTAR)
                {
                    if (!stackTelas.TryPop(out telaAtual))
                        return;

                    continue;
                }

                var telaAcao = telaAtual.TratarInput(linha);
                if (telaAcao != null)
                {
                    stackTelas.Push(telaAcao);
                }
            }
        }
    }
}
