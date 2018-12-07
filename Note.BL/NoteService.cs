using NoteAPI.BL.ExceptionHandling;
using NoteAPI.BL.Interfaces;
using NoteAPI.BL.Models;
using NoteAPI.BL.Util;
using System;
using System.Collections.Generic;

namespace NoteAPI.BL
{
    public class NoteService: INoteService
    {
        public Guid AddNote(UserNote note)
        {
            MemoryCacheHandler handler = new MemoryCacheHandler();
            Guid noteId = Guid.NewGuid();
            bool noKey = false;
            note.NoteId = noteId;
            List<UserNote> notes = null;

            try
            {
               notes = handler.GetFromCache<List<UserNote>>(note.UserId);
            }
            catch(MemoryCacheKeyException ex)
            {
                noKey = true;
            }

            if(notes == null)
            {
                notes = new List<UserNote>();
            }
            notes.Add(note);

            handler.AddToCacheWithKey(notes, note.UserId);          

            return noteId;
        }

        public List<UserNote> GetNotes(Guid userId)
        {
            MemoryCacheHandler handler = new MemoryCacheHandler();
            List<UserNote> notes = handler.GetFromCache<List<UserNote>>(userId);
            return notes;           
        }

    }
}
