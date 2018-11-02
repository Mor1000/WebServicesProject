using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FinalWebProject
{
    /// <summary>
    /// This is the code behind of the users login page.
    /// </summary>
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        /// <summary>
        /// A method called when the user clicked the login button. If the username and password
        /// are valid and exists in the users table in the databese the user 
        /// should be successfully signed in. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void LoginClick(object sender, EventArgs e)
        {
            /*loading all the validators from the "loginValidation" validation group
                * and checking if they are all valid.*/
            Validate("loginValidation");
            if (IsValid)
                //The database connection in the using block will be automatically closed in any event.      
                using (OleDbConnection conn = new OleDbConnection(Connection.GetConnectionString()))
                {
                    string query = "SELECT UserName, UserPassword FROM Users where userName = @user and UserPassword = @password"; ;//This query is parameterized so that the user input will be checked only as one of the fields in the table.
                    OleDbCommand command = new OleDbCommand(query, conn);
                    //defining the query's parameters.
                    command.Parameters.AddWithValue("@user", userTextBox.Text);
                    command.Parameters.AddWithValue("@password", passwordTextBox.Text);
                    conn.Open();
                    OleDbDataReader reader = command.ExecuteReader();//executing the query.
                    if (!reader.Read())//check if the data was found in the database. 
                        Response.Write("<script>alert('Login failed');</script>");
                }
        }

        /// <summary>
        /// switching to the registration page when the sign up button is clicked.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void SignUpClick(object sender, EventArgs e)
        {
            Response.Redirect("~/Register.aspx");
        }
    }
}