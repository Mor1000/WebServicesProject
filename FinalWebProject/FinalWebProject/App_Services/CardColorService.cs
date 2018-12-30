using FinalWebProject.ClassTypes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Web;

namespace FinalWebProject.App_Services
{
    public class CardColorService
    {
        public CardColorType cardColor { get; set; }
        public CardColorService()
        {

        }
        public CardColorService(CardColorType cardColor)
        {
            this.cardColor = cardColor;
        }
        public int InsertColorCard()
        {
            using (OleDbConnection conn = new OleDbConnection(Connection.GetConnectionString()))
            {
                string query = "INSERT INTO CardsColor VALUES(@card_name, @card_color)";//This query is parameterized so that the card input will be checked only as one of the fields in the table.
                OleDbCommand command = new OleDbCommand(query, conn);
                //defining the query's parameters.
                command.Parameters.AddWithValue("@card_name", cardColor.card);
                command.Parameters.AddWithValue("@card_color", cardColor.color);
                conn.Open();
                return command.ExecuteNonQuery();//executing the query. the method returns the number of lines inserted.
            }
        }
        public DataSet GetAllColors()
        {
            string query = "SELECT colorName,colorId FROM Colors";//This query is parameterized so that the user input will be checked only as one of the fields in the table.
            OleDbCommand command = new OleDbCommand(query);
            return new GeneralService().GetDataset(command, "Colors");
        }
        
    }
}