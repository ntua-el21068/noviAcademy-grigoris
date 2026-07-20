using MediatR;
using WorldRank.Application.DTOs;

namespace WorldRank.Application.Commands.CurrencyRates;

public record SaveCurrencyRatesCommand(IReadOnlyList<CurrencyRateDto> Rates) : IRequest;