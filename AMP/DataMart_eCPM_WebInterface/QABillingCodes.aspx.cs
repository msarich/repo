using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.IO;

namespace DataMart_eCPM_WebInterface
{
    public partial class QABillingCodes : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                SqlDataReader sqlDataReader = DataAccess.executeStoredProcedureWithResults("AMP_usp_D_Company", new SqlParameter[1] { new SqlParameter("@Action", "GetCompaniesWithoutBillingCodes") }, conn: "GoogleDataMart");
                DataTable dataTable = new DataTable();
                dataTable.Load(sqlDataReader);
                gvBillingCodes.DataSource = dataTable;
                gvBillingCodes.DataBind();
                lbExportBillingCodesDataToExcel.Visible = (dataTable.Rows.Count > 0);
                Session.Add("dataTable", dataTable);
                Session.Add("gvBillingCodesSortDirection", "ASC");
                Session.Add("gvBillingCodesSortExpression", "");
            }
        }

        protected void UpdateRow(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Update")
            {
                DataTable dataTable = Session["dataTable"] as DataTable;
                int idIndex = -1;
                int nameIndex = -1;
                /*int addressIndex = -1;
                int appliedLabelsIndex = -1;
                int commentIndex = -1;
                int creditStatusIndex = -1;
                int emailIndex = -1;
                int enableSameAdvertiserCompetitiveExclusionIndex = -1;
                int externalIdIndex = -1;
                int faxPhoneIndex = -1;
                int primaryPhoneIndex = -1;
                int thirdPartyCompanyIdIndex = -1;
                int typeIndex = -1;
                int creditStatusSpecifiedIndex = -1;
                int enableSameAdvertiserCompetitiveExclusionSpecifiedIndex = -1;
                int idSpecifiedIndex = -1;
                int lastModifiedDateTimeIndex = -1;
                int thirdPartyCompanyIdSpecifiedIndex = -1;
                int typeSpecifiedIndex = -1;
                int appliedTeamIdsIndex = -1;*/

                for (int i = 0; i < dataTable.Columns.Count; i++)
                {
                    if (dataTable.Columns[i].ColumnName.CompareTo("id") == 0) { idIndex = i + 1; }
                    else if (dataTable.Columns[i].ColumnName.CompareTo("name") == 0) { nameIndex = i + 1; }
                    /*else if (gvBillingCodes.HeaderRow.Cells[i].Text.CompareTo("address") == 0) { addressIndex = i; }
                    else if (gvBillingCodes.HeaderRow.Cells[i].Text.CompareTo("appliedLabels") == 0) { appliedLabelsIndex = i; }
                    else if (gvBillingCodes.HeaderRow.Cells[i].Text.CompareTo("comment") == 0) { commentIndex = i; }
                    else if (gvBillingCodes.HeaderRow.Cells[i].Text.CompareTo("creditStatus") == 0) { creditStatusIndex = i; }
                    else if (gvBillingCodes.HeaderRow.Cells[i].Text.CompareTo("email") == 0) { emailIndex = i; }
                    else if (gvBillingCodes.HeaderRow.Cells[i].Text.CompareTo("enableSameAdvertiserCompetitiveExclusion") == 0) { enableSameAdvertiserCompetitiveExclusionIndex = i; }
                    else if (gvBillingCodes.HeaderRow.Cells[i].Text.CompareTo("externalId") == 0) { externalIdIndex = i; }
                    else if (gvBillingCodes.HeaderRow.Cells[i].Text.CompareTo("faxPhone") == 0) { faxPhoneIndex = i; }
                    else if (gvBillingCodes.HeaderRow.Cells[i].Text.CompareTo("primaryPhone") == 0) { primaryPhoneIndex = i; }
                    else if (gvBillingCodes.HeaderRow.Cells[i].Text.CompareTo("thirdPartyCompanyId") == 0) { thirdPartyCompanyIdIndex = i; }
                    else if (gvBillingCodes.HeaderRow.Cells[i].Text.CompareTo("type") == 0) { typeIndex = i; }
                    else if (gvBillingCodes.HeaderRow.Cells[i].Text.CompareTo("creditStatusSpecified") == 0) { creditStatusSpecifiedIndex = i; }
                    else if (gvBillingCodes.HeaderRow.Cells[i].Text.CompareTo("enableSameAdvertiserCompetitiveExclusionSpecified") == 0) { enableSameAdvertiserCompetitiveExclusionSpecifiedIndex = i; }
                    else if (gvBillingCodes.HeaderRow.Cells[i].Text.CompareTo("idSpecified") == 0) { idSpecifiedIndex = i; }
                    else if (gvBillingCodes.HeaderRow.Cells[i].Text.CompareTo("lastModifiedDateTime") == 0) { lastModifiedDateTimeIndex = i; }
                    else if (gvBillingCodes.HeaderRow.Cells[i].Text.CompareTo("thirdPartyCompanyIdSpecified") == 0) { thirdPartyCompanyIdSpecifiedIndex = i; }
                    else if (gvBillingCodes.HeaderRow.Cells[i].Text.CompareTo("typeSpecified") == 0) { typeSpecifiedIndex = i; }
                    else if (gvBillingCodes.HeaderRow.Cells[i].Text.CompareTo("appliedTeamIds") == 0) { appliedTeamIdsIndex = i; }*/
                }

                int index = Convert.ToInt32(e.CommandArgument);

                String id = "&id=" + gvBillingCodes.Rows[index].Cells[idIndex].Text;
                String name = "&name=" + gvBillingCodes.Rows[index].Cells[nameIndex].Text;
                /*String address = "&address=" + gvBillingCodes.Rows[index].Cells[addressIndex].Text;
                String appliedLabels = "&appliedLabels=" + gvBillingCodes.Rows[index].Cells[appliedLabelsIndex].Text;
                String comment = "&comment=" + gvBillingCodes.Rows[index].Cells[commentIndex].Text;
                String creditStatus = "&creditStatus=" + gvBillingCodes.Rows[index].Cells[creditStatusIndex].Text;
                String email = "&email=" + gvBillingCodes.Rows[index].Cells[emailIndex].Text;
                String enableSameAdvertiserCompetitiveExclusion = "&enableSameAdvertiserCompetitiveExclusion=" + gvBillingCodes.Rows[index].Cells[enableSameAdvertiserCompetitiveExclusionIndex].Text;
                String externalId = "&externalId=" + gvBillingCodes.Rows[index].Cells[externalIdIndex].Text;
                String faxPhone = "&faxPhone=" + gvBillingCodes.Rows[index].Cells[faxPhoneIndex].Text;
                String primaryPhone = "&primaryPhone=" + gvBillingCodes.Rows[index].Cells[primaryPhoneIndex].Text;
                String thirdPartyCompanyId = "&thirdPartyCompanyId=" + gvBillingCodes.Rows[index].Cells[thirdPartyCompanyIdIndex].Text;
                String type = "&type=" + gvBillingCodes.Rows[index].Cells[typeIndex].Text;
                String creditStatusSpecified = "&creditStatusSpecified=" + gvBillingCodes.Rows[index].Cells[creditStatusSpecifiedIndex].Text;
                String enableSameAdvertiserCompetitiveExclusionSpecified = "&enableSameAdvertiserCompetitiveExclusionSpecified=" + gvBillingCodes.Rows[index].Cells[enableSameAdvertiserCompetitiveExclusionSpecifiedIndex].Text;
                String idSpecified = "&idSpecified=" + gvBillingCodes.Rows[index].Cells[idSpecifiedIndex].Text;
                String lastModifiedDateTime = "&lastModifiedDateTime=" + gvBillingCodes.Rows[index].Cells[lastModifiedDateTimeIndex].Text;
                String thirdPartyCompanyIdSpecified = "&thirdPartyCompanyIdSpecified=" + gvBillingCodes.Rows[index].Cells[thirdPartyCompanyIdSpecifiedIndex].Text;
                String typeSpecified = "&typeSpecified=" + gvBillingCodes.Rows[index].Cells[typeSpecifiedIndex].Text;
                String appliedTeamIds = "&appliedTeamIds=" + gvBillingCodes.Rows[index].Cells[appliedTeamIdsIndex].Text;*/
                String sourcePage = "&SourcePage=QABillingCodes";
                Page.Response.Redirect("~/UpdateQABillingCodes.aspx?Action=Update"
                    + id
                    + name
                    /*+ address
                    + appliedLabels
                    + comment
                    + creditStatus
                    + email
                    + enableSameAdvertiserCompetitiveExclusion
                    + externalId
                    + faxPhone
                    + primaryPhone
                    + thirdPartyCompanyId
                    + type
                    + creditStatusSpecified
                    + enableSameAdvertiserCompetitiveExclusionSpecified
                    + idSpecified
                    + lastModifiedDateTime
                    + thirdPartyCompanyIdSpecified
                    + typeSpecified
                    + appliedTeamIds*/
                    + sourcePage);
            }
        }

        protected void gvBillingCodesSorting(object sender, GridViewSortEventArgs e)
        {
            DataTable dataTable = Session["dataTable"] as DataTable;

            //Always sort ascending when sorting by a new column
            if (Session["gvBillingCodesSortExpression"].ToString() != e.SortExpression)
            {
                Session["gvBillingCodesSortDirection"] = "ASC";
            }

            if (dataTable != null)
            {
                DataView dataView = new DataView(dataTable);
                dataView.Sort = e.SortExpression + " " + Session["gvBillingCodesSortDirection"];

                gvBillingCodes.DataSource = dataView;
                gvBillingCodes.DataBind();
            }

            if (Session["gvBillingCodesSortDirection"].ToString() == "ASC")
            {
                Session["gvBillingCodesSortDirection"] = "DESC";
            }
            else
            {
                Session["gvBillingCodesSortDirection"] = "ASC";
            }

            Session["gvBillingCodesSortExpression"] = e.SortExpression;
        }

        protected void ExportCSV_Click(object sender, EventArgs e)
        {
            DataTable dataTable = new DataTable();
            dataTable.Load(DataAccess.executeStoredProcedureWithResults("AMP_usp_D_Company", new SqlParameter[1] { new SqlParameter("@Action", "GetCompaniesWithoutBillingCodes") }, conn: "GoogleDataMart"));

            String fileDate = Convert.ToString(System.DateTime.Today.Year) +
                Convert.ToString(System.DateTime.Today.Month) +
                Convert.ToString(System.DateTime.Today.Day);
            Response.Clear();
            Response.ContentType = "text/csv";
            Response.AddHeader("Content-Disposition", "attachment;filename=\"" + ((LinkButton)sender).Attributes["TableName"] + "_" + fileDate + ".csv\"");
            // write your CSV data to Response.OutputStream here
            StreamWriter streamWriter = new StreamWriter(Response.OutputStream);
            // First we will write the headers.
            int columnCount = dataTable.Columns.Count;
            for (int i = 0; i < columnCount; i++)
            {
                streamWriter.Write(dataTable.Columns[i].ColumnName);
                if (i < columnCount - 1)
                {
                    streamWriter.Write(",");
                }
            }
            // Now write all the rows.
            streamWriter.Write(streamWriter.NewLine);
            foreach (DataRow row in dataTable.Rows)
            {
                for (int i = 0; i < columnCount; i++)
                {
                    if (!Convert.IsDBNull(row[i]))
                    {
                        streamWriter.Write("\"");
                        streamWriter.Write(row[i].ToString());
                        streamWriter.Write("\"");
                    }
                    if (i < columnCount - 1)
                    {
                        streamWriter.Write(",");
                    }
                }
                streamWriter.Write(streamWriter.NewLine);
            }
            Response.End();
            streamWriter.Close();
        }
    }
}