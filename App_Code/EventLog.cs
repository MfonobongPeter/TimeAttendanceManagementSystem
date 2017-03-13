using log4net;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for EventLog
/// </summary>
public class EventLog
{
    public int Id { get; set; }
    public string Message { get; set; }
    public string StackTrace { get; set; }
    public string Source { get; set; }
    public string PageName { get; set; }
    public DateTime Date { get; set; }
    public string DispMsg = "";

    private static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
    string strConnString = ConfigurationManager.ConnectionStrings["RCOAttnRegisterConnectionString"].ConnectionString;


    public void EventLogInsert(string _message, string _stackTrace, string _source, string _pageName, DateTime _date)
    {
        using (var con = new SqlConnection(strConnString))
        {
            using (var cmd = new SqlCommand())
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "EventLogInsert";
                cmd.Parameters.Add("@Message", SqlDbType.VarChar, 1000).Value = _message;
                cmd.Parameters.Add("@StackTrace", SqlDbType.VarChar, 4000).Value = _stackTrace;
                cmd.Parameters.Add("@Source", SqlDbType.VarChar, 1000).Value = _source;
                cmd.Parameters.Add("@PageName", SqlDbType.VarChar, 50).Value = _pageName;
                cmd.Parameters.Add("@IncidentDate", SqlDbType.DateTime).Value = _date;
                //cmd.Parameters.Add("@Id", SqlDbType.Int).Direction = ParameterDirection.Output;
                cmd.Connection = con;
                try
                {
                    con.Open();
                    cmd.ExecuteNonQuery();
                    //string id = cmd.Parameters["@Id"].Value.ToString();
                    //DispMsg = "Record inserted successfully. ID = " + id;
                    con.Close();
                }
                catch (Exception ex)
                {
                    DispMsg = ex.Message;
                    Log.Error(ex.Message + "\n\n\n\n" + ex.StackTrace);
                }
            }
        }
    }
}