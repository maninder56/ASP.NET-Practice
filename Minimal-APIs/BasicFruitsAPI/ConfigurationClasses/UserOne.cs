using Microsoft.Extensions.Configuration.UserSecrets;

namespace BasicFruitsAPI.ConfigurationClasses;

public class UserOne
{
    public string? UserID { get; set; }

    public string? Password { get; set; }
}
