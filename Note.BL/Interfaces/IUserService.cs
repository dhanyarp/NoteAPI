using NoteAPI.BL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteAPI.BL.Interfaces
{
    public interface IUserService
    {
        Guid AddUser(User user);

        User GetUser(Guid userId);

        bool IsUserAuthorized(Guid userId);
    }
}
