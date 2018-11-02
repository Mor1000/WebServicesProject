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
    /// This is the code behind of the users registration page.
    /// </summary>
    public partial class Register : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// clearing all the input fields.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// A method called when the user clicked the sign up button. If all 
        /// of the input data is valid, the user is registered the login page should be loaded
        ///and the user details are added to the users the users table in the databese.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void SignUpClick(object sender, EventArgs e)
        {

            if (!IsAlreadyExistsUser())
            {
                /*loading all the validators from the "signUp" validation group
                 * and checking if they are all valid.*/
                Validate("signUp"); 
                if (IsValid)   
                    //The database connection in the using block will be automatically closed in any event.      
                    using (OleDbConnection conn = new OleDbConnection(Connection.GetConnectionString()))
                    {
                        string query = "INSERT INTO Users VALUES(@user, @password, @email, @birthdate, @name, @lastName)";//This query is parameterized so that the user input will be checked only as one of the fields in the table.
                        OleDbCommand command = new OleDbCommand(query, conn);
                        //defining the query's parameters.
                        command.Parameters.AddWithValue("@user", userTextBox.Text);
                        command.Parameters.AddWithValue("@password", passwordTextBox.Text);
                        command.Parameters.AddWithValue("@email", emailTextBox.Text);
                        command.Parameters.AddWithValue("@birthdate", birthDate.Text);
                        command.Parameters.AddWithValue("@name", nameTextBox.Text);
                        command.Parameters.AddWithValue("@lastName", lastNameTextBox.Text);
                        conn.Open();
                        int reader = command.ExecuteNonQuery();//executing the query. the method returns the number of lines inserted.
                        if (reader > 0)
                        {
                            Response.Write("<script> alert('Successfuly registered');</script>"); //writing in a popup window message.
                            Response.Redirect("~/Login.aspx"); //switching to the login page.
                        }
                    }
            }
            else
                Response.Write("<script> alert('User already taken');</script>");
        }
        /// <summary>
        /// This method checked if the username that was entered is already exists
        /// in the users table in the database.
        /// </summary>
        /// <returns>
        /// If the username exists this methot returns true,
        /// otherwise it returns false.
        /// </returns>
        public bool IsAlreadyExistsUser()
        {
            using (OleDbConnection conn = new OleDbConnection(Connection.GetConnectionString()))
            {
                string query = "SELECT username, userEmail FROM Users WHERE username=@user OR userEmail=@email";
                OleDbCommand command = new OleDbCommand(query, conn);
                command.Parameters.AddWithValue("@user", userTextBox.Text);
                command.Parameters.AddWithValue("@email", emailTextBox.Text);
                conn.Open();
                OleDbDataReader reader = command.ExecuteReader();
                return reader.Read();
            }
        }

        /// <summary>
        /// This method checks whether the username and the password are valid or not.
        /// </summary>
        /// <param name="source"></param>
        /// <param name="args"></param>
        protected void CheckChars(object source, ServerValidateEventArgs args)
        {
            args.IsValid = args.Value.Length >= 5;

        }
    }
}