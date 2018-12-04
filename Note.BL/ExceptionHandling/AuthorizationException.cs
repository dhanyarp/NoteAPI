using System;

namespace NoteAPI.BL.ExceptionHandling
{
    public class AuthorizationException : Exception
    {
        public AuthorizationException(string message) : base(message) { }

    }
}
