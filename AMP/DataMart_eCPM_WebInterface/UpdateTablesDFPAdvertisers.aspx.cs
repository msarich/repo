using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

namespace DataMart_eCPM_WebInterface
{
    public partial class UpdateTablesDFPAdvertisers : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //Populate Sales Origin DropDownList
                ddlSalesOrigin.DataSource = DataAccess.executeStoredProcedureWithResults("AMP_getSalesOrigins", new SqlParameter[0]);
                ddlSalesOrigin.DataTextField = "DropDownListText";
                ddlSalesOrigin.DataValueField = "id";
                ddlSalesOrigin.DataBind();
                if (Request["Action"] == "Add" || Request["sales_origin_id"].Length == 0)
                {
                    ddlSalesOrigin.Items.Insert(0, new ListItem("- Select Sales Origin -", "DoNotSave"));
                    ddlSalesOrigin.SelectedIndex = 0;
                }
                btnAdd.Visible = (Request["Action"] == "Add");
                btnUpdate.Visible = (Request["Action"] == "Update");
                btnDelete.Visible = (Request["Action"] == "Update");
                tbName.Text = Request["name"];
                tbName.Enabled = (Request["Action"] == "Add");
                if (ddlMatch.Items.FindByText(Request["match"]) != null)
                {
                    ddlMatch.Items.FindByText(Request["match"]).Selected = true;
                }
                if (ddlSalesOrigin.Items.FindByValue(Request["sales_origin_id"]) != null)
                {
                    ddlSalesOrigin.Items.FindByValue(Request["sales_origin_id"]).Selected = true;
                }
                Session.Add("SourcePage", Request["SourcePage"]);
            }
        }

        protected void AddNewRecord(object sender, EventArgs e)
        {
            if (ddlMatch.SelectedValue.CompareTo("DoNotSave") != 0 && ddlSalesOrigin.SelectedValue.CompareTo("DoNotSave") != 0)
            {
                SqlParameter[] parameters = new SqlParameter[4];
                parameters[0] = new SqlParameter("@Action", "Insert");
                parameters[1] = new SqlParameter("@Name", tbName.Text);
                parameters[2] = new SqlParameter("@Match", ddlMatch.SelectedValue);
                parameters[3] = new SqlParameter("@Sales_Origin_Id", ddlSalesOrigin.SelectedValue);
                DataAccess.executeStoredProcedureWithoutResults("AMP_usp_GoogleDFP_M_Advertiser", parameters);
                RedirectToPreviousPage();
            }
            else
            {
                lblError.Text = "You must select a Match and a Sales Origin.";
            }
        }

        protected void UpdateRecord(object sender, EventArgs e)
        {
            if (ddlMatch.SelectedValue.CompareTo("DoNotSave") != 0 && ddlSalesOrigin.SelectedValue.CompareTo("DoNotSave") != 0)
            {
                SqlParameter[] parameters = new SqlParameter[4];
                parameters[0] = new SqlParameter("@Action", "Update");
                parameters[1] = new SqlParameter("@Name", tbName.Text);
                parameters[2] = new SqlParameter("@Match", ddlMatch.SelectedValue);
                parameters[3] = new SqlParameter("@Sales_Origin_Id", ddlSalesOrigin.SelectedValue);
                DataAccess.executeStoredProcedureWithoutResults("AMP_usp_GoogleDFP_M_Advertiser", parameters);
                RedirectToPreviousPage();
            }
            else
            {
                lblError.Text = "You must select a Match and a Sales Origin.";
            }
        }

        protected void DeleteRecord(object sender, EventArgs e)
        {
            SqlParameter[] parameters = new SqlParameter[2];
            parameters[0] = new SqlParameter("@Action", "Delete");
            parameters[1] = new SqlParameter("@Name", tbName.Text);
            DataAccess.executeStoredProcedureWithoutResults("AMP_usp_GoogleDFP_M_Advertiser", parameters);
            RedirectToPreviousPage();
        }

        protected void Cancel(object sender, EventArgs e)
        {
            RedirectToPreviousPage();
        }

        private void RedirectToPreviousPage()
        {
            Page.Response.Redirect("~/" + Session["SourcePage"] + ".aspx");
        }
    }
}