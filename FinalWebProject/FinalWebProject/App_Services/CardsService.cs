using FinalWebProject;
using FinalWebProject.App_Services;
using FinalWebProject.ClassTypes;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

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
            string query = "SELECT* FROM AllMagicCards";//This query is parameterized so that the user input will be checked only as one of the fields in the table.
            OleDbCommand command = new OleDbCommand(query);
            return GetCardsWithRarities(command, null, null);

        }
        public int DeleteCardsColor(int cardId, int cardColorId)
        {
            using (OleDbConnection conn = new OleDbConnection(Connection.GetConnectionString()))
            {
                OleDbCommand command = new OleDbCommand();
                command.Connection = conn;
                string query;
                if (cardColorId == -1)
                    query = "DELETE FROM CardsColor WHERE cardName=@selected_card";//This query is parameterized so that the user input will be checked only as one of the fields in the table.
                else
                    query = "DELETE FROM CardsColor WHERE cardName=@selected_card AND colorName=@selected_color";
                command.CommandText = query;
                command.Parameters.AddWithValue("@selected_card", cardId);
                if (cardColorId != -1)
                    command.Parameters.AddWithValue("@selected_color", cardColorId);
                conn.Open();
                return command.ExecuteNonQuery();
            }
        }
        public int DeleteCardsKinds(int cardId, int cardKindId)
        {
            using (OleDbConnection conn = new OleDbConnection(Connection.GetConnectionString()))
            {
                OleDbCommand command = new OleDbCommand();
                command.Connection = conn;
                string query;
                if (cardKindId == -1)
                    query = "DELETE FROM CardsAndKinds WHERE cardName=@selected_card";//This query is parameterized so that the user input will be checked only as one of the fields in the table.
                else
                    query = "DELETE FROM CardsAndKinds WHERE cardName=@selected_card AND kindName=@selected_kind";
                command.CommandText = query;
                command.Parameters.AddWithValue("@selected_card", cardId);
                if (cardKindId != -1)
                    command.Parameters.AddWithValue("@selected_kind", cardKindId);
                conn.Open();
                return command.ExecuteNonQuery();
            }
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
        public int InsertAttributes(int cardId, int power, int toughness)
        {
            using (OleDbConnection conn = new OleDbConnection(Connection.GetConnectionString()))
            {
                OleDbCommand command = new OleDbCommand();
                command.Connection = conn;
                command.CommandText = "Insert INTO CardsAttributes VALUES(@card_id,@card_power,@card_toughness)";
                command.Parameters.AddWithValue("@card_id", cardId);
                command.Parameters.AddWithValue("@card_power", power);
                command.Parameters.AddWithValue("@card_toughness", toughness);
                conn.Open();
                return command.ExecuteNonQuery();
            }
        }
        public DataSet GetSelectedCards(ArrayList colors, ArrayList kinds)
        {
            int count = 0;
            string query = "SELECT AllMagicCards.cardId, AllMagicCards.cardName, AllMagicCards.cardAbility, AllMagicCards.cardManaCost, AllMagicCards.cardRarity, AllMagicCards.cardImage FROM ";//"SELECT* FROM AllMagicCards WHERE "; 
            bool colorSelected = colors != null && colors.Count > 0;
            bool kindSelected = kinds != null && kinds.Count > 0;
            if (colorSelected && kindSelected)
                query += "((";
            else if (colorSelected || kindSelected)
                query += "(";
            query += "AllMagicCards ";
            if (colorSelected)
            {
                query += "INNER JOIN CardsColor ON CardsColor.cardName=AllMagicCards.cardId)";
            }
            if (kindSelected)
                query += "INNER JOIN CardsAndKinds ON CardsAndKinds.cardName = AllMagicCards.cardId)";
            query += " WHERE ";
            OleDbCommand command = new OleDbCommand();
            if (cardDetails != null)
            {
                if (cardDetails.cardName != "")
                {
                    query += "AllMagicCards.cardName like @card_name";
                    count++;
                    command.Parameters.AddWithValue("@card_name", cardDetails.cardName + "%");
                }

                if (cardDetails.cardAbility != "")
                {
                    if (count > 0)
                        query += " AND ";
                    query += "AllMagicCards.cardAbility like @card_ability";
                    count++;
                    command.Parameters.AddWithValue("@card_ability", cardDetails.cardAbility + "%");

                }


                if (cardDetails.cardManaCost != -1)
                {
                    if (count > 0)
                        query += " AND ";
                    query += "AllMagicCards.cardManaCost=@mana_cost";
                    count++;
                    command.Parameters.AddWithValue("@mana_cost", cardDetails.cardManaCost);

                }

                if (cardDetails.cardRarity > 0)
                {

                    if (count > 0)
                        query += " AND ";
                    query += "AllMagicCards.cardRarity=@card_rarity";
                    count++;
                    command.Parameters.AddWithValue("@card_rarity", cardDetails.cardRarity);
                }
                if (colors.Count > 0)
                {
                    if (count > 0)
                    {
                        query += " AND ";
                        count++;
                    }
                    query += "(";
                    for (int i = 0; i < colors.Count; i++)
                    {
                        if (i > 0)
                            query += " OR ";
                        query += "CardsColor.colorName=@card_color" + i + "";
                        command.Parameters.AddWithValue("@card_color" + i + "", colors[i]);
                    }
                        query += ")";
                    if(count==0)
                        count++;
                }
                if (kinds.Count > 0)
                {
                    if (count > 0)
                    {
                        query += " AND (";
                        count++;
                    }
                    for (int i = 0; i < kinds.Count; i++)
                    {
                        if (i > 0)
                            query += " OR ";
                        query += "CardsAndKinds.kindName=@card_kind" + i + "";
                        command.Parameters.AddWithValue("@card_kind" + i + "", kinds[i]);
                    }
                    if (count > 0)
                        query += ")";
                    else
                        count++;
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
            ds = GetCardsWithRarities(command, colors, kinds);
            return ds;
        }
        public void FilterColors(DataTable cardsTable, int colorsCount,int kindsCount)
        {
            DataView dv = new DataView(cardsTable);
            string s1;
            int i = 0;
            while (i < cardsTable.Rows.Count)
            {
                s1 = "cardId=" + int.Parse(cardsTable.Rows[i]["cardId"].ToString());
                dv.RowFilter = s1;
                int delta = 0;
                if (colorsCount+kindsCount<=dv.Count)
                {
                    delta =1;
                }

                for (int j = i; j < dv.Count - delta + i;)
                {
                    cardsTable.Rows.Remove(cardsTable.Rows[j]);
                }
                if (delta == 1)
                    i++;
            }

        }
        public void FilterKinds(DataTable cardsTable, int kindsCount)
        {
            DataView dv = new DataView(cardsTable);
            string s1;
            int i = 0;
            while (i < cardsTable.Rows.Count)
            {
                s1 = "cardId=" + int.Parse(cardsTable.Rows[i]["cardId"].ToString());
                dv.RowFilter = s1;
                int delta = 1;
                if (kindsCount != dv.Count)
                {
                    delta = 0;
                }

                for (int j = i; j < dv.Count - delta + i; j++)
                {
                    cardsTable.Rows.Remove(cardsTable.Rows[j]);
                }
                if (delta == 1)
                    i++;
            }

        }

        public DataSet GetCardsWithRarities(OleDbCommand command, ArrayList colors, ArrayList kinds)
        {
            using (OleDbConnection conn = new OleDbConnection(Connection.GetConnectionString()))
            {
                DataSet ds = new DataSet();
                command.Connection = conn;
                OleDbDataAdapter adapterCards = new OleDbDataAdapter(command);

                DataTable cardsTable = new DataTable("AllMagicCards");

                adapterCards.Fill(cardsTable);
                DataTable copyTable = cardsTable.Copy();
                //dv.Sort = "cardId";
                //cardsTable= dv.ToTable();

                if ((colors != null && colors.Count > 0)||((kinds != null && kinds.Count > 0)))
                {
               
                    FilterColors(cardsTable, colors.Count,kinds.Count);
                }
                //if (kinds != null && kinds.Count > 0)
                //{
                //    hasKinds = true;
                //    FilterKinds(copyTable, kinds.Count);
                //}

                //if (hasColors && hasKinds)
                //{
                //    cardsTable.PrimaryKey = new DataColumn[] { cardsTable.Columns["cardId"] };
                //    copyTable.PrimaryKey = new DataColumn[] { copyTable.Columns["cardId"] };
                //    foreach (DataRow row in cardsTable.Rows)
                //    {
                //        //int cardId = int.Parse(row["cardId"].ToString());
                //        if (copyTable.Rows.Find(Convert.ToInt32(row["cardId"])) == null)
                //            cardsTable.Rows.Remove(row);
                //    }
                //    foreach (DataRow row in cardsTable.Rows)
                //    {
                //        if (copyTable.Rows.Find(Convert.ToInt32(row["cardId"])) == null)
                //            cardsTable.Rows.Remove(row);
                //    }
                //}
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
                //    AddColorsToDataset(ds, command);
                return ds;
            }

        }
        public int DeleteRow(int cardId)
        {
            using (OleDbConnection conn = new OleDbConnection(Connection.GetConnectionString()))
            {
                string query = "SELECT* FROM CardsColor WHERE cardName=@card_id";
                OleDbCommand command = new OleDbCommand(query, conn);
                command.Parameters.AddWithValue("@card_id", cardId);
                GeneralService service = new GeneralService();
                int rowUpdated = 0;
                if (service.nameAlreadyExists(command))
                {
                    //      command.Connection = conn;
                    rowUpdated = -1;
                    command = new OleDbCommand("DELETE FROM CardsColor WHERE cardName=@card_id", conn);
                    command.Parameters.AddWithValue("@card_id", cardId);
                    conn.Open();
                    rowUpdated = command.ExecuteNonQuery();
                }
                if (rowUpdated != -1)
                {
                    //  command.Connection = conn;
                    query = "DELETE FROM AllMagicCards WHERE cardId=@card_id";
                    if (rowUpdated == 0)
                    {
                        command = new OleDbCommand(query, conn);
                        command.Parameters.AddWithValue("@card_id", cardId);
                        conn.Open();
                    }
                    else
                        command.CommandText = query;
                    rowUpdated = command.ExecuteNonQuery();
                }
                return rowUpdated;
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
            //foreach (DataRow row in ds.Tables["AllMagicCards"].Rows)
            //{
            //    row["cardColorName"] = FindCardColors(int.Parse(row["cardId"].ToString()), ds);
            //}
        }
        public DataTable FindCardColors(int cardId)
        {
            using (OleDbConnection conn = new OleDbConnection(Connection.GetConnectionString()))
            {
                OleDbCommand command = new OleDbCommand("SELECT* FROM cardsColor WHERE cardName=@selected_card");
                command.Parameters.AddWithValue("@selected_card", cardId);
                DataSet ds = new DataSet();
                command.Connection = conn;
                OleDbDataAdapter adapterCards = new OleDbDataAdapter(command);
                DataTable cardsColorTable = new DataTable("CardsColor");
                adapterCards.Fill(cardsColorTable);
                command = new OleDbCommand("SELECT* FROM Colors", conn);
                adapterCards = new OleDbDataAdapter(command);
                DataTable colorsTable = new DataTable("Colors");
                adapterCards.Fill(colorsTable);
                ds.Tables.Add(cardsColorTable);
                ds.Tables.Add(colorsTable);
                ds.Tables["CardsColor"].PrimaryKey = new DataColumn[] { ds.Tables["CardsColor"].Columns["cardName"], ds.Tables["CardsColor"].Columns["colorName"] };
                ds.Tables["Colors"].PrimaryKey = new DataColumn[] { ds.Tables["Colors"].Columns["colorId"] };
                DataRelation cardsRel = new DataRelation("CardsColorsRel", ds.Tables["Colors"].Columns["colorId"], ds.Tables["CardsColor"].Columns["colorName"]);
                ds.Relations.Add(cardsRel);
                ds.Tables["CardsColor"].Columns.Add("colorCardName");

                DataRow temp;
                foreach (DataRow row in ds.Tables["CardsColor"].Rows)
                {
                    temp = row.GetParentRow("CardsColorsRel");
                    row["colorCardName"] = temp["colorName"];
                }

                return ds.Tables["CardsColor"];
            }
        }
        public DataTable FindCardKindes(int cardId)
        {
            using (OleDbConnection conn = new OleDbConnection(Connection.GetConnectionString()))
            {
                OleDbCommand command = new OleDbCommand("SELECT* FROM CardsAndKinds WHERE cardName=@selected_card");
                command.Parameters.AddWithValue("@selected_card", cardId);
                DataSet ds = new DataSet();
                command.Connection = conn;
                OleDbDataAdapter adapterCards = new OleDbDataAdapter(command);
                DataTable cardskindsTable = new DataTable("CardsAndKinds");
                adapterCards.Fill(cardskindsTable);
                command = new OleDbCommand("SELECT* FROM CardKinds", conn);
                adapterCards = new OleDbDataAdapter(command);
                DataTable colorsTable = new DataTable("CardKinds");
                adapterCards.Fill(colorsTable);
                ds.Tables.Add(cardskindsTable);
                ds.Tables.Add(colorsTable);
                ds.Tables["CardsAndKinds"].PrimaryKey = new DataColumn[] { ds.Tables["CardsAndKinds"].Columns["cardName"], ds.Tables["CardsAndKinds"].Columns["kindName"] };
                ds.Tables["CardKinds"].PrimaryKey = new DataColumn[] { ds.Tables["CardKinds"].Columns["cardName"] };
                DataRelation cardsRel = new DataRelation("CardsKindsRel", ds.Tables["CardKinds"].Columns["kindId"], ds.Tables["CardsAndKinds"].Columns["kindName"]);
                ds.Relations.Add(cardsRel);
                ds.Tables["CardsAndKinds"].Columns.Add("kindCardName");

                DataRow temp;
                foreach (DataRow row in ds.Tables["CardsAndKinds"].Rows)
                {
                    temp = row.GetParentRow("CardsKindsRel");
                    row["kindCardName"] = temp["kindName"];
                }

                return ds.Tables["CardsAndKinds"];
            }
        }

        public bool CardAlreadyExists()
        {
            string query = "SELECT cardName FROM AllMagicCards WHERE cardName=@card_name";
            OleDbCommand command = new OleDbCommand(query);
            command.Parameters.AddWithValue("@card_name", cardDetails.cardName);
            return new GeneralService().nameAlreadyExists(command);
        }
        public bool CardAlreadyExists(int cardId, string cardName)
        {
            string query = "SELECT cardId, cardName FROM AllMagicCards WHERE cardId<>@card_id AND cardName=@card_name";
            OleDbCommand command = new OleDbCommand(query);
            command.Parameters.AddWithValue("@card_id", cardId);
            command.Parameters.AddWithValue("@card_name", cardName);
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
        public int[] GetCreatureAttributes(int cardId)
        {
            OleDbCommand command = new OleDbCommand();
            command.CommandText = "SELECT* FROM CardsAttributes WHERE cardID=@card_id";
            command.Parameters.AddWithValue("@card_id", cardId);
            DataSet ds = new GeneralService().GetDataset(command, "CardAttributes");
            if (ds.Tables["CardAttributes"].Rows.Count > 0)
            {
                int power = int.Parse(ds.Tables["CardAttributes"].Rows[0]["power"].ToString());
                int toughness = int.Parse(ds.Tables["CardAttributes"].Rows[0]["toughness"].ToString());
                return new int[] { power, toughness };
            }
            return null;
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
        public int UpdateTableRow(int cardId)
        {
            using (OleDbConnection conn = new OleDbConnection(Connection.GetConnectionString()))
            {
                OleDbCommand command = new OleDbCommand();
                command.Connection = conn;
                string query = "UPDATE AllMagicCards SET cardName=@card_name, cardAbility=@card_ability, cardManaCost=@mana_cost, cardRarity=@card_rarity, ";//, cardImage=@card_image WHERE cardId=@card_id";
                command.Parameters.AddWithValue("@card_name", cardDetails.cardName);
                command.Parameters.AddWithValue("@card_ability", cardDetails.cardAbility);
                command.Parameters.AddWithValue("@mana_cost", cardDetails.cardManaCost);
                command.Parameters.AddWithValue("@card_rarity", cardDetails.cardRarity);
                if (cardDetails.cardImage != "")
                {
                    query += "cardImage=@card_image ";
                    command.Parameters.AddWithValue("@card_image", cardDetails.cardImage);

                }
                query += "WHERE cardId=@card_id";
                command.Parameters.AddWithValue("@card_id", cardId);
                command.CommandText = query;
                conn.Open();
                return command.ExecuteNonQuery();
            }
        }
        public int UpdateCardAttributes(int cardId, int power, int toughness)
        {
            using (OleDbConnection conn = new OleDbConnection(Connection.GetConnectionString()))
            {
                string query = "UPDATE CardsAttributes SET power=@card_power, toughness=@card_toughness WHERE cardId=@card_id";
                OleDbCommand command = new OleDbCommand(query, conn);

                command.Parameters.AddWithValue("@card_power", power);
                command.Parameters.AddWithValue("@card_toughness", toughness);
                command.Parameters.AddWithValue("@card_id", cardId);
                conn.Open();
                return command.ExecuteNonQuery();
            }
        }
        public int DeleteCardAttributes(int cardId)
        {
            using (OleDbConnection conn = new OleDbConnection(Connection.GetConnectionString()))
            {
                string query = "DELETE FROM CardsAttributes WHERE cardId=@card_id";
                OleDbCommand command = new OleDbCommand(query, conn);
                command.Parameters.AddWithValue("@card_id", cardId);
                conn.Open();
                return command.ExecuteNonQuery();
            }
        }
        public int UnAbleCard(int cardId)
        {
            using (OleDbConnection conn = new OleDbConnection(Connection.GetConnectionString()))
            {
                string query = "UPDATE AllMagicCards SET notValid=true WHERE cardId=@card_id";
                OleDbCommand command = new OleDbCommand(query, conn);
                command.Parameters.AddWithValue("@card_id", cardId);
                conn.Open();
                int rowUpdated = command.ExecuteNonQuery();
                return rowUpdated;
            }
        }
    }
}
