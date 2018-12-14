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
    public partial class UserCardsInsert : System.Web.UI.Page
    {
        UserCardService userCardService;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                AddCards();
                AddUsers();
            }
        }

        protected void OnInsert(object sender, EventArgs e)
        {
            
                try
                {
                    if (IsValid)
                    {
                        UserCardType userCard = new UserCardType(userDropDownList.SelectedValue, cardDropDownList.SelectedIndex + 9, int.Parse(amountTextBox.Text));
                        userCardService = new UserCardService(userCard);
                        if (userCardService.InsertUserCard() > 0)
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

        
        private void AddUsers()
        {
            try
            {
                userCardService = new UserCardService();
                DataSet userCardsDataSet = userCardService.GetAllUsers();
                foreach (DataRow rows in userCardsDataSet.Tables["Users"].Rows)
                {
                    userDropDownList.Items.Add(new ListItem(rows["userName"].ToString()));
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
                userCardService = new UserCardService();
                DataSet userCardsDataSet = new CardsService().GetAllCards();
                foreach (DataRow rows in userCardsDataSet.Tables["AllMagicCards"].Rows)
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