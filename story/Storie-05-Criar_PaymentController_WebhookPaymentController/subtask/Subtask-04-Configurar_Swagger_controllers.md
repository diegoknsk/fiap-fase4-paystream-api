# Subtask 04: Configurar documentação Swagger nos controllers

## Status
- **Estado:** 🔄 Em desenvolvimento
- **Data de Conclusão:** [DD/MM/AAAA]

## Descrição
Garantir que os controllers estão configurados corretamente para documentação Swagger, incluindo XML comments e configuração do Swagger no Program.cs se necessário.

## Passos de implementação
- [ ] Verificar se o Swagger está configurado no `Program.cs` da API
- [ ] Se não estiver, adicionar configuração do Swagger:
  - `builder.Services.AddEndpointsApiExplorer()`
  - `builder.Services.AddSwaggerGen()`
  - `app.UseSwagger()` e `app.UseSwaggerUI()` no pipeline
- [ ] Verificar se a geração de XML documentation está habilitada no `.csproj` da API:
  - Adicionar `<GenerateDocumentationFile>true</GenerateDocumentationFile>` no PropertyGroup
  - Adicionar `<NoWarn>$(NoWarn);1591</NoWarn>` para suprimir warnings de XML comments
- [ ] Configurar SwaggerGen para incluir XML comments:
  - `c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "FastFood.PayStream.Api.xml"))`
- [ ] Verificar que os controllers têm comentários XML adequados
- [ ] Testar que o Swagger exibe os controllers corretamente

## Como testar
- Executar `dotnet build` no projeto Api (deve compilar sem erros)
- Executar `dotnet run` no projeto Api
- Acessar `/swagger` no navegador
- Verificar que os controllers aparecem no Swagger
- Verificar que os comentários XML aparecem na documentação
- Validar que não há erros no Swagger UI

## Critérios de aceite
- [ ] Swagger configurado no `Program.cs` (AddEndpointsApiExplorer, AddSwaggerGen)
- [ ] Swagger middleware configurado no pipeline (UseSwagger, UseSwaggerUI)
- [ ] Geração de XML documentation habilitada no .csproj
- [ ] SwaggerGen configurado para incluir XML comments
- [ ] Controllers aparecem no Swagger UI
- [ ] Comentários XML dos controllers aparecem na documentação
- [ ] Projeto Api compila sem erros
- [ ] Swagger acessível em `/swagger` quando API está rodando
