using Forum.Common;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Forum.Services.CurrentUserContext
{
    public class CurrentUserContext : ICurrentUserContext
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ClaimsPrincipal _claimsPrincipal;

        public CurrentUserContext(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            _claimsPrincipal = _httpContextAccessor.HttpContext.User;
        }

        public long? Id
        {
            get
            {
                string value = _claimsPrincipal.Claims.FirstOrDefault(claim => claim.Type == AuthenticationClaimTypes.ID)?.Value;
                if (string.IsNullOrEmpty(value))
                {
                    return null;
                }

                if (!long.TryParse(value, out long id))
                {
                    return null;
                }

                return id;
            }
        }

        public string? Login
        {
            get
            {
                string value = _claimsPrincipal.Claims.FirstOrDefault(claim => claim.Type == AuthenticationClaimTypes.LOGIN)?.Value;
                if (string.IsNullOrEmpty(value))
                {
                    return null;
                }
                return value;
            }
        }

        public string? Email
        {
            get
            {
                string value = _claimsPrincipal.Claims.FirstOrDefault(claim => claim.Type == AuthenticationClaimTypes.EMAIL)?.Value;
                if (string.IsNullOrEmpty(value))
                {
                    return null;
                }
                return value;
            }
        }
    }
}