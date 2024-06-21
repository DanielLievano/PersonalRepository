using Authorization.Aplication.Interfaces;
using Authorization.Aplication.Models;
using Authorization.Domain.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
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
                string methodAndClass = LogCurrentMethodName();
                if (!AuthorizationLog(methodAndClass, ex.Message))
                    Debug.WriteLine(ex);
                throw new Exception(ex.Message);
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
                string methodAndClass = LogCurrentMethodName();
                if (!AuthorizationLog(methodAndClass, ex.Message))
                    Debug.WriteLine(ex);
                throw new Exception(ex.Message);
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
                string methodAndClass = LogCurrentMethodName();
                if (!AuthorizationLog(methodAndClass,ex.Message))
                    Debug.WriteLine(ex);
                throw new Exception(ex.Message);
            }
        }
        public bool AuthorizationLog(string origin, string message)
        {
            try
            {
                return _authorization.AuthorizationLog(origin, message);
            }catch (Exception ex) {
                Debug.WriteLine(ex);
                throw new Exception(ex.Message);
            }
        }

        private string LogCurrentMethodName()
        {
            var stackTrace = new StackTrace();
            var stackFrame = stackTrace.GetFrame(1);
            var methodBase = stackFrame.GetMethod();
            var methodName = methodBase.Name;
            return $"method: {methodName}/class: {methodBase.DeclaringType.Name}";
        }
    }
}
