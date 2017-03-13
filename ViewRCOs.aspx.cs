using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ViewRCOs : System.Web.UI.Page
{
    string username ="";
     DateTime date;
     EventLog El = new EventLog();
     DateTime dt = DateTime.Now;
     string conStr = ConfigurationManager.ConnectionStrings["RCOAttnRegisterConnectionString"].ConnectionString;
     AuditTrail At = new AuditTrail();
     string clientIPAddress = HttpContext.Current.Request.UserHostAddress;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["loginRole"] != null)
        {
            string loginRole = Session["loginRole"].ToString();
            if (loginRole == "ISControl")
            {
                if (!Page.IsPostBack)
                {
                    //System.Threading.Thread.Sleep(3000); 
                    
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

    private void FetchRCORecordBYUsernameAndDate(string _userName, DateTime date)
    {
        try
        {
            using (var con = new SqlConnection(conStr))
            {
                using (var cmd = new SqlCommand("SelectRCOByUsernameAndDate", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@Username", SqlDbType.VarChar, 50).Value = _userName;
                    cmd.Parameters.Add("@AttndDate", SqlDbType.Date).Value = date.ToShortDateString();
                    con.Open();
                    cmd.ExecuteNonQuery();
                    using (var adap = new SqlDataAdapter(cmd))
                    {
                        using (var ds = new DataSet())
                        {
                            adap.Fill(ds, "DataSet1");
                            ReportViewer1.LocalReport.ReportPath = Server.MapPath("Reports/RCOUsernameAndDate.rdlc");
                            ReportViewer1.ProcessingMode = ProcessingMode.Local;
                            ReportViewer1.LocalReport.DataSources.Clear();
                            ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", ds.Tables[0]));
                            ReportViewer1.LocalReport.Refresh();
                            if (ds.Tables[0].Rows.Count == 0)
                            {
                                lblNoRecord.Visible = true;
                                lblNoRecord.Text = "No record found!";
                            }
                            else
                            {
                                lblNoRecord.Visible = false;
                            }
                            con.Close();
                        }
                    }
                }
            }

        }
        catch (Exception ex)
        {
            string pageName = System.IO.Path.GetFileName(Request.Url.AbsolutePath);
            El.EventLogInsert(ex.Message, ex.StackTrace, ex.Source, pageName, dt);
            lblNoRecord.Text = ex.Message;
        }
    }

    private void FetchRCORecordBYDate(DateTime date)
    {
        try
        {
            using (var con = new SqlConnection(conStr))
            {
                using (var cmd = new SqlCommand("SelectRCOByDate", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@AttndDate", SqlDbType.Date).Value = date.ToShortDateString();
                    con.Open();
                    cmd.ExecuteNonQuery();
                    using (var adap = new SqlDataAdapter(cmd))
                    {
                        using (var ds = new DataSet())
                        {
                            adap.Fill(ds, "DataSet1");
                            ReportViewer1.LocalReport.ReportPath = Server.MapPath("Reports/RCOUsernameAndDate.rdlc");
                            ReportViewer1.ProcessingMode = ProcessingMode.Local;
                            ReportViewer1.LocalReport.DataSources.Clear();
                            ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", ds.Tables[0]));
                            ReportViewer1.LocalReport.Refresh();
                            if (ds.Tables[0].Rows.Count == 0)
                            {
                                lblNoRecord.Visible = true;
                                lblNoRecord.Text = "No record found!";
                            }
                            else
                            {
                                lblNoRecord.Visible = false;
                            }
                            con.Close();
                        }
                    }
                }
            }

        }
        catch (Exception ex)
        {
            string pageName = System.IO.Path.GetFileName(Request.Url.AbsolutePath);
            El.EventLogInsert(ex.Message, ex.StackTrace, ex.Source, pageName, dt);
            lblNoRecord.Text = ex.Message;
        }
    }

    private void FetchRCORecordBYUsername(string _userName)
    {
        try
        {
            using (var con = new SqlConnection(conStr))
            {
                using (var cmd = new SqlCommand("SelectRCOByUserName", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@Username", SqlDbType.VarChar, 50).Value = _userName;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    using (var adap = new SqlDataAdapter(cmd))
                    {
                        using (var ds = new DataSet())
                        {
                            adap.Fill(ds, "DataSet1");
                            ReportViewer1.LocalReport.ReportPath = Server.MapPath("Reports/RCOUsernameAndDate.rdlc");
                            ReportViewer1.ProcessingMode = ProcessingMode.Local;
                            ReportViewer1.LocalReport.DataSources.Clear();
                            ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", ds.Tables[0]));
                            ReportViewer1.LocalReport.Refresh();
                            if (ds.Tables[0].Rows.Count == 0)
                            {
                                lblNoRecord.Visible = true;
                                lblNoRecord.Text = "No record found!";
                            }
                            else
                            {
                                lblNoRecord.Visible = false;
                            }
                            con.Close();
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            string pageName = System.IO.Path.GetFileName(Request.Url.AbsolutePath);
            El.EventLogInsert(ex.Message, ex.StackTrace, ex.Source, pageName, dt);
            lblNoRecord.Text = ex.Message;
        }
    }

    private void FetchAllRCORecords()
    {
        try
        {
            using (var con = new SqlConnection(conStr))
            {
                using (var cmd = new SqlCommand("SelectAllRCOs", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    //cmd.Parameters.Add("@Username", SqlDbType.VarChar, 50).Value = _userName;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    using (var adap = new SqlDataAdapter(cmd))
                    {
                        using (var ds = new DataSet())
                        {
                            adap.Fill(ds, "DataSet1");
                            ReportViewer1.LocalReport.ReportPath = Server.MapPath("Reports/RCOUsernameAndDate.rdlc");
                            ReportViewer1.ProcessingMode = ProcessingMode.Local;
                            ReportViewer1.LocalReport.DataSources.Clear();
                            ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", ds.Tables[0]));
                            ReportViewer1.LocalReport.Refresh();
                            if (ds.Tables[0].Rows.Count == 0)
                            {
                                lblNoRecord.Visible = true;
                                lblNoRecord.Text = "No record found!";
                            }
                            else
                            {
                                lblNoRecord.Visible = false;
                            }
                            con.Close();
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            lblNoRecord.Text = ex.Message;
            string pageName = System.IO.Path.GetFileName(Request.Url.AbsolutePath);
            El.EventLogInsert(ex.Message, ex.StackTrace, ex.Source, pageName, dt);
        }
    }
    protected void rdbUsername_CheckedChanged(object sender, EventArgs e)
    {
        lblDate.Visible = false;
        txtDate.Visible = false;
        lblUsername.Visible = true;
        txtUsername.Visible = true;
        btnSearch.Enabled = true;
        btnSearch.Visible = true;
        lblNoRecord.Text = "";
        ReportViewer1.Reset();
        RequiredFieldValidator1.Enabled = true;
        txtUsername.Enabled = true;
        Image2.Visible = false;
        DisableUnwantedExportFormat(ReportViewer1, "PDF");
        DisableUnwantedExportFormat(ReportViewer1, "WORD");
    }
    protected void rdbUsernameAndDate_CheckedChanged(object sender, EventArgs e)
    {
        lblDate.Visible = true;
        lblUsername.Visible = true;
        txtDate.Visible = true;
        txtUsername.Visible = true;
        btnSearch.Enabled = true;
        Image2.Visible = true;
        btnSearch.Visible = true;
        lblNoRecord.Text = "";
        ReportViewer1.Reset();
        RequiredFieldValidator1.Enabled = true;
        txtUsername.Enabled = true;
        DisableUnwantedExportFormat(ReportViewer1, "PDF");
        DisableUnwantedExportFormat(ReportViewer1, "WORD");
    }
    protected void rdbDate_CheckedChanged(object sender, EventArgs e)
    {
        lblUsername.Visible = false;
        txtUsername.Visible = false;
        lblDate.Visible = true;
        txtDate.Visible = true;
        btnSearch.Enabled = true;
        Image2.Visible = true;
        btnSearch.Visible = true;
        lblNoRecord.Text = "";
        ReportViewer1.Reset();
        RequiredFieldValidator1.Enabled = false;
        txtUsername.Enabled = true;
        DisableUnwantedExportFormat(ReportViewer1, "PDF");
        DisableUnwantedExportFormat(ReportViewer1, "WORD");
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        List<string> dateConcatenated = new List<string>();
        string month = "", day = "", year = "", convertedDate = "";
       
            if (rdbUsernameAndDate.Checked)
            {
                if (txtUsername.Text != "")
                {
                    string dateInput = Server.HtmlEncode(txtDate.Text);
                    username = Server.HtmlEncode(txtUsername.Text);
                    try
                    {
                        List<string> dateSplit = new List<string>(dateInput.Split('/'));

                        for (int i = 2; i < dateSplit.Count; i++) // Loop with for.
                        {
                            day = dateSplit[0].ToString();
                            month = dateSplit[1].ToString();
                            year = dateSplit[2].ToString();
                        }
                        convertedDate = year + "-" + month + "-" + day;
                        date = Convert.ToDateTime(convertedDate);
                        FetchRCORecordBYUsernameAndDate(username, date);
                        string staffId = Session["StaffId"].ToString();
                        At.AuditTrailInsert(staffId, "RCO attendance register report was viewed, user selected search by username and date option", clientIPAddress, "Successful", DateTime.Now);
                    }
                    catch (Exception ex)
                    {
                        lblNoRecord.Text = ex.Message;
                        string pageName = System.IO.Path.GetFileName(Request.Url.AbsolutePath);
                        El.EventLogInsert(ex.Message, ex.StackTrace, ex.Source, pageName, dt);
                    }
                }
                else
                {
                    lblNoRecord.Text = "Enter search criteria!";
                }
            }
            else if (rdbUsername.Checked)
            {

                string dateInput = Server.HtmlEncode(txtDate.Text);
                username = Server.HtmlEncode(txtUsername.Text);
                try
                {
                    FetchRCORecordBYUsername(username);
                    string staffId = Session["StaffId"].ToString();
                    At.AuditTrailInsert(staffId, "RCO attendance register report was viewed, user selected search by username option", clientIPAddress, "Successful", DateTime.Now);
                }
                catch (Exception ex)
                {
                    lblNoRecord.Text = ex.Message;
                    string pageName = System.IO.Path.GetFileName(Request.Url.AbsolutePath);
                    El.EventLogInsert(ex.Message, ex.StackTrace, ex.Source, pageName, dt);
                }
            }
            else if (rdbDate.Checked)
            {

                string dateInput = Server.HtmlEncode(txtDate.Text);
                //username = Server.HtmlEncode(txtUsername.Text);
                try
                {
                    List<string> dateSplit = new List<string>(dateInput.Split('/'));

                    for (int i = 2; i < dateSplit.Count; i++) // Loop with for.
                    {
                        day = dateSplit[0].ToString();
                        month = dateSplit[1].ToString();
                        year = dateSplit[2].ToString();
                    }
                    convertedDate = year + "-" + month + "-" + day;
                    date = Convert.ToDateTime(convertedDate);
                    FetchRCORecordBYDate(date);
                    string staffId = Session["StaffId"].ToString();
                    At.AuditTrailInsert(staffId, "RCO attendance register report was viewed, user selected search by date option", clientIPAddress, "Successful", DateTime.Now);
                }
                catch (Exception ex)
                {
                    lblNoRecord.Text = ex.Message;
                    string pageName = System.IO.Path.GetFileName(Request.Url.AbsolutePath);
                    El.EventLogInsert(ex.Message, ex.StackTrace, ex.Source, pageName, dt);
                }
            }    

    }
    protected void RadioButton1_CheckedChanged(object sender, EventArgs e)
    {
        txtUsername.Text = string.Empty;
        txtDate.Text = string.Empty;
        lblUsername.Visible = false;
        txtUsername.Visible = false;
        lblDate.Visible = false;
        txtDate.Visible = false;
        btnSearch.Enabled = false;
        Image2.Visible = false;
        btnSearch.Visible = false;
        
        try
        {
            DisableUnwantedExportFormat(ReportViewer1, "PDF");
            DisableUnwantedExportFormat(ReportViewer1, "WORD");
            FetchAllRCORecords();
            string staffId = Session["StaffId"].ToString();
            At.AuditTrailInsert(staffId, "RCO attendance register report was viewed, user selected search by All option", clientIPAddress, "Successful", DateTime.Now);
        }
        catch (Exception ex)
        {
            lblNoRecord.Text = ex.Message;
            string pageName = System.IO.Path.GetFileName(Request.Url.AbsolutePath);
            El.EventLogInsert(ex.Message, ex.StackTrace, ex.Source, pageName, dt);
        }
    }

    public void DisableUnwantedExportFormat(ReportViewer ReportViewerID, string strFormatName)
    {
        FieldInfo info;

        foreach (RenderingExtension extension in ReportViewerID.LocalReport.ListRenderingExtensions())
        {
            if (extension.Name == strFormatName)
            {
                info = extension.GetType().GetField("m_isVisible", BindingFlags.Instance | BindingFlags.NonPublic);
                info.SetValue(extension, false);
            }
        }
    }
}