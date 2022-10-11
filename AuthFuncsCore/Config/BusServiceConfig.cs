using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthFuncsCore.Config
{
    public class BusServiceConfig
    {
        public BusServiceConfig(IConfiguration configuration)
        {
            ConnectionString = configuration["BusServiceConnectionString"];
            EmailQueueName = configuration["BusServiceQueueName"];
        }

        public string ConnectionString { get; }
        public string EmailQueueName { get; }
    }
}
