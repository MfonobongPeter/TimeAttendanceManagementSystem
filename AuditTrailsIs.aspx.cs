using log4net;
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

public partial class AuditTrailsIs : System.Web.UI.Page
{
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
            if (loginRole == "ISControl")
            {
                if (!Page.IsPostBack)
                {
                    FetchAuditTrialRecords();
                    DisableUnwantedExportFormat(ReportViewer1, "PDF");
                    DisableUnwantedExportFormat(ReportViewer1, "WORD");
                    string staffId = Session["StaffId"].ToString();
                    At.AuditTrailInsert(staffId, "AuditTrial was viewed", clientIPAddress, "Successful", DateTime.Now);
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


    private void FetchAuditTrialRecords()
    {
        try
        {
            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["RCOAttnRegisterConnectionString"].ConnectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SelectAuditTrail", con);
                //cmd.CommandTimeout = 900000000;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.ExecuteNonQuery();
                SqlDataAdapter adap = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adap.Fill(ds, "DataSet1");

                // Response.Write(ds.Tables[0].Rows.Count.ToString());

                ReportViewer1.LocalReport.ReportPath = Server.MapPath("Reports/RcoAuditTrailReport.rdlc");
                ReportViewer1.ProcessingMode = ProcessingMode.Local;
                ReportViewer1.LocalReport.DataSources.Clear();
                ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", ds.Tables[0]));
                ReportViewer1.LocalReport.Refresh();
                if (ds.Tables[0].Rows.Count == 0)
                {
                    //norecord.Visible = true;
                    //norecord.Text = "No record found";
                }
                else
                {
                    // norecord.Visible = false;
                }

                con.Close();
            }

        }
        catch (Exception ex)
        {
            Log.Error(ex.Message + "\n\n\n" + ex.StackTrace);
            string pageName = System.IO.Path.GetFileName(Request.Url.AbsolutePath);
            El.EventLogInsert(ex.Message, ex.StackTrace, ex.Source, pageName, dt);
            //throw ex;
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