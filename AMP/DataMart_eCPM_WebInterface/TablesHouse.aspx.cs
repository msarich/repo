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
    public partial class TablesHouse : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                SqlParameter[] parameters = new SqlParameter[1];
                parameters[0] = new SqlParameter("@Action", "GetData");
                SqlDataReader sqlDataReader = DataAccess.executeStoredProcedureWithResults("AMP_usp_M_House", parameters);
                DataTable dataTable = new DataTable();
                dataTable.Load(sqlDataReader);
                gvHouse.DataSource = dataTable;
                gvHouse.DataBind();
                lbExportHouseDataToExcel.Visible = (dataTable.Rows.Count > 0);
                Session.Add("dataTable", dataTable);
                Session.Add("gvHouseSortDirection", "ASC");
                Session.Add("gvHouseSortExpression", "");
            }
        }

        protected void AddRow(object sender, EventArgs e)
        {
            Page.Response.Redirect("~/UpdateTablesHouse.aspx?Action=Add&SourcePage=TablesHouse");
        }

        protected void UpdateRow(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Update")
            {
                int nameIndex = -1;
                int isHouseIndex = -1;
                for (int i = 0; i < gvHouse.HeaderRow.Cells.Count; i++)
                {
                    if (gvHouse.Columns[i].HeaderText.CompareTo("Advertiser") == 0) { nameIndex = i; }
                    if (gvHouse.Columns[i].HeaderText.CompareTo("Is House") == 0) { isHouseIndex = i; }
                }

                int index = Convert.ToInt32(e.CommandArgument);
                String name = "&name=" + gvHouse.Rows[index].Cells[nameIndex].Text;
                String isHouse = "&is_house=" + gvHouse.Rows[index].Cells[isHouseIndex].Text;
                String sourcePage = "&SourcePage=TablesHouse";
                Page.Response.Redirect("~/UpdateTablesHouse.aspx?Action=Update" + name + isHouse + sourcePage);
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
                if (importLocation["Table"].InnerText.CompareTo("M_House") == 0)
                {
                    bulkInsertPath = importLocation["FilePath"].InnerText;
                }
            }

            fuFileUpload.SaveAs(bulkInsertPath);
            SqlParameter[] sqlParameters = new SqlParameter[2];
            sqlParameters[0] = new SqlParameter("@Action", "BulkInsert");
            sqlParameters[1] = new SqlParameter("@SkipHeader", cbFileIncludesHeader.Checked);
            DataAccess.executeAsyncStoredProcedureWithoutResults("AMP_usp_M_House", sqlParameters);
            Response.Redirect("~/TablesHouse.aspx");
        }

        protected void gvHouseSorting(object sender, GridViewSortEventArgs e)
        {
            DataTable dataTable = Session["dataTable"] as DataTable;

            //Always sort ascending when sorting by a new column
            if (Session["gvHouseSortExpression"].ToString() != e.SortExpression)
            {
                Session["gvHouseSortDirection"] = "ASC";
            }

            if (dataTable != null)
            {
                DataView dataView = new DataView(dataTable);
                dataView.Sort = e.SortExpression + " " + Session["gvHouseSortDirection"];

                gvHouse.DataSource = dataView;
                gvHouse.DataBind();
            }

            if (Session["gvHouseSortDirection"].ToString() == "ASC")
            {
                Session["gvHouseSortDirection"] = "DESC";
            }
            else
            {
                Session["gvHouseSortDirection"] = "ASC";
            }

            Session["gvHouseSortExpression"] = e.SortExpression;
        }

        protected void ExportCSV_Click(object sender, EventArgs e)
        {
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter("@Action", "GetData");
            DataTable dataTable = new DataTable();
            dataTable.Load(DataAccess.executeStoredProcedureWithResults("AMP_usp_M_House", parameters));

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