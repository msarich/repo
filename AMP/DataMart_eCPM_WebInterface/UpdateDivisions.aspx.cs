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
    public partial class UpdateDivisions : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                btnAdd.Visible = (Request["Action"] == "Add");
                btnUpdate.Visible = (Request["Action"] == "Update");
                btnDelete.Visible = (Request["Action"] == "Update");
                hfDivisionID.Value = Request["id"];
                tbDivisionName.Text = Request["name"];
                Session.Add("SourcePage", Request["SourcePage"]);
            }
        }

        protected void AddNewRecord(object sender, EventArgs e)
        {
            SqlParameter[] parameters = new SqlParameter[2];
            parameters[0] = new SqlParameter("@Action", "Insert");
            parameters[1] = new SqlParameter("@DivisionName", tbDivisionName.Text);
            DataAccess.executeStoredProcedureWithoutResults("AMP_usp_D_Divisions", parameters);
            RedirectToPreviousPage();
        }

        protected void UpdateRecord(object sender, EventArgs e)
        {
            SqlParameter[] parameters = new SqlParameter[3];
            parameters[0] = new SqlParameter("@Action", "Update");
            parameters[1] = new SqlParameter("@DivisionID", hfDivisionID.Value);
            parameters[2] = new SqlParameter("@DivisionName", tbDivisionName.Text);
            DataAccess.executeStoredProcedureWithoutResults("AMP_usp_D_Divisions", parameters);
            RedirectToPreviousPage();
        }

        protected void DeleteRecord(object sender, EventArgs e)
        {
            SqlParameter[] parameters = new SqlParameter[2];
            parameters[0] = new SqlParameter("@Action", "Delete");
            parameters[1] = new SqlParameter("@DivisionID", Convert.ToInt32(hfDivisionID.Value));
            DataAccess.executeStoredProcedureWithoutResults("AMP_usp_D_Divisions", parameters);
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