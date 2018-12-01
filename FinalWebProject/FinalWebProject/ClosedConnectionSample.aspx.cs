using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FinalWebProject
{
    public partial class ClosedConnectionSample : System.Web.UI.Page
    {
        string CS = (new Connection()).GetConnectionString();
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void onShowButton(object sender, EventArgs e)
        {
            try
            {
                //The database connection in the using block will be automatically closed in any event.      
                using (OleDbConnection conn = new OleDbConnection(CS))
                {

                    string query = "SELECT mtgArenaName FROM Users";//This query is parameterized so that the user input will be checked only as one of the fields in the table.
                    OleDbCommand command = new OleDbCommand(query, conn);
                    // defining the query's parameters.
                    conn.Open();
                    DataSet usersDataSet = GetAllArenanames(command);
                    foreach (DataRow rows in usersDataSet.Tables["Users"].Rows)
                    {
                        arenaDropDownList.Items.Add(new ListItem(rows["mtgArenaName"].ToString()));
                    }
                }
            }
            catch (OleDbException ex)
            {
                Debug.WriteLine("Error occured: " + ex.Message);
                Debug.WriteLine(ex.StackTrace);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error occured: " + ex.Message + ": " + ex.GetType());
                Debug.WriteLine(ex.StackTrace);
            }
        }
        public DataSet GetAllArenanames(OleDbCommand command)
        {
            DataSet ds = new DataSet();
            OleDbDataAdapter dataAdapter=new OleDbDataAdapter(command);
            DataTable usersTable = new DataTable("Users");
            dataAdapter.Fill(usersTable);
            ds.Tables.Add(usersTable);
            return ds;
        }
    }
}