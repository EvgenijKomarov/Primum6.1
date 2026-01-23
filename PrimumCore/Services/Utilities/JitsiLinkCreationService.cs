using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace PrimumCore.Services.Utilities
{
    public class JitsiLinkCreationService(ILogger<JitsiLinkCreationService>? _logger = null)
    {
        private const string JitsiBaseUrl = "https://meet.jit.si/";

        public virtual (string adminLink, string guestLink) CreateJitsiMeeting(string input)
        {
            try
            {
                _logger?.LogInformation($"Creating links for ({input})");
                using SHA256 sha256 = SHA256.Create();
                byte[] array = sha256.ComputeHash(Encoding.UTF8.GetBytes(input));
                StringBuilder builder = new StringBuilder();
                foreach (byte b in array)
                {
                    builder.Append(b.ToString("x2"));
                }
                string adminUrl = "https://meet.jit.si/" + builder.ToString() + "?userType=admin";
                string guestUrl = "https://meet.jit.si/" + builder.ToString() + "?userType=guest";
                _logger?.LogInformation($"Links creation successful. AdminLink: {adminUrl}, GuestLink: {guestUrl}");
                return (adminLink: adminUrl, guestLink: guestUrl);
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "Failed to create link");
                throw;
            }
        }
    }
}
