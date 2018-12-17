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
    public partial class UserCardsGridView : System.Web.UI.Page
    {
        UserCardService ucService = new UserCardService();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Utilities.AddToDropDownList(new CardsService().GetAllCards(),cardsDropDownList, "AllMagicCards", "cardName","cardId");
                ResetTable();
            }
        }
        private void ResetTable()
        {
            usersCardsGridView.DataSource = ucService.GetUserCardsTable();
            usersCardsGridView.DataBind();
        }

        protected void OnSearch(object sender, EventArgs e)
        {
            int cardId = -1;
            int amount = -1;
            if (cardsDropDownList.SelectedValue != "None")
                cardId = int.Parse(cardsDropDownList.SelectedValue);
            if (amountTextBox.Text != "")
                amount = int.Parse(amountTextBox.Text);
            UserCardType userCard = new UserCardType(userTextBox.Text,cardId,amount);
            ucService = new UserCardService(userCard);
            DataSet userCards = ucService.GetSelectedUserCard();
            if (userCards != null)
            {
                usersCardsGridView.DataSource = userCards;
                usersCardsGridView.DataBind();
            }
        }

        protected void OnReset(object sender, EventArgs e)
        {
            ResetTable();
        }
    }
}