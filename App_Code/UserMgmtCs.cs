using log4net;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for UserMgmtCs
/// </summary>
public class UserMgmtCs
{
    public string UserId;
    public string Username;
    public string Branch;
    public string UserRole;
    public string DispMsg = "";

    //private static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
    string connStr = ConfigurationManager.ConnectionStrings["RCOAttnRegisterConnectionString"].ConnectionString;
    public void UserMgmtInsert(string _UserId, string _userName,string _SurName,string _FirstName, string _branch, string _userRole)
    {
        try
        {
            string clientIPAddress = HttpContext.Current.Request.UserHostAddress;
            using (var con = new SqlConnection(connStr))
            {
                using (var cmd = new SqlCommand("UserMgmtInsert", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@StaffId", SqlDbType.VarChar, 50).Value = _UserId;
                    cmd.Parameters.Add("@Username", SqlDbType.VarChar, 50).Value = _userName;
                    cmd.Parameters.Add("@Surname", SqlDbType.VarChar, 50).Value = _SurName;
                    cmd.Parameters.Add("@Firstname", SqlDbType.VarChar, 50).Value = _FirstName;
                    cmd.Parameters.Add("@Branch", SqlDbType.VarChar, 50).Value = _branch;
                    cmd.Parameters.Add("@UserRoles", SqlDbType.VarChar, 50).Value = _userRole;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    DispMsg = "User Created Successfully!";
                }
            }
        }
        catch (SqlException ex)
        {
            DispMsg = "User already exist!";
           // Log.Error(ex.Message + "\n\n\n\n" + ex.StackTrace);
        }
    }


    public void UpdateUserRole(string _userName, string _userRole)
    {
        try
        {
            using (var con = new SqlConnection(connStr))
            {
                using (var cmd = new SqlCommand("UpdateRcoRegister", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@UserName", SqlDbType.VarChar, 50).Value = _userName;
                    cmd.Parameters.Add("@UserRoles", SqlDbType.VarChar, 50).Value = _userRole;
                    cmd.Parameters.Add("@SelectOption", SqlDbType.VarChar, 50).Value = "UpdateUserRole";
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    DispMsg = "User record updated successfully!";
                }
            }
        }
        catch (SqlException ex)
        {
            DispMsg = ex.Message;
            //Log.Error(ex.Message + "\n\n\n\n" + ex.StackTrace);
        }
    }

    
    public void UserActivation(string _userName, int _isActive)
    {
        try
        {
            using (var con = new SqlConnection(connStr))
            {
                using (var cmd = new SqlCommand("UpdateRcoRegister", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@UserName", SqlDbType.VarChar, 50).Value = _userName;
                    cmd.Parameters.Add("@IsActive", SqlDbType.VarChar, 50).Value = _isActive;
                    cmd.Parameters.Add("@SelectOption", SqlDbType.VarChar, 50).Value = "UserActivation";
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    DispMsg = "Success";
                }
            }
        }
        catch (SqlException ex)
        {
            DispMsg = ex.Message;
            //Log.Error(ex.Message + "\n\n\n\n" + ex.StackTrace);
        }
    }

    public void SelectExistingUserRole(string _userName)
    {
        try
        {
            SqlDataReader rder = null;
            using (var connection = new SqlConnection(connStr))
            {
                using (var command = new SqlCommand("SelectExistingUserRole", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@Username", SqlDbType.VarChar, 50).Value = _userName;
                    command.Parameters.Add("@SelectOption", SqlDbType.VarChar, 50).Value = "SelectAndPopulateExistingUserRole";
                    connection.Open();
                    rder = command.ExecuteReader();
                    while (rder.Read())
                    {
                        Username = (rder["Username"].ToString());
                        UserRole = (rder["UserRoles"].ToString());
                    }
                }
            }
        }
        catch (SqlException ex)
        {
            DispMsg = ex.Message;
            //Log.Error(ex.InnerException + "\n\n" + ex.StackTrace);
        }
    }
}