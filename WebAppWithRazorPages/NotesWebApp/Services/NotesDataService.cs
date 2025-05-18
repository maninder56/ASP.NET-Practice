using NotesWebApp.Models;

namespace NotesWebApp.Services;

public class NotesDataService : INotesDataService
{
    private ILogger<NotesDataService> logger; 

    private List<Note> noteList;

    public NotesDataService(ILogger<NotesDataService> logger, NotesData Notes)
    {
        this.logger = logger;
        noteList = Notes.Data;
    }



    public List<Note> GetAllNotes()
    {
        if(noteList.Count == 0)
        {
            logger.LogWarning("No Note available in Note Data"); 
            return new List<Note>(); 
        }

        logger.LogInformation("{NoteCount} Notes Loaded into service", noteList.Count);
        return noteList;
    }

    public Note? GetNoteById(int id)
    {
        Note? note = noteList.Find(x => x.Id == id);

        if(note == null)
        {
            logger.LogWarning("Note with {NoteID} does not exists", id); 
            return null;
        }

        logger.LogInformation("Get Note with ID {NoteID} from service", id); 
        return note;
    }


    public bool CreateNote(Note note)
    {
        note.Id = noteList.Count + 1; 

        noteList.Add(note);

        if (noteList.Find(n => n.Id == note.Id) == null)
        {
            logger.LogWarning("Failed to Create Note from service"); 
            return false;
        }

        logger.LogInformation("Note with ID {NoteID} created", note.Id); 
        return true;
    }



    public bool UpdateNoteByID(int id, Note note)
    {
        Note? oldNote = noteList.Find(n => n.Id == id);

        if (oldNote == null)
        {
            logger.LogWarning("Failed to update note with ID {NoteID} which does not exits", id); 
            return false;
        }

        oldNote.Title = note.Title;
        oldNote.Content = note.Content;

        logger.LogInformation("Note with ID {NoteID} has been updated", id); 
        return true; 
    }


    public bool DeleteNoteByID(int id)
    {
        Note? note = GetNoteById(id);

        if (note is null)
        {
            logger.LogWarning("Failed to delete note with ID {NoteID} which does not exists", id); 
            return false; 
        }

        noteList.Remove(note);
        logger.LogInformation("Successfully deleted note with ID {NoteID}", id);
        return true; 
    }
}
