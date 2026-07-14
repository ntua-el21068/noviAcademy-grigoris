using WorldRank.Domain.Enums;

namespace WorldRank.Api.DTOs
{
public record CreateWalletRequest(int playerId, Currency currency, decimal balance);
}