using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NotesWebApp.Models;
using NotesWebApp.Services;

namespace NotesWebApp.Pages.Notes
{
    public class AllNotesModel : PageModel
    {
        private readonly ILogger<AllNotesModel> logger;

        private INotesDataService service; 

        public AllNotesModel(ILogger<AllNotesModel> logger, INotesDataService service)
        {
            this.logger = logger; 
            this.service = service;
        }
        

        // Data shared with view 
        public List<Note>? notes { get; private set; }

        public IActionResult OnGet()
        {
            notes = service.GetAllNotes();

            if (notes.Count == 0 )
            {
                logger.LogWarning("Unable to get notes from service"); 
                return Page();
            }

            logger.LogInformation("Loaded {NoteCount} Notes from service", notes.Count);
            return Page();
        }
    }
}
