using System;
using System.Collections.Generic;
using System.Text;
using WorldRank.Domain.Enums;

namespace WorldRank.Domain.Entities
{
    public  class CurrencyRate 
    {
        public int Id { get; private set; }
        public string Currency { get; private set; }

        public decimal Rate { get; private set; }

        public DateTime Date { get; private set; }
        private CurrencyRate() { }

        public CurrencyRate(string currency, decimal rate, DateTime date) {
            Currency = currency;
            Rate = rate;
            Date = date;
        }
    }
}
