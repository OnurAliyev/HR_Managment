﻿namespace HR.Business.Utilities.Exceptions;

public class EmptyNameException:Exception
{
    public EmptyNameException(string message) : base(message) { }
}
