namespace Manutencao.Solicitacao.Dominio.Subsidiarias
{
    public class Subsidiaria : Entidade
    {
        public string Nome { get; private set; }

        public Subsidiaria(string nome)
        {
            ExcecaoDeDominioException.LancarQuando(string.IsNullOrEmpty(nome),
             "O nome da subsidiária é Obrigatório.");

            Nome = nome;
        }

    }
}