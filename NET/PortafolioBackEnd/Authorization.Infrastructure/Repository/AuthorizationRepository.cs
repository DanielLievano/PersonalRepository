using Authorization.Domain.Interfaces;
using Authorization.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Authorization.Infrastructure.Repository
{
    public class AuthorizationRepository : IAuthorizationRepository
    {
        private readonly AuthorizationDbContext _authorizationDbContext;
        public AuthorizationRepository(AuthorizationDbContext authorizationDbContext)
        {
            _authorizationDbContext = authorizationDbContext;
        }

        public bool CreateUser(Domain.Models.Authorization user)
        {
            string userName = string.Empty;
            string userName_userObject = user.UserName ?? string.Empty;
            try
            {
                userName = _authorizationDbContext.Authorizations.Where(x => x.UserName.Equals(userName_userObject)).
                Select(x => x.UserName).FirstOrDefault() ?? string.Empty;
                if (string.Equals(userName,
                    userName_userObject,
                    StringComparison.Ordinal))
                {
                    return false;
                }
                _authorizationDbContext.Authorizations.Add(user);
                return _authorizationDbContext.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<IEnumerable<Domain.Models.Authorization>> GetUsers()
        {
            try
            {
                return await _authorizationDbContext.Authorizations.ToListAsync();
            }
            catch (Exception ex)
            {
                return Enumerable.Empty<Domain.Models.Authorization>();
            }
        }

        public Task<bool> UserExist(Domain.Models.Authorization user)
        {
            try
            {
                return _authorizationDbContext.Authorizations.AnyAsync(x => x.UserName.Equals(user.UserName) && x.Password.Equals(user.Password));
            }
            catch(Exception ex)
            {
                return Task.FromResult(false);
            }
        }
    }
}
