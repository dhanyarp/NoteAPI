using System;

namespace NoteAPI.BL.ExceptionHandling
{
    public class MemoryCacheException : Exception
    {
        /// <summary>
        /// standard default constructor
        /// </summary>
        public MemoryCacheException()
        {
        }

        /// <summary>
        /// Constructor with a exception message passed in
        /// </summary>
        public MemoryCacheException(string message) : base(message)
        {
        }
    }
}

