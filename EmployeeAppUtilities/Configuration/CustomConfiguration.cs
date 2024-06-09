using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeAppUtilities.Configuration
{
    public class CustomConfiguration : ConfigurationProvider
    {
        public override void Load()
        {
            // Custom logic to load configuration, e.g., from a database, API, or custom file
            Data["JsonPlaceholder:BaseUrl"] = "https://jsonplaceholder.typicode.com/";
        }
    }    
}
