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
                magicCardsGridView.DataSource = cards.Tables["AllMagicCards"];
                magicCardsGridView.DataBind();
            }
        }



       

        protected void OnReset(object sender, EventArgs e)
        {
            ShowCardsTable();
        }
        private void ShowCardsTable()
        {
            cards = cardsService.GetSelectedCards();
            magicCardsGridView.DataSource = cards;
            magicCardsGridView.DataBind();
        }

        protected void CardsGridClick(object sender, GridViewCommandEventArgs e)
        {
            magicCardsGridView.Columns[0].Visible = true;
            if (e.CommandName == "showColors")
            {
                object numRow = e.CommandArgument;
                GridViewRow selectedRow = ((GridView)sender).Rows[Convert.ToInt32(numRow)];
                Session["SelectedCardID"] = selectedRow.Cells[0].Text;
                Session["From"] = "~/App_aspx/CardsGridView.aspx";
                Response.Redirect("~/App_aspx/ShowCardColors.aspx");

            }
        }
        protected void OnColorBound(object sender, EventArgs e)
        {
            try
            {
                foreach (ListItem item in colorsList.Items)
                {
                    item.Text = string.Format("<img src= \"{0}\" width=35px; height=35px /> {1}", this.GetImageURL(item.Text), item.Text);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message, ": " + ex.StackTrace);
            }
        }
        private string GetImageURL(string color)
        {
            return string.Format("/ColorsImage/{0}.png", color);
        }
    } 
}