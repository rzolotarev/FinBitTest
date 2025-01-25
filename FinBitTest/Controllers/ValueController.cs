using FinBit.Services.Contracts;
using FinBit.Services.Contracts.Models;
using FinBitTest.Controllers.Dtos;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace FinBitTest.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ValueController : ControllerBase
    {
        private readonly IValuesHandler _valuesHandler;

        public ValueController(IValuesHandler valuesHandler)
        {
            _valuesHandler = valuesHandler ?? throw new ArgumentNullException(nameof(valuesHandler));
        }

        [HttpGet]
        public async Task<IEnumerable<Services.Contracts.Dtos.ValueDto>> GetAsync([FromQuery] ValueFilter filter)
        {
            return await _valuesHandler.FetchAsync(filter, new Log { Query = $"{nameof(ValueController)}.{nameof(GetAsync)}", Payload = filter.ToString() });
        }

        [HttpPost]
        public async Task SaveAsync([FromBody] string jsonValues)
        {
            var values = JsonSerializer.Deserialize<ICollection<ValueDto>>(jsonValues);
            await _valuesHandler.LogAsync(values.Select(v => new Services.Contracts.Dtos.ValueDto { Code = int.Parse(v.Code), Value = v.Value }), new Log { Query = $"{nameof(ValueController)}.{nameof(SaveAsync)}", Payload = jsonValues });
        }
    }
}