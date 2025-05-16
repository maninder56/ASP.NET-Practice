namespace NotesWebApp.Models; 

public class Note
{
    public int Id { get; set; }

    public string Title { get; set; } = string.Empty;

    public string Content { get; set; } = string.Empty;

    public override string ToString()
    {
        return $"ID: {Id}, Title: {Title}, Content: {Content}";
    }
}
