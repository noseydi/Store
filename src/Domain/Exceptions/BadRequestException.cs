using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions
{
    public class BadRequestException : BaseException
    {
        public BadRequestException(List<string> messages) : base(messages)
        {
        }
        public BadRequestException() : base ("خطایی رخ داده است. مجددا تلاش کنید")
        {
            
        }
    }
}
