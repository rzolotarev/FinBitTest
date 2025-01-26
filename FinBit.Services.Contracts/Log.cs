using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinBit.Services.Contracts
{
    public class Log
    {
        public int Id { get; set; }
        public string Query { get; set; }
        public string Payload { get; set; }

        // index in db
        public DateTime DateTime { get; set; }
    }
}
