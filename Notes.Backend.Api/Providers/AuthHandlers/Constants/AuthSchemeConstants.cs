namespace Notes.Backend.Api.Providers.AuthHandlers.Constants
{
    public class AuthSchemeConstants
    {
        public const string BasicAuthScheme = "Basic";
        public const string Token = $"{BasicAuthScheme} (?<token>.*)";
    }
}
