using System;
using System.ComponentModel.DataAnnotations;

namespace NoteAPI.BL.Models
{
    public class UserNote
    {
        [Required]
        public Guid UserId { get; set; }

        public Guid NoteId { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Title is a mandatory field")]
        [MaxLength(255, ErrorMessage = "Maximum length of Title field is 255 characters.")]
        public string Title { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Content is a mandatory field")]
        [MaxLength(255, ErrorMessage = "Maximum length of Content field is 255 characters.")]
        public string Content { get; set; }
    }
    
}
