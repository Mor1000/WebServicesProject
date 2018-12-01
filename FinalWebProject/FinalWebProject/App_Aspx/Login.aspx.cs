using FinalWebProject.ClassTypes;
using FinalWebProject_App_Services;
using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FinalWebProject.App_Aspx
{
    public partial class Login : System.Web.UI.Page
    {
        UserService us;
        protected void Page_Load(object sender, EventArgs e)
        {

        }   /// <summary>
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

            if (IsValid)
                try
                {
                    us = new UserService(new UserType(userTextBox.Text, passwordTextBox.Text));
                    //The database connection in the using block will be automatically closed in any event.      
                    
                    if (!us.UserLogin())//check if the data was found in the database. 
                        Response.Write("<script>alert('Login failed');</script>");
                    else
                        Response.Write("<script>alert('Login succeeded');</script>");
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

        /// <summary>
        /// switching to the registration page when the sign up button is clicked.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void SignUpClick(object sender, EventArgs e)
        {
            Response.Redirect("~/App_Aspx/Register.aspx");
        }
    }
}