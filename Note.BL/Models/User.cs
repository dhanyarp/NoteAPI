using System.ComponentModel.DataAnnotations;

namespace NoteAPI.BL.Models
{
    public class User
    {       
        public string FirstName { get; set; }
       
        public string LastName { get; set; }

        public string Email { get; set; }

        private string password;
        public string Password
        {
            set { password = value;  }
        }
    }
}
