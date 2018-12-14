using FinalWebProject;
using FinalWebProject.App_Services;
using FinalWebProject.ClassTypes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Web;

namespace FinalWebProject_App_Services
{
    public class CardsService
    {
        CardType cardDetails { get; set; }
        public CardsService()
        {

        }
        public CardsService(CardType card)
        {
            cardDetails = card;
        }
        public int InsertCard()
        {
            using (OleDbConnection conn = new OleDbConnection(Connection.GetConnectionString()))
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
        public DataSet GetAllCards()
        {
            string query = "SELECT cardName,cardId FROM AllMagicCards";//This query is parameterized so that the user input will be checked only as one of the fields in the table.
            OleDbCommand command = new OleDbCommand(query);
            return new GeneralService().GetDataset(command, "AllMagicCards");

        }
        public DataSet GetAllRarities()
        {
            string query = "SELECT rarityName,rarityId FROM Rarities";//This query is parameterized so that the user input will be checked only as one of the fields in the table.
            OleDbCommand command = new OleDbCommand(query);
            return new GeneralService().GetDataset(command, "Rarities");
        }
        public DataSet GetCardsTable()
        {
            string query = "SELECT* FROM AllMagicCards";//This query is parameterized so that the user input will be checked only as one of the fields in the table.
            OleDbCommand command = new OleDbCommand(query);
            return new GeneralService().GetDataset(command, "AllMagicCards");
        }
        public DataSet GetSelectedCards()
        {

            int count = 0;
            string query = "SELECT* FROM AllMagicCards WHERE "; //WHERE cardName like @card_name AND cardAbility like @card_ability AND cardManaCost=@mana_cost AND cardRarity=@card_rarity";
            OleDbCommand command = new OleDbCommand();

            if (cardDetails.cardName != "")
            {
                query += "cardName like @card_name";
                count++;
                command.Parameters.AddWithValue("@card_name", cardDetails.cardName + "%");
            }

            if (cardDetails.cardAbility != "")
            {
                if (count > 0)
                    query += " AND ";
                query += "cardAbility like @card_ability";
                count++;
                command.Parameters.AddWithValue("@card_ability", cardDetails.cardAbility + "%");

            }


            if (cardDetails.cardManaCost != -1)
            {
                if (count > 0)
                    query += " AND ";
                query += "cardManaCost=@mana_cost";
                count++;
                command.Parameters.AddWithValue("@mana_cost", cardDetails.cardManaCost);

            }

            if (cardDetails.cardRarity > 0)
            {

                if (count > 0)
                    query += " AND ";
                query += "cardRarity=@card_rarity";
                count++;
                command.Parameters.AddWithValue("@card_rarity", cardDetails.cardRarity);

            }
            DataSet ds = null;
            if (count > 0)
            {
                command.CommandText = query;
                ds = new GeneralService().GetDataset(command, "AllMagicCards");
            }
            return ds;
        }
    }
}