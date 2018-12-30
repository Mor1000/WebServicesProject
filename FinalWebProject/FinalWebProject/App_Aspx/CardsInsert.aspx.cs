using FinalWebProject.App_Services;
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
            {
                Utilities.AddRarities(raritiesDropDownList);
                AddKinds();
            }
        }
        protected void insertClick(object sender, EventArgs e)
        {
            //try
            //{
            if (IsValid)
            {
                if (cardImage.PostedFile.FileName == "")
                    Response.Write("<script>alert('File has no name');</script>");
                else
                {
                    string filename = Path.GetFileName(cardImage.FileName);
                    cardImage.PostedFile.SaveAs(Server.MapPath(filename));
                    CardType card = new CardType(cardNameTextBox.Text, abilityTextBox.Text, int.Parse(manaCostTextBox.Text), int.Parse(raritiesDropDownList.SelectedValue), filename);
                    CardsService insertService = new CardsService(card);
                    if (!insertService.CardAlreadyExists())
                    {
                        if (insertService.InsertCard() > 0)
                        {
                            Response.Write("<script>alert('Successfully inserted');</script>");
                            int cardId = insertService.LastCardId();
                            foreach (ListItem item in colorsList.Items)
                            {
                                if (item.Selected)
                                {
                                    if (insertService.InsertColor(cardId, int.Parse(item.Value)) != 0)
                                        Response.Write("<script>alert('color successfully inserted');</script>");
                                    else
                                        Response.Write("<script>alert('color insertion failed');</script>");
                                }
                            }
                            int kindId = int.Parse(kindDropDownList.SelectedValue);
                            if (insertService.InsertKind(cardId, kindId) > 0)
                                Response.Write("<script>alert('Kind insertion successful');</script>");
                            else
                                Response.Write("<script>alert('Kind insertion failed');</script>");

                        }
                        else
                        {
                            Response.Write("<script>alert('Insertion failed');</script>");
                        }

                    }
                    else
                        Response.Write("<script>alert('Card already exists');</script>");
                }
            }
            //}
            //catch(Exception ex)
            //{

            //}
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
        private void AddKinds()
        {
            CardKindService cardKindService = new CardKindService();
            DataSet kindCardsDataSet = cardKindService.GetAllKinds();
            Utilities.AddToDropDownList(kindCardsDataSet, kindDropDownList, "CardKinds", "kindName", "kindId");

        }
    }
}