using NoteAPI.BL.Models;
using System;
using System.Collections.Generic;

namespace NoteAPI.BL.Interfaces
{
    public interface INoteService
    {
        Guid AddNote(UserNote note);

        List<UserNote> GetNotes(Guid UserId);

    }
}
