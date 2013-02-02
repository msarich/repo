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
    public partial class UpdateTablesDFPMarkets : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //Populate Division DropDownList
                ddlDivision.DataSource = DataAccess.executeStoredProcedureWithResults("AMP_getDivisions", new SqlParameter[0]);
                ddlDivision.DataTextField = "DivisionName";
                ddlDivision.DataValueField = "DivisionID";
                ddlDivision.DataBind();
                if (Request["Action"] == "Add")
                {
                    ddlDivision.Items.Insert(0, new ListItem("- Select Division -", "DoNotSave"));
                    ddlDivision.SelectedIndex = 0;
                }
                //Populate Sales Origin DropDownList
                ddlSalesOrigin.DataSource = DataAccess.executeStoredProcedureWithResults("AMP_getSalesOrigins", new SqlParameter[0]);
                ddlSalesOrigin.DataTextField = "DropDownListText";
                ddlSalesOrigin.DataValueField = "id";
                ddlSalesOrigin.DataBind();
                ddlSalesOrigin.Items.Insert(0, new ListItem("- Select Sales Origin -", "DoNotSave"));
                btnAdd.Visible = (Request["Action"] == "Add");
                btnUpdate.Visible = (Request["Action"] == "Update");
                btnDelete.Visible = (Request["Action"] == "Update");
                tbId.Text = Request["id"];
                tbId.Enabled = (Request["Action"] == "Add");
                tbCode.Text = Request["code"];
                if (ddlDivision.Items.FindByText(Request["division"]) != null)
                {
                    ddlDivision.Items.FindByText(Request["division"]).Selected = true;
                }
                tbGDMNWebsite.Text = Request["gdmn_website"];
                tbGDMNSite.Text = Request["gdmn_site"];
                if (ddlSalesOrigin.Items.FindByValue(Request["sales_origin_id"]) != null)
                {
                    ddlSalesOrigin.Items.FindByValue(Request["sales_origin_id"]).Selected = true;
                }
                else
                {
                    ddlSalesOrigin.SelectedIndex = 0;
                }
                Session.Add("SourcePage", Request["SourcePage"]);
            }
        }

        protected void AddNewRecord(object sender, EventArgs e)
        {
            if (ddlDivision.SelectedValue.CompareTo("DoNotSave") != 0)
            {
                SqlParameter[] parameters = new SqlParameter[7];
                parameters[0] = new SqlParameter("@Action", "Insert");
                parameters[1] = new SqlParameter("@ID", tbId.Text);
                parameters[2] = new SqlParameter("@Code", tbCode.Text);
                parameters[3] = new SqlParameter("@Division", ddlDivision.SelectedItem.Text);
                parameters[4] = new SqlParameter("@GDMN_Website", tbGDMNWebsite.Text);
                parameters[5] = new SqlParameter("@GDMN_Site", tbGDMNSite.Text);
                if (ddlSalesOrigin.SelectedValue == "DoNotSave")
                {
                    parameters[6] = new SqlParameter("@Sales_Origin_Id", DBNull.Value);
                }
                else
                {
                    parameters[6] = new SqlParameter("@Sales_Origin_Id", ddlSalesOrigin.SelectedValue);
                }
                DataAccess.executeStoredProcedureWithoutResults("AMP_usp_GoogleDFP_M_Market", parameters);
                RedirectToPreviousPage();
            }
            else
            {
                lblError.Text = "You must select a Division.";
            }
        }

        protected void UpdateRecord(object sender, EventArgs e)
        {
            if (ddlDivision.SelectedValue.CompareTo("DoNotSave") != 0)
            {
                SqlParameter[] parameters = new SqlParameter[7];
                parameters[0] = new SqlParameter("@Action", "Update");
                parameters[1] = new SqlParameter("@ID", tbId.Text);
                parameters[2] = new SqlParameter("@Code", tbCode.Text);
                parameters[3] = new SqlParameter("@Division", ddlDivision.SelectedItem.Text);
                parameters[4] = new SqlParameter("@GDMN_Website", tbGDMNWebsite.Text);
                parameters[5] = new SqlParameter("@GDMN_Site", tbGDMNSite.Text);
                if (ddlSalesOrigin.SelectedValue == "DoNotSave")
                {
                    parameters[6] = new SqlParameter("@Sales_Origin_Id", null);
                }
                else
                {
                    parameters[6] = new SqlParameter("@Sales_Origin_Id", ddlSalesOrigin.SelectedValue);
                }
                DataAccess.executeStoredProcedureWithoutResults("AMP_usp_GoogleDFP_M_Market", parameters);
                RedirectToPreviousPage();
            }
            else
            {
                lblError.Text = "You must select a Division.";
            }
        }

        protected void DeleteRecord(object sender, EventArgs e)
        {
            SqlParameter[] parameters = new SqlParameter[2];
            parameters[0] = new SqlParameter("@Action", "Delete");
            parameters[1] = new SqlParameter("@ID", tbId.Text);
            DataAccess.executeStoredProcedureWithoutResults("AMP_usp_GoogleDFP_M_Market", parameters);
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