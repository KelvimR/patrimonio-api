using System;

namespace Patrimonio.Exceptions;

public class BusinessException : Exception
{
    public BusinessException(string message) : base(message) { }
}
