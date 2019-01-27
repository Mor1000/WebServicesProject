using FinalWebProject.App_Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FinalWebProject.App_Aspx
{
    public partial class FormatsGridView : System.Web.UI.Page
    {
        DecksService decksService=new DecksService();
        DataSet formatsDS;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                formatsDS= decksService.GetAllFormats();
                formatsGrid.DataSource = formatsDS;
                formatsGrid.DataBind();
                Cache["Formats"]=formatsDS;
            }
        }

        protected void OnRowEditing(object sender, GridViewEditEventArgs e)
        {
            formatsGrid.EditIndex = e.NewEditIndex;
            formatsGrid.DataSource = (DataSet)Cache["Formats"];
            formatsGrid.DataBind();
        }

        protected void OnEditCanceling(object sender, GridViewCancelEditEventArgs e)
        {
            formatsGrid.EditIndex = -1;
            formatsGrid.DataSource = (DataSet)Cache["Formats"];
            formatsGrid.DataBind();
        }

        protected void OnRowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            formatsDS = (DataSet)Cache["Formats"];
            int formatId = int.Parse(formatsGrid.Rows[e.RowIndex].Cells[0].Text);
            DataRow row = formatsDS.Tables["Formats"].Rows.Find(formatId);
            
            if (!decksService.FormatInDeck(formatId))
            {
                if (decksService.DeleteFormat(formatId) > 0)
                    row.Delete();
                else
                    Response.Write("<script>alert('Error Deleting');</script>");
            }
            else
                decksService.UnAbleCard(formatId);

            formatsGrid.EditIndex = -1;
            formatsGrid.DataSource = (DataSet)Cache["Formats"];
            formatsGrid.DataBind();
        }
     
        protected void OnRowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            DataSet formatsDS = (DataSet)Cache["Formats"];
            int selectedIndex = e.RowIndex;
            string temp = formatsGrid.Rows[selectedIndex].Cells[0].Text;
            int formatId = int.Parse(temp);
            DataRow row = formatsDS.Tables["Formats"].Rows.Find(formatId);
            row[1] = ((TextBox)formatsGrid.Rows[e.RowIndex].Cells[1].Controls[0]).Text;
            if (!decksService.formatAlreadyExists(formatId, row[1].ToString()))
               decksService.UpdateTableRow(formatId,row[1].ToString());
            else
                Response.Write("<script>alert('name already taken');</script>");
            formatsGrid.EditIndex = -1;
            formatsGrid.DataSource = formatsDS;
            formatsGrid.DataBind();
        }

        protected void OnRowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //check if is in edit mode
                if ((e.Row.RowState & DataControlRowState.Edit) > 0)
                {
                    string temp = e.Row.Cells[0].Text;

                }
            }
        }
    }
}