using CommunityBoardAPI.Model;

namespace CommunityBoardAPI.Data; 

public class PosterData
{
    public List<PosterModel> PosterList { get; set; } = new List<PosterModel>()
    {
        new PosterModel(1, "Explore the Cosmos",
        "Join us for an evening of stargazing and learn about constellations, black holes, and the future of space travel.",
        new DateOnly(2025, 8, 1)),

        new PosterModel(2, "Art Expo 2025",
            "A vibrant collection of modern art from local and international artists. Open all week at the City Gallery.",
            new DateOnly(2025, 7, 15)),

        new PosterModel(3, "Tech Conference: FutureNow",
            "Discover the latest trends in AI, cybersecurity, and sustainable tech innovations. Register online.",
            new DateOnly(2025, 6, 10)),

        new PosterModel(4, "Summer Music Fest",
            "Live performances by indie bands, food trucks, and more. A weekend to remember!",
            new DateOnly(2025, 8, 18)),

        new PosterModel(5, "Wellness Retreat",
            "Unplug and recharge with yoga, meditation, and wellness workshops in the mountains.",
            new DateOnly(2025, 5, 22)),

        new PosterModel(6, "Coding Bootcamp Kickoff",
            "Start your journey into web development with our 12-week intensive course. Limited seats!",
            new DateOnly(2025, 4, 5)),

        new PosterModel(7, "Vintage Car Show",
            "Step back in time with rare vintage cars, music, and memorabilia. Family-friendly event.",
            new DateOnly(2025, 8, 20)),

        new PosterModel(8, "Film Night: Classic Sci-Fi",
            "Watch and discuss some of the most influential sci-fi films of the 20th century. Snacks provided.",
            new DateOnly(2025, 7, 30)),

         new PosterModel(9, "Photography Masterclass",
        "Learn the art of storytelling through your lens. Open to all skill levels. Hosted by award-winning photographers.",
        new DateOnly(2025, 8, 5)),

        new PosterModel(10, "Startup Pitch Night",
            "Watch local startups pitch their ideas to investors. Network with entrepreneurs and tech enthusiasts.",
            new DateOnly(2025, 6, 25)),

        new PosterModel(11, "Community Clean-Up Drive",
            "Join neighbors in making our parks and streets cleaner. Supplies and refreshments provided.",
            new DateOnly(2025, 8, 10)),

        new PosterModel(12, "Cultural Food Festival",
            "Taste dishes from around the world, enjoy live music, and celebrate cultural diversity.",
            new DateOnly(2025, 8, 19))
    };
}
