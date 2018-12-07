using System;

namespace NoteAPI.BL.ExceptionHandling
{
    public class MemoryCacheKeyException : Exception
    {
        /// <summary>
        /// standard default constructor
        /// </summary>
        public MemoryCacheKeyException()
        {
        }

        /// <summary>
        /// Constructor with a exception message passed in
        /// </summary>
        public MemoryCacheKeyException(string message) : base(message)
        {
        }
    }
}

