using FinalWebProject.App_Services;
using FinalWebProject.ClassTypes;
using FinalWebProject_App_Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FinalWebProject.App_Aspx
{
    public partial class DecksGridView : System.Web.UI.Page
    {
        DecksService deckService = new DecksService();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadDropDpwnLists();
                ShowDecksTable();

            }
        }
        public void LoadDropDpwnLists()
        {
            try
            {
                Utilities.AddToDropDownList(deckService.GetAllDeckNames(), nameDropDownList, "Decks", "deckName", "deckId");
                Utilities.AddToDropDownList(deckService.GetAllFormats(), formatDropDownList, "Formats", "formatName", "formatId");

            }
            catch (Exception ex)
            {
            }
        }

        protected void OnSearch(object sender, EventArgs e)
        {
            int formatId = -1;
            if (formatDropDownList.SelectedValue != "None")
                formatId = int.Parse(formatDropDownList.SelectedValue);
                DeckType deck = new DeckType(nameDropDownList.SelectedItem.Text,formatId, "","");
            DecksService service = new DecksService(deck);
            DataSet deckSelected = service.GetSelectedDecks(minDateTextBox.Text, maxDateTextBox.Text);
            if (deckSelected != null)
            {
                deckGridView.DataSource = deckSelected;
                deckGridView.DataBind();
            }
        }

        protected void OnReset(object sender, EventArgs e)
        {
            ShowDecksTable();

        }
        private void ShowDecksTable()
        {
            deckGridView.DataSource = deckService.GetDecksTable();
            deckGridView.DataBind();
        }
    }
}