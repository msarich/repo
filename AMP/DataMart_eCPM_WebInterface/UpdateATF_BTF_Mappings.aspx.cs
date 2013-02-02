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
	public partial class UpdateATF_BTF_Mappings : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
            if (!IsPostBack)
            {
                btnAdd.Visible = (Request["Action"] == "Add");
                btnUpdate.Visible = (Request["Action"] == "Update");
                btnDelete.Visible = (Request["Action"] == "Update");
                hfATF_BTF_Mappings_Id.Value = Request["ATF_BTF_Mappings_Id"];
                tbPosition.Text = Request["Position"];
                if (Request["ATF_BTF"] != null)
                {
                    ddlATF_BTF.Items.FindByValue(Request["ATF_BTF"]).Selected = true;
                }
                if (Request["USATSITE"] != null)
                {
                    cbUSATSite.Checked = (Request["USATSITE"] == "YES");
                }
                Session.Add("SourcePage", Request["SourcePage"]);
            }
		}

        public void AddNewRecord(object sender, EventArgs e)
        {
            SqlParameter[] parameters = new SqlParameter[4];
            parameters[0] = new SqlParameter("@Action", "Insert");
            parameters[1] = new SqlParameter("@Position", tbPosition.Text);
            parameters[2] = new SqlParameter("@ATF_BTF", ddlATF_BTF.SelectedValue);
            parameters[3] = new SqlParameter("@USAT_SITE", cbUSATSite.Checked);
            DataAccess.executeStoredProcedureWithoutResults("AMP_usp_ATF_BTF_Mappings", parameters, conn:"HeliosDataMart_Fresh");
            RedirectToPreviousPage();
        }

        public void UpdateRecord(object sender, EventArgs e)
        {
            SqlParameter[] parameters = new SqlParameter[5];
            parameters[0] = new SqlParameter("@Action", "Update");
            parameters[1] = new SqlParameter("@Position", tbPosition.Text);
            parameters[2] = new SqlParameter("@ATF_BTF", ddlATF_BTF.SelectedValue);
            parameters[3] = new SqlParameter("@USAT_SITE", cbUSATSite.Checked);
            parameters[4] = new SqlParameter("@ATF_BTF_Mappings_Id", hfATF_BTF_Mappings_Id.Value);
            DataAccess.executeStoredProcedureWithoutResults("AMP_usp_ATF_BTF_Mappings", parameters, conn: "HeliosDataMart_Fresh");
            RedirectToPreviousPage();
        }

        public void DeleteRecord(object sender, EventArgs e)
        {
            SqlParameter[] parameters = new SqlParameter[2];
            parameters[0] = new SqlParameter("@Action", "Delete");
            parameters[1] = new SqlParameter("@ATF_BTF_Mappings_Id", hfATF_BTF_Mappings_Id.Value);
            DataAccess.executeStoredProcedureWithoutResults("AMP_usp_ATF_BTF_Mappings", parameters, conn: "HeliosDataMart_Fresh");
            RedirectToPreviousPage();
        }

        public void Cancel(object sender, EventArgs e)
        {
            RedirectToPreviousPage();
        }

        private void RedirectToPreviousPage()
        {
            Page.Response.Redirect("~/" + Session["SourcePage"] + ".aspx");
        }
	}
}