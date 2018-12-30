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
    public partial class ShowCardColors : System.Web.UI.Page
    {
        CardsService cardsService = new CardsService();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ShowSelectedCardColors();
            }
        }
        private void ShowSelectedCardColors()
        {
            int selectedCard = Convert.ToInt32(Session["SelectedCardID"]);
            //string ds = cardsService.FindCardColors(selectedCard);
            //foreach(DataRow row in ds.Tables["CardsColor"].Rows) {
            //    colorsLabel.Text += " "+row["colorName"].ToString();
            //}
        }
    }
}