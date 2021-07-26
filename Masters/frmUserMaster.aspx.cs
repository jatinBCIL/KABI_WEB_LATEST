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

public partial class frmUser_Master : System.Web.UI.Page
{

    public static string strFlg = "";
    DataTable dtImport = new DataTable();

    protected void Page_Load(object sender, EventArgs e)
    {
        #region
        //if (!IsPostBack)btnTemplate
        //{
        //    ViewState["EDIT"] = null;
        //    BL_PlantMaster objPlant = new BL_PlantMaster();
        //    BL_DepartmentMaster objDepartment = new BL_DepartmentMaster();
        //    BL_UserMaster objMaster = new BL_UserMaster();
        //    DataTable dt_AscPlant = new DataTable();
        //    DataTable dt = new DataTable();
        //    DataTable dt_Plant = new DataTable();
        //    DataSet ds = new DataSet();
        //    try
        //    {



        //        ds = objMaster.BL_GetuserRols("");
        //        if (ds.Tables[0].Rows.Count > 0)
        //        {
        //            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        //            {
        //                cblTRole.Items.Add(ds.Tables[0].Rows[i][0].ToString());

        //            }

        //        }
        //        if (ds.Tables[1].Rows.Count > 0)
        //        {
        //            for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
        //            {

        //                cblDRole.Items.Add(ds.Tables[1].Rows[i][0].ToString());
        //            }

        //        }
        //        //clsStandards.fillCombobox(ddPlantcode, objPlant.BL_Get_Plant(), "PLANTCODE");
        //        clsStandards.fillCombobox(ddPlantcode, objPlant.BL_Get_Plant_with_Desc(), "PLANTCODE");
        //        //   clsStandards.fillCombobox(ddDepartment, objDepartment.BL_GetDepartmentCode(), "DEPARTMENTCODE");

        //        //dt = objDepartment.BL_GetDepartmentCode();
        //        //if (dt.Rows.Count > 0)
        //        //{
        //        //    for (int i = 0; i < dt.Rows.Count; i++)
        //        //    {
        //        //        cblTDept.Items.Add(dt.Rows[i][0].ToString());
        //        //        cblDDept.Items.Add(dt.Rows[i][0].ToString());
        //        //    }
        //        //}

        //        dt_Plant = objPlant.BL_Get_Plant_with_Desc();

        //        dt_AscPlant = objPlant.BL_getAsc_Desc();
        //        if (dt_Plant.Rows.Count > 0)
        //        {
        //            for (int i = 0; i < dt_Plant.Rows.Count; i++)
        //            {
        //                cblTPlant.Items.Add(dt_Plant.Rows[i][0].ToString());
        //                cblDPlant.Items.Add(dt_Plant.Rows[i][0].ToString());
        //            }
        //        }
        //        clsStandards.fillCombobox(ddlUserid, objMaster.BL_GetUserID(), "USER_ID");

        //        grPlant1.DataSource = null;
        //        grPlant1.DataBind();
        //        pnlImport.Visible = false;

        //        grdExcelRight.DataSource = null;
        //        grdExcelRight.DataBind();
        //        Panel1.Visible = false;
        //    }
        //    catch (Exception ex)
        //    {
        //        clsStandards.WriteLog(this, ex, clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 0, true);
        //    }
        //    finally
        //    {
        //        objPlant = null;
        //    }
        //    MultiView1.SetActiveView(View2);
        //    strFlg = "ADD";
        //}
        #endregion
        ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);
        scriptManager.RegisterPostBackControl(this.btnTemplate);
        scriptManager.RegisterPostBackControl(this.btnTemplate1);

