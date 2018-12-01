using FinalWebProject;
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
        string CS = (new Connection()).GetConnectionString();

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
            OleDbConnection conn = new OleDbConnection(CS);

            string query = "SELECT username, userEmail FROM Users WHERE username=@user_name OR userEmail=@email";
            OleDbCommand command = new OleDbCommand(query, conn);
            command.Parameters.AddWithValue("@user_name", userDetails.username);
            command.Parameters.AddWithValue("@user_email", userDetails.email);
            conn.Open();
            OleDbDataReader reader = command.ExecuteReader();
            return reader.Read();
        }
        public int SignUp()
        {
            using (OleDbConnection conn = new OleDbConnection(CS))
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
            using (OleDbConnection conn = new OleDbConnection(CS))
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
            using (OleDbConnection conn = new OleDbConnection(CS))
            {

                string query = "SELECT countryName FROM CountriesList";//This query is parameterized so that the user input will be checked only as one of the fields in the table.
                OleDbCommand command = new OleDbCommand(query, conn);
                conn.Open();
                DataSet ds = new DataSet();
                OleDbDataAdapter dataAdapter = new OleDbDataAdapter(command);
                DataTable usersTable = new DataTable("CountriesList");
                dataAdapter.Fill(usersTable);
                ds.Tables.Add(usersTable);
                return ds;
            }
        }
    }
}
