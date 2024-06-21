using Authorization.Domain.Interfaces;
using Authorization.Domain.Models;
using Authorization.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Reflection;

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
                string methodAndClass = LogCurrentMethodName();
                if (!AuthorizationLog(methodAndClass, ex.Message))
                    Debug.WriteLine(ex);
                throw new Exception(ex.Message);
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
                string methodAndClass = LogCurrentMethodName();
                if (!AuthorizationLog(methodAndClass, ex.Message))
                    Debug.WriteLine(ex);
                throw new Exception(ex.Message);
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
                string methodAndClass = LogCurrentMethodName();
                if (!AuthorizationLog(methodAndClass, ex.Message))
                    Debug.WriteLine(ex);
                throw new Exception(ex.Message);
            }
        }
        public bool AuthorizationLog(string origin,string message)
        {
            int result;
            LogEntry log = new LogEntry{
                Origin = origin ?? string.Empty,
                Message = message ?? string.Empty,
            };
            try
            {
                _authorizationDbContext.Add(log);
                result = _authorizationDbContext.SaveChanges();
                if(result > 0)
                    return true;
                else
                    return false;

            }catch(Exception ex)
            {
                Debug.WriteLine(ex);
                return false;
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
