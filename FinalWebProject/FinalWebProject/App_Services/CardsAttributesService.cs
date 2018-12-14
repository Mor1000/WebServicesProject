using FinalWebProject.ClassTypes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Web;

namespace FinalWebProject.App_Services
{
    public class CardsAttributesService
    {
        CardAttributeType cardAttribute;
        public CardsAttributesService()
        {

        }
        public CardsAttributesService(CardAttributeType cardAttribute)
        {
            this.cardAttribute = cardAttribute;
        }
        public int InsertAttributeCard()
        {
            using (OleDbConnection conn = new OleDbConnection(Connection.GetConnectionString()))
            {
                string query = "INSERT INTO CardsAttributes VALUES(@card_id, @card_power,@card_toughness)";//This query is parameterized so that the card input will be checked only as one of the fields in the table.
                OleDbCommand command = new OleDbCommand(query, conn);
                //defining the query's parameters.
                command.Parameters.AddWithValue("@card_id", cardAttribute.card);
                command.Parameters.AddWithValue("@card_power", cardAttribute.power);
                command.Parameters.AddWithValue("@card_toughness", cardAttribute.toughness);
                conn.Open();
                return command.ExecuteNonQuery();//executing the query. the method returns the number of lines inserted.
            }
        }
    }
}