namespace PrimumCore.Exceptions
{
    public class RequestingUserNotFoundException(int userId) : Exception($"Requesting user ({userId}) not found");
}
