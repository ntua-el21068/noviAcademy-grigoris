
using WorldRank.Domain.Enums;

namespace WorldRank.Api.DTOs;

public record CreateDepositRequest( Currency currency, decimal amount);

