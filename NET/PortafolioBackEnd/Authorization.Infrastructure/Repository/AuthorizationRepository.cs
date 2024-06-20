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
            if(_authorizationDbContext.Authorizations.Any(x=>x.UserName.Equals(user.UserName))) {
                return false;
            }
            _authorizationDbContext.Authorizations.Add(user);
            return _authorizationDbContext.SaveChanges()>0;
        }

        public Task<bool> UserExist(Domain.Models.Authorization user)
        {
            return _authorizationDbContext.Authorizations.AnyAsync(x=>x.UserName.Equals(user.UserName) && x.Password.Equals(user.Password));
        }
    }
}
