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
    public partial class ATF_BTF_Mappings : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                SqlParameter[] parameters = new SqlParameter[1];
                parameters[0] = new SqlParameter("@Action", "GetData");
                SqlDataReader sqlDataReader = DataAccess.executeStoredProcedureWithResults("AMP_usp_ATF_BTF_Mappings", parameters, conn:"HeliosDataMart_Fresh");
                DataTable dataTable = new DataTable();
                dataTable.Load(sqlDataReader);
                gvATFBTFMappings.DataSource = dataTable;
                gvATFBTFMappings.DataBind();
                lbExportSalesOriginsDataToExcel.Visible = (dataTable.Rows.Count > 0);
                Session.Add("dataTable", dataTable);
                Session.Add("gvATFBTFMappingsSortDirection", "ASC");
                Session.Add("gvATFBTFMappingsSortExpression", "");
            }
        }

        protected void AddRow(object sender, EventArgs e)
        {
            Page.Response.Redirect("~/UpdateATF_BTF_Mappings.aspx?Action=Add&SourcePage=ATF_BTF_Mappings");
        }

        protected void UpdateRow(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Update")
            {
                int positionIndex = -1;
                int atf_btfIndex = -1;
                int usatsiteIndex = -1;
                for (int i = 0; i < gvATFBTFMappings.Columns.Count; i++)
                {
                    if (gvATFBTFMappings.Columns[i].HeaderText.CompareTo("Position") == 0) { positionIndex = i; }
                    if (gvATFBTFMappings.Columns[i].HeaderText.CompareTo("ATF_BTF") == 0) { atf_btfIndex = i; }
                    if (gvATFBTFMappings.Columns[i].HeaderText.CompareTo("USAT SITE") == 0) { usatsiteIndex = i; }
                }

                int index = Convert.ToInt32(e.CommandArgument);
                String aTF_BTF_Mappings_Id = "&ATF_BTF_Mappings_Id=" + gvATFBTFMappings.DataKeys[index].Value.ToString();
                String position = "&Position=" + gvATFBTFMappings.Rows[index].Cells[positionIndex].Text;
                String atf_btf = "&ATF_BTF=" + gvATFBTFMappings.Rows[index].Cells[atf_btfIndex].Text;
                String usatsite = "&USATSITE=" + gvATFBTFMappings.Rows[index].Cells[usatsiteIndex].Text;
                String sourcePage = "&SourcePage=ATF_BTF_Mappings";
                Page.Response.Redirect("~/UpdateATF_BTF_Mappings.aspx?Action=Update" + aTF_BTF_Mappings_Id + position + atf_btf + usatsite + sourcePage);
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
                if (importLocation["Table"].InnerText.CompareTo("ATF_BTF_Mappings") == 0)
                {
                    bulkInsertPath = importLocation["FilePath"].InnerText;
                }
            }

            fuFileUpload.SaveAs(bulkInsertPath);
            SqlParameter[] sqlParameters = new SqlParameter[2];
            sqlParameters[0] = new SqlParameter("@Action", "BulkInsert");
            sqlParameters[1] = new SqlParameter("@SkipHeader", cbFileIncludesHeader.Checked);
            DataAccess.executeAsyncStoredProcedureWithoutResults("AMP_usp_ATF_BTF_Mappings", sqlParameters, conn: "HeliosDataMart_Fresh");
            Response.Redirect("~/ATF_BTF_Mappings.aspx");
        }

        protected void gvATFBTFMappingsSorting(object sender, GridViewSortEventArgs e)
        {
            DataTable dataTable = Session["dataTable"] as DataTable;

            //Always sort ascending when sorting by a new column
            if (Session["gvATFBTFMappingsSortExpression"].ToString() != e.SortExpression)
            {
                Session["gvATFBTFMappingsSortDirection"] = "ASC";
            }

            if (dataTable != null)
            {
                DataView dataView = new DataView(dataTable);
                dataView.Sort = e.SortExpression + " " + Session["gvATFBTFMappingsSortDirection"];

                gvATFBTFMappings.DataSource = dataView;
                gvATFBTFMappings.DataBind();
            }

            if (Session["gvATFBTFMappingsSortDirection"].ToString() == "ASC")
            {
                Session["gvATFBTFMappingsSortDirection"] = "DESC";
            }
            else
            {
                Session["gvATFBTFMappingsSortDirection"] = "ASC";
            }

            Session["gvATFBTFMappingsSortExpression"] = e.SortExpression;
        }

        protected void ExportCSV_Click(object sender, EventArgs e)
        {
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter("@Action", "GetData");
            DataTable dataTable = new DataTable();
            dataTable.Load(DataAccess.executeStoredProcedureWithResults("AMP_usp_ATF_BTF_Mappings", parameters, conn:"HeliosDataMart_Fresh"));

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