using Authorization.Aplication.Interfaces;
using Authorization.Aplication.Models;
using Authorization.Domain.Interfaces;
using System;
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
            Domain.Models.Authorization user = new Domain.Models.Authorization
            {
                UserName = authorization.UserName,
                Password = authorization.Password,
            };
            return _authorization.UserExist(user);
        }
        public bool CreateUser(User CreateUser)
        {
            Domain.Models.Authorization user = new Domain.Models.Authorization
            {
                UserName = CreateUser.UserName,
                Password = CreateUser.Password,
            };
            return _authorization.CreateUser(user);
        }
    }
}
