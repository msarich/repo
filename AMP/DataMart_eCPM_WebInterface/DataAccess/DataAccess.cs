using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace DataMart_eCPM_WebInterface
{
    public static class DataAccess
    {
        private const string CONNECTION_STRING_NAME = "DataMart_eCPM";

        public static void executeStoredProcedureWithoutResults(String storedProcedureName, SqlParameter[] parameters, int timeout = 30, String conn = CONNECTION_STRING_NAME)
        {
            try
            {
                string connectionString = System.Web.Configuration.WebConfigurationManager.ConnectionStrings[conn].ConnectionString;
                SqlConnection sqlConnection = new SqlConnection(connectionString);
                SqlCommand sqlCommand = new SqlCommand(storedProcedureName, sqlConnection);
                sqlCommand.CommandTimeout = timeout;
                sqlCommand.CommandType = CommandType.StoredProcedure;
                foreach (SqlParameter parameter in parameters)
                {
                    sqlCommand.Parameters.Add(parameter);
                }
                sqlConnection.Open();
                sqlCommand.ExecuteNonQuery();
                sqlConnection.Close();
            }
            catch (Exception e)
            {
            }
        }

        public static SqlDataReader executeStoredProcedureWithResults(String storedProcedureName, SqlParameter[] parameters, int timeout = 30, String conn = CONNECTION_STRING_NAME)
        {
            try
            {
                string connectionString = System.Web.Configuration.WebConfigurationManager.ConnectionStrings[conn].ConnectionString;
                SqlConnection sqlConnection = new SqlConnection(connectionString);
                SqlCommand sqlCommand = new SqlCommand(storedProcedureName, sqlConnection);
                sqlCommand.CommandTimeout = timeout;
                sqlCommand.CommandType = CommandType.StoredProcedure;
                foreach (SqlParameter parameter in parameters)
                {
                    sqlCommand.Parameters.Add(parameter);
                }
                sqlConnection.Open();
                return sqlCommand.ExecuteReader(CommandBehavior.CloseConnection);
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public static void executeAsyncStoredProcedureWithoutResults(String storedProcedureName, SqlParameter[] parameters, int timeout = 0, String conn = CONNECTION_STRING_NAME)
        {
            string connectionString = System.Web.Configuration.WebConfigurationManager.ConnectionStrings[conn].ConnectionString + "Async=true;";
            SqlConnection sqlConnection = new SqlConnection(connectionString);
	        try
	        {
		        SqlCommand sqlCommand = new SqlCommand(storedProcedureName, sqlConnection);
                sqlCommand.CommandTimeout = timeout;
		        sqlCommand.CommandType = CommandType.StoredProcedure;
                foreach (SqlParameter parameter in parameters)
                {
                    sqlCommand.Parameters.Add(parameter);
                }
                sqlConnection.Open();
                sqlCommand.BeginExecuteNonQuery(new AsyncCallback(AsyncCommandCompletionCallbackWithoutResults), sqlCommand);
	        }
	        catch(Exception ex)
	        {
		        sqlConnection.Close();
		        throw ex;
	        }
        }

        static void AsyncCommandCompletionCallbackWithoutResults(IAsyncResult result)
        {
            SqlCommand sqlCommand = null;
            try
            {
                // Get our command object from AsyncState, then call EndExecuteNonQuery.
                sqlCommand = (SqlCommand)result.AsyncState;
                sqlCommand.EndExecuteNonQuery(result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                sqlCommand.Connection.Close();
                sqlCommand.Dispose();
            }
        }

        public static JobStatus SpExecutionStatus(String spName)
        {
            JobStatus jobStatus = new JobStatus();
            jobStatus.CurrentStatus = JobStatus.Status.NeverExecuted;
            SqlParameter[] sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@SpName", spName);
            SqlDataReader sqlDataReader = executeStoredProcedureWithResults("AMP_GetSpStatusBySpNameMostRecent", sqlParameters);
            //This should return only one row
            while (sqlDataReader.Read())
            {
                jobStatus.CurrentStatus = JobStatus.Status.CurrentlyExecuting;
                jobStatus.Datetime = sqlDataReader.GetValue(sqlDataReader.GetOrdinal("SpStartDatetime")).ToString().ToLower();
                String SpEndDatetime = sqlDataReader.GetValue(sqlDataReader.GetOrdinal("SpEndDatetime")).ToString().ToLower();
                if (SpEndDatetime.CompareTo("") != 0)
                {
                    jobStatus.Datetime = SpEndDatetime;
                    jobStatus.CurrentStatus = JobStatus.Status.Executed;
                }
            }
            return jobStatus;
        }

        public static String getQueryLastSuccessfulRunDate(String queryName)
        {
            if (queryName == "BuildHeliosDataStoredProcedure")
            {
                return System.DateTime.Today.AddDays(-1.0).ToShortDateString();
            }
            else if (queryName == "CreateGDMNTableStoredProcedure")
            {
                return System.DateTime.Today.AddDays(-3.0).ToShortDateString();
            }
            return "NEVER";   
        }

        public static void runSqlJob(String jobName)
        {
            SqlParameter[] sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("job_name", jobName);
            executeStoredProcedureWithoutResults("msdb.dbo.sp_start_job", sqlParameters);
        }
    }
}