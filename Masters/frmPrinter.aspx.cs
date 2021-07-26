using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using System.IO;
using System.Data.OleDb;
using BusinessLayer;
using PropertyLayer;

public partial class Modules_frmPrinter : System.Web.UI.Page
{
    DataTable dtImport = new DataTable();
    public static string strFlag = "";

    //On Page Load, Fill Plant Code ComboBox, Fill Cubicle Code ComboBox, Fill data of Printer in Grid
    protected void Page_Load(object sender, EventArgs e)
    {
        ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);
        scriptManager.RegisterPostBackControl(this.btnTemplate);
        if (!IsPostBack)
        {
            VisibleRemarkFalse();
            BL_PrinterMaster objPrinter = new BL_PrinterMaster();
            BL_PlantMaster objPlant = new BL_PlantMaster();
            BL_BoothMaster objBooth = new BL_BoothMaster();
            //clsStandards.fillCombobox(ddlDepartmentCode, objDept.BL_GetDepartmentCode(), "DEPARTMENTCODE");
            clsStandards.populateGrid(objPrinter.BL_GetPrinterDtl(clsStandards.WhereStatement_NoST(cboSearch.SelectedValue, cboCriteria.SelectedValue, txtSearch.Text.Trim()), clsStandards.current_Plant()), GrPrinter, "PLANTCODE");
            clsStandards.fillCombobox(ddlPlantCode, objPlant.BL_Get_Plant_with_Desc(), "PLANTCODE");
            clsStandards.fillCombobox(ddlBooth, objBooth.BL_GetBoothCode(clsStandards.current_Plant().Trim().ToString()), "BOOTHCODE");
            GrPrinter_Grid.DataSource = null;
            GrPrinter_Grid.DataBind();
            MultiView1.SetActiveView(View2);
            pnlImport.Visible = false;
            strFlag = "ADD";
            RBactivate.Checked = true;
            RBdeactivate.Checked = false;
        }
    }

    public int status()
    {
        if (RBactivate.Checked)
        {
            return 1;
        }
        else
        {
            return 0;
        }
    }


    //sets the Remark fields Visible
    public void VisibleRemarkTrue()
    {
        lblRemark.Visible = true;
        txtRemark.Visible = true;
    }

    //sets the Remark fields Visible False
    public void VisibleRemarkFalse()
    {
        lblRemark.Visible = false;
        txtRemark.Visible = false;
    }

    //Importing Data From Excel and Populating in Grid
    protected void btnImp_Click(object sender, EventArgs e)
    {

        btnCancel.Visible = true;
        btnSave.Visible = true;
        GrPrinter_Grid.Visible = true;

        try
        {
            string SheetName;
            if (FlUpload.HasFile)
            {
                string excelPath = Server.MapPath(ConfigurationManager.AppSettings["Data"].ToString()) + Path.GetFileName(FlUpload.PostedFile.FileName);
                FlUpload.SaveAs(excelPath);

                string conString = string.Empty;
                string extension = Path.GetExtension(FlUpload.PostedFile.FileName);
                if (extension == ".xls" || extension == ".xlsx")
                {
                    switch (extension)
                    {
                        case "xls": //Excel 97-03
                            conString = ConfigurationManager.ConnectionStrings["Excel03ConString"].ConnectionString;
                            break;
                        case ".xlsx": //Excel 07 or higher
                            conString = ConfigurationManager.ConnectionStrings["Excel07+ConString"].ConnectionString;
                            break;
                    }
                    conString = string.Format(conString, excelPath);
                    using (OleDbConnection excel_con = new OleDbConnection(conString))
                    {

                        //Get the name of the First Sheet.

                        using (OleDbCommand cmd = new OleDbCommand())
                        {
                            cmd.Connection = excel_con;
                            excel_con.Open();
                            DataTable dtExcelSchema = excel_con.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                            SheetName = dtExcelSchema.Rows[0]["TABLE_NAME"].ToString();
                            excel_con.Close();
                        }
                    }


                    //Read Data from the First Sheet.
                    using (OleDbConnection con = new OleDbConnection(conString))
                    {
                        using (OleDbCommand cmd = new OleDbCommand())
                        {
                            using (OleDbDataAdapter oda = new OleDbDataAdapter())
                            {
                                DataTable dt = new DataTable();
                                cmd.CommandText = "SELECT * From [" + SheetName + "]";
                                cmd.Connection = con;
                                con.Open();
                                oda.SelectCommand = cmd;
                                oda.Fill(dtImport);
                                con.Close();

                                //Populate DataGridView.
                                string Header = Path.GetFileName(excelPath);
                                if (dtImport.Rows.Count > 0)
                                {
                                    GrPrinter_Grid.DataSource = dtImport;
                                    GrPrinter_Grid.DataBind();
                                    ViewState["Import"] = dtImport;
                                    pnlImport.Visible = true;
                                }
                                else
                                {
                                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "alert('No records found in excel');", true);
                                    return;
                                }

                            }

                        }

                    }
                }
                else
                {

                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "alert('File Format not supported');", true);
                    return;

                }
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.ToString());
        }
    }

    //Inserting And Updating Printer Data
    protected void imgSave_Click(object sender, ImageClickEventArgs e)
    {
        string[] strPlant = ddlPlantCode.SelectedItem.Text.Split('-');
        //string[] strDept=ddlDepartmentCode.SelectedItem.Text.Split('-');
        //string[] strCubicle = ddlCubicleCode.SelectedItem.Text.Split('-');



        PL_PrinterMaster obj = new PL_PrinterMaster();
        BL_PrinterMaster objCubicle = new BL_PrinterMaster();
        string strResult = "";
        try
        {

            obj.strPlantCode = strPlant[0];
            obj.strPrinterCode = txtPrinterCode.Text.Trim();
            obj.strPrinterIp = txtPrinterIP.Text.Trim();
            obj.strPrinterPort = Convert.ToInt32(txtPrinterPort.Text.Trim());
            obj.strBoothCode = ddlBooth.Text.Trim();
            obj.strDeptCode = "";
            obj.strDeptDesc = "";
            obj.iSt = status();
            obj.strFlag = strFlag;
            obj.strCreatedBy = clsStandards.current_Username();
            obj.strModifyBy = clsStandards.current_Username();
            if (strFlag == "EDIT" && ViewState["REFNO"].ToString() != "")
            {
                if (txtRemark.Text == "" || txtRemark.Text == null)
                {
                    clsStandards.WriteLog(this, new Exception("Remark Field Is Empty "), clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 0, true);
                    return;
                }
                obj.REFNO = Convert.ToInt32(ViewState["REFNO"].ToString());
            }
            else
            {
                obj.REFNO = 0;
            }
            obj.strRemark = txtRemark.Text.Trim();

            strResult = objCubicle.BL_InsertPrinter(obj);


            if (strResult.StartsWith("0"))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "alert('" + strResult.Split('|').GetValue(1).ToString() + "');", true);
            }
            else if (strResult.StartsWith("1"))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "alert('" + strResult.Split('|').GetValue(1).ToString() + "');", true);
                return;

            }
            txtPrinterCode.Enabled = true;
            Clear();
            txtRemark.Text = "";
            VisibleRemarkFalse();
        }
        catch (Exception ex)
        {
            clsStandards.WriteLog(this, ex, clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 0, true);
            return;
        }
    }

    //Populate Printer Master Data in Grid
    protected void imgDetails_Click(object sender, ImageClickEventArgs e)
    {
        txtRemark.Text = "";
        BL_PrinterMaster objPrinter = new BL_PrinterMaster();
        try
        {
            txtRemark.Text = "";
            clsStandards.populateGrid(objPrinter.BL_GetPrinterDtl(clsStandards.WhereStatement_NoST(cboSearch.SelectedValue, cboCriteria.SelectedValue, txtSearch.Text.Trim()), clsStandards.current_Plant()), GrPrinter, "PLANTCODE");
            MultiView1.SetActiveView(View1);
            Clear();
        }
        catch (Exception ex)
        {
            clsStandards.WriteLog(this, ex, clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 0, true);
        }
    }

    //Exports Printer Data into Excel
    protected void imgExport_Click(object sender, ImageClickEventArgs e)
    {
        BL_PrinterMaster objPrinter = new BL_PrinterMaster();
        try
        {
            GrPrinter.AllowPaging = false;
            GrPrinter.AllowSorting = false;

            //clsxlsExport.ExportExcel(GrPlant, "Company.xls");

            clsxlsExport.ConvertToxls(objPrinter.BL_GetPrinterDtl(clsStandards.WhereStatement_NoST(cboSearch.SelectedValue, cboCriteria.SelectedValue, txtSearch.Text.Trim()), clsStandards.current_Plant()), "PrinterMaster");
        }
        catch (Exception ex)
        {
            clsStandards.WriteLog(this, ex, clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 0, true);
        }
        finally
        {
            objPrinter = null;
        }
    }

    //Navigating from Detailed view to Entry View.
    protected void imgAdd1_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            Clear();
            txtRemark.Text = "";
            VisibleRemarkFalse();
            MultiView1.SetActiveView(View2);
            strFlag = "ADD";
        }
        catch (Exception ex)
        {
            clsStandards.WriteLog(this, ex, clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 0, true);
        }
    }

    //Inserting Printer Data which was imported from Excel.
    protected void btnSave_Click(object sender, EventArgs e)
    {
        string strResult = "";
        int pass = 0;
        strFlag = "ADDEXCEL";
        PL_PrinterMaster obj = new PL_PrinterMaster();
        BL_PrinterMaster objCubicle = new BL_PrinterMaster();
        try
        {
            for (int i = 0; i < GrPrinter_Grid.Rows.Count; i++)
            {


                if (GrPrinter_Grid.Rows[i].Cells[0].Text == clsStandards.current_Plant())
                {


                    obj.strPlantCode = GrPrinter_Grid.Rows[i].Cells[0].Text;
                    //obj.strDeptCode = "";
                    //obj.strDeptDesc = GrPrinter_Grid.Rows[i].Cells[1].Text;
                    obj.strPrinterCode = GrPrinter_Grid.Rows[i].Cells[1].Text;
                    obj.strPrinterIp = GrPrinter_Grid.Rows[i].Cells[2].Text;
                    obj.strPrinterPort = Convert.ToInt32(GrPrinter_Grid.Rows[i].Cells[3].Text);
                    obj.strBoothCode = GrPrinter_Grid.Rows[i].Cells[4].Text;
                    obj.strRemark = GrPrinter_Grid.Rows[i].Cells[5].Text; ;

                    obj.strCreatedBy = clsStandards.current_Username();
                    obj.strModifyBy = clsStandards.current_Username();
                    obj.strFlag = "ADDEXCEL";
                    obj.iSt = Convert.ToInt32(GrPrinter_Grid.Rows[i].Cells[6].Text);
                    obj.REFNO = 0;



                    strResult = objCubicle.BL_InsertPrinter(obj);


                    if (strResult.StartsWith("0") == true)
                    {
                        pass = pass + 1;
                        GrPrinter_Grid.Rows[i].BackColor = System.Drawing.Color.Yellow;
                        //ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "alert('" + strResult.Split('|').GetValue(1).ToString() + "');", true);
                        //return;
                    }
                    else
                    {
                        GrPrinter_Grid.Rows[i].BackColor = System.Drawing.Color.Red;
                    }

                }
                else
                {

                    GrPrinter_Grid.Rows[i].BackColor = System.Drawing.Color.Red;
                }

            }
        }
        catch (Exception ex)
        {
            clsStandards.WriteLog(this, ex, clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 0, true);
        }
        clsStandards.WriteLog(this, new Exception(GrPrinter_Grid.Rows.Count + " Out Of " + pass + " Record inserted"), clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 0, true);
        pass = 0;
        return;
    }

    //Bind the data to grid GrPrinter_Grid with Page indexing
    protected void GrPrinter_Grid_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[0].Text = "Page " + (GrPrinter_Grid.PageIndex + 1) + " of " + GrPrinter_Grid.PageCount;
            e.Row.Cells[1].Text = clsStandards.FooterInfo(GrPrinter_Grid);
        }
    }

    //Populate data of seleced page index in grid GrPrinter_Grid.
    protected void GrPrinter_Grid_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            dtImport = ViewState["Import"] == null ? new DataTable() : (DataTable)ViewState["Import"];
            GrPrinter_Grid.PageIndex = e.NewPageIndex;
            GrPrinter_Grid.DataSource = dtImport;
            GrPrinter_Grid.DataBind();
        }
        catch (Exception ex)
        {
            clsStandards.WriteLog(this, ex, clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 0, true);
        }
        finally
        {
        }

    }

    //Bind the data to grid GrPrinter with Page indexing
    protected void GrPrinter_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[0].Text = "Page " + (GrPrinter.PageIndex + 1) + " of " + GrPrinter.PageCount;
            e.Row.Cells[1].Text = clsStandards.FooterInfo(GrPrinter);
        }
    }

    //Populate data of seleced page index in grid GrPrinter.
    protected void GrPrinter_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        BL_PrinterMaster objPrinter = new BL_PrinterMaster();
        try
        {

            GrPrinter.PageIndex = e.NewPageIndex;
            clsStandards.populateGrid(objPrinter.BL_GetPrinterDtl(clsStandards.WhereStatement_NoST(cboSearch.SelectedValue, cboCriteria.SelectedValue, txtSearch.Text.Trim()), clsStandards.current_Plant()), GrPrinter, "PLANTCODE");
        }
        catch (Exception ex)
        {
            clsStandards.WriteLog(this, ex, clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 0, true);
        }
        finally
        {
            objPrinter = null;
        }
    }

    //Fill the selected data into controls which is selected from Grid.
    protected void GrPrinter_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        DataTable dt = new DataTable();
        BL_PrinterMaster objPrinter = new BL_PrinterMaster();
        BL_PlantMaster objPlant = new BL_PlantMaster();
        BL_Chnage objChange = new BL_Chnage();
        BL_BoothMaster objBooth = new BL_BoothMaster();
        if (e.CommandName == "Select")
        {

            int RowInd = ((GridViewRow)(((LinkButton)e.CommandSource).NamingContainer)).RowIndex;

            try
            {
                dt = objPrinter.BL_GetPrinterDtl(clsStandards.WhereStatement_NoST("[REFNO]", "Equal To", GrPrinter.DataKeys[RowInd].Value.ToString()), clsStandards.current_Plant());
                if (dt.Rows.Count != 0)
                {
                    clsStandards.fillCombobox(ddlPlantCode, objPlant.BL_Get_Plant_with_Desc(), "PLANTCODE");
                    clsStandards.fillCombobox(ddlBooth, objBooth.BL_GetBoothCode(clsStandards.current_Plant().Trim().ToString()), "BOOTHCODE");
                    txtPrinterCode.Text = dt.Rows[0]["PRINTERCODE"].ToString();
                    txtPrinterIP.Text = dt.Rows[0]["PRINTERIP"].ToString();
                    txtPrinterPort.Text = dt.Rows[0]["PRINTERPORT"].ToString();

                    //try { ddlDepartmentCode.SelectedValue = objChange.Bl_get_strDept_desc( dt.Rows[0]["DEPARTMENTCODE"].ToString()); }
                    //catch { }
                    //ddlDepartmentCode_SelectedIndexChanged(sender, e);
                    try
                    {
                        ddlPlantCode.SelectedValue = objChange.Bl_get_strPlant_desc(dt.Rows[0]["PLANTCODE"].ToString());
                        // ddlPlantCode.SelectedValue = objChange.Bl_get_strPlant_desc(dt.Rows[0]["PLANTCODE"].ToString());
                    }
                    catch { }
                    //ddlPlantCode_SelectedIndexChanged(sender, e);
                    try
                    {

                        //ddlBooth.SelectedValue = dt.Rows[0]["BOOTHCODE"].ToString().Trim(); //objBooth.BL_GetBoothCode
                        ddlBooth.SelectedValue = dt.Rows[0]["BOOTHCODE"].ToString();

                    }
                    catch { }
                    //lblDesc.Text = dt.Rows[0]["DEPARTMENTDESC"].ToString();
                    strFlag = "EDIT";
                    ViewState["REFNO"] = dt.Rows[0]["REFNO"].ToString();
                    txtRemark.Text = dt.Rows[0]["REMARK"].ToString();


                    RBactivate.Checked = ((dt.Rows[0]["Status"].ToString() == "ACTIVATE") ? true : false);
                    RBdeactivate.Checked = ((dt.Rows[0]["Status"].ToString() == "DEACTIVATE") ? true : false);

                    //if (dt.Rows[0]["STATUS"].ToString() == "True")
                    //{
                    //    ChkActivate.Checked = true;
                    //}
                    //else
                    //{
                    //    ChkActivate.Checked = false;
                    //}
                    txtPrinterCode.Enabled = false;
                    //ddlDepartmentCode.Enabled = false;
                    ddlPlantCode.Enabled = false;
                    //ddlCubicleCode.Enabled = false;

                    VisibleRemarkTrue();
                    MultiView1.SetActiveView(View2);

                }
                else
                {
                    clsStandards.WriteLog(this, new Exception("Problem fetching details for selected Plant."), clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 0, true);
                    return;
                }
            }
            catch (Exception ex)
            {
                clsStandards.WriteLog(this, ex, clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 0, true);
            }
            finally
            {
                dt = null;
                objPrinter = null;
            }


        }

    }

    //Clear the Grid GrPrinter_Grid
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        try
        {
            GrPrinter_Grid.DataSource = null;
            GrPrinter_Grid.DataBind();
            dtImport = null;
            ViewState["Import"] = null;
            pnlImport.Visible = false;
        }
        catch (Exception ex)
        {
            clsStandards.WriteLog(this, ex, clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 0, true);
            return;

        }
    }

    //Bind the data to grid GrPrinter with Page indexing
    protected void GrPrinter_RowDataBound1(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[0].Text = "Page " + (GrPrinter.PageIndex + 1) + " of " + GrPrinter.PageCount;
            e.Row.Cells[1].Text = clsStandards.FooterInfo(GrPrinter);
        }

    }

    //Populate data of seleced page index in grid GrPrinter_Grid.
    protected void GrPrinter_Grid_PageIndexChanging1(object sender, GridViewPageEventArgs e)
    {
        try
        {
            dtImport = ViewState["Import"] == null ? new DataTable() : (DataTable)ViewState["Import"];
            GrPrinter_Grid.PageIndex = e.NewPageIndex;
            GrPrinter_Grid.DataSource = dtImport;
            GrPrinter_Grid.DataBind();
        }
        catch (Exception ex)
        {
            clsStandards.WriteLog(this, ex, clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 0, true);
        }
        finally
        {
        }
    }

    //Bind the data to grid GrPrinter_Grid with Page indexing
    protected void GrPrinter_Grid_RowDataBound1(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[0].Text = "Page " + (GrPrinter_Grid.PageIndex + 1) + " of " + GrPrinter_Grid.PageCount;
            e.Row.Cells[1].Text = clsStandards.FooterInfo(GrPrinter_Grid);
        }

    }

    //Populates Data in Grid Against Search Condition.
    protected void imgSearch_Click(object sender, ImageClickEventArgs e)
    {
        BL_PrinterMaster objPrinter = new BL_PrinterMaster();
        try
        {
            clsStandards.populateGrid(objPrinter.BL_GetPrinterDtl(clsStandards.WhereStatement_NoST(cboSearch.SelectedValue, cboCriteria.SelectedValue, txtSearch.Text.Trim()), clsStandards.current_Plant()), GrPrinter, "PLANTCODE");
            MultiView1.SetActiveView(View1);
        }
        catch (Exception ex)
        {
            clsStandards.WriteLog(this, ex, clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 0, true);
        }
        finally
        {
            objPrinter = null;
        }


    }

    //Navigating from Detailed view to Entry View.
    protected void imgCLose_Click(object sender, ImageClickEventArgs e)
    {
        MultiView1.SetActiveView(View2);
    }
    //Fetching Department Desc
    //protected void ddlDepartmentCode_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    BL_PrinterMaster objPrinter = new BL_PrinterMaster();
    //    BL_DepartmentMaster objDept = new BL_DepartmentMaster();
    //    PL_DepartmentMaster objPl = new PL_DepartmentMaster();
    //    if(ddlDepartmentCode.SelectedValue=="Select")
    //    {
    //        ddlPlantCode.SelectedValue = "Select";
    //        lblDesc.Text = "";
    //        ddlPlantCode.DataSource = null;
    //        ddlPlantCode.DataBind();
    //        ddlDepartmentCode.SelectedValue = "Select";
    //        return;
    //    }

    //    try
    //    {
    //        string[] strDept = ddlDepartmentCode.SelectedItem.Text.Split('-');
    //        lblDesc.Text = string.Empty;
    //        if (strDept.Length > 2)
    //        {

    //            lblDesc.Text = strDept[1] + "-"+strDept[2];

    //        }
    //        else
    //        {
    //            lblDesc.Text = strDept[1];
    //        }
    //        clsStandards.fillCombobox(ddlPlantCode, objDept.BL_GetDepartmentPlantCode(strDept[0].Replace("-","")), "PLANTCODE");

    //        //lblDesc.Text = objPrinter.BL_GetDeptDesc(ddlDepartmentCode.Text).Rows[0][0].ToString();


    //    }
    //    catch (Exception ex)
    //    {
    //        clsStandards.WriteLog(this, ex, clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 0, true);
    //    }
    //    finally
    //    {
    //        objPrinter = null;
    //        objDept = null;
    //        objPl = null;
    //    }
    //}

    //Fetching Cubicle Code
    //protected void ddlPlantCode_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    BL_PrinterMaster objPrinter = new BL_PrinterMaster();

    //    string[] strplant = ddlPlantCode.Text.Split('-');
    //    string[] strDept = ddlDepartmentCode.Text.Split('-');

    //    if (ddlDepartmentCode.SelectedValue == "Select")
    //    {
    //        ddlDepartmentCode.SelectedValue = "Select";
    //        lblDesc.Text = "";
    //        return;
    //    }


    //    try
    //    {
    //        clsStandards.fillCombobox(ddlCubicleCode, objPrinter.BL_GetCubicleCode(strDept[0],strplant[0]), "CUBICLECODE");
    //    }
    //    catch (Exception ex)
    //    {
    //        clsStandards.WriteLog(this, ex, clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 0, true);
    //    }
    //    finally
    //    {
    //        objPrinter = null;
    //    }
    //}

    //Clear all control
    public void Clear()
    {

        //try { ddlDepartmentCode.SelectedValue = "Select"; }
        //catch { }
        ddlBooth.Items.Clear();
        ddlBooth.Items.Add("Select");
        try { ddlBooth.SelectedValue = "Select"; }
        catch { }
        ddlPlantCode.Items.Clear();
        ddlPlantCode.Items.Add("Select");
        try { ddlPlantCode.SelectedValue = "Select"; }
        catch { }
        //lblDesc.Text = "";
        txtPrinterCode.Text = "";
        txtPrinterIP.Text = "";
        txtPrinterPort.Text = "";
        txtSearch.Text = "";

        RBactivate.Checked = true;
        RBdeactivate.Checked = false;

        // ChkActivate.Checked = true;
        txtPrinterCode.Enabled = true;
        //ddlDepartmentCode.Enabled = true;
        ddlPlantCode.Enabled = true;
        ddlBooth.Enabled = true;
    }
    protected void btnTemplate_Click(object sender, EventArgs e)
    {
        try
        {
            Response.ContentType = "application/ms-excel";
            Response.AddHeader("content-disposition", "inline; filename=PrinterMaster.xlsx");
            Response.TransmitFile(Server.MapPath("~/PrnFile/PrinterMaster.xlsx"));
            Response.Flush();

        }
        catch (Exception ex)
        {
            throw ex;
        }

    }
}