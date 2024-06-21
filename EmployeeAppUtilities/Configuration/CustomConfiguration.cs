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
            Data["DummyApi:BaseUri"] = "https://dummyapi.io/data/v1/";
            //App Id generated in dummyapi.io website
            Data["DummyApi:AppId"] = "666d38f359e1f2a6b5be6ebe";
        }
    }    
}
