using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLayer;
using PropertyLayer;
using System.Data;

public partial class frmMasterBarcodePrinting : System.Web.UI.Page
{
    public static string strFlg = "";
    static int printype = 0;
    BL_MasterBarcodePrinting objBs = new BL_MasterBarcodePrinting();
    BL_PlantMaster objPlant = new BL_PlantMaster();

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            objBs = new BL_MasterBarcodePrinting();
            objPlant = new BL_PlantMaster();

            if (!Page.IsPostBack)
            {
                //clsStandards.fillCombobox(DDReson, objLbPrint.BLAPI_GetReason(clsStandards.current_Plant(), "MASTER REPRINT"), "REASON_DESC");
                clsStandards.fillCombobox(DD_Plantcode, objPlant.BL_Get_Plant(clsStandards.current_Plant().ToString().Trim()), "PLANTCODE");
                RBPrint.Checked = true;
                RBReprint.Checked = false;
                EnablePrint();
                clsStandards.fillCombobox(DDPrinter, objBs.Bl_GetPrinterCode(clsStandards.current_Plant()), "PRINTER_CODE");
            }
        }
        catch(Exception ex)
            {}

    }



    public void EnablePrint()
    {
        lblHeader.Text = " Master Barcode Printing";
        lblReson.Visible = false;
        DDReson.Visible = false;
        btnPrint.Text = "Print ";
    }

    public void EnableRePrint()
    {
        lblHeader.Text = " Master Barcode Re-Printing";
        lblReson.Visible = true;
        DDReson.Visible = true;
        btnPrint.Text = "Re-Print ";
    }

    public void clear()
    {
        DDReson.SelectedValue = "Select";
        DDPrinter.SelectedValue = "Select";
        chkAll.Checked = false;
    }

    protected void RBPrint_CheckedChanged(object sender, EventArgs e)
    {
        if (RBPrint.Checked == true)
        {
            DDModule.SelectedValue = "Select";
            DDPrinter.SelectedValue = "Select";
            DDReson.SelectedValue = "Select";
            GrMasterBarcode.DataSource = null;
            GrMasterBarcode.DataBind();
            printype = 0;
            EnablePrint();
        }
        if (RBReprint.Checked == true)
        {
            DDModule.SelectedValue = "Select";
            DDPrinter.SelectedValue = "Select";
            GrMasterBarcode.DataSource = null;
            GrMasterBarcode.DataBind();
            printype = 1;
            EnableRePrint();
        }
    }
    protected void RBReprint_CheckedChanged(object sender, EventArgs e)
    {
        if (RBPrint.Checked == true)
        {
            DDModule.SelectedValue = "Select";
            DDPrinter.SelectedValue = "Select";
            GrMasterBarcode.DataSource = null;
            GrMasterBarcode.DataBind();
            lblCubicleCode.Visible = false;
            ddCubicleCode.Visible = false;
            DD_Plantcode.SelectedIndex = 0;

            printype = 0;
            EnablePrint();
        }
        if (RBReprint.Checked == true)
        {
            DDModule.SelectedValue = "Select";
            DDPrinter.SelectedValue = "Select";
            GrMasterBarcode.DataSource = null;
            GrMasterBarcode.DataBind();
            printype = 1;
            lblCubicleCode.Visible = false;
            ddCubicleCode.Visible = false;
            DD_Plantcode.SelectedIndex = 0;
            EnableRePrint();
        }
    }
    protected void DDModule_SelectedIndexChanged(object sender, EventArgs e)
    {
        DDReson.Enabled = true;
        DDPrinter.Enabled = true;
        btnPrint.Enabled = true;
        chkAll.Enabled = true;
        GrMasterBarcode.DataSource = null;
        GrMasterBarcode.DataBind();

        

        if (DDModule.SelectedIndex == 0)
        {
            ddCubicleCode.SelectedValue = "Select";
            GrMasterBarcode.DataSource = null;
            GrMasterBarcode.DataBind();
        }

   
        chkAll.Checked = false;
        GrMasterBarcode.DataSource = null;
        GrMasterBarcode.DataBind();
        clsStandards.populateGrid(objBs.Bl_get_GridData(DDModule.SelectedValue, printype, DD_Plantcode.Text.Trim()), GrMasterBarcode, "CODE");

        if (GrMasterBarcode.Rows.Count == 0)
        {
            DDReson.Enabled = false;
            DDPrinter.Enabled = false;
            btnPrint.Enabled = false;
            chkAll.Enabled = false;
        }


    }
    protected void CheckBox1_CheckedChanged(object sender, EventArgs e)
    {
        foreach (GridViewRow gr in GrMasterBarcode.Rows)
        {
            ((CheckBox)gr.FindControl("chkSelect")).Checked = chkAll.Checked;
        }
    }
    protected void btnPrint_Click(object sender, EventArgs e)
    {
        BL_MasterBarcodePrinting objBl = new BL_MasterBarcodePrinting();
        PL_MasterBarcodePrinting objPl = new PL_MasterBarcodePrinting();
        Boolean print_flag = true;
        int dataCount = 0;
        int printBarcode = 0;
        DataTable dtPrinter = new DataTable();
        DataTable dt = new DataTable();
        dt.Columns.Add("Barcode", typeof(string));
        if (RBReprint.Checked)
        {
            if (DDReson.SelectedIndex == 0)
            {
                clsStandards.WriteLog(this, new Exception("Select Reson For Re-Printing !"), clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 0, true);
                return;
            }

        }
        objPl.strPlant = clsStandards.current_Plant().ToString();
        objPl.strPrinter = DDPrinter.Text.Trim();
        dtPrinter = objBl.BL_GetPrinterIPPost(objPl);

        if (chkAll.Checked)
        {
            for (int i = 0; i < GrMasterBarcode.Rows.Count; i++)
            {
                dt.Rows.Add(GrMasterBarcode.Rows[i].Cells[1].Text);

                objPl.strCode = GrMasterBarcode.Rows[i].Cells[1].Text;
                objPl.strModule = DDModule.SelectedValue;
                if (RBPrint.Checked)
                {
                    objPl.strReson = "";
                }
                else
                {

                    objPl.strReson = DDReson.SelectedValue;
                }
                objPl.strUser = clsStandards.current_Username();
                //string strResult = objBl.BL_IN_UP_MasterBarcode(objPl);

                string strResult = "0";


                if (strResult.StartsWith("0"))
                {

                    dataCount = dataCount + 1;
                    print_flag = PrintDirect.Print_Master_Barcode(DDPrinter.SelectedValue.ToString(), objPl.strCode, dtPrinter.Rows[0][0].ToString(), dtPrinter.Rows[0][1].ToString());
                    if (print_flag == true)
                    {
                        printBarcode = printBarcode + 1;
                    }

                }

            }

            clsStandards.WriteLog(this, new Exception(printBarcode + " Label(s) Printed "), clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 0, true);
            printBarcode = 0;
            BL_MasterBarcodePrinting objBs = new BL_MasterBarcodePrinting();
            clsStandards.populateGrid(objBs.Bl_get_GridData(DDModule.SelectedValue, printype, DD_Plantcode.Text.Trim()), GrMasterBarcode, "CODE");
            objBs = null;
            clear();
            if (chkAll.Checked == false)
            {
                return;
            }

        }
        //if check all btn not selected and we want get selectedd recored from grid

        if (chkAll.Checked == false)
        {
            for (int i = 0; i < GrMasterBarcode.Rows.Count; i++)
            {
                CheckBox chkRow = (GrMasterBarcode.Rows[i].Cells[0].FindControl("chkSelect") as CheckBox);
                if (chkRow.Checked)
                {
                    dt.Rows.Add(GrMasterBarcode.Rows[i].Cells[1].Text);

                    objPl.strCode = GrMasterBarcode.Rows[i].Cells[1].Text;
                    objPl.strModule = DDModule.SelectedValue;
                    if (RBPrint.Checked)
                    {
                        objPl.strReson = "";
                    }
                    else
                    {

                        objPl.strReson = DDReson.SelectedValue;
                    }
                    objPl.strUser = clsStandards.current_Username();
                    // string strResult = objBl.BL_IN_UP_MasterBarcode(objPl);

                    string strResult = "0";
                    if (strResult.StartsWith("0"))
                    {
                        dataCount = dataCount + 1;
                        print_flag = PrintDirect.Print_Master_Barcode(DDPrinter.SelectedValue.ToString(), objPl.strCode, dtPrinter.Rows[0][0].ToString(), dtPrinter.Rows[0][1].ToString());
                        if (print_flag == true)
                        {
                            printBarcode = printBarcode + 1;
                        }
                    }
                }
            }

            clear();

            clsStandards.WriteLog(this, new Exception(printBarcode + " Label(s) Printed "), clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 0, true);
            printBarcode = 0;
            BL_MasterBarcodePrinting objBs = new BL_MasterBarcodePrinting();
            clsStandards.populateGrid(objBs.Bl_get_GridData(DDModule.SelectedValue, printype, DD_Plantcode.Text.Trim()), GrMasterBarcode, "CODE");
            objBs = null;
        }





    }
    protected void DD_Plantcode_SelectedIndexChanged(object sender, EventArgs e)
    {
        DDModule.SelectedIndex = 0;

        if (DD_Plantcode.SelectedValue == "Select")
        {
            DDModule.SelectedValue = "Select";
            GrMasterBarcode.DataSource = null;
            GrMasterBarcode.DataBind();
            DDModule.Enabled = false;
            lblCubicleCode.Visible = false;
            ddCubicleCode.Visible = false;

        }
        else
        {

            DDModule.Enabled = true;
        }

    }
    protected void ddCubicleCode_SelectedIndexChanged(object sender, EventArgs e)
    {

        if (ddCubicleCode.SelectedValue == "Select")
        {
            GrMasterBarcode.DataSource = null;
            GrMasterBarcode.DataBind();
            return;
        }

        BL_MasterBarcodePrinting objBs = new BL_MasterBarcodePrinting();
        try
        {

            string strCubicle;
            string printflag;
            string[] strPlant = DD_Plantcode.SelectedValue.Split('-');

            strCubicle = ddCubicleCode.SelectedValue;

            if (RBPrint.Checked)
            {
                printflag = "0";
            }
            else
            {
                printflag = "1";
            }

            clsStandards.populateGrid(objBs.Bl_getDataFromCubicle(strCubicle, strPlant[0], printflag), GrMasterBarcode, "CODE");


        }
        catch (Exception ex)
        {
            clsStandards.WriteLog(this, ex, clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 0, true);


        }
        finally
        {
            objBs = null;
        }
    }
}