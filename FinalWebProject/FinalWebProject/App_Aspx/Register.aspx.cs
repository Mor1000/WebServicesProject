using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FinalWebProject.App_Aspx
{
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
            countryTextBox.Text = "";
            arenaNameTextBox.Text = "";

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
            try
            {
                if (!RegisterService.IsAlreadyExistsUser(userTextBox.Text, emailTextBox.Text))
                {
                    /*loading all the validators from the "signUp" validation group
                     * and checking if they are all valid.*/
                    Validate("signUp");
                    if (IsValid)
                    {
                        ArrayList details = new ArrayList();
                        details.Add(userTextBox.Text);
                        details.Add(passwordTextBox.Text);
                        details.Add(emailTextBox.Text);
                        details.Add(birthDate.Text);
                        details.Add(countryTextBox.Text);
                        details.Add(arenaNameTextBox.Text);

                        if (RegisterService.SignUp(details) > 0)
                        {
                            Response.Write("<script> alert('Successfuly registered');</script>"); //writing in a popup window message.
                            Response.Redirect("~/App_Aspx/Login.aspx"); //switching to the login page.
                        }
                    }
                }
                else
                    Response.Write("<script> alert('User already taken');</script>");
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
        /// This method checked if the username that was entered is already exists
        /// in the users table in the database.
        /// </summary>
        /// <returns>
        /// If the username exists this methot returns true,
        /// otherwise it returns false.
        /// </returns>


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