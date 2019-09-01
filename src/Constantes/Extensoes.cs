using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Utilitario
{
    public static class Extensoes
    {
        public static IEnumerable<IEnumerable<T>> ProdutoCartesiano<T>(this IEnumerable<IEnumerable<T>> listas)
        {
            IEnumerable<IEnumerable<T>> produto = new[] { Enumerable.Empty<T>() };
            return listas.Aggregate(
                produto,
                (acumulador, lista) =>
                from listaAcumulada in acumulador
                from item in lista
                select listaAcumulada.Concat(new[] { item }));
        }
    }
}
