using FinBit.Services.Contracts.Models;
using FinBitTest.Services.Contracts.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FinBit.Services.Contracts
{
    public interface IValuesHandler
    {
        Task LogAsync(IEnumerable<ValueDto> values, Log log);

        Task<IEnumerable<ValueDto>> FetchAsync(ValueFilter filter, Log log);
    }
}