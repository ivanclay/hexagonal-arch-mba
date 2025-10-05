using System;

namespace HexagonalArch.API.Application.Exceptions;

public class ValidationException : Exception
{
    public ValidationException(string message)
        : base(message)
    {
        // Em C#, não há suporte direto para desabilitar o rastreamento de pilha.
        // Uma alternativa seria sobrescrever StackTrace, mas isso não é recomendado.
    }

    public ValidationException(string message, Exception innerException)
        : base(message, innerException)
    {
        // Mesmo comentário sobre rastreamento de pilha.
    }

    // Opcional: se quiser evitar o rastreamento de pilha, pode sobrescrever StackTrace
    public override string StackTrace => string.Empty;
}

