using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherForecast.Service.Errors
{
    public class GenericError
    {
        public string Code { get; protected set; }

        public string Message { get; protected set; }

        public string ExceptionMessage { get; protected set; }

        protected GenericError()
        {

        }

        public GenericError(string errorMessage, string errorCode, Exception ex = null)
        {
            Code = errorCode;
            Message = errorMessage;
            ExceptionMessage = ex?.Message;
        }
    }

    public class NullGenericError : GenericError
    {
        
    }
}
