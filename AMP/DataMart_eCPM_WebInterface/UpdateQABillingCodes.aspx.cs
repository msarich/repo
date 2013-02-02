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
    public partial class UpdateQABillingCodes : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                tbId.Text = Request["id"];
                tbName.Text = Request["name"];
                tbaddress.Text = Request["address"];
                tbappliedLabels.Text = Request["appliedLabels"];
                tbcomment.Text = Request["comment"];
                tbcreditStatus.Text = Request["creditStatus"];
                tbemail.Text = Request["email"];
                tbenableSameAdvertiserCompetitiveExclusion.Text = Request["enableSameAdvertiserCompetitiveExclusion"];
                tbexternalId.Text = Request["externalId"];
                tbfaxPhone.Text = Request["faxPhone"];
                tbprimaryPhone.Text = Request["primaryPhone"];
                tbthirdPartyCompanyId.Text = Request["thirdPartyCompanyId"];
                tbtype.Text = Request["type"];
                tbcreditStatusSpecified.Text = Request["creditStatusSpecified"];
                tbenableSameAdvertiserCompetitiveExclusionSpecified.Text = Request["enableSameAdvertiserCompetitiveExclusionSpecified"];
                tbidSpecified.Text = Request["idSpecified"];
                tblastModifiedDateTime.Text = Request["lastModifiedDateTime"];
                tbthirdPartyCompanyIdSpecified.Text = Request["thirdPartyCompanyIdSpecified"];
                tbtypeSpecified.Text = Request["typeSpecified"];
                tbappliedTeamIds.Text = Request["appliedTeamIds"];
                Session.Add("SourcePage", Request["SourcePage"]);
            }
        }

        protected void UpdateRecord(object sender, EventArgs e)
        {
            SqlParameter[] parameters = new SqlParameter[21];
            parameters[0] = new SqlParameter("@Action", "Update");
            parameters[1] = new SqlParameter("@id", Convert.ToInt32(tbId.Text));
            parameters[2] = new SqlParameter("@name", tbName.Text);
            parameters[3] = new SqlParameter("@address", tbaddress.Text);
            parameters[4] = new SqlParameter("@appliedLabels", tbappliedLabels.Text);
            parameters[5] = new SqlParameter("@comment", tbcomment.Text);
            parameters[6] = new SqlParameter("@creditStatus", tbcreditStatus.Text);
            parameters[7] = new SqlParameter("@email", tbemail.Text);
            parameters[8] = new SqlParameter("@enableSameAdvertiserCompetitiveExclusion", tbenableSameAdvertiserCompetitiveExclusion.Text);
            parameters[9] = new SqlParameter("@externalId", tbexternalId.Text);
            parameters[10] = new SqlParameter("@faxPhone", tbfaxPhone.Text);
            parameters[11] = new SqlParameter("@primaryPhone", tbprimaryPhone.Text);
            parameters[12] = new SqlParameter("@thirdPartyCompanyId", tbthirdPartyCompanyId.Text);
            parameters[13] = new SqlParameter("@type", tbtype.Text);
            parameters[14] = new SqlParameter("@creditStatusSpecified", tbcreditStatusSpecified.Text);
            parameters[15] = new SqlParameter("@enableSameAdvertiserCompetitiveExclusionSpecified", tbenableSameAdvertiserCompetitiveExclusionSpecified.Text);
            parameters[16] = new SqlParameter("@idSpecified", tbidSpecified.Text);
            parameters[17] = new SqlParameter("@lastModifiedDateTime", tblastModifiedDateTime.Text);
            parameters[18] = new SqlParameter("@thirdPartyCompanyIdSpecified", tbthirdPartyCompanyIdSpecified.Text);
            parameters[19] = new SqlParameter("@typeSpecified", tbtypeSpecified.Text);
            parameters[20] = new SqlParameter("@appliedTeamIds", tbappliedTeamIds.Text);
            DataAccess.executeStoredProcedureWithoutResults("AMP_usp_D_Company", parameters, conn: "GoogleDataMart");
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