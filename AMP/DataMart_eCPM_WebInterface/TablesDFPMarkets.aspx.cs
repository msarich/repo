using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Xml;
using System.IO;

namespace DataMart_eCPM_WebInterface
{
    public partial class TablesDFPMarkets : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                SqlParameter[] parameters = new SqlParameter[1];
                parameters[0] = new SqlParameter("@Action", "GetData");
                SqlDataReader sqlDataReader = DataAccess.executeStoredProcedureWithResults("AMP_usp_GoogleDFP_M_Market", parameters);
                DataTable dataTable = new DataTable();
                dataTable.Load(sqlDataReader);
                gvDFPMarkets.DataSource = dataTable;
                gvDFPMarkets.DataBind();
                lbExportDFPMarketsDataToExcel.Visible = (dataTable.Rows.Count > 0);
                Session.Add("dataTable", dataTable);
                Session.Add("gvDFPMarketsSortDirection", "DESC");
                Session.Add("gvDFPMarketsSortExpression", "ID");
                gvDFPMarketsSorting(null, new GridViewSortEventArgs(Session["gvDFPMarketsSortExpression"].ToString(), SortDirection.Descending));
            }
        }

        protected void AddRow(object sender, EventArgs e)
        {
            Page.Response.Redirect("~/UpdateTablesDFPMarkets.aspx?Action=Add&SourcePage=TablesDFPMarkets");
        }

        protected void UpdateRow(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Update")
            {
                int idIndex = -1;
                int codeIndex = -1;
                int divisionIndex = -1;
                int gdmn_websiteIndex = -1;
                int gdmn_siteIndex = -1;
                int sales_origin_idIndex = -1;
                for (int i = 0; i < gvDFPMarkets.HeaderRow.Cells.Count; i++)
                {
                    if (gvDFPMarkets.Columns[i].HeaderText.CompareTo("ID") == 0) { idIndex = i; }
                    if (gvDFPMarkets.Columns[i].HeaderText.CompareTo("Market Code") == 0) { codeIndex = i; }
                    if (gvDFPMarkets.Columns[i].HeaderText.CompareTo("Division") == 0) { divisionIndex = i; }
                    if (gvDFPMarkets.Columns[i].HeaderText.CompareTo("GDMN Website") == 0) { gdmn_websiteIndex = i; }
                    if (gvDFPMarkets.Columns[i].HeaderText.CompareTo("GDMN Site") == 0) { gdmn_siteIndex = i; }
                    if (gvDFPMarkets.Columns[i].HeaderText.CompareTo("sales_origin_id") == 0) { sales_origin_idIndex = i; }
                }

                int index = Convert.ToInt32(e.CommandArgument);
                String id = "&id=" + gvDFPMarkets.DataKeys[index].Values["ID"].ToString();
                String code = "&code=" + gvDFPMarkets.Rows[index].Cells[codeIndex].Text;
                String division = "&division=" + gvDFPMarkets.Rows[index].Cells[divisionIndex].Text;
                String gdmn_website = "&gdmn_website=" + gvDFPMarkets.Rows[index].Cells[gdmn_websiteIndex].Text;
                String gdmn_site = "&gdmn_site=" + gvDFPMarkets.Rows[index].Cells[gdmn_siteIndex].Text;
                String sales_origin_id = "&sales_origin_id=" + gvDFPMarkets.DataKeys[index].Values["sales_origin_id"].ToString();
                String sourcePage = "&SourcePage=TablesDFPMarkets";
                Page.Response.Redirect("~/UpdateTablesDFPMarkets.aspx?Action=Update" + id + code + division + gdmn_website + gdmn_site + sales_origin_id + sourcePage);
            }
        }

        protected void AppendRecordsFromFile(object sender, EventArgs e)
        {
            String bulkInsertPath = "";
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load(Server.MapPath("~/DynamicSettings.xml"));
            XmlNodeList importLocations = xmlDocument.GetElementsByTagName("BulkImport");
            foreach (XmlNode importLocation in importLocations)
            {
                if(importLocation["Table"].InnerText.CompareTo("GoogleDFP_M_Market")==0)
                {
                    bulkInsertPath = importLocation["FilePath"].InnerText;
                }
            }

            fuFileUpload.SaveAs(bulkInsertPath);
            SqlParameter[] sqlParameters = new SqlParameter[2];
            sqlParameters[0] = new SqlParameter("@Action", "BulkInsert");
            sqlParameters[1] = new SqlParameter("@SkipHeader", cbFileIncludesHeader.Checked);
            DataAccess.executeAsyncStoredProcedureWithoutResults("AMP_usp_GoogleDFP_M_Market", sqlParameters);
            Response.Redirect("~/TablesDFPMarkets.aspx");
        }

        protected void gvDFPMarketsSorting(object sender, GridViewSortEventArgs e)
        {
            DataTable dataTable = Session["dataTable"] as DataTable;

            if (Session["gvDFPMarketsSortDirection"].ToString() == "ASC")
            {
                Session["gvDFPMarketsSortDirection"] = "DESC";
            }
            else
            {
                Session["gvDFPMarketsSortDirection"] = "ASC";
            }

            //Always sort ascending when sorting by a new column
            if (Session["gvDFPMarketsSortExpression"].ToString() != e.SortExpression)
            {
                Session["gvDFPMarketsSortDirection"] = "ASC";
            }

            if (dataTable != null)
            {
                DataView dataView = new DataView(dataTable);
                dataView.Sort = e.SortExpression + " " + Session["gvDFPMarketsSortDirection"];

                gvDFPMarkets.DataSource = dataView;
                gvDFPMarkets.DataBind();
            }

            Session["gvDFPMarketsSortExpression"] = e.SortExpression;
        }

        protected void gvDFPMarketsPageIndexChanging(Object sender, GridViewPageEventArgs e)
        {
            DataTable dataTable = Session["dataTable"] as DataTable;
            DataView dataView = new DataView(dataTable);
            if (Session["gvDFPMarketsSortExpression"].ToString().Length > 0)
            {
                dataView.Sort = Session["gvDFPMarketsSortExpression"] + " " + Session["gvDFPMarketsSortDirection"];
            }
            gvDFPMarkets.DataSource = dataView;
            gvDFPMarkets.PageIndex = e.NewPageIndex;
            gvDFPMarkets.DataBind();
        }

        protected void gvDFPMarketsPageIndexChanged(Object sender, EventArgs e)
        {
        }

        protected void ExportCSV_Click(object sender, EventArgs e)
        {
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter("@Action", "GetData");
            DataTable dataTable = new DataTable();
            dataTable.Load(DataAccess.executeStoredProcedureWithResults("AMP_usp_GoogleDFP_M_Market", parameters));

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