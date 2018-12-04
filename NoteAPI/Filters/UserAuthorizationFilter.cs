using System;
using System.Web.Http;
using System.Security.Principal;
using System.Web.Http.Controllers;
using System.Reflection;

using NoteAPI.BL;

using log4net;
using NoteAPI.BL.Interfaces;

namespace NoteAPI.Filters
{
    /// <summary>
    /// Custom Authorization filter
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class UserAuthorizationFilter : AuthorizeAttribute
    {

        private IUserService userService = new UserService();



        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public UserAuthorizationFilter()
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="actionContext"></param>
        /// <returns></returns>
        protected override bool IsAuthorized(HttpActionContext actionContext)
        {
            IPrincipal incomingPrincipal = actionContext.RequestContext.Principal;
            bool isAccessAuthorizedFlag = false;
            try
            {
                log.Debug($"Principal is authenticated at the start of IsAuthorized in MemberAuthorizationFilter: {incomingPrincipal.Identity.IsAuthenticated}");
                string userId = System.Web.HttpContext.Current.Request.Headers["Authorization"];
                isAccessAuthorizedFlag = userService.IsUserAuthorized(new Guid(userId));
            }
            catch (Exception e)
            {
                log.Error("MemberAuthorizationFilter exception", e);
                isAccessAuthorizedFlag = false;
            }
            return isAccessAuthorizedFlag;
        }


        /// <summary>
        /// Handles the unauthorized request situation
        /// </summary>
        /// <param name="actionContext"></param>
        protected override void HandleUnauthorizedRequest(HttpActionContext actionContext)
        {
            log.Debug("Running HandleUnauthorizedRequest in MemberAuthorizationFilter as principal is not authorized.");

            base.HandleUnauthorizedRequest(actionContext);
        }
    }
}