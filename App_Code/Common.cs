using log4net;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Common
/// </summary>
public class Common
{
        public string StaffId;
        public string userName;
        public string surName;
        public string firstName;
        public string UserRoles;
        public string IsActive;
        public string DspMsg;

        private static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        string strConnString = ConfigurationManager.ConnectionStrings["RCOAttnRegisterConnectionString"].ConnectionString;
        string clientIPAddress = HttpContext.Current.Request.UserHostAddress;

        AuditTrail At = new AuditTrail();
        public void SelectUserRecord(string _userName)
        {
            try
            {
                SqlDataReader rder = null;
                using (var connection = new SqlConnection(strConnString))
                {
                    using (var command = new SqlCommand("SelectUserDetailOnLogin", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@Username", SqlDbType.VarChar, 50).Value = _userName.ToLower();
                        connection.Open();
                        rder = command.ExecuteReader();
                        while (rder.Read())
                        {
                            StaffId = (rder["StaffId"].ToString());
                            userName = (rder["Username"].ToString().ToLower());
                            surName = (rder["Surname"].ToString());
                            firstName = (rder["Firstname"].ToString());
                            UserRoles = (rder["UserRoles"].ToString());
                            IsActive = (rder["IsActive"].ToString());
                            if (userName == _userName)
                            {                              
                                    DspMsg = "Login Successful!";    
                            }
                        }
                        if (userName != _userName)
                        {
                            DspMsg = "Incorrect username or password!";
                            At.AuditTrailInsert(StaffId, "Invalid User Login Attempt!", clientIPAddress, "UnSuccessful", DateTime.Now);
                        }
                        
                    }
                }
            }
            catch (SqlException ex)
            {
                DspMsg = ex.Message;
                Log.Error(ex.InnerException + "\n\n" + ex.StackTrace);
            }
        }


       
}