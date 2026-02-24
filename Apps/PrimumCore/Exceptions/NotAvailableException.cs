namespace PrimumCore.Exceptions
{
    public class NotAvailableException(string objectName) : Exception($"{objectName} is not available");
}
