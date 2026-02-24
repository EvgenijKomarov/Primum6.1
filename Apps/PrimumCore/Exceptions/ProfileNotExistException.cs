namespace PrimumCore.Exceptions
{
    public class ProfileNotExistException(string objectName, int userId) : Exception($"{objectName} profile not exist by user ({userId})");
}
