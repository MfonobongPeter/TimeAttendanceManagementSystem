using log4net;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class RCODashBoard : System.Web.UI.Page
{
    
    RcoRegister rco = new RcoRegister();
    string userName = "", surName = "", firstName = "",rTime="";
    EventLog El = new EventLog();
    DateTime dt = DateTime.Now;
    AuditTrail At = new AuditTrail();
    string clientIPAddress = HttpContext.Current.Request.UserHostAddress;

    private static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

    protected void Page_Load(object sender, EventArgs e)
    {
        
        if (Session["loginRole"] != null)
        {
            string loginRole = Session["loginRole"].ToString();
            if (loginRole == "RCO")
            {
                //System.Threading.Thread.Sleep(3000);
                DateTime date = DateTime.Now;
                try
                {
                    userName = Session["userName"].ToString();
                    surName = Session["surName"].ToString();
                    firstName = Session["firstName"].ToString();

                    rco.CheckUserLoginForTheDay(userName, date);
                    string  chkSignInTrue = rco.IsSignIn;
                    string userNameDb = rco.UserName;

                    

                    rTime = DateTime.Parse(date.ToString())
                    .ToString("hh:mm tt", System.Globalization.CultureInfo.CurrentCulture);
                    //DateTime date5 = DateTime.Parse("08:01:00 PM");
                    DateTime date1 = Convert.ToDateTime(ConfigurationManager.AppSettings["OfficialResumptionTime"]);
                    int result = DateTime.Compare(date, date1);

                    if (result <= 0)
                    {
                        //lblMsg.ForeColor = System.Drawing.Color.Green;
                       // chkSignIn.Enabled = true;
                        if (chkSignInTrue == "True")
                        {
                            lblMsg.Text = "You have signed in for today! Click on sign out if you want to sign out.";
                            lblMsg.ForeColor = System.Drawing.Color.Green;
                            chkSignIn.Checked = true;
                            chkSignIn.Enabled = false;
                        }
                        else
                        {
                            chkSignIn.Enabled = true;
                        }
                    }
                    else if (result > 0)
                    {
                        lblMsg.Text = "Sorry, you can't sign in now! Sign in closes at 8:00 AM";
                        chkSignIn.Enabled = false;
                        lblMsg.ForeColor = System.Drawing.Color.Red;

                    }
                }
                catch (Exception ex)
                {
                    Log.Error(ex.Message + "\n\n\n" + ex.StackTrace);
                    string pageName = System.IO.Path.GetFileName(Request.Url.AbsolutePath);
                    El.EventLogInsert(ex.Message, ex.StackTrace, ex.Source, pageName, dt);
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
            if (chkSignIn.Checked)
            {
                rco.RcoSignInInsert(userName, surName, firstName, rTime);
                lblMsg.Text = rco.DispMsg.ToString();
                lblMsg.ForeColor = System.Drawing.Color.Green;
                chkSignIn.Enabled = false;
                string staffId = Session["StaffId"].ToString();
                At.AuditTrailInsert(staffId, Session["userName"]+" marked attendance register (sign in)!", clientIPAddress, "Successful", DateTime.Now);
            }
        }
        catch (SqlException ex)
        {
            Log.Error(ex.Message + "\n\n\n" + ex.StackTrace);
            string pageName = System.IO.Path.GetFileName(Request.Url.AbsolutePath);
            El.EventLogInsert(ex.Message, ex.StackTrace, ex.Source, pageName, dt);
        }
    }
}