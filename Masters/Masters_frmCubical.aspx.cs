using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using PropertyLayer;
using BusinessLayer;
using System.IO;
using System.Data.OleDb;
using System.Configuration;
public partial class Masters_frmCubical : System.Web.UI.Page
{
    public static string strFlag = "";
    DataTable dtImport = new DataTable();

    public static string strGPLANT;
    public static string strGPLANT_DESC;
    public static string strGDEPARTMENT;
    public static string strGDEPARTMENT_DESC;

    //On Page Load, Fill Plant Code ComboBox, Fill Department Code ComboBox, Fill Data in Grid
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);
            scriptManager.RegisterPostBackControl(this.btnTemplate);
            VisibleRemarkFalse();
            BL_PlantMaster objPlant = new BL_PlantMaster();
            BL_CubicleMaster objCubicle = new BL_CubicleMaster();
            BL_DepartmentMaster objDepartment = new BL_DepartmentMaster();

            BL_DepartmentMaster objDept = new BL_DepartmentMaster();
            try
            {
                clsStandards.fillCombobox(ddlPlantCode, objPlant.BL_Get_Plant(clsStandards.current_Plant().ToString().Trim()), "PLANTCODE");
                clsStandards.fillCombobox(ddlDepartmentCode, objDepartment.BL_GetDepartmentCodeforPlant(clsStandards.current_Plant().ToString().Trim()), "DEPARTMENTCODE");
                //clsStandards.populateGrid(objCubicle.BL_GetcubicleDtl(clsStandards.WhereStatement_NoST(cboSearch.SelectedValue, cboCriteria.SelectedValue, txtSearch.Text.Trim())), GRCUBICLE, "PLANTCODE");

                ddlDepartmentCode.Items.Clear();
                MultiView1.SetActiveView(View2);
                GRCUBICLE_GRID.DataSource = null;
                GRCUBICLE_GRID.DataBind();
                Panel1.Visible = false;
                pnlImport.Visible = false;
                strFlag = "ADD";
                RBactivate.Checked = true;
                RBdeactivate.Checked = false;

                if (Plant_type().Trim() == "API")
                {
                    lblHoldPeriod.Visible = false;
                    lblHoldPeriodCleanUnit.Visible = false;
                    Label3.Visible = false;
                    Label6.Visible = false;


                    rvHoldePeriod.Visible = false;
                    rvHoldPeriodClean.Visible = false;
                    rvTxtHoldPeriod.Visible = false;
                    rvHoldUnit.Visible = false;

                    txtHoldPeriod.Visible = false;
                    txtHoldPeriod.Text = "0";
                    txtHoldPeriodToBeClean.Visible = false;
                    ddlHoldClean.Visible = false;
                    ddlHoldClean.Items.Clear();
                    ddlHoldPeriodUnit.Visible = false;
                    ddlHoldClean.SelectedValue = "";

                    Image6.Visible = false;
                    Image2.Visible = false;
                    img_Str1.Visible = false;
                    Image3.Visible = false;

                    txtHoldPeriodToBeClean.Text = "0";


                    ddlHoldClean.Items.Clear();
                    ddlHoldClean.SelectedValue = "";


                }
                ddlDepartmentCode.Items.Clear();



            }
            catch (Exception ex)
            {
                clsStandards.WriteLog(this, ex, clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 0, true);
            }
        }

    }

    //sets the Remark fields Visible
    public void VisibleRemarkTrue()
    {
        lblRemark.Visible = true;
        txtRemark.Visible = true;
    }



    //get plant type from session
    public static string Plant_type()
    {
        try
        {
            if (HttpContext.Current.Session["PlantType"] != null)
            {

                return (Convert.ToString(HttpContext.Current.Session["PlantType"]));
            }
            else
            {
                HttpContext.Current.Session.Abandon();
                HttpContext.Current.Response.Redirect(VirtualPathUtility.ToAbsolute(System.Web.Configuration.WebConfigurationManager.AppSettings["Login"].ToString()));
                throw new Exception("Login session expired.");
            }
        }
        catch
        {
            HttpContext.Current.Session.Abandon();
            HttpContext.Current.Response.Redirect(VirtualPathUtility.ToAbsolute(System.Web.Configuration.WebConfigurationManager.AppSettings["Login"].ToString()));
            throw new Exception("Login session expired.");
        }
    }


    //sets the Remark fields Visible False
    public void VisibleRemarkFalse()
    {
        lblRemark.Visible = false;
        txtRemark.Visible = false;
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

    //Inserting And Updating Cubicle Data
    protected void imgSave_Click(object sender, ImageClickEventArgs e)
    {
        string[] strplantcode = ddlPlantCode.SelectedItem.Text.Split('-');
        string[] strDepartment = ddlDepartmentCode.SelectedItem.Text.Split('-');

        string strResult = "";
        try
        {
            PL_CubicleMaster obj = new PL_CubicleMaster();
            BL_CubicleMaster objCubicle = new BL_CubicleMaster();
            obj.strPlantCode = strplantcode[0];
            obj.strDepartDesc = strDepartment[1];
            obj.strCubicleCode = txtCubicleCode.Text.Trim();
            obj.strCubicleDesc = txtCubicleDesc.Text.Trim();
            obj.IHoldPeriodClean = Convert.ToInt32(txtHoldPeriod.Text.Trim() == "" ? "0" : txtHoldPeriod.Text.Trim());




            if (ddlHoldClean.SelectedValue.ToString().Trim() == "Select")
            {
                obj.strHoldPeriodClean = "";
            }
            else
            {
                obj.strHoldPeriodClean = ddlHoldClean.SelectedValue.ToString().Trim();
            }

            obj.IHoldPeriondToBeClean = Convert.ToInt32(txtHoldPeriodToBeClean.Text.Trim() == "" ? "0" : txtHoldPeriodToBeClean.Text.Trim());

            if (ddlHoldPeriodUnit.SelectedValue.ToString().Trim() == "Select")
            {
                obj.strToBeHoldPeriod = "";
            }
            else
            {
                obj.strToBeHoldPeriod = ddlHoldPeriodUnit.SelectedValue.ToString().Trim() == "Select" ? "" : ddlHoldPeriodUnit.SelectedValue.ToString().Trim();
            }
            ////addes by sachin 06-03-2017
            obj.strSOPNo = txtCLenaingSOP.Text.Trim();

            ///

            obj.iSt = status();
            obj.strFlag = strFlag;
            obj.strCreatedBy = clsStandards.current_Username();
            obj.strModifyBy = clsStandards.current_Username();
            obj.strPreFilterFrom = txtReadingPref.Text.Trim();
            obj.strPreFilterTo = txtReadingPrefTo.Text.Trim();
            obj.strInterFrom = txtInterMediateFilterFrom.Text.Trim();
            obj.strInterTo = txtInterMediateFilterTo.Text.Trim();
            obj.strHepaFrom = txtHepaFilterReadingFrom.Text.Trim();
            obj.strHepaTo = txtHepaFilterReadingTo.Text.Trim();
            obj.strDispTempFrom = txtDispTempFrom.Text.Trim();
            obj.strDispTempTo = txtDispTempTo.Text.Trim();
            obj.strHumidityFrom = txtDispHumidityFrom.Text.Trim();
            obj.strHumidityTo = txtDispHumidiyTo.Text.Trim();
            obj.iRLAFTime = Convert.ToInt32(txtRLAFtime.Text.Trim() == "" ? "0" : "0");

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
            obj.strDept = strDepartment[0].ToString();
            obj.strRemark = txtRemark.Text.Trim();
            obj.strModuleNo = txtModuleNo.Text.Trim();
            strResult = objCubicle.BL_InsertCubicle(obj);


            if (strResult.StartsWith("0"))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "alert('" + strResult.Split('|').GetValue(1).ToString() + "');", true);
            }
            else if (strResult.StartsWith("1"))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "alert('" + strResult.Split('|').GetValue(1).ToString() + "');", true);
                return;

            }

            txtRemark.Text = "";
            VisibleRemarkFalse();
            clear();
        }
        catch (Exception ex)
        {
            clsStandards.WriteLog(this, ex, clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 0, true);
            return;
        }

    }

    //Clear All control
    public void clear()
    {
        //lblDesc.Text = string.Empty;
        txtCubicleCode.Text = string.Empty;
        txtCubicleDesc.Text = string.Empty;
        txtHoldPeriod.Text = string.Empty;
        txtHoldPeriodToBeClean.Text = string.Empty;
        txtSearch.Text = string.Empty;
        txtCLenaingSOP.Text = string.Empty;
        try { ddlDepartmentCode.SelectedValue = "Select"; }
        catch { }
        ddlPlantCode.Items.Clear();
        ddlPlantCode.Items.Add("Select");
        try { ddlPlantCode.SelectedValue = "Select"; }
        catch { }
        try { ddlHoldClean.SelectedValue = "Select"; }
        catch { }
        try { ddlHoldPeriodUnit.SelectedValue = "Select"; }
        catch { }
        RBactivate.Checked = true;

        txtCubicleCode.Enabled = true;
        ddlPlantCode.Enabled = true;
        ddlDepartmentCode.Enabled = true;
        ///addded by tejaswini
        ///
        txtRLAFtime.Text = string.Empty;
        txtReadingPref.Text = string.Empty;
        txtReadingPrefTo.Text = string.Empty;
        txtInterMediateFilterFrom.Text = string.Empty;
        txtInterMediateFilterTo.Text = string.Empty;
        txtHepaFilterReadingFrom.Text = string.Empty;
        txtHepaFilterReadingTo.Text = string.Empty;
        txtDispTempFrom.Text = string.Empty;
        txtDispTempTo.Text = string.Empty;
        txtDispHumidityFrom.Text = string.Empty;
        txtDispHumidiyTo.Text = string.Empty;
        txtModuleNo.Text = string.Empty;
        txtRemark.Text = string.Empty;

        BL_PlantMaster objPlant = new BL_PlantMaster();
        clsStandards.fillCombobox(ddlPlantCode, objPlant.BL_Get_Plant(clsStandards.current_Plant().ToString().Trim()), "PLANTCODE");
        objPlant = null;
    }

    //Populate Cubicle Master Data in Grid
    protected void imgDetails_Click(object sender, ImageClickEventArgs e)
    {
        BL_CubicleMaster objCubicle = new BL_CubicleMaster();
        try
        {
            clear();
            clsStandards.populateGrid(objCubicle.BL_GetcubicleDtl(clsStandards.WhereStatement_NoST(cboSearch.SelectedValue, cboCriteria.SelectedValue, txtSearch.Text.Trim()), clsStandards.current_Plant()), GRCUBICLE, "PLANTCODE");
            MultiView1.SetActiveView(View1);

        }
        catch (Exception ex)
        {
            clsStandards.WriteLog(this, ex, clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 0, true);
        }

    }

    //Populates the Entry view of Cubicle Master
    protected void imgAdd1_Click1(object sender, ImageClickEventArgs e)
    {
        BL_PlantMaster objPlant = new BL_PlantMaster();
        try
        {
            clear();
            VisibleRemarkFalse();
            MultiView1.SetActiveView(View2);
            strFlag = "ADD";
            clsStandards.fillCombobox(ddlPlantCode, objPlant.BL_Get_Plant(clsStandards.current_Plant().ToString().Trim()), "PLANTCODE");
        }
        catch (Exception ex)
        {
            clsStandards.WriteLog(this, ex, clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 0, true);
        }

    }

    //Exports Cubicle Data into Excel
    protected void imgExport_Click(object sender, ImageClickEventArgs e)
    {
        BL_CubicleMaster objMaster = new BL_CubicleMaster();
        try
        {
            GRCUBICLE.AllowPaging = false;
            GRCUBICLE.AllowSorting = false;

            //clsxlsExport.ExportExcel(GrPlant, "Company.xls");

            clsxlsExport.ConvertToxls(objMaster.BL_GetcubicleDtl(clsStandards.WhereStatement_NoST(cboSearch.SelectedValue, cboCriteria.SelectedValue, txtSearch.Text.Trim()), clsStandards.current_Plant()), "CubicleMaster");
        }
        catch (Exception ex)
        {
            clsStandards.WriteLog(this, ex, clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 0, true);
        }
        finally
        {
            objMaster = null;
        }
    }

    //Navigating from Detailed view to Entry View.
    protected void imgCloseV1_Click(object sender, ImageClickEventArgs e)
    {
        MultiView1.SetActiveView(View2);
    }

    //Importing Data From Excel and Populating in Grid
    protected void btnImp_Click(object sender, EventArgs e)
    {

        PL_PlantMastercs objPlant = new PL_PlantMastercs();
        BL_PlantMaster objBlPlant = new BL_PlantMaster();
        btnCancel.Visible = true;
        btnSave.Visible = true;
        GRCUBICLE_GRID.Visible = true;

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
                                    GRCUBICLE_GRID.DataSource = dtImport;
                                    GRCUBICLE_GRID.DataBind();
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

    //Inserting Cubicle Data which was imported from Excel.
    protected void btnSave_Click(object sender, EventArgs e)
    {
        int pass = 0;

        string strResult = "ADDEXCEL";
        PL_CubicleMaster obj = new PL_CubicleMaster();
        BL_CubicleMaster objCubicle = new BL_CubicleMaster();
        try
        {
            for (int i = 0; i < GRCUBICLE_GRID.Rows.Count; i++)
            {


                if (GRCUBICLE_GRID.Rows[i].Cells[0].Text.ToString() == clsStandards.current_Plant())
                {

                    obj.strPlantCode = GRCUBICLE_GRID.Rows[i].Cells[0].Text.ToString();
                    // obj.strPlantCode = clsStandards.current_Plant().ToString().Trim();
                    obj.strDept = GRCUBICLE_GRID.Rows[i].Cells[1].Text.ToString(); ;
                    obj.strDepartDesc = GRCUBICLE_GRID.Rows[i].Cells[1].Text.ToString();
                    obj.strCubicleCode = GRCUBICLE_GRID.Rows[i].Cells[2].Text.ToString();
                    obj.strCubicleDesc = GRCUBICLE_GRID.Rows[i].Cells[3].Text.ToString();
                    obj.IHoldPeriodClean = Convert.ToInt32(GRCUBICLE_GRID.Rows[i].Cells[4].Text.ToString());
                    obj.strHoldPeriodClean = GRCUBICLE_GRID.Rows[i].Cells[5].Text.ToString();
                    obj.IHoldPeriondToBeClean = Convert.ToInt32(GRCUBICLE_GRID.Rows[i].Cells[6].Text.ToString());
                    obj.strToBeHoldPeriod = GRCUBICLE_GRID.Rows[i].Cells[7].Text.ToString();
                    obj.strSOPNo = GRCUBICLE_GRID.Rows[i].Cells[8].Text.ToString();
                    obj.iSt = int.Parse(GRCUBICLE_GRID.Rows[i].Cells[9].Text.ToString());
                    obj.strRemark = "";
                    obj.strFlag = "ADDEXCEL";
                    obj.strCreatedBy = clsStandards.current_Username();
                    obj.strModifyBy = clsStandards.current_Username();
                    strResult = objCubicle.BL_InsertCubicle(obj);


                    if (strResult.StartsWith("0") == true)
                    {

                        pass = pass + 1;
                        //ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "alert('" + strResult.Split('|').GetValue(1).ToString() + "');", true);
                    }
                    else
                    {

                        GRCUBICLE_GRID.Rows[i].BackColor = System.Drawing.Color.Red;
                    }

                }
                else
                {

                    GRCUBICLE_GRID.Rows[i].BackColor = System.Drawing.Color.Red;
                }


            }

            clsStandards.WriteLog(this, new Exception(pass + " Out Of " + GRCUBICLE_GRID.Rows.Count + " Records Saved Sucessfully"), clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 0, true);
            strFlag = "ADD";
            pass = 0;
            return;
        }
        catch (Exception ex)
        {
            clsStandards.WriteLog(this, ex, clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 0, true);
            return;
        }

    }

    //Clear the Grid GRCUBICLE_GRID
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        try
        {
            GRCUBICLE_GRID.DataSource = null;
            GRCUBICLE_GRID.DataBind();
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

    //Populate data of seleced page index in grid GRCUBICLE.
    protected void GRCUBICLE_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        BL_CubicleMaster objCubicle = new BL_CubicleMaster();
        try
        {

            GRCUBICLE.PageIndex = e.NewPageIndex;
            clsStandards.populateGrid(objCubicle.BL_GetcubicleDtl(clsStandards.WhereStatement_NoST(cboSearch.SelectedValue, cboCriteria.SelectedValue, txtSearch.Text.Trim()), clsStandards.current_Plant()), GRCUBICLE, "PLANTCODE");
        }
        catch (Exception ex)
        {
            clsStandards.WriteLog(this, ex, clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 0, true);
        }
        finally
        {
            objCubicle = null;
        }
    }

    //Populate data of seleced page index in grid GRCUBICLE_GRID.
    protected void GRCUBICLE_GRID_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            dtImport = ViewState["Import"] == null ? new DataTable() : (DataTable)ViewState["Import"];
            GRCUBICLE_GRID.PageIndex = e.NewPageIndex;
            GRCUBICLE_GRID.DataSource = dtImport;
            GRCUBICLE_GRID.DataBind();
        }
        catch (Exception ex)
        {
            clsStandards.WriteLog(this, ex, clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 0, true);
        }
        finally
        {
        }

    }

    //Bind the data to grid GRCUBICLE_GRID with Page indexing
    protected void GRCUBICLE_GRID_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[0].Text = "Page " + (GRCUBICLE_GRID.PageIndex + 1) + " of " + GRCUBICLE_GRID.PageCount;
            e.Row.Cells[1].Text = clsStandards.FooterInfo(GRCUBICLE_GRID);
        }
    }

    //Fill the selected data into controls which is selected from Grid.
    protected void GRCUBICLE_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        BL_PlantMaster objPlant = new BL_PlantMaster();
        BL_DepartmentMaster objDepartment = new BL_DepartmentMaster();
        clsStandards.fillCombobox(ddlPlantCode, objPlant.BL_Get_Plant(clsStandards.current_Plant().ToString().Trim()), "PLANTCODE");
        clsStandards.fillCombobox(ddlDepartmentCode, objDepartment.BL_GetDepartmentCodeforPlant(clsStandards.current_Plant().ToString().Trim()), "DEPARTMENTCODE");

        BL_Chnage objchange = new BL_Chnage();

        if (e.CommandName == "Select")
        {

            int RowInd = ((GridViewRow)(((LinkButton)e.CommandSource).NamingContainer)).RowIndex;
            DataTable dt = new DataTable();
            BL_CubicleMaster objMaster = new BL_CubicleMaster();


            try
            {
                dt = objMaster.BL_GetcubicleDtl(clsStandards.WhereStatement_NoST("[REFNO]", "Equal To", GRCUBICLE.DataKeys[RowInd].Value.ToString()), clsStandards.current_Plant());
                if (dt.Rows.Count != 0)
                {


                    try
                    {
                        ddlPlantCode.SelectedValue = objchange.Bl_get_strPlant_desc(dt.Rows[0]["PLANTCODE"].ToString());
                        clsStandards.fillCombobox(ddlDepartmentCode, objMaster.BL_getDepartmet(dt.Rows[0]["PLANTCODE"].ToString()), "DepartmentCode");
                    }
                    catch { }
                    try { ddlDepartmentCode.SelectedValue = objchange.Bl_get_strDept_desc(dt.Rows[0]["DEPARTMENTCODE"].ToString().Replace("-", "")); }
                    catch { }
                    ddlDepartmentCode_SelectedIndexChanged(sender, e);

                    //lblDesc.Text = dt.Rows[0]["DEPARTMENTDESC"].ToString();
                    txtCubicleCode.Text = dt.Rows[0]["CUBICLECODE"].ToString();
                    txtCubicleDesc.Text = dt.Rows[0]["CUBICLEDESC"].ToString();
                    txtHoldPeriod.Text = dt.Rows[0]["HOLDPERIODCLEAN"].ToString();
                    try { ddlHoldClean.SelectedValue = dt.Rows[0]["HOLDPERIODCLEANUNIT"].ToString(); }
                    catch { }
                    txtHoldPeriodToBeClean.Text = dt.Rows[0]["HOLDPERIODTOBECLEAN"].ToString();
                    try { ddlHoldPeriodUnit.SelectedValue = dt.Rows[0]["HOLDPERIODTOBECLEANUNIT"].ToString(); }
                    catch { }
                    ///addded by TEjaswini
                    txtRemark.Text = dt.Rows[0]["REMARK"].ToString();
                    //txtRLAFtime.Text=
                    //txtReadingPref.Text
                    //txtReadingPrefTo.Text
                    //    txtInterMediateFilterFrom.Text
                    //    txtInterMediateFilterTo.Text
                    //        txtHepaFilterReadingFrom.Text
                    //        txtHepaFilterReadingTo.Text
                    //            txtDispTempFrom.Text
                    //            txtDispTempTo.Text
                    //                txtDispHumidityFrom.Text
                    //                txtDispHumidiyTo.Text

                    //                    txtModuleNo.Text

                    strFlag = "EDIT";
                    ViewState["REFNO"] = dt.Rows[0]["REFNO"].ToString();


                    RBactivate.Checked = ((dt.Rows[0]["St"].ToString() == "ACTIVATE") ? true : false);
                    RBdeactivate.Checked = ((dt.Rows[0]["St"].ToString() == "DEACTIVATE") ? true : false);

                    txtRemark.Text = dt.Rows[0]["REMARK"].ToString();

                    txtCLenaingSOP.Text = dt.Rows[0]["CL_SOP_NO"].ToString();

                    txtCubicleCode.Enabled = false;
                    ddlPlantCode.Enabled = false;
                    ddlDepartmentCode.Enabled = false;
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
                objMaster = null;
            }


        }

    }

    //Bind the data to grid GRCUBICLE with Page indexing
    protected void GRCUBICLE_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[0].Text = "Page " + (GRCUBICLE.PageIndex + 1) + " of " + GRCUBICLE.PageCount;
            e.Row.Cells[1].Text = clsStandards.FooterInfo(GRCUBICLE);
        }


    }

    //Populates Data in Grid Against Search Condition.
    protected void imgSearch_Click(object sender, ImageClickEventArgs e)
    {
        BL_CubicleMaster objCubicle = new BL_CubicleMaster();
        try
        {
            clsStandards.populateGrid(objCubicle.BL_GetcubicleDtl(clsStandards.WhereStatement_NoST(cboSearch.SelectedValue, cboCriteria.SelectedValue, txtSearch.Text.Trim()), clsStandards.current_Plant()), GRCUBICLE, "PLANTCODE");
            MultiView1.SetActiveView(View1);
        }
        catch (Exception ex)
        {
            clsStandards.WriteLog(this, ex, clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 0, true);
        }
        finally
        {
            objCubicle = null;
        }

    }
    protected void ddlDepartmentCode_SelectedIndexChanged(object sender, EventArgs e)
    {
        BL_CubicleMaster objCubicle = new BL_CubicleMaster();



        string[] strDepartment = ddlDepartmentCode.SelectedItem.Text.Split('-');


        if (strDepartment.Length > 2)
        {
            strGDEPARTMENT = strDepartment[0];
            strGDEPARTMENT_DESC = strDepartment[1] + '-' + strDepartment[2];
        }
        else
        {
            if (strDepartment[0] != "Select")
            {
                strGDEPARTMENT = strDepartment[0];
                strGDEPARTMENT_DESC = strDepartment[1];
            }
        }

        try
        {

            //lblDesc.Text = objCubicle.BL_GetDeptDesc(ddlDepartmentCode.Text).Rows[0][0].ToString();
            //lblDesc.Text = strGDEPARTMENT_DESC;
            //clsStandards.fillCombobox(ddlPlantCode, objCubicle.BL_getDepartmentPlantCode(ddlDepartmentCode.Text), "PLANTCODE");
        }
        catch (Exception ex)
        {
            clsStandards.WriteLog(this, ex, clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 0, true);
        }
        finally
        {
            objCubicle = null;
        }
    }


    protected void ddlPlantCode_SelectedIndexChanged(object sender, EventArgs e)
    {
        //string strPlnatCode;
        string[] plantcode = ddlPlantCode.SelectedItem.Text.Split('-');
        strGPLANT = plantcode[0];
        strGPLANT_DESC = plantcode[1];

        DataTable dt = new DataTable();

        BL_CubicleMaster objCubicle = new BL_CubicleMaster();
        try
        {

            clsStandards.fillCombobox(ddlDepartmentCode, objCubicle.BL_getDepartmet(plantcode[0]), "DepartmentCode");
        }
        catch (Exception ex)
        {
            clsStandards.WriteLog(this, ex, clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 0, true);
        }
        finally
        {
            objCubicle = null;
        }
    }

    //added by tejaswini

    protected void btnTemplate_Click(object sender, EventArgs e)
    {
        try
        {

            Response.ContentType = "application/ms-excel";
            Response.AddHeader("content-disposition", "inline; filename=CubicalMaster.xlsx");
            Response.TransmitFile(Server.MapPath("~/PrnFile/Cubical_Master.xlsx"));
            Response.Flush();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }


}