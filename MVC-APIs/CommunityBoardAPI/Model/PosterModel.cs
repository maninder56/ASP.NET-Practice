namespace CommunityBoardAPI.Model; 

public class PosterModel
{
    public int Id { get; set; }

    public string Title { get; set; }

    public string Description { get; set; } = string.Empty; 

    public DateOnly CreatedAt { get; set; }

    public PosterModel(int id, string title) 
    {
        Id = id;
        Title = title;
        CreatedAt = DateOnly.FromDateTime(DateTime.Now);
    }

    public PosterModel(int id, string title, string description)
    {
        Id = id;
        Title = title;
        Description = description;
        CreatedAt = DateOnly.FromDateTime(DateTime.Now);
    }


    public PosterModel(int id, string title, string description, DateOnly createdAt)
    {
        Id = id;
        Title = title;
        Description = description;
        CreatedAt = createdAt;
    }
}
