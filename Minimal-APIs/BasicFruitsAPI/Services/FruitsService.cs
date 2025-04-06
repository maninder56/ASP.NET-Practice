using BasicFruitsAPI.Model;
using System.Collections.Concurrent;
using System.Security.Cryptography.Xml;

namespace BasicFruitsAPI.Services; 

public class FruitsService
{
    private List<KeyValuePair<int, Fruit>> fruitListWithKeys = new List<KeyValuePair<int, Fruit>>()
    {
        new KeyValuePair<int, Fruit>(1, new Fruit(1, "Mango", "Simple", "A mango is an edible stone fruit produced by the tropical tree Mangifera indica. It originated from the region between northwestern Myanmar, Bangladesh, and northeastern India.")), 
        new KeyValuePair<int, Fruit>(2, new Fruit(2, "Apple", "Simple", "An apple is a round, edible fruit produced by an apple tree (Malus spp.). Fruit trees of the orchard or domestic apple (Malus domestica), the most widely grown in the genus, are cultivated worldwide.")), 
        new KeyValuePair<int, Fruit>(3, new Fruit(3, "Grape", "Simple", "A grape is a fruit, botanically a berry, of the deciduous woody vines of the flowering plant genus Vitis. Grapes are a non-climacteric type of fruit, generally occurring in clusters.")), 
        new KeyValuePair<int, Fruit>(4, new Fruit(4, "Squash", "Simple", "Although squash is a fruit according to its botanical classification, it is generally considered a vegetable in food preparation. A squash can grow up to fifteen feet tall.")), 

        new KeyValuePair<int, Fruit>(5, new Fruit(5, "Raspberry", "Aggregate", "The raspberry is the edible fruit of several plant species in the genus Rubus of the rose family, most of which are in the subgenus Idaeobatus.")),
        new KeyValuePair<int, Fruit>(6, new Fruit(6, "Blackberry", "Aggregate", "The blackberry is an edible fruit produced by many species in the genus Rubus in the family Rosaceae, hybrids among these species within the subgenus Rubus, and hybrids between the subgenera Rubus and Idaeobatus.")),
        new KeyValuePair<int, Fruit>(7, new Fruit(7, "Strawberry", "Aggregate", "The garden strawberry  is a widely grown hybrid plant cultivated worldwide for its fruit. ")),

        new KeyValuePair<int, Fruit>(8, new Fruit(8, "Pineapple", "Multiple", "The pineapple(Ananas comosus) is a tropical plant with an edible fruit; it is the most economically significant plant in the family Bromeliaceae.")),
        new KeyValuePair<int, Fruit>(9, new Fruit(9, "Jackfruit", "Multiple", "The jackfruit or nangka (Artocarpus heterophyllus)is a species of tree in the fig, mulberry, and breadfruit family (Moraceae).")),
        new KeyValuePair<int, Fruit>(10, new Fruit(10, "Breadfruit", "Multiple", "Breadfruit (Artocarpus altilis) is a species of flowering tree in the mulberry and jackfruit family (Moraceae) believed to be a domesticated descendant of Artocarpus camansi originating in New Guinea, the Maluku Islands, and the Philippines.")),
    }; 

    private ConcurrentDictionary<int, Fruit> fruitDictionary; 

    public FruitsService()
    {
        fruitDictionary = new ConcurrentDictionary<int, Fruit>(fruitListWithKeys);        
    }

    public bool isIDValid(int id) => fruitDictionary.ContainsKey(id);

    public bool isFruitNameDubplicate(string name) => 
        fruitDictionary.Values.Any(f => f.Name == name);    

    // Service CRUD Operations

    // Read Operations 
    public List<Fruit> GetFruitList() => fruitDictionary.Values.ToList();

    public Fruit? GetFruitByID(int id)
    {
        if (!fruitDictionary.TryGetValue(id, out Fruit? fruit))
        {
            return null;
        }
        
        return fruit;
    }

    public Fruit? GetFruitByName(string name)
    {
        if (string.IsNullOrEmpty(name))
        {
            return null;
        }

        return fruitDictionary.Where(pair => pair.Value.Name == name)
            .Select(pair => pair.Value)
            .FirstOrDefault(); 
    }

    public List<Fruit>? GetFruitsByClassification(string classification)
    {
        if (string.IsNullOrEmpty(classification))
        {
            return null;
        }

        return fruitDictionary
            .Where(pair => pair.Value.Classification == classification)
            .Select(pair => pair.Value)
            .ToList();
    }


    // Create Operations
    public Fruit? CreateFruit(Fruit fruit)
    {
        if (fruitDictionary.Values.Where(f => f.Name == fruit.Name).Any())
        {
            return null; 
        }

        fruit.Id = fruitDictionary.Keys.Max() + 1;

        if (!fruitDictionary.TryAdd(fruit.Id, fruit))
        {
            return null;
        }

        return fruit;
    }


    // Update Operations 
    public Fruit? UpdateFruitByID(int id,  Fruit newFruit)
    {
        if (!isIDValid(id))
        {
            return null; 
        }

        Fruit oldFruit = fruitDictionary.Values.First(f => f.Id == id);

        newFruit.Id = oldFruit.Id; 

        if (!fruitDictionary.TryUpdate(newFruit.Id, newFruit, oldFruit))
        {
            return null; 
        }

        return newFruit; 
    }

    
    // Delete Operations
    public bool DeleteFruitByID(int id)
    {
        return fruitDictionary.TryRemove(id, out _); 
    }
}
