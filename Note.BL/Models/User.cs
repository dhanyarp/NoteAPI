using System.ComponentModel.DataAnnotations;

namespace NoteAPI.BL.Models
{
    public class User
    {
        [RegularExpression("^[0-9]*$", ErrorMessage = "OrderId must be numeric")]
        public string FirstName { get; set; }

        [RegularExpression("^[0-9]*$", ErrorMessage = "OrderId must be numeric")]
        public string LastName { get; set; }

        public string Email { get; set; }

        private string password;
        public string Password
        {
            set { password = value;  }
        }
    }
}
