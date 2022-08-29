using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthFuncsService.Exception
{
    public class BadRequestException : System.Exception
    {
        public BadRequestException()
        {
        }

        public BadRequestException(string message) : base(message)
        {
        }
    }
}
