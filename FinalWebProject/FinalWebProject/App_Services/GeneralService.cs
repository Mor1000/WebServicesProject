using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Web;

namespace FinalWebProject.App_Services
{
    public class GeneralService
    {
        /// <summary>
        /// /This is a generic method for generating a dataset with table.
        /// </summary>
        /// <param name="command">Object exceutes the query. There should be a query assigned (commendText property) in this object.</param>
        /// <param name="tableName">The name of the dataset table.</param>
        /// <returns></returns>
        public DataSet GetDataset(OleDbCommand command, string tableName)
        {
            if (command == null)
                return null;
            using (OleDbConnection conn = new OleDbConnection(Connection.GetConnectionString()))
            {
                command.Connection = conn;
                conn.Open();
                DataSet ds = new DataSet();
                OleDbDataAdapter dataAdapter = new OleDbDataAdapter(command);
                DataTable dataTable = new DataTable(tableName);
                dataAdapter.Fill(dataTable);
                ds.Tables.Add(dataTable);
                return ds;
            }
        }
    }
}