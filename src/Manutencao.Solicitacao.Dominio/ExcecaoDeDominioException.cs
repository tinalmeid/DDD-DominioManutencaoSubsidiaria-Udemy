using System;

namespace Manutencao.Solicitacao.Dominio
{
    public class ExcecaoDeDominioException : Exception
    {
        public ExcecaoDeDominioException(string mensagem) : base(mensagem) { }
        public static void LancarQuando(bool regraInvalida, string mensagem)
        {
            if (regraInvalida)
            {
                throw new ExcecaoDeDominioException(mensagem);
            }
        }
    }
}