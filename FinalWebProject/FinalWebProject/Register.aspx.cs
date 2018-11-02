using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FinalWebProject
{
    public partial class Register : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void OnClearClick(object sender, EventArgs e)
        {
            userTextBox.Text = "";
            passwordTextBox.Text = "";
            confirmPasswordTextBox.Text = "";
            emailTextBox.Text = "";
            birthDate.Text = "";
            nameTextBox.Text = "";
            lastNameTextBox.Text = "";

        }

        protected void SignUpClick(object sender, EventArgs e)
        {

            if (!IsAlreadyExistsUser())
            {
                Validate("signUp");
                if (IsValid)
                    using (OleDbConnection conn = new OleDbConnection(Connection.GetConnectionString()))
                    {
                        string query = "INSERT INTO Users VALUES(@user, @password, @email, @birthdate, @name, @lastName)";
                        OleDbCommand command = new OleDbCommand(query, conn);
                        command.Parameters.AddWithValue("@user", userTextBox.Text);
                        command.Parameters.AddWithValue("@password", passwordTextBox.Text);
                        command.Parameters.AddWithValue("@email", emailTextBox.Text);
                        command.Parameters.AddWithValue("@birthdate", birthDate.Text);
                        command.Parameters.AddWithValue("@name", nameTextBox.Text);
                        command.Parameters.AddWithValue("@lastName", lastNameTextBox.Text);
                        conn.Open();
                        int reader = command.ExecuteNonQuery();
                        if (reader > 0)
                        {
                            Response.Write("<script> alert('Successfuly registered');</script>");
                            Response.Redirect("~/Login.aspx");
                        }
                    }
            }
            else
                Response.Write("<script> alert('User already taken');</script>");
        }
        public bool IsAlreadyExistsUser()
        {
            using (OleDbConnection conn = new OleDbConnection(Connection.GetConnectionString()))
            {
                string query = "SELECT UserName FROM Users where userName=@user";
                OleDbCommand command = new OleDbCommand(query, conn);
                command.Parameters.AddWithValue("@user", userTextBox.Text);
                conn.Open();
                OleDbDataReader reader = command.ExecuteReader();
                if (reader.Read())
                    return true;
                return false;
            }
        }
        protected void CheckChars(object source, ServerValidateEventArgs args)
        {
            args.IsValid = args.Value.Length >= 5;

        }
    }
}