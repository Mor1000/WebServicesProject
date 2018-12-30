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
            if (cardDetails != null)
            {
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
            }
            DataSet ds = null;
            if (count > 0)
            {
                command.CommandText = query;
            }
            else
            {
                command.CommandText = "SELECT* FROM AllMagicCards";
            }
            ds = GetCardsWithRarities(command);
            return ds;
        }
        public DataSet GetCardsWithRarities(OleDbCommand command)
        {
            using (OleDbConnection conn = new OleDbConnection(Connection.GetConnectionString()))
            {
                DataSet ds = new DataSet();
                command.Connection = conn;
                OleDbDataAdapter adapterCards = new OleDbDataAdapter(command);
                DataTable cardsTable = new DataTable("AllMagicCards");
                adapterCards.Fill(cardsTable);
                command = new OleDbCommand("SELECT* FROM Rarities", conn);
                adapterCards = new OleDbDataAdapter(command);
                DataTable raritiesTable = new DataTable("Rarities");
                adapterCards.Fill(raritiesTable);
                ds.Tables.Add(cardsTable);
                ds.Tables.Add(raritiesTable);
                ds.Tables["AllMagicCards"].PrimaryKey = new DataColumn[] { ds.Tables["AllMagicCards"].Columns["cardId"] };
                ds.Tables["Rarities"].PrimaryKey = new DataColumn[] { ds.Tables["Rarities"].Columns["rarityId"] };
                DataRelation cardsRel = new DataRelation("CardsRel", ds.Tables["Rarities"].Columns["rarityId"], ds.Tables["AllMagicCards"].Columns["cardRarity"]);
                ds.Relations.Add(cardsRel);
                ds.Tables["AllMagicCards"].Columns.Add("rarityCardName");

                DataRow temp;
                foreach (DataRow row in ds.Tables["AllMagicCards"].Rows)
                {
                    temp = row.GetParentRow("CardsRel");
                    row["rarityCardName"] = temp["rarityName"];
                }
                command.CommandText = "SELECT* FROM CardsColor";
                AddColorsToDataset(ds, command);
                return ds;
            }

        }
        private void AddColorsToDataset(DataSet ds, OleDbCommand command)
        {
            ds.Tables["AllMagicCards"].Columns.Add("cardColorName");
            DataTable cardColorTable = new DataTable("CardsColor");
            OleDbDataAdapter adapter = new OleDbDataAdapter(command);
            adapter.Fill(cardColorTable);
            ds.Tables.Add(cardColorTable);
            DataTable colorsTable = new DataTable("Colors");
            command.CommandText = "SELECT* FROM Colors";
            adapter = new OleDbDataAdapter(command);
            adapter.Fill(colorsTable);
            ds.Tables.Add(colorsTable);
            ds.Tables["Colors"].PrimaryKey = new DataColumn[] { ds.Tables["Colors"].Columns["colorId"] };
            ds.Tables["CardsColor"].PrimaryKey = new DataColumn[] { ds.Tables["CardsColor"].Columns["cardName"], ds.Tables["CardsColor"].Columns["colorName"] };
            DataColumn cardColorName = ds.Tables["CardsColor"].Columns["colorName"];
            DataColumn colorNum = ds.Tables["Colors"].Columns["colorId"];
            DataRelation colorCardRel = new DataRelation("CardColors", colorNum, cardColorName);
            ds.Relations.Add(colorCardRel);
            foreach (DataRow row in ds.Tables["AllMagicCards"].Rows)
            {
                row["cardColorName"] = FindCardColors(int.Parse(row["cardId"].ToString()), ds);
            }
        }
        public string FindCardColors(int cardId, DataSet ds)
        {

            string colorName = "";
            DataRow temp;
            foreach (DataRow row in ds.Tables["CardsColor"].Rows)
            {
                if (cardId == int.Parse(row["cardName"].ToString()))
                {
                    temp = row.GetParentRow("CardColors");
                    colorName += " " + temp["colorName"].ToString();
                }
            }
            return colorName;
        }

        public bool CardAlreadyExists()
        {
            string query = "SELECT cardName FROM AllMagicCards WHERE cardName=@card_name";
            OleDbCommand command = new OleDbCommand(query);
            command.Parameters.AddWithValue("@card_name", cardDetails.cardName);
            return new GeneralService().nameAlreadyExists(command);
        }
        public int InsertColor(int cardId, int colorId)
        {
            using (OleDbConnection conn = new OleDbConnection(Connection.GetConnectionString()))
            {
                OleDbCommand command = new OleDbCommand();
                command.Connection = conn;
                command.CommandText = "Insert INTO CardsColor VALUES(@card_id,@color_id)";
                command.Parameters.AddWithValue("@card_id", cardId);
                command.Parameters.AddWithValue("@color_id", colorId);
                conn.Open();
                return command.ExecuteNonQuery();
            }
        }
        public int InsertKind(int cardId, int kindId)
        {
            using (OleDbConnection conn = new OleDbConnection(Connection.GetConnectionString()))
            {
                OleDbCommand command = new OleDbCommand();
                command.Connection = conn;
                command.CommandText = "Insert INTO CardsAndKinds VALUES(@card_id,@kind_id)";
                command.Parameters.AddWithValue("@card_id", cardId);
                command.Parameters.AddWithValue("@kind_id", kindId);
                conn.Open();
                return command.ExecuteNonQuery();
            }
        }
        public int LastCardId()
        {
            using (OleDbConnection conn = new OleDbConnection(Connection.GetConnectionString()))
            {
                int last = -1;
                string query = "SELECT MAX(cardId) from AllMagicCards";//This query is parameterized so that the card input will be checked only as one of the fields in the table.
                OleDbCommand command = new OleDbCommand(query, conn);
                conn.Open();
                OleDbDataReader reader = command.ExecuteReader();
                if (reader.Read())
                    last = reader.GetInt32(0);
                return last;
            }
        }


    }
}
