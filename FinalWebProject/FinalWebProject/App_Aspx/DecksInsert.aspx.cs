using FinalWebProject.App_Services;
using FinalWebProject.ClassTypes;
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
    public partial class DecksInsert : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                AddFormats();
        }

        protected void OnInsert(object sender, EventArgs e)
        {
            //try
            //{
            if (IsValid)
            {
                DeckType deck = new DeckType(nameTextBox.Text, formatsDropDownList.SelectedIndex + 1, DateTime.Now.ToShortDateString(), descriptionTextBox.Text);
                DecksService insertService = new DecksService(deck);
                if (insertService.InsertDeck() > 0)
                {
                    Response.Write("<script>alert('Successfully inserted');</script>");
                }
                else
                {
                    Response.Write("<script>alert('Insertion failed');</script>");
                }
                //    }
                //}
                //catch (Exception ex)
                //{
                //    Debug.WriteLine(ex.StackTrace);
                //}
            }
        }
        private void AddFormats()
        {
            try
            {
                DecksService cardsServices = new DecksService();
                DataSet cardsDataSet = cardsServices.GetAllFormats();
                foreach (DataRow rows in cardsDataSet.Tables["Formats"].Rows)
                {
                    formatsDropDownList.Items.Add(new ListItem(rows["formatName"].ToString()));
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.StackTrace);
            }
        }
    }
}
