using Authorization.Aplication.Interfaces;
using Authorization.Aplication.Models;
using Authorization.Domain.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authorization.Aplication.Services
{
    public class AuthorizationService : IAuthorizationService
    {
        private readonly IAuthorizationRepository _authorization;
        public AuthorizationService(IAuthorizationRepository authorizationRepository)
        {
            _authorization = authorizationRepository;
        }
        public Task<bool> Authorization(User authorization)
        {
            try
            {
                Domain.Models.Authorization user = new Domain.Models.Authorization
                {
                    UserName = authorization.UserName,
                    Password = authorization.Password,
                };
                return _authorization.UserExist(user);
            }
            catch (Exception ex)
            {
                return Task.FromResult(false);
            }
        }
        public bool CreateUser(User CreateUser)
        {
            try
            {
                Domain.Models.Authorization user = new Domain.Models.Authorization
                {
                    UserName = CreateUser.UserName,
                    Password = CreateUser.Password,
                };
                return _authorization.CreateUser(user);
            }
            catch(Exception ex)
            {
                return false;
            }
        }
        public async Task<IEnumerable<User>> GetUsers()
        {
            try
            {
                List<User> users = new List<User>();
                foreach (var user in await _authorization.GetUsers())
                {
                    User tempUser = new User() { UserName = user.UserName, Password = user.Password };
                    users.Add(tempUser);
                }
                return users;
            }catch (Exception ex)
            {
                return Enumerable.Empty<User>();
            }
        } 
    }
}
