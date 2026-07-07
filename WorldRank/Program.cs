using WorldRank.Models;

List<Player> players = new List<Player>();

while (true)
{
    Console.WriteLine("\n=== PLayer Managements System ===");
    Console.WriteLine("1. Add player");
    Console.WriteLine("2. List players");
    Console.WriteLine("3. Find by name");
    Console.WriteLine("4. Exit");
    Console.Write("Choose an option (type and eneter corresponding number): ");

    string? choice = Console.ReadLine();

    switch (choice)
    {
        case "1":
            Console.Write("Enter player name: ");
            string? name = Console.ReadLine();
            try
            {
                Player newPlayer = new Player(name!);
                players.Add(newPlayer);
                Console.WriteLine("Player added successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            break;

        case "2": 
            if (players.Count == 0)
            {
                Console.WriteLine("No players found.");
            }
            else
            {
                Console.WriteLine("\n--- All Players ---");
                foreach (Player p in players)
                {
                    Console.WriteLine(p);
                }
            }
            break;

        case "3":
            Console.Write("Enter the exact name to search for: ");
            string? searchName = Console.ReadLine();

            Player? foundPlayer = players.FirstOrDefault(p => p.Name.Equals(searchName, StringComparison.OrdinalIgnoreCase));

            if (foundPlayer != null)
            {
                Console.WriteLine($"\nFound Player: {foundPlayer}");
            }
            else
            {
                Console.WriteLine("\nPlayer not found.");
            }
            break;

        case "4":
            return;

        default:
            Console.WriteLine("Invalid choice. Please enter one of the available options 1-4.");
            break;
    }
}