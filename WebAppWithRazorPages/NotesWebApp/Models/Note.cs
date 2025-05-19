namespace NotesWebApp.Models; 

public class Note
{
    public int Id { get; set; }

    public string Title { get; set; } = string.Empty;

    public string Content { get; set; } = string.Empty;

    public Note () { }

    public Note (int id, string title, string content)
    {
        Id = id;
        Title = title;
        Content = content;
    }

    public override string ToString()
    {
        return $"ID: {Id}, Title: {Title}, Content: {Content}";
    }
}
