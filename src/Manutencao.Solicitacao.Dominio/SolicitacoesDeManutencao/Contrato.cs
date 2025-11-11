using System;

namespace Manutencao.Solicitacao.Dominio.SolicitacoesDeManutencao
{
    public class Contrato
    {
        public string Numero { get; private set; }
        public string NomeDaTerceirizada { get; private set; }
        public string CNPJDaTerceirizada { get; private set; }
        public string GestorDoContrato { get; private set; }    
        public DateTime DataFinalDaVigencia { get; private set; }

        private Contrato()
        {
            // Apenas para satisfazer o EF Core. Elas nunca serão 'null'
            Numero = string.Empty;
            NomeDaTerceirizada = string.Empty;
            CNPJDaTerceirizada = string.Empty;
            GestorDoContrato = string.Empty;
        }

        public Contrato(
            string numero,
            string nomeDaTerceirizada,
            string cnpjDaTerceirizada,
            string gestorDoContrato,
            DateTime dataFinalDaVigencia)
        {
            Numero = numero;
            NomeDaTerceirizada = nomeDaTerceirizada;
            CNPJDaTerceirizada = cnpjDaTerceirizada;
            GestorDoContrato = gestorDoContrato;
            DataFinalDaVigencia = dataFinalDaVigencia;
        }
    }
}