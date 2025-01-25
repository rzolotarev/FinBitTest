using System.Collections.Generic;
using System.Threading.Tasks;
using FinBit.Services.Contracts.Models;
using FinBitTest.Services.Contracts.Dtos;

namespace FinBit.Services.Contracts
{
    public interface IValueRepository
    {
        Task SaveAsync(IEnumerable<PersistenceValue> values, Log log);

        Task<IEnumerable<ValueDto>> GetAsync(ValueFilter filter, Log log);
    }
}
