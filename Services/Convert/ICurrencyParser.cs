using System.Collections.Generic;
using System.Threading.Tasks;
using webTest.Models;

namespace webTest.Services.Convert
{
    public interface ICurrencyParser
    {
        public Task<List<Currency>> GetCurrencies();
    }
}