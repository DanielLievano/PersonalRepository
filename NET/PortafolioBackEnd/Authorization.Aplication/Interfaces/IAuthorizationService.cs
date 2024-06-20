

using Authorization.Aplication.Models;

namespace Authorization.Aplication.Interfaces
{
    public interface IAuthorizationService
    {
        public Task<bool> Authorization(User authorization);
        public bool CreateUser(User user);
        public Task<IEnumerable<User>> GetUsers();
    }
}
