using System;
using WorldRank.Exceptions;

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

        if(decimal.Round(amount, 2) != amount)
        {
            throw new InvalidCurrencyPrecisionException(amount);
        }

        Balance += amount;
    }

    public void Withdraw(decimal amount)
    {
        if (amount <= 0)
        {
            throw new ArgumentException("Withdraw amount must be positive");
        }

        if(decimal.Round(amount, 2) != amount)
        {
            throw new InvalidCurrencyPrecisionException(amount);
        }
        // Negative Balance is not allowed, this is the only method that could lead to such a scenario
        if (Balance - amount < 0)
        {
            throw new InsufficientFundsException(Id);
        }

        Balance -= amount;
    } 


}