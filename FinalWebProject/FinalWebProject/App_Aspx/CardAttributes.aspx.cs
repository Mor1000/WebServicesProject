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
    public partial class CardAttributes : System.Web.UI.Page
    {
        CardsAttributesService cardAttService;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                AddCards();
        }

        protected void OnInsert(object sender, EventArgs e)
        {
            try
            {
                if (IsValid)
                {
                    CardAttributeType attributeCard = new CardAttributeType(int.Parse(cardDropDownList.SelectedValue), int.Parse(powerTextBox.Text), int.Parse(toughnessTextBox.Text));
                    cardAttService = new CardsAttributesService(attributeCard);
                    if (cardAttService.InsertAttributeCard() > 0)
                    {
                        Response.Write("<script>alert('Successfully inserted');</script>");
                    }
                    else
                    {
                        Response.Write("<script>alert('Insertion failed');</script>");
                    }

                }
            }


            catch (Exception ex)
            {
                Debug.WriteLine(ex.StackTrace);
            }
        }
        private void AddCards()
        {
            try
            {
                DataSet kindCardsDataSet = new CardsService().GetAllCards();
                Utilities.AddToDropDownList(kindCardsDataSet,cardDropDownList, "AllMagicCards","cardName","cardId");
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.StackTrace);
            }
        }
    }
}