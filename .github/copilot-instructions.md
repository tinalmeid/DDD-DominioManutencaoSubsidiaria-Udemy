## Visão rápida do repositório

Este repositório implementa um exemplo didático de Domain-Driven Design (.NET 6) organizado segundo princípios de Clean Architecture.
Solução: `Manutencao.Solicitacao.sln` com camadas principais:

- `Manutencao.Solicitacao.Dominio` — Entidades, Agregados, Value Objects e regras de negócio.
- `Manutencao.Solicitacao.Aplicacao` — Serviços de aplicação, DTOs e fábricas (coordenação de casos de uso).
- `Manutencao.Solicitacao.Infra.*` — Implementações de repositórios, integrações (ERP/Email) e bootstrap.
- `Manutencao.Solicitacao.Api` — API REST (controllers) — ponto de entrada.
- `test/Manutencao.SolicitacaoTestes` — Testes unitários / integração.

Principais arquivos a consultar:

- `README.MD` — visão geral e requisitos (menciona .NET 6, Docker e json-server).
- `.github/workflows/ci.yml` — pipeline CI (build, testes, cobertura e SonarCloud).
- `src/Manutencao.Solicitacao.Api` — API e execução local.
- `test/Manutencao.SolicitacaoTestes` — projeto de testes (caminho usado pelo CI).

## Comandos essenciais (rápido)

Instale .NET 6 SDK. Para build/test localmente use:

```powershell
dotnet build --no-incremental
dotnet test test/Manutencao.SolicitacaoTestes/Manutencao.SolicitacaoTestes.csproj /p:CollectCoverage=true /p:CoverletOutputFormat=opencover --logger "trx;LogFileName=TestResults.trx"
dotnet run --project src/Manutencao.Solicitacao.Api
```

Observações relacionadas à cobertura e CI:

- O workflow CI gera cobertura em formato OpenCover e espera o arquivo em `test/Manutencao.SolicitacaoTestes/coverage.opencover.xml`.
- SonarCloud está configurado em `.github/workflows/ci.yml` e usa o secret `SONARCLOUD_TOKEN` (key: `tinalmeid_DDD_DominioManutencaoSubsidiaria_Udemy`).
- Artefatos opcionais: `src/Manutencao.Solicitacao.Api/bin/Release/` (upload no CI).

## Padrões e convenções do projeto (observáveis)

- Nomeclatura: classes e interfaces em português; interfaces começam com `I` (ex.: `IBuscadorDeContrato`).
- Fábricas/Serviços de aplicação citados no README: `FabricaDeSolicitacaoDeManutencao` — comece por `Aplicacao` ao implementar flows.
- Integrações externas (ERP/Email) estão isoladas atrás de interfaces no domínio e implementadas na camada `Infra`.
- Testes possuem projeto separado sob `test/` e o pipeline espera rodá-los isoladamente (apontado no CI).

## Integrações e ambiente de desenvolvimento

- Banco: README indica uso de SQL Server via Docker — ver `Infra` para strings de conexão/Bootstrap.
- ERP simulado: o repositório requer `json-server` para mockar contratos (instalar globalmente com `npm install -g json-server`).
- Antes de rodar a API localmente verifique containers necessários (SQL) e se o mock do ERP está ativo.

## Onde alterar para requisitos comuns

- Implementar casos de uso / factories: alterar `Manutencao.Solicitacao.Aplicacao`.
- Mudanças de persistência: procurar implementações em `Manutencao.Solicitacao.Infra.*` que implementam interfaces do domínio.
- Testes: adicione cenários em `test/Manutencao.SolicitacaoTestes` e garanta que a cobertura gerada respeite o caminho usado pelo CI.

## Dicas rápidas para o agente

- Priorize mudanças na camada de Aplicação para novos requisitos de fluxo; mantenha invariantes no Domínio.
- Evite modificar regras do domínio sem evidência em testes — elas são o núcleo do sistema.
- Ao propor mudanças que impactam CI/Quality, atualize `.github/workflows/ci.yml` e verifique o caminho de cobertura.

## Pontos não descobertos automaticamente (peça ao mantenedor quando necessário)

- Endpoints exatos do ERP fake (json-server) e fixtures iniciais (arquivo `db.json` ou pasta de mocks).
- Variáveis de ambiente esperadas pela camada `Infra` para strings de conexão.

Se algo estiver impreciso ou faltar contexto, diga o trecho que não conseguiu localizar (ex.: path de fixture ou string de conexão) e eu ajusto as instruções.
