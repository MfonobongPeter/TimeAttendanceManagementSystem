using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class EventLogPage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            FetchEventLogRecords();
        }
    }

    private void FetchEventLogRecords()
    {
        try
        {
            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["RCOAttnRegisterConnectionString"].ConnectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SelectEventLog", con);
                //cmd.CommandTimeout = 900000000;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.ExecuteNonQuery();
                SqlDataAdapter adap = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adap.Fill(ds, "DataSet1");

                // Response.Write(ds.Tables[0].Rows.Count.ToString());

                ReportViewer1.LocalReport.ReportPath = Server.MapPath("Reports/EventLogReport.rdlc");
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
        catch (Exception ex)
        {
            lblNoRecord.Text = ex.Message;
        }
    }

}