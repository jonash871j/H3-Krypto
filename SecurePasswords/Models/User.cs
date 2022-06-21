using System.Text.Json.Serialization;

namespace SecurePasswords.Models
{
    internal class User
    {
        public string Username { get; set; }
        [JsonIgnore]
        public string Password { get; set; }
        public byte[] Hash { get; set; }
        public byte[] Salt { get; set; }
        public int FailedAttempts { get; set; } = 0;
    }
}
