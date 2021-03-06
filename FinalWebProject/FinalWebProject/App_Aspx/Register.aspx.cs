﻿using FinalWebProject.ClassTypes;
using FinalWebProject_App_Services;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
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
        UserService us;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                AddCountries();
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
            birthDateTextBox.Text = "";
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
                /*loading all the validators from the "signUp" validation group
                 * and checking if they are all valid.*/
                if (IsValid)
                {
                    UserType user = new UserType(userTextBox.Text, passwordTextBox.Text, emailTextBox.Text, birthDateTextBox.Text, int.Parse(countriesDropDownList.SelectedValue), false, arenaNameTextBox.Text);
                    us = new UserService(user);
                    if (us.IsAlreadyExistsUser())
                        Response.Write("<script> alert('User already taken');</script>");
                    else if (us.IsAlreadyExistsEmail())
                        Response.Write("<script> alert('Email already taken');</script>");

                    else if (us.SignUp() > 0)
                    {
                        Response.Write("<script> alert('Successfuly registered');</script>"); //writing in a popup window message.
                        Response.Redirect("~/App_Aspx/Login.aspx"); //switching to the login page.
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

        /// <summary>
        /// This method checks whether the username and the password are valid or not.
        /// </summary>
        /// <param name="source"></param>
        /// <param name="args"></param>
        protected void CheckChars(object source, ServerValidateEventArgs args)
        {
            args.IsValid = args.Value.Length >= 5;

        }

        private void AddCountries()
        {
            try
            {
                UserService userServices = new UserService();
                DataSet usersDataSet = userServices.GetAllCountries();
                Utilities.AddToDropDownList(usersDataSet, countriesDropDownList, "CountriesList", "countryName", "countryId");
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.StackTrace);
            }
        }

    }
}