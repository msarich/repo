using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Xml;

namespace DataMart_eCPM_WebInterface
{
    public partial class _Default : System.Web.UI.Page
    {
        private const String NEVER_EXECUTED_STRING = "Never executed.";
        private const String EXECUTING_STRING = "Currently executing, started at ";
        private const String SUCCESSFUL_EXECUTION_STRING = "Last successful execution completed at ";
        private const String FAILED_EXECUTION_STRING = "Last execution failed at ";
        private System.Drawing.Color NEVER_EXECUTED_COLOR = System.Drawing.Color.Blue;
        private System.Drawing.Color EXECUTING_COLOR = System.Drawing.Color.Red;
        private System.Drawing.Color SUCCESSFUL_EXECUTION_COLOR = System.Drawing.Color.Green;
        protected XmlDocument xmlDocument;
        protected String thirdPartyUploadLocation = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            xmlDocument = new XmlDocument();
            xmlDocument.Load(Server.MapPath("~/DynamicSettings.xml"));
            thirdPartyUploadLocation = xmlDocument.GetElementsByTagName("ThirdPartyFileUploadLocation")[0].InnerText;
            if (!IsPostBack)
            {
                ddlMonth.Items.Add(new ListItem("January", "1"));
                ddlMonth.Items.Add(new ListItem("February", "2"));
                ddlMonth.Items.Add(new ListItem("March", "3"));
                ddlMonth.Items.Add(new ListItem("April", "4"));
                ddlMonth.Items.Add(new ListItem("May", "5"));
                ddlMonth.Items.Add(new ListItem("June", "6"));
                ddlMonth.Items.Add(new ListItem("July", "7"));
                ddlMonth.Items.Add(new ListItem("August", "8"));
                ddlMonth.Items.Add(new ListItem("September", "9"));
                ddlMonth.Items.Add(new ListItem("October", "10"));
                ddlMonth.Items.Add(new ListItem("November", "11"));
                ddlMonth.Items.Add(new ListItem("December", "12"));
                ddlMonthGDMNExport.Items.Add(new ListItem("January", "1"));
                ddlMonthGDMNExport.Items.Add(new ListItem("February", "2"));
                ddlMonthGDMNExport.Items.Add(new ListItem("March", "3"));
                ddlMonthGDMNExport.Items.Add(new ListItem("April", "4"));
                ddlMonthGDMNExport.Items.Add(new ListItem("May", "5"));
                ddlMonthGDMNExport.Items.Add(new ListItem("June", "6"));
                ddlMonthGDMNExport.Items.Add(new ListItem("July", "7"));
                ddlMonthGDMNExport.Items.Add(new ListItem("August", "8"));
                ddlMonthGDMNExport.Items.Add(new ListItem("September", "9"));
                ddlMonthGDMNExport.Items.Add(new ListItem("October", "10"));
                ddlMonthGDMNExport.Items.Add(new ListItem("November", "11"));
                ddlMonthGDMNExport.Items.Add(new ListItem("December", "12"));
                ddlMonthGDMN6Export.Items.Add(new ListItem("January", "1"));
                ddlMonthGDMN6Export.Items.Add(new ListItem("February", "2"));
                ddlMonthGDMN6Export.Items.Add(new ListItem("March", "3"));
                ddlMonthGDMN6Export.Items.Add(new ListItem("April", "4"));
                ddlMonthGDMN6Export.Items.Add(new ListItem("May", "5"));
                ddlMonthGDMN6Export.Items.Add(new ListItem("June", "6"));
                ddlMonthGDMN6Export.Items.Add(new ListItem("July", "7"));
                ddlMonthGDMN6Export.Items.Add(new ListItem("August", "8"));
                ddlMonthGDMN6Export.Items.Add(new ListItem("September", "9"));
                ddlMonthGDMN6Export.Items.Add(new ListItem("October", "10"));
                ddlMonthGDMN6Export.Items.Add(new ListItem("November", "11"));
                ddlMonthGDMN6Export.Items.Add(new ListItem("December", "12"));
                if (System.DateTime.Today.Month > 1)
                {
                    ddlMonth.SelectedValue = Convert.ToString(System.DateTime.Today.Month - 1);
                    ddlMonthGDMNExport.SelectedValue = Convert.ToString(System.DateTime.Today.Month - 1);
                    ddlMonthGDMN6Export.SelectedValue = Convert.ToString(System.DateTime.Today.Month - 1);
                    tbYear.Text = Convert.ToString(System.DateTime.Today.Year);
                    tbYearGDMNExport.Text = Convert.ToString(System.DateTime.Today.Year);
                    tbYearGDMN6Export.Text = Convert.ToString(System.DateTime.Today.Year);
                }
                else
                {
                    ddlMonth.SelectedValue = "12";
                    ddlMonthGDMNExport.SelectedValue = "12";
                    ddlMonthGDMN6Export.SelectedValue = "12";
                    tbYear.Text = Convert.ToString(System.DateTime.Today.Year - 1);
                    tbYearGDMNExport.Text = Convert.ToString(System.DateTime.Today.Year - 1);
                    tbYearGDMN6Export.Text = Convert.ToString(System.DateTime.Today.Year - 1);
                }

                PopulateFileList();
            }

