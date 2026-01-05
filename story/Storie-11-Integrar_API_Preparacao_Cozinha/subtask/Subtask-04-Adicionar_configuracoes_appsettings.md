# Subtask 04: Adicionar configurações no appsettings.json

## Status
- **Estado:** 🔄 Em desenvolvimento
- **Data de Conclusão:** [DD/MM/AAAA]

## Descrição
Adicionar as configurações necessárias para a integração com a API de preparação da cozinha no arquivo appsettings.json.

## Passos de implementação
- [ ] Abrir arquivo `src/InterfacesExternas/FastFood.PayStream.Api/appsettings.json`
- [ ] Adicionar seção `KitchenApi` com as seguintes propriedades:
  - `BaseUrl` - URL base da API de preparação (ex: "http://localhost:5010")
  - `Token` - Token de autenticação para a API
- [ ] Manter estrutura JSON válida
- [ ] Adicionar comentários se necessário (JSON não suporta comentários, mas pode documentar no README se necessário)

## Exemplo de estrutura:
```json
{
  "KitchenApi": {
    "BaseUrl": "http://localhost:5010",
    "Token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9..."
  }
}
```

## Como testar
- Executar `dotnet build` no projeto Api (deve compilar sem erros)
- Verificar que o JSON é válido
- Validar que as configurações podem ser lidas via `IConfiguration`

## Critérios de aceite
- [ ] Seção `KitchenApi` adicionada no appsettings.json
- [ ] Propriedade `BaseUrl` configurada
- [ ] Propriedade `Token` configurada
- [ ] JSON é válido
- [ ] Projeto Api compila sem erros
