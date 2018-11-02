using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FinalWebProject
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void LoginClick(object sender, EventArgs e)
        {
            Validate("loginValidation");
            if (IsValid)
                using (OleDbConnection conn = new OleDbConnection(Connection.GetConnectionString()))
                {
                    string query = "SELECT UserName, UserPassword FROM Users where userName = @user and UserPassword = @password";
                    OleDbCommand command = new OleDbCommand(query, conn);
                    command.Parameters.AddWithValue("@user", userTextBox.Text);
                    command.Parameters.AddWithValue("@password", passwordTextBox.Text);
                    conn.Open();
                    OleDbDataReader reader = command.ExecuteReader();
                    if (!reader.Read())
                        Response.Write("<script>alert('Login failed');</script>");
                }
        }

        protected void SignUpClick(object sender, EventArgs e)
        {
            Response.Redirect("~/Register.aspx");
        }
    }
}