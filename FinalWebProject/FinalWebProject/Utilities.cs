using FinalWebProject_App_Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace FinalWebProject
{
    public class Utilities
    {
        public static void AddToDropDownList(DataSet usersDataSet, DropDownList info, string tableName, string text, string value)
        {
            foreach (DataRow rows in usersDataSet.Tables[tableName].Rows)
            {
                info.Items.Add(new ListItem(rows[text].ToString(), rows[value].ToString()));
            }
        }
        public static void AddRarities(DropDownList raritiesDropDownList)
        {
            try
            {
                CardsService cardsServices = new CardsService();
                DataSet cardsDataSet = cardsServices.GetAllRarities();
                Utilities.AddToDropDownList(cardsDataSet, raritiesDropDownList, "Rarities", "rarityName", "rarityId");
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.StackTrace);
            }
        }
    }
}