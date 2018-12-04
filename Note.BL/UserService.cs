using NoteAPI.BL.Interfaces;
using NoteAPI.BL.Models;
using System;
using System.Collections.Concurrent;


namespace NoteAPI.BL
{
    public class UserService : IUserService
    {
        private static ConcurrentDictionary<Guid, User> userDictionary = new ConcurrentDictionary<Guid, User>();

        public Guid AddUser(User user)
        {            
            Guid userId = Guid.NewGuid();

            if (!userDictionary.TryAdd(userId, user))
            {
                throw new Exception("User cannot be added to the dictionary");
            }                        

            return userId;
        }

        public User GetUser(Guid userId)
        {
            if (!userDictionary.TryGetValue(userId, out User user))
            {
                return null;
            }
            return user;
        }

        public bool IsUserAuthorized(Guid userId)
        {
            User user = GetUser(userId);

            if(user == null)
            {
                return false;
            }

            return true;
        }

    }
}
