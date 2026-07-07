namespace WorldRank.Models;

public class Wallet
{
    private static int _nextAvailableId = 0;
    public int Id { get; }
    public decimal Balance { get; private set; }
    public CurrencyTypes Currency  { get; }
    bool IsBlocked { get; set; }

    public Wallet(CurrencyTypes currency)
    {
        Currency = currency;
        Balance = 0;
        Id = _nextAvailableId;

        _nextAvailableId++;
    }

    public void Block()
    {
        IsBlocked = true;
    }

    public void Unblock()
    {
        IsBlocked = false;
    }

    public void Deposit(decimal amount)
    {
        if (amount <= 0)
        {
            throw new ArgumentException("Deposit amount must be positive");
        }
        Balance += amount;
    }

    public void Withdraw(decimal amount)
    {
        if (amount <= 0)
        {
            throw new ArgumentException("Withdraw amount must be positive");
        }
        // Negative Balance is not allowed, this is the only method that could lead to such a scenario
        if (Balance - amount < 0)
        {
            throw new InvalidOperationException($"Insufficient funds. Your current balance is: {Balance}.\nBalance cannot go negative");
        }

        Balance -= amount;
    } 


}