using System;
using Manutencao.Solicitacao.Aplicacao.SolicitacoesDeManutencao;
using Manutencao.Solicitacao.Dominio;
using Manutencao.Solicitacao.Dominio.SolicitacoesDeManutencao;
using Manutencao.Solicitacao.Aplicacao.Subsidiarias;

namespace Manutencao.Solicitacao.Aplicacao.SolicitacoesDeManutencao
{
    public class FabricaDeSolicitacaoDeManutencao
    {
        private readonly ISubsidiariaRepositorio _subsidiariaRepositorio;
        private readonly IBuscadorDeContrato _buscadorDeContrato;

        protected FabricaDeSolicitacaoDeManutencao()
        {
            // Construtor protegido para uso em testes ou herança
        }

        public FabricaDeSolicitacaoDeManutencao(
            ISubsidiariaRepositorio subsidiariaRepositorio,
            IBuscadorDeContrato buscadorDeContrato)
        {
            _subsidiariaRepositorio = subsidiariaRepositorio;
            _buscadorDeContrato = buscadorDeContrato;
        }

        public virtual SolicitacaoDeManutencao Fabricar(SolicitacaoDeManutencaoDto dto)
        {
            var subsidiaria = _subsidiariaRepositorio.ObterPorId(dto.SubsidiariaId);
            ExcecaoDeDominioException.LancarQuando(subsidiaria == null,
             "A subsidiária não foi encontrada.");

            var contratoDto = _buscadorDeContrato.Buscar(dto.NumeroDoContrato).Result;
            ExcecaoDeDominioException.LancarQuando(contratoDto == null,
             "O contrato não foi encontrado.");

            var tipoDeSolicitacaoDeManutencao =
                Enum.Parse(typeof(TipoDeSolicitacaoDeManutencao),
                dto.TipoDeSolicitacaoDeManutencao.ToString());

            return new SolicitacaoDeManutencao(
                subsidiaria.Id,
                dto.SolicitanteId,
                dto.NomeDoSolicitante,
                (TipoDeSolicitacaoDeManutencao)tipoDeSolicitacaoDeManutencao,
                dto.Justificativa,
                contratoDto.NumeroDoContrato,
                contratoDto.NomeDaTerceirizadaDoContrato,
                contratoDto.CnpjDaTerceirizadaDoContrato,
                contratoDto.GestorDoContrato,
                contratoDto.DataFinalDaVigenciaDoContrato,
                dto.InicioDesejadoParaManutencao);
        }
    }   
}