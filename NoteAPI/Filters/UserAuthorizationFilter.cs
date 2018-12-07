using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Filters;
using log4net;
using System.Reflection;
using NoteAPI.BL.ExceptionHandling;
using NoteAPI.BL.Interfaces;
using NoteAPI.BL;

namespace NoteAPI.Filters
{ 
    /// <summary>
    /// 
    /// </summary>
    public class UserAuthorizationFilter : Attribute, IAuthenticationFilter
    {
        private IUserService userService = new UserService();



        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);


        /// <summary>
        /// 
        /// </summary>
        public UserAuthorizationFilter()
        {

        }

      
        /// <summary>
        /// Intercept the Authenticate flow for validatin credentials for Basic Authentication
        /// </summary>
        /// <param name="context"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task AuthenticateAsync(HttpAuthenticationContext context, CancellationToken cancellationToken)
        {
            var req = context.Request;

            // Get credential from the Authorization header           
            if (req.Headers.Authorization != null)
            {
                log.Info("Start of Authenticate interception for user Authentication.");

                var rawCreds = req.Headers.Authorization.Parameter;
                bool isAccessAuthorizedFlag = false;

                try
                {
                    log.Info("Calling IsUserAuthorized.");
                    string userId = System.Web.HttpContext.Current.Request.Headers["Authorization"];
                    isAccessAuthorizedFlag = userService.IsUserAuthorized(new Guid(userId));
                    if(!isAccessAuthorizedFlag)
                    {
                        throw new AuthorizationException("User not authorized");
                    }
                    HttpContext.Current.User = new GenericPrincipal(new GenericIdentity(userId.ToString(), AuthenticationSchemes.Basic.ToString()), new[] { userId.ToString() });

                }
                catch (Exception ex)
                {
                    string errorMessage = string.Format("Exception in Authenticate method. Error Message : 0}", ex.Message);
                    log.Error(errorMessage, ex);
                }

            }


            return Task.FromResult(0);
        }

        /// <summary>
        /// Asynchronously challenges an authentication.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task ChallengeAsync(HttpAuthenticationChallengeContext context, CancellationToken cancellationToken)
        {
            context.Result = new ResultWithChallenge(context.Result);
            return Task.FromResult(0);
        }

        /// <summary>
        /// Challenge Result
        /// </summary>
        public class ResultWithChallenge : IHttpActionResult
        {
            private readonly IHttpActionResult next;

            /// <summary>
            /// 
            /// </summary>
            /// <param name="next"></param>
            public ResultWithChallenge(IHttpActionResult next)
            {
                this.next = next;
            }

            /// <summary>
            /// Creates a response message asynchronously.
            /// </summary>
            /// <param name="cancellationToken"></param>
            /// <returns></returns>
            public async Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
            {
                var response = await next.ExecuteAsync(cancellationToken);
                if (response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    response.Headers.WwwAuthenticate.Add(new AuthenticationHeaderValue(new Guid().ToString()));
                }
                return response;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public bool AllowMultiple
        {
            get { return false; }
        }

    }
}