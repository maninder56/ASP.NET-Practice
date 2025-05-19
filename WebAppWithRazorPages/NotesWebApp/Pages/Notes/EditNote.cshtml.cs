using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NotesWebApp.Models;
using NotesWebApp.Services;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlTypes;
using System.Diagnostics;

namespace NotesWebApp.Pages.Notes; 

public class EditNoteModel : PageModel
{
    private ILogger<EditNoteModel> logger;

    private INotesDataService service;

    public EditNoteModel(ILogger<EditNoteModel> logger, INotesDataService service)
    {
        this.logger = logger;
        this.service = service;
    }


    // Data shared with view
    public bool NoteIDIsNull { get; private set; }
    public bool NoteIDDoesNotExits {  get; private set; }
    public int NoteIDFromRoute { get; private set; }

    // post handler properites 
    public bool InValidInputModel { get; private set; }
    public bool InputModelIsNullForPostRequest { get; private set; }
    public bool NoteFailedToUpdate { get; private set; }

    // Data to and from view 
    [BindProperty]
    [FromForm]
    public InputModel? Input { get; set; }

    public IActionResult OnGet(int? id)
    {
        if (id is null)
        {
            NoteIDIsNull = true;
            logger.LogWarning("No Note ID Provided to Edit"); 
            return Page();
        }

        if (id is int noteID)
        {
            Note? note = service.GetNoteById(noteID);

            if (note is null)
            {
                NoteIDDoesNotExits = true;
                NoteIDFromRoute = noteID; 
                logger.LogWarning("Note With ID {NoteID} Requested to Edit, which does not exists", noteID);
                return Page();
            }

            Input = new InputModel()
            {
                ID = noteID,
                Title = note.Title, 
                Content = note.Content
            };  
        }

        logger.LogInformation("Note with ID {NoteID} Requested to Edit", id); 
        return Page();
    }



    public IActionResult OnPost()
    {
        if (!ModelState.IsValid)
        {
            InValidInputModel = true;
            logger.LogWarning("Form validation failed, One or more properties are invalid when requrested to edit a note"); 
            return Page();
        }

        if (Input is null)
        {
            InputModelIsNullForPostRequest = true;
            logger.LogError("Inupt model is null, after validation, Binding Failed"); 
            return Page();
        }

        Note editedNote = new Note()
        {
            Id = Input.ID, Title = Input.Title, Content = Input.Content ?? string.Empty
        };

        if (!service.UpdateNoteByID(Input.ID, editedNote))
        {
            NoteFailedToUpdate = true;
            logger.LogWarning("Failed to Update Note from Edit request"); 
            return Page();
        }

        return RedirectToPage("AllNotes"); 
    }



    public class InputModel
    {
        [Required]
        public required int ID { get; set; }

        [Required]
        public required string Title { get; set; }

        public string? Content { get; set; }
    }
}
