using FinalWebProject.ClassTypes;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Web;

namespace FinalWebProject
{
    public class UserService
    {
        public bool IsAlreadyExistsUser(string username, string email)
        {
            OleDbConnection conn = new OleDbConnection(Connection.GetConnectionString());

            string query = "SELECT username, userEmail FROM Users WHERE username=@user OR userEmail=@email";
            OleDbCommand command = new OleDbCommand(query, conn);
            command.Parameters.AddWithValue("@user", username);
            command.Parameters.AddWithValue("@email", email);
            conn.Open();
            OleDbDataReader reader = command.ExecuteReader();
            return reader.Read();
        }
        public int SignUp(UserType userDetails)
        {
            using (OleDbConnection conn = new OleDbConnection(Connection.GetConnectionString()))
            {
                string query = "INSERT INTO Users (userName, userPassword ,userEmail, birthdate, country, mtgArenaName) VALUES(@user, @password, @email, @birthdate, @country, @arena)";//This query is parameterized so that the user input will be checked only as one of the fields in the table.
                OleDbCommand command = new OleDbCommand(query, conn);
                //defining the query's parameters.
                command.Parameters.AddWithValue("@user", userDetails.username);
                command.Parameters.AddWithValue("@password", userDetails.password);
                command.Parameters.AddWithValue("@email", userDetails.email);
                command.Parameters.AddWithValue("@birthdate", userDetails.birthdate);
                command.Parameters.AddWithValue("@country", userDetails.country);
                command.Parameters.AddWithValue("@arena", userDetails.arenaName);
                conn.Open();
                return command.ExecuteNonQuery();//executing the query. the method returns the number of lines inserted.
            }
        }
        public bool UserLogin(string user, string password)
        {
            using (OleDbConnection conn = new OleDbConnection(Connection.GetConnectionString()))
            {

                string query = "SELECT UserName, UserPassword FROM Users WHERE userName = @user and UserPassword = @password"; ;//This query is parameterized so that the user input will be checked only as one of the fields in the table.
                OleDbCommand command = new OleDbCommand(query, conn);
                //defining the query's parameters.
                command.Parameters.AddWithValue("@user", user);
                command.Parameters.AddWithValue("@password", password);
                conn.Open();
                return command.ExecuteReader().Read();//executing the query.
            }
        }
       

    }
}
