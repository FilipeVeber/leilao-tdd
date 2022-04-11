using System;
using System.Collections.Generic;

namespace Alura.LeilaoOnline.Core
{
    public enum EEstadoLeilao
    {
        AntesDoPregao,
        EmAndamento,
        Finalizado
    }

    public class Leilao
    {
        private IList<Lance> _lances;
        public IEnumerable<Lance> Lances => _lances;
        public string Peca { get; }
        public Lance Ganhador { get; private set; }
        public EEstadoLeilao Estado { get; private set; }
        private Interessada ClienteUltimoLance { get; set; }
        private IModalidadeAvaliacao _modalidadeAvaliacao { get; set; }

        public Leilao(string peca, IModalidadeAvaliacao modalidadeAvaliacao)
        {
            Peca = peca;
            _lances = new List<Lance>();
            Estado = EEstadoLeilao.AntesDoPregao;
            _modalidadeAvaliacao = modalidadeAvaliacao;
        }

        public void RecebeLance(Interessada cliente, double valor)
        {
            if (AceitaNovoLance(cliente, valor))
            {
                _lances.Add(new Lance(cliente, valor));
                ClienteUltimoLance = cliente;
            }
        }

        public void IniciaPregao()
        {
            Estado = EEstadoLeilao.EmAndamento;
        }

        public void TerminaPregao()
        {
            if (Estado != EEstadoLeilao.EmAndamento)
            {
                throw new InvalidOperationException("Não é possível terminar o pregão sem que o mesmo tenha iniciado.");
            }

            Ganhador = _modalidadeAvaliacao.Avalia(this);

            Estado = EEstadoLeilao.Finalizado;
        }

        private bool AceitaNovoLance(Interessada cliente, double valor)
        {
            return (Estado == EEstadoLeilao.EmAndamento)
                && (cliente != ClienteUltimoLance);
        }
    }
}