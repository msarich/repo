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
    public partial class Divisions : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                SqlDataReader sqlDataReader = DataAccess.executeStoredProcedureWithResults("AMP_getDivisions", new SqlParameter[0]);
                DataTable dataTable = new DataTable();
                dataTable.Load(sqlDataReader);
                gvDivisions.DataSource = dataTable;
                gvDivisions.DataBind();
                Session.Add("dataTable", dataTable);
                Session.Add("gvDivisionsSortDirection", "ASC");
                Session.Add("gvDivisionsSortExpression", "");
            }
        }

        protected void AddRow(object sender, EventArgs e)
        {
            Page.Response.Redirect("~/UpdateDivisions.aspx?Action=Add&SourcePage=Divisions");
        }

        protected void UpdateRow(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Update")
            {
                int nameIndex = -1;
                for (int i = 0; i < gvDivisions.HeaderRow.Cells.Count; i++)
                {
                    if (gvDivisions.Columns[i].HeaderText.CompareTo("Division Name") == 0) { nameIndex = i; }
                }

                int index = Convert.ToInt32(e.CommandArgument);
                String id = "&id=" + gvDivisions.DataKeys[index].Value.ToString();
                String name = "&name=" + gvDivisions.Rows[index].Cells[nameIndex].Text;
                String sourcePage = "&SourcePage=Divisions";
                Page.Response.Redirect("~/UpdateDivisions.aspx?Action=Update" + id + name + sourcePage);
            }
        }

        protected void gvDivisionsSorting(object sender, GridViewSortEventArgs e)
        {
            DataTable dataTable = Session["dataTable"] as DataTable;

            //Always sort ascending when sorting by a new column
            if (Session["gvDivisionsSortExpression"].ToString() != e.SortExpression)
            {
                Session["gvDivisionsSortDirection"] = "ASC";
            }

            if (dataTable != null)
            {
                DataView dataView = new DataView(dataTable);
                dataView.Sort = e.SortExpression + " " + Session["gvDivisionsSortDirection"];

                gvDivisions.DataSource = dataView;
                gvDivisions.DataBind();
            }

            if (Session["gvDivisionsSortDirection"].ToString() == "ASC")
            {
                Session["gvDivisionsSortDirection"] = "DESC";
            }
            else
            {
                Session["gvDivisionsSortDirection"] = "ASC";
            }

            Session["gvDivisionsSortExpression"] = e.SortExpression;
        }
    }
}