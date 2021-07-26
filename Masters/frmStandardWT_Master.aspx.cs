using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Drawing;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLayer;
using PropertyLayer;
using System.Configuration;
using System.IO;
using System.Data.OleDb;
using System.Data;


public partial class frmStandardWT_Master : System.Web.UI.Page
{
    public static string strFlag = "";
    DataTable dtImport = new DataTable();
    //On Page Load, Fill Plant Code ComboBox, Fill Department Code ComboBox, Fill Data in Grid
    protected void Page_Load(object sender, EventArgs e)
    {
        ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);
        scriptManager.RegisterPostBackControl(this.btnTemplate);

        BL_Chnage objchange = new BL_Chnage();
        BL_CubicleMaster objCubicle = new BL_CubicleMaster();
        BL_UserMaster objUser = new BL_UserMaster();
        BL_AreaMaster objArea = new BL_AreaMaster();
        BL_PlantMaster objPlant = new BL_PlantMaster();
        BL_DepartmentMaster objDepartment = new BL_DepartmentMaster();
        try
        {
            if (!IsPostBack)
            {
                DVisibleRemark();
                MultiView1.SetActiveView(View2);
                strFlag = "ADD"; ;
                CalendarExtender1.StartDate = DateTime.Now.AddDays(1);
                CalendarExtender2.EndDate = DateTime.Now;
                ddlArea.Visible = false;
                Label2.Visible = false;

                //txtStandardWT_No.Attributes.Add("onkeypress", "return isNumberKey(event)");
                //txtStamp_Cert_No.Attributes.Add("onkeypress", "return isNumberKey(event)");

                //clsStandards.fillCombobox(ddlPlantCode, objUser.getPlant(""), "PLANTCODE");
                //clsStandards.fillCombobox(ddlArea, objArea.BL_getArea(), "AREA_CODE");
                //clsStandards.fillCombobox(ddDepartment, objArea.getDept11(), "DepartmentCode");

                //clsStandards.fillCombobox(ddlPlantCode, objPlant.BL_Get_Plant_with_Desc(), "PLANTCODE");
                clsStandards.fillCombo(DD_Plantcode, objchange.Bl_get_strPlant_desc(clsStandards.current_Plant()));
                clsStandards.fillCombobox(ddlArea, objCubicle.BL_GetcubicleCode(clsStandards.current_Plant().Trim().ToString()), "CUBICLECODE");
                clsStandards.fillCombobox(ddDepartment, objDepartment.BL_GetDepartmentCode(), "DEPARTMENTCODE");
                GrStandard_Grid.DataSource = null;
                GrStandard_Grid.DataBind();
                pnlImport.Visible = false;
                clear();
            }
        }
        catch (Exception ex)
        {
            clsStandards.WriteLog(this, ex, clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 0, true);
        }
        finally
        {
            objUser = null;
        }



    }
    //Clear All control

    public void VisibleRemark()
    {
        Re.Visible = true;
        txtRemark.Visible = true;
    }

    public void DVisibleRemark()
    {
        Re.Visible = false;
        txtRemark.Visible = false;
    }
    public void clear()
    {
        DVisibleRemark();
        txtRemark.Text = string.Empty;
        txtStandardWT_No.Text = string.Empty;
        txtCapacity.Text = string.Empty;
        txtStamp_Cert_No.Text = string.Empty;
        txtStamp_DONE.Text = string.Empty;
        TxtStamp_Due_ON.Text = string.Empty;
        RequiredFieldValidator1.Enabled = true;
        RBactivate.Checked = true;
        RBdeactivate.Checked = false;
        //if (ChkActivate.Checked == false)
        //{
        //    ChkActivate.Checked = false;
        //    ChkActivate.Text = "DeActivate";
        //}
        //else
        //{
        //    ChkActivate.Checked = true;
        //    ChkActivate.Text = "Activate";
        //} 
        TxtAreaDesc.Text = string.Empty;
        //txtDept.Text = string.Empty;
        TxtPlantDESC.Text = string.Empty;
        DD_Plantcode.Enabled = true;
        txtStandardWT_No.Enabled = true;
        try
        {
            DD_Plantcode.SelectedValue = "Select";
        }
        catch (Exception ex)
        {
            clsStandards.WriteLog(this, ex, clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 0, true);
        }

        try { ddDepartment.SelectedValue = "Select"; }
        catch { }

        try { ddlArea.SelectedValue = "Select"; }
        catch { }
    }
    //Importing Data From Excel and Populating in Grid
    protected void btnImp_Click(object sender, EventArgs e)
    {
        btnCancel.Visible = true;
        btnSave.Visible = true;
        GrStandard_Grid.Visible = true;

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
                                    GrStandard_Grid.DataSource = dtImport;
                                    GrStandard_Grid.DataBind();
                                    ViewState["Import"] = dtImport;
                                    pnlImport.Visible = true;
                                }
                                else
                                {
                                    clsStandards.WriteLog(this, new Exception("No records found in excel"), clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 1, true);
                                }
                            }
                        }
                    }
                }
                else
                {

                    clsStandards.WriteLog(this, new Exception("File Format Not Supported"), clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 1, true);

                }
            }
        }
        catch (Exception ex)
        {
            clsStandards.WriteLog(this, new Exception("File Format Not Supported"), clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 1, true);

        }
    }
    //Inserting Area Data which was imported from Excel.

    protected void btnSave_Click(object sender, EventArgs e)
    {
        string strResult = "";
        PL_Standard_WT_Master obj = new PL_Standard_WT_Master();
        BL_Standard_Master objStandard = new BL_Standard_Master();
        int iFailed = 0;
        int pass = 0;
        dtImport = (DataTable)ViewState["Import"];
        try
        {
            if (GrStandard_Grid.Rows.Count > 0)
            {

                for (int i = 0; i < GrStandard_Grid.Rows.Count; i++)
                {
                    if (clsStandards.filter(GrStandard_Grid.Rows[i].Cells[0].Text) == clsStandards.current_Plant())
                    {
                        obj.strPlantCode = clsStandards.filter(GrStandard_Grid.Rows[i].Cells[0].Text);
                        obj.strStandard_wt_no = clsStandards.filter(GrStandard_Grid.Rows[i].Cells[1].Text);
                        obj.strDept = clsStandards.filter(GrStandard_Grid.Rows[i].Cells[2].Text);
                        obj.strUOM = clsStandards.filter(GrStandard_Grid.Rows[i].Cells[3].Text);
                        obj.strCapacity = clsStandards.filter(GrStandard_Grid.Rows[i].Cells[4].Text);



                        obj.strStandard_c_No = clsStandards.filter(GrStandard_Grid.Rows[i].Cells[5].Text);
                        obj.strStamping_Done_ON = clsStandards.filter(GrStandard_Grid.Rows[i].Cells[6].Text);
                        obj.strStamping_Due_ON = clsStandards.filter(GrStandard_Grid.Rows[i].Cells[7].Text);
                        obj.strArea = clsStandards.filter(GrStandard_Grid.Rows[i].Cells[8].Text);


                        obj.intStatus = Convert.ToInt32(GrStandard_Grid.Rows[i].Cells[9].Text);


                        // obj.intStatus = Convert.ToInt32(dtImport.Rows[i][8].ToString());
                        obj.strFlag = "ADD";
                        obj.strCreatedBy = clsStandards.current_Username();
                        obj.strModifyBy = clsStandards.current_Username();
                        if (strFlag == "EDIT" && ViewState["REFNO"].ToString() != "")
                        {
                            obj.REFNO = Convert.ToInt32(ViewState["REFNO"].ToString());
                        }
                        else
                        {
                            obj.REFNO = 0;
                        }


                        strResult = objStandard.BL_InsertStandard_WT_Master(obj);

                        if (strResult.StartsWith("0") == false)
                        {
                            GrStandard_Grid.Rows[i].BackColor = Color.Red;
                            //  GrStandard_Grid.Rows[i].Cells[GrStandard_Grid.Columns.Count - 1].Text = strResult.Split('|').GetValue(1).ToString();
                            iFailed++;
                            //clsStandards.WriteLog(this, new Exception(strResult.Split('|').GetValue(1).ToString()), clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 1, true);
                        }
                        else
                        {
                            pass = pass + 1;
                        }

                    }
                    else
                    {
                        GrStandard_Grid.Rows[i].BackColor = Color.Red;

                    }



                }


                clsStandards.WriteLog(this, new Exception(pass.ToString() + "  Out Of " + GrStandard_Grid.Rows.Count.ToString() + " Details saved successfully"), clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 2, true);
                //if (iFailed == 0)
                //{
                //    GrStandard_Grid.DataSource = null;
                //    GrStandard_Grid.DataBind();
                //    pnlImport.Visible = false;
                //}
                //else
                //{
                //    GrStandard_Grid.Columns[GrStandard_Grid.Columns.Count - 1].Visible = true;
                //}
                //pnlImport.Visible = false;
                //GrStandard_Grid.DataSource = null;

                //GrStandard_Grid.DataBind();
                clear();
            }

        }
        catch (Exception ex)
        {
            clsStandards.WriteLog(this, ex, clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 0, true);
        }
        finally
        {
            strResult = null;
            obj = null;
            //objStandard = null;
            //ViewState["Import"] = null;
        }
    }

    //Fill the selected data into controls which is selected from Grid.

    protected void GrStandardDTL_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[0].Text = "Page " + (GrStandardDTL.PageIndex + 1) + " of " + GrStandardDTL.PageCount;
            e.Row.Cells[1].Text = clsStandards.FooterInfo(GrStandardDTL);
        }

    }

    protected void GrStandard_Grid_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            dtImport = ViewState["Import"] == null ? new DataTable() : (DataTable)ViewState["Import"];
            GrStandard_Grid.PageIndex = e.NewPageIndex;
            GrStandard_Grid.DataSource = dtImport;
            GrStandard_Grid.DataBind();
        }
        catch (Exception ex)
        {
            clsStandards.WriteLog(this, ex, clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 0, true);
        }
        finally
        {
        }
    }

    protected void GrStandard_Grid_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[0].Text = "Page " + (GrStandard_Grid.PageIndex + 1) + " of " + GrStandard_Grid.PageCount;
            e.Row.Cells[1].Text = clsStandards.FooterInfo(GrStandard_Grid);
        }
    }

    //Inserting Area Data which is added manually.
    protected void imgSave_Click(object sender, ImageClickEventArgs e)
    {
        PL_Standard_WT_Master obj = new PL_Standard_WT_Master();
        BL_Standard_Master objStandard = new BL_Standard_Master();
        string strResult = "";

        if (ddUnit.SelectedItem.Text == "Select")
        {
            clsStandards.WriteLog(this, new Exception("Select A Unit For Capacity "), clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 2, true);
            return;
        }

        if (txtCapacity.Text == null || txtCapacity.Text.Trim() == "")
        {
            clsStandards.WriteLog(this, new Exception("Enter Capacity "), clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 2, true);
            return;
        }
        if (strFlag == "EDIT" && txtRemark.Text.Trim() == "")
        {
            clsStandards.WriteLog(this, new Exception("Remark Field Empty "), clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 0, true);
            return;
        }


        string strCapacity = txtCapacity.Text.ToString();
        //int i = DateTime.Compare(Convert.ToDateTime(TxtStamp_Due_ON), Convert.ToDateTime(txtStamp_DONE));
        //if(i<0 || i==0)
        //{
        //    clsStandards.WriteLog(this, new Exception("Select Proper Date For Certificate Date"), clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 2, true);
        //    return;
        //}


        try
        {

            string data = DD_Plantcode.Text.ToString();
            string[] splitData = data.Split('-');
            obj.strPlantCode = splitData[0].ToString();

            obj.strStandard_wt_no = txtStandardWT_No.Text.Trim();

            string data1 = ddDepartment.Text.ToString();
            string[] splitData1 = data1.Split('-');
            obj.strDept = splitData1[0].ToString();

            obj.strCapacity = strCapacity.Trim();
            obj.strStandard_c_No = txtStamp_Cert_No.Text.Trim();
            obj.strStamping_Done_ON = txtStamp_DONE.Text;
            obj.strStamping_Due_ON = TxtStamp_Due_ON.Text;
            obj.strRemark = txtRemark.Text.Trim();

            if (ddlArea.SelectedValue == "Select")
            {
                obj.strArea = string.Empty;
            }
            else
            {

                string data_A = ddlArea.Text.ToString();
                string[] splitData_A = data_A.Split('-');
                obj.strArea = splitData_A[0].ToString().Replace("-", "");
            }

            //  obj.intStatus = clsStandards.ActivationStatus(ChkActivate);
            obj.intStatus = status();
            obj.strFlag = strFlag;
            obj.strCreatedBy = clsStandards.current_Username();
            obj.strModifyBy = clsStandards.current_Username();
            if (strFlag == "EDIT" && ViewState["REFNO"].ToString() != "")
            {
                obj.REFNO = Convert.ToInt32(ViewState["REFNO"].ToString());
            }
            else
            {
                obj.REFNO = 0;
            }

            obj.strUOM = ddUnit.Text.Trim();
            strResult = objStandard.BL_InsertStandard_WT_Master(obj);
            if (strResult.StartsWith("0"))
            {
                clsStandards.WriteLog(this, new Exception(strResult.Split('|').GetValue(1).ToString()), clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 2, true);
                clear();
            }
            else if (strResult.StartsWith("1"))
            {
                clsStandards.WriteLog(this, new Exception(strResult.Split('|').GetValue(1).ToString()), clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 1, true);
            }
            else
            {
                clsStandards.WriteLog(this, new Exception("Problem in save."), clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 0, true);
            }

        }
        catch (Exception ex)
        {
            clsStandards.WriteLog(this, ex, clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 0, true);
            return;
        }
        finally
        {
            obj = null;
            objStandard = null;
        }
    }
    //Populate printer Master Data in Grid

    protected void imgDetails_Click(object sender, ImageClickEventArgs e)
    {
        BL_Standard_Master objStandard = new BL_Standard_Master();
        try
        {
            clsStandards.populateGrid(objStandard.BL_GetStandardDtl(clsStandards.WhereStatement_NoST(cboSearch.SelectedValue, cboCriteria.SelectedValue, txtSearch.Text.Trim()), clsStandards.current_Plant()), GrStandardDTL, "PLANTCODE");
            MultiView1.SetActiveView(View1);

        }
        catch (Exception ex)
        {
            clsStandards.WriteLog(this, ex, clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 0, true);
        }
        finally
        {
            objStandard = null;
        }
    }
    //populates the entry od Add Master
    protected void imgAdd1_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            MultiView1.SetActiveView(View2);
            pnlImport.Visible = false;
            clear();
            strFlag = "ADD";
        }
        catch (Exception ex)
        {
            clsStandards.WriteLog(this, ex, clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 0, true);
        }
    }

    //Export gridview data in Excel
    protected void imgExport_Click(object sender, ImageClickEventArgs e)
    {
        BL_Standard_Master objStandard = new BL_Standard_Master();
        try
        {
            GrStandardDTL.AllowPaging = false;
            GrStandardDTL.AllowSorting = false;

            //clsxlsExport.ExportExcel(GrPlant, "Company.xls");

            clsxlsExport.ConvertToxls(objStandard.BL_GetStandardDtl(clsStandards.WhereStatement(0, cboSearch.SelectedValue, cboCriteria.SelectedValue, txtSearch.Text.Trim()), clsStandards.current_Plant()), "StandardWT_Master");
        }
        catch (Exception ex)
        {
            clsStandards.WriteLog(this, ex, clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 0, true);
        }
        finally
        {
            objStandard = null;
        }
    }

    //Populate data of seleced page index in grid GRPlant
    protected void GrStandardDTL_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        BL_Standard_Master objStandard = new BL_Standard_Master();
        try
        {
            GrStandardDTL.PageIndex = e.NewPageIndex;
            clsStandards.populateGrid(objStandard.BL_GetStandardDtl(clsStandards.WhereStatement_NoST(cboSearch.SelectedValue, cboCriteria.SelectedValue, txtSearch.Text.Trim()), clsStandards.current_Plant()), GrStandardDTL, "PLANTCODE");
        }
        catch (Exception ex)
        {
            clsStandards.WriteLog(this, ex, clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 0, true);
        }
        finally
        {
            objStandard = null;
        }
    }
    //Bind the data to grid  with Page indexing
    protected void GrStandardDTL_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        DataTable dt = new DataTable();
        BL_Standard_Master objStandard = new BL_Standard_Master();
        BL_Chnage objChnage = new BL_Chnage();
        BL_CubicleMaster objCubicle = new BL_CubicleMaster();
        if (e.CommandName == "Select")
        {

            int RowInd = ((GridViewRow)(((LinkButton)e.CommandSource).NamingContainer)).RowIndex;

            try
            {
                clsStandards.fillCombo(DD_Plantcode, objChnage.Bl_get_strPlant_desc(clsStandards.current_Plant()));
                clsStandards.fillCombobox(ddlArea, objCubicle.BL_GetcubicleCode(clsStandards.current_Plant().Trim().ToString()), "CUBICLECODE");
                dt = objStandard.BL_GetStandardDtl(clsStandards.WhereStatement_NoST("[REFNO]", "Equal To", GrStandardDTL.DataKeys[RowInd].Value.ToString()), clsStandards.current_Plant());
                if (dt.Rows.Count != 0)
                {
                    try
                    {
                        DD_Plantcode.SelectedValue = objChnage.Bl_get_strPlant_desc(dt.Rows[0]["PLANTCODE"].ToString());
                        RequiredFieldValidator1.Enabled = false;
                    }
                    catch { }
                    DD_Plantcode_SelectedIndexChanged(sender, e);
                    try
                    {

                        ddDepartment.SelectedValue = objChnage.Bl_get_strDept_desc(dt.Rows[0]["DEPARTMENT"].ToString());
                    }
                    catch (Exception ex)
                    {
                        throw ex;


                    }
                    ddDepartment_SelectedIndexChanged(sender, e);
                    try
                    {

                        ddlArea.SelectedValue = objChnage.Bl_get_strCubicle_desc(dt.Rows[0]["AREA"].ToString());
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                    ddlArea_SelectedIndexChanged(sender, e);

                    string[] strTemp = dt.Rows[0]["CAPACITY"].ToString().Split(' ');


                    txtCapacity.Text = strTemp[0]; ;
                    ddUnit.SelectedValue = strTemp[1];
                    txtStandardWT_No.Text = dt.Rows[0]["STANDARD_WT_NO"].ToString();
                    txtStamp_Cert_No.Text = dt.Rows[0]["STAMPING_CERTIFICATE_NO"].ToString();
                    //string[] strDate = dt.Rows[0]["STAMPING_DONE_ON"].ToString().Split(' ');
                    //txtStamp_DONE.Text = dt.Rows[0]["STAMPING_DONE_ON"].ToString();
                    //strDate = null;
                    //strDate = dt.Rows[0]["STAMPING_DUE_ON"].ToString().Split(' ');
                    //TxtStamp_Due_ON.Text = dt.Rows[0]["STAMPING_DUE_ON"].ToString();


                    string[] strDate = dt.Rows[0]["STAMPING_DONE_ON"].ToString().Split(' ');
                    txtStamp_DONE.Text = strDate[0];
                    strDate = null;
                    strDate = dt.Rows[0]["STAMPING_DUE_ON"].ToString().Split(' ');
                    TxtStamp_Due_ON.Text = strDate[0];
                    //added by tejaswini
                    txtRemark.Text = dt.Rows[0]["remark"].ToString();

                    //[STANDARD_WT_NO],[DEPARTMENT],[STAMPING_CERTIFICATE_NO],[STAMPING_DONE_ON],[STAMPING_DUE_ON],[AREA],[STATUS]
                    strFlag = "EDIT";
                    ViewState["REFNO"] = dt.Rows[0]["REFNO"].ToString();
                    //if (dt.Rows[0]["STATUS"].ToString() == "Activate")
                    //{
                    //    ChkActivate.Checked = true;
                    //    ChkActivate.Text = "Activate";
                    //}
                    //else
                    //{
                    //    ChkActivate.Checked = false;
                    //    ChkActivate.Text = "DeActivate";
                    //}
                    RBactivate.Checked = ((dt.Rows[0]["Status"].ToString() == "Activate") ? true : false);
                    RBdeactivate.Checked = ((dt.Rows[0]["Status"].ToString() == "Deactivate") ? true : false);
                    pnlImport.Visible = false;
                    // ddlArea.Text = dt.Rows[0]["STANDARD_WT_NO"].ToString();
                    // TxtAreaDesc.Text = dt.Rows[0]["AREA"].ToString();
                    DD_Plantcode.Enabled = false;
                    txtStandardWT_No.Enabled = false;
                    VisibleRemark();
                    MultiView1.SetActiveView(View2);

                }
                else
                {
                    clsStandards.WriteLog(this, new Exception("Problem fetching details for selected Standard Master."), clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 0, true);
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
                objStandard = null;
            }


        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        try
        {
            GrStandard_Grid.DataSource = null;
            GrStandard_Grid.DataBind();
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
    //search data from Gridview
    protected void imgSearch_Click(object sender, ImageClickEventArgs e)
    {
        BL_Standard_Master objStandard = new BL_Standard_Master();
        try
        {
            clsStandards.populateGrid(objStandard.BL_GetStandardDtl(clsStandards.WhereStatement(0, cboSearch.SelectedValue, cboCriteria.SelectedValue, txtSearch.Text.Trim()), clsStandards.current_Plant()), GrStandardDTL, "PLANTCODE");
            MultiView1.SetActiveView(View1);
        }
        catch (Exception ex)
        {
            clsStandards.WriteLog(this, ex, clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 0, true);
        }
        finally
        {
            objStandard = null;
        }

    }

    protected void imgCLose_Click(object sender, ImageClickEventArgs e)
    {
        MultiView1.SetActiveView(View2);
    }


    protected void ddDepartment_SelectedIndexChanged(object sender, EventArgs e)
    {

        //if (ddDepartment.SelectedValue == "Select")
        //{
        //    txtDept.Text = string.Empty;
        //    ddlArea.Items.Clear();
        //    TxtAreaDesc.Text = string.Empty;
        //    return;
        //}



        //BL_AreaMaster objArea = new BL_AreaMaster();
        //BL_Standard_Master objStdWt = new BL_Standard_Master();
        //try
        //{
        //    string data1 = ddDepartment.Text.ToString();
        //    string[] splitData1 = data1.Split('-');

        //    if (splitData1.Length > 2)
        //    {
        //        txtDept.Text = splitData1[1] + '-' + splitData1[2];
        //    }
        //    else
        //    {
        //        txtDept.Text = splitData1[1];
        //    }



        //    string data11 = DD_Plantcode.Text.ToString();
        //    string[] splitData_P = data11.Split('-');
        //    //txtDept.Text = (objArea.getDeptDESC(DD_DeptCode.SelectedValue.ToString())).Rows[0][0].ToString();

        //    clsStandards.fillCombobox(ddlArea, objStdWt.BL_GetAreaCode(splitData_P[0].ToString(), splitData1[0].ToString()), "AREA_CODE");

        //}
        //catch (Exception ex)
        //{
        //    clsStandards.WriteLog(this, ex, clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 0, true);
        //}
        //finally
        //{

        //    objStdWt = null;
        //}
    }

    protected void ddlArea_SelectedIndexChanged(object sender, EventArgs e)
    {
        BL_AreaMaster objArea = new BL_AreaMaster();
        DataTable dt = new DataTable();

        if (ddlArea.SelectedValue == "Select")
        {
            TxtAreaDesc.Text = string.Empty;
            return;
        }

        try
        {
            //TxtAreaDesc.Text = (objArea.getAreaDESC(ddlArea.SelectedValue.ToString())).Rows[0][0].ToString();
            string data1 = ddlArea.Text.ToString();
            string[] splitData1 = data1.Split('-');

            if (splitData1.Length > 2)
            {
                TxtAreaDesc.Text = splitData1[1] + '-' + splitData1[2];
            }
            else
            {
                TxtAreaDesc.Text = splitData1[1];
            }



            // clsStandards.fillCombobox(ddlCubicleCode, objPrinter.BL_GetCubicle(ddlPlantCode.SelectedValue.ToString(), ddlArea.SelectedValue.ToString()), "CUBICLE_ID");
        }
        catch (Exception ex)
        {
            clsStandards.WriteLog(this, ex, clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 0, true);
        }
        finally
        {

            objArea = null;
        }
    }
    protected void imgCloseV1_Click(object sender, ImageClickEventArgs e)
    {

    }

    //protected void ChkActivate_CheckedChanged(object sender, EventArgs e)
    //{
    //    if (ChkActivate.Checked == true)
    //    {
    //        ChkActivate.Text = "Activate";
    //    }
    //    else
    //    {
    //        ChkActivate.Text = "DeActivate";
    //    }
    //}
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
    protected void DD_Plantcode_SelectedIndexChanged(object sender, EventArgs e)
    {
        BL_AreaMaster objArea = new BL_AreaMaster();
        BL_CubicleMaster objCubicle = new BL_CubicleMaster();
        DataTable dt = new DataTable();
        if (DD_Plantcode.SelectedValue == "Select")
        {
            ddDepartment.Items.Clear();
            TxtPlantDESC.Text = string.Empty;
            //txtDept.Text = string.Empty;
            ddlArea.Items.Clear();
            TxtAreaDesc.Text = string.Empty;

            return;
        }

        try
        {
            string data = DD_Plantcode.SelectedItem.Text.ToString();

            BL_PrinterMaster objPrinter = new BL_PrinterMaster();
            string[] splitData = data.Split('-');


            if (splitData.Length > 2)
            {
                TxtPlantDESC.Text = splitData[1] + '-' + splitData[2];
            }
            else
            {
                TxtPlantDESC.Text = splitData[1];
            }


            // TxtPlantDESC.Text = splitData[1];

            //dt = objPrinter.BL_GetAreaCode(splitData[0].ToString());
            //if (dt.Rows.Count > 0)
            //{
            //    txtAreaCode.Text = dt.Rows[0][0].ToString();
            //    //string data_DESC = dt.Rows[0][0].ToString();
            //    //string[] splitData_DESC = data_DESC.Split('-');
            //    //txtAreaDesc.Text = splitData_DESC[1];
            //}
            clsStandards.fillCombobox(ddDepartment, objArea.GetDeptCode(splitData[0].ToString()), "DEPT_CODE");
            clsStandards.fillCombobox(ddlArea, objCubicle.BL_GetcubicleCode(clsStandards.current_Plant().Trim().ToString()), "CUBICLECODE");
            //TxtPlantDESC.Text = (objArea.BL_Get_PlantDESC(ddlPlantCode.SelectedValue.ToString())).Rows[0][0].ToString();

        }
        catch (Exception ex)
        {
            clsStandards.WriteLog(this, ex, clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 0, true);
        }
        finally
        {

            objArea = null;
        }
    }
    protected void btnTemplate_Click(object sender, EventArgs e)
    {
        try
        {

            Response.ContentType = "application/ms-excel";
            Response.AddHeader("content-disposition", "inline; filename=BoothMaster.xlsx");
            Response.TransmitFile(Server.MapPath("~/PrnFile/StandardWT.xlsx"));
            Response.Flush();
        }
        catch (Exception)
        {
        }
    }
}