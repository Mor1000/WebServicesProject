using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FinalWebProject
{
    public partial class GridView_sample : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
           
        }

        public DataSet GetAllRarities()
        {
            using (OleDbConnection conn = new OleDbConnection(Connection.GetConnectionString()))
            {

                string query = "SELECT* FROM AllMagicCards";//This query is parameterized so that the user input will be checked only as one of the fields in the table.
                OleDbCommand command = new OleDbCommand(query, conn);
                conn.Open();
                DataSet ds = new DataSet();
                OleDbDataAdapter dataAdapter = new OleDbDataAdapter(command);
                DataTable usersTable = new DataTable("AllMagicCards");
                dataAdapter.Fill(usersTable);
                ds.Tables.Add(usersTable);
                return ds;
            }
        }
    }
}