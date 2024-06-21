using Authorization.Domain.Models;

namespace Authorization.Domain.Interfaces
{
    public interface IAuthorizationRepository
    {
        public Task<bool> UserExist(Models.Authorization user);
        public bool CreateUser(Models.Authorization user);
        public Task<IEnumerable<Models.Authorization>> GetUsers();
        public bool AuthorizationLog(string origin, string message);
    }
}
