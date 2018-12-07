using NoteAPI.BL;
using NoteAPI.BL.Interfaces;
using NoteAPI.BL.Models;
using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace NoteAPI.Controllers
{
    [AllowAnonymous]
    public class UserController : BaseApiController
    {

        private IUserService userService = new UserService();

        [Route("add/user")]
        [HttpPost]
        public HttpResponseMessage AddUser(User user)
        {
            Func<HttpResponseMessage> serviceFunction = () =>
            {
                Guid result = userService.AddUser(user);
                return Request.CreateResponse(HttpStatusCode.OK, result);
            };

            return CallService(serviceFunction);
        }


        [Route("user/{userId}")]
        [HttpGet]
        public HttpResponseMessage GetUser(Guid userId)
        {
            Func<HttpResponseMessage> serviceFunction = () =>
            {
                User result = userService.GetUser(userId);
                return Request.CreateResponse(HttpStatusCode.OK, result);
            };

            return CallService(serviceFunction);
        }

    }
}
