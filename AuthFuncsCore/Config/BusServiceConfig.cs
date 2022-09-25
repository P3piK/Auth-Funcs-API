using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthFuncsCore.Config
{
    public class BusServiceConfig
    {
        public string ConnectionString { get; set; }
        public string EmailQueueName { get; set; }
    }
}
