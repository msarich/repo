using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DataMart_eCPM_WebInterface
{
    public partial class PrimarySalesOrigin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        public void AddTableRow(object sender, EventArgs e)
        {
            btnAddTableRow.Enabled=(!lcilDefault.showNewInlike());
        }

        public void UpdateLogic(object sender, EventArgs e)
        {
            string test = lcilDefault.GenerateQuery();
        }

        public void loadLogic(string salesOrigin)
        {
            lcilDefault.Reset();
        }

        public void ChangeSalesOrigin(object sender, EventArgs e)
        {
            if (ddlSalesOrigin.SelectedIndex != 0)
            {
                btnAddTableRow.Visible = true;
                btnUpdate.Visible = true;
                lcilDefault.Visible = true;
                loadLogic(ddlSalesOrigin.SelectedValue);
            }
            else
            {
                btnAddTableRow.Visible = false;
                btnUpdate.Visible = false;
                lcilDefault.Visible = false;
            }
        }
    }
}