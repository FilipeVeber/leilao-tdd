using Alura.LeilaoOnline.Core;
using System;
using Xunit;

namespace Alura.LeilaoOnline.Tests
{
    public class LeilaoTerminaPregao
    {
        [Theory]
        [InlineData(1000, new double[] { 800, 900, 999, 1000 })]
        [InlineData(1000, new double[] { 800, 900, 1000, 999 })]
        [InlineData(800, new double[] { 800 })]
        public void RetornaMaiorValorDadoLeilaoComPeloMenosUmLance(double valorEsperado, double[] lances)
        {
            // Arrange
            IModalidadeAvaliacao modalidade = new OfertaMaiorValor();
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

            // Act
            leilao.TerminaPregao();

            // Assert
            Assert.Equal(valorEsperado, leilao.Ganhador.Valor);
        }

        [Fact]
        public void RetornaZeroDadoLeilaoSemLances()
        {
            // Arrange
            var modalidade = new OfertaMaiorValor();
            var leilao = new Leilao("Van Gogh", modalidade);
            leilao.IniciaPregao();

            // Act
            leilao.TerminaPregao();

            // Assert
            Assert.Equal(0, leilao.Ganhador.Valor);
        }

        [Fact]
        public void LancaInvalidOpereationExceptionDadoPregaoNaoIniciado()
        {
            // Arrange
            var modalidade = new OfertaMaiorValor();
            var leilao = new Leilao("Van Gogh", modalidade);

            // Assert
            //Assert.Throws<InvalidOperationException>(
            //    // Act
            //    () => leilao.TerminaPregao());

            // Para capturar a exceção e checar propriedades, como uma mensagem específica, por exemplo
            // Assert
            var excecaoLancada = Assert.Throws<InvalidOperationException>(
                // Act
                () => leilao.TerminaPregao());

            var mensagemEsperada = "Não é possível terminar o pregão sem que o mesmo tenha iniciado.";
            Assert.Equal(mensagemEsperada, excecaoLancada.Message);
        }

        [Theory]
        [InlineData(1200, 1250, new double[] { 800, 1150, 1400, 1250 })]
        public void RetornaValorSuperiorMaisProximoDadoLeilaoNessaModalidade(double valorDestino, double valorEsperado, double[] lances)
        {
            // Arrange
            IModalidadeAvaliacao modalidade = new OfertaSuperiorMaisProxima(valorDestino);

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

            // Act
            leilao.TerminaPregao();

            // Assert
            Assert.Equal(valorEsperado, leilao.Ganhador.Valor);
        }
    }
}