        if (!IsPostBack)
        {

            ViewState["EDIT"] = null;
            BL_PlantMaster objPlant = new BL_PlantMaster();
            //BL_DepartmentMaster objDepartment = new BL_sDepartmentMaster();
            BL_UserMaster objMaster = new BL_UserMaster();
            DataTable dt = new DataTable();
            DataTable dt_Plant = new DataTable();
            DataTable dt_AscPlant = new DataTable();
            DataSet ds = new DataSet();
            try
            {
                ds = objMaster.BL_GetuserRols("");
                if (ds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        cblTRole.Items.Add(ds.Tables[0].Rows[i][0].ToString());

                    }

                }
                if (ds.Tables[1].Rows.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
                    {

                        //cblDRole.Items.Add(ds.Tables[1].Rows[i][0].ToString());
                    }

                }
                //clsStandards.fillCombobox(ddPlantcode, objPlant.BL_Get_Plant(), "PLANTCODE");
                clsStandards.fillCombobox(ddPlantcode, objPlant.BL_Get_Plant_with_Desc(), "PLANTCODE");
                //   clsStandards.fillCombobox(ddDepartment, objDepartment.BL_GetDepartmentCode(), "DEPARTMENTCODE");

                //dt = objDepartment.BL_GetDepartmentCode();
                //if (dt.Rows.Count > 0)
                //{
                //    for (int i = 0; i < dt.Rows.Count; i++)
                //    {
                //        cblTDept.Items.Add(dt.Rows[i][0].ToString());
                //        cblDDept.Items.Add(dt.Rows[i][0].ToString());
                //    }
                //}

                dt_Plant = objPlant.BL_Get_Plant_with_Desc();
                dt_AscPlant = objPlant.BL_getAsc_Desc();
                if (dt_Plant.Rows.Count > 0)
                {
                    for (int i = 0; i < dt_Plant.Rows.Count; i++)
                    {
                        cblTPlant.Items.Add(dt_Plant.Rows[i][0].ToString());

                        //cblDPlant.Items.Add(dt_Plant.Rows[i][0].ToString());
                    }
                }
                if (dt_AscPlant.Rows.Count > 0)
                {
                    for (int i = 0; i < dt_AscPlant.Rows.Count; i++)
                    {
                        //cbTAssociatePlant.Items.Add(dt_AscPlant.Rows[i][0].ToString());
                        // cbDAscPlant.Items.Add(dt_AscPlant.Rows[i][0].ToString());
                    }
                }

                clsStandards.fillCombobox(ddlUserid, objMaster.BL_GetUserID(), "USER_ID");

                grPlant1.DataSource = null;
                grPlant1.DataBind();
                pnlImport.Visible = true;

                grdExcelRight.DataSource = null;
                grdExcelRight.DataBind();
                Panel1.Visible = true;

                btnSave.Visible = false;
                btnCancel.Visible = false;
                btnSaveRightExcel.Visible = false;
                btnCancelRightExcel.Visible = false;
            }
            catch (Exception ex)
            {
                clsStandards.WriteLog(this, ex, clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 0, true);
            }
            finally
            {
                objPlant = null;
            }
            MultiView1.SetActiveView(View2);
            strFlg = "ADD";
        }
    }

    protected void imgDetails_Click(object sender, ImageClickEventArgs e)
    {
        BL_UserMaster objMaster = new BL_UserMaster();

        try
        {

            cboSearch.Items.Clear();
            cboSearch.Items.Add(new ListItem("Plant Code", "PLANTCODE"));
            cboSearch.Items.Add(new ListItem("Department", "DEPARTMENT"));
            cboSearch.Items.Add(new ListItem("User ID", "USER_ID"));
            cboSearch.Items.Add(new ListItem("Module", "MODULE"));
            cboSearch.Items.Add(new ListItem("User Type", "USER_TYPE"));
            clsStandards.populateGrid(objMaster.GetUsersDisplayData(clsStandards.WhereStatement_NoST(string.Empty, string.Empty, string.Empty)), GrUser, "RECNO");
            //clsStandards.populateGrid(objMaster.getUsers(clsStandards.WhereStatement(0, cboSearch.SelectedValue, cboCriteria.SelectedValue, txtSearch.Text.Trim())), GrUser, "RECNO");

            MultiView1.SetActiveView(View1);
        }
        catch (Exception ex)
        {
            clsStandards.WriteLog(this, ex, clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 0, true);
        }
        finally { objMaster = null; }
    }
    protected void imgCloseV1_Click(object sender, ImageClickEventArgs e)
    {
        MultiView1.SetActiveView(View2);
    }
    protected void imgCloseV2_Click(object sender, ImageClickEventArgs e)
    {

    }
    public void clear()
    {
        //BL_DepartmentMaster objDepartment = new BL_DepartmentMaster();
        txtUserId.Text = string.Empty;
        txtFirstName.Text = string.Empty;
        txtEmpAdd.Text = string.Empty;
        txtEmpId.Text = string.Empty;
        txtLastName.Text = string.Empty;
        txtUserId.Enabled = true;
        txtpwd.Text = string.Empty;
        try
        {
            //clsStandards.fillCombobox(ddDepartment, objDepartment.BL_GetDepartmentCode(), "DEPARTMENTCODE");
            ddPlantcode.SelectedValue = "Select";
            ddModule.SelectedValue = "Select";
            ddType.SelectedValue = "Select";
            //ddDepartment.SelectedValue = "Select";
        }
        catch (Exception ex)
        {
            clsStandards.WriteLog(this, ex, clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 0, true);
        }

    }

    public void Rightsclear()
    {
        grdUserDtls.DataSource = null;
        grdUserDtls.DataBind();
        txtTDate.Text = string.Empty;
        //txtDdate.Text = string.Empty;

        txtTplant.Text = string.Empty;
        //txtDPlant.Text = string.Empty;
        //txtTdepartment.Text = string.Empty;
        //txtDDepartment.Text = string.Empty;
        txtTRole.Text = string.Empty;
        //txtDRole.Text = string.Empty;

        try
        {

            ddlUserid.SelectedValue = "Select";
            cblTPlant.Items.Clear();
            //cblDPlant.Items.Clear();
            //cblTDept.Items.Clear();
            //cblDDept.Items.Clear();
            //cblTRole.Items.Clear();
            //cblDRole.Items.Clear();
        }
        catch (Exception ex)
        {
            clsStandards.WriteLog(this, ex, clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 0, true);
        }

    }


    public void SelectedDataClear()
    {
        txtTDate.Text = string.Empty;
        txtTRole.Text = string.Empty;
        //txtDdate.Text = string.Empty;
        txtTplant.Text = string.Empty;
        //txtDPlant.Text = string.Empty;
        //txtTdepartment.Text = string.Empty;
        //txtDDepartment.Text = string.Empty;
        //txtTRole.Text = string.Empty;
        //txtDRole.Text = string.Empty;
        cblTPlant.Items.Clear();
        // cblDPlant.Items.Clear();
        //cblTDept.Items.Clear();
        // cblDDept.Items.Clear();
        //cblTRole.Items.Clear();
        //cblDRole.Items.Clear();
    }
    protected void imgSave_Click(object sender, ImageClickEventArgs e)
    {

        PL_Username objField = new PL_Username();
        BL_UserMaster objMaster = new BL_UserMaster();
        try
        {
            //string data1 = ddDepartment.Text.ToString();
            //string[] splitData1 = data1.Split('-');

            //objField.strDepartment = splitData1[0].ToString();


            objField.strUserID = txtUserId.Text.Trim();
            objField.strUsername = txtFirstName.Text.Trim();
            objField.strFirstName = txtFirstName.Text.Trim();
            objField.strLastName = txtLastName.Text.Trim();
            objField.strEmpCode = txtEmpId.Text.Trim();
            objField.strEmailID = txtEmpAdd.Text.Trim();
            objField.strPlantCode = ddPlantcode.SelectedItem.ToString().Trim().Split('-').GetValue(0).ToString();
            objField.strDepartment = "";
            objField.strModule = ddModule.SelectedItem.ToString().Trim();
            objField.strUserType = ddType.SelectedItem.ToString().Trim();
            objField.strMethod = strFlg;
            objField.strPwd = txtpwd.Text.Trim();
            //  objField.intStatus = clsStandards.ActivationStatus(chkStatus);
            objField.intStatus = status();
            objField.strCreatedBy = clsStandards.current_Username();
            if (strFlg == "EDIT" && ViewState["REFNO"].ToString() != "")
            {
                objField.REFNO = Convert.ToInt32(ViewState["REFNO"].ToString());
            }
            else
            {
                objField.REFNO = 0;
            }
            //  objField.strUsername = clsStandards.current_Username();

            string strResult = objMaster.BL_Save_Users(objField);
            if (strResult.StartsWith("0"))
            {
                clsStandards.WriteLog(this, new Exception(strResult.Split('|').GetValue(1).ToString()), clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 2, true);
            }
            else if (strResult.StartsWith("1"))
            {
                clsStandards.WriteLog(this, new Exception(strResult.Split('|').GetValue(1).ToString()), clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 1, true);
            }
            else
            {
                clsStandards.WriteLog(this, new Exception("Problem in save."), clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 0, true);
            }
            try
            {
                clear();
                strFlg = "ADD";
                clsStandards.fillCombobox(ddlUserid, objMaster.BL_GetUserID(), "USER_ID");
            }
            catch (Exception ex)
            {
                clsStandards.WriteLog(this, ex, clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 0, true);
            }

        }
        catch (Exception ex)
        {
            clsStandards.WriteLog(this, ex, clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 0, true);
        }
        finally
        {
            objMaster = null;
            objField = null;
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        strFlg = "ADD";
        int pass = 0;
        int tcount = 0;
        //PL_DepartmentMaster objField = new PL_DepartmentMaster();
        //BL_DepartmentMaster objMaster = new BL_DepartmentMaster();
        PL_Username objField = new PL_Username();
        BL_UserMaster objMaster = new BL_UserMaster();
        string strResult = "";
        dtImport = (DataTable)ViewState["Import"];
        tcount = dtImport.Rows.Count;
        try
        {
            if (dtImport.Rows.Count > 0)
            {
                for (int i = 0; i < dtImport.Rows.Count; i++)
                {
                    objField.strUserID = clsStandards.filter(dtImport.Rows[i][0].ToString());
                    objField.strUsername = "";
                    objField.strFirstName = clsStandards.filter(dtImport.Rows[i][1].ToString());
                    objField.strLastName = clsStandards.filter(dtImport.Rows[i][2].ToString());
                    objField.strEmpCode = clsStandards.filter(dtImport.Rows[i][3].ToString());
                    objField.strEmailID = clsStandards.filter(dtImport.Rows[i][4].ToString());

                    objField.strPlantCode = clsStandards.filter(dtImport.Rows[i][5].ToString());

                    objField.strDepartment = clsStandards.filter(dtImport.Rows[i][6].ToString());

                    objField.strModule = clsStandards.filter(dtImport.Rows[i][7].ToString());

                    objField.strUserType = clsStandards.filter(dtImport.Rows[i][8].ToString());

                    objField.strMethod = strFlg;

                    objField.intStatus = 1;

                    objField.strCreatedBy = clsStandards.current_Username();

                    strResult = objMaster.BL_Save_Users(objField);
                    if (strResult.StartsWith("0") == true)
                    {
                        //clsStandards.WriteLog(this, new Exception(strResult.Split('|').GetValue(1).ToString()), clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 0, true);
                        pass++;
                    }
                    else
                    {

                        //grPlant1.Rows[i].BackColor = System.Drawing.Color.Red;
                        //grPlant1.Rows[i].ForeColor = System.Drawing.Color.Wheat;

                    }

                }

                clsStandards.WriteLog(this, new Exception(tcount + " Out Of " + pass + "Record Inserted"), clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 2, true);
                tcount = pass = 0;

                // clear();

            }

            grPlant1.DataSource = null;
            grPlant1.DataBind();
            pnlImport.Visible = false;
        }
        catch (Exception ex)
        {
            clsStandards.WriteLog(this, ex, clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 0, true);

        }
        finally
        {
            objField = null;
            objMaster = null; strResult = null;
            // ViewState["Import"] = null;
        }
    }


    protected void imgAdd1_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            clear();
            MultiView1.SetActiveView(View2);
            strFlg = "ADD";
        }
        catch (Exception ex)
        {
            clsStandards.WriteLog(this, ex, clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 0, true);
        }
    }
    protected void imgExport_Click(object sender, ImageClickEventArgs e)
    {
        BL_UserMaster objMaster = new BL_UserMaster();
        try
        {
            GrUser.AllowPaging = false;
            GrUser.AllowSorting = false;

            //clsxlsExport.ExportExcel(GrPlant, "Company.xls");
            // clsStandards.populateGrid(objMaster.GetUsersDisplayData(clsStandards.WhereStatement_NoST(string.Empty, string.Empty, string.Empty)), GrUser, "RECNO");
            clsxlsExport.ConvertToxls(objMaster.GetUsersDisplayData(clsStandards.WhereStatement_NoST(cboSearch.SelectedValue, cboCriteria.SelectedValue, txtSearch.Text.Trim())), "User_Master");
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
    protected void imgSearch_Click(object sender, ImageClickEventArgs e)
    {
        BL_UserMaster objItems = new BL_UserMaster();
        try
        {
            clsStandards.populateGrid(objItems.GetUsersDisplayData(clsStandards.WhereStatement(0, cboSearch.SelectedValue, cboCriteria.SelectedValue, txtSearch.Text.Trim())), GrUser, "RECNO");
        }
        catch (Exception ex)
        {
            clsStandards.WriteLog(this, ex, clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 0, true);
        }
        finally
        {
            objItems = null;
        }
        MultiView1.SetActiveView(View1);
    }


    protected void GrUser_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GrUser.PageIndex = e.NewPageIndex;
        BL_UserMaster objMaster = new BL_UserMaster();
        try
        {
            clsStandards.populateGrid(objMaster.GetUsersDisplayData(clsStandards.WhereStatement_NoST(cboSearch.SelectedValue, cboCriteria.SelectedValue, txtSearch.Text.Trim())), GrUser, "RECNO");
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



    protected void GrUser_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        DataTable dt = new DataTable();
        BL_UserMaster objMaster = new BL_UserMaster();
        PL_Username objCon = new PL_Username();
        BL_Chnage blChange = new BL_Chnage();

        if (e.CommandName == "Select")
        {
            int RowInd = ((GridViewRow)(((LinkButton)e.CommandSource).NamingContainer)).RowIndex;
            try
            {
                dt = objMaster.getUsers(clsStandards.WhereStatement_NoST("[RECNO]", "Equal to", GrUser.DataKeys[RowInd].Value.ToString()));
                if (dt.Rows.Count != 0)
                {
                    try
                    {
                        ddPlantcode.SelectedValue = blChange.Bl_get_strPlant_desc(dt.Rows[0]["PLANTCODE"].ToString());
                        ddModule.SelectedValue = dt.Rows[0]["MODULE"].ToString();
                        ddType.SelectedValue = dt.Rows[0]["USER_TYPE"].ToString();
                    }
                    catch
                    {
                    }
                    txtUserId.Text = dt.Rows[0]["USER_ID"].ToString();
                    txtFirstName.Text = dt.Rows[0]["FIRSTNAME"].ToString();
                    txtLastName.Text = dt.Rows[0]["LASTNAME"].ToString();
                    txtEmpId.Text = dt.Rows[0]["EMP_ID"].ToString();
                    txtEmpAdd.Text = dt.Rows[0]["EMAIL_ADDRESS"].ToString();
                    txtpwd.Text = dt.Rows[0]["PASS_WORD"].ToString();
                    //chkStatus.Checked = ((dt.Rows[0]["STATUS"].ToString() == "True") ? true : false);
                    RBactivate.Checked = ((dt.Rows[0]["APStatus"].ToString() == "Activate") ? true : false);
                    RBdeactivate.Checked = ((dt.Rows[0]["APStatus"].ToString() == "Deactivate") ? true : false);

                    ViewState["REFNO"] = dt.Rows[0]["RECNO"].ToString();
                    strFlg = "EDIT";
                    txtUserId.Enabled = false;
                    imgSave.Enabled = true;
                    MultiView1.SetActiveView(View2);

                }
                else
                {
                    clsStandards.WriteLog(this, new Exception("Problem fetching details for selected Other Cleaning."), clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 0, true);
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
        else if (e.CommandName == "Unlock")
        {
            try
            {

                int RowInd = ((GridViewRow)(((Button)e.CommandSource).NamingContainer)).RowIndex;
                clsBLCommon _BusinessLayer = new clsBLCommon();
                dt = objMaster.getUsers(clsStandards.WhereStatement_NoST("[RECNO]", "Equal to", GrUser.DataKeys[RowInd].Value.ToString()));
                if (dt.Rows.Count != 0)
                {
                    if (_BusinessLayer.UnLockUser(dt.Rows[0]["USER_ID"].ToString()) == true)
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), Guid.NewGuid().ToString(), "alert('" + (dt.Rows[0]["USER_ID"].ToString()) + " : User id has been unlocked.');", true);
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                clsStandards.WriteLog(this, ex, clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 0, true);
            }

        }
    }
    protected void GrUser_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[0].Text = "Page " + (GrUser.PageIndex + 1) + " of " + GrUser.PageCount;
            e.Row.Cells[1].Text = clsStandards.FooterInfo(GrUser);
        }
    }
    protected void GrUser_Sorting(object sender, GridViewSortEventArgs e)
    {
        BL_UserMaster objMaster = new BL_UserMaster();
        try
        {
            clsStandards.populateGrid(objMaster.GetUsersDisplayData(clsStandards.WhereStatement_NoST(cboSearch.SelectedValue, cboCriteria.SelectedValue, txtSearch.Text.Trim())), GrUser, e.SortExpression.ToString());
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

    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Verifies that the control is rendered */
    }

    protected void ddlUserid_SelectedIndexChanged1(object sender, EventArgs e)
    {
        #region "Comments"
        //BL_UserMaster objItems = new BL_UserMaster();
        //BL_PlantMaster objplant = new BL_PlantMaster();
        //BL_DepartmentMaster objDept = new BL_DepartmentMaster();
        //DataTable dt = new DataTable();

        //string[] strTransRole, strDispRole;

        //try
        //{

        //    SelectedDataClear();

        //    clsStandards.FillMultiCheckList(cblTPlant, objplant.BL_Get_Plant_with_Desc(), "PLANTCODE");
        //    clsStandards.FillMultiCheckList(cblDPlant, objplant.BL_Get_Plant_with_Desc(), "PLANTCODE");

        //    //clsStandards.FillMultiCheckList(cblTDept, objDept.BL_GetDepartmentCode(), "DEPARTMENTCODE");
        //    //clsStandards.FillMultiCheckList(cblDDept, objDept.BL_GetDepartmentCode(), "DEPARTMENTCODE");

        //    clsStandards.populateGrid(objItems.getUsers(clsStandards.WhereStatementUserMaster(1, "USER_ID", "Equal to", ddlUserid.Text.Trim())), grdUserDtls, "USER_ID");
        //    dt = objItems.BL_GetExistingRoles(ddlUserid.Text.Trim());
        //    if (dt.Rows.Count > 0)
        //    {
        //        #region Transaction Rights

        //        txtTDate.Text = dt.Rows[0]["T_ACCESSUPTO"].ToString();
        //        txtTdepartment.Text = dt.Rows[0]["T_DEPARTMENT"].ToString();
        //        txtTplant.Text = dt.Rows[0]["T_PLANTID"].ToString();
        //        txtTRole.Text = dt.Rows[0]["T_ROLES"].ToString();

        //        strTransRole = dt.Rows[0]["T_ROLES"].ToString().Split(',');

        //        clsStandards.FillMultiCheckList(cblTDept, objDept.BL_GetDepartmentCodeforPlant(txtTplant.Text.Trim()), "DEPARTMENTCODE");
        //        for (int i = 0; i < cblTDept.Items.Count; i++)
        //        {
        //            if (dt.Rows[0]["T_DEPARTMENT"].ToString().Contains(cblTDept.Items[i].Text.Trim().Split('-').GetValue(0).ToString()))
        //            {
        //                cblTDept.Items[i].Selected = true;
        //            }
        //        }
        //        for (int i = 0; i < cblTPlant.Items.Count; i++)
        //        {
        //            if (dt.Rows[0]["T_PLANTID"].ToString().Contains(cblTPlant.Items[i].Text.Trim().Split('-').GetValue(0).ToString()))
        //            {
        //                cblTPlant.Items[i].Selected = true;
        //            }
        //        }

        //        //for (int i = 0; i < cblTRole.Items.Count; i++)
        //        //{
        //        //    if (dt.Rows[0]["T_ROLES"].ToString().ToUpper() == cblTRole.Items[i].Text.ToUpper())
        //        //    {
        //        //        cblTRole.Items[i].Selected = true;
        //        //    }
        //        //}

        //        for (int i = 0; i < strTransRole.Length; i++)
        //        {
        //            for (int j = 0; j < cblTRole.Items.Count; j++)
        //            {
        //                if (cblTRole.Items[j].Text.ToUpper() == strTransRole[i].ToUpper())
        //                {
        //                    cblTRole.Items[j].Selected = true;
        //                    break;
        //                }
        //            }
        //        }

        //        ViewState["EDIT"] = "EDIT";
        //        #endregion

        //        #region DisPlay Rights


        //        txtDdate.Text = dt.Rows[0]["D_ACCESSUPTO"].ToString();
        //        txtDDepartment.Text = dt.Rows[0]["D_DEPARTMENT"].ToString();
        //        txtDPlant.Text = dt.Rows[0]["D_PLANTID"].ToString();
        //        txtDRole.Text = dt.Rows[0]["D_ROLES"].ToString();

        //        strDispRole = dt.Rows[0]["D_ROLES"].ToString().Split(',');

        //        clsStandards.FillMultiCheckList(cblDDept, objDept.BL_GetDepartmentCodeforPlant(txtDPlant.Text.Trim()), "DEPARTMENTCODE");
        //        for (int i = 0; i < cblDDept.Items.Count; i++)
        //        {
        //            if (dt.Rows[0]["D_DEPARTMENT"].ToString().Contains(cblDDept.Items[i].Text.Trim().Split('-').GetValue(0).ToString()))
        //            {
        //                cblDDept.Items[i].Selected = true;
        //            }
        //        }

        //        for (int i = 0; i < cblDPlant.Items.Count; i++)
        //        {
        //            if (dt.Rows[0]["D_PLANTID"].ToString().Contains(cblDPlant.Items[i].Text.Trim().Split('-').GetValue(0).ToString()))
        //            {
        //                cblDPlant.Items[i].Selected = true;
        //            }
        //        }

        //        //for (int i = 0; i < cblDRole.Items.Count; i++)
        //        //{
        //        //    if (dt.Rows[0]["D_ROLES"].ToString() == cblDRole.Items[i].Text)
        //        //    {
        //        //        cblDRole.Items[i].Selected = true;
        //        //    }
        //        //}

        //        for (int i = 0; i < strDispRole.Length; i++)
        //        {
        //            for (int j = 0; j < cblDRole.Items.Count; j++)
        //            {
        //                if (cblDRole.Items[j].Text.ToUpper() == strDispRole[i].ToUpper())
        //                {
        //                    cblDRole.Items[j].Selected = true;
        //                    break;
        //                }
        //            }
        //        }

        //        #endregion


        //    }

        //}
        //catch (Exception ex)
        //{
        //    clsStandards.WriteLog(this, ex, clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 0, true);
        //}
        //finally
        //{
        //    objItems = null;
        //}

        #endregion

        BL_UserMaster objItems = new BL_UserMaster();
        BL_PlantMaster objplant = new BL_PlantMaster();
        //BL_DepartmentMaster objDept = new BL_DepartmentMaster();
        DataTable dt = new DataTable();

        string[] strTransRole, strDispRole;

        try
        {

            SelectedDataClear();

            clsStandards.FillMultiCheckList(cblTPlant, objplant.BL_Get_Plant_with_Desc(), "PLANTCODE");
            //clsStandards.FillMultiCheckList(cblDPlant, objplant.BL_Get_Plant_with_Desc(), "PLANTCODE");

            //clsStandards.FillMultiCheckList(cblTDept, objDept.BL_GetDepartmentCode(), "DEPARTMENTCODE");
            //clsStandards.FillMultiCheckList(cblDDept, objDept.BL_GetDepartmentCode(), "DEPARTMENTCODE");

            clsStandards.populateGrid(objItems.getUsers(clsStandards.WhereStatementUserMaster(1, "USER_ID", "Equal to", ddlUserid.Text.Trim())), grdUserDtls, "USER_ID");
            dt = objItems.BL_GetExistingRoles(ddlUserid.Text.Trim());
            if (dt.Rows.Count > 0)
            {
                #region Transaction Rights

                txtTDate.Text = dt.Rows[0]["T_ACCESSUPTO"].ToString();
                //txtTdepartment.Text = dt.Rows[0]["T_DEPARTMENT"].ToString();
                txtTplant.Text = dt.Rows[0]["T_PLANTID"].ToString();
                txtTRole.Text = dt.Rows[0]["T_ROLES"].ToString();

                strTransRole = dt.Rows[0]["T_ROLES"].ToString().Split(',');

                //clsStandards.FillMultiCheckList(cblTDept, objDept.BL_GetDepartmentCodeforPlant(txtTplant.Text.Trim()), "DEPARTMENTCODE");
                //for (int i = 0; i < cblTDept.Items.Count; i++)
                //{
                //    if (dt.Rows[0]["T_DEPARTMENT"].ToString().Contains(cblTDept.Items[i].Text.Trim().Split('-').GetValue(0).ToString()))
                //    {
                //        cblTDept.Items[i].Selected = true;
                //    }
                //}
                for (int i = 0; i < cblTPlant.Items.Count; i++)
                {
                    if (dt.Rows[0]["T_PLANTID"].ToString().Contains(cblTPlant.Items[i].Text.Trim().Split('-').GetValue(0).ToString()))
                    {
                        cblTPlant.Items[i].Selected = true;
                    }
                }
                //for (int i = 0; i < cbTAssociatePlant.Items.Count; i++)
                //{
                //    if (dt.Rows[0]["T_PLANTID"].ToString().Contains(cbTAssociatePlant.Items[i].Text.Trim().Split('-').GetValue(0).ToString()))
                //    {
                //        cbTAssociatePlant.Items[i].Selected = true;
                //    }
                //}

                //for (int i = 0; i < cblTRole.Items.Count; i++)
                //{
                //    if (dt.Rows[0]["T_ROLES"].ToString().ToUpper() == cblTRole.Items[i].Text.ToUpper())
                //    {
                //        cblTRole.Items[i].Selected = true;
                //    }
                //}

                for (int i = 0; i < strTransRole.Length; i++)
                {
                    for (int j = 0; j < cblTRole.Items.Count; j++)
                    {
                        if (cblTRole.Items[j].Text.ToUpper() == strTransRole[i].ToUpper())
                        {
                            cblTRole.Items[j].Selected = true;
                            break;
                        }
                    }
                }

                ViewState["EDIT"] = "EDIT";
                #endregion

                #region DisPlay Rights


                //txtDdate.Text = dt.Rows[0]["D_ACCESSUPTO"].ToString();
                //txtDDepartment.Text = dt.Rows[0]["D_DEPARTMENT"].ToString();
                //txtDPlant.Text = dt.Rows[0]["D_PLANTID"].ToString();
                //txtDRole.Text = dt.Rows[0]["D_ROLES"].ToString();

                strDispRole = dt.Rows[0]["D_ROLES"].ToString().Split(',');

                //clsStandards.FillMultiCheckList(cblDDept, objDept.BL_GetDepartmentCodeforPlant(txtDPlant.Text.Trim()), "DEPARTMENTCODE");
                //for (int i = 0; i < cblDDept.Items.Count; i++)
                //{
                //    if (dt.Rows[0]["D_DEPARTMENT"].ToString().Contains(cblDDept.Items[i].Text.Trim().Split('-').GetValue(0).ToString()))
                //    {
                //        cblDDept.Items[i].Selected = true;
                //    }
                //}

                //for (int i = 0; i < cblDPlant.Items.Count; i++)
                //{
                //    if (dt.Rows[0]["D_PLANTID"].ToString().Contains(cblDPlant.Items[i].Text.Trim().Split('-').GetValue(0).ToString()))
                //    {
                //        cblDPlant.Items[i].Selected = true;
                //    }
                //}

                //for (int i = 0; i < cbDAscPlant.Items.Count; i++)
                //{
                //    if (dt.Rows[0]["D_PLANTID"].ToString().Contains(cbDAscPlant.Items[i].Text.Trim().Split('-').GetValue(0).ToString()))
                //    {
                //        cbDAscPlant.Items[i].Selected = true;
                //    }
                //}

                //for (int i = 0; i < cblDRole.Items.Count; i++)
                //{
                //    if (dt.Rows[0]["D_ROLES"].ToString() == cblDRole.Items[i].Text)
                //    {
                //        cblDRole.Items[i].Selected = true;
                //    }
                //}

                //for (int i = 0; i < strDispRole.Length; i++)
                //{
                //    for (int j = 0; j < cblDRole.Items.Count; j++)
                //    {
                //        if (cblDRole.Items[j].Text.ToUpper() == strDispRole[i].ToUpper())
                //        {
                //            cblDRole.Items[j].Selected = true;
                //            break;
                //        }
                //    }
                //}

                #endregion


            }

        }
        catch (Exception ex)
        {
            clsStandards.WriteLog(this, ex, clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 0, true);
        }
        finally
        {
            objItems = null;
        }
    }


    protected void btnTPlant_Cancel_Click(object sender, EventArgs e)
    {
        // txtTplant.Text = "";
        //clsStandards.ClearCheckbox(cblTPlant);
    }
    protected void btnTDeptOk_Click(object sender, EventArgs e)
    {
        // txtTdepartment.Text = clsStandards.GetSelectedItems_WODESC(cblTDept);

    }
    protected void btnTDept_Cancel_Click(object sender, EventArgs e)
    {
        //txtTdepartment.Text = "";
        //clsStandards.ClearCheckbox(cblTDept);
    }
    protected void btnTRoleOk_Click(object sender, EventArgs e)
    {
        txtTRole.Text = clsStandards.GetSelectedItems(cblTRole);

    }

    /// added by tejaswini

    protected void btnTPlantOk_Click(object sender, EventArgs e)
    {
        txtTplant.Text = clsStandards.GetSelectedItems(cblTPlant);

    }
    //ended by tejaswini
    protected void btnTRoleCancel_Click(object sender, EventArgs e)
    {
        //txtTRole.Text = "";
        //clsStandards.ClearCheckbox(cblTRole);
    }

    protected void btnDPlantOk_Click(object sender, EventArgs e)
    {
        //DataTable dtDispDept;
        //BL_DepartmentMaster objDepartment = new BL_DepartmentMaster();

        //txtDPlant.Text = clsStandards.GetSelectedItems_WODESC(cblDPlant);

        //try
        //{
        //    cblDDept.Items.Clear();
        //    dtDispDept = objDepartment.BL_GetDepartmentCodeforPlant(clsStandards.GetSelectedItems_WODESC(cblDPlant));
        //    if (dtDispDept.Rows.Count > 0)
        //    {
        //        for (int i = 0; i < dtDispDept.Rows.Count; i++)
        //        {
        //            cblDDept.Items.Add(dtDispDept.Rows[i][0].ToString());
        //        }
        //    }
        //}
        //catch (Exception ex)
        //{
        //    clsStandards.WriteLog(this, ex, clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 0, true);
        //}
        //finally
        //{
        //    objDepartment = null;
        //}

        DataTable dtDispDept;
        //BL_DepartmentMaster objDepartment = new BL_DepartmentMaster();
        string strDAscPlant = "";

        //strDAscPlant = clsStandards.GetSelectedItems_WODESC(cbDAscPlant);
        //if (strDAscPlant == "")
        //{
        //    txtDPlant.Text = clsStandards.GetSelectedItems_WODESC(cblDPlant);
        //}
        //else
        //{
        //    txtDPlant.Text = clsStandards.GetSelectedItems_WODESC(cblDPlant) + ',' + clsStandards.GetSelectedItems_WODESC(cbDAscPlant);
        //}


        try
        {
            // cblDDept.Items.Clear();
            // dtDispDept = objDepartment.BL_GetDepartmentCodeforPlant(clsStandards.GetSelectedItems_WODESC(cblDPlant));
            //if (dtDispDept.Rows.Count > 0)
            //{
            //    for (int i = 0; i < dtDispDept.Rows.Count; i++)
            //    {
            //      //  cblDDept.Items.Add(dtDispDept.Rows[i][0].ToString());
            //    }
            //}
        }
        catch (Exception ex)
        {
            clsStandards.WriteLog(this, ex, clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 0, true);
        }
        finally
        {
            // objDepartment = null;
        }


    }
    protected void btnDPlant_Cancel_Click(object sender, EventArgs e)
    {
        // txtDPlant.Text = "";
        // clsStandards.ClearCheckbox(cblDPlant);
    }
    protected void btnDDeptOk_Click(object sender, EventArgs e)
    {
        // txtDDepartment.Text = clsStandards.GetSelectedItems_WODESC(cblDDept);

    }
    protected void btnDDept_Cancel_Click(object sender, EventArgs e)
    {
        //txtDDepartment.Text = "";
        //clsStandards.ClearCheckbox(cblDDept);
    }
    protected void btnDRoleOk_Click(object sender, EventArgs e)
    {
        //txtDRole.Text = clsStandards.GetSelectedItems(cblDRole);

    }
    protected void btnDRoleCancel_Click(object sender, EventArgs e)
    {
        //txtDRole.Text = "";
        //clsStandards.ClearCheckbox(cblDRole);
    }
    protected void btnSaveRight_Click(object sender, EventArgs e)
    {

    }
    protected void btnSaveRight_Click1(object sender, EventArgs e)
    {
        PL_UserRight objField = new PL_UserRight();
        BL_UserMaster objMaster = new BL_UserMaster();
        strFlg = "ADD";
        try
        {


            if (txtTplant.Text == string.Empty)
            {
                //rv_TransPlant.Validate();
                //rv_DipPlant.Validate();
                clsStandards.WriteLog(this, new Exception("Select Plant"), clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 2, true);
                return;
            }
            //else
            //{
            //    rv_TransPlant.Dispose();
            //    rv_DipPlant.Dispose();
            //}


            if (txtTRole.Text == string.Empty)
            {
                clsStandards.WriteLog(this, new Exception("Select Role"), clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 2, true);
                //rvTransRole.Validate();
                //rv_DispRole.Validate();
                return;
            }
            //else
            //{
            //    rvTransRole.Dispose();
            //    rv_DispRole.Dispose();
            //}


            //if (txtTdepartment.Text == string.Empty && txtDDepartment.Text == string.Empty)
            //{

            //    clsStandards.WriteLog(this, new Exception("Select Department"), clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 2, true);
            //    //rv_TransDept.Validate();
            //    //rv_DispDept.Validate();
            //    return;
            //}
            //else
            //{
            //    rv_TransDept.Dispose();
            //    rv_DispDept.Dispose();
            //}



            //if (txtTDate.Text == string.Empty && txtDdate.Text == string.Empty)
            //{
            //    clsStandards.WriteLog(this, new Exception("Select Access Upto date"), clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 2, true);
            //    //rvTransDate.Validate();
            //    //rv_DispDate.Validate();
            //    return;
            //}
            //else
            //{
            //    rvTransDate.Dispose();
            //    rv_DispDate.Dispose();
            //}


            objField.strUserID = ddlUserid.Text.Trim();

            objField.strT_PlantID = txtTplant.Text.Trim().Split('-').GetValue(0).ToString();
            objField.strT_Department = "";// txtTdepartment.Text.Trim();
            objField.strT_Roles = txtTRole.Text.Trim();
            objField.DT_T_ACCESSUPTO = txtTDate.Text.Trim();
            objField.strD_PlantID = "";//txtDPlant.Text.Trim();
            objField.strD_Department = "";// txtDDepartment.Text.Trim();
            objField.strD_Roles = "";// txtDRole.Text.Trim();
            objField.DT_D_ACCESSUPTO = "";
            if (ViewState["EDIT"] != null)
            {
                objField.strMethod = ViewState["EDIT"].ToString();
            }
            else
            {
                objField.strMethod = strFlg;
            }

            objField.intStatus = 1;
            objField.strCreatedBy = clsStandards.current_Username();
            if (strFlg == "EDIT" && ViewState["REFNO"].ToString() != "")
            {
                objField.REFNO = Convert.ToInt32(ViewState["REFNO"].ToString());
            }
            else
            {
                objField.REFNO = 0;
            }
            //  objField.strUsername = clsStandards.current_Username();

            string strResult = objMaster.BL_Save_UserRights(objField);
            if (strResult.StartsWith("0"))
            {
                clsStandards.WriteLog(this, new Exception(strResult.Split('|').GetValue(1).ToString()), clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 2, true);
            }
            else if (strResult.StartsWith("1"))
            {
                clsStandards.WriteLog(this, new Exception(strResult.Split('|').GetValue(1).ToString()), clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 1, true);
            }
            else
            {
                clsStandards.WriteLog(this, new Exception("Problem in save."), clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 0, true);
            }
            try
            {
                Rightsclear();
                strFlg = "ADD";
            }
            catch (Exception ex)
            {
                clsStandards.WriteLog(this, ex, clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 0, true);
            }

        }
        catch (Exception ex)
        {
            clsStandards.WriteLog(this, ex, clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 0, true);
        }
        finally
        {
            objMaster = null;
            objField = null;
        }

    }

    //protected void ImageButton1_Click1(object sender, ImageClickEventArgs e)
    //{
    //    BL_UserMaster objMaster = new BL_UserMaster();
    //    DataTable dtUserCHK;
    //    try
    //    {


    //        if (txtUserId.Text.Trim() == string.Empty)
    //        {
    //            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), Guid.NewGuid().ToString(), @"alert('User id is required.');", true);
    //            return;
    //        }

    //        dtUserCHK = objMaster.BL_CheckUserID(txtUserId.Text.Trim());
    //        if (dtUserCHK.Rows.Count > 0)
    //        {
    //            clsStandards.WriteLog(this, new Exception("User Id already exists"), clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 1, true);
    //            return;
    //        }
    //        ActiveDirectoryHelper adh = new ActiveDirectoryHelper();
    //        ADUserDetail ad = adh.GetUserByLoginName(txtUserId.Text);
    //        txtFirstName.Text = ad.FirstName.ToString();
    //        txtLastName.Text = ad.LastName.ToString();
    //        txtEmpId.Text = "";
    //        txtEmpAdd.Text = ad.EmailAddress.ToString();
    //        //ddDepartment.Items.Clear();
    //        //ddDepartment.Items.Add(ad.Department.ToString());
    //        //ddDepartment.SelectedIndex = -1;
    //    }
    //    catch (Exception ex)
    //    {
    //        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), Guid.NewGuid().ToString(), @"alert('No Data Found');", true);
    //        clear();
    //    }
    //    finally
    //    {
    //        dtUserCHK = null;
    //        objMaster = null;
    //    }
    //}

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        grPlant1.DataSource = null;
        grPlant1.DataBind();
        dtImport = null;
        ViewState["Import"] = null;
        pnlImport.Visible = false;
    }
    protected void grPlant1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            dtImport = ViewState["Import"] == null ? new DataTable() : (DataTable)ViewState["Import"];
            grPlant1.PageIndex = e.NewPageIndex;
            grPlant1.DataSource = dtImport;
            grPlant1.DataBind();
        }
        catch (Exception ex)
        {
            clsStandards.WriteLog(this, ex, clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 0, true);
        }
        finally
        {
        }
    }
    protected void grPlant1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[0].Text = "Page " + (grPlant1.PageIndex + 1) + " of " + grPlant1.PageCount;
            e.Row.Cells[1].Text = clsStandards.FooterInfo(grPlant1);
        }
    }
    protected void btnImp_Click1(object sender, EventArgs e)
    {
        btnCancel.Visible = true;
        btnSave.Visible = true;
        grPlant1.Visible = true;

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
                                    grPlant1.DataSource = dtImport;
                                    grPlant1.DataBind();
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
            clsStandards.WriteLog(this, ex, clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 0, true);
        }
        finally
        {

        }
    }
    protected void btnImpRight_Click(object sender, EventArgs e)
    {
        btnCancelRight.Visible = true;
        btnSaveRight.Visible = true;
        grdExcelRight.Visible = true;
        btnSaveRightExcel.Visible = true;
        btnCancelRightExcel.Visible = true;

        try
        {
            string SheetName;
            if (uploadRight.HasFile)
            {
                string excelPath = Server.MapPath(ConfigurationManager.AppSettings["Data"].ToString()) + Path.GetFileName(uploadRight.PostedFile.FileName);
                uploadRight.SaveAs(excelPath);

                string conString = string.Empty;
                string extension = Path.GetExtension(uploadRight.PostedFile.FileName);
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
                                    grdExcelRight.DataSource = dtImport;
                                    grdExcelRight.DataBind();
                                    ViewState["Import"] = dtImport;
                                    Panel1.Visible = true;
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
            clsStandards.WriteLog(this, ex, clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 0, true);
        }
        finally
        {

        }
    }
    protected void btnSaveRightExcel_Click(object sender, EventArgs e)
    {
        strFlg = "ADD";
        int PASSCOUNT = 0;


        //PL_DepartmentMaster objField = new PL_DepartmentMaster();
        //BL_DepartmentMaster objMaster = new BL_DepartmentMaster();
        PL_UserRight objField = new PL_UserRight();
        BL_UserMaster objMaster = new BL_UserMaster();
        string strResult = "";
        dtImport = (DataTable)ViewState["Import"];
        int TOTCOUNT = dtImport.Rows.Count;
        try
        {
            if (dtImport.Rows.Count > 0)
            {
                for (int i = 0; i < dtImport.Rows.Count; i++)
                {
                    objField.strUserID = clsStandards.filter(dtImport.Rows[i][0].ToString());

                    objField.strT_PlantID = clsStandards.filter(dtImport.Rows[i][1].ToString());

                    objField.strT_Department = clsStandards.filter(dtImport.Rows[i][2].ToString());

                    objField.strT_Roles = "";

                    objField.DT_T_ACCESSUPTO = clsStandards.filter(dtImport.Rows[i][3].ToString());


                    objField.strD_PlantID = clsStandards.filter(dtImport.Rows[i][4].ToString());

                    objField.strD_Department = clsStandards.filter(dtImport.Rows[i][5].ToString());

                    objField.strD_Roles = "";

                    objField.DT_D_ACCESSUPTO = clsStandards.filter(dtImport.Rows[i][6].ToString());

                    objField.strMethod = strFlg;
                    objField.intStatus = 1;
                    objField.strCreatedBy = clsStandards.current_Username();

                    //strResult = objMaster.BL_InsertDepartment(objField);
                    strResult = objMaster.BL_Save_UserRights(objField);
                    if (strResult.StartsWith("0") == true)
                    {
                        // clsStandards.WriteLog(this, new Exception(strResult.Split('|').GetValue(1).ToString()), clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 0, true);
                        PASSCOUNT++;

                    }
                    else
                    {
                        //grdExcelRight.Rows[i].BackColor = System.Drawing.Color.Red;
                        //grdExcelRight.Rows[i].ForeColor = System.Drawing.Color.Wheat;
                    }

                }
                clsStandards.WriteLog(this, new Exception(PASSCOUNT + " Out Of " + TOTCOUNT + "Record Inserted"), clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 2, true);
                PASSCOUNT = 0;
                TOTCOUNT = 0;
                clear();


            }

            grdExcelRight.DataSource = null;
            grdExcelRight.DataBind();
            Panel1.Visible = false;
        }
        catch (Exception ex)
        {
            clsStandards.WriteLog(this, ex, clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 0, true);

        }
        finally
        {
            objField = null;
            objMaster = null; strResult = null;
            // ViewState["Import"] = null;
        }
    }
    protected void btnCancelRightExcel_Click(object sender, EventArgs e)
    {
        grdExcelRight.DataSource = null;
        grdExcelRight.DataBind();
        dtImport = null;
        ViewState["Import"] = null;
        Panel1.Visible = false;
    }
    protected void grdExcelRight_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            dtImport = ViewState["Import"] == null ? new DataTable() : (DataTable)ViewState["Import"];
            grdExcelRight.PageIndex = e.NewPageIndex;
            grdExcelRight.DataSource = dtImport;
            grdExcelRight.DataBind();
        }
        catch (Exception ex)
        {
            clsStandards.WriteLog(this, ex, clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 0, true);
        }
        finally
        {
        }
    }
    protected void grdExcelRight_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[0].Text = "Page " + (grdExcelRight.PageIndex + 1) + " of " + grdExcelRight.PageCount;
            e.Row.Cells[1].Text = clsStandards.FooterInfo(grdExcelRight);
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

    protected void btnTDepartment_Click(object sender, EventArgs e)
    {
        DataTable dtTranDept;
        //BL_DepartmentMaster objDepartment = new BL_DepartmentMaster();
        try
        {
            //dtTranDept = objDepartment.BL_GetDepartmentCodeforPlant(txtDPlant.Text.Trim());
            //if (dtTranDept.Rows.Count > 0)
            //{
            //    for (int i = 0; i < dtTranDept.Rows.Count; i++)
            //    {
            //        cblTDept.Items.Add(dtTranDept.Rows[i][0].ToString());
            //        //cblDDept.Items.Add(dt.Rows[i][0].ToString());
            //    }
            //}
        }
        catch (Exception ex)
        {
            clsStandards.WriteLog(this, ex, clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 0, true);
        }
        finally
        {
            // objDepartment = null;
        }

    }

    protected void btnCancelRight_Click(object sender, EventArgs e)
    {
        Rightsclear();


    }

    protected void btnTemplateUserRight_Click(object sender, EventArgs e)
    {

        try
        {
            Response.ContentType = "application/ms-excel";
            Response.AddHeader("content-disposition", "inline; filename=UserRights.xlsx");
            Response.TransmitFile(Server.MapPath("~/PrnFile/UserRights.xlsx"));
            Response.Flush();

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected void btnTemplate1_Click(object sender, EventArgs e)
    {
        try
        {
            Response.ContentType = "application/ms-excel";
            Response.AddHeader("content-disposition", "inline; filename=User_Master.xlsx");
            Response.TransmitFile(Server.MapPath("~/PrnFile/User_Master.xlsx"));
            Response.Flush();

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

}