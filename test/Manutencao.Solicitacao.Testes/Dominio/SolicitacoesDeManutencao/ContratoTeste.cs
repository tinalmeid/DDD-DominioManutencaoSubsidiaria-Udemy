using System;
using Xunit;
using ExpectedObjects;
using Manutencao.Solicitacao.Testes._Util;
using Manutencao.Solicitacao.Dominio.SolicitacoesDeManutencao;

namespace Manutencao.Solicitacao.Testes.Dominio.SolicitacoesDeManutencao
{
    public class ContratoTeste
    {
        private const string Numero = "12345";
        private const string NomeDaTerceirizada = "Gramas Ltda";
        private const string CnpjDaTerceirizada = "12345678000190";
        private const string GestorDoContrato = "Marina Silva";
        private readonly DateTime DataFinalDaVigencia = DateTime.Now.AddMonths(1);

        [Fact]
        public void Deve_criar_contrato()
        {
            var contratoEsperado = new
            {
                Numero,
                NomeDaTerceirizada,
                CnpjDaTerceirizada,
                GestorDoContrato,
                DataFinalDaVigencia
            };

            var contrato = new Contrato(
                Numero,
                NomeDaTerceirizada,
                CnpjDaTerceirizada,
                GestorDoContrato,
                DataFinalDaVigencia);

            contratoEsperado.ToExpectedObject().ShouldMatch(contrato);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void Deve_validar_numero(string numeroDoContratoInvalido)
        {
            const string mensagemEsperada = "O número do contrato é obrigatório.";

            AssertExtensions.ThrowsWithMessage(() => new Contrato(
                numeroDoContratoInvalido,
                NomeDaTerceirizada,
                CnpjDaTerceirizada,
                GestorDoContrato,
                DataFinalDaVigencia),
             mensagemEsperada);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void Deve_validar_nome_da_terceirizada(string nomeDaTerceirizadaInvalido)
        {
            const string mensagemEsperada = "O nome da terceirizada do contrato é obrigatório.";

            AssertExtensions.ThrowsWithMessage(() => new Contrato(
                Numero,
                nomeDaTerceirizadaInvalido,
                CnpjDaTerceirizada,
                GestorDoContrato,
                DataFinalDaVigencia),
             mensagemEsperada);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void Deve_validar_cnpj_da_terceirizada(string cnpjDaTerceirizadaInvalido)
        {
            const string mensagemEsperada = "O CNPJ da terceirizada do contrato é obrigatório.";

            AssertExtensions.ThrowsWithMessage(() => new Contrato(
                Numero,
                NomeDaTerceirizada,
                cnpjDaTerceirizadaInvalido,
                GestorDoContrato,
                DataFinalDaVigencia),
             mensagemEsperada);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void Deve_validar_gestor_do_contrato(string gestorDoContratoInvalido)
        {
            const string mensagemEsperada = "O nome do gestor do contrato é obrigatório.";

            AssertExtensions.ThrowsWithMessage(() => new Contrato(
                Numero,
                NomeDaTerceirizada,
                CnpjDaTerceirizada,
                gestorDoContratoInvalido,
                DataFinalDaVigencia),
             mensagemEsperada);
        }

        [Fact]
        public void Deve_validar_data_final_da_vigencia()
        {
            const string mensagemEsperada = "A data final da vigência deve ser MAIOR que a data atual.";

            AssertExtensions.ThrowsWithMessage(() => new Contrato(
                Numero,
                NomeDaTerceirizada,
                CnpjDaTerceirizada,
                GestorDoContrato,
                DateTime.Now.AddDays(-1)),
             mensagemEsperada);
        }
    }
}


