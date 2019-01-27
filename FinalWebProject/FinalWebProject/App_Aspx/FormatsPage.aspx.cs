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
    public partial class FormatsPage : System.Web.UI.Page
    {
        DecksService decksService = new DecksService();
        DataSet formatsDS;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                formatsDS = decksService.GetAllFormats();
                formatsGridView.DataSource = formatsDS;
                formatsGridView.DataBind();
                Cache["Formats"] = formatsDS;
            }
        }

        protected void RowEditingMode(object sender, GridViewEditEventArgs e)
        {
            formatsGridView.EditIndex = e.NewEditIndex;
            Session["FormatID"] = int.Parse(formatsGridView.Rows[e.NewEditIndex].Cells[0].Text);
            formatsGridView.DataSource = (DataSet)Cache["Formats"];
            formatsGridView.DataBind();
        }

        protected void OnCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            formatsGridView.EditIndex = -1;
            formatsGridView.DataSource = (DataSet)Cache["Formats"];
            formatsGridView.DataBind();
        }

        protected void OnRowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            formatsDS = (DataSet)Cache["Formats"];
            int selectedIndex = e.RowIndex;
            int formatId = (int)Session["FormatID"]; 
            DataRow row = formatsDS.Tables["Formats"].Rows.Find(formatId);
            row[1] = ((TextBox)formatsGridView.Rows[e.RowIndex].Cells[1].Controls[0]).Text;
            if (row[1].ToString().Length != 0)
            {
                if (!decksService.formatAlreadyExists(formatId, row[1].ToString()))
                {
                    decksService.UpdateTableRow(formatId, row[1].ToString());
                    formatsGridView.EditIndex = -1;
                    formatsGridView.DataSource = formatsDS;
                    formatsGridView.DataBind();
                }
                else
                    Response.Write("<script>alert('name already taken');</script>");
            }
            else
                Response.Write("<script>alert('name can't be empty');</script>");
      
        }

        protected void OnRowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            formatsDS = (DataSet)Cache["Formats"];
            int formatId = int.Parse(formatsGridView.Rows[e.RowIndex].Cells[0].Text);
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

            formatsGridView.EditIndex = -1;
            formatsGridView.DataSource = (DataSet)Cache["Formats"];
            formatsGridView.DataBind();
        }

        protected void OnSearch(object sender, EventArgs e)
        {
            if (formatNameTextBox.Text.Length > 0)
            {
                DataView dv = decksService.GetSelectedFormat((DataSet)Cache["Formats"], formatNameTextBox.Text);
                if (dv.Count > 0)
                {
                    formatsGridView.DataSource = dv;
                    formatsGridView.DataBind();
                }
            }
        }

        protected void OnReset(object sender, EventArgs e)
        {
            formatsGridView.DataSource = (DataSet)Cache["Formats"];
            formatsGridView.DataBind();
        }
    }
}