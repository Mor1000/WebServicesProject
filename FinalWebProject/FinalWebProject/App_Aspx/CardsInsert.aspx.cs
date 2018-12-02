using FinalWebProject.ClassTypes;
using FinalWebProject_App_Services;
using System;
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
    public partial class CardsInsert : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                AddRarities();
        }
        protected void insertClick(object sender, EventArgs e)
        {
            if (IsValid)
            {
                if (cardImage.PostedFile.FileName == "")
                    Response.Write("<script>alert('File has no name');</script>");
                else
                {
                    string filename = Path.GetFileName(cardImage.FileName);
                    cardImage.PostedFile.SaveAs(Server.MapPath(filename));
                    CardType card = new CardType(cardNameTextBox.Text, abilityTextBox.Text, int.Parse(manaCostTextBox.Text), raritiesDropDownList.SelectedIndex+1, filename);
                    CardsService insertService = new CardsService(card);
                    if (insertService.InsertCard() > 0)
                    {
                        Response.Write("<script>alert('Successfully inserted');</script>");
                    }
                    else
                    {
                        Response.Write("<script>alert('Insertion failed');</script>");
                    }
                }
            }
        }
        private void AddRarities()
        {
            try
            {
                CardsService cardsServices = new CardsService();
                DataSet cardsDataSet = cardsServices.GetAllRarities();
                foreach (DataRow rows in cardsDataSet.Tables["Rarities"].Rows)
                {
                    raritiesDropDownList.Items.Add(new ListItem(rows["rarityName"].ToString()));
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.StackTrace);
            }
        }
    }
}