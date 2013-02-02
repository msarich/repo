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
    public partial class UpdateTablesHouse : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                btnAdd.Visible = (Request["Action"] == "Add");
                btnUpdate.Visible = (Request["Action"] == "Update");
                btnDelete.Visible = (Request["Action"] == "Update");
                tbName.Text = Request["name"];
                tbName.Enabled = (Request["Action"] == "Add");
                if (ddlIsHouse.Items.FindByText(Request["is_house"]) != null)
                {
                    ddlIsHouse.Items.FindByText(Request["is_house"]).Selected = true;
                }
                Session.Add("SourcePage", Request["SourcePage"]);
            }
        }

        protected void AddNewRecord(object sender, EventArgs e)
        {
            if (ddlIsHouse.SelectedValue.CompareTo("DoNotSave") != 0)
            {
                SqlParameter[] parameters = new SqlParameter[3];
                parameters[0] = new SqlParameter("@Action", "Insert");
                parameters[1] = new SqlParameter("@Name", tbName.Text);
                parameters[2] = new SqlParameter("@Is_House", SqlDbType.Bit);
                parameters[2].Value = Convert.ToInt32(ddlIsHouse.SelectedValue);
                DataAccess.executeStoredProcedureWithoutResults("AMP_usp_M_House", parameters);
                RedirectToPreviousPage();
            }
            else
            {
                lblError.Text = "You must select a value for Is House.";
            }
        }

        protected void UpdateRecord(object sender, EventArgs e)
        {
            if (ddlIsHouse.SelectedValue.CompareTo("DoNotSave") != 0)
            {
                SqlParameter[] parameters = new SqlParameter[3];
                parameters[0] = new SqlParameter("@Action", "Update");
                parameters[1] = new SqlParameter("@Name", tbName.Text);
                parameters[2] = new SqlParameter("@Is_House", SqlDbType.Bit);
                parameters[2].Value = Convert.ToInt32(ddlIsHouse.SelectedValue);
                DataAccess.executeStoredProcedureWithoutResults("AMP_usp_M_House", parameters);
                RedirectToPreviousPage();
            }
            else
            {
                lblError.Text = "You must select a value for Is House.";
            }
        }

        protected void DeleteRecord(object sender, EventArgs e)
        {
            SqlParameter[] parameters = new SqlParameter[2];
            parameters[0] = new SqlParameter("@Action", "Delete");
            parameters[1] = new SqlParameter("@Name", tbName.Text);
            DataAccess.executeStoredProcedureWithoutResults("AMP_usp_M_House", parameters);
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