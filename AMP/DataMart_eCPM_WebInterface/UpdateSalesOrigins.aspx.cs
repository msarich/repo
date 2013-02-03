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
    public partial class UpdateSalesOrigins : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                btnUpdate.Visible = (Request["Action"] == "Update");
                hfID.Value = Request["id"];
                lblPrimary.Text = Request["primary"];
                tbSecondary.Text = Request["secondary"];
                Session.Add("SourcePage", Request["SourcePage"]);
            }
        }

        protected void UpdateRecord(object sender, EventArgs e)
        {
            SqlParameter[] parameters = new SqlParameter[3];
            parameters[0] = new SqlParameter("@Action", "Update");
            parameters[1] = new SqlParameter("@ID", hfID.Value);
            parameters[2] = new SqlParameter("@Secondary", tbSecondary.Text);
            DataAccess.executeStoredProcedureWithoutResults("AMP_SalesOrigins", parameters);
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