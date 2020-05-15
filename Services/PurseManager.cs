using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using webCurrencyPurse.Data;
using webCurrencyPurse.Models;

namespace webCurrencyPurse.Services
{
    public interface IPurseManager
    {
        public Task<bool> CheckPurseAsync(string userid);
        public Task<Result> AddPurseAsync(string userid);

        public Task<Result<Purse>> GetPurseAsync(string userid);
    }

    public class PurseManager : IPurseManager
    {
        private readonly ApplicationDbContext _dbContext;

        public PurseManager(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> CheckPurseAsync(string userid)
        {
            if (string.IsNullOrEmpty(userid))
                return false;

            return await _dbContext.Purses.AnyAsync(user => user.Id == userid);
        }

        public async Task<Result> AddPurseAsync(string userid)
        {
            if (string.IsNullOrEmpty(userid))
                return new Result(ResultEnum.Error, "Invalid userid");

            var checkResult = await CheckPurseAsync(userid);
            if (checkResult)
                return new Result(ResultEnum.Fail, "Purse already exist");

            var addResult = await _dbContext.Purses.AddAsync(new Purse {Id = userid});
            await _dbContext.SaveChangesAsync();
            if (addResult.Entity == null)
                return new Result(ResultEnum.Fail, "Cannot add user");

            return new Result(ResultEnum.Success, string.Empty);
        }

        public async Task<Result<Purse>> GetPurseAsync(string userid)
        {
            var linqPurse = await _dbContext.Purses
                .Include(purse => purse.Bills)
                .FirstOrDefaultAsync(purse => purse.Id == userid);
            if (linqPurse == null)
                return new Result<Purse>(ResultEnum.Error, null, "Purse does not exist");

            return new Result<Purse>(ResultEnum.Success, linqPurse, string.Empty);
        }
    }
}