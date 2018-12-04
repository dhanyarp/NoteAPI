using NoteAPI.BL;
using NoteAPI.BL.Interfaces;
using NoteAPI.BL.Models;
using NoteAPI.Filters;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace NoteAPI.Controllers
{
    [UserAuthorizationFilter]
    public class NoteController : BaseApiController    {

        private INoteService noteService= new NoteService();

        [Route("note")]
        [HttpPost]
        public HttpResponseMessage AddNote(UserNote note)
        {
            Func<HttpResponseMessage> serviceFunction = () =>
            {
                Guid result = noteService.AddNote(note);
                return Request.CreateResponse(HttpStatusCode.OK, result);
            };

            return CallService(serviceFunction);
        }


        [Route("notes")]
        [HttpGet]
        public HttpResponseMessage GetNotes()
        {
            Func<HttpResponseMessage> serviceFunction = () =>
            {
                string userId = System.Web.HttpContext.Current.Request.Headers["Authorization"];
                List<UserNote> result = noteService.GetNotes(new Guid(userId));
                return Request.CreateResponse(HttpStatusCode.OK, result);
            };

            return CallService(serviceFunction);
        }
       
    }
}
