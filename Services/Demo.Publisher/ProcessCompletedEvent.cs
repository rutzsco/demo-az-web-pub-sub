using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Publisher
{
    public class ProcessCompletedEvent
    {
        public Guid Id { get; set; }

        public Guid ProcessId { get; set; }

        public string ClientId { get; set; }

        public DateTime Timestamp { get; set; }
    }

    public class ProcessCompletedEventSummary
    {
        public Guid ProcessId { get; set; }

        public string ClientId { get; set; }
    }
}
