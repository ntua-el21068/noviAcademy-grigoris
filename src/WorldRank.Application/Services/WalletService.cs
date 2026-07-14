using Microsoft.Extensions.Logging;
using WorldRank.Application.Interfaces;
using WorldRank.Application.Strategies;
using WorldRank.Domain.Entities;
using WorldRank.Domain.Exceptions;
using WorldRank.Domain.Enums;


namespace WorldRank.Application.Services;

public class WalletService
{
	private readonly IWalletRepository _walletRepository;
	private readonly IPlayerRepository _playerRepository;
	private readonly ILogger<WalletService> _logger;
	private readonly IReadOnlyDictionary<FundsOperation, IFundsStrategy> _fundsStrategies;
	private readonly ICache _cache;

	public WalletService(
		IWalletRepository walletRepository,
		IPlayerRepository playerRepository,
		IEnumerable<IFundsStrategy> strategies,
		ILogger<WalletService> logger,
		ICache cache)
		
	{
		_walletRepository = walletRepository;
		_playerRepository = playerRepository;
		_logger = logger;
		_cache = cache;

		// Index every registered strategy by the operation it implements.
		_fundsStrategies = strategies.ToDictionary(strategy => strategy.Operation);
	}

	public async Task <Wallet> AddWalletToPlayer(int playerId, Currency currency, decimal balance, CancellationToken cancellationToken)
	{
		var player = await _playerRepository.FindPlayerAsync(playerId, cancellationToken) ;
		if (player is null)
			throw new PlayerNotFoundException(playerId);
		
		var wallet = new Wallet(await GenerateWalletIdAsync(cancellationToken), playerId, currency, balance);
		await _walletRepository.AddAsync(wallet, cancellationToken);
		_cache.Remove($"Wallets_{playerId}");
		return wallet;
	}

	public async Task <List<Wallet>> GetWalletsOfPlayer(int playerId, CancellationToken cancellationToken)
	{
		var cacheKey = $"Wallets_{playerId}";
		if(_cache.TryGet(cacheKey, out List<Wallet>? cached) && cached is not null)
		{
			return cached;
		}
		var player = await _playerRepository.FindPlayerAsync(playerId, cancellationToken);
		if(player is null) throw new PlayerNotFoundException(playerId);

		var wallets = await _walletRepository.GetAllWalletsByPlayerIdAsync(playerId, cancellationToken);
		_cache.Set(cacheKey, wallets, TimeSpan.FromSeconds(60));

		return wallets;

		
	}

	public async Task DepositToWalletAsync(int playerId, decimal amount, Currency currency, CancellationToken cancellationToken)
	{
		var player = await _playerRepository.FindPlayerAsync(playerId, cancellationToken);
		if(player is null) throw new PlayerNotFoundException(playerId);
		var wallet = await _walletRepository.GetWalletAsync(playerId, currency, cancellationToken);

		wallet.Deposit(amount);
		await _walletRepository.SaveChangesAsync(cancellationToken);
		_cache.Remove($"Wallets_{playerId}");

	}

	// public void WithdrawFromWallet()
	// {
	// 	var playerId = Prompts.PromptPlayerId();
	// 	if (playerId is null)
	// 		return;

	// 	var currency = Prompts.PromptCurrency();
	// 	if (currency is null)
	// 		return;

	// 	var amount = Prompts.PromptAmount("Amount to withdraw");
	// 	if (amount is null)
	// 		return;

	// 	RunWalletOperation(() =>
	// 	{
	// 		_walletRepository.Withdraw(playerId.Value, currency.Value, amount.Value);
	// 		Console.WriteLine("Withdrawal successful.");
	// 	});
	// }

	// public void BlockWallet()
	// {
	// 	var playerId = Prompts.PromptPlayerId();
	// 	if (playerId is null)
	// 		return;

	// 	var currency = Prompts.PromptCurrency();
	// 	if (currency is null)
	// 		return;

	// 	RunWalletOperation(() =>
	// 	{
	// 		_walletRepository.Block(playerId.Value, currency.Value);
	// 		Console.WriteLine("Wallet blocked.");
	// 	});
	// }

	// public void UnblockWallet()
	// {
	// 	var playerId = Prompts.PromptPlayerId();
	// 	if (playerId is null)
	// 		return;

	// 	var currency = Prompts.PromptCurrency();
	// 	if (currency is null)
	// 		return;

	// 	RunWalletOperation(() =>
	// 	{
	// 		_walletRepository.Unblock(playerId.Value, currency.Value);
	// 		Console.WriteLine("Wallet unblocked.");
	// 	});
	// }

	// public void UpdateWalletBalance()
	// {
	// 	var playerId = Prompts.PromptPlayerId();
	// 	if (playerId is null)
	// 		return;

	// 	var currency = Prompts.PromptCurrency();
	// 	if (currency is null)
	// 		return;

	// 	var newBalance = Prompts.PromptAmount("New balance");
	// 	if (newBalance is null)
	// 		return;

	// 	RunWalletOperation(() =>
	// 	{
	// 		_walletRepository.UpdateBalance(playerId.Value, currency.Value, newBalance.Value);
	// 		Console.WriteLine("Balance updated.");
	// 	});
	// }

	// public void ApplyFundsStrategy()
	// {
	// 	var playerId = Prompts.PromptPlayerId();
	// 	if (playerId is null)
	// 		return;

	// 	var currency = Prompts.PromptCurrency();
	// 	if (currency is null)
	// 		return;

	// 	var operation = Prompts.PromptFundsOperation();
	// 	if (operation is null)
	// 		return;

	// 	var amount = Prompts.PromptAmount("Amount");
	// 	if (amount is null)
	// 		return;

	// 	// Pick the strategy that matches the chosen operation (resolved from DI, no factory).
	// 	var strategy = _fundsStrategies[operation.Value];

	// 	RunWalletOperation(() =>
	// 	{
	// 		var wallet = _walletRepository.GetWallet(playerId.Value, currency.Value);
	// 		strategy.Execute(wallet, amount.Value);
	// 		_logger.LogInformation("Applied {Strategy} of {Amount} to player {PlayerId} {Currency} wallet (balance {Balance})",
	// 			strategy.GetType().Name, amount, playerId, currency, wallet.Balance);
	// 		Console.WriteLine($"{operation} operation applied.");
	// 	});
	// }

	// // Runs a wallet operation and turns any domain (WalletException) failure into a friendly message + log.
	// private void RunWalletOperation(Action operation)
	// {
	// 	try
	// 	{
	// 		operation();
	// 	}
	// 	catch (WalletException ex)
	// 	{
	// 		_logger.LogWarning(ex, "Wallet operation failed");
	// 		Console.WriteLine($"Error: {ex.Message}");
	// 	}
	// }

	private async Task <int> GenerateWalletIdAsync(CancellationToken cancellationToken)
	{
		var existingIds = (await _walletRepository.GetAllAsync(cancellationToken)).Select(p => p.Id).ToHashSet();

		int id;
		do
		{
			id = Random.Shared.Next(1, int.MaxValue);
		}
		while (existingIds.Contains(id));

		return id;
	}
}
