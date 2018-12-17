using FinalWebProject.ClassTypes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Web;

namespace FinalWebProject.App_Services
{
    public class UserCardService
    {
        public UserCardType userCard { get; set; }
        public UserCardService()
        {

        }
        public UserCardService(UserCardType userCard)
        {
            this.userCard = userCard;
        }
        public int InsertUserCard()
        {
            using (OleDbConnection conn = new OleDbConnection(Connection.GetConnectionString()))
            {
                string query = "INSERT INTO UserCards VALUES(@user_name, @user_card, @card_amount)";//This query is parameterized so that the card input will be checked only as one of the fields in the table.
                OleDbCommand command = new OleDbCommand(query, conn);
                //defining the query's parameters.
                command.Parameters.AddWithValue("@user_name", userCard.userName);
                command.Parameters.AddWithValue("@user_card", userCard.userCard);
                command.Parameters.AddWithValue("@card_amount", userCard.userAmount);
                conn.Open();
                return command.ExecuteNonQuery();//executing the query. the method returns the number of lines inserted.
            }
        }
        public DataSet GetAllUsers()
        {
            string query = "SELECT userName FROM Users";//This query is parameterized so that the user input will be checked only as one of the fields in the table.
            OleDbCommand command = new OleDbCommand(query);
            return new GeneralService().GetDataset(command, "Users");
        }
        public DataSet GetUserCardsTable()
        {
            string query = "SELECT* FROM UserCards";//This query is parameterized so that the user input will be checked only as one of the fields in the table.
            OleDbCommand command = new OleDbCommand(query);
            return new GeneralService().GetDataset(command, "Users");
        }
        public DataSet GetSelectedUserCard()
        {

            int count = 0;
            string query = "SELECT* FROM UserCards WHERE "; 
            OleDbCommand command = new OleDbCommand();

            if (userCard.userName != "")
            {
                query += "userName like @user_name";
                count++;
                command.Parameters.AddWithValue("@user_name", userCard.userName+"%");
            }
            if (userCard.userCard != -1)
            {
                if (count > 0)
                    query += " AND ";
                query += "userCard=@user_card";
                count++;
                command.Parameters.AddWithValue("@user_card", userCard.userCard);
            }
           
            if (userCard.userAmount != -1)
            {
                if (count > 0)
                    query += " AND ";
                query += "cardAmount=@amount";
                count++;
                command.Parameters.AddWithValue("@amount", userCard.userAmount);
            }
            DataSet ds = null;
            if (count > 0)
            {
                command.CommandText = query;
                ds = new GeneralService().GetDataset(command, "UserCards");
            }
            return ds;
        }
    }
}
