using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class AuditDashBoard : System.Web.UI.Page
{
    AuditTrail At = new AuditTrail();
    protected void Page_Load(object sender, EventArgs e)
    {

        if (Session["loginRole"] != null)
        {
            string loginRole = Session["loginRole"].ToString();
            if (loginRole == "Audit")
            {
                
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
}