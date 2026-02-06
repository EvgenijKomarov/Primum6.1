using System.Security.AccessControl;

namespace PrimumCore.Exceptions
{
    public class BusinessLogicException(string message) : Exception(message);
}
