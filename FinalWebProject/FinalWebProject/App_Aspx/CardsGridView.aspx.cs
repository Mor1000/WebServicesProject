using FinalWebProject.App_Services;
using FinalWebProject.ClassTypes;
using FinalWebProject_App_Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FinalWebProject.App_Aspx
{
    public partial class CardsGridView : System.Web.UI.Page
    {
        CardsService cardsService = new CardsService();
        DataSet cards = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Utilities.AddRarities(rarityDropDownList);
                ShowCardsTable();
            }
        }
        protected void OnSearch(object sender, EventArgs e)
        {
            int manaCost = -1;
            if (manaCostTextBox.Text.Length > 0)
                manaCost = int.Parse(manaCostTextBox.Text);
            CardType card = new CardType(nameTextBox.Text, abilityTextBox.Text, manaCost, rarityDropDownList.SelectedIndex, "");
            cardsService = new CardsService(card);
            cards = cardsService.GetSelectedCards();
            if (cards != null)
            {
                magicCardsGridView.DataSource = cards;
                magicCardsGridView.DataBind();
            }
        }



       

        protected void OnReset(object sender, EventArgs e)
        {
            ShowCardsTable();
        }
        private void ShowCardsTable()
        {
            cards = cardsService.GetCardsWithRarities();
            magicCardsGridView.DataSource = cards;
            magicCardsGridView.DataBind();
        }

       
    } 
}