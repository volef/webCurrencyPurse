using Microsoft.Extensions.DependencyInjection;
using webTest.Services.Convert;

namespace webTest.Services
{
    public static class ExtendServices
    {
        public static void AddUserManager(this IServiceCollection collection)
        {
            collection.AddScoped<IPurseManager, PurseManager>();
        }

        public static void AddCurrencyParser(this IServiceCollection collection, string useparser)
        {
            collection.AddHttpClient();
            
            switch (useparser)
            {
                case "СBR":
                {
                    collection.AddTransient<ICurrencyParser, CbrCurrencyParser>();
                    break;
                }
                case "ECB":
                {
                    collection.AddTransient<ICurrencyParser, EcbCurrencyParser>();
                    break;
                }
                //Anything else...
                default:
                {
                    collection.AddTransient<ICurrencyParser, CbrCurrencyParser>();
                    break;
                }
            }
        }

        public static void AddCurrencyConverter(this IServiceCollection collection)
        {
            collection.AddSingleton<ICurrencyConverter, CurrencyConverter>();
        }

        public static void AddBillManager(this IServiceCollection collection)
        {
            collection.AddScoped<IBillManager, BillManager>();
        }
    }
}