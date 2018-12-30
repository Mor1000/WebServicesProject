using FinalWebProject;
using FinalWebProject.App_Services;
using FinalWebProject.ClassTypes;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Web;

namespace FinalWebProject_App_Services
{
    public class UserService
    {

        public UserType userDetails { get; set; }

        public UserService()
        {

        }
        public UserService(UserType user)
        {
            userDetails = user;
        }
        public bool IsAlreadyExistsUser()
        {
            string query = "SELECT username FROM Users WHERE username=@user_name";
            OleDbCommand command = new OleDbCommand(query);
            command.Parameters.AddWithValue("@user_name", userDetails.username);
            return new GeneralService().nameAlreadyExists(command); 
        }
        public bool IsAlreadyExistsEmail()
        {
            string query = "SELECT userEmail FROM Users WHERE userEmail=@user_email";
            OleDbCommand command = new OleDbCommand(query);
            command.Parameters.AddWithValue("@user_email", userDetails.email);
            return new GeneralService().nameAlreadyExists(command);
        }
        public int SignUp()
        {
            using (OleDbConnection conn = new OleDbConnection(Connection.GetConnectionString()))
            {
                string query = "INSERT INTO Users (userName, userPassword ,userEmail, userBirthdate, userCountry, userMtgArenaName) VALUES(@user_name, @user_password, @user_email, @user_birthdate, @user_country, @user_arena)";//This query is parameterized so that the user input will be checked only as one of the fields in the table.
                OleDbCommand command = new OleDbCommand(query, conn);
                //defining the query's parameters.
                command.Parameters.AddWithValue("@user_name", userDetails.username);
                command.Parameters.AddWithValue("@user_password", userDetails.password);
                command.Parameters.AddWithValue("@user_email", userDetails.email);
                command.Parameters.AddWithValue("@user_birthdate", userDetails.birthdate);
                command.Parameters.AddWithValue("@user_country", userDetails.country);
                command.Parameters.AddWithValue("@user_arena", userDetails.arenaName);
                conn.Open();
                return command.ExecuteNonQuery();//executing the query. the method returns the number of lines inserted.
            }
        }
        public bool UserLogin()
        {
            using (OleDbConnection conn = new OleDbConnection(Connection.GetConnectionString()))
            {

                string query = "SELECT userName, userPassword FROM Users WHERE userName = @user_name and userPassword = @user_password"; ;//This query is parameterized so that the user input will be checked only as one of the fields in the table.
                OleDbCommand command = new OleDbCommand(query, conn);
                //defining the query's parameters.
                command.Parameters.AddWithValue("@user_name", userDetails.username);
                command.Parameters.AddWithValue("@user_password", userDetails.password);
                conn.Open();
                return command.ExecuteReader().Read();//executing the query.
            }
        }
        public DataSet GetAllCountries()
        {
            string query = "SELECT countryId,countryName FROM CountriesList";//This query is parameterized so that the user input will be checked only as one of the fields in the table.
            OleDbCommand command = new OleDbCommand(query);
            return new GeneralService().GetDataset(command, "CountriesList");
        }
    }
}
