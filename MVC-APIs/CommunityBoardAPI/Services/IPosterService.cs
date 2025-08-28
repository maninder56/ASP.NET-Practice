using CommunityBoardAPI.Model.DTOs;

namespace CommunityBoardAPI.Services; 

public interface IPosterService
{
    public bool PosterExists(int id); 

    public List<PosterRecord> GetAllPosterList(); 

    public PosterDetailedRecord GetPosterInDetailByID(int id);

    public int? CreatePoster(NewPosterRecord newPoster);

    public bool DeletePosterByID(int id);
}
