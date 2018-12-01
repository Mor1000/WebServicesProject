using System;
using System.Collections.Generic;
using System.Configuration;
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
        public string GetConnectionString()
        {
            return ConfigurationManager.ConnectionStrings["DBconnection"].ToString();
            
        }
    }
} 