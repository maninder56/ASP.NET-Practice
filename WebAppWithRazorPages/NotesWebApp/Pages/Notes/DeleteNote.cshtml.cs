using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NotesWebApp.Services;
using System.ComponentModel.DataAnnotations;

namespace NotesWebApp.Pages.Notes
{
    public class DeleteNoteModel : PageModel
    {
        private ILogger<EditNoteModel> logger;

        private INotesDataService service;

        public DeleteNoteModel(ILogger<EditNoteModel> logger, INotesDataService service)
        {
            this.logger = logger;
            this.service = service;
        }

        // Data Shared with view 
        public bool NoteIDFromRouteIsNull { get; private set; }
        public bool NoteDoesNotExists { get; private set; }
        public int NoteIDFromRoute { get; private set; }

        public Models.Note? NoteToDelete { get; private set; }

        // Data from form 
        [BindProperty]
        [FromForm]
        public InputModel? Input { get; set; }

        // Post data 
        public bool InValidInput { get; private set; }
        public bool InputModelIsNullAfterPost { get; private set; }
        public bool FailedToDeleteNote { get; private set; }


        public IActionResult OnGet(int? id)
        {
            if (id is null)
            {
                NoteIDFromRouteIsNull = true;
                logger.LogWarning("No Note ID Provided to Delete");
                return Page();
            }

            NoteToDelete = service.GetNoteById((int)id);
            NoteIDFromRoute = (int)id;

            if (NoteToDelete is null)
            {
                NoteDoesNotExists = true;
                logger.LogWarning("Failed to load Note with ID {NoteID} because it does not exists", id); 
                return Page();
            }

            logger.LogInformation("Note with ID {NoteID} loaded to delete", id); 
            return Page();
        }


        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                InValidInput = true;
                logger.LogWarning("From Validation failed when requested to delete note");
                return Page();
            }

            if (Input is null)
            {
                InputModelIsNullAfterPost = true;
                logger.LogError("Inupt model is null, after validation, Binding Failed");
                return Page();
            }

            bool noteDeleted = service.DeleteNoteByID(Input.Id);

            if (noteDeleted)
            {
                logger.LogInformation("Successfully deleted a note with ID {NoteID}", Input.Id);
                return RedirectToPage("AllNotes"); 
            }

            FailedToDeleteNote = true; 
            logger.LogWarning("Failed to delete note with {NoteID}", Input.Id);
            return Page();
        }


        public class InputModel
        {
            [Required]
            public int Id { get; set; }
        }
    }
}
