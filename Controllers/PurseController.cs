using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using webTest.Models;
using webTest.Serialization;
using webTest.Services;

namespace webTest.Controllers
{
    [ApiController]
    public class PurseController : ControllerBase
    {
        private readonly IPurseManager _purseManager;
        private readonly IBillManager _billManager;


        public PurseController(IPurseManager purseManager, IBillManager billManager)
        {
            _purseManager = purseManager;
            _billManager = billManager;
        }

        /// <summary>
        /// Вывод состояния всех счетов валютного кошелька
        /// </summary>
        /// <param name="id">Идентификатор кошелька</param>
        /// <returns>Состояние счетов указанного кошелька</returns>
        /// <response code="200">Данные успешно получены</response>
        /// <response code="403">Данные не получены (причину см description)</response>
        [HttpGet]
        [Route("api/[controller]/{id}")]
        public async Task<IActionResult> GetPurse(string id)
        {
            var addresult = await _purseManager.GetPurseAsync(id);

            if (addresult.Status != ResultEnum.Success)
                return StatusCode(403, addresult);

            return StatusCode(200, addresult);
        }

        /// <summary>
        /// Создание валютного кошелька
        /// </summary>
        /// <param name="id">Идентификатор кошелька</param>
        /// <returns>Состояние счетов указанного кошелька</returns>
        /// <response code="201">Кошелек успешно создан</response>
        /// <response code="403">Кошелек не создан (причину см description)</response>
        [HttpPut]
        [Route("api/[controller]/add")]
        public async Task<IActionResult> AddPurse([Required] string id)
        {
            var addresult = await _purseManager.AddPurseAsync(id);

            if (addresult.Status != ResultEnum.Success)
                return StatusCode(400, addresult);

            return StatusCode(201, addresult);
        }

        /// <summary>
        /// Внесение денег на валютный счет кошелька
        /// </summary>
        /// <param name="id">Идентификатор кошелька</param>
        /// <param name="currency">Сокращенное название валюты (RUB, USD, EUR...)</param>
        /// <param name="money">Сумма для внесения</param>
        /// <returns>Состояние счета пополняемой валюты</returns>
        /// <response code="200">Деньги успешно внесены</response>
        /// <response code="400">Деньги не внесены (причину см description)</response>
        /// <response code="403">Кошелек не найден</response>
        [HttpPost]
        [Route("api/[controller]/{id}/put")]
        public async Task<IActionResult> PutMoney(string id, [Required] string currency, [Required] decimal money)
        {
            var userexist = await _purseManager.CheckPurseAsync(id);
            if (!userexist)
                return StatusCode(403, new Result(ResultEnum.Error, "Purse does not exist"));

            var putresult = await _billManager.PutMoneyAsync(id, currency, money);
            if (putresult.Status != ResultEnum.Success)
                return StatusCode(400, putresult);

            return StatusCode(200, putresult);
        }

        /// <summary>
        /// Вывод денег на валютный счет кошелька
        /// </summary>
        /// <param name="id">Идентификатор кошелька</param>
        /// <param name="currency">Сокращенное название валюты (RUB, USD, EUR...)</param>
        /// <param name="money">Сумма для вывода</param>
        /// <returns>Состояние счета выводимой валюты</returns>
        /// <response code="200">Деньги успешно выведены</response>
        /// <response code="400">Деньги не выведены (причину см description)</response>
        /// <response code="403">Кошелек не найден</response>
        [HttpPost]
        [Route("api/[controller]/{id}/withdraw")]
        public async Task<IActionResult> WithDrawMoney(string id, [Required] string currency, [Required] decimal money)
        {
            var userexist = await _purseManager.CheckPurseAsync(id);
            if (!userexist)
                return StatusCode(403, new Result(ResultEnum.Error, "Purse does not exist"));

            var withDrawMoney = await _billManager.WithDrawMoneyAsync(id, currency, money);
            if (withDrawMoney.Status != ResultEnum.Success)
                return StatusCode(400, withDrawMoney);

            return StatusCode(200, withDrawMoney);
        }

        /// <summary>
        /// Обмен валюты кошелька
        /// </summary>
        /// <param name="id">Идентификатор кошелька</param>
        /// <param name="from">Сокращенное название исходной валюты (RUB, USD, EUR...)</param>
        /// <param name="money">Сумма для обмена</param>
        /// <param name="to">Сокращенное название желаемой валюты (RUB, USD, EUR...)</param>
        /// <returns>Состояние счетов указанного кошелька</returns>
        /// <response code="200">Деньги успешно конвертированы</response>
        /// <response code="400">Деньги не конвертированы (причину см description)</response>
        /// <response code="403">Кошелек не найден</response>
        [HttpPost]
        [Route("api/[controller]/{id}/convert")]
        public async Task<IActionResult> ConvertMoney(string id, [Required] string from, [Required] decimal money,
            [Required] string to)
        {
            var userExist = await _purseManager.CheckPurseAsync(id);
            if (!userExist)
                return StatusCode(403, new Result(ResultEnum.Error, "Purse does not exist"));

            var convertResult = await _billManager.ConvertMoneyAsync(id, from, money, to);
            if (convertResult.Status != ResultEnum.Success)
                return StatusCode(400, convertResult);

            return StatusCode(200, convertResult);
        }
    }
}