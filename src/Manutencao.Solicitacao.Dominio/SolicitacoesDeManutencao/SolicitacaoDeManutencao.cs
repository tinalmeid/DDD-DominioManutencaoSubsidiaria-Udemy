using System;
using Manutencao.Solicitacao.Dominio;

namespace Manutencao.Solicitacao.Dominio.SolicitacoesDeManutencao
{
    public class SolicitacaoDeManutencao : Entidade
    {
        public Autor Solicitante { get; private set; }
        public Autor Aprovador { get; private set; }
        public String IdentificadorDaSubsidiaria { get; private set; }
        public TipoDeSolicitacaoDeManutencao TipoDeSolicitacaoDeManutencao { get; private set; }
        public String Justificativa { get; private set; }
        public Contrato Contrato { get; private set; }
        public DateTime InicioDesejadoParaManutencao { get; private set; }
        public DateTime DataDaSolicitacao { get; private set; }
        public StatusDeSolicitacao StatusDaSolicitacao { get; private set; }

        private SolicitacaoDeManutencao()
        {
            // Apenas para satisfazer o EF Core e o compilador C#
            Solicitante = new Autor(0, "Sem Solicitante");
            Aprovador = new Autor(0, "Sem Aprovador");
            // Incluir inicialização da string IdentificadorDaSubsidiaria para evitar erro de campo obrigatório
            IdentificadorDaSubsidiaria = string.Empty;
            Justificativa = string.Empty;
            // Incluir inicialização do Value Object Contrato para evitar erro de campo obrigatório
            Contrato = null!;
        }

        public SolicitacaoDeManutencao(
            string identificadorDaSubsidiaria,
            int identificadorDoSolicitante,
            string nomeDoSolicitante,
            TipoDeSolicitacaoDeManutencao tipoDeSolicitacaoDeManutencao,
            string justificativa,
            string numeroDoContrato,
            string nomeDaTerceirizada,
            string cnpjDaTerceirizada,
            string gestorDoContrato,
            DateTime dataFinalDaVigencia,
            DateTime inicioDesejadoParaManutencao)
        {
            // Validações das regras de negócio (Domain Exceptions)
            ExcecaoDeDominioException.LancarQuando(string.IsNullOrEmpty(identificadorDaSubsidiaria),
             "A subsidiária é obrigatória.");
            ExcecaoDeDominioException.LancarQuando(string.IsNullOrEmpty(justificativa),
             "A justificativa é obrigatória.");
            ExcecaoDeDominioException.LancarQuando(inicioDesejadoParaManutencao < DateTime.Now.Date,
             "Data de início para manutenção não pode ser inferior a data atual.");

            // Atribuições dos valores aos campos da classe
            Solicitante = new Autor(identificadorDoSolicitante, nomeDoSolicitante);
            IdentificadorDaSubsidiaria = identificadorDaSubsidiaria;
            TipoDeSolicitacaoDeManutencao = tipoDeSolicitacaoDeManutencao;
            Justificativa = justificativa;
            // Cria o Value Object Contrato (VO do Contrato)
            Contrato = new Contrato(numeroDoContrato, nomeDaTerceirizada, cnpjDaTerceirizada, gestorDoContrato, dataFinalDaVigencia);
            InicioDesejadoParaManutencao = inicioDesejadoParaManutencao;
            DataDaSolicitacao = DateTime.Now;
            StatusDaSolicitacao = StatusDeSolicitacao.Pendente;
            // Apenas para satisfazer o EF Core
            Aprovador = new Autor(0, "Sem Aprovador");
        }

        public void Cancelar()
        {
            StatusDaSolicitacao = StatusDeSolicitacao.Cancelada;
        }

        public void Rejeitar(Autor aprovador)
        {
            Aprovador = aprovador;
            StatusDaSolicitacao = StatusDeSolicitacao.Rejeitada;
        }

        public void Aprovar(Autor aprovador)
        {
            Aprovador = aprovador;
            StatusDaSolicitacao = StatusDeSolicitacao.Aprovada;
        }

        public bool Rejeitada()
        {
            return StatusDaSolicitacao == StatusDeSolicitacao.Rejeitada;
        }

        public bool Aprovada()
        {
            return StatusDaSolicitacao == StatusDeSolicitacao.Aprovada;
        }
    }
}
        






