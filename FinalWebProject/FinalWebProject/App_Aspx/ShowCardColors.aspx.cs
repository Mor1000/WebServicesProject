using FinalWebProject_App_Services;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FinalWebProject.App_Aspx
{
    public partial class ShowCardColors : System.Web.UI.Page
    {
        int selectedCard;
        DataTable dtColors;
        DataTable dtKinds;
        CardsService cardsService = new CardsService();
        ArrayList initSelectedColors;
        ArrayList initSelectedKinds;
        protected void Page_Load(object sender, EventArgs e)
        {
            selectedCard = Convert.ToInt32(Session["SelectedCardID"]);
            Response.Write("Card ID: " + selectedCard);
            if (!IsPostBack)
            {
                Session["save"] = false;
                Session["saveClicks"] = 0;
                Session["colorSelected"] = false;
                Session["kindSelected"] = false;
                colorsLabel.Text += "<br />";
                // ShowSelectedCardColors();
            }
        }
        private void ShowSelectedCardColors()
        {
            dtColors = cardsService.FindCardColors(selectedCard);
            Session["CardsColors"] = dtColors;
            Session["InitCardsColors"] = dtColors.Copy();
            initSelectedColors = new ArrayList();
            foreach (DataRow row in dtColors.Rows)
            {
                //   colorsLabel.Text += row["colorCardName"].ToString() + "<br />";
                colorsList.Items.FindByValue(row["colorName"].ToString()).Selected = true;
                initSelectedColors.Add(row["colorName"].ToString());

            }
            Session["ColorsList"] = initSelectedColors;
        }
        protected void OnColorBound(object sender, EventArgs e)
        {
            try
            {
                foreach (ListItem item in colorsList.Items)
                {
                    item.Text = string.Format("<img src= \"{0}\" width=35px; height=35px /> {1}", this.GetImageURL(item.Text), item.Text);
                }
                ShowSelectedCardColors();
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
        protected void OnReturn(object sender, EventArgs e)
        {
            if ((bool)Session["save"])
            {
                dtColors = (DataTable)Session["CardsColors"];
                DataTable initColors = (DataTable)Session["InitCardsColors"];
                foreach (DataRow row in dtColors.Rows)
                {
                    int selectedColor = Convert.ToInt32(row["colorName"]);
                    if (initColors.Rows.Find(new object[] { selectedCard, selectedColor }) == null)
                        if (cardsService.InsertColor(selectedCard, selectedColor) > 0)
                            Response.Write("<script>alert('Color successfully inserted');</script>");
                        else
                            Response.Write("<script>alert('Color insertion failed');</script>");
                }
                foreach (DataRow row in initColors.Rows)
                {
                    int selectedColor = Convert.ToInt32(row["colorName"]);
                    if (dtColors.Rows.Find(new object[] { selectedCard, selectedColor }) == null)
                        if (cardsService.DeleteCardsColor(selectedCard, selectedColor) > 0)
                            Response.Write("<script>alert('Color successfully inserted');</script>");
                        else
                            Response.Write("<script>alert('Color insertion failed');</script>");
                }
                dtKinds = (DataTable)Session["kindsDataTable"];
                DataTable initKinds = (DataTable)Session["InitKinds"];
                int[] attributes = (int[])Session["Attributes"];
                int[] initAttributes = (int[])Session["InitAttributes"];

                foreach (DataRow row in dtKinds.Rows)
                {
                    int selectedKind = Convert.ToInt32(row["kindName"]);
                    if (initKinds.Rows.Find(new object[] { selectedCard, selectedKind }) == null)
                        if (cardsService.InsertKind(selectedCard, selectedKind) > 0)
                        {
                            Response.Write("<script>alert('Kind successfully inserted');</script>");
                            if (int.Parse(kindsList.Items.FindByText("creature").Value) == selectedKind)
                                cardsService.InsertAttributes(selectedCard, attributes[0], attributes[1]);
                        }
                        else
                            Response.Write("<script>alert('Kind insertion Kind');</script>");
                }
                foreach (DataRow row in initKinds.Rows)
                {
                    int selectedKind = Convert.ToInt32(row["kindName"]);
                    if (dtKinds.Rows.Find(new object[] { selectedCard, selectedKind }) == null)
                    {
                        if (cardsService.DeleteCardsKinds(selectedCard, selectedKind) > 0)
                        {
                            Response.Write("<script>alert('Kind successfully inserted');</script>");
                        }
                        else
                            Response.Write("<script>alert('Kind insertion failed');</script>");
                    }
                    else
                     if (int.Parse(kindsList.Items.FindByText("creature").Value) == selectedKind && (initAttributes[0]!=attributes[0]|| initAttributes[1] != attributes[1]))
                        cardsService.UpdateCardAttributes(selectedCard, attributes[0], attributes[1]);
                }
                    
            }
            Response.Redirect(Session["From"].ToString());
        }

        protected void OnSave(object sender, EventArgs e)
        {
            int[] attributes = (int[])Session["Attributes"];
            dtColors = ((DataTable)Session["CardsColors"]);
            dtKinds = (DataTable)Session["kindsDataTable"];
            if (attributes == null)
                attributes = new int[2];
            Session["save"] = true;
            if ((bool)Session["colorSelected"])
            {
                //initSelectedColors = (ArrayList)Session["ColorsList"];

                foreach (ListItem item in colorsList.Items)
                {
                    object[] keys = new object[2];
                    keys[0] = selectedCard;
                    keys[1] = int.Parse(item.Value);
                    DataRow currColorRow = dtColors.Rows.Find(keys);
                    if (item.Selected && currColorRow == null)
                    {
                        currColorRow = dtColors.NewRow();
                        currColorRow["cardName"] = selectedCard;
                        currColorRow["colorName"] = int.Parse(item.Value);
                        dtColors.Rows.Add(currColorRow);
                        //if (cardsService.InsertColor(selectedCard, int.Parse(item.Value)) > 0)
                        //    Response.Write("<script>alert('Color successfully inserted');</script>");
                        //else
                        //    Response.Write("<script>alert('Color insertion failed');</script>");
                    }
                    else if (!item.Selected && currColorRow != null)
                        dtColors.Rows.Remove(currColorRow);
                    //if (cardsService.DeleteCardsColor(selectedCard, int.Parse(item.Value)) == 0)
                    //    Response.Write("<script>alert('Color insertion failed');</script>");
                }
                Session["CardsColors"] = dtColors;
            }
            bool initSelectedCreature = (bool)Session["creatureSelected"];
            int power = 0;
            int toughness = 0;
            if (kindsList.Items.FindByText("creature").Selected)
            {
                Validate("AttributesValidation");
                if (IsValid)
                {
                    power = int.Parse(powerTextBox.Text);
                    toughness = int.Parse(toughnessTextBox.Text);
                }
            }
            if (kindsListCustomValidator.IsValid)
            {
                if ((bool)Session["kindSelected"])
                {
                    Session["Attributes"] = attributes;
                    foreach (ListItem item in kindsList.Items)
                    {
                        DataRow currKindRow = dtKinds.Rows.Find(new object[] { selectedCard, int.Parse(item.Value) });
                        if (item.Selected && currKindRow == null && item.Text != "creature")
                        {
                            currKindRow = dtKinds.NewRow();
                            currKindRow["cardName"] = selectedCard;
                            currKindRow["kindName"] = int.Parse(item.Value);
                            dtKinds.Rows.Add(currKindRow);
                            //if (cardsService.InsertKind(selectedCard, int.Parse(item.Value)) > 0)
                            //    Response.Write("<script>alert('Kind successfully inserted');</script>");
                            //else
                            //    Response.Write("<script>alert('Kind insertion failed');</script>");
                        }
                        else if (!item.Selected && currKindRow != null)
                            currKindRow.Delete();
                        //if (cardsService.DeleteCardsKinds(selectedCard, int.Parse(item.Value)) == 0)
                        //    Response.Write("<script>alert('kind insertion failed');</script>");
                    }
                }
                ListItem creatureItem = kindsList.Items.FindByText("creature");
                int creatureId = int.Parse(creatureItem.Value);
                if (creatureItem.Selected)
                {
                    if (IsValid)
                    {
                        if (!initSelectedCreature)
                        {
                            DataRow creatureRow = dtKinds.NewRow();
                            creatureRow["cardName"] = selectedCard;
                            creatureRow["kindName"] = creatureId;
                            dtKinds.Rows.Add(creatureRow);
                        }
                        attributes[0] = power;
                        attributes[1] = toughness;
                        //if (cardsService.UpdateCardAttributes(selectedCard, power, toughness) == 0)
                        //    Response.Write("<script>alert('Kind insertion failed');</script>");

                        //int attributesInsert = cardsService.InsertAttributes(selectedCard, power, toughness);
                        //if (cardsService.InsertKind(selectedCard, creatureId) == 0 && attributesInsert == 0)
                        //    Response.Write("<script>alert('Kind insertion failed');</script>");
                        //else
                        Session["creatureSelected"] = true;

                    }
                    else
                        Response.Write("<script>alert('Attributes cannot be empty');</script>");
                }

                else if (initSelectedCreature)
                    dtKinds.Rows.Remove(dtKinds.Rows.Find(new object[] { selectedCard, creatureId }));
                //                    cardsService.DeleteCardAttributes(selectedCard);
                //                    Response.Redirect(Session["From"].ToString());


                Session["kindsDataTable"] = dtKinds;
            }
            else
                Response.Write("<script>alert('At least 1 card kind must be checked');</script>");
            //count++;
            //Session["saveClicks"] = count;
        }
        protected void OnColorIndexChanged(object sender, EventArgs e)
        {

            Session["colorSelected"] = true;
        }

        protected void KindSelectionChanged(object sender, EventArgs e)
        {
            Session["kindSelected"] = true;
            CreatureFields();
        }

        protected void OnKindsBound(object sender, EventArgs e)
        {
            dtKinds = cardsService.FindCardKindes(selectedCard);

            initSelectedKinds = new ArrayList();
            foreach (DataRow row in dtKinds.Rows)
            {
                initSelectedKinds.Add(row["kindName"].ToString());
                kindsList.Items.FindByValue(row["kindName"].ToString()).Selected = true;
            }
            Session["KindsList"] = initSelectedKinds;
            if (CreatureFields())
            {
                Session["creatureSelected"] = true;
                int[] attributes = cardsService.GetCreatureAttributes(selectedCard);
                if (attributes != null)
                {
                    powerTextBox.Text += attributes[0];
                    toughnessTextBox.Text += attributes[1];
                    Session["Attributes"] = new int[] { attributes[0], attributes[1] };
                    Session["InitAttributes"] = new int[] { attributes[0], attributes[1] };
                }
            }
            else
                Session["creatureSelected"] = false;
            Session["kindsDataTable"] = dtKinds;
            Session["InitKinds"] = dtKinds.Copy();
        }
        private bool CreatureFields()
        {
            bool isCreature = false;
            if (kindsList.Items.FindByText("creature").Selected)
            {
                isCreature = true;
                powerLabel.Visible = true;
                powerTextBox.Visible = true;
                toughnessLabel.Visible = true;
                toughnessTextBox.Visible = true;
            }
            else
            {
                powerLabel.Visible = false;
                powerTextBox.Visible = false;
                toughnessLabel.Visible = false;
                toughnessTextBox.Visible = false;
            }
            return isCreature;
        }

        protected void KindsListChecked(object source, ServerValidateEventArgs args)
        {
            bool selected = false;
            foreach (ListItem item in kindsList.Items)
            {
                if (item.Selected)
                {
                    selected = true;
                }

            }
            args.IsValid = selected;
        }
        //private void GetAllKinds(DataTable ds)
        //{
        //    initSelectedKinds = new ArrayList();
        //    foreach (DataRow row in ds.Rows)
        //    {
        //        initSelectedKinds.Add(row["kindName"].ToString());
        //    }
        //    Session["KindsList"] = initSelectedKinds;
        //}
    }
}