using WorldRank.Models;

InMemoryPlayerRepository players = new();
InMemoryWalletRepository wallets = new();

while (true)
{
    Console.WriteLine("\n=== PLayer Managements System ===");
    Console.WriteLine("1. Add player");
    Console.WriteLine("2. List players");
    Console.WriteLine("3. Find by name");
    Console.WriteLine("4. Add Wallet to player");
    Console.WriteLine("5. Get Wallets by player");
    Console.WriteLine("6. Exit");
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
                players.AddPlayer(newPlayer);
                Console.WriteLine("Player added successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            break;

        case "2": 
            if (players.CountPlayers() == 0)
            {
                Console.WriteLine("No players found.");
            }
            else
            {
                Console.WriteLine("\n--- All Players ---");
                foreach (var p in players.GetAll())
                {
                    Console.WriteLine(p);
                }
            }
            break;

        case "3":
        {
            Console.Write("Enter the exact name to search for: ");
            string? searchName = Console.ReadLine();

            if (!string.IsNullOrEmpty(searchName))
            {
                Player? foundPlayer = players.FindPlayerByName(searchName);

                if (foundPlayer != null)
                {
                    Console.WriteLine($"\nFound Player: {foundPlayer}");
                }
                else
                {   
                    Console.WriteLine("\nPlayer not found.");
                }
            }
            else
            {
                Console.WriteLine("\nInvalid input. Please enter a name.");
            }
            break;
        }

        case "4":
        {
             Console.Write("Enter the Player ID to add a wallet for: ");
            if (int.TryParse(Console.ReadLine(), out int playerId))
            {
                Player? foundPlayer = players.FindPlayer(playerId);
        
                if (foundPlayer == null)
                {
                    Console.WriteLine("\nPlayer not found.");
                    break; 
                }

                Console.WriteLine("\nAvailable Currencies:");
                foreach (CurrencyTypes currency in Enum.GetValues(typeof(CurrencyTypes)))
                {
                    Console.WriteLine($"{(int)currency} - {currency}");
                }

                Console.Write("Choose a currency (enter number): ");
                if (int.TryParse(Console.ReadLine(), out int currencyChoice))
                {
                    if (Enum.IsDefined(typeof(CurrencyTypes), currencyChoice))
                    {
                        CurrencyTypes selectedCurrency = (CurrencyTypes)currencyChoice;

                        Wallet newWallet = new Wallet(selectedCurrency); 
                
                        try 
                        {
                            wallets.Add(newWallet, playerId);
                            Console.WriteLine($"\nSuccessfully added {selectedCurrency} wallet for {foundPlayer.Name}.");
                        }
                        catch (InvalidOperationException ex)
                        {
                            Console.WriteLine($"\nError: {ex.Message}");
                        }
                    }
                    else
                    {
                        Console.WriteLine("\nInvalid currency selection.");
                    }
                }
            }
            else
            {
                Console.WriteLine("\nInvalid ID input.");
            }
            break;
        }

        case "5":
        {
            Console.Write("Enter the Player ID to view wallets: ");
            string? input = Console.ReadLine();

            if (int.TryParse(input, out int playerId))
            {
        
                Player? foundPlayer = players.FindPlayer(playerId);
        
                if (foundPlayer == null)
                {
                    Console.WriteLine("\nPlayer does not exist.");
                    break;
                }

                var playerWallets = wallets.GetByPlayer(playerId);

                if (playerWallets.Count == 0)
                {
                    Console.WriteLine($"\nPlayer {foundPlayer.Name} (ID: {playerId}) has no wallets assigned.");
                }
                else
                {
                    Console.WriteLine($"\nPlayer {foundPlayer.Name} (ID: {playerId}) has the following wallets:");
            
                    foreach (var kvp in playerWallets)
                    {
                        Console.WriteLine($"- Currency: {kvp.Key}, Balance: {kvp.Value.Balance}");
                    }
                }
            }
            else
            {
                Console.WriteLine("\nInvalid input. Please enter a numeric ID.");
            }
            break;
        }

        case "6":
            return;

        default:
            Console.WriteLine("Invalid choice. Please enter one of the available options 1-4.");
            break;
    }
}