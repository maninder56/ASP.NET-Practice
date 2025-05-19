using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NotesWebApp.Services;
using System.ComponentModel.DataAnnotations;

namespace NotesWebApp.Pages.Notes
{
    public class AddNoteModel : PageModel
    {
        private ILogger<AddNoteModel> logger;

        private INotesDataService service; 

        public AddNoteModel(ILogger<AddNoteModel> logger, INotesDataService service)
        {
            this.logger = logger;
            this.service = service;
        }

        // Data to view
        public bool InvalidFormData { get; private set; }   
        public bool InputIsNullAfterPost { get; private set; }
        public bool FailedToSaveNote { get; private set; }


        // Data from view 
        [BindProperty]
        [FromForm]
        public InputModel? Input { get; set; }
        

        public IActionResult OnGet()
        {
            logger.LogInformation("Requested to Add Note"); 
            return Page();
        }


        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                logger.LogWarning("Validation failed at adding a new note");
                InvalidFormData = true; 
                return Page();
            }

            if (Input is null)
            {
                logger.LogWarning("Binding failed when attempted to add note"); 
                InputIsNullAfterPost = true;
                return Page();
            }

            bool noteCreated = service.CreateNote(
                new Models.Note(0, Input.Title, Input.Content ?? string.Empty)); 

            if (noteCreated)
            {
                logger.LogInformation("Successfully added new note"); 
                return RedirectToPage("AllNotes"); 
            }

            logger.LogWarning("Failed to Save save note"); 
            FailedToSaveNote = true;    
            return Page();
        }



        public class InputModel
        {
            [Required]
            public required string Title { get; set; }

            public string? Content { get; set; }
        }
    }
}
