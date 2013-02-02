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
    public partial class QAMissingATF_BTF : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            SqlDataReader sqlDataReader = DataAccess.executeStoredProcedureWithResults("AMP_Get_GDMN_ATF_BTF_Missing", new SqlParameter[0]);
            DataTable dataTable = new DataTable();
            dataTable.Load(sqlDataReader);
            gvATF_BTFMissing.DataSource = dataTable;
            gvATF_BTFMissing.DataBind();
            lbExportATF_BTFMissingDataToExcel.Visible = (dataTable.Rows.Count > 0);
            if (!IsPostBack)
            {
                Session.Add("gvATF_BTFMissingSortDirection", "ASC");
                Session.Add("gvATF_BTFMissingSortExpression", "");
            }
        }

        protected void gvATF_BTFMissingSorting(object sender, GridViewSortEventArgs e)
        {
            DataTable dataTable = gvATF_BTFMissing.DataSource as DataTable;

            //Always sort ascending when sorting by a new column
            if (Session["gvATF_BTFMissingSortExpression"].ToString() != e.SortExpression)
            {
                Session["gvATF_BTFMissingSortDirection"] = "ASC";
            }

            if (dataTable != null)
            {
                DataView dataView = new DataView(dataTable);
                dataView.Sort = e.SortExpression + " " + Session["gvATF_BTFMissingSortDirection"];

                gvATF_BTFMissing.DataSource = dataView;
                gvATF_BTFMissing.DataBind();
            }

            if (Session["gvATF_BTFMissingSortDirection"].ToString() == "ASC")
            {
                Session["gvATF_BTFMissingSortDirection"] = "DESC";
            }
            else
            {
                Session["gvATF_BTFMissingSortDirection"] = "ASC";
            }

            Session["gvATF_BTFMissingSortExpression"] = e.SortExpression;
        }

        protected void ExportCSV_Click(object sender, EventArgs e)
        {

            DataTable dataTable = new DataTable();
            dataTable.Load(DataAccess.executeStoredProcedureWithResults("AMP_Get_GDMN_ATF_BTF_Missing", new SqlParameter[0]));

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