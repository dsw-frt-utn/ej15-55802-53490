using System;

namespace Dsw2026Ej15.Dsw2026Ej15.Domain;

public class ValidationException : Exception
{
    public ValidationException(string message)
        : base(message)
    {
    }
}