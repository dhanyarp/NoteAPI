using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Reflection;
using NoteAPI.BL.ExceptionHandling;

using log4net;

namespace NoteAPI.Controllers
{
    public class BaseApiController : ApiController
    {

        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// Logger Property
        /// </summary>
        public static ILog Logger
        {
            get
            {
                return log;
            }
        }

        /// <summary>
        /// Method for handling exceptions
        /// </summary>
        /// <param name="e"></param>
        public HttpResponseMessage HandleException(Exception e)
        {
            log.Error(e);
            return Request.CreateResponse(HttpStatusCode.InternalServerError, e.Message);
        }


        /// <summary>
        /// CallService
        /// </summary>      
        /// <param name="serviceFunction"></param>
        /// <returns></returns>
        public HttpResponseMessage CallService(Func<HttpResponseMessage> serviceFunction)
        {
            try
            {
                return serviceFunction();
            }            
            catch (AuthorizationException ae) // Unauthorized - 401
            {
                log.Error(ae);
                return Request.CreateResponse(HttpStatusCode.Unauthorized, ae.Message);
            }            
            catch (Exception e) // Internal Server Error - Returning code 400 as WAF does not support 500
            {
                log.Error(e);
                return Request.CreateResponse(HttpStatusCode.BadRequest, "An unexpected application error has occured while processing your request. We apologize for any inconvenience. Please try again shortly.");
            }
        }
    }
}