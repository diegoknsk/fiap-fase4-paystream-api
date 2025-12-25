# Subtask 07: Criar projeto Api

## Descrição
Criar o projeto `FastFood.PayStream.Api` na pasta `src/InterfacesExternas/`, que será a API HTTP ASP.NET Core para execução no EKS, contendo controllers, Program.cs e configurações.

## Passos de implementação
- Criar pasta `src/InterfacesExternas/FastFood.PayStream.Api/`
- Executar `dotnet new webapi -n FastFood.PayStream.Api -f net8.0` na pasta criada
- Adicionar referências:
  - `dotnet add reference ../../Core/FastFood.PayStream.Application/FastFood.PayStream.Application.csproj`
  - `dotnet add reference ../../Core/FastFood.PayStream.CrossCutting/FastFood.PayStream.CrossCutting.csproj`
  - `dotnet add reference ../../Core/FastFood.PayStream.Infra/FastFood.PayStream.Infra.csproj`
- Criar estrutura de pastas:
  - `Controllers/` (para controllers HTTP)
- Configurar Swagger no `Program.cs` (já vem configurado por padrão no template webapi)
- Verificar que o `Program.cs` está configurado corretamente

## Como testar
- Executar `dotnet build` no projeto Api (deve compilar sem erros)
- Executar `dotnet run` no projeto Api (deve iniciar sem erros)
- Acessar `https://localhost:5001/swagger` ou `http://localhost:5000/swagger` (deve abrir o Swagger UI)
- Verificar que o arquivo `.csproj` contém referências aos projetos necessários

## Critérios de aceite
- Projeto `FastFood.PayStream.Api` criado em `src/InterfacesExternas/FastFood.PayStream.Api/`
- Estrutura de pastas `Controllers/` criada
- Referências aos projetos Application, CrossCutting e Infra adicionadas
- Swagger configurado no `Program.cs`
- Projeto compila sem erros (`dotnet build`)
- API inicia sem erros (`dotnet run`)
- Swagger UI acessível

