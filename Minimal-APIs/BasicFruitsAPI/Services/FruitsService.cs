using BasicFruitsAPI.Model;

namespace BasicFruitsAPI.Services; 

public class FruitsService
{
    List<Fruit> fruitList = new List<Fruit>()
        {
            new Fruit(1, "Mango", "Simple", "A mango is an edible stone fruit produced by the tropical tree Mangifera indica. It originated from the region between northwestern Myanmar, Bangladesh, and northeastern India."),
            new Fruit(2, "Apple", "Simple", "An apple is a round, edible fruit produced by an apple tree (Malus spp.). Fruit trees of the orchard or domestic apple (Malus domestica), the most widely grown in the genus, are cultivated worldwide."),
            new Fruit(3, "Grape", "Simple", "A grape is a fruit, botanically a berry, of the deciduous woody vines of the flowering plant genus Vitis. Grapes are a non-climacteric type of fruit, generally occurring in clusters."),
            new Fruit(4, "Squash", "Simple", "Although squash is a fruit according to its botanical classification, it is generally considered a vegetable in food preparation. A squash can grow up to fifteen feet tall."),

            new Fruit(5, "Raspberry", "Aggregate", "The raspberry is the edible fruit of several plant species in the genus Rubus of the rose family, most of which are in the subgenus Idaeobatus."),
            new Fruit(6, "Blackberry", "Aggregate", "The blackberry is an edible fruit produced by many species in the genus Rubus in the family Rosaceae, hybrids among these species within the subgenus Rubus, and hybrids between the subgenera Rubus and Idaeobatus."),
            new Fruit(7, "Strawberry", "Aggregate", "The garden strawberry  is a widely grown hybrid plant cultivated worldwide for its fruit. "),

            new Fruit(8, "Pineapple", "Multiple", "The pineapple(Ananas comosus) is a tropical plant with an edible fruit; it is the most economically significant plant in the family Bromeliaceae."),
            new Fruit(9, "Jackfruit", "Multiple", "The jackfruit or nangka (Artocarpus heterophyllus)is a species of tree in the fig, mulberry, and breadfruit family (Moraceae)."),
            new Fruit(10, "Breadfruit", "Multiple", "Breadfruit (Artocarpus altilis) is a species of flowering tree in the mulberry and jackfruit family (Moraceae) believed to be a domesticated descendant of Artocarpus camansi originating in New Guinea, the Maluku Islands, and the Philippines."),
        };


    // Service CRUD Operations

    // Read Operations 
    public List<Fruit> GetFruitList() => fruitList;

    public Fruit? GetFruitByID(int id)
    {
        if (id < 1)
        {
            return null;
        }

        return fruitList.FirstOrDefault(f => f.Id == id);  
    }

    public Fruit? GetFruitByName(string name)
    {
        if (string.IsNullOrEmpty(name))
        {
            return null;
        }

        return fruitList.Where(f => f.Name == name)
            .FirstOrDefault();
    }

    public List<Fruit>? GetFruitsByClassification(string classification)
    {
        if (string.IsNullOrEmpty(classification))
        {
            return null;
        }

        if (!fruitList.Exists(f => f.Classification == classification))
        {
            return null; 
        }

        List<Fruit> query =  fruitList.Where(f => f.Classification == classification)
            .ToList();

        return query; 
    }


    // Create Operations
    public Fruit? CreateFruit(Fruit fruit)
    {
        if (fruitList.Exists(f => f.Name == fruit.Name))
        {
            return null; 
        }

        fruit.Id = fruitList.Count + 1;
        fruitList.Add(fruit);

        return fruit;
    }


    // Update Operations 
    public Fruit? UpdateFruitByID(int id,  Fruit newFruit)
    {
        if (id < 1)
        {
            return null;
        }

        if (!fruitList.Exists(f => f.Id == id))
        {
            return null; 
        }

        Fruit oldFruit = fruitList.First(f => f.Id == id);

        int oldFruitIndex = fruitList.IndexOf(oldFruit);

        newFruit.Id = id; 

        fruitList[oldFruitIndex] = newFruit;
        return newFruit; 
    }

    
    // Delete Operations
    public bool DeleteFruitByID(int id)
    {
        if (id < 1)
        {
            return false;
        }

        if (!fruitList.Exists(f => f.Id == id))
        {
            return false; 
        }

        Fruit fruit = fruitList.First(f => f.Id == id);

        return fruitList.Remove(fruit);
    }
}
