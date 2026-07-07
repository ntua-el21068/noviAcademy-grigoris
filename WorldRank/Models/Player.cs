namespace WorldRank.Models;

public class Player: IPlayer
{
    private static int _nextAvailableId = 0;
    public int Id {get;}
    public string Name {get; private set;}
    public int Score {get; private set;}
    



    // Constructor
    public Player(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        throw new ArgumentException("Name cannot be empty", nameof(name));
        Id = _nextAvailableId;
        Name = name;
        Score = 0;
        _nextAvailableId++;
    }
    
    //Method 
    public void AddScore(int points)
    {
        if (points < 0) throw new ArgumentOutOfRangeException(nameof(points));
        Score += points;
    }

    public override string ToString() => $"{Name} (Score: {Score})";
}
