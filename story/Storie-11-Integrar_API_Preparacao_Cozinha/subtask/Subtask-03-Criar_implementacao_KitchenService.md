# Subtask 03: Criar implementação KitchenService na Infra

## Status
- **Estado:** 🔄 Em desenvolvimento
- **Data de Conclusão:** [DD/MM/AAAA]

## Descrição
Criar a implementação concreta do KitchenService na camada Infra que faz a chamada HTTP para a API de preparação da cozinha, usando HttpClient e seguindo as boas práticas de .NET.

## Passos de implementação
- [ ] Criar arquivo `src/Infra/FastFood.PayStream.Infra/Services/KitchenService.cs`
- [ ] Definir namespace `FastFood.PayStream.Infra.Services`
- [ ] Criar classe pública `KitchenService` que implementa `IKitchenService`
- [ ] Adicionar dependências no construtor:
  - `IHttpClientFactory httpClientFactory` - Para criar HttpClient
  - `IConfiguration configuration` - Para ler configurações (URL e token)
- [ ] Criar campo privado para armazenar URL base e token (lidos da configuração)
- [ ] Implementar método `SendToPreparationAsync`:
  - Criar HttpClient usando `httpClientFactory.CreateClient()`
  - Configurar base address do HttpClient (se necessário)
  - Criar objeto `KitchenPreparationRequest` com orderId e orderSnapshot
  - Serializar objeto para JSON usando `System.Text.Json.JsonSerializer`
  - Criar `HttpRequestMessage` com método POST
  - Configurar URL completa: `{BaseUrl}/api/Preparation`
  - Adicionar header `Authorization: Bearer {Token}`
  - Adicionar header `Content-Type: application/json`
  - Adicionar body JSON serializado
  - Enviar requisição usando `httpClient.SendAsync()`
  - Verificar status code da resposta
  - Se não for sucesso (2xx), lançar `HttpRequestException` com detalhes do erro
  - Garantir que exceções HTTP sejam propagadas
- [ ] Adicionar comentários XML para documentação
- [ ] Tratar exceções adequadamente (propagar erros HTTP)

## Configurações necessárias
- `KitchenApi:BaseUrl` - URL base da API de preparação (ex: "http://localhost:5010")
- `KitchenApi:Token` - Token de autenticação para a API

## Como testar
- Executar `dotnet build` no projeto Infra (deve compilar sem erros)
- Verificar que a classe implementa `IKitchenService`
- Validar que HttpClient é usado corretamente
- Verificar que headers são configurados corretamente

## Critérios de aceite
- [ ] Arquivo `KitchenService.cs` criado em `src/Infra/FastFood.PayStream.Infra/Services/`
- [ ] Classe `KitchenService` implementa `IKitchenService`
- [ ] Construtor recebe `IHttpClientFactory` e `IConfiguration`
- [ ] Método `SendToPreparationAsync` implementado
- [ ] HttpClient é criado via `IHttpClientFactory`
- [ ] Requisição POST é feita para `/api/Preparation`
- [ ] Headers `Authorization` e `Content-Type` são configurados
- [ ] Body contém `orderId` e `orderSnapshot` serializados
- [ ] Erros HTTP são propagados como exceções
- [ ] Comentários XML adicionados
- [ ] Projeto Infra compila sem erros
