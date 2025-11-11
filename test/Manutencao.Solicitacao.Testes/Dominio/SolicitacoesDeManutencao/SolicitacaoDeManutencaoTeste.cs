using System;
using ExpectedObjects;
using Manutencao.Solicitacao.Dominio.SolicitacoesDeManutencao;
using Manutencao.Solicitacao.Dominio.Subsidiarias;
using Manutencao.Solicitacao.Testes._Util;
using Nosbor.FluentBuilder.Lib;
using Xunit;

namespace Manutencao.Solicitacao.Testes.Dominio.SolicitacoesDeManutencao
{
    public class SolicitacaoDeManutencaoTeste
    {
        private const int IdentificadorDoSolicitante = 10;
        private const string NomeDoSolicitante = "Solicitante Teste";
        private const string NumeroDoContrato = "12345";
        private const string NomeDaTerceirizadaDoContrato = "Terceirizada Teste";
        private const string CnpjDaTerceirizadaDoContrato = "12.345.678/0001-90";
        private const string GestorDoContrato = "Gestor Teste";
        private readonly DateTime _dataFinalDaVigenciaDoContrato = DateTime.Now.AddMonths(2);
        private readonly TipoDeSolicitacaoDeManutencao _tipoDeSolicitacaoDeManutencao = TipoDeSolicitacaoDeManutencao.Jardinagem;
        private string _identificadorDaSubsidiaria = "ID-123";
        private string _justificativa = "Justificativa Teste";
        private DateTime _inicioDesejadoParaManutencao = DateTime.Now.AddDays(20);

        private SolicitacaoDeManutencao CriarNovaSolicitacao()
        {
            return new SolicitacaoDeManutencao(
                _identificadorDaSubsidiaria,
                IdentificadorDoSolicitante,
                NomeDoSolicitante,
                _tipoDeSolicitacaoDeManutencao,
                _justificativa,
                NumeroDoContrato,
                NomeDaTerceirizadaDoContrato,
                CnpjDaTerceirizadaDoContrato,
                GestorDoContrato,
                _dataFinalDaVigenciaDoContrato,
                _inicioDesejadoParaManutencao
            );
        }

        [Fact]
        public void Deve_criar_solicitacao_de_manutencao()
        {
            var solicitacao = new
            {
                IdentificadorDaSubsidiaria = _identificadorDaSubsidiaria,
                Solicitante = new Autor(IdentificadorDoSolicitante, NomeDoSolicitante),
                TipoDeSolicitacaoDeManutencao = _tipoDeSolicitacaoDeManutencao,
                Justificativa = _justificativa,
                Contrato = new Contrato(
                    NumeroDoContrato,
                    NomeDaTerceirizadaDoContrato,
                    CnpjDaTerceirizadaDoContrato,
                    GestorDoContrato,
                    _dataFinalDaVigenciaDoContrato),
                InicioDesejadoParaManutencao = _inicioDesejadoParaManutencao
            };

            var solicitacaoDeManutencao = CriarNovaSolicitacao();

            solicitacao.ToExpectedObject().ShouldMatch(solicitacaoDeManutencao);

        }

        [Fact]
        public void Deve_solicitacao_de_manutencao_ter_data_de_hoje()
        {
            var dataDaSolicitacaoEsperada = DateTime.Now;
            var solicitacaoDeManutencao = CriarNovaSolicitacao();

            Assert.Equal(dataDaSolicitacaoEsperada.Date, solicitacaoDeManutencao.DataDaSolicitacao.Date);
        }

        [Fact]
        public void Deve_solicitacao_de_manutencao_iniciar_com_status_pedente()
        {
            var statusDaSolicitacao = StatusDeSolicitacao.Pendente;
            var solicitacaoDeManutencao = CriarNovaSolicitacao();

            Assert.Equal(statusDaSolicitacao, solicitacaoDeManutencao.StatusDaSolicitacao);
        }

        [Fact]
        public void Deve_validar_subsidiaria()
        {
            const string mensagemEsperada = "A subsidiária é obrigatória.";
            _identificadorDaSubsidiaria = "";

            AssertExtensions.ThrowsWithMessage(() => CriarNovaSolicitacao(),
             mensagemEsperada);
        }

        [Fact]
        public void Deve_cancelar_solicitacao_de_manutencao()
        {
            var solicitacao = FluentBuilder<SolicitacaoDeManutencao>.New().Build();
            solicitacao.Cancelar();

            Assert.Equal(StatusDeSolicitacao.Cancelada, solicitacao.StatusDaSolicitacao);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void Deve_validar_justificativa(string justificativaInvalida)
        {
            const string mensagemEsperada = "A justificativa é obrigatória.";
            _justificativa = justificativaInvalida;

            AssertExtensions.ThrowsWithMessage(() => CriarNovaSolicitacao(),
             mensagemEsperada);
        }

        [Fact]
        public void Deve_validar_data_de_inicio_desejado_para_manutencao()
        {
            const string mensagemEsperada = "Data de início para manutenção não pode ser inferior a data atual.";
            var dataInvalida = DateTime.Now.AddDays(-1);
            _inicioDesejadoParaManutencao = dataInvalida;

            AssertExtensions.ThrowsWithMessage(() => CriarNovaSolicitacao(),
             mensagemEsperada);
        }

        [Fact]
        public void Deve_reprovar_solicitacao_de_manutencao()
        {
            var aprovador = new Autor(20, "Rejeitador Teste");
            var solicitacao = FluentBuilder<SolicitacaoDeManutencao>.New().Build();

            solicitacao.Rejeitar(aprovador);

            Assert.Equal(StatusDeSolicitacao.Rejeitada, solicitacao.StatusDaSolicitacao);
        }

        [Fact]
        public void Deve_informar_aprovador_da_rejeicao()
        {
            var aprovador = new Autor(20, "Rejeitador Teste");
            var solicitacao = FluentBuilder<SolicitacaoDeManutencao>.New().Build();

            solicitacao.Rejeitar(aprovador);

            Assert.Equal(aprovador, solicitacao.Aprovador);
        }

        [Fact]
        public void Deve_aprovar_solicitacao_de_manutencao()
        {
            var aprovador = new Autor(20, "Aprovador Teste");
            var solicitacao = FluentBuilder<SolicitacaoDeManutencao>.New().Build();

            solicitacao.Aprovar(aprovador);

            Assert.Equal(StatusDeSolicitacao.Aprovada, solicitacao.StatusDaSolicitacao);
        }

        [Fact]
        public void Deve_informar_aprovador_da_aprovacao()
        {
            var aprovador = new Autor(20, "Aprovador Teste");
            var solicitacao = FluentBuilder<SolicitacaoDeManutencao>.New().Build();

            solicitacao.Aprovar(aprovador);

            Assert.Equal(aprovador, solicitacao.Aprovador);
        }
    }
}

