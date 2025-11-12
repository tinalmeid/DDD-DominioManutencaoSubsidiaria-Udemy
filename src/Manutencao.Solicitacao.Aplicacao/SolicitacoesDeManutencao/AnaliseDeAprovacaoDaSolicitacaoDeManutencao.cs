using System.Reflection;
using System.Threading.Tasks;
using Manutencao.Solicitacao.Dominio;
using Manutencao.Solicitacao.Dominio.SolicitacoesDeManutencao;

namespace Manutencao.Solicitacao.Aplicacao.SolicitacoesDeManutencao
{
    public class AnaliseDeAprovacaoDaSolicitacaoDeManutencao
    {
        private readonly ISolicitacaoDeManutencaoRepositorio _solicitacaoDeManutencaoRepositorio;
        private readonly INotificaReprovacaoParaSolicitante _notificaReprovacaoParaSolicitante;
        private readonly INotificaContextoDeServico _notificaContextoDeServico;

        public AnaliseDeAprovacaoDaSolicitacaoDeManutencao(
            ISolicitacaoDeManutencaoRepositorio solicitacaoDeManutencaoRepositorio,
            INotificaReprovacaoParaSolicitante notificaReprovacaoParaSolicitante,
            INotificaContextoDeServico notificaContextoDeServico)
        {
            _solicitacaoDeManutencaoRepositorio = solicitacaoDeManutencaoRepositorio;
            _notificaReprovacaoParaSolicitante = notificaReprovacaoParaSolicitante;
            _notificaContextoDeServico = notificaContextoDeServico;
        }

        public async Task Analisar(AnaliseDeAprovacaoDto analiseDeAprovacaoDto)
        {
            var solicitacaoDeManutencao =
                _solicitacaoDeManutencaoRepositorio.ObterPorId(analiseDeAprovacaoDto.IdDaSolicitacao);
            ExceptionDoDominioException.LancarQuando(solicitacaoDeManutencao == null,
                "Solicitação de manutenção não encontrada.");
            ExceptionDoDominioException.LancarQuando(solicitacaoDeManutencao.Reprovada(),
                "Solicitação de manutencao já está com status: reprovada.");
            ExceptionDoDominioException.LancarQuando(solicitacaoDeManutencao.Aprovada(),
                "Solicitação de manutencao já está com status: aprovada.");

            var aprovador = new Autor(analiseDeAprovacaoDto.AprovadorId, analiseDeAprovacaoDto.NomeDoAprovador);

            if (analiseDeAprovacaoDto.Aprovada)
                await Aprovar(solicitacaoDeManutencao, aprovador);
            else
                await Reprovar(solicitacaoDeManutencao, aprovador);

        }

        private async Task Aprovar(SolicitacaoDeManutencao solicitacaoDeManutencao, Autor aprovador)
        {
            solicitacaoDeManutencao.Aprovar(aprovador);
            await _notificaContextoDeServico.Notificar(solicitacaoDeManutencao);
        }

        private void Reprovar(SolicitacaoDeManutencao solicitacaoDeManutencao, Autor aprovador)
        {
            solicitacaoDeManutencao.Reprovar(aprovador);
            _notificaReprovacaoParaSolicitante.Notificar(solicitacaoDeManutencao);
        }        

    }
}