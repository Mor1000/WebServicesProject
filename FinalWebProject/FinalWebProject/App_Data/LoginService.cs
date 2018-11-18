using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Web;

namespace FinalWebProject
{
    public class LoginService
    {public static bool UserLogin(string user, string password)
        {
            using (OleDbConnection conn = new OleDbConnection(Connection.GetConnectionString()))
            {

                string query = "SELECT UserName, UserPassword FROM Users WHERE userName = @user and UserPassword = @password"; ;//This query is parameterized so that the user input will be checked only as one of the fields in the table.
                OleDbCommand command = new OleDbCommand(query, conn);
                //defining the query's parameters.
                command.Parameters.AddWithValue("@user", user);
                command.Parameters.AddWithValue("@password",password);
                conn.Open();
                return command.ExecuteReader().Read();//executing the query.
            }
        }
    }
}