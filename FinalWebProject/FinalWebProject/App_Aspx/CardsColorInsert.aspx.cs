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
    public partial class CardsColorInsert : System.Web.UI.Page
    {
        CardColorService cardColorService;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                AddCards();
                AddColors();
            }
        }

        protected void OnInsert(object sender, EventArgs e)
        {
            try
            {
                if (IsValid)
                {
                    CardColorType colorCard = new CardColorType(int.Parse(cardDropDownList.SelectedValue),int.Parse(colorDropDownList.SelectedValue));
                    cardColorService = new CardColorService(colorCard);
                    if (cardColorService.InsertColorCard() > 0)
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
        private void AddColors()
        {
            try
            {
                cardColorService = new CardColorService();
                DataSet colorCardsDataSet = cardColorService.GetAllColors();
                Utilities.AddToDropDownList(colorCardsDataSet, colorDropDownList, "Colors", "colorName", "colorId");

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
                DataSet userCardsDataSet = new CardsService().GetAllCards();
                Utilities.AddToDropDownList(userCardsDataSet,cardDropDownList, "AllMagicCards","cardName","cardId");
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.StackTrace);
            }
        }
    }
}