using BasicFruitsAPI.Model;

namespace BasicFruitsAPI.Services; 

public interface IFruitService
{
    // Helper Methods
    public bool DoesFruitIDExists(int id);
    public bool isFruitNameDubplicate(string name); 

    // CRUD Operations
    public List<Fruit> GetFruitList();
    public Fruit? GetFruitByID(int id);
    public Fruit? GetFruitByName(string name);
    public List<Fruit>? GetFruitsByClassification(string classification);
    public List<string> GetAvailableClassifications();

    public Fruit? CreateFruit(Fruit fruit);

    public Fruit? UpdateFruitByID(int id, Fruit newFruit);

    public bool DeleteFruitByID(int id); 
}
