using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using webCurrencyPurse.Models;
using webCurrencyPurse.Serialization;

namespace webCurrencyPurse.Services.Convert
{
    public class CbrCurrencyParser : ICurrencyParser
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private const string Uri = "https://www.cbr-xml-daily.ru/daily.xml";

        public CbrCurrencyParser(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            Encoding.GetEncoding("windows-1251");
        }

        private async Task<Stream> Get()
        {
            using var client = _httpClientFactory.CreateClient();
            var httpResponse = await client.GetAsync(Uri);
            var stream = await httpResponse.Content.ReadAsStreamAsync();
            return stream;
        }

        private static Task<List<Currency>> Parse(ValCurs toparse)
        {
            var result = toparse.Valute.Select(currency => new Currency
                {Name = currency.CharCode, Rate = decimal.Parse(currency.Value) / currency.Nominal}).ToList();

            return Task.FromResult(result);
        }

        public async Task<List<Currency>> GetCurrencies()
        {
            var serializer = new XmlSerializer(typeof(ValCurs));
            var toparse = (ValCurs) serializer.Deserialize(await Get());

            return await Parse(toparse).ConfigureAwait(false);
        }
    }
}