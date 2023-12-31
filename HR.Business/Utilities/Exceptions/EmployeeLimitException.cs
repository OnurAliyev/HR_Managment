namespace HR.Business.Utilities.Exceptions;

public class EmployeeLimitException:Exception
{
    public EmployeeLimitException(string message) : base(message) { }
}
