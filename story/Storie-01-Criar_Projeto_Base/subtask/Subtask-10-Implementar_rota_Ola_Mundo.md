# Subtask 10: Implementar rota Olá Mundo na API

## Descrição
Implementar uma rota GET `/api/hello` na API que retorna "Olá Mundo" para validar que a estrutura do projeto está funcionando corretamente e que podemos fazer requisições HTTP.

## Passos de implementação
- Criar controller `HelloController.cs` na pasta `Controllers/` do projeto Api
- Implementar método GET que retorna "Olá Mundo"
- Usar atributo `[ApiController]` e `[Route("api/[controller]")]`
- Método deve retornar uma string ou um objeto JSON simples
- Exemplo de retorno: `{ "message": "Olá Mundo" }` ou simplesmente a string "Olá Mundo"

## Como testar
- Executar `dotnet run` no projeto Api
- Acessar `https://localhost:5001/swagger` ou `http://localhost:5000/swagger`
- Verificar que a rota `/api/hello` aparece no Swagger
- Fazer requisição GET para `/api/hello` via Swagger ou Postman
- Verificar que retorna "Olá Mundo" com status 200
- Teste manual: Abrir navegador em `https://localhost:5001/api/hello` (deve retornar "Olá Mundo")

## Critérios de aceite
- Controller `HelloController.cs` criado em `src/InterfacesExternas/FastFood.PayStream.Api/Controllers/`
- Rota GET `/api/hello` implementada
- Rota retorna "Olá Mundo" (pode ser string ou JSON)
- Rota aparece no Swagger UI
- Requisição GET para `/api/hello` retorna status 200
- Teste manual via navegador ou Swagger funciona corretamente
- API compila e executa sem erros



