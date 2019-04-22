using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Project_DMX_2._0.Exceptions
{
    public class UnknownDeviceTypeException : ApplicationException
    {
        public UnknownDeviceTypeException()
            : base()
        { }

        public UnknownDeviceTypeException(string message)
            : base(message)
        { }

        public UnknownDeviceTypeException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
