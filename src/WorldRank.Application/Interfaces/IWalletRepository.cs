using WorldRank.Domain.Entities;
using WorldRank.Domain.Enums;

namespace WorldRank.Application.Interfaces;

public interface IWalletRepository
{
	Task AddAsync(Wallet wallet,CancellationToken cancellationToken);

	Task <Wallet[]> GetAllAsync(CancellationToken cancellationToken);
	Task <List<Wallet>> GetAllWalletsByPlayerIdAsync(int playerId, CancellationToken cancellationToken);

	Task<Wallet> GetWalletAsync(int playerId, Currency currency, CancellationToken cancellationToken);

	//Task UpdateBalanceAsync(int playerId, Currency currency, decimal newBalance, CancellationToken cancellationToken);

	Task DepositAsync(int playerId, Currency currency, decimal amount, CancellationToken cancellationToken);

	//Task WithdrawAsync(int playerId, Currency currency, decimal amount, CancellationToken cancellationToken);

	//Task BlockAsync(int playerId, Currency currency, CancellationToken cancellationToken);

	//Task UnblockAsync(int playerId, Currency currency, CancellationToken cancellationToken);
}
