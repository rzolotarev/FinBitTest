using Dapper.FluentMap.Mapping;
using FinBit.Services.Contracts.Models;

namespace FinBit.Persistence
{
    public class ValueMap : EntityMap<PersistenceValue>
    {
        public ValueMap()
        {
            Map(v => v.Value).ToColumn("Value");

            Map(v => v.Code).ToColumn("Code");

            Map(v => v.Id).ToColumn("Id");
        }
    }
}
