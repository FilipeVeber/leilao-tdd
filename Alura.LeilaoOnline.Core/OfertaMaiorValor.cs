using System.Linq;

namespace Alura.LeilaoOnline.Core
{
    public class OfertaMaiorValor : IModalidadeAvaliacao
    {
        public OfertaMaiorValor()
        {

        }

        public Lance Avalia(Leilao leilao)
        {
            return leilao.Lances
                .DefaultIfEmpty(new Lance(null, 0))
                .OrderByDescending(l => l.Valor)
                .FirstOrDefault();
        }
    }
}
