using System.Security.Claims;
using Business.Abstract;
using Microsoft.AspNetCore.Http;

namespace Business.Concrete
{
    public class CurrentUserService : ICurrentUserService
    {

        private readonly IHttpContextAccessor _httpContextAccessor;
        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public Guid UserId
        {
            get
            {
                var userIdClaim = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (Guid.TryParse(userIdClaim, out Guid userId))
                    return userId;
                throw new UnauthorizedAccessException("User is not authenticated.");
            }
        }
    }
}
