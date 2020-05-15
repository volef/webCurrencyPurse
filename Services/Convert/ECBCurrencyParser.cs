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
    public class EcbCurrencyParser : ICurrencyParser
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private const string Uri = "https://www.ecb.europa.eu/stats/eurofxref/eurofxref-daily.xml";

        public EcbCurrencyParser(IHttpClientFactory httpClientFactory)
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

        private static Task<List<Currency>> Parse(Envelope toparse)
        {
            var result = toparse.Cube.Cube1.Cube
                .Select(currency => new Currency {Name = currency.Currency, Rate = currency.Rate}).ToList();

            return Task.FromResult(result);
        }

        public async Task<List<Currency>> GetCurrencies()
        {
            var serializer = new XmlSerializer(typeof(Envelope));
            var toparse = (Envelope) serializer.Deserialize(await Get());

            return await Parse(toparse).ConfigureAwait(false);
        }
    }
}