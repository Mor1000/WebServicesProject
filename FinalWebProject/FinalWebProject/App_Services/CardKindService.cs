using FinalWebProject.ClassTypes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Web;

namespace FinalWebProject.App_Services
{
    public class CardKindService
    {
        CardKindType cardKind;
        public CardKindService()
        {

        }
        public CardKindService(CardKindType cardKind)
        {
            this.cardKind = cardKind;
        }
        public int InsertKindCard()
        {
            using (OleDbConnection conn = new OleDbConnection(Connection.GetConnectionString()))
            {
                string query = "INSERT INTO CardsAndKinds VALUES(@card_name, @card_kind)";//This query is parameterized so that the card input will be checked only as one of the fields in the table.
                OleDbCommand command = new OleDbCommand(query, conn);
                //defining the query's parameters.
                command.Parameters.AddWithValue("@card_name", cardKind.cardId);
                command.Parameters.AddWithValue("@card_kind", cardKind.cardKind);
                conn.Open();
                return command.ExecuteNonQuery();//executing the query. the method returns the number of lines inserted.
            }
        }
        public DataSet GetAllKinds()
        {
                string query = "SELECT kindName FROM CardKinds";//This query is parameterized so that the user input will be checked only as one of the fields in the table.
                OleDbCommand command = new OleDbCommand(query);
                return new GeneralService().GetDataset(command, "CardKinds");
        }
    }
}