using FinalWebProject.App_Services;
using FinalWebProject.ClassTypes;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FinalWebProject.App_Aspx
{
    public partial class FormatsInsert : System.Web.UI.Page
    {
        FormatsService formatService;
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void OnInsert(object sender, EventArgs e)
        {
            try
            {
                if (IsValid)
                {
                    formatService = new FormatsService(new FormatType(nameTextBox.Text));
                    if (formatService.InsertUserCard() > 0)
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
    }
}
