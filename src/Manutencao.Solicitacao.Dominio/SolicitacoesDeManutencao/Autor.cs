    namespace Manutencao.Solicitacao.Dominio.SolicitacoesDeManutencao
{
    public class Autor
    {
        public int Identificador { get; private set; }
        public String Nome { get; private set; }

        private Autor()
        {
            //Apenas para satisfação do EF Core
            Nome = string.Empty;
        }
        public Autor(int identificador, String nome)
        {
            ExcecaoDeDominioException.LancarQuando(string.IsNullOrWhiteSpace(nome),
             "Nome do autor é obrigatório.");

            Identificador = identificador;
            Nome = nome;
        }
    }
}
