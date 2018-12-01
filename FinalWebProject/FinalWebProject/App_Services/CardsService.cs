using FinalWebProject;
using FinalWebProject.ClassTypes;
using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Web;

namespace FinalWebProject_App_Services
{
    public class CardsService
    {
        string CS = (new Connection()).GetConnectionString();
        CardType cardDetails;
        public CardsService(CardType card)
        {
            cardDetails = card;
        }
        public int InsertCard()
        {
            using (OleDbConnection conn = new OleDbConnection(CS))
            {
                string query = "INSERT INTO AllMagicCards (cardName, cardAbility ,cardManaCost, cardRarity, cardImage) VALUES(@card_name, @ability, @mana_cost, @rarity, @card_image)";//This query is parameterized so that the card input will be checked only as one of the fields in the table.
                OleDbCommand command = new OleDbCommand(query, conn);
                //defining the query's parameters.
                command.Parameters.AddWithValue("@card_name", cardDetails.cardName);
                command.Parameters.AddWithValue("@ability", cardDetails.cardAbility);
                command.Parameters.AddWithValue("@mana_cost", cardDetails.cardManaCost);
                command.Parameters.AddWithValue("@rarity", cardDetails.cardRarity);
                command.Parameters.AddWithValue("@card_image", cardDetails.cardImage);
                conn.Open();
                return command.ExecuteNonQuery();//executing the query. the method returns the number of lines inserted.
            }
        }
    }
}