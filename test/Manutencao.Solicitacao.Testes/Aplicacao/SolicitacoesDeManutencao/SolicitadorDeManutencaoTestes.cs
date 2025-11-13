using System;
using Nosbor.FluentBuilder.Lib;
using NSubstitute;
using Xunit;
using Manutencao.Solicitacao.Aplicacao.SolicitacoesDeManutencao;
using Manutencao.Solicitacao.Dominio.SolicitacoesDeManutencao;

namespace Manutencao.Solicitacao.Testes.Aplicacao.SolicitacoesDeManutencao
{
    public class SolicitadorDeManutencaoTeste
    {
        private readonly SolicitacaoDeManutencaoDto _dto;
        private readonly SolicitadorDeManutencao _solicitador;
        private readonly SolicitacaoDeManutencao _solicitacaoDeManutencao;
        private readonly ISolicitacaoDeManutencaoRepositorio _solicitacaoDeManutencaoRepositorio;
        private readonly ICanceladorDeSolicitacoesDeManutencaoPendentes _canceladorDeSolicitacoesDeManutencaoPendentes;

        public SolicitadorDeManutencaoTeste()
        {
            _dto = new SolicitacaoDeManutencaoDto
            {
                SubsidiariaId = "idsubsidiaria-123",
                SolicitanteId = 123,
                NomeDoSolicitante = "Solicitante Teste",
                TipoDeSolicitacaoDeManutencao = TipoDeSolicitacaoDeManutencao.Jardinagem.GetHashCode(),
                Justificativa = "Justificativa Teste",
                NumeroDoContrato = "Contrato-456",
                InicioDesejadoParaManutencao = DateTime.Now.AddMonths(2)
            };

            _solicitacaoDeManutencaoRepositorio = Substitute.For<ISolicitacaoDeManutencaoRepositorio>();
            _canceladorDeSolicitacoesDeManutencaoPendentes = Substitute.For<ICanceladorDeSolicitacoesDeManutencaoPendentes>();
            _solicitacaoDeManutencao = FluentBuilder<SolicitacaoDeManutencao>.New().With(s => s.IdentificadorDaSubsidiaria, _dto.SubsidiariaId).Build();

            var fabricaDeSolicitacaoDeManutencao = Substitute.For<FabricaDeSolicitacaoDeManutencao>();
            fabricaDeSolicitacaoDeManutencao.Fabricar(_dto).Returns(_solicitacaoDeManutencao);
            _solicitador = new SolicitadorDeManutencao(
                _solicitacaoDeManutencaoRepositorio,
                fabricaDeSolicitacaoDeManutencao,
                _canceladorDeSolicitacoesDeManutencaoPendentes);
             
        }
    }
}          

