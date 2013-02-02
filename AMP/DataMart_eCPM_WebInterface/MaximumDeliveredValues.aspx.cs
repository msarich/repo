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
    public partial class MaximumDeliveredValues : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            SqlDataReader sqlDataReader = DataAccess.executeStoredProcedureWithResults("AMP_usp_MaxDeliveredValues", new SqlParameter[1]{new SqlParameter("Action", "GetData")});
            DataTable dataTable = new DataTable();
            dataTable.Load(sqlDataReader);
            gvMaximumDeliveredValues.DataSource = dataTable;
            gvMaximumDeliveredValues.DataBind();
            if (!IsPostBack)
            {
                Session.Add("gvMaximumDeliveredValuesSortDirection", "ASC");
                Session.Add("gvMaximumDeliveredValuesSortExpression", "");
            }
        }

        protected void gvMaximumDeliveredValuesSorting(object sender, GridViewSortEventArgs e)
        {
            DataTable dataTable = gvMaximumDeliveredValues.DataSource as DataTable;

            //Always sort ascending when sorting by a new column
            if (Session["gvMaximumDeliveredValuesSortExpression"].ToString() != e.SortExpression)
            {
                Session["gvMaximumDeliveredValuesSortDirection"] = "ASC";
            }

            if (dataTable != null)
            {
                DataView dataView = new DataView(dataTable);
                dataView.Sort = e.SortExpression + " " + Session["gvMaximumDeliveredValuesSortDirection"];

                gvMaximumDeliveredValues.DataSource = dataView;
                gvMaximumDeliveredValues.DataBind();
            }

            if (Session["gvMaximumDeliveredValuesSortDirection"].ToString() == "ASC")
            {
                Session["gvMaximumDeliveredValuesSortDirection"] = "DESC";
            }
            else
            {
                Session["gvMaximumDeliveredValuesSortDirection"] = "ASC";
            }

            Session["gvMaximumDeliveredValuesSortExpression"] = e.SortExpression;
        }

        protected void ViewData(object sender, EventArgs e)
        {
        }
    }
}