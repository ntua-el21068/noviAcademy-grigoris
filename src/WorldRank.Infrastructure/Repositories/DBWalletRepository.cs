using Microsoft.Extensions.Logging;
using WorldRank.Domain.Entities;
using WorldRank.Domain.Enums;
using WorldRank.Domain.Exceptions;
using WorldRank.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using WorldRank.Application.Repositories;

namespace WorldRank.Infrastructure.Repositories;

public class DBWalletRepository : IWalletRepository
{
    private readonly WorldRankDbContext _context;
    private readonly ILogger<DBWalletRepository> _logger;

    public DBWalletRepository(WorldRankDbContext context, ILogger<DBWalletRepository> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task AddAsync(Wallet wallet, CancellationToken cancellationToken)
    {
        var exists = await _context.Wallets.AnyAsync(w => w.PlayerId == wallet.PlayerId && w.Currency == wallet.Currency, cancellationToken);
        if (exists)
            throw new DuplicateWalletException(wallet.PlayerId, wallet.Currency);

        _context.Wallets.Add(wallet); //Deferred Execution, (synch) Add is okay
        await SaveChangesAsync(cancellationToken);
        _logger.LogInformation("Wallet created for player {PlayerId} in {Currency} with balance {Balance}", wallet.PlayerId, wallet.Currency, wallet.Balance);
    }

    public async Task<Wallet[]> GetAllAsync(CancellationToken cancellationToken)
    {
        var wallets = await _context.Wallets.ToArrayAsync(cancellationToken);
        return wallets;
    }

    public async Task<List<Wallet>> GetAllWalletsByPlayerIdAsync(int playerId, CancellationToken cancellationToken)
    {
        var wallets = await _context.Wallets.Where(w => w.PlayerId == playerId).ToListAsync(cancellationToken);
        return wallets;
    }
        
    public async Task< Wallet > GetWalletAsync(int playerId, Currency currency, CancellationToken cancellationToken)
    {
        var wallet = await _context.Wallets.SingleOrDefaultAsync(w => w.PlayerId == playerId && w.Currency == currency, cancellationToken);
        if (wallet is null)
            throw new WalletNotFoundException(playerId, currency);
        return wallet;
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken)
    {
       await  _context.SaveChangesAsync(cancellationToken);
    }


    // public void UpdateBalance(int playerId, Currency currency, decimal newBalance)
    // {
    //     GetWallet(playerId, currency).SetBalance(newBalance);
    //     _context.SaveChanges();
    //     _logger.LogInformation("Player {PlayerId} {Currency} wallet balance set to {Balance}", playerId, currency, newBalance);
    // }

    // public void Deposit(int playerId, Currency currency, decimal amount)
    // {
    //     var wallet = GetWallet(playerId, currency);
    //     wallet.Deposit(amount);
    //     _context.SaveChanges();
    //     _logger.LogInformation("Deposited {Amount} to player {PlayerId} {Currency} wallet (balance {Balance})", amount, playerId, currency, wallet.Balance);
    // }

    // public void Withdraw(int playerId, Currency currency, decimal amount)
    // {
    //     var wallet = GetWallet(playerId, currency);
    //     wallet.Withdraw(amount);
    //     _context.SaveChanges();
    //     _logger.LogInformation("Withdrew {Amount} from player {PlayerId} {Currency} wallet (balance {Balance})", amount, playerId, currency, wallet.Balance);
    // }

    // public void Block(int playerId, Currency currency)
    // {
    //     GetWallet(playerId, currency).Block();
    //     _context.SaveChanges();
    //     _logger.LogInformation("Player {PlayerId} {Currency} wallet blocked", playerId, currency);
    // }

    // public void Unblock(int playerId, Currency currency)
    // {
    //     GetWallet(playerId, currency).Unblock();
    //     _context.SaveChanges();
    //     _logger.LogInformation("Player {PlayerId} {Currency} wallet unblocked", playerId, currency);
    // }
}
