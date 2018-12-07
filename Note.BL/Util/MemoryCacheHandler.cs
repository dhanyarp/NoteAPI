using System;
using System.Configuration;
using System.Reflection;
using System.Runtime.Caching;
using log4net;
using NoteAPI.BL.ExceptionHandling;

namespace NoteAPI.BL.Util
{
    /// <summary>
    /// Encapsulates the MemoryCache write and read operations
    /// </summary>
    public class MemoryCacheHandler
    {
        static ILog log;
        static MemoryCache noteAPICache;
        static int expirationTime;
        static readonly object cacheLock;

        static MemoryCacheHandler()
        {
            noteAPICache = MemoryCache.Default;
            expirationTime = Int32.Parse(ConfigurationManager.AppSettings["MemoryCacheExpirationTimeInSeconds"]);
            log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
            cacheLock = new object();
        }


        /// <summary>
        /// Adds a generic object to the Memory Cache
        /// </summary>
        public void AddToCacheWithKey(object objectToCache, Guid keyGuid)
        {
            try
            {
                DateTimeOffset dateTimeOffset = new DateTimeOffset(DateTime.Now.AddSeconds(expirationTime));

                lock (cacheLock)
                {
                    noteAPICache.Set(keyGuid.ToString(), objectToCache, dateTimeOffset);
                }               
            }
            catch (Exception e)
            {
                string errorMessage = $"Exception writing to the MemoryCache: {e.Message} {e.InnerException}";
                log.Error(errorMessage);
                throw new MemoryCacheException(errorMessage);
            }
        }

        /// <summary>
        /// Reads a generic object from the Memory Cache
        /// </summary>
        /// <param name="memoryKeyGuid">the Guid corresponding to the server key</param>
        /// <returns>the generic object stored in the Memory server</returns>
        public T GetFromCache<T>(Guid memoryKeyGuid)
        {
            object cachedObject;

            lock (cacheLock)
            {
                if (!noteAPICache.Contains(memoryKeyGuid.ToString()))
                {
                    string errorMessage = $"Key not found in the Memory Cache: {memoryKeyGuid.ToString()}";
                    log.Error(errorMessage);
                    throw new MemoryCacheKeyException(errorMessage);
                }

                cachedObject = noteAPICache.Get(memoryKeyGuid.ToString());
            }

            if (!(cachedObject is T cachedValue))
            {
                string errorMessage = $"Cached object is not of the expected type: {memoryKeyGuid.ToString()}";
                log.Error(errorMessage);
                throw new MemoryCacheException(errorMessage);
            }

            return cachedValue;
        }

       
    }
}