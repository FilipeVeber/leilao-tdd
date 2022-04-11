using Alura.LeilaoOnline.Core;
using System;

namespace Alura.LeilaoOnline.ConsoleApp
{
    public class Program
    {
        static void Main()
        {
            var modalidade = new OfertaMaiorValor();
            var leilao = new Leilao("Van Gogh", modalidade);
            var fulano = new Interessada("Fulano", leilao);
            var maria = new Interessada("Maria", leilao);

            leilao.RecebeLance(fulano, 800);
            leilao.RecebeLance(maria, 900);
            leilao.RecebeLance(fulano, 1000);

            leilao.TerminaPregao();

            Console.WriteLine(leilao.Ganhador.Valor);
        }
    }
}
