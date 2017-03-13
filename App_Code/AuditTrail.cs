using log4net;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for AuditTrail
/// </summary>
public class AuditTrail
{
	
    public int Id { get; set; }
    public string UserId { get; set; }
    public string Activity { get; set; }
    public string IPAddress { get; set; }
    public string Status { get; set; }
    public DateTime Date { get; set; }
    public string DispMsg = "";

    private static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);


    public void AuditTrailInsert(string _staffId, string _activity, string _iPAddress, string _status,DateTime _date)
    {
        try
        {
            string clientIPAddress = HttpContext.Current.Request.UserHostAddress;
            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["RCOAttnRegisterConnectionString"].ConnectionString))
            {
                using (var cmd = new SqlCommand("AuditTrailInsert", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@StaffId", SqlDbType.VarChar, 50).Value = _staffId;
                    cmd.Parameters.Add("@Activity", SqlDbType.VarChar, 200).Value = _activity;
                    cmd.Parameters.Add("@IPAddress", SqlDbType.VarChar, 50).Value = _iPAddress;
                    cmd.Parameters.Add("@Status", SqlDbType.VarChar, 50).Value = _status;
                    cmd.Parameters.Add("@ActionDate", SqlDbType.DateTime).Value = _date;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
        }
        catch (SqlException ex)
        {
            DispMsg = ex.Message;
            Log.Error(ex.Message + "\n\n\n\n" + ex.StackTrace);
        }
    }
}