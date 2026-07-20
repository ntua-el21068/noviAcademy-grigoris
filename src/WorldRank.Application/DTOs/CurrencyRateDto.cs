using System;
using System.Collections.Generic;
using System.Text;

namespace WorldRank.Application.DTOs
{
    public record class CurrencyRateDto(string Currency, decimal Rate, DateTime Date);
    
}
