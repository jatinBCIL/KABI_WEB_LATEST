using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.OleDb;
using DataLayer;
using PropertyLayer;
using BusinessLayer;
using System.Configuration;
using System.DirectoryServices;

public partial class FrmStatusLabelPrint : System.Web.UI.Page
{
    public static string strFlg = "";
    DataTable dtImport = new DataTable();
    BL_PrinterMaster objPrint = new BL_PrinterMaster();
    BL_GrnPrinting objBL = new BL_GrnPrinting();

    DataTable dtGrn = new DataTable();
    DataTable dtPRint = new DataTable();

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                objPrint = new BL_PrinterMaster();

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
         objBL = new BL_GrnPrinting();
         dtGrn = new DataTable();
        try
        {
            dtGrn = objBL.BL_GetDocumenDetails(txtDocumentNo.Text.Trim(), "PRINT");
            if (dtGrn.Rows.Count > 0)
            {
                GrGRNDetails.DataSource = dtGrn;
                GrGRNDetails.DataBind();
            }
            else
            {
                GrGRNDetails.DataSource = null;
                GrGRNDetails.DataBind();
                clsStandards.WriteLog(this, new Exception("    No details found for document   "), clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 0, true);
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
        dtGrn = new DataTable();
        objBL = new BL_GrnPrinting();
        
        try
        {
            if (ddlPrinterName.Text.Trim().ToUpper().Contains("SELECT"))
            {
                clsStandards.WriteLog(this, new Exception("Please select printer"), clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 0, true);
                return;
            }
            string strPrinter = objBL.BL_GetPrinterIP(ddlPrinterName.Text.Trim(), clsStandards.current_Plant());
            if (PrintDirect.CheckConnection(strPrinter.Split('=').GetValue(0).ToString(), strPrinter.Split('=').GetValue(1).ToString()) == false)
            {
                clsStandards.WriteLog(this, new Exception("Printer IP not Connected"), clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 0, true);
                return;
            }
            if (GrGRNDetails.Rows.Count > 0)
            {
                for (int i = 0; i < GrGRNDetails.Rows.Count; i++)
                {
                    CheckBox cb = (CheckBox)GrGRNDetails.Rows[i].FindControl("chkSelect");
                    if (cb.Checked == true)
                    {
                                dtPRint = objBL.BL_GetBarcodeNo(clsStandards.filter(GrGRNDetails.Rows[i].Cells[2].Text.Trim()),
                                                                clsStandards.filter(GrGRNDetails.Rows[i].Cells[3].Text.Trim()),
                                                                clsStandards.filter(GrGRNDetails.Rows[i].Cells[4].Text.Trim()),
                                                                clsStandards.filter(GrGRNDetails.Rows[i].Cells[10].Text.Trim()),
                                                                clsStandards.filter(GrGRNDetails.Rows[i].Cells[12].Text.Trim()), 
                                                                clsStandards.current_Plant().ToString(), 
                                                                clsStandards.current_Username().ToString(),
                                                                "PRINT", Convert.ToDecimal(clsStandards.filter(GrGRNDetails.Rows[i].Cells[11].Text.Trim())),
                                                                clsStandards.filter(GrGRNDetails.Rows[i].Cells[14].Text.Trim()), "",
                                                                Convert.ToInt32(clsStandards.filter(GrGRNDetails.Rows[i].Cells[18].Text.Trim())));

                                if (dtPRint.Rows.Count > 0)
                                {
                                    cb.Enabled = false;
                                    cb.Checked = false;
                                    GrGRNDetails.Rows[i].BackColor = System.Drawing.Color.Yellow;

                                }
                                else
                                {
                                    clsStandards.WriteLog(this, new Exception("Barcode already Printed"), clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 0, true);
                                    return;
                                }
                                PFlag = PrintDirect.Print_StatusLabel(ddlPrinterName.SelectedItem.ToString().Trim(), clsStandards.current_User(),
                                                    System.DateTime.Now.ToString("yyyy-MM-dd"), dtPRint, strPrinter.Split('=').GetValue(0).ToString(), strPrinter.Split('=').GetValue(1).ToString());


                               
                                //if (PFlag == true)
                                //{
                                //    cb.Enabled = false;
                                //    cb.Checked = false;
                                //    GrGRNDetails.Rows[i].BackColor = System.Drawing.Color.Yellow;                                   
                                //}
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
            dtGrn = null;
        }
    }

    protected void ChkSelect_CheckedChanged(object sender, EventArgs e)
    {
        string strMaterialType = "";
        try
        {
            CheckBox chk = (CheckBox)sender;
            GridViewRow gv = (GridViewRow)chk.NamingContainer;
            int rownumber = gv.RowIndex;
            if (chk.Checked)
            {
                int j;
                for (j = 0; j <= GrGRNDetails.Rows.Count - 1; j++)
                {
                    if (j != rownumber)
                    {
                        CheckBox chkcheckbox = ((CheckBox)(GrGRNDetails.Rows[j].FindControl("chkSelect")));
                        chkcheckbox.Checked = false;
                    }
                }
            }

            if (GrGRNDetails.Rows.Count > 0)
            {
                for (int i = 0; i < GrGRNDetails.Rows.Count; i++)
                {
                    CheckBox cb = (CheckBox)GrGRNDetails.Rows[i].FindControl("chkSelect");
                    if (cb.Checked == true)
                    {
                        strMaterialType = GrGRNDetails.Rows[i].Cells[15].ToString();
                        if (strMaterialType == "RM")
                        {
                            pnlPackQty.Visible = false;
                        }
                        else
                        {
                            pnlPackQty.Visible = true;
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            clsStandards.WriteLog(this, ex, clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 0, true);
        }
        finally
        {

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