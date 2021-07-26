using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using System.Data.OleDb;
using DataLayer;
using PropertyLayer;
using BusinessLayer;
using System.Configuration;
using System.DirectoryServices;

public partial class frmDispensingReprinting : System.Web.UI.Page
{
    
    BL_PickList objGrn = new BL_PickList();
    BL_GrnPrinting objPrint = new BL_GrnPrinting();
    public static string strFlg = "";
    DataTable dtImport = new DataTable();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            BL_PrinterMaster objPrint = new BL_PrinterMaster();
            DataTable dt = new DataTable();
            DataTable dt_Plant = new DataTable();
            DataTable dt_AscPlant = new DataTable();
            DataSet ds = new DataSet();
            ViewState["dtStatus"] = null;
            try
            {
                clsStandards.fillCombobox(ddlPrinterName, objPrint.BL_PrinterCodes(clsStandards.current_Plant()), "PRINTERCODE");

            }
            catch (Exception ex)
            {
                clsStandards.WriteLog(this, ex, clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 0, true);
            }
            finally
            {
                objPrint = null;
            }
            MultiView1.SetActiveView(View1);
            strFlg = "ADD";

        }
    }

    protected void btnGet_Click(object sender, EventArgs e)
    {
        objGrn = new BL_PickList();
        DataTable dtGrn = new DataTable();
        try
        {
            dtGrn = objGrn.BL_GetProcessOrderReprinting(txtProcessOrderNo.Text.Trim(), "GET", clsStandards.current_Plant().ToString());
            if (dtGrn.Rows.Count > 0)
            {
                GrGRNDetails.DataSource = dtGrn;
                ViewState["dtStatus"] = dtGrn;
                GrGRNDetails.DataBind();
            }
            else
            {
                GrGRNDetails.DataSource = null;
                GrGRNDetails.DataBind();
                clsStandards.WriteLog(this, new Exception("No details found for document"), clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 0, true);

            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.ToString());
        }
        finally
        {
            objGrn = null;
        }

    }

    protected void btnPrint11_Click(object sender, EventArgs e)
    {

    }


    protected void btnClose_Click(object sender, EventArgs e)
    {
        try
        {
            HttpContext.Current.Response.Redirect(ResolveUrl("~/Modules/frmMain.aspx"), false);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.ToString());
        }
        finally
        {
        }
    }

    protected void btnPrint_Click(object sender, EventArgs e)
    {
        objGrn = new BL_PickList();
        objPrint = new BL_GrnPrinting();
        DataTable dtPrint = new DataTable();
        string strPrinter = "";
        bool bFlag = false;
        try
        {
            if (ddlPrinterName.Text.Trim().ToUpper().Contains("SELECT"))
            {
                clsStandards.WriteLog(this, new Exception("Select Printer"), clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 0, true);
                return;
            }
            if (GrGRNDetails.Rows.Count > 0)
            {
                for (int i = 0; i < GrGRNDetails.Rows.Count; i++)
                {
                    CheckBox cb = (CheckBox)GrGRNDetails.Rows[i].FindControl("chkSelect");
                    if (cb.Checked == true)
                    {
                         strPrinter = objPrint.BL_GetPrinterIP(ddlPrinterName.Text.Trim(), clsStandards.current_Plant());
                         if (PrintDirect.CheckConnection(strPrinter.Split('=').GetValue(0).ToString(), strPrinter.Split('=').GetValue(1).ToString()) == false)
                         {
                             clsStandards.WriteLog(this, new Exception("Printed IP not Connected"), clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 0, true);
                             return;
                         }
                        dtPrint = objGrn.BL_GetProcessOrderReprinting(GrGRNDetails.Rows[i].Cells[1].Text.ToString(), "REPRINT", clsStandards.current_Plant().ToString());


                        if (dtPrint.Columns.Count > 0)
                        {
                           
                            for (int j = 0; j < dtPrint.Rows.Count; j++)
                            {

                            //    clsStandards.WriteLog(this, new Exception("Item Code:" + dtPrint.Rows[0]["ItemCode"].ToString().Trim()), clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 0, true);
                                bFlag = PrintDirect.Print_DispenseRawLabel(
                                   ddlPrinterName.Text.Trim(),
                                   dtPrint.Rows[j]["BarcodeNo"].ToString(),
                                   dtPrint.Rows[j]["ItemCode"].ToString(),
                                    (dtPrint.Rows[0]["ItemCode"].ToString().Trim() == "7960011522" ? "Silicone Platinum Braided Tube 12.7x20mm" : dtPrint.Rows[0]["IteemDesc"].ToString()),
                                   dtPrint.Rows[j]["MaterialBatch"].ToString(),
                                   dtPrint.Rows[j]["MfgDate"].ToString(),
                                   dtPrint.Rows[j]["ExpDate"].ToString(),
                                   dtPrint.Rows[j]["ProcessOrder"].ToString(),
                                   dtPrint.Rows[j]["ProdCode"].ToString(),
                                   dtPrint.Rows[j]["ProdBatch"].ToString(),
                                   dtPrint.Rows[j]["ProductDesc"].ToString(),
                                  Convert.ToDecimal(dtPrint.Rows[j]["GrossWt"].ToString()),
                                  Convert.ToDecimal(dtPrint.Rows[j]["NetWt"].ToString()),
                                  Convert.ToDecimal(dtPrint.Rows[j]["TareWt"].ToString()),
                                   dtPrint.Rows[j]["CreatedBy"].ToString(),
                                   dtPrint.Rows[j]["CreatedOn"].ToString(),
                                   dtPrint.Rows[j]["UOM"].ToString(),
                                   dtPrint.Rows[j]["PlantCode"].ToString(),
                                   dtPrint.Rows[j]["BatchSize"].ToString(),
                                   dtPrint.Rows[j]["ARNo"].ToString(),
                                   dtPrint.Rows[j]["ContainerNo"].ToString(), strPrinter.Split('=').GetValue(0).ToString(), strPrinter.Split('=').GetValue(1).ToString(), dtPrint.Rows[j]["VerifiedBy"].ToString());

                                if (bFlag == true)
                                {
                                    cb.Enabled = false;
                                    cb.Checked = false;
                                    GrGRNDetails.Rows[i].BackColor = System.Drawing.Color.LightYellow;
                                    clsStandards.WriteLog(this, new Exception("Barcode Printed Successfully"), clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 0, true);
                                    return;
                                }
                                else
                                {
                                    clsStandards.WriteLog(this, new Exception("Transaction Failed"), clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 0, true);
                                    return;
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                clsStandards.WriteLog(this, new Exception("No details to print against this document"), clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 0, true);
            }
        }
        catch (Exception ex)
        {
            clsStandards.WriteLog(this, new Exception(ex.ToString()), clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 0, true);
        }
        finally
        {
            objGrn = null;
            objPrint = null;
        }
    }
    protected void GrGRNDetails_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        DataTable dtpo = new DataTable();
        dtpo = (DataTable)ViewState["dtStatus"];
        GrGRNDetails.PageIndex = e.NewPageIndex;
        GrGRNDetails.DataSource = dtpo;
        GrGRNDetails.DataBind();
    }
}