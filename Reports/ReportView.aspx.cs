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
using System.IO;

public partial class Reports_ReportView : System.Web.UI.Page
{
    BL_For_Reports objReports = new BL_For_Reports();

    protected void Page_Load(object sender, EventArgs e)
    {
        string weigID = Session["WeighID"].ToString();
        string fromdt = Session["FromDate"].ToString();
        string todt = Session["ToDate"].ToString();
        string type = Session["ReportType"].ToString();
        string plantno = HttpContext.Current.Session["MyPlant"].ToString();
        if (!IsPostBack)
        {
            if (type.ToString() == "Montly")
            {
                try
                {
                    string types="";
                    ReportViewer1.SizeToReportContent = true;
                    ReportViewer1.LocalReport.ReportPath = HttpContext.Current.Server.MapPath(ConfigurationManager.AppSettings["ReportPath"].ToString() + "Reports/ReportMonthlyM.rdlc");
                    ReportViewer1.LocalReport.DataSources.Clear();
                    clsStandards.WriteLog(this, new Exception("Monthly Report : 4 Point Calibration"), clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 2, false); 
                    DataTable dt1 = objReports.BL_MonthReport(weigID, plantno, types = "4 Point Calibration", fromdt, todt);
                    clsStandards.WriteLog(this, new Exception("Monthly Report :" + dt1.Rows.Count.ToString()), clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 2, false); 

                    try
                    {
                        clsStandards.WriteLog(this, new Exception("Store Data "), clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 2, false); 
                        if (File.Exists(Server.MapPath("~/App_Data/StoreData.xml")) == true) { File.Delete(Server.MapPath("~/App_Data/StoreData.xml")); }
                        dt1.WriteXml(Server.MapPath("~/App_Data/StoreData.xml"));
                        clsStandards.WriteLog(this, new Exception("Store Data one"), clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 2, false); 
                    }
                    catch { 
                    }

                   
                        string departments = dt1.Rows[0]["Department"].ToString() == "" ? "NA" : dt1.Rows[0]["Department"].ToString();
                        string WEIGHINGCODEs = dt1.Rows[0]["WEIGHINGCODE"].ToString() == "" ? "NA" : dt1.Rows[0]["WEIGHINGCODE"].ToString();
                        string WEIGHTBOXIDs = dt1.Rows[0]["WEIGHTBOXID"].ToString() == "" ? "NA" : dt1.Rows[0]["WEIGHTBOXID"].ToString();
                        string PLANTCODEs = dt1.Rows[0]["PlantDesc"].ToString() == "" ? "NA" : dt1.Rows[0]["PlantDesc"].ToString();
                        string BalanceLevels = dt1.Rows[0]["BalanceLevel"].ToString() == "" ? "NA" : dt1.Rows[0]["BalanceLevel"].ToString();
                        clsStandards.WriteLog(this, new Exception("CalibrationDate Format "), clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 2, false); 
                        string CREATEDONs = dt1.Rows[0]["CalibrationDate"].ToString() == "" ? "NA" : Convert.ToDateTime(dt1.Rows[0]["CalibrationDate"].ToString()).ToString("dd/MM/yyyy");
                        clsStandards.WriteLog(this, new Exception("CalibrationDate Format Done"), clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 2, false); 
                        string WEIGHINGCAPACITYs = dt1.Rows[0]["WEIGHINGCAPACITY"].ToString() == "" ? "NA" : dt1.Rows[0]["WEIGHINGCAPACITY"].ToString();
                        string UOM = dt1.Rows[0]["WEIGHINGUOM"].ToString() == "" ? "NA" : dt1.Rows[0]["WEIGHINGUOM"].ToString();
                        //WEIGHINGCAPACITYs = WEIGHINGCAPACITYs + " " + dt1.Rows[0]["WEIGHINGUOM"].ToString() == "" ? "NA" : dt1.Rows[0]["WEIGHINGUOM"].ToString();
                        string WEIGHTS = WEIGHINGCAPACITYs + " " + UOM;
                        string CERTIFICATENOs = dt1.Rows[0]["CERTIFICATENO"].ToString() == "" ? "NA" : dt1.Rows[0]["CERTIFICATENO"].ToString();
                        string WorkingRanges = dt1.Rows[0]["WorkingRange"].ToString() == "" ? "NA" : dt1.Rows[0]["WorkingRange"].ToString();
                        string ZerroErrors = dt1.Rows[0]["ZerroError"].ToString() == "" ? "NA" : dt1.Rows[0]["ZerroError"].ToString();
                        clsStandards.WriteLog(this, new Exception("DUEDAYS Format "), clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 2, false); 
                        string DUEDAYSs = dt1.Rows[0]["DUEDAYS"].ToString() == "" ? "NA" : Convert.ToDateTime(dt1.Rows[0]["DUEDAYS"].ToString()).ToString("dd/MM/yyyy");
                        clsStandards.WriteLog(this, new Exception("DUEDAYS Format Done"), clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 2, false); 
                        string Doneby = dt1.Rows[0]["CalibratinBy"].ToString() == "" ? "NA" : dt1.Rows[0]["CalibratinBy"].ToString();
                        string Checkby = dt1.Rows[0]["CheckBy"].ToString() == "" ? "NA" : dt1.Rows[0]["CheckBy"].ToString();


                        clsStandards.WriteLog(this, new Exception("Reproducibility Format "), clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 2, false); 
                    DataTable dt2 = objReports.BL_MonthReport(weigID, plantno, types = "Reproducibility", fromdt, todt);
                    clsStandards.WriteLog(this, new Exception("Reproducibility one "), clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 2, false); 

                    try
                    {
                        if (File.Exists(Server.MapPath("~/App_Data/StoreData2.xml")) == true) { File.Delete(Server.MapPath("~/App_Data/StoreData2.xml")); }
                        dt2.WriteXml(Server.MapPath("~/App_Data/StoreData2.xml"));
                    }
                    catch { }

                    string Standarweighttb4 = dt2.Rows[0]["Standarweighttbf"].ToString() == "" ? "0" : dt2.Rows[0]["Standarweighttbf"].ToString();
                    decimal capturedweighttb1 = dt2.Rows[1]["capturedweighttbf"].ToString() == "" ? 0 : decimal.Parse(dt2.Rows[0]["capturedweighttbf"].ToString());
                    decimal capturedweighttb2 = dt2.Rows[2]["capturedweighttbf"].ToString() == "" ? 0 : decimal.Parse(dt2.Rows[1]["capturedweighttbf"].ToString());
                    decimal capturedweighttb3 = dt2.Rows[3]["capturedweighttbf"].ToString() == "" ? 0 : decimal.Parse(dt2.Rows[2]["capturedweighttbf"].ToString());
                    decimal capturedweighttb4 = dt2.Rows[4]["capturedweighttbf"].ToString() == "" ? 0 : decimal.Parse(dt2.Rows[3]["capturedweighttbf"].ToString());
                    decimal capturedweighttb5 = dt2.Rows[5]["capturedweighttbf"].ToString() == "" ? 0 : decimal.Parse(dt2.Rows[4]["capturedweighttbf"].ToString());
                    decimal Meansff = (capturedweighttb1 + capturedweighttb2 + capturedweighttb3 + capturedweighttb4 + capturedweighttb5) / 5;

                    decimal standar1 = capturedweighttb1 * capturedweighttb1 + Meansff * Meansff - 2 * capturedweighttb1 * Meansff;
                    decimal standar2 = capturedweighttb2 * capturedweighttb2 + Meansff * Meansff - 2 * capturedweighttb2 * Meansff;
                    decimal standar3 = capturedweighttb3 * capturedweighttb3 + Meansff * Meansff - 2 * capturedweighttb3 * Meansff;
                    decimal standar4 = capturedweighttb4 * capturedweighttb4 + Meansff * Meansff - 2 * capturedweighttb4 * Meansff;
                    decimal standar5 = capturedweighttb5 * capturedweighttb5 + Meansff * Meansff - 2 * capturedweighttb5 * Meansff;
                    double standarTotal = Convert.ToDouble((standar1 + standar2 + standar3 + standar4 + standar5) / (5 - 1));
                    double rsdvalues1 = Math.Round(Math.Sqrt((double)standarTotal),3);
                    

                    string Standarweighttb5 = dt2.Rows[5]["Standarweighttbf"].ToString() == "" ? "0" : dt2.Rows[5]["Standarweighttbf"].ToString();
                    decimal capturedweighttba1 = dt2.Rows[5]["capturedweighttbf"].ToString() == "" ? 0  : decimal.Parse(dt2.Rows[5]["capturedweighttbf"].ToString());
                    decimal capturedweighttba2 = dt2.Rows[6]["capturedweighttbf"].ToString() == "" ? 0  : decimal.Parse(dt2.Rows[6]["capturedweighttbf"].ToString());
                    decimal capturedweighttba3 = dt2.Rows[7]["capturedweighttbf"].ToString() == "" ? 0  : decimal.Parse(dt2.Rows[7]["capturedweighttbf"].ToString());
                    decimal capturedweighttba4 = dt2.Rows[8]["capturedweighttbf"].ToString() == "" ? 0  : decimal.Parse(dt2.Rows[8]["capturedweighttbf"].ToString());
                    decimal capturedweighttba5 = dt2.Rows[9]["capturedweighttbf"].ToString() == "" ? 0  : decimal.Parse(dt2.Rows[9]["capturedweighttbf"].ToString());
                    decimal Meansffs = (capturedweighttba1 + capturedweighttba2 + capturedweighttba3 + capturedweighttba4 + capturedweighttba5) / 5;

                    decimal standard1 = capturedweighttba1 * capturedweighttba1 + Meansffs * Meansffs - 2 * capturedweighttba1 * Meansffs;
                    decimal standard2 = capturedweighttba2 * capturedweighttba2 + Meansffs * Meansffs - 2 * capturedweighttba2 * Meansffs;
                    decimal standard3 = capturedweighttba3 * capturedweighttba3 + Meansffs * Meansffs - 2 * capturedweighttba3 * Meansffs;
                    decimal standard4 = capturedweighttba4 * capturedweighttba4 + Meansffs * Meansffs - 2 * capturedweighttba4 * Meansffs;
                    decimal standard5 = capturedweighttba5 * capturedweighttba5 + Meansffs * Meansffs - 2 * capturedweighttba5 * Meansffs;
                    double standardTotal = Convert.ToDouble((standard1 + standard2 + standard3 + standard4 + standard5) / (5 - 1));
                    double rsdvalues2 = Math.Round(Math.Sqrt((double)standardTotal),3);

                    DataTable dt3 = objReports.BL_MonthReport(weigID, plantno, types = "Corner Calibration", fromdt, todt);

                    try
                    {
                        if (File.Exists(Server.MapPath("~/App_Data/StoreData3.xml")) == true) { File.Delete(Server.MapPath("~/App_Data/StoreData3.xml")); }
                        dt3.WriteXml(Server.MapPath("~/App_Data/StoreData3.xml"));
                    }
                    catch { }

                    string Standarweighttb3 = dt3.Rows[0]["Standarweighttb3"].ToString() == "" ? "0" : dt3.Rows[0]["Standarweighttb3"].ToString();
                    decimal capturedweighttba = dt3.Rows[0]["capturedweighttb3"].ToString() == "" ? 0 : decimal.Parse(dt3.Rows[0]["capturedweighttb3"].ToString());
                    decimal capturedweighttbb = dt3.Rows[1]["capturedweighttb3"].ToString() == "" ? 0 : decimal.Parse(dt3.Rows[1]["capturedweighttb3"].ToString());
                    decimal capturedweighttbc = dt3.Rows[2]["capturedweighttb3"].ToString() == "" ? 0 : decimal.Parse(dt3.Rows[2]["capturedweighttb3"].ToString());
                    decimal capturedweighttbd = dt3.Rows[3]["capturedweighttb3"].ToString() == "" ? 0 : decimal.Parse(dt3.Rows[3]["capturedweighttb3"].ToString());
                    decimal capturedweighttbe = dt3.Rows[4]["capturedweighttb3"].ToString() == "" ? 0 : decimal.Parse(dt3.Rows[4]["capturedweighttb3"].ToString());
                    decimal Meansffss = (capturedweighttba + capturedweighttbb + capturedweighttbc + capturedweighttbd + capturedweighttbe) / 5;

                    decimal standardevition1 = capturedweighttba * capturedweighttba + Meansffss * Meansffss - 2 * capturedweighttba * Meansffss;
                    decimal standardevition2 = capturedweighttbb * capturedweighttbb + Meansffss * Meansffss - 2 * capturedweighttbb * Meansffss;
                    decimal standardevition3 = capturedweighttbc * capturedweighttbc + Meansffss * Meansffss - 2 * capturedweighttbc * Meansffss;
                    decimal standardevition4 = capturedweighttbd * capturedweighttbd + Meansffss * Meansffss - 2 * capturedweighttbd * Meansffss;
                    decimal standardevition5 = capturedweighttbe * capturedweighttbe + Meansffss * Meansffss - 2 * capturedweighttbe * Meansffss;
                    double standTotal = Convert.ToDouble((standardevition1 + standardevition2 + standardevition3 + standardevition4 + standardevition5) / (5 - 1));
                    double rsdvalues3 = Math.Round(Math.Sqrt((double)standTotal), 3);

                    ReportParameter[] rpParam = new ReportParameter[38];
                    rpParam[0] = new ReportParameter { Name = "HD_Department", Values = { departments } };
                    rpParam[1] = new ReportParameter { Name = "HD_Balanceidno", Values = { WEIGHINGCODEs } };
                    rpParam[2] = new ReportParameter { Name = "HD_Weightboxid", Values = { WEIGHTBOXIDs } };
                    rpParam[3] = new ReportParameter { Name = "HD_Location", Values = { PLANTCODEs } };
                    rpParam[4] = new ReportParameter { Name = "HD_Balanceleveling", Values = { BalanceLevels } };
                    rpParam[5] = new ReportParameter { Name = "HD_Dateverification", Values = { CREATEDONs } };
                    rpParam[6] = new ReportParameter { Name = "HD_Month", Values = { DateTime.Parse(fromdt).ToString("MMMM") } };
                    rpParam[7] = new ReportParameter { Name = "HD_Balancecapacity", Values = { WEIGHTS } };
                    rpParam[8] = new ReportParameter { Name = "HD_Certificateno", Values = { CERTIFICATENOs } };
                    rpParam[9] = new ReportParameter { Name = "HD_Operatingrange", Values = { WorkingRanges } };
                    rpParam[10] = new ReportParameter { Name = "HD_ZeroError", Values = { ZerroErrors } };
                    rpParam[11] = new ReportParameter { Name = "HD_Nextduedate", Values = { DUEDAYSs } };
                    rpParam[12] = new ReportParameter { Name = "DP_StandardWeight_A", Values = { Standarweighttb4 } };
                    rpParam[13] = new ReportParameter { Name = "DP_StandardWeight_B", Values = { Standarweighttb5 } };
                    rpParam[14] = new ReportParameter { Name = "DP_StandWeight_F", Values = { Convert.ToString(capturedweighttb1) } };
                    rpParam[15] = new ReportParameter { Name = "DP_StandWeight_S", Values = { Convert.ToString(capturedweighttb2) } };
                    rpParam[16] = new ReportParameter { Name = "DP_StandWeight_T", Values = { Convert.ToString(capturedweighttb3) } };
                    rpParam[17] = new ReportParameter { Name = "DP_StandWeight_FO", Values = { Convert.ToString(capturedweighttb4) } };
                    rpParam[18] = new ReportParameter { Name = "DP_StandWeight_FI", Values = { Convert.ToString(capturedweighttb5) } };
                    rpParam[19] = new ReportParameter { Name = "DP_Mean_F", Values = { Convert.ToString(Meansff) } };
                    rpParam[20] = new ReportParameter { Name = "DP_RSD_F", Values = { Convert.ToString(rsdvalues1) } };
                    rpParam[21] = new ReportParameter { Name = "DP_StandWeight_FF", Values = { Convert.ToString(capturedweighttba1) } };
                    rpParam[22] = new ReportParameter { Name = "DP_StandWeight_SS", Values = { Convert.ToString(capturedweighttba2) } };
                    rpParam[23] = new ReportParameter { Name = "DP_StandWeight_TT", Values = { Convert.ToString(capturedweighttba3) } };
                    rpParam[24] = new ReportParameter { Name = "DP_StandWeight_FOFO", Values = { Convert.ToString(capturedweighttba4) } };
                    rpParam[25] = new ReportParameter { Name = "DP_StandWeight_FIFI", Values = { Convert.ToString(capturedweighttba5) } };
                    rpParam[26] = new ReportParameter { Name = "DP_Mean_FF", Values = { Convert.ToString(Meansffs) } };
                    rpParam[27] = new ReportParameter { Name = "DP_RSD_FF", Values = { Convert.ToString(rsdvalues2) } };
                    rpParam[28] = new ReportParameter { Name = "DC_StandWeight", Values = { Standarweighttb3 } };
                    rpParam[29] = new ReportParameter { Name = "DC_StandWeight_F", Values = { Convert.ToString(capturedweighttba) } };
                    rpParam[30] = new ReportParameter { Name = "DC_StandWeight_S", Values = { Convert.ToString(capturedweighttbb) } };
                    rpParam[31] = new ReportParameter { Name = "DC_StandWeight_T", Values = { Convert.ToString(capturedweighttbc) } };
                    rpParam[32] = new ReportParameter { Name = "DC_StandWeight_FO", Values = { Convert.ToString(capturedweighttbd) } };
                    rpParam[33] = new ReportParameter { Name = "DC_StandWeight_FI", Values = { Convert.ToString(capturedweighttbe) } };
                    rpParam[34] = new ReportParameter { Name = "DC_Mean_F", Values = { Convert.ToString(Meansffss) } };
                    rpParam[35] = new ReportParameter { Name = "DC_RSD_F", Values = { Convert.ToString(rsdvalues3) } };
                    rpParam[36] = new ReportParameter { Name = "HD_DoneBy", Values = { Convert.ToString(Doneby) } };
                    rpParam[37] = new ReportParameter { Name = "HD_Checkby", Values = { Convert.ToString(Checkby) } };

                    try
                    {
                        if (File.Exists(Server.MapPath("~/App_Data/StoreData1.xml")) == true) { File.Delete(Server.MapPath("~/App_Data/StoreData1.xml")); }
                        dt1.WriteXml(Server.MapPath("~/App_Data/StoreData1.xml"));
                    }
                    catch { }

                    ReportViewer1.LocalReport.DataSources.Clear();
                    ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("tbl_STR_WeighingCalibration", dt1));
                    ReportViewer1.LocalReport.SetParameters(rpParam);
                    ReportViewer1.LocalReport.Refresh();

                    Warning[] warnings;
                    string[] streamIds;
                    string contentType;
                    string encoding;
                    string extension;

                    //Export the RDLC Report to Byte Array.
                    byte[] bytes = ReportViewer1.LocalReport.Render("PDF", null, out contentType, out encoding, out extension, out streamIds, out warnings);

                    // Open generated PDF.
                    Response.Clear();
                    Response.Buffer = true;
                    Response.Charset = "";
                    Response.Cache.SetCacheability(HttpCacheability.NoCache);
                    Response.ContentType = contentType;
                    Response.AddHeader("content-disposition", "attachment; filename=" + "ReportMonthly_" + weigID + "_" + System.DateTime.Now.ToString("ddMMyyyy") + "." + extension);
                    Response.BinaryWrite(bytes);
                    HttpContext.Current.Response.Flush();
                    //HttpContext.Current.Response.End();
                }
                catch (Exception ex)
                {
                    clsStandards.WriteLog(this, new Exception(ex.Message), clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 2, false); 
                }
            }
            else
            {
                try
                {
                    DataTable dt1 = objReports.BL_DailyReporttable(weigID, plantno, type, fromdt, todt);

                    if (dt1.Rows.Count > 0)
                    {
                        
                        ReportViewer1.SizeToReportContent = true;
                        ReportViewer1.LocalReport.ReportPath = HttpContext.Current.Server.MapPath(ConfigurationManager.AppSettings["ReportPath"].ToString() + "Reports/ReportDailyD.rdlc");
                        ReportViewer1.LocalReport.DataSources.Clear();
                        string departments = dt1.Rows[0]["Department"].ToString() == "" ? "NA" : dt1.Rows[0]["Department"].ToString();
                        string WeighingBarcodes = dt1.Rows[0]["WeighingBarcode"].ToString() == "" ? "NA" : dt1.Rows[0]["WeighingBarcode"].ToString();
                        string WEIGHTBOXIDs = dt1.Rows[0]["WEIGHTBOXID"].ToString() == "" ? "NA" : dt1.Rows[0]["WEIGHTBOXID"].ToString();
                        string PLANTCODEs = dt1.Rows[0]["PlantDesc"].ToString() == "" ? "NA" : dt1.Rows[0]["PlantDesc"].ToString();
                        string MINVALUEs = dt1.Rows[0]["MINVALUE"].ToString() == "" ? "NA" : dt1.Rows[0]["MINVALUE"].ToString();
                        string MAXVALUEs = dt1.Rows[1]["MAXVALUE"].ToString() == "" ? "NA" : dt1.Rows[0]["MAXVALUE"].ToString();
                        string WEIGHINGCAPACITYs = dt1.Rows[0]["WEIGHINGCAPACITY"].ToString() == "" ? "NA" : dt1.Rows[0]["WEIGHINGCAPACITY"].ToString();
                        string UOM = dt1.Rows[0]["WEIGHINGUOM"].ToString() == "" ? "NA" : dt1.Rows[0]["WEIGHINGUOM"].ToString();
                        //WEIGHINGCAPACITYs = WEIGHINGCAPACITYs + " " + dt1.Rows[0]["WEIGHINGUOM"].ToString() == "" ? "NA" : dt1.Rows[0]["WEIGHINGUOM"].ToString();
                        string WEIGHTS = WEIGHINGCAPACITYs + " " + UOM;
                        string CERTIFICATENOs = dt1.Rows[0]["CERTIFICATENO"].ToString() == "" ? "NA" : dt1.Rows[0]["CERTIFICATENO"].ToString();
                        string MinRangeFrom = dt1.Rows[0]["values2"].ToString() == "" ? "NA" : dt1.Rows[0]["values2"].ToString();
                        string MinRangeto = dt1.Rows[0]["values3"].ToString() == "" ? "NA" : dt1.Rows[0]["values3"].ToString();
                        string maxRangeFrom = dt1.Rows[0]["values4"].ToString() == "" ? "NA" : dt1.Rows[0]["values4"].ToString();
                        string maxRangeto = dt1.Rows[0]["values5"].ToString() == "" ? "NA" : dt1.Rows[0]["values5"].ToString();



                        ReportParameter[] rpParam = new ReportParameter[13];
                        rpParam[0] = new ReportParameter { Name = "HD_Department", Values = { departments } };
                        rpParam[1] = new ReportParameter { Name = "HD_Balanceidno", Values = { WeighingBarcodes } };
                        rpParam[2] = new ReportParameter { Name = "HD_Weightboxid", Values = { WEIGHTBOXIDs } };
                        rpParam[3] = new ReportParameter { Name = "HD_Location", Values = { PLANTCODEs } };
                        rpParam[4] = new ReportParameter { Name = "HD_Miniweight", Values = { MINVALUEs } };
                        rpParam[5] = new ReportParameter { Name = "HD_Maxweight", Values = { MAXVALUEs } };
                        rpParam[6] = new ReportParameter { Name = "HD_Month", Values = { DateTime.Parse(fromdt).ToString("MMMM") } };
                        rpParam[7] = new ReportParameter { Name = "HD_Balancecapacity", Values = { WEIGHTS } };
                        rpParam[8] = new ReportParameter { Name = "HD_Certificateno", Values = { CERTIFICATENOs } };
                        rpParam[9] = new ReportParameter { Name = "MinRangeFrom", Values = { MinRangeFrom } };
                        rpParam[10] = new ReportParameter { Name = "MinRangeTo", Values = { MinRangeto } };
                        rpParam[11] = new ReportParameter { Name = "MaxRangeFrom", Values = { maxRangeFrom } };
                        rpParam[12] = new ReportParameter { Name = "MaxRangeTo", Values = { maxRangeto } };

                        DataTable dt2 = objReports.BL_DailyReport(weigID, plantno, type, fromdt, todt);
                        ReportViewer1.LocalReport.DataSources.Clear();
                        ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("WeighingCalibrtation_Daily_Rpt", dt2));
                        ReportViewer1.LocalReport.SetParameters(rpParam);
                        ReportViewer1.LocalReport.Refresh();

                        Warning[] warnings;
                        string[] streamIds;
                        string contentType;
                        string encoding;
                        string extension;

                        //Export the RDLC Report to Byte Array.
                        byte[] bytes = ReportViewer1.LocalReport.Render("PDF", null, out contentType, out encoding, out extension, out streamIds, out warnings);

                        // Open generated PDF.
                        Response.Clear();
                        Response.Buffer = true;
                        Response.Charset = "";
                        Response.Cache.SetCacheability(HttpCacheability.NoCache);
                        Response.ContentType = contentType;
                        Response.AddHeader("content-disposition", "attachment; filename=" + "ReportDaily_"+weigID+"_"+System.DateTime.Now.ToString("ddMMyyyy") + "." + extension);
                        Response.BinaryWrite(bytes);
                        HttpContext.Current.Response.Flush();
                       //HttpContext.Current.Response.End();
                    }
                    else
                    {
                        clsStandards.WriteLog(this, new Exception("  No Details found  "), clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 2, false);
                        ClientScript.RegisterStartupScript(this.GetType(), "message", "alert('  No Details found  ')", true);
                        Response.Redirect("Reports.aspx", false);
                    }
                }
                catch (Exception ex)
                {
                    clsStandards.WriteLog(this, new Exception(ex.Message), clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 2, false);

                }
            }
        }
    }
}