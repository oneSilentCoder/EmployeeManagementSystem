using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeAppUtilities.Configuration
{
    public class CustomConfigurationSource: IConfigurationSource
    {
        public IConfigurationProvider Build(IConfigurationBuilder builder)
        {
            return new CustomConfiguration();
        }
    }
}
