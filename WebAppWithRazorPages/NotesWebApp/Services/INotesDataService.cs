using NotesWebApp.Models;

namespace NotesWebApp.Services; 

public interface INotesDataService
{
    public List<Note> GetAllNotes();
    public Note? GetNoteById(int id);

    public bool CreateNote(Note note);

    public bool UpdateNoteByID(int id, Note note);

    public bool DeleteNoteByID(int id);
}
