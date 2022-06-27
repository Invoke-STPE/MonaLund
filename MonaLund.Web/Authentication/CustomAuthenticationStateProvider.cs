using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using System.Security.Claims;
using MonaLund.Web.Models;

namespace MonaLund.Web.Authentication
{
    public class CustomAuthenticationStateProvider : AuthenticationStateProvider
    {
        private readonly ProtectedSessionStorage _protectedSessionStorage;

        private ClaimsPrincipal _annoymous = new ClaimsPrincipal(new ClaimsIdentity());

        public CustomAuthenticationStateProvider(ProtectedSessionStorage protectedSessionStorage)
        {
            _protectedSessionStorage = protectedSessionStorage;
        }
        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            try
            {
                var sessionStorageResult = await _protectedSessionStorage.GetAsync<AuthUser>("lailaSession");
                var session = sessionStorageResult.Success ? sessionStorageResult.Value : null;
                if (session == null)
                {
                    return await Task.FromResult(new AuthenticationState((_annoymous)));
                }

                var claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, session.Name),
                }, "CustomAuth"));
                return await Task.FromResult(new AuthenticationState(claimsPrincipal));
            }
            catch
            {
                return await Task.FromResult(new AuthenticationState(_annoymous));
            }
        }

        public async Task UpdateAuthenticationState(AuthUser user)
        {
            ClaimsPrincipal claimsPrincipal;

            if (user != null)
            {
                await _protectedSessionStorage.SetAsync("lailaSession", user);
                claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Name)
                }));
            }
            else
            {
                await _protectedSessionStorage.DeleteAsync("lailaSession");
                claimsPrincipal = _annoymous;
            }
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }
    }
}
