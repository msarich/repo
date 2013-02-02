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
    public partial class QAMissingMarkets : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                SqlDataReader sqlDataReader = DataAccess.executeStoredProcedureWithResults("AMP_usp_GoogleDFP_M_Market", new SqlParameter[1]{new SqlParameter("@Action", "Missing")});
                DataTable dataTable = new DataTable();
                dataTable.Load(sqlDataReader);
                gvMissingMarkets.DataSource = dataTable;
                gvMissingMarkets.DataBind();
                lbExportMissingMarketsDataToExcel.Visible = (dataTable.Rows.Count > 0);
                Session.Add("dataTable", dataTable);
                Session.Add("gvMissingMarketsSortDirection", "ASC");
                Session.Add("gvMissingMarketsSortExpression", "");
            }
        }

        protected void AddRowToMarket(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Add")
            {
                int stdCmpgnCustomerIndex = -1; //code
                int divisionNameIndex = -1; //division
                int websiteNameIndex = -1; //gdmn_website
                int marketNameIndex = -1; //gdmn_site
                for (int i = 0; i < gvMissingMarkets.HeaderRow.Cells.Count; i++)
                {
                    if (gvMissingMarkets.Columns[i].HeaderText.CompareTo("Customer") == 0) { stdCmpgnCustomerIndex = i; }
                    if (gvMissingMarkets.Columns[i].HeaderText.CompareTo("Division Name") == 0) { divisionNameIndex = i; }
                    if (gvMissingMarkets.Columns[i].HeaderText.CompareTo("Website Name") == 0) { websiteNameIndex = i; }
                    if (gvMissingMarkets.Columns[i].HeaderText.CompareTo("Market Name") == 0) { marketNameIndex = i; }
                }

                int index = Convert.ToInt32(e.CommandArgument);
                String id = "&id=" + gvMissingMarkets.DataKeys[index].Value.ToString();
                String code = "&code=" + gvMissingMarkets.Rows[index].Cells[stdCmpgnCustomerIndex].Text;
                String division = "&division=" + gvMissingMarkets.Rows[index].Cells[divisionNameIndex].Text;
                String gdmn_website = "&gdmn_website=" + gvMissingMarkets.Rows[index].Cells[websiteNameIndex].Text;
                String gdmn_site = "&gdmn_site=" + gvMissingMarkets.Rows[index].Cells[marketNameIndex].Text;
                String sourcePage = "&SourcePage=QAMissingMarkets";
                Page.Response.Redirect("~/UpdateTablesDFPMarkets.aspx?Action=Add" + id + code + division + gdmn_website + gdmn_site + sourcePage);
            }
        }

        protected void gvMissingMarketsSorting(object sender, GridViewSortEventArgs e)
        {
            DataTable dataTable = Session["dataTable"] as DataTable;

            //Always sort ascending when sorting by a new column
            if (Session["gvMissingMarketsSortExpression"].ToString() != e.SortExpression)
            {
                Session["gvMissingMarketsSortDirection"] = "ASC";
            }

            if (dataTable != null)
            {
                DataView dataView = new DataView(dataTable);
                dataView.Sort = e.SortExpression + " " + Session["gvMissingMarketsSortDirection"];

                gvMissingMarkets.DataSource = dataView;
                gvMissingMarkets.DataBind();
            }

            if (Session["gvMissingMarketsSortDirection"].ToString() == "ASC")
            {
                Session["gvMissingMarketsSortDirection"] = "DESC";
            }
            else
            {
                Session["gvMissingMarketsSortDirection"] = "ASC";
            }

            Session["gvMissingMarketsSortExpression"] = e.SortExpression;
        }

        protected void ExportCSV_Click(object sender, EventArgs e)
        {
            DataTable dataTable = new DataTable();
            dataTable.Load(DataAccess.executeStoredProcedureWithResults("AMP_usp_GoogleDFP_M_Market", new SqlParameter[1]{new SqlParameter("@Action", "Missing")}));

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