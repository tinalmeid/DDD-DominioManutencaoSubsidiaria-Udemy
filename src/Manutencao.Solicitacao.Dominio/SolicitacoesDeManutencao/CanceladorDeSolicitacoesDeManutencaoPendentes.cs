using System.Collections.Generic;

namespace Manutencao.Solicitacao.Dominio.SolicitacoesDeManutencao
{
    // Implementação do serviço de domínio para cancelar solicitações de manutenção pendentes
    public class CanceladorDeSolicitacoesDeManutencaoPendentes : ICanceladorDeSolicitacoesDeManutencaoPendentes
    {
        public void Cancelar(IEnumerable<SolicitacaoDeManutencao> solicitacoesDeManutencaoPendentes)
        {
            // Lógica para cancelar as solicitações de manutenção pendentes
            if (solicitacoesDeManutencaoPendentes == null)
                return;

            foreach (var solicitacaoDeManutencao in solicitacoesDeManutencaoPendentes)
            {
                solicitacaoDeManutencao.Cancelar();
            }
        }
    }
}