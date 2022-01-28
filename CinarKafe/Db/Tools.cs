using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;

namespace CinarKafe.Db
{
    public static class Tools
    {
        public static string GetConnectionString(string name = "DefaultConnection")
        {
            return ConfigurationManager.ConnectionStrings[name].ConnectionString;
        }
    }
}