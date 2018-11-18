using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Web;

namespace FinalWebProject
{
    public class RegisterService
    {
        public static bool IsAlreadyExistsUser(string username,string email)
        {
            OleDbConnection conn = new OleDbConnection(Connection.GetConnectionString());

            string query = "SELECT username, userEmail FROM Users WHERE username=@user OR userEmail=@email";
            OleDbCommand command = new OleDbCommand(query, conn);
            command.Parameters.AddWithValue("@user", username);
            command.Parameters.AddWithValue("@email",email);
            conn.Open();
            OleDbDataReader reader = command.ExecuteReader();
            return reader.Read();
        }
        public static int SignUp(ArrayList userDetails)
        {
            using (OleDbConnection conn = new OleDbConnection(Connection.GetConnectionString()))
            {
                string query = "INSERT INTO Users (userName, userPassword ,userEmail, birthdate, country, mtgArenaName) VALUES(@user, @password, @email, @birthdate, @country, @arena)";//This query is parameterized so that the user input will be checked only as one of the fields in the table.
                OleDbCommand command = new OleDbCommand(query, conn);
                //defining the query's parameters.
                command.Parameters.AddWithValue("@user", userDetails[0]);
                command.Parameters.AddWithValue("@password", userDetails[1]);
                command.Parameters.AddWithValue("@email", userDetails[2]);
                command.Parameters.AddWithValue("@birthdate", userDetails[3]);
                command.Parameters.AddWithValue("@country", userDetails[4]);
                command.Parameters.AddWithValue("@arena", userDetails[5]);
                conn.Open();
                return command.ExecuteNonQuery();//executing the query. the method returns the number of lines inserted.
            }
        }
    }
}