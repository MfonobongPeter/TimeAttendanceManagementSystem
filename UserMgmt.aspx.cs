using ActiveDirectoryUtils;
using log4net;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UserMgmt : System.Web.UI.Page
{
    ADUserDetail UserInfo;
    //DataTable dtab = null;
    //int noRS;
    UserMgmtCs Um = new UserMgmtCs();
    string staffBranch = "";
    string staffSurname = "";
    string staffFirstname = "";
    EventLog El = new EventLog();
    DateTime dt = DateTime.Now;
    AuditTrail At = new AuditTrail();
    string clientIPAddress = HttpContext.Current.Request.UserHostAddress;
    //private static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        protected void Page_Load(object sender, EventArgs e)
    {

        if (Session["loginRole"] != null)
        {
            string loginRole = Session["loginRole"].ToString();
            if (loginRole == "ISControl")
            {
                if (!Page.IsPostBack)
                {
                    System.Threading.Thread.Sleep(2000);
                    bindGrid();
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
    
    protected void txtUserName_TextChanged(object sender, EventArgs e)
    {
        if (txtUserName.Text != "")
        {
            if (DoesUserExist(txtUserName.Text) == true)
            {
                try
                {
                    UserInfo = new ActiveDirectoryUtils.ActiveDirectoryUtil().GetUserByLoginName(txtUserName.Text.Trim());
                    lblUserIdDsp.Text = UserInfo.Company;
                    staffBranch = UserInfo.PhysicalDeliveryOfficename;
                    Session["staffBranch"] = staffBranch;
                    staffFirstname = UserInfo.FirstName;
                    Session["staffFirstname"] = staffFirstname;
                    staffSurname = UserInfo.LastName;
                    Session["staffSurname"] = staffSurname;
                    string UsernameTextInput = "";
                    UsernameTextInput = Server.HtmlEncode(txtUserName.Text).ToLower();
                    lblMsg.Text = "";
                    Um.SelectExistingUserRole(UsernameTextInput);
                    string userName = Um.Username.ToString().ToLower();
                    string userRoles = Um.UserRole.ToString().ToLower();

                    if (userName != "")
                    {
                        if (userName == UsernameTextInput && userRoles == "iscontrol")
                        {
                            RadioButtonList1.SelectedIndex = 2;
                        }
                        else if (userName == UsernameTextInput && userRoles == "audit")
                        {
                            RadioButtonList1.SelectedIndex = 1;
                        }
                        else if (userName == UsernameTextInput && userRoles == "rco")
                        {
                            RadioButtonList1.SelectedIndex = 0;
                        }
                    }
                    else
                    {
                        lblMsg.Text = "New user!";
                        lblMsg.ForeColor = System.Drawing.Color.Red;
                        RadioButtonList1.SelectedIndex = -1;

                    }
                }
                catch (Exception)
                {
                    if (lblUserIdDsp.Text != "")
                    {
                        lblMsg.Text = "This user has not been profiled yet! ";
                        lblMsg.ForeColor = System.Drawing.Color.Red;
                        RadioButtonList1.SelectedIndex = -1;
                    }
                    else
                    {
                        lblMsg.Text = "User does not exist!";
                        lblUserIdDsp.Text = "";
                        lblMsg.ForeColor = System.Drawing.Color.Red;
                        RadioButtonList1.SelectedIndex = -1;
                    }
                }
            }
            else if (DoesUserExist(txtUserName.Text) == false)
            {
                lblMsg.Text = "This user does not exist!";
                lblUserIdDsp.Text = "";
                lblMsg.ForeColor = System.Drawing.Color.Red;
                RadioButtonList1.SelectedIndex = -1;
            }
        }
        else
        {
            lblMsg.Text = "Please enter Username!";
            lblMsg.ForeColor = System.Drawing.Color.Red;
        }
    }


    public bool DoesUserExist(string userName)
    {
        string ad = ConfigurationManager.AppSettings["AD"].ToString();
        string ImpUsername = ConfigurationManager.AppSettings["ImpUsername"].ToString();
        string ImpPassword = ConfigurationManager.AppSettings["ImpPassword"].ToString();
        using (var domainContext = new PrincipalContext(ContextType.Domain, ad, ImpUsername, ImpPassword))
        {
            using (var foundUser = UserPrincipal.FindByIdentity(domainContext, IdentityType.SamAccountName, userName))
            {
                return foundUser != null;
            }
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        string userRole = "";
        string UsernameTextInput = Server.HtmlEncode(txtUserName.Text);
        try
        {
            if (txtUserName.Text != "")
            {
                if (RadioButtonList1.SelectedIndex != -1)
                {
                    if (DoesUserExist(UsernameTextInput) == true)
                    {
                        string userId = lblUserIdDsp.Text;
                        string userName = Server.HtmlEncode(txtUserName.Text);
                        string branch = "", surNameSession = "", firstNameSession = "";
                        try
                        {
                            branch = Session["staffBranch"].ToString();
                            surNameSession = Session["staffSurname"].ToString();
                            firstNameSession = Session["staffFirstname"].ToString();

                        }
                        catch (Exception ex)
                        {
                            //Log.Error(ex.Message + "\n\n\n" + ex.StackTrace);
                            string pageName = System.IO.Path.GetFileName(Request.Url.AbsolutePath);
                            El.EventLogInsert(ex.Message, ex.StackTrace, ex.Source, pageName, dt);
                            lblMsg.Text = "User does not exist!";
                            lblUserIdDsp.Text = "";
                            lblMsg.ForeColor = System.Drawing.Color.Red;
                            RadioButtonList1.SelectedIndex = -1;
                        }

                        Um.UserId = userId;
                        Um.Username = userName;
                        Um.Branch = branch;

                        foreach (ListItem li in RadioButtonList1.Items)
                        {
                            if (li.Selected)
                            {
                                userRole = li.Text.ToLower();
                                Um.UserRole = userRole;
                            }
                        }
                        Um.UserMgmtInsert(userId, userName, surNameSession, firstNameSession, branch, userRole);
                        //GridView1.DataBind();
                        lblMsg.Text = Um.DispMsg;
                        lblMsg.ForeColor = System.Drawing.Color.Green;
                        string staffId = Session["StaffId"].ToString();
                        At.AuditTrailInsert(staffId, "User profile created, profile username: " + txtUserName.Text, clientIPAddress, "Successful", DateTime.Now);
                    }
                    else if (DoesUserExist(UsernameTextInput) == false)
                    {
                        lblMsg.Text = "This user does not exist!";
                        lblUserIdDsp.Text = "";
                        lblMsg.ForeColor = System.Drawing.Color.Red;
                        RadioButtonList1.SelectedIndex = -1;
                    }
                }
                else
                {
                    lblMsg.Text = "Please select user role!";
                }
            }
            else
            {
                lblMsg.Text = "Please enter Username!";
                lblMsg.ForeColor = System.Drawing.Color.Red;
            }
        }
        catch (Exception ex)
        {
                //Log.Error(ex.Message + "\n\n\n" + ex.StackTrace);
                string pageName = System.IO.Path.GetFileName(Request.Url.AbsolutePath);
                El.EventLogInsert(ex.Message, ex.StackTrace, ex.Source, pageName, dt);
                lblMsg.Text = ex.Message;
                lblMsg.ForeColor = System.Drawing.Color.Red;
        }
    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        string userRole = "";
        string UsernameTextInput = Server.HtmlEncode(txtUserName.Text);
        if (txtUserName.Text != "")
        {
            if (RadioButtonList1.SelectedIndex != -1)
            {
                try
                {
                    if (DoesUserExist(UsernameTextInput) == true)
                    {
                        string userName = Server.HtmlEncode(txtUserName.Text);
                        foreach (ListItem li in RadioButtonList1.Items)
                        {
                            if (li.Selected)
                            {
                                userRole = li.Text;
                                Um.UserRole = userRole;
                            }
                        }
                        try
                        {
                            Um.SelectExistingUserRole(UsernameTextInput);
                            string userNameDb = Um.Username.ToString().ToLower().Trim();
                            string userRoles = Um.UserRole.ToString().ToLower();
                            string userNameCompare = UsernameTextInput.ToLower().Trim();
                            if (userNameDb == userNameCompare)
                            {
                                Um.UpdateUserRole(userName, userRole);
                                //GridView1.DataBind();
                                lblMsg.Text = Um.DispMsg;
                                lblMsg.ForeColor = System.Drawing.Color.Green;
                                string staffId = Session["StaffId"].ToString();
                                At.AuditTrailInsert(staffId, "User profile updated, profile username: " + txtUserName.Text, clientIPAddress, "Successful", DateTime.Now);
                            }
                            else
                            {
                                lblMsg.Text = "Update failed, user not profiled yet!";
                            }
                        }
                        catch (Exception)
                        {
                            lblMsg.Text = "Update failed, user not profiled yet!";
                        }
                    }
                    else if (DoesUserExist(UsernameTextInput) == false)
                    {
                        lblMsg.Text = "Update failed! user does not exist!";
                        lblMsg.ForeColor = System.Drawing.Color.Red;
                        lblUserIdDsp.Text = "";
                    }
                }
                catch (Exception ex)
                {
                    //Log.Error(ex.Message + "\n\n\n" + ex.StackTrace);
                    string pageName = System.IO.Path.GetFileName(Request.Url.AbsolutePath);
                    El.EventLogInsert(ex.Message, ex.StackTrace, ex.Source, pageName, dt);
                    lblMsg.Text = ex.Message;
                    lblMsg.ForeColor = System.Drawing.Color.Red;
                }
            }
            else
            {
                lblMsg.Text = "Select user role!";
            }
        }
        else
        {
            lblMsg.Text = "Please enter Username!";
            lblMsg.ForeColor = System.Drawing.Color.Red;
        }
    }


    private void bindGrid()
    {
        string strConnString = ConfigurationManager.ConnectionStrings["RCOAttnRegisterConnectionString"].ConnectionString;
        SqlConnection con = new SqlConnection(strConnString);
        try
        {
            con.Open();
            SqlCommand com = new SqlCommand("SelectUserMgmtProfile", con);
            com.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(com);
            DataSet ds = new DataSet();
            da.Fill(ds, "UserMgmt");
            GridView1.DataSource = ds.Tables[0];
            GridView1.DataBind();
            con.Close();
            //lbldisplay.Text = "records are found";
        }
        catch(Exception ex) 
        {
            lblMsg.Text = ex.Message;
            lblMsg.ForeColor = System.Drawing.Color.Red;
        }
    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        lblMsg.Text = "";
        txtUserName.Text = "";
        lblUserIdDsp.Text = "";

            if (e.CommandName == "activate")
            {
                // Retrieve the row index stored in the 
                // CommandArgument property.
                int index = Convert.ToInt32(e.CommandArgument);

                // Retrieve the row that contains the button 
                // from the Rows collection.
                GridViewRow row = GridView1.Rows[index];
                string username = Server.HtmlDecode(row.Cells[2].Text);
                Um.UserActivation(username, 1);
                lblUserActivation.Text = Um.DispMsg+", "+ username +" account activated";

            }

            if (e.CommandName == "deactivate")
            {
                // Retrieve the row index stored in the 
                // CommandArgument property.
                int index = Convert.ToInt32(e.CommandArgument);

                // Retrieve the row that contains the button 
                // from the Rows collection.
                GridViewRow row = GridView1.Rows[index];
                string username = Server.HtmlDecode(row.Cells[2].Text);
                Um.UserActivation(username, 0);
                lblUserActivation.Text = Um.DispMsg+", "+ username +" account deactivated";;
                
            }
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string isActiveValue = e.Row.Cells[8].Text;

            foreach (TableCell cell in e.Row.Cells)
            {
                if (isActiveValue == "NO")
                {
                    cell.BackColor = Color.Red;
                }
                if (isActiveValue == "YES")
                {
                    cell.BackColor = Color.White;
                }
                
            }
        }
    }
}