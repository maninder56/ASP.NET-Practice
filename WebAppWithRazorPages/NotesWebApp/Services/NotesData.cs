using NotesWebApp.Models;

namespace NotesWebApp.Services; 

public class NotesData
{
    public List<Note> Data { get; set; }

    public NotesData(ILogger<NotesData> logger)
    {
        Data = new List<Note>()
        {
            new Note { Id = 1, Title = "Meeting Notes", Content = "Discuss Q2 goals and product roadmap." },
            new Note { Id = 2, Title = "Shopping List", Content = "Milk, Bread, Eggs, Coffee, Apples." },
            new Note { Id = 3, Title = "Project Ideas", Content = "Build a task manager app with reminders." },
            new Note { Id = 4, Title = "Book Summary", Content = "Key takeaways from 'Atomic Habits' by James Clear." },
            new Note { Id = 5, Title = "Workout Plan", Content = "Mon: Chest, Tue: Back, Wed: Legs, Thu: Rest." },
            new Note { Id = 6, Title = "Recipe - Pancakes", Content = "Flour, eggs, milk, sugar, baking powder, mix & fry." },
            new Note { Id = 7, Title = "Vacation Planning", Content = "Look into flights, hotels, and activities in Japan." },
            new Note { Id = 8, Title = "Daily Journal", Content = "Today was productive, focused on coding and reading." },
            new Note { Id = 9, Title = "To-Do List", Content = "Reply to emails, finish report, schedule team meeting." },
            new Note { Id = 10, Title = "Quotes", Content = "“Success is not final, failure is not fatal...” - Churchill" }
        };

        if (Data.Count == 0 )
        {
            logger.LogWarning("Failed to laod temporary notes in memory"); 
        }
        else
        {
            logger.LogInformation("{NoteCount} Temporary Notes loaded in memroy", Data.Count); 
        }
    }
}
