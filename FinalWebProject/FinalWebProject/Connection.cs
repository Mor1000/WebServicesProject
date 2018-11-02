using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinalWebProject
{
    /// <summary>
    /// This class conatains utilities for databases connection.
    /// </summary>
    public class Connection
    {
        /// <summary>
        /// Connecting to the local database.
        /// </summary>
        /// <returns>A string with the database connection content.</returns>
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