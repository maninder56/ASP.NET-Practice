using NotesWebApp.Models;

namespace NotesWebApp.Services; 

public class NotesData
{
    public List<Note> Data { get; set; }

    public NotesData(ILogger<NotesData> logger)
    {
        Data = new List<Note>()
        {
            new Note
            {
                Id = 1,
                Title = "Meeting Notes",
                Content = "In today’s strategy meeting, we discussed the Q2 goals, including expanding our market reach by 15%, launching the new mobile app feature by mid-June, and improving customer satisfaction scores. Action items were assigned to team leads, and follow-up is scheduled for next Monday."
            },
            new Note
            {
                Id = 2,
                Title = "Shopping List",
                Content = "Here's a detailed list for grocery shopping this weekend: 2 liters of whole milk, 1 dozen free-range eggs, a loaf of sourdough bread, a bag of organic apples, ground coffee (medium roast), pasta, marinara sauce, fresh basil, mozzarella cheese, and a bottle of olive oil."
            },
            new Note
            {
                Id = 3,
                Title = "Project Ideas",
                Content = "I’ve been brainstorming some new side project ideas: 1) A minimalist task manager with natural language input and dark mode. 2) A book tracker app with personal notes and recommendations. 3) A budget planner with monthly and annual views, exportable to PDF or CSV."
            },
            //new Note
            //{
            //    Id = 4,
            //    Title = "Book Summary",
            //    Content = "Atomic Habits by James Clear emphasizes the power of small, incremental changes in building lasting habits. Key principles include focusing on systems over goals, using identity-based habits, and designing your environment to support good behaviors while making bad habits harder to perform."
            //},
            //new Note
            //{
            //    Id = 5,
            //    Title = "Workout Plan",
            //    Content = "Weekly Workout Plan:\n- Monday: Chest & Triceps (bench press, dips, push-ups)\n- Tuesday: Back & Biceps (pull-ups, deadlifts, rows)\n- Wednesday: Legs (squats, lunges, calf raises)\n- Thursday: Rest\n- Friday: Shoulders & Abs\n- Saturday: Cardio\n- Sunday: Full body stretch and mobility"
            //},
            //new Note
            //{
            //    Id = 6,
            //    Title = "Recipe - Pancakes",
            //    Content = "To make fluffy pancakes: mix 1½ cups all-purpose flour, 3½ tsp baking powder, 1 tsp salt, 1 tbsp sugar. In another bowl, combine 1¼ cups milk, 1 egg, 3 tbsp melted butter. Mix wet into dry ingredients. Heat a lightly oiled griddle and cook each side until golden brown. Serve with syrup or fruit."
            //},
            //new Note
            //{
            //    Id = 7,
            //    Title = "Vacation Planning",
            //    Content = "Planning a trip to Japan: Research flights to Tokyo (preferably direct), book hotel near Shinjuku or Shibuya, create a list of must-see places: Mount Fuji, Kyoto temples, Hiroshima Peace Park, Nara deer park. Also, look into JR Rail Pass, local SIM cards, and cultural etiquette tips."
            //},
            //new Note
            //{
            //    Id = 8,
            //    Title = "Daily Journal",
            //    Content = "Today was productive. Started with a 30-minute workout and then focused on a C# project. Implemented a new feature in the API and fixed a few UI bugs. Read an insightful article on time management and journaled some thoughts about balancing work and personal growth."
            //},
            //new Note
            //{
            //    Id = 9,
            //    Title = "To-Do List",
            //    Content = "Today's agenda:\n- Respond to all pending emails by noon.\n- Finalize Q1 financial report for review.\n- Schedule a meeting with the design team to review prototypes.\n- Pick up dry cleaning.\n- Research gift ideas for mom's birthday next week.\n- Water the plants."
            //},
            //new Note
            //{
            //    Id = 10,
            //    Title = "Quotes",
            //    Content = "Here are some inspiring quotes:\n1) “Success is not final, failure is not fatal: It is the courage to continue that counts.” – Winston Churchill\n2) “Don’t watch the clock; do what it does. Keep going.” – Sam Levenson\n3) “You miss 100% of the shots you don’t take.” – Wayne Gretzky"
            //}
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
