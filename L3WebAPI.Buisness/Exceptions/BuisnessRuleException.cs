namespace L3WebAPI.Buisness.Exceptions;

public class BuisnessRuleException : Exception
{
    public BuisnessRuleException() {}
    public BuisnessRuleException(string message) : base(message) {}
    public BuisnessRuleException(string message, Exception inner) : base(message, inner) {}
}