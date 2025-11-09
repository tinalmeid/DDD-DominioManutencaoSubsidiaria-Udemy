# 📘 DominioManutencaoSubsidiaria

Este repositório contém o projeto de estudos do curso "Aprendendo Domain-Driven Design (DDD) do Zero" da Udemy, implementado em .NET Core (C#).

O projeto é estruturado em Arquitetura Limpa (Clean Architecture), onde o Domínio é o coração, isolando as regras de negócio de alto valor.

## 🚀 Estrutura da Solução

A solução `Manutencao.Solicitacao.sln` é organizada pelas seguintes camadas/projetos:

- **Dominio (`Manutencao.Solicitacao.Dominio`):** Contém as Entidades (Agregados), Objetos de Valor e Regras de Negócio. É a camada central.
- **Aplicação (`Manutencao.Solicitacao.Aplicacao`):** Coordena os fluxos de trabalho do Domínio (Serviços de Aplicação, DTOs e Fábricas).
- **Infraestrutura (`Manutencao.Solicitacao.Infra.*`):** Implementa os Repositórios (BancoDeDados), as integrações (ErpContracts, Email) e o setup (`Bootstrap`).
- **Interface (`Manutencao.Solicitacao.Api`):** O ponto de entrada da API (REST Controllers).
- **Testes (`Manutencao.SolicitacaoTestes`):** Projeto dedicado a Testes Unitários e de Integração.

## 🛠️ Requisitos e Setup Local

Para executar e desenvolver neste projeto, você precisará de:

1.  **Plataforma .NET Core (SDK):** Versão 6.0 ou superior.
2.  **Docker:** Necessário para subir o container do SQL Server.
3.  **json-server (API Faker):** Para simular o ERP de contratos.
    ```bash
    npm install -g json-server
    ```

## ⚙️ CI/CD e Qualidade

Este projeto utiliza **GitHub Actions** para automatizar o _build_ e os testes, e o **SonarCloud** para análise de qualidade do código em Pull Requests (configurado no `.github/workflows/ci.yml`).

## 🔑 Próximos Passos de Codificação

O foco atual é na camada de Aplicação, implementando:

1.  `FabricaDeSolicitacaoDeManutencao`
2.  Interfaces de Integração (`IBuscadorDeContrato`)
