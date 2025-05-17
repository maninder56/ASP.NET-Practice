using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NotesWebApp.Models;
using NotesWebApp.Services;
using System.ComponentModel.DataAnnotations;
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

    // Data to and from view 
    [BindProperty]
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
                Ttitle = note.Title, 
                Content = note.Content
            };  
        }

        logger.LogInformation("Note with ID {NoteID} Requested to Edit", id); 
        return Page();
    }

    public class InputModel
    {
        [Required]
        public required string Ttitle { get; set; }

        [Required]
        public required string Content { get; set; }
    }
}
