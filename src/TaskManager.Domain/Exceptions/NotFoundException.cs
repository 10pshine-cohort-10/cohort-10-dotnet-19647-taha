namespace TaskManager.Domain.Exceptions;

public class NotFoundException : Exception
{
    public NotFoundException(string message) : base(message) { }

    public NotFoundException(string entityName, object key)
        : base(BuildMessage(entityName, key)) { }

    private static string BuildMessage(string entityName, object key)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(entityName);
        ArgumentNullException.ThrowIfNull(key);
        return $"{entityName} with id '{key}' was not found.";
    }
}