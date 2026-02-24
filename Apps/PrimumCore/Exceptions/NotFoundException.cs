namespace PrimumCore.Exceptions
{
    public class NotFoundException(string objectName) : Exception($"{objectName} not found");
}
