using Alura.LeilaoOnline.Core;
using System.Linq;
using Xunit;

namespace Alura.LeilaoOnline.Tests
{
    public class LeilaoRecebeLance
    {
        [Theory]
        [InlineData(2, new double[] { 800, 900 })]
        [InlineData(4, new double[] { 1000, 2000, 3000, 4000 })]
        public void NaoPermiteNovosLancesDadoLeilaoFinalizado(int valorEsperado, double[] lances)
        {
            // Arrange
            var modalidade = new OfertaMaiorValor();
            var leilao = new Leilao("Van Gogh", modalidade);
            var fulano = new Interessada("Fulano", leilao);
            var maria = new Interessada("Maria", leilao);
            leilao.IniciaPregao();

            for (int i = 0; i < lances.Length; i++)
            {
                var valor = lances[i];
                if ((i % 2) == 0)
                {
                    leilao.RecebeLance(fulano, valor);
                }
                else
                {
                    leilao.RecebeLance(maria, valor);
                }
            }

            leilao.TerminaPregao();

            // Act
            leilao.RecebeLance(fulano, 300);

            // Assert
            Assert.Equal(valorEsperado, leilao.Lances.Count());
        }

        [Fact]
        public void NaoPermiteProximoLanceDadoMesmoInteressadoRealizouUltimoLance()
        {
            // Arrange
            var modalidade = new OfertaMaiorValor();
            var leilao = new Leilao("Van Gogh", modalidade);
            var fulano = new Interessada("Fulano", leilao);
            leilao.IniciaPregao();
            leilao.RecebeLance(fulano, 300);

            // Act
            leilao.RecebeLance(fulano, 400);

            // Assert
            var qtdeEsperada = 1;
            var qtdeObtida = leilao.Lances.Count();
            Assert.Equal(qtdeEsperada, qtdeObtida);
        }
    }
}