            if (Request["refreshStatus"] == "1")
            {
                lblImportStarted.Attributes["style"] = "display:block;";
                lblSelectFileDescription.Attributes["style"] = "display:inline;";
                lbRefreshStatus.Attributes["style"] = "display:inline;";
                rblImportLocation.Attributes["style"] = "display:block;";
                btnImport.Attributes["style"] = "display:block;";
            }
            else
            {
                lblImportStarted.Attributes["style"] = "display:none;";
                lblSelectFileDescription.Attributes["style"] = "display:none;";
                lbRefreshStatus.Attributes["style"] = "display:none;";
                rblImportLocation.Attributes["style"] = "display:none;";
                btnImport.Attributes["style"] = "display:none;";
            }
            updateThirdPartyImportExecutionStatus();
            updateQueryExecutionStatus();
        }

        protected void RefreshFileList(object sender, EventArgs e)
        {
            PopulateFileList();
        }

        private void PopulateFileList()
        {
            List<ListItem> fileList = new List<ListItem>();
            try
            {
                //Go to the upload location and get the list of files.
                ListItem selectFile = new ListItem("Select File", "SelectFile");
                fileList.Add(selectFile);
                string[] filePaths = Directory.GetFiles(thirdPartyUploadLocation, "*.csv", SearchOption.AllDirectories);
                foreach (String filePath in filePaths)
                {
                    fileList.Add(new ListItem(filePath.Substring(filePath.LastIndexOf("\\")+1), filePath));
                }
            }
            catch (Exception e)
            {
                fileList.Clear();
                ListItem selectFile = new ListItem("File Location Unavailable.", "FileLocationUnavailable");
                fileList.Add(selectFile);
            }
            ddlFileList.DataSource = fileList;
            ddlFileList.DataTextField = "Text";
            ddlFileList.DataValueField = "Value";
            ddlFileList.DataBind();
        }

        protected void RefreshStatus(object sender, EventArgs e)
        {
            Response.Redirect("~/Default.aspx?&refreshStatus=1");
        }

        protected void ImportFile(object sender, EventArgs e)
        {
            XmlNodeList importLocations = xmlDocument.GetElementsByTagName("ImportLocation");
            String importJobName = "";
            String sourcePath = (thirdPartyUploadLocation + "\\" + ddlFileList.SelectedItem.Text).Replace(" ","\" \"" );
            String destinationPath = "";
            foreach (XmlNode importLocation in importLocations)
            {
                if (importLocation["Text"].InnerText == rblImportLocation.SelectedItem.Value)
                {
                    importJobName = importLocation["ImportJobName"].InnerText;
                    destinationPath = importLocation["ImportFileLocation"].InnerText.Replace(" ", "\" \"");
                    break;
                }
            }
            SqlParameter[] sqlParameters = new SqlParameter[2];
            sqlParameters[0] = new SqlParameter("@SourcePath", sourcePath);
            sqlParameters[1] = new SqlParameter("@DestinationPath", destinationPath);
            DataAccess.executeStoredProcedureWithoutResults("AMP_CopyFile", sqlParameters);
            DataAccess.runSqlJob(importJobName);
            lblImportStarted.Text = "Import process of file " + ddlFileList.SelectedItem.Text + " to " + rblImportLocation.SelectedItem.Value + " has started.";
            lblImportStarted.Attributes["style"] = "display:block";
            ddlFileList.SelectedIndex = 0;
        }

