using CommunityBoardAPI.Data;
using CommunityBoardAPI.Model;
using CommunityBoardAPI.Model.DTOs;

namespace CommunityBoardAPI.Services; 

public class PosterService : IPosterService
{
    private List<PosterModel> list; 

    public PosterService(PosterData data)
    {
        list = data.PosterList;
    }

    public List<PosterRecord> GetAllPosterList()
    {
        return list.Select(l => new PosterRecord(l.Id, l.Title)).ToList(); 
    }

    public PosterDetailedRecord GetPosterInDetailByID(int id)
    {
        return list.Where(l => l.Id == id)
            .Select(l => new PosterDetailedRecord(l.Id, l.Title, l.Description, l.CreatedAt))
            .First();
    }

    public int? CreatePoster(NewPosterRecord newPoster)
    {
        int id = list.Count + 1;

        list.Add(new PosterModel(id, newPoster.Title, newPoster.Description)); 

        return id;
    }

    public bool DeletePosterByID(int id)
    {
        PosterModel poster = list.First(l => l.Id == id);  

        return list.Remove(poster); 
    }

    public bool PosterExists(int id)
    {
        return list.Any(l => l.Id == id);
    }
}
