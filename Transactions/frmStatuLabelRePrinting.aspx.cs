using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data;
using System.Data.OleDb;
using DataLayer;
using PropertyLayer;
using BusinessLayer;
using System.Configuration;
using System.DirectoryServices;

public partial class frmStatuLabelRePrinting : System.Web.UI.Page
{

    public static string strFlg = "";
    DataTable dtImport = new DataTable();
    BL_GrnPrinting objBL = new BL_GrnPrinting();
    BL_PrinterMaster objPrint = new BL_PrinterMaster();
    DataTable dtPRint = new DataTable();
    DataTable dtGrn = new DataTable();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            objPrint = new BL_PrinterMaster();
            try
            {
                ViewState["dtStatus"] = null;
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

    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Verifies that the control is rendered */
    }

    protected void btnGet_Click(object sender, EventArgs e)
    {
        objBL = new BL_GrnPrinting();
        dtGrn = new DataTable();
        try
        {
            dtGrn = objBL.BL_GetDocumenDetails(txtManNo.Text.Trim(), "REPRINT");
            if (dtGrn.Rows.Count > 0)
            {
                ViewState["dtStatus"] = dtGrn;
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
            objBL = null;
            dtGrn = null;
        }
    }

    protected void btnPrint_Click(object sender, EventArgs e)
    {
        bool PFlag = false;
        dtPRint = new DataTable();
        objBL = new BL_GrnPrinting();
        try
        {
            if (ddlPrinterName.Text.Trim().ToUpper().Contains("SELECT"))
            {
                clsStandards.WriteLog(this, new Exception("Please select Printer"), clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 0, true);
                return;
            }
            string strPrinter = objBL.BL_GetPrinterIP(ddlPrinterName.Text.Trim(), clsStandards.current_Plant());
            if (PrintDirect.CheckConnection(strPrinter.Split('=').GetValue(0).ToString(), strPrinter.Split('=').GetValue(1).ToString()) == false)
            {
                clsStandards.WriteLog(this, new Exception("Printed IP not Connected"), clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 0, true);
                return;
            }

            if (GrGRNDetails.Rows.Count > 0)
            {
                for (int i = 0; i < GrGRNDetails.Rows.Count; i++)
                {
                    CheckBox cb = (CheckBox)GrGRNDetails.Rows[i].FindControl("chkSelect");
                    if (cb.Checked == true)
                    {
                        
                        dtPRint = objBL.BL_GetBarcodeNo(GrGRNDetails.Rows[i].Cells[3].Text.Trim(), GrGRNDetails.Rows[i].Cells[4].Text.Trim(),
                            GrGRNDetails.Rows[i].Cells[5].Text.Trim(), GrGRNDetails.Rows[i].Cells[9].Text.Trim(), 
                            GrGRNDetails.Rows[i].Cells[13].Text.Trim().ToString(),
                            clsStandards.current_Plant().ToString(), clsStandards.current_Username().ToString(), 
                            (GrGRNDetails.Rows[i].Cells[1].Text.Trim()), Convert.ToDecimal(GrGRNDetails.Rows[i].Cells[12].Text.Trim()),
                            GrGRNDetails.Rows[i].Cells[15].Text.Trim(), ddlReason.SelectedItem.ToString().Trim(),0);

                        PFlag = PrintDirect.Print_StatusLabel(ddlPrinterName.SelectedItem.ToString().Trim(), clsStandards.current_User().ToString(), System.DateTime.Now.ToString("dd/MM/yyyy"), dtPRint, strPrinter.Split('=').GetValue(0).ToString(), strPrinter.Split('=').GetValue(1).ToString());
                        
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
            dtPRint = null;
            objBL = null;
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


    protected void GrGRNDetails_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        DataTable dtpo = new DataTable();
        dtpo = (DataTable)ViewState["dtStatus"];
        GrGRNDetails.PageIndex = e.NewPageIndex;
        GrGRNDetails.DataSource = dtpo;
        GrGRNDetails.DataBind();
    }

    protected void btnSelectAll_CheckedChanged(object sender, EventArgs e)
    {
       // int i = 0;
        if (GrGRNDetails.Rows.Count > 0)
        {
            if (btnSelectAll.Checked)
            {
                foreach (GridViewRow gvr in GrGRNDetails.Rows)
                {
                    CheckBox chkSelect = gvr.FindControl("chkSelect") as CheckBox;
                    if (chkSelect != null)
                    {
                        chkSelect.Checked = true;
                        //i++;
                    }
                }
                //i++;
            }
        }
        
    }
}