# Subtask 07: Implementar AuthorizeBySchemeOperationFilter

## Status
- **Estado:** 🔄 Pendente
- **Data de Início:** -
- **Data de Conclusão:** -

## Descrição
Copiar e adaptar a implementação de `AuthorizeBySchemeOperationFilter.cs` do projeto orderhub, que filtra operações do Swagger para adicionar automaticamente os esquemas de segurança baseado nos atributos `[Authorize]`.

## Objetivo
Criar o filtro que detecta automaticamente qual esquema de autenticação usar baseado no atributo `[Authorize]` nos endpoints, permitindo que o Swagger exiba corretamente os botões de autenticação.

## Arquivo a Criar

### `src/InterfacesExternas/FastFood.PayStream.Api/Config/Auth/AuthorizeBySchemeOperationFilter.cs`

## Passos de Implementação

1. [ ] Criar diretório `Config/Auth/` se não existir
2. [ ] Copiar `AuthorizeBySchemeOperationFilter.cs` do orderhub
3. [ ] Adaptar namespace para `FastFood.PayStream.Api.Config.Auth`
4. [ ] Verificar que todas as dependências estão disponíveis
5. [ ] Verificar que o código compila sem erros

## Estrutura Esperada

O filtro deve:
- Detectar atributos `[Authorize]` nos endpoints
- Extrair o esquema de autenticação do atributo (ex: "CustomerBearer", "Cognito")
- Adicionar o esquema correspondente ao Swagger operation
- Ignorar endpoints com `[AllowAnonymous]`

## Como Testar

- Executar `dotnet build` no projeto Api (deve compilar sem erros)
- Executar `dotnet run` e acessar Swagger UI
- Verificar que endpoints com `[Authorize(AuthenticationSchemes = "CustomerBearer")]` mostram apenas CustomerBearer no Swagger
- Verificar que endpoints com `[AllowAnonymous]` não mostram botão de autenticação

## Critérios de Aceite

- [ ] `AuthorizeBySchemeOperationFilter.cs` criado
- [ ] Namespace adaptado para `FastFood.PayStream.Api.Config.Auth`
- [ ] Filtro implementa `IOperationFilter`
- [ ] Filtro detecta atributos `[Authorize]`
- [ ] Filtro extrai esquema de autenticação do atributo
- [ ] Filtro adiciona esquema ao Swagger operation
- [ ] Filtro ignora endpoints com `[AllowAnonymous]`
- [ ] Código compila sem erros
- [ ] Swagger exibe corretamente os esquemas de autenticação

## Observações

- Se não encontrar o arquivo no orderhub, pode ser necessário criar baseado na documentação da story do orderhub
- O filtro deve funcionar automaticamente após ser registrado no `AddSwaggerGen()`

## Referências

- **Story do OrderHub:** `C:\Projetos\Fiap\fiap-fase4-orderhub-api\story\Storie-04-Implementar_Autenticacao_Autorizacao\subtask\Subtask-06-Implementar_OperationFilter.md`
