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
    public partial class SalesOrigins : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                SqlParameter[] parameters = new SqlParameter[1];
                parameters[0] = new SqlParameter("@Action", "GetData");
                SqlDataReader sqlDataReader = DataAccess.executeStoredProcedureWithResults("AMP_SalesOrigins", parameters);
                DataTable dataTable = new DataTable();
                dataTable.Load(sqlDataReader);
                gvSalesOrigins.DataSource = dataTable;
                gvSalesOrigins.DataBind();
                Session.Add("dataTable", dataTable);
                Session.Add("gvSalesOriginsSortDirection", "ASC");
                Session.Add("gvSalesOriginsSortExpression", "");
            }
        }

        protected void UpdateRow(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Update")
            {
                int primaryIndex = -1;
                int secondaryIndex = -1;
                for (int i = 0; i < gvSalesOrigins.HeaderRow.Cells.Count; i++)
                {
                    if (gvSalesOrigins.Columns[i].HeaderText.CompareTo("Primary") == 0) { primaryIndex = i; }
                    if (gvSalesOrigins.Columns[i].HeaderText.CompareTo("Secondary") == 0) { secondaryIndex = i; }
                }

                int index = Convert.ToInt32(e.CommandArgument);
                String id = "&id=" + gvSalesOrigins.DataKeys[index].Value.ToString();
                String primary = "&primary=" + gvSalesOrigins.Rows[index].Cells[primaryIndex].Text;
                String secondary = "&secondary=" + gvSalesOrigins.Rows[index].Cells[secondaryIndex].Text;
                String sourcePage = "&SourcePage=SalesOrigins";
                Page.Response.Redirect("~/UpdateSalesOrigins.aspx?Action=Update" + id + primary + secondary + sourcePage);
            }
        }

        protected void gvSalesOriginsSorting(object sender, GridViewSortEventArgs e)
        {
            DataTable dataTable = Session["dataTable"] as DataTable;

            //Always sort ascending when sorting by a new column
            if (Session["gvSalesOriginsSortExpression"].ToString() != e.SortExpression)
            {
                Session["gvSalesOriginsSortDirection"] = "ASC";
            }

            if (dataTable != null)
            {
                DataView dataView = new DataView(dataTable);
                dataView.Sort = e.SortExpression + " " + Session["gvSalesOriginsSortDirection"];

                gvSalesOrigins.DataSource = dataView;
                gvSalesOrigins.DataBind();
            }

            if (Session["gvSalesOriginsSortDirection"].ToString() == "ASC")
            {
                Session["gvSalesOriginsSortDirection"] = "DESC";
            }
            else
            {
                Session["gvSalesOriginsSortDirection"] = "ASC";
            }

            Session["gvSalesOriginsSortExpression"] = e.SortExpression;
        }

    }
}