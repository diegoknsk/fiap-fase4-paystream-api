namespace FastFood.PayStream.Domain.Common.Exceptions;

/// <summary>
/// Classe utilitária para validações de regras de domínio.
/// </summary>
public static class DomainValidation
{
    /// <summary>
    /// Valida se o valor fornecido não é null, vazio ou contém apenas espaços em branco.
    /// Lança uma ArgumentException se a validação falhar.
    /// </summary>
    /// <param name="value">O valor a ser validado.</param>
    /// <param name="message">A mensagem de erro a ser lançada se a validação falhar.</param>
    /// <exception cref="ArgumentException">Lançada quando o valor é null, vazio ou contém apenas espaços em branco.</exception>
    public static void ThrowIfNullOrWhiteSpace(string? value, string message)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentException(message);
        }
    }
}
