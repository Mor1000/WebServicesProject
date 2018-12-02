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
        public DataSet GetAllFormats()
        {
            using (OleDbConnection conn = new OleDbConnection(Connection.GetConnectionString()))
            {

                string query = "SELECT formatName FROM Formats";//This query is parameterized so that the user input will be checked only as one of the fields in the table.
                OleDbCommand command = new OleDbCommand(query, conn);
                conn.Open();
                DataSet ds = new DataSet();
                OleDbDataAdapter dataAdapter = new OleDbDataAdapter(command);
                DataTable usersTable = new DataTable("Formats");
                dataAdapter.Fill(usersTable);
                ds.Tables.Add(usersTable);
                return ds;
            }
        }
    }
}