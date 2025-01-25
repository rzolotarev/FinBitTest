using FinBit.Services.Contracts;
using FinBit.Services.Contracts.Models;
using FinBitTest.Services.Contracts.Dtos;

namespace FinBit.Services
{
    public class ValuesHandler : IValuesHandler
    {
        private readonly IValueRepository _valueRepository;

        public ValuesHandler(IValueRepository valueRepository)
        {
            _valueRepository = valueRepository ?? throw new ArgumentNullException(nameof(valueRepository));
        }

        public async Task LogAsync(IEnumerable<ValueDto> values, Log log)
        {
            await _valueRepository.SaveAsync(values.Select(v => new PersistenceValue { Code = v.Code, Value = v.Value }).OrderBy(v => v.Code).ToArray(), log);
        }

        public async Task<IEnumerable<ValueDto>> FetchAsync(ValueFilter filter, Log log)
        {
            return await _valueRepository.GetAsync(filter, log);
        }
    }
}