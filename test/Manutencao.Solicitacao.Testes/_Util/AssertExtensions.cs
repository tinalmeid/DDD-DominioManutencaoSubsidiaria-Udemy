using System;
using System.Threading.Tasks;
using Manutencao.Solicitacao.Dominio; // Importa a Exceção personalizada
using Xunit; // Importa o framework de testes

namespace Manutencao.Solicitacao.Testes._Util
{
    public static class AssertExtensions
    {
        // Método síncrono para verificar se uma ação lança a ExceçãoDeDominio com a mensagem esperada
        public static void ThrowsWithMessage(Action testCode, string messageExpected)
        {
            var message = Assert.Throws<ExcecaoDeDominioException>(testCode).Message;
            // Verifica se a mensagem da exceção é igual à mensagem esperada
            Assert.Equal(messageExpected, message);
        }

        // Método assíncrono para verificar se uma ação lança a ExceçãoDeDominio com a mensagem esperada
        public static async Task ThrowsWithMessageAsync(Func<Task> testCode, string messageExpected)
        {
            var exception = await Assert.ThrowsAsync<ExcecaoDeDominioException>(testCode);
            Assert.Equal(messageExpected, exception.Message);
        }
    }
}