using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinalWebProject
{
    public class Connection
    {
        public static string GetConnectionString()
        {
            string FILE_NAME = "projectDatabase1.accdb";
            string location = HttpContext.Current.Server.MapPath("~/App_Data/" + FILE_NAME);
            string connectionString = @"provider=Microsoft.ACE.OLEDB.12.0;
             data source=" + location;
            return connectionString;
        }
    }
}