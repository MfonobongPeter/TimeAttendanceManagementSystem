using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class SignOut : System.Web.UI.Page
{
    string userName = "", CTime="";
    RcoRegister rco = new RcoRegister();
    DateTime date ;
    EventLog El = new EventLog();
    DateTime dt = DateTime.Now;
    //private static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
    AuditTrail At = new AuditTrail();
    string clientIPAddress = HttpContext.Current.Request.UserHostAddress;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["loginRole"] != null)
        {
            string loginRole = Session["loginRole"].ToString();
            if (loginRole == "RCO")
            {
                
                date = DateTime.Now;
                userName = Session["userName"].ToString();
                //System.Threading.Thread.Sleep(3000);
                CTime = DateTime.Parse(date.ToString())
                    .ToString("hh:mm tt", System.Globalization.CultureInfo.CurrentCulture);

                rco.CheckRCOMultipleSignOutForTheDay(userName, date);
                string CTimeDb = rco.CTime;
                if (CTimeDb != "Nill")
                {
                    if (CTimeDb != "")
                    {
                        if (CTimeDb != null)
                        {
                            chkSignIn.Enabled = false;
                            chkSignIn.Checked = true;
                            lblMsg.Text = "You have signed out for today!";
                            lblMsg.ForeColor = System.Drawing.Color.Green;
                        }
                        else
                        {
                            chkSignIn.Enabled = true;
                        }
                    }
                    else
                    {
                        chkSignIn.Enabled = true;
                    }
                }
                else if (CTimeDb == "Nill")
                {
                    chkSignIn.Enabled = true;
                }
                
            }
            else
            {
                Response.Redirect("~/login.aspx");
            }
        }
        else
        {
            Response.Redirect("~/login.aspx");
        }
    }
    protected void chkSignIn_CheckedChanged(object sender, EventArgs e)
    {      
        try
        {
            rco.CheckUserLoginForTheDay(userName, date);
            string chkSignInTrue = rco.IsSignIn;
            string userNameDb = rco.UserName;
            if (chkSignInTrue == "True")
            {
                rco.RcoSignOutUpdate(userName, CTime, date);
                lblMsg.Text = rco.DispMsg;
                lblMsg.ForeColor = System.Drawing.Color.Green;
                chkSignIn.Enabled = false;
                string staffId = Session["StaffId"].ToString();
                At.AuditTrailInsert(staffId, Session["userName"] + "marked attendance register (sign out)!", clientIPAddress, "Successful", DateTime.Now);
            }
            else
            {
                lblMsg.Text = "You haven't signed in for today, please sign in before you sign out.";
                lblMsg.ForeColor = System.Drawing.Color.Red;
                chkSignIn.Enabled = false;
            }
        }
        catch (Exception ex)
        {
            //Log.Error(ex.Message + "\n\n\n" + ex.StackTrace);
            string pageName = System.IO.Path.GetFileName(Request.Url.AbsolutePath);
            El.EventLogInsert(ex.Message, ex.StackTrace, ex.Source, pageName, dt);
            lblMsg.Text = ex.Message;
        }

    }
}