using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DataMart_eCPM_WebInterface
{
    public partial class LogicControlInLike : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        public void Reset()
        {
            ddlCategory.SelectedIndex = 0;
            ddlInOperator.SelectedIndex = 0;
            ddlNotInOperator.SelectedIndex = 0;
            ddlLikeOperator.SelectedIndex = 0;
            ddlNotLikeOperator.SelectedIndex = 0;
            tbInValues.Text = "";
            tbNotInValues.Text = "";
            tbLikeValues.Text = "";
            tbNotLikeValues.Text = "";
        }

        public string GenerateQuery()
        {
            string query = "";
            string category = ddlCategory.SelectedItem.Value;

            string[] inTermList = tbInValues.Text.Split(',');
            if (inTermList.Length > 0)
            {
                query += " " + ddlInOperator.SelectedItem.Text + " " + category + " IN (";
                foreach (string term in inTermList)
                {
                    query += "'" + term + "',";
                }
                query = query.TrimEnd(',') + ")";
            }
            string[] notInTermList = tbNotInValues.Text.Split(',');
            if (notInTermList.Length > 0)
            {
                query += " " + ddlNotInOperator.SelectedItem.Text + " " + category + " NOT IN (";
                foreach (string term in notInTermList)
                {
                    query += "'" + term + "',";
                }
                query = query.TrimEnd(',') + ")";
            }
            string[] likeTermList = tbLikeValues.Text.Split(',');
            if (likeTermList.Length > 0)
            {
                foreach (string term in likeTermList)
                {
                    query += " " + ddlLikeOperator.SelectedItem.Text + " " + category + " LIKE ";
                    query += "'" + term + "'";
                }
            }
            string[] notLikeTermList = tbNotLikeValues.Text.Split(',');
            if (notLikeTermList.Length > 0)
            {
                foreach (string term in notLikeTermList)
                {
                    query += " " + ddlLikeOperator.SelectedItem.Text + " " + category + " NOT LIKE ";
                    query += "'" + term + "'";
                }
            }

            return query.Substring(1);
        }

        public String InValues
        {
            get { return tbInValues.Text; }
            set { tbInValues.Text = value; }
        }

        public String Category
        {
            get { return ddlCategory.SelectedItem.Text; }
        }
    }
}