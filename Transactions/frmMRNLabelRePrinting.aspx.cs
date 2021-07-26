using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using BusinessLayer;

public partial class frmMRNLabelRePrinting : System.Web.UI.Page
{
    string strFlg = "";
    BL_SamplePrinting objBL = new BL_SamplePrinting();
    BL_GrnPrinting objGrn = new BL_GrnPrinting();
    DataTable dtGrn = new DataTable();
    BL_PrinterMaster objPrint = new BL_PrinterMaster();
    DataTable dt = new DataTable();
    DataTable dt_Plant = new DataTable();
    DataTable dt_AscPlant = new DataTable();
    DataSet ds = new DataSet();

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                objPrint = new BL_PrinterMaster();
                dt = new DataTable();
                dt_Plant = new DataTable();
                dt_AscPlant = new DataTable();
                ds = new DataSet();
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
        catch (Exception ex)
        {

        }
    }
    protected void btnGet_Click(object sender, EventArgs e)
    {
        dtGrn = new DataTable();
        objBL = new BL_SamplePrinting();
        try
        {
            dtGrn = objBL.BL_GETMRN_RePrintData(txtMatDocNo.Text, txtProcessOrder.Text, clsStandards.current_Plant());
            if (dtGrn.Rows.Count > 0)
            {
                GrGRNDetails.DataSource = dtGrn;
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
            clsStandards.WriteLog(this, ex, clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 0, true);
        }
        finally
        {
            dtGrn = null;
            objBL = null;
        }
    }

    protected void btnPrint_Click(object sender, EventArgs e)
    {
        bool PFlag = false;
        dt = new DataTable();
        objBL = new BL_SamplePrinting();
        objGrn = new BL_GrnPrinting();
        decimal iPackSize = 0;
        try
        {
            if (ddlPrinterName.Text.Trim().ToUpper().Contains("SELECT"))
            {
                clsStandards.WriteLog(this, new Exception("Printer IP not Connected"), clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 0, true);
                return;
            }
            string strPrinter = objGrn.BL_GetPrinterIP(ddlPrinterName.Text.Trim(), clsStandards.current_Plant());
            if (PrintDirect.CheckConnection(strPrinter.Split('=').GetValue(0).ToString(), strPrinter.Split('=').GetValue(1).ToString()) == false)
            {
                clsStandards.WriteLog(this, new Exception("Printed IP not Connected"), clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 0, true);
                return;
            }
            if (GrGRNDetails.Rows.Count > 0)
            {
                for (int i = 0; i < GrGRNDetails.Rows.Count; i++)
                {
                    string strType = "";
                    //iPackSize = Convert.ToDecimal(GrGRNDetails.Rows[i].Cells[11].Text.Trim());
                    CheckBox cb = (CheckBox)GrGRNDetails.Rows[i].FindControl("chkSelect");
                    if (cb.Checked == true)
                    {
                        dt = objBL.BL_GetMRN_ReprintData_Barcode(GrGRNDetails.Rows[i].Cells[3].Text.Trim(), GrGRNDetails.Rows[i].Cells[1].Text.Trim(),
                                    clsStandards.current_Plant(), GrGRNDetails.Rows[i].Cells[5].Text.Trim(),ddlReason.Text.Trim());

                        PFlag = PrintDirect.Print_MRNLabel(ddlPrinterName.Text.Trim(), clsStandards.current_User().ToString(), System.DateTime.Now.ToString("dd/MM/yyyy"), dt, strPrinter.Split('=').GetValue(0).ToString(),
                            strPrinter.Split('=').GetValue(1).ToString(), strType);
                        if (PFlag == true)
                        {
                            cb.Enabled = false;
                            cb.Checked = false;
                            GrGRNDetails.Rows[i].BackColor = System.Drawing.Color.Yellow;
                        }
                    }
                }
                if (PFlag == true)
                {
                    clsStandards.WriteLog(this, new Exception("Barcode Printed successfully"), clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 0, true);
                }
                else
                {
                    clsStandards.WriteLog(this, new Exception("Problem in Printing"), clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 0, true);
                }
            }
            else
            {
                clsStandards.WriteLog(this, new Exception("No details to print against this document"), clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 0, true);
            }
        }
        catch (Exception ex)
        {
            clsStandards.WriteLog(this, ex, clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 0, true);
        }
        finally
        {
            dt = null;
            objBL = null;
            objGrn = null;
        }
    }


    protected void btnClose_Click(object sender, EventArgs e)
    {
        try
        {
            HttpContext.Current.Response.Redirect(ResolveUrl("~/Modules/frmMain.aspx"), false);
        }
        catch (Exception ex)
        {
            clsStandards.WriteLog(this, ex, clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 0, true);
        }
        finally
        {

        }
    }
}