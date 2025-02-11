using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions
{
    public class ApiToReturn
    {
        public string Message { get; set; }
        public int StatusCode { get; set; }
        public string Detail { get; set; }
        public List<string> Messages { get; set; }

        public ApiToReturn()
        {

        }
        public ApiToReturn(string message)
        {
            Message = message;
        }
        public ApiToReturn(int statusCode , string message )
        {
            statusCode = statusCode;
            Message = message;
        }
        public ApiToReturn(int statusCode, List<string> messages)
        {
            StatusCode = statusCode;
           // Message = message;
            Messages = messages;
        }
        public ApiToReturn(int statusCode , List<string> messages , string   detail)
        {
            statusCode = statusCode;
            Messages = messages;
            //Message = messages != null && messages.Any() ? null : message ;
            Detail = detail;
        }
    }
}
