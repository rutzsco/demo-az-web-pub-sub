using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Subscriber
{
    public class ProcessCompletedEvent
    {
        public Guid Id { get; set; }

        public Guid ProcessId { get; set; }

    }
}
