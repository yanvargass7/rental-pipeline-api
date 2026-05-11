# Rental Pipeline API

API REST desenvolvida em .NET 8 para gerenciamento do fluxo de locação de imóveis, utilizando arquitetura em camadas, máquina de estados, histórico de transições e publicação de eventos.

---
# Como Executar o Projeto

## 1. Clonar o repositório

```bash
git clone <repository-url>
cd RentalPipeline
```

---

## 2. Configurar a aplicação
A aplicação já possui configuração padrão para execução local via Docker ou
Atualize a connection string no:

```text
appsettings.json
```

Exemplo:

```json
"ConnectionStrings": {
  "DefaultConnection": "Host=localhost;Port=5432;Database=rentaldb;Username=postgres;Password=postgres"
}
```

---

# Opção 1 — Executar com Docker

Executa:
- API
- PostgreSQL
- RabbitMQ
- PgAdmin

```bash
docker compose up --build
```

### Serviços

| Serviço | URL |
|---|---|
| Swagger | http://localhost:8080/swagger |
| RabbitMQ | http://localhost:15672 |
| PgAdmin | http://localhost:5050 |

---

# Opção 2 — Executar Localmente

## Aplicar migrations

```bash
dotnet ef database update
```

## Executar a API

```bash
dotnet run --project RentalPipeline.API
```

---

# Executar Testes

```bash
dotnet test
```

## Coverage

```bash
dotnet test --collect:"XPlat Code Coverage"
```

---

# Tecnologias Utilizadas

- .NET 8
- ASP.NET Core Web API
- Entity Framework Core
- PostgreSQL
- RabbitMQ
- xUnit
- FluentAssertions
- Moq
- Swagger / OpenAPI

---

# Arquitetura

O projeto foi estruturado seguindo separação de responsabilidades em camadas:

```text
RentalPipeline.API
RentalPipeline.Application
RentalPipeline.Domain
RentalPipeline.Infrastructure
RentalPipeline.Tests
```

## Camadas

### API
Responsável pelos controllers, configuração da aplicação, Swagger e middlewares.

### Application
Contém:
- services
- DTOs
- mappers
- interfaces
- eventos da aplicação

### Domain
Contém:
- entidades
- regras de negócio
- máquina de estados
- exceptions de domínio

### Infrastructure
Responsável por:
- Entity Framework Core
- repositories
- Unit Of Work
- RabbitMQ

### Tests
Contém testes unitários de:
- domínio
- services

---

# Regras de Negócio

## Fluxo obrigatório da proposta

```text
NEW
→ CREDIT_ANALYSIS
→ CONTRACT_ISSUED
→ SIGNED
→ ACTIVE
```

Também é possível:

```text
REJECTED
CANCELLED
```

a partir de qualquer estado anterior a `ACTIVE`.

---

## Regras implementadas

- Um imóvel só pode receber proposta quando estiver `Available`
- Não é permitido pular etapas da máquina de estados
- Quando a proposta chega em `Active`, o imóvel passa para `Rented`
- Quando a proposta é `Rejected` ou `Cancelled`, o imóvel volta para `Available`
- Histórico de transições é registrado automaticamente
- Evento é publicado quando o contrato se torna `Active`

---

# Máquina de Estados

A máquina de estados foi modelada diretamente na entidade `RentalProposal`, centralizando as regras de transição no domínio da aplicação.

Exemplo:

```csharp
proposal.MoveTo(ProposalStatus.CreditAnalysis);
```

Essa abordagem garante:
- encapsulamento das regras
- proteção contra estados inválidos
- maior facilidade de manutenção
- testes mais confiáveis

---

# Prevenção de Concorrência

Para evitar que duas propostas sejam criadas simultaneamente para o mesmo imóvel disponível, foi utilizada uma abordagem baseada em:

- transações
- lock pessimista (`FOR UPDATE`)

Implementado através de:

```csharp
GetByIdForUpdateAsync()
```

Isso garante integridade de dados durante o processo de negociação.

---

# Arquitetura Orientada a Eventos

Quando uma proposta atinge o status `Active`, um evento é publicado no RabbitMQ simulando integração com sistemas externos, como financeiro ou faturamento.

Evento publicado:

```text
ContractActivatedEvent
```

---

# Histórico de Transições

Toda mudança de status gera automaticamente um registro de auditoria contendo:

- status anterior
- novo status
- data da alteração

Endpoint:

```http
GET /api/proposals/{id}/history
```

---

# Cobertura de Testes

Os testes cobrem principalmente:

- regras de negócio
- máquina de estados
- workflow da proposta
- publicação de eventos
- histórico de auditoria

A cobertura foi priorizada nas partes críticas do domínio da aplicação.

---

# Padrões e Decisões de Design

## Domain-Driven Design (DDD)

As regras de negócio foram centralizadas no domínio para evitar lógica espalhada em controllers ou repositories.

---

## Repository Pattern

Utilizado para abstrair acesso a dados e facilitar testes.

---

## Event-Driven Architecture

Eventos foram utilizados para desacoplar integrações externas da regra principal de negócio.

---


# Autor

Yan Vargas