        private void executeComputeProcedure(String procedureName)
        {
            SqlParameter date = new SqlParameter("@DATE_START", SqlDbType.Date);
            SqlParameter email = new SqlParameter("@EMAIL_RECIPIENT", SqlDbType.VarChar);
            SqlParameter procedure = new SqlParameter("@PROCEDURE_NAME", SqlDbType.VarChar);
            date.Value = new DateTime(Convert.ToInt32(tbYear.Text), Convert.ToInt32(ddlMonth.SelectedValue), 1);
            email.Value = tbEmail.Text;
            procedure.Value = procedureName;
            List<SqlParameter> parameterList = new List<SqlParameter>();
            parameterList.Add(date);
            if (tbEmail.Text.Length > 0)
            {
                parameterList.Add(email);
            }
            parameterList.Add(procedure);
            DataAccess.executeAsyncStoredProcedureWithoutResults("AMP_ExecuteGDMNCreateProcedure", parameterList.ToArray());
        }

        protected void Execute(object sender, EventArgs e)
        {
            if (cbBuildHeliosData.Checked)
            {
                executeComputeProcedure("Create_GDMN_Monthly_AdtechHeliosIQ");
            }
            if (cbCreateGDMN6LevelTable.Checked)
            {
                executeComputeProcedure("Create_GDMN_Monthly_GoogleDFP_5Level");
            }
            if (cbCreateGDMNTable.Checked)
            {
                executeComputeProcedure("Create_GDMN_Monthly_GoogleDFP");
            }  

            Response.Redirect("~/Default.aspx");
        }

        private void updateQueryExecutionStatus()
        {
            setExecutionStringAndColor(DataAccess.SpExecutionStatus("Create_GDMN_Monthly_AdtechHeliosIQ"), lblBuildHeliosDataStatus, cbBuildHeliosData);
            setExecutionStringAndColor(DataAccess.SpExecutionStatus("Create_GDMN_Monthly_GoogleDFP_5Level"), lblCreateGDMN6LevelTable, cbCreateGDMN6LevelTable);
            setExecutionStringAndColor(DataAccess.SpExecutionStatus("Create_GDMN_Monthly_GoogleDFP"), lblCreateGDMNTable, cbCreateGDMNTable);
        }

        private void updateThirdPartyImportExecutionStatus()
        {
            string selectedItemValue = "";
            if (rblImportLocation.SelectedItem != null)
            {
               selectedItemValue = rblImportLocation.SelectedItem.Value;
            }           
            rblImportLocation.Items.Clear();
            //Boolean listWasEmpty = (rblImportLocation.Items.Count == 0);
            SqlDataReader jobsStatus = DataAccess.executeStoredProcedureWithResults("msdb.dbo.sp_help_jobactivity", new SqlParameter[0]);
            XmlNodeList importLocations = xmlDocument.GetElementsByTagName("ImportLocation");

            String jobStatusString = NEVER_EXECUTED_STRING;
            System.Drawing.Color jobStatusColor = NEVER_EXECUTED_COLOR;
            foreach (XmlNode importLocation in importLocations)
            {
                ListItem importLocationListItem = new ListItem(importLocation["Text"].InnerText + " (" + jobStatusString + ")", importLocation["Text"].InnerText);
                importLocationListItem.Attributes.Add("style", "color:" + jobStatusColor.Name);
                importLocationListItem.Selected = (importLocationListItem.Value.CompareTo(selectedItemValue) == 0);
                rblImportLocation.Items.Add(importLocationListItem);
            }
            while (jobsStatus.Read())
            {
                foreach (XmlNode importLocation in importLocations)
                {
                    if (jobsStatus.GetValue(jobsStatus.GetOrdinal("job_name")).ToString().CompareTo(importLocation["ImportJobName"].InnerText) == 0)
                    {
                        if ((jobsStatus.GetValue(jobsStatus.GetOrdinal("start_execution_date")).ToString().CompareTo("") != 0) && (jobsStatus.GetValue(jobsStatus.GetOrdinal("stop_execution_date")).ToString().CompareTo("") == 0))
                        {
                            jobStatusString = EXECUTING_STRING;
                            jobStatusColor = EXECUTING_COLOR;
                        }
                        else if (jobsStatus.GetValue(jobsStatus.GetOrdinal("run_status")).ToString().CompareTo("1") == 0)
                        {
                            jobStatusString = SUCCESSFUL_EXECUTION_STRING + jobsStatus.GetValue(jobsStatus.GetOrdinal("stop_execution_date")).ToString();
                            jobStatusColor = SUCCESSFUL_EXECUTION_COLOR;
                        }
                        else if (jobsStatus.GetValue(jobsStatus.GetOrdinal("run_status")).ToString().CompareTo("0") == 0)
                        {
                            jobStatusString = FAILED_EXECUTION_STRING + jobsStatus.GetValue(jobsStatus.GetOrdinal("stop_execution_date")).ToString(); ;
                            jobStatusColor = EXECUTING_COLOR;
                        }
                        rblImportLocation.Items.FindByValue(importLocation["Text"].InnerText).Text = importLocation["Text"].InnerText + " (" + jobStatusString + ")";
                        rblImportLocation.Items.FindByValue(importLocation["Text"].InnerText).Attributes["style"] = "color:" + jobStatusColor.Name;
                    }
                }
            }
        }

        private void setExecutionStringAndColor(JobStatus jobStatus, Label label, CheckBox checkBox)
        {
            switch (jobStatus.CurrentStatus)
            {
                case JobStatus.Status.NeverExecuted:
                    label.Text = NEVER_EXECUTED_STRING;
                    label.ForeColor = NEVER_EXECUTED_COLOR;
                    checkBox.Enabled = true;
                    break;
                case JobStatus.Status.CurrentlyExecuting:
                    label.Text = EXECUTING_STRING + jobStatus.Datetime + ".";
                    label.ForeColor = EXECUTING_COLOR;
                    checkBox.Enabled = false;
                    break;
                case JobStatus.Status.Executed:
                    label.Text = SUCCESSFUL_EXECUTION_STRING + jobStatus.Datetime + ".";
                    label.ForeColor = SUCCESSFUL_EXECUTION_COLOR;
                    checkBox.Enabled = true;
                    break;
            }
        }



        protected void ExportCSV_Click(object sender, EventArgs e)
        {
            SqlParameter date = new SqlParameter("@DATE_START", SqlDbType.Date);
            DataTable dataTable = new DataTable();
            if(((LinkButton)sender).ID.CompareTo("lbExportGDMN6LevelTableToExcel")==0)
            {
                date.Value = new DateTime(Convert.ToInt32(tbYearGDMN6Export.Text), Convert.ToInt32(ddlMonthGDMN6Export.SelectedValue), 1);
                dataTable.Load(DataAccess.executeStoredProcedureWithResults("AMP_GetGDMN6LevelTable", new SqlParameter[1] { date }));
            }
            else if (((LinkButton)sender).ID.CompareTo("lbExportGDMNTableToExcel") == 0)
            {
                date.Value = new DateTime(Convert.ToInt32(tbYearGDMNExport.Text), Convert.ToInt32(ddlMonthGDMNExport.SelectedValue), 1);
                dataTable.Load(DataAccess.executeStoredProcedureWithResults("AMP_GetGDMNTable", new SqlParameter[1] { date }));
            }
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
