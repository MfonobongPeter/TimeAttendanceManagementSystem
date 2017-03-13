using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using log4net;
using System.Configuration;
using System.Globalization;
using System.Data.SqlClient;
using System.Data;

public partial class Login : System.Web.UI.Page
{
    private static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
    string userRoles = "";
    string IsActive = "";
    EventLog El = new EventLog();
    DateTime dt = DateTime.Now;
    AuditTrail At = new AuditTrail();
    string staffId = "";
    protected void Page_Load(object sender, EventArgs e)
    {
       
    }
    protected void ImgLogin_Click(object sender, ImageClickEventArgs e)
    {
            string userNameInput = Server.HtmlEncode(txtUsername.Text.ToLower().Trim());
            string clientIPAddress = HttpContext.Current.Request.UserHostAddress;
            var commom = new Common();
            commom.SelectUserRecord(userNameInput);
            msgLabel.Text = commom.DspMsg;
            string userName = commom.userName;
            string surName = commom.surName;
            string firstName = commom.firstName;
            staffId = commom.StaffId;
            IsActive = commom.IsActive;
            userRoles = commom.UserRoles;

            //msgLabel.Text = commom.MsgDsp.ToString();
            Session["staffId"] = commom.StaffId;
            Session["userName"] = userName;
            Session["surName"] = surName;
            Session["firstName"] = firstName;
            Session["UserRoles"] = userRoles;
            Session["IsActive"] = IsActive;
            
            try
            {
                checkAuth();
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message + "\n\n\n" + ex.StackTrace);
                //string pageName = System.IO.Path.GetFileName(Request.Url.AbsolutePath);
                //El.EventLogInsert(ex.Message, ex.StackTrace, ex.Source, pageName, dt);
                //msgLabel.Text = ex.Message;
            }
    }

    protected void checkAuth()
    {
        string path = null;
        string user = null;
        string pass = null;
        path = ConfigurationManager.AppSettings["path"];
        user = txtUsername.Text;
        pass = txtPassword.Text;
        try
        {
            string clientIPAddress = HttpContext.Current.Request.UserHostAddress;
            Session["IPAddress"] = clientIPAddress.ToString();
            if (AuthenticateUser(user, pass) == true)
            {
                if (IsActive == "True")
                {
                    At.AuditTrailInsert(staffId, Session["userName"] + " Logged in to the application!", clientIPAddress, "Successful", DateTime.Now);
                    if (userRoles.ToString().Trim().ToLower() == "rco")
                    {
                        Session["LoginRole"] = "RCO";
                        Response.Redirect("~/RcoDashBoard.aspx");
                    }
                    else if (userRoles.ToString().Trim().ToLower() == "audit")
                    {
                        Session["LoginRole"] = "Audit";
                        Response.Redirect("~/AuditDashBoard.aspx");
                    }
                    else if (userRoles.ToString().Trim().ToLower() == "iscontrol")
                    {
                        Session["LoginRole"] = "ISControl";
                        Response.Redirect("~/IsControlDashBoard.aspx");
                    }

                    else
                    {
                        msgLabel.Text = "User Not profiled!";
                    }
                }
                else
                {
                    msgLabel.Text = "User account has been deactived, please contact ISControl!";
                }
            }
            else
            {
                msgLabel.Text = "Incorrect username or password!";
                //lblstatus.Text = "Invalid login account";
                //audit.AuditTrail(nname, "Logging On", "Failed");
            }
        }
        catch (Exception ex)
        {
            Log.Error(ex.Message + "\n\n\n" + ex.StackTrace);
            //string pageName = System.IO.Path.GetFileName(Request.Url.AbsolutePath);
            //El.EventLogInsert(ex.Message, ex.StackTrace, ex.Source, pageName, dt);
            //msgLabel.Text = ex.Message;
        }
    }

    public bool AuthenticateUser(string _username, string _password)
    {
        string ad ="";
        string domain="";
        try
        {
             ad = ConfigurationManager.AppSettings["AD"].ToString();
             domain = ConfigurationManager.AppSettings["Domain"].ToString();
            
        }
        catch (Exception ex)
        {
            Log.Error(ex.Message + "\n\n\n" + ex.StackTrace);
            string pageName = System.IO.Path.GetFileName(Request.Url.AbsolutePath);
            El.EventLogInsert(ex.Message, ex.StackTrace, ex.Source, pageName, dt);
            msgLabel.Text = ex.Message;
        }
        return CardUtil.Authenticate.AuthenticateUserAgainstAD(_username, _password, ad, domain);
    }

    
}