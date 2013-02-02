using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DataMart_eCPM_WebInterface
{
    public partial class LogicControlWhen : System.Web.UI.UserControl
    {
        private List<LogicControlInLike> logicControlInLikes = new List<LogicControlInLike>();

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        public bool showNewInlike()
        {
            bool allAreVisible = true;
            for (int i = 0; i < logicControlInLikes.Count; i++ )
            {
                if (!logicControlInLikes[i].Visible)
                {
                    logicControlInLikes[i].Visible = true;
                    return (i == logicControlInLikes.Count - 1);
                }
            }
            return allAreVisible;
        }

        public string GenerateQuery()
        {
            string query = "";
            foreach (LogicControlInLike logicControlInLike in logicControlInLikes)
            {
                if (logicControlInLike.Visible)
                {
                    query += logicControlInLike.GenerateQuery() + " ";
                }
            }
            int spaceIndex = query.IndexOf(' ');
            query = "When" + query.Substring(spaceIndex);
            return query;
        }

        public void Reset()
        {
            foreach (LogicControlInLike logicControlInLike in logicControlInLikes)
            {
                logicControlInLike.Reset();
                logicControlInLike.Visible = false;
            }
            logicControlInLikes[0].Visible = true;
        }

        [PersistenceMode(PersistenceMode.InnerDefaultProperty)]
        public List<LogicControlInLike> LogicControlInLikes
        {
            get { return logicControlInLikes; }
            set
            {
                logicControlInLikes = value;
                placeholder.Controls.Clear();
                foreach (LogicControlInLike logicControlInLike in logicControlInLikes)
                {
                    placeholder.Controls.Add(logicControlInLike);
                }
            }
        }
    }
}