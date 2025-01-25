using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinBit.Services.Contracts.Models
{
    public class ValueFilter
    {
        public int? Code { get; set; }
        public string Value { get; set; }
        public bool And { get; set; }

        public override string ToString()
        {
            return $"Code = {Code}, Value = {Value}, And = {And}";
        }
    }
}
