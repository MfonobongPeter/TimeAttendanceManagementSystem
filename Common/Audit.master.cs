using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Common_Audit : System.Web.UI.MasterPage
{
    EventLog El = new EventLog();
    DateTime dt = DateTime.Now;
    //private static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
    AuditTrail At = new AuditTrail();
    string clientIPAddress = HttpContext.Current.Request.UserHostAddress;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            DateTime date = DateTime.Now;
            lblDateTime.Text = DateTime.Parse(date.ToString()).ToString("MMMM dd, yyyy - hh:mm tt", System.Globalization.CultureInfo.CurrentCulture);
            lblUserNameDisp.Text = "Welcome: " + Session["firstname"].ToString().ToUpper() + " " + Session["surname"].ToString().ToUpper();
        }
        catch (Exception ex)
        {
            //Log.Error(ex.Message + "\n\n\n" + ex.StackTrace);
            string pageName = System.IO.Path.GetFileName(Request.Url.AbsolutePath);
            El.EventLogInsert(ex.Message, ex.StackTrace, ex.Source, pageName, dt);
        }
    }

    
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        string staffId = Session["StaffId"].ToString();
        At.AuditTrailInsert(staffId, Session["userName"] +" logged out from the application!", clientIPAddress, "Successful", DateTime.Now);
        Session.Remove("userName");
        Session.Remove("loginRole");
        Session.Clear();
        Session.Abandon();
        Session["userName"] = null;
        Session["loginRole"] = null;
        FormsAuthentication.SignOut();
        Response.Redirect("~/Login.aspx");
    }
}
