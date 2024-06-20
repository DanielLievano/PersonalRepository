using Authorization.Aplication.Interfaces;
using Authorization.Aplication.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace AuthenticationAPI.Controllers
{
    [Controller]
    [Route("/api/[controller]")]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthorizationService _authorizationService;
        public AuthenticationController(IAuthorizationService authorizationService)
        {
            _authorizationService = authorizationService;
        }
        [HttpPost]
        public ActionResult<Response<object>> ValidateUser([FromBody]User user)
        {
            Response<object> response;
            try
            {
                if (!ModelState.IsValid)
                {
                    response = new Response<object>(ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList());
                    return BadRequest(response);
                }

                response = new Response<object>(null);

                if (_authorizationService.Authorization(user).Result)
                    return Ok(response);
                else
                    return Unauthorized(response);
            }
            catch (Exception ex)
            {
                response = new Response<object> (ex.Message);
                return UnprocessableEntity(response);
            }
        }
        [HttpPut]
        public ActionResult<Response<object>> PutUser([FromBody] User user)
        {
            object response; try
            {
                if (!ModelState.IsValid)
                {
                    response = new Response<object>(ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList());
                    return BadRequest(response);
                }
                if (_authorizationService.CreateUser(user))
                {
                    response = new Response<object>(user);
                    return Ok(response);
                }
                else
                {
                    response = new Response<string>("Usuario existente.");
                    return BadRequest(response);
                }
            }
            catch(Exception ex)
            {
                response = new Response<object>(ex.Message);
                return UnprocessableEntity(response);
            }
        }
        [HttpGet]
        public async Task<ActionResult<Response<object>>> GetUsers()
        {
            object response;
            try
            {
                List<User> users = (List<User>)await _authorizationService.GetUsers();
                if (users.Count > 0)
                {
                    response = new Response<object>(users);
                    return Ok(response);
                }
                else
                {
                    response = new Response<string>("No se encontraron registros.");
                    return UnprocessableEntity(response);
                }
            }
            catch (Exception ex)
            {
                response = new Response<object>(ex.Message);
                return UnprocessableEntity(response);
            }
        }
    }
}
