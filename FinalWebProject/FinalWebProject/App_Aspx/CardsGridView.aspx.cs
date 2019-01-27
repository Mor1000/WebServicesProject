using FinalWebProject.App_Services;
using FinalWebProject.ClassTypes;
using FinalWebProject_App_Services;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FinalWebProject.App_Aspx
{
    public partial class CardsGridView : System.Web.UI.Page
    {
        int index = 0;
        CardsService cardsService = new CardsService();
        DataSet cards = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                AddRarities(rarityDropDownList);
                ShowCardsTable();
            }
        }
        protected void OnSearch(object sender, EventArgs e)
        {
            int manaCost = -1;
            if (manaCostTextBox.Text.Length > 0)
                manaCost = int.Parse(manaCostTextBox.Text);
            CardType card = new CardType(nameTextBox.Text, abilityTextBox.Text, manaCost, rarityDropDownList.SelectedIndex, "");
            ArrayList colors = new ArrayList();
            foreach (ListItem item in colorsList.Items)
            {
                if (item.Selected)
                    colors.Add(item.Value);
            }
            ArrayList kinds = new ArrayList();
            foreach (ListItem item in kindsList.Items)
            {
                if (item.Selected)
                    kinds.Add(item.Value);
            }
            cardsService = new CardsService(card);
            cards = cardsService.GetSelectedCards(colors, kinds);
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
            cards = cardsService.GetAllCards();
            Cache["Cards"] = cards;
            magicCardsGridView.DataSource = cards;
            magicCardsGridView.DataBind();
        }

        protected void CardsGridClick(object sender, GridViewCommandEventArgs e)
        {
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

        protected void OnEditMode(object sender, GridViewEditEventArgs e)
        {
            magicCardsGridView.EditIndex = e.NewEditIndex;
            magicCardsGridView.DataSource = (DataSet)Cache["Cards"];
            magicCardsGridView.DataBind();
            //       ((DropDownList)(magicCardsGridView.FindControl("DropDownList1"))).Items.Add("test");
            //  AddRarities(((DropDownList)(magicCardsGridView.Rows[e.NewEditIndex].Cells[4].Controls[0])));
        }

        protected void OnCancelingEditing(object sender, GridViewCancelEditEventArgs e)
        {
            magicCardsGridView.EditIndex = -1;
            magicCardsGridView.DataSource = (DataSet)Cache["Cards"];
            magicCardsGridView.DataBind();
        }

        protected void OnRowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            DataSet cardsDS = (DataSet)Cache["Cards"];
            int cardId = int.Parse(magicCardsGridView.Rows[e.RowIndex].Cells[0].Text);
            DataRow row = cardsDS.Tables["AllMagicCards"].Rows.Find(cardId);
            bool cardInDeck = new DecksService().cardAlreadyInDeck(cardId);
            bool userAddedCard = new UserCardService().userAlreadyAddedCard(cardId);
            if (!cardInDeck && !userAddedCard)
            {
                if (cardsService.DeleteRow(cardId) > 0)
                    row.Delete();
                else
                    Response.Write("<script>alert('Error Deleting');</script>");
            }
            else
                cardsService.UnAbleCard(cardId);

            magicCardsGridView.EditIndex = -1;
            magicCardsGridView.DataSource = (DataSet)Cache["Cards"];
            magicCardsGridView.DataBind();
        }

        protected void OnRowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            DataSet cardsDS = (DataSet)Cache["Cards"];
            int selectedIndex = e.RowIndex;
            string temp = magicCardsGridView.Rows[selectedIndex].Cells[0].Text;
            int cardIds = int.Parse(temp);
            DataRow row = cardsDS.Tables["AllMagicCards"].Rows.Find(cardIds);

            for (int i = 1; i < magicCardsGridView.Rows[e.RowIndex].Cells.Count - 4; i++)
            {
                row[i] = ((TextBox)magicCardsGridView.Rows[e.RowIndex].Cells[i].Controls[0]).Text;
            }
            string filename = "";
            FileUpload cardImage = (FileUpload)magicCardsGridView.Rows[e.RowIndex].FindControl("cardImageFileUpload");
            if (cardImage.PostedFile.FileName != "")
            {
                filename = Path.GetFileName(cardImage.FileName);
                cardImage.PostedFile.SaveAs(Server.MapPath(filename));
            }
            CardType card = new CardType(row[1].ToString(), row[2].ToString(), int.Parse(row[3].ToString()), 1, filename);
            cardsService = new CardsService(card);
            int rowUpdated;
            string cardName = row["cardName"].ToString();
            if (cardName.Length != 0)
            {
                if (!cardsService.CardAlreadyExists(cardIds, cardName))
                    rowUpdated = cardsService.UpdateTableRow(int.Parse(row[0].ToString()));
                else
                    Response.Write("<script>alert('name already taken');</script>");
            }
            else
                Response.Write("<script>alert('name can't be empty');</script>");
            magicCardsGridView.EditIndex = -1;
            magicCardsGridView.DataSource = cardsService.GetAllCards();
            magicCardsGridView.DataBind();
        }
    
        private void AddRarities(DropDownList list)
        {
            DataSet ds = cardsService.GetAllRarities();
            foreach (DataRow row in ds.Tables["Rarities"].Rows)
                list.Items.Add(new ListItem(row["rarityName"].ToString(), row["rarityId"].ToString()));
        }

        protected void OnRowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //check if is in edit mode
                if ((e.Row.RowState & DataControlRowState.Edit) > 0)
                {
                    ((TextBox)e.Row.Cells[3].Controls[0]).TextMode = TextBoxMode.Number;
                    DropDownList tempddl = (DropDownList)e.Row.FindControl("DropDownList1");
                    AddRarities(tempddl);
                }
            }
        }

        protected void KindSelectionChanged(object sender, EventArgs e)
        {

        }

       
    }
}