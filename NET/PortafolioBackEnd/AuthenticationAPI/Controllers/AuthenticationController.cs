using Authorization.Aplication.Interfaces;
using Authorization.Aplication.Models;
using Microsoft.AspNetCore.Mvc;

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
            object response;
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
        [HttpPut]
        public ActionResult<Response<object>> PutUser([FromBody] User user)
        {
            object response; 
            if (!ModelState.IsValid)
            {
                response = new Response<object>(ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList());
                return BadRequest(response);
            }
            if (_authorizationService.CreateUser(user)){
                response = new Response<object>(user);
                return Ok(response);
            }
            else
            {
                response = new Response<string>("Usuario existente.");
                return UnprocessableEntity(response);
            }
        }
    }
}
