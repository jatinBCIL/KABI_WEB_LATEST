using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using Microsoft.Reporting.WebForms;
using BusinessLayer;
using PropertyLayer;
using System.Globalization;

public partial class Modules_Reports : System.Web.UI.Page
{
    BL_For_Reports objReports = new BL_For_Reports();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            rWeight.Visible = false;
            rDate.Visible = false;
        }

    }

    protected void ddType_SelectedIndexChanged(object sender, EventArgs e)
    {
        rWeight.Visible = true;
        rDate.Visible = true;
        if (ddType.SelectedItem.ToString().ToUpper().Contains("MONTHLY"))
        {
            tdTodate.Visible = true;
        }
        else
        {
            tdTodate.Visible = true;
        }
    }

    protected void btnShow_Click(object sender, EventArgs e)
    {
        string weigID = ""; 
        string fromdt = ""; 
        string todt = "";
        try
        {
            weigID = txtWeighingid.Text;
            fromdt = txtFDate.Text;
            todt = txtTdate.Text;

            DateTime fromdate = DateTime.Parse(txtFDate.Text.Trim());
            DateTime todate = DateTime.Parse(txtTdate.Text.Trim());  //DateTime.ParseExact(txtTdate.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture);
            if (fromdate > todate)
            {
                clsStandards.WriteLog(this, new Exception(" From-Date Should be Before TO-Date "), clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 2, true);
                return;
            }
            Session["WeighID"] = weigID;
            Session["FromDate"] = fromdt;
            Session["ToDate"] = todt;
            Session["ReportType"] = ddType.SelectedValue;

            if (ddType.SelectedItem.ToString().ToUpper().Contains("MONTHLY"))
            {
                int frommonth = fromdate.Month;
                int tomonth = todate.Month;
                if (frommonth != tomonth)
                {
                    clsStandards.WriteLog(this, new Exception(" Please Select Date Range for Single Month "), clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 2, true);
                    return;
                }
                else
                {
                    GetReport_Monthly(weigID, fromdt, todt);
                }
            }
            else
            {
                int days = (fromdate - todate).Days;
                if (days > 7)
                {
                    clsStandards.WriteLog(this, new Exception(" Please select Date range within 7 days "), clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 2, true);
                    return;
                }
                else
                {
                    GetReport_Daily(weigID, fromdt, todt);
                }
            }
        }
        catch (Exception EX)
        {
            clsStandards.WriteLog(this, new Exception(EX.Message), clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 2, true);

        }
    }

    private void GetReport_Monthly(string weigID, string fromdt, string todt)
    {
        objReports = new BL_For_Reports();
        try
        {
            string types = "";
            ReportViewer1.SizeToReportContent = true;
            ReportViewer1.LocalReport.ReportPath = HttpContext.Current.Server.MapPath(ConfigurationManager.AppSettings["ReportPath"].ToString() + "Reports/ReportMonthlyM.rdlc");
            ReportViewer1.LocalReport.DataSources.Clear();

            DataTable dt1 = objReports.BL_MonthReport(weigID, clsStandards.current_Plant(), types = "4 Point Calibration", fromdt, todt);
            if (dt1.Rows.Count > 0)
            {
                Response.Redirect("ReportView.aspx", false);
            }
            else
            {
                clsStandards.WriteLog(this, new Exception("  No Details found  "), clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 2, true);
            }


        }
        catch (Exception ex)
        {
            clsStandards.WriteLog(this, new Exception(ex.Message), clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 2, true);
        }
        finally
        {
            objReports = null;
        }
    }

    private void GetReport_Daily(string weigID, string fromdt, string todt)
    {
        objReports = new BL_For_Reports();
        try
        {
            DataTable dt1 = objReports.BL_DailyReporttable(weigID, clsStandards.current_Plant(), "Daily", fromdt, todt);

            if (dt1.Rows.Count > 0)
            {
                Response.Redirect("ReportView.aspx", false);
            }
            else
            {
                clsStandards.WriteLog(this, new Exception("  No Details found  "), clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 2, true);
            }
        }
        catch (Exception ex)
        {
            clsStandards.WriteLog(this, new Exception(ex.Message), clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 2, true);
        }
        finally
        {
            objReports = null;
        }
    }
    
}
