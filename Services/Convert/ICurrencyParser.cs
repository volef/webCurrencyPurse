using System.Collections.Generic;
using System.Threading.Tasks;
using webCurrencyPurse.Models;

namespace webCurrencyPurse.Services.Convert
{
    public interface ICurrencyParser
    {
        public Task<List<Currency>> GetCurrencies();
    }
}