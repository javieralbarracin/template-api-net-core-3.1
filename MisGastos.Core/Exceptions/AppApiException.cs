using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace MisGastos.Core.Exceptions
{
    public class AppApiException : Exception
    {
        public AppApiException(string message) : base(message)
        {

        }

        public AppApiException(Exception exception) : base(exception.Message, exception)
        {

        }

        public AppApiException(string message, Exception exception) : base(message, exception)
        {

        }

        public AppApiException(IEnumerable<string> message) : base(string.Join(",", message.Distinct()))
        {

        }

        public AppApiException(IEnumerable<string> message, Exception exception) : base(string.Join(",", message.Distinct()), exception)
        {

        }

        public override string ToString()
        {
            if (InnerException == null)
            {
                return base.ToString();
            }

            return string.Format(CultureInfo.InvariantCulture, "{0} [See nested exception: {1}]", base.ToString(), InnerException);
        }
    }

}
