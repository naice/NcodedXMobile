using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NcodedXMobile
{
    public class InvalidConfigurationException : Exception
    {
        public InvalidConfigurationException(string defaultConfig)
            : base($"Invalid configuration detected. No {defaultConfig} set. Setup default dependecys via Configuration class before using.")
        {

        }
    }
}
