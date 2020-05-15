using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using webTest.Models;

namespace webTest.Services.Convert
{
    public class CurrencyConverter : ICurrencyConverter
    {
        public ICurrencyParser Parser { get; }
        public string CurrencyBase { get; }
        public List<Currency> Currencies { get; private set; }

        public CurrencyConverter(ICurrencyParser parser)
        {
            Parser = parser;
            CurrencyBase = "RUB";
        }

        public async Task Update()
        {
            var parserresult = await Parser.GetCurrencies();
            if (parserresult != null)
                Currencies = parserresult;
        }

        public async Task<bool> CheckCurrency(string currency)
        {
            await Update();
            return Currencies.Exists(curr => curr.Name == currency) || currency == CurrencyBase;
        }

        public async Task<decimal> GetConvertCashAsync(string fromcurrency, decimal money, string tocurrency)
        {
            if (string.IsNullOrEmpty(fromcurrency) || string.IsNullOrEmpty(tocurrency))
                return -1m;

            await Update();

            var fromrate = fromcurrency == CurrencyBase
                ? 1
                : Currencies.FirstOrDefault(curr => curr.Name == fromcurrency)?.Rate;
            var torate = tocurrency == CurrencyBase
                ? 1
                : Currencies.FirstOrDefault(curr => curr.Name == tocurrency)?.Rate;
            if (fromrate == null || torate == null)
                return -1m;

            return (fromrate.Value*money) / torate.Value;
        }
    }
}