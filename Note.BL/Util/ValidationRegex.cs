
namespace NoteAPI.BL.Util
{
    public static class ValidationRegex
    {
        public const string NAME = @"^[a-zA-Z '\-\u00C0-\u00D6\u00D8-\u00F6\u00F8-\u00FF]+$";
        public const string EMAIL = @"^([a-zA-Z0-9_\-\.\+]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})$";        
    }
}
