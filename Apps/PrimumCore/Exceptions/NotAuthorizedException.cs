namespace PrimumCore.Exceptions
{
    public class NotAuthorizedException: Exception
    {
        public NotAuthorizedException(string role, int id): base($"{role} ({id}) not authorized") { }
        public NotAuthorizedException(string role) : base($"{role} not authorized due to missing id or failed id parsing") { }
    }
}
