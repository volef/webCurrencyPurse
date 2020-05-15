using System.Collections.Generic;
using System.Threading.Tasks;
using webCurrencyPurse.Models;

namespace webCurrencyPurse.Services.Convert
{
    public interface ICurrencyConverter
    {
        public ICurrencyParser Parser { get; }
        public string CurrencyBase { get; }
        public List<Currency> Currencies { get; }
        public Task Update();

        public Task<bool> CheckCurrency(string currency);
        public Task<decimal> GetConvertCashAsync(string fromcurrency, decimal money, string tocurrency);
    }
}