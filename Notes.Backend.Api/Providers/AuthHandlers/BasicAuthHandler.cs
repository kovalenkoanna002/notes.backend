using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using Microsoft.Net.Http.Headers;
using Newtonsoft.Json;
using Notes.Backend.Api.Providers.AuthHandlers.Constants;
using Notes.Backend.Api.Providers.AuthHandlers.Models;
using Notes.Backend.Api.Providers.AuthHandlers.Scheme;
using Notes.Backend.Application.Resources;
using Notes.Backend.Domain.Interfaces.Services;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.RegularExpressions;

namespace Notes.Backend.Api.Providers.AuthHandlers
{
    public class BasicAuthHandler
        : AuthenticationHandler<BasicAuthSchemeOptions>
    {
        private readonly IUserService _service;

        public BasicAuthHandler(
            IOptionsMonitor<BasicAuthSchemeOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            ISystemClock clock,
            IUserService service)
            : base(options, logger, encoder, clock)
        {
            _service = service;
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            TokenModel model;

            if (!Request.Headers.ContainsKey(HeaderNames.Authorization))
            {
                return AuthenticateResult.Fail(ErrorMessages.AuthenticationFailed);
            }

            var header = Request.Headers[HeaderNames.Authorization].ToString();
            var tokenMatch = Regex.Match(header, AuthSchemeConstants.Token);

            if (!tokenMatch.Success)
            {
                return AuthenticateResult.Fail(ErrorMessages.AuthenticationFailed);
            }

            var token = tokenMatch.Groups["token"].Value;

            try
            {
                byte[] fromBase64String = Convert.FromBase64String(token);
                var parsedToken = Encoding.UTF8.GetString(fromBase64String);

                model = JsonConvert.DeserializeObject<TokenModel>(parsedToken);
            }
            catch (Exception ex)
            {
                return AuthenticateResult.Fail(ErrorMessages.AuthenticationFailed);
            }

            if (model == null)
            {
                return AuthenticateResult.Fail(ErrorMessages.AuthenticationFailed);
            }

            var result = await _service.GetUserByPasswordAndUserName(model.Name, model.Password, new CancellationToken());

            if (!result.IsSuccess)
            {
                return AuthenticateResult.Fail(ErrorMessages.AuthenticationFailed);
            }

            var claims = new[] {
                    new Claim(ClaimTypes.NameIdentifier, result.Content.Id.ToString())};

            var claimsIdentity = new ClaimsIdentity(claims,
                        nameof(BasicAuthHandler));

            var ticket = new AuthenticationTicket(
                new ClaimsPrincipal(claimsIdentity), Scheme.Name);

            return AuthenticateResult.Success(ticket);
        }
    }
}
