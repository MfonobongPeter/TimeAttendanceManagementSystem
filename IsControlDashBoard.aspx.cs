using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class IsControlDashBoard : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        if (Session["loginRole"] != null)
        {
            string loginRole = Session["loginRole"].ToString();
            if (loginRole == "ISControl")
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