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
    public partial class CardsKindsInsert : System.Web.UI.Page
    {
        CardKindService cardKindService;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                AddCards();
                AddKinds();
            }
        }

        protected void OnInsert(object sender, EventArgs e)
        {

            try
            {
                if (IsValid)
                {
                    CardKindType kindCard = new CardKindType(cardDropDownList.SelectedIndex + 9, kindDropDownList.SelectedIndex + 1);
                    cardKindService = new CardKindService(kindCard);
                    if (cardKindService.InsertKindCard() > 0)
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
        private void AddKinds()
        {
            try
            {
                CardKindService cardKindService = new CardKindService();
                DataSet kindCardsDataSet = cardKindService.GetAllKinds();
                foreach (DataRow rows in kindCardsDataSet.Tables["CardKinds"].Rows)
                {
                    kindDropDownList.Items.Add(new ListItem(rows["kindName"].ToString()));
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
                CardKindService cardKindService = new CardKindService();
                DataSet kindCardsDataSet = new CardsService().GetAllCards();
                foreach (DataRow rows in kindCardsDataSet.Tables["AllMagicCards"].Rows)
                {
                    cardDropDownList.Items.Add(new ListItem(rows["cardName"].ToString()));
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.StackTrace);
            }
        }
    }
}