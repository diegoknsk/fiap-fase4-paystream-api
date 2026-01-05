namespace FastFood.PayStream.Application.Models.Common;

/// <summary>
/// Classe genérica para padronizar as respostas da API.
/// Segue o padrão do projeto orderhub para garantir consistência nas respostas HTTP.
/// </summary>
/// <typeparam name="T">Tipo do conteúdo da resposta.</typeparam>
public class ApiResponse<T>
{
    /// <summary>
    /// Indica se a requisição foi bem-sucedida.
    /// </summary>
    public bool Success { get; set; }

    /// <summary>
    /// Mensagem descritiva sobre o resultado da operação.
    /// </summary>
    public string? Message { get; set; }

    /// <summary>
    /// Conteúdo da resposta (dados retornados).
    /// </summary>
    public object? Content { get; set; }

    /// <summary>
    /// Construtor da classe ApiResponse.
    /// </summary>
    /// <param name="content">Conteúdo da resposta.</param>
    /// <param name="message">Mensagem descritiva (padrão: "Requisição bem-sucedida.").</param>
    /// <param name="success">Indica se foi bem-sucedido (padrão: true).</param>
    public ApiResponse(object? content, string? message = "Requisição bem-sucedida.", bool success = true)
    {
        Content = content;
        Message = message;
        Success = success;
    }

    /// <summary>
    /// Cria uma resposta de sucesso com dados.
    /// </summary>
    /// <param name="data">Dados a serem retornados.</param>
    /// <param name="message">Mensagem descritiva (padrão: "Requisição bem-sucedida.").</param>
    /// <returns>Instância de ApiResponse com sucesso.</returns>
    public static ApiResponse<T> Ok(T? data, string? message = "Requisição bem-sucedida.")
    {
        if (data == null)
        {
            return new ApiResponse<T>(null, message, true);
        }
        return new ApiResponse<T>(data.ToNamedContent(), message, true);
    }

    /// <summary>
    /// Cria uma resposta de sucesso sem dados.
    /// </summary>
    /// <param name="message">Mensagem descritiva (padrão: "Requisição bem-sucedida.").</param>
    /// <returns>Instância de ApiResponse com sucesso.</returns>
    public static ApiResponse<T> Ok(string? message = "Requisição bem-sucedida.")
    {
        return new ApiResponse<T>(null, message, true);
    }

    /// <summary>
    /// Cria uma resposta de falha.
    /// </summary>
    /// <param name="message">Mensagem descritiva do erro.</param>
    /// <returns>Instância de ApiResponse com falha.</returns>
    public static ApiResponse<T> Fail(string? message)
    {
        return new ApiResponse<T>(null, message, false);
    }
}

/// <summary>
/// Classe de extensões para objetos, fornecendo métodos auxiliares.
/// </summary>
public static class ObjectExtensions
{
    /// <summary>
    /// Converte um objeto em um conteúdo nomeado para uso em ApiResponse.
    /// </summary>
    /// <typeparam name="T">Tipo do objeto.</typeparam>
    /// <param name="obj">Objeto a ser convertido.</param>
    /// <returns>O próprio objeto (sem transformação adicional).</returns>
    public static T ToNamedContent<T>(this T obj)
    {
        return obj;
    }
}
