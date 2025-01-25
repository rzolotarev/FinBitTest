namespace FinBit.Services.Contracts.Models
{
    public class PersistenceValue
    {
        public int Id { get; set; }

        // В БД индекс по этому полю.
        public int Code { get; set; }

        public string Value { get; set; }
    }
}
