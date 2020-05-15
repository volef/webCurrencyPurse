using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using webTest.Data;
using webTest.Models;
using webTest.Services.Convert;

namespace webTest.Services
{
    public interface IBillManager
    {
        Task<Result<Bill>> PutMoneyAsync(string userid, string currency, decimal cashvalue);
        Task<Result<Bill>> WithDrawMoneyAsync(string userid, string currency, decimal cashvalue);

        Task<Result<Purse>> ConvertMoneyAsync(string userid, string fromcurrency, decimal cashvalue, string tocurrency);
    }

    public class BillManager : IBillManager
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly ICurrencyConverter _currencyConverter;
        private readonly IPurseManager _purseManager;

        public BillManager(ApplicationDbContext dbContext, ICurrencyConverter currencyConverter,
            IPurseManager purseManager)
        {
            _dbContext = dbContext;
            _currencyConverter = currencyConverter;
            _purseManager = purseManager;
        }


        private async Task<Result<Bill>> GetBillAsync(string userid, string currency)
        {
            currency = currency?.ToUpper();
            if (string.IsNullOrEmpty(userid))
                return new Result<Bill>(ResultEnum.Error, null, "Invalid user id");
            if (string.IsNullOrEmpty(currency) || currency.Length != 3)
                return new Result<Bill>(ResultEnum.Error, null, "Invalid currency");

            var result = await _dbContext.Bills.FirstOrDefaultAsync(bill =>
                bill.PurseId == userid && bill.CurrencyName == currency);

            if (result == null)
                return new Result<Bill>(ResultEnum.Fail, null, "Bill does not exist");

            return new Result<Bill>(ResultEnum.Success, result, string.Empty);
        }

        public async Task<Result<Bill>> PutMoneyAsync(string userid, string currency, decimal cashvalue)
        {
            currency = currency?.ToUpper();

            var getBillResult = await GetBillAsync(userid, currency);
            if (getBillResult.Status == ResultEnum.Error)
                return new Result<Bill>(getBillResult.Status, getBillResult.Value, getBillResult.Description);
            
            if (cashvalue <= 0)
                return new Result<Bill>(ResultEnum.Error, getBillResult.Value, "Invalid cashvalue");

            var result = getBillResult.Value;

            if (getBillResult.Status == ResultEnum.Fail)
            {
                var checkCurrency = await _currencyConverter.CheckCurrency(currency);
                if (!checkCurrency)
                    return new Result<Bill>(ResultEnum.Error, getBillResult.Value, "This currency not found in DB");
                var dbBill = await _dbContext.Bills.AddAsync(new Bill
                    {CurrencyName = currency, PurseId = userid, Cash = cashvalue});
                result = dbBill.Entity;
            }
            else
            {
                result.Cash += cashvalue;
            }

            await _dbContext.SaveChangesAsync();

            return new Result<Bill>(ResultEnum.Success, result, string.Empty);
        }

        public async Task<Result<Bill>> WithDrawMoneyAsync(string userid, string currency, decimal cashvalue)
        {
            currency = currency?.ToUpper();

            var getBillResult = await GetBillAsync(userid, currency);
            if (getBillResult.Status != ResultEnum.Success)
                return new Result<Bill>(getBillResult.Status, getBillResult.Value, getBillResult.Description);
            
            if (cashvalue <= 0)
                return new Result<Bill>(ResultEnum.Error, getBillResult.Value, "Invalid cashvalue");

            if (getBillResult.Value.Cash < cashvalue)
                return new Result<Bill>(ResultEnum.Fail, getBillResult.Value, "Not enough money");

            getBillResult.Value.Cash -= cashvalue;

            await _dbContext.SaveChangesAsync();

            return new Result<Bill>(ResultEnum.Success, getBillResult.Value, string.Empty);
        }

        public async Task<Result<Purse>> ConvertMoneyAsync(string userid, string fromcurrency, decimal cashvalue,
            string tocurrency)
        {
            tocurrency = tocurrency?.ToUpper();
            fromcurrency = fromcurrency?.ToUpper();

            var purseResult = await _purseManager.GetPurseAsync(userid);
            if (purseResult.Status != ResultEnum.Success)
                return purseResult;

            if (cashvalue <= 0)
                return new Result<Purse>(ResultEnum.Error, purseResult.Value, "Invalid cashvalue");

            var checkCurrency = await _currencyConverter.CheckCurrency(tocurrency);
            if (!checkCurrency)
                return new Result<Purse>(ResultEnum.Error, purseResult.Value, "Сonvertible currency not found in DB");

            var fromBill = purseResult.Value.Bills.SingleOrDefault(from => from.CurrencyName == fromcurrency);
            if (fromBill != null)
            {
                if (fromBill.Cash < cashvalue)
                    return new Result<Purse>(ResultEnum.Error, purseResult.Value, "Not enough money for convertation");

                var cashToAdd = await _currencyConverter.GetConvertCashAsync(fromcurrency,cashvalue, tocurrency);
                if (cashToAdd <= 0)
                    return new Result<Purse>(ResultEnum.Error, purseResult.Value, "Error convertation");

                fromBill.Cash -= cashvalue;

                if (purseResult.Value.Bills.Exists(bill => bill.CurrencyName == tocurrency))
                    purseResult.Value.Bills.Single(bill => bill.CurrencyName == tocurrency).Cash += cashToAdd;
                else
                    purseResult.Value.Bills.Add(
                        new Bill
                        {
                            CurrencyName = tocurrency, PurseId = userid, Cash = cashToAdd
                        });

                await _dbContext.SaveChangesAsync();

                return new Result<Purse>(ResultEnum.Success, purseResult.Value, string.Empty);
            }
            else
            {
                return new Result<Purse>(ResultEnum.Error, purseResult.Value, "Invalid bill for convertation");
            }
        }
    }
}