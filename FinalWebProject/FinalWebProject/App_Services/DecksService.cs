using FinalWebProject.ClassTypes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Web;

namespace FinalWebProject.App_Services
{
    public class DecksService
    {
        DeckType deckDetails { get; set; }
        public DecksService()
        {

        }
        public DecksService(DeckType deck)
        {
            deckDetails = deck;
        }
        public int InsertDeck()
        {
            using (OleDbConnection conn = new OleDbConnection(Connection.GetConnectionString()))
            {
                string query = "INSERT INTO Decks (deckName, deckFormat ,deckCreationDate, deckDescription) VALUES(@deck_name, @deck_format, @creation_date, @deck_description)";//This query is parameterized so that the card input will be checked only as one of the fields in the table.
                OleDbCommand command = new OleDbCommand(query, conn);
                //defining the query's parameters.
                command.Parameters.AddWithValue("@deck_name", deckDetails.deckName);
                command.Parameters.AddWithValue("@deck_format", deckDetails.deckFormat);
                command.Parameters.AddWithValue("@creation_date", deckDetails.deckCreationDate);
                command.Parameters.AddWithValue("@deck_description", deckDetails.deckDescription);
                conn.Open();
                return command.ExecuteNonQuery();//executing the query. the method returns the number of lines inserted.
            }
        }
        public DataSet GetDecksTable()
        {
            string query = "SELECT* FROM Decks";//This query is parameterized so that the user input will be checked only as one of the fields in the table.
            OleDbCommand command = new OleDbCommand(query);
            return new GeneralService().GetDataset(command, "Decks");
        }
        public DataSet GetAllFormats()
        {
            string query = "SELECT formatId,formatName FROM Formats";//This query is parameterized so that the user input will be checked only as one of the fields in the table.
            OleDbCommand command = new OleDbCommand(query);
            return new GeneralService().GetDataset(command, "Formats");
        }
        public DataSet GetAllDeckNames()
        {
            string query = "SELECT deckId,deckName FROM Decks";//This query is parameterized so that the user input will be checked only as one of the fields in the table.
            OleDbCommand command = new OleDbCommand(query);
            return new GeneralService().GetDataset(command, "Decks");
        }
        public DataSet GetSelectedDecks(string minDate, string maxDate)
        {

            int count = 0;
            string query = "SELECT* FROM Decks WHERE "; //WHERE cardName like @card_name AND cardAbility like @card_ability AND cardManaCost=@mana_cost AND cardRarity=@card_rarity";
            OleDbCommand command = new OleDbCommand();

            if (deckDetails.deckName != "None")
            {
                query += "deckName=@deck_name";
                count++;
                command.Parameters.AddWithValue("@deck_name", deckDetails.deckName);
            }
            if (deckDetails.deckFormat != -1)
            {
                if (count > 0)
                    query += " AND ";
                query += "deckFormat=@deck_format";
                count++;
                command.Parameters.AddWithValue("@deck_format", deckDetails.deckFormat);
            }
            if (maxDate != "" && minDate != "")
            {
                if (count > 0)
                    query += " AND ";
                query += "deckCreationDate between @min_date And @max_date";
                count++;
                command.Parameters.AddWithValue("@min_date", minDate);
                command.Parameters.AddWithValue("@max_date", maxDate);

            }
            else if (maxDate != "")
            {
                if (count > 0)
                    query += " AND ";
                query += "deckCreationDate<=@max_date";
                count++;
                command.Parameters.AddWithValue("@max_date", maxDate);
            }
            else if (minDate != "")
            {
                if (count > 0)
                    query += " AND ";
                query += "deckCreationDate>=@min_date";
                count++;
                command.Parameters.AddWithValue("@min_date", minDate);
            }

         
            DataSet ds = null;
            if (count > 0)
            {
                command.CommandText = query;
                ds = new GeneralService().GetDataset(command, "Decks");
            }
            return ds;
        }
        public bool deckAlreadyExists()
        {
            string query = "SELECT deckName FROM Decks WHERE deckName=@deck_name";
            OleDbCommand command = new OleDbCommand(query);
            command.Parameters.AddWithValue("@deck_name", deckDetails.deckName);
            return new GeneralService().nameAlreadyExists(command);
        }
    }
}