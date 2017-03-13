using log4net;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
//using System.IO;
/// <summary>
/// Summary description for RcoRegister
/// </summary>
public class RcoRegister
{
    
        public string UserName { get; set; }
        public string Surname { get; set; }
        public string Firstname { get; set; }
        public string RTime { get; set; }
        public string CTime { get; set; }
        public DateTime Date { get; set; }
        public string DispMsg;
        public string AttndDate;
        public string IsSignIn;
        
        private static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        string conStr = ConfigurationManager.ConnectionStrings["RCOAttnRegisterConnectionString"].ConnectionString;
        public void RcoSignInInsert(string _userName, string _surName, string _firstName, string _Rtime)
        {
            try
            {
                //string clientIPAddress = HttpContext.Current.Request.UserHostAddress;
                using (var con = new SqlConnection(conStr))
                {
                    using (var cmd = new SqlCommand("RCOATTNDRegisterInsert", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@Username", SqlDbType.VarChar, 50).Value = _userName;
                        cmd.Parameters.Add("@Surname", SqlDbType.VarChar, 50).Value = _surName;
                        cmd.Parameters.Add("@Firstname", SqlDbType.VarChar, 50).Value = _firstName;
                        cmd.Parameters.Add("@Rtime", SqlDbType.VarChar, 50).Value = _Rtime;
                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();
                        DispMsg = "You have successfully signed in for today!";
                    }
                }
            }
            catch (SqlException ex)
            {
                DispMsg = ex.Message;
                Log.Error(ex.Message + "\n\n\n\n" + ex.StackTrace);
                //El.EventLogInsert(ex.Message,ex.StackTrace,ex.Source,,dt);
            }
        }


        public void CheckUserLoginForTheDay(string _userName, DateTime date)
        {
            try
            {
                SqlDataReader rder = null;
                using (var connection = new SqlConnection(conStr))
                {
                    using (var command = new SqlCommand("SelectExistingUserRole", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@Username", SqlDbType.VarChar, 50).Value = _userName;
                        command.Parameters.Add("@ATTNDDATE ", SqlDbType.Date).Value = date.ToShortDateString();
                        command.Parameters.Add("@SelectOption", SqlDbType.VarChar, 50).Value = "CheckUserSignInForTheDay";
                        connection.Open();
                        rder = command.ExecuteReader();
                        while (rder.Read())
                        {
                            UserName = (rder["Username"].ToString());
                            AttndDate = (rder["ATTNDDate"].ToString());
                            IsSignIn = (rder["IsSignIn"].ToString());
                            
                          
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                DispMsg = ex.Message;
                Log.Error(ex.InnerException + "\n\n" + ex.StackTrace);
            }
        }


        public void RcoSignOutUpdate(string _userName, string _CTime, DateTime _date)
        {
            try
            {
                //string clientIPAddress = HttpContext.Current.Request.UserHostAddress;
                using (var con = new SqlConnection(conStr))
                {
                    using (var cmd = new SqlCommand("UpdateRcoRegister", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@Username", SqlDbType.VarChar, 50).Value = _userName;
                        cmd.Parameters.Add("@CTime", SqlDbType.VarChar, 50).Value = _CTime;
                        cmd.Parameters.Add("@ATTNDDate", SqlDbType.Date).Value = _date.ToShortDateString();
                        cmd.Parameters.Add("@SelectOption", SqlDbType.VarChar, 50).Value = "UpdateRCOSignOutTime";
                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();
                        DispMsg = "You have successfully signed out for today!";
                    }
                }
            }
            catch (SqlException ex)
            {
                DispMsg = ex.Message;
                Log.Error(ex.Message + "\n\n\n\n" + ex.StackTrace);
            }
        }






        public void CheckRCOMultipleSignOutForTheDay(string _userName, DateTime date)
        {
            try
            {
                SqlDataReader rder = null;
                using (var connection = new SqlConnection(conStr))
                {
                    using (var command = new SqlCommand("SelectExistingUserRole", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@Username", SqlDbType.VarChar, 50).Value = _userName;
                        command.Parameters.Add("@ATTNDDATE ", SqlDbType.Date).Value = date.ToShortDateString();
                        command.Parameters.Add("@SelectOption", SqlDbType.VarChar, 50).Value = "CheckRCOMultipleSignOutForTheDay";
                        connection.Open();
                        rder = command.ExecuteReader();
                        while (rder.Read())
                        {
                            UserName = (rder["Username"].ToString());
                            AttndDate = (rder["ATTNDDate"].ToString());
                            CTime = (rder["CTime"].ToString());


                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                DispMsg = ex.Message;
                Log.Error(ex.InnerException + "\n\n" + ex.StackTrace);
            }
        }

}