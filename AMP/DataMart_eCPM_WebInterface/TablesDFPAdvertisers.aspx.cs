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
    public partial class TablesDFPAdvertisers : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                SqlParameter[] parameters = new SqlParameter[1];
                parameters[0] = new SqlParameter("@Action", "GetData");
                SqlDataReader sqlDataReader = DataAccess.executeStoredProcedureWithResults("AMP_usp_GoogleDFP_M_Advertiser", parameters);
                DataTable dataTable = new DataTable();
                dataTable.Load(sqlDataReader);
                gvDFPAdvertisers.DataSource = dataTable;
                gvDFPAdvertisers.DataBind();
                lbExportDFPAdvertisersDataToExcel.Visible = (dataTable.Rows.Count > 0);
                Session.Add("dataTable", dataTable);
                Session.Add("gvDFPAdvertisersSortDirection", "ASC");
                Session.Add("gvDFPAdvertisersSortExpression", "");
            }
        }

        protected void AddRow(object sender, EventArgs e)
        {
            Page.Response.Redirect("~/UpdateTablesDFPAdvertisers.aspx?Action=Add&SourcePage=TablesDFPAdvertisers");
        }

        protected void UpdateRow(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Update")
            {
                int nameIndex = -1;
                int matchIndex = -1;
                for (int i = 0; i < gvDFPAdvertisers.HeaderRow.Cells.Count; i++)
                {
                    if (gvDFPAdvertisers.Columns[i].HeaderText.CompareTo("Name") == 0) { nameIndex = i; }
                    if (gvDFPAdvertisers.Columns[i].HeaderText.CompareTo("Match") == 0) { matchIndex = i; }
                }

                int index = Convert.ToInt32(e.CommandArgument);
                String name = "&name=" + System.Web.HttpUtility.UrlEncode(gvDFPAdvertisers.Rows[index].Cells[nameIndex].Text);
                String match = "&match=" + gvDFPAdvertisers.Rows[index].Cells[matchIndex].Text;
                String salesOriginId = "&sales_origin_id=" + gvDFPAdvertisers.DataKeys[index].Values["sales_origin_id"].ToString();
                String sourcePage = "&SourcePage=TablesDFPAdvertisers";
                
                Page.Response.Redirect("~/UpdateTablesDFPAdvertisers.aspx?Action=Update" + name + match + salesOriginId + sourcePage);
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
                if (importLocation["Table"].InnerText.CompareTo("GoogleDFP_M_Advertiser") == 0)
                {
                    bulkInsertPath = importLocation["FilePath"].InnerText;
                }
            }

            fuFileUpload.SaveAs(bulkInsertPath);
            SqlParameter[] sqlParameters = new SqlParameter[2];
            sqlParameters[0] = new SqlParameter("@Action", "BulkInsert");
            sqlParameters[1] = new SqlParameter("@SkipHeader", cbFileIncludesHeader.Checked);
            DataAccess.executeAsyncStoredProcedureWithoutResults("AMP_usp_GoogleDFP_M_Advertiser", sqlParameters);
            Response.Redirect("~/TablesDFPAdvertisers.aspx");
        }

        protected void gvDFPAdvertisersSorting(object sender, GridViewSortEventArgs e)
        {
            DataTable dataTable = Session["dataTable"] as DataTable;

            //Always sort ascending when sorting by a new column
            if (Session["gvDFPAdvertisersSortExpression"].ToString() != e.SortExpression)
            {
                Session["gvDFPAdvertisersSortDirection"] = "ASC";
            }

            if (dataTable != null)
            {
                DataView dataView = new DataView(dataTable);
                dataView.Sort = e.SortExpression + " " + Session["gvDFPAdvertisersSortDirection"];

                gvDFPAdvertisers.DataSource = dataView;
                gvDFPAdvertisers.DataBind();
            }

            if (Session["gvDFPAdvertisersSortDirection"].ToString() == "ASC")
            {
                Session["gvDFPAdvertisersSortDirection"] = "DESC";
            }
            else
            {
                Session["gvDFPAdvertisersSortDirection"] = "ASC";
            }

            Session["gvDFPAdvertisersSortExpression"] = e.SortExpression;
        }

        protected void ExportCSV_Click(object sender, EventArgs e)
        {
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter("@Action", "GetData");
            DataTable dataTable = new DataTable();
            dataTable.Load(DataAccess.executeStoredProcedureWithResults("AMP_usp_GoogleDFP_M_Advertiser", parameters));

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