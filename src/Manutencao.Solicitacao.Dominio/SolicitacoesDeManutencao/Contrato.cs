using System;
using Manutencao.Solicitacao.Dominio;

namespace Manutencao.Solicitacao.Dominio.SolicitacoesDeManutencao
{
    public class Contrato
    {
        public string Numero { get; private set; }
        public string NomeDaTerceirizada { get; private set; }
        public string CnpjDaTerceirizada { get; private set; }
        public string GestorDoContrato { get; private set; }    
        public DateTime DataFinalDaVigencia { get; private set; }

        private Contrato()
        {
            // Apenas para satisfazer o EF Core. Elas nunca serão 'null'
            Numero = string.Empty;
            NomeDaTerceirizada = string.Empty;
            CnpjDaTerceirizada = string.Empty;
            GestorDoContrato = string.Empty;
        }

        public Contrato(
            string numero,
            string nomeDaTerceirizada,
            string cnpjDaTerceirizada,
            string gestorDoContrato,
            DateTime dataFinalDaVigencia)
        {
            // Deve ser em ordem dos parâmetros. Isso é primordial para leitura e testes.
             ExcecaoDeDominioException.LancarQuando(string.IsNullOrEmpty(numero),
            "O número do contrato é obrigatório.");
            // Nome da terceirizada           
            ExcecaoDeDominioException.LancarQuando(string.IsNullOrEmpty(nomeDaTerceirizada),
            "O nome da terceirizada do contrato é obrigatório.");
            // CNPJ da terceirizada
            ExcecaoDeDominioException.LancarQuando(string.IsNullOrEmpty(cnpjDaTerceirizada),
            "O CNPJ da terceirizada do contrato é obrigatório.");
            // Gestor do contrato       
            ExcecaoDeDominioException.LancarQuando(string.IsNullOrEmpty(gestorDoContrato),
            "O nome do gestor do contrato é obrigatório.");
            // Data final da vigência (A regra 'data futura' deve seer MAIOR que a data atual)
            ExcecaoDeDominioException.LancarQuando(dataFinalDaVigencia <= DateTime.Now,
            "A data final da vigência deve ser MAIOR que a data atual.");

            // Atribuições dos valores aos campos da classe
            Numero = numero;
            NomeDaTerceirizada = nomeDaTerceirizada;
            CnpjDaTerceirizada = cnpjDaTerceirizada;
            GestorDoContrato = gestorDoContrato;
            DataFinalDaVigencia = dataFinalDaVigencia;
        }
    }
}