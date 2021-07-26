using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLayer;
using PropertyLayer;
using System.Data;
using System.Configuration;
using System.IO;
using System.Data.OleDb;

public partial class Masters_frmWeigningMaster : System.Web.UI.Page
{
    public static string strFlg = "";
    DataTable dtImport = new DataTable();

    protected void Page_Load(object sender, EventArgs e)
    {
        ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);
        scriptManager.RegisterPostBackControl(this.imgExport);
        scriptManager.RegisterPostBackControl(this.btnTemplate);
        BL_PlantMaster objPlant = new BL_PlantMaster();
        BL_BoothMaster objBooth = new BL_BoothMaster();
        BL_DepartmentMaster objDepartment = new BL_DepartmentMaster();
        //BL_CubicleMaster objCubicle = new BL_CubicleMaster();  nik
        if (!IsPostBack)
        {
            pnlImport.Visible = false;
            strFlg = "ADD";
            ViewState["REFNO"] = "";
            MultiView1.SetActiveView(View2);
            clsStandards.fillCombobox(ddPlantcode, objPlant.BL_Get_Plant_with_Desc(), "PLANTCODE");
            clsStandards.fillCombobox(ddlBooth, objBooth.BL_GetBoothCode(clsStandards.current_Plant().Trim().ToString()), "BOOTHCODE");
            clsStandards.fillCombobox(ddDepartment, objDepartment.BL_GetDepartmentCode(), "DEPARTMENTCODE");
            //clsStandards.fillCombobox(ddlCubicleCode, objCubicle.BL_GetcubicleCode(clsStandards.current_Plant().Trim().ToString()), "CUBICLECODE");   nik
            get6PointWeight();
            getCornerCalibration();
            getReproducibility();
            getDaily();
        }

    }

    private void get6PointWeight()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("Point");
        dt.Columns.Add("STANDARDWEIGHT");
        dt.Columns.Add("RangeFrom");
        dt.Columns.Add("RangeTo");

        for (int i = 0; i < 4; i++)
        {
            DataRow dr = dt.NewRow();
            dr["Point"] = "Weight (" + (i + 1).ToString() + " )";
            dt.Rows.Add(dr);
            dt.AcceptChanges();
        }
        GridView1.DataSource = dt;
        GridView1.DataBind();

    }
    private void getCornerCalibration()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("Point");
        dt.Columns.Add("STANDARDWEIGHT");
        //dt.Columns.Add("RangeTo");

        for (int i = 0; i < 1; i++)
        {
            DataRow dr = dt.NewRow();
            if (i == 0)
            {
                dr["Point"] = "Corner (A)";
            }
            //if (i == 1)
            //{
            //    dr["Point"] = "Corner (B)";
            //}
            //if (i == 2)
            //{
            //    dr["Point"] = "Corner (C)";
            //}
            //if (i == 3)
            //{
            //    dr["Point"] = "Corner (D)";
            //}
            //if (i == 4)
            //{
            //    dr["Point"] = "Corner (E)";
            //}
            dt.Rows.Add(dr);
            dt.AcceptChanges();
        }
        GridView2.DataSource = dt;
        GridView2.DataBind();

    }

    private void getReproducibility()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("Point");
        dt.Columns.Add("STANDARDWEIGHT");
        //dt.Columns.Add("Intermediate_Range");
        //dt.Columns.Add("RangeTo");

        for (int i = 0; i < 2; i++)
        {
            DataRow dr = dt.NewRow();
            dr["Point"] = "Weight (" + (i + 1).ToString() + " )";
            dt.Rows.Add(dr);
            dt.AcceptChanges();
        }
        
        GridView3.DataSource = dt;
        GridView3.DataBind();

    }

    private void getDaily()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("Point");
        dt.Columns.Add("RangeFrom");
        dt.Columns.Add("RangeTo");

        for (int i = 0; i < 2; i++)
        {
            DataRow dr = dt.NewRow();
            if (i == 0)
            {
                dr["Point"] = "Min (" + (i + 1).ToString() + " )";
            }
            //if (i == 1)
            //{
            //    dr["Point"] = "Intermediate (" + (i + 1).ToString() + " )";
            //}
            if (i == 1)
            {
                dr["Point"] = "Max (" + (i + 1).ToString() + " )";
            }
            dt.Rows.Add(dr);
            dt.AcceptChanges();
        }
        GridView4.DataSource = dt;
        GridView4.DataBind();

    }

   

    protected void imgCloseV1_Click(object sender, ImageClickEventArgs e)
    {
        MultiView1.SetActiveView(View1);
    }
    protected void imgDetails_Click(object sender, ImageClickEventArgs e)
    {
        BL_WeighingMaster objMaster = new BL_WeighingMaster();
        try
        {

            cboSearch.Items.Clear();
            cboSearch.Items.Add(new ListItem("Location ID", "LOCID"));
            cboSearch.Items.Add(new ListItem("Location Name", "LOCNM"));
            cboSearch.Items.Add(new ListItem("Warehouse Type", "WH_TYPE"));
            clsStandards.populateGrid(objMaster.BL_GetWeighingDisplayData(clsStandards.WhereStatement_NoST(string.Empty, string.Empty, string.Empty)), GrPlant, "REFNO");
            //clsStandards.populateGrid(objMaster.getUsers(clsStandards.WhereStatement(0, cboSearch.SelectedValue, cboCriteria.SelectedValue, txtSearch.Text.Trim())), GrUser, "RECNO");

            //MultiView1.SetActiveView(View2);
        }
        catch (Exception ex)
        {
            clsStandards.WriteLog(this, ex, clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 0, true);
        }
        finally { objMaster = null; }
        MultiView1.SetActiveView(View1);
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
    protected void imgSave_Click(object sender, ImageClickEventArgs e)
    {
        CheckRangeValue();
        PL_WeighingMaster objField = new PL_WeighingMaster();
        BL_WeighingMaster objMaster = new BL_WeighingMaster();
        try
        {
      
            objField.strPlantCode = ddPlantcode.SelectedItem.ToString().Trim().Split('-').GetValue(0).ToString();
            objField.strWeighingCode = txtWeighingCode.Text.Trim();
            objField.strWeighingIP = txtWeighingIP.Text.Trim();
            objField.strPort = txtWeighingPort.Text.Trim();
            objField.DScaleCapacity = Convert.ToDecimal(txtWeighingCapacity.Text.Trim());
            objField.ILeastCount = Convert.ToDecimal(txtLeastCount.Text.Trim());
            objField.DMinValue = Convert.ToDecimal(txtMinValue.Text.Trim());
            objField.DMaxValue = Convert.ToDecimal(txtMaxValue.Text.Trim());
            objField.IDueDays = Convert.ToInt32(txtDueDays.Text.Trim());
            objField.ITolaranceDays = Convert.ToInt32(txtTolarance.Text.Trim());
            objField.strMode = strFlg;
            objField.strUom = ddlUom.Text.Trim();
            objField.strWeightboxId = txtWeightboxId.Text;
            objField.strCertificateNo = txtCertificateNo.Text;
            objField.strDepartment = ddDepartment.Text.Trim().Split('-').GetValue(0).ToString();
            objField.IStatus = status();
            objField.strUser = clsStandards.current_Username();
            if (strFlg == "EDIT" && ViewState["REFNO"].ToString() != "")
            {
                if (string.IsNullOrEmpty(txtRemark.Text.Trim()))
                {
                    clsStandards.WriteLog(this, new Exception("Remark Field Empty"), clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 0, true);
                    return;
                }
                objField.REFNO = Convert.ToInt32(ViewState["REFNO"].ToString());
            }
            else
            {
                objField.REFNO = 0;
            }
            //  objField.strUsername = clsStandards.current_Username();
            objField.strRemark = txtRemark.Text.Trim();
            objField.strSerialNo = txtSerialNo.Text.Trim();
            objField.strWorkingRange = txtWorkingRange.Text;
            string strResult = objMaster.BL_InsertWeighingMaster(objField);
            if (strResult.StartsWith("0"))
            {
                SaveCalibration();
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
                RangeClear();
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

    private void CheckRangeValue()
    {
        string strStdWt = "";
        string strRangeFrom = "";
        string strRangeTo = "";
        decimal dchkStdWt = 0;
        decimal dchkRangeFrom = 0;
        decimal dchkRangeTo = 0;
        foreach (GridViewRow gr in GridView1.Rows)
        {
            strStdWt = ((TextBox)gr.FindControl("txtstdwt")).Text.Trim();
            strRangeFrom = ((TextBox)gr.FindControl("txtRangeFrom")).Text.Trim();
            strRangeTo = ((TextBox)gr.FindControl("txtRangeTo")).Text.Trim();
            dchkStdWt = Convert.ToDecimal(strStdWt == "" ? "0" : strStdWt);
            dchkRangeFrom = Convert.ToDecimal(strRangeFrom == "" ? "0" : strRangeFrom);
            dchkRangeTo = Convert.ToDecimal(strRangeTo == "" ? "0" : strRangeTo);

            if (dchkStdWt == 0)
            {

                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "alert('Please enter Standard Weight');", true);
                return;
            }
            else if (dchkStdWt == 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "alert('Please enter from range');", true);
                return;
            }
            else if (dchkStdWt == 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "alert('Please enter to range');", true);
                return;
            }
        }
        foreach (GridViewRow gr in GridView2.Rows)
        {
            strStdWt = ((TextBox)gr.FindControl("txtstdwt")).Text.Trim();
            dchkStdWt = Convert.ToDecimal(strStdWt == "" ? "0" : strStdWt);
            if (dchkStdWt == 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "alert('Please enter Standard Weight');", true);
                return;
            }
        }

        foreach (GridViewRow gr in GridView3.Rows)
        {
            strStdWt = ((TextBox)gr.FindControl("txtstdwt")).Text.Trim();
            dchkStdWt = Convert.ToDecimal(strStdWt == "" ? "0" : strStdWt);
            if (dchkStdWt == 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "alert('Please enter Standard Weight');", true);

            }
        }
        foreach (GridViewRow gr in GridView4.Rows)
        {
            strRangeFrom = ((TextBox)gr.FindControl("txtRangeFrom")).Text.Trim();
            strRangeTo = ((TextBox)gr.FindControl("txtRangeTo")).Text.Trim();
            dchkRangeFrom = Convert.ToDecimal(strRangeFrom == "" ? "0" : strRangeFrom);
            dchkRangeTo = Convert.ToDecimal(strRangeTo == "" ? "0" : strRangeTo);
            if (dchkStdWt == 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "alert('Please enter from range');", true);
                return;

            }
            else if (dchkStdWt == 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "alert('Please enter to range');", true);
                return;

            }

        }
    }

    private void SaveCalibration()
    {
        PL_WeighingMaster objField = new PL_WeighingMaster();
        BL_WeighingMaster objMaster = new BL_WeighingMaster();


        objField.strPlantCode = ddPlantcode.SelectedItem.ToString().Trim().Split('-').GetValue(0).ToString();
        objField.strWeighingCode = txtWeighingCode.Text.Trim();
        objField.strMode = strFlg;
        objField.IStatus = status();
        objField.strUser = clsStandards.current_Username();
        objField.strRemark = txtRemark.Text.Trim();

        foreach (GridViewRow gr in GridView1.Rows)
        {

            string strType = ((Label)gr.FindControl("lblPoint")).Text.Trim();
            string strStdWt = ((TextBox)gr.FindControl("txtstdwt")).Text.Trim();
            string strRangeFrom = ((TextBox)gr.FindControl("txtRangeFrom")).Text.Trim();
            string strRangeTo = ((TextBox)gr.FindControl("txtRangeTo")).Text.Trim();
            objField.strWeighingMode = "4 Point Calibration";
            objField.strWeightType = strType;
            objField.DRangeFrom = Convert.ToDecimal(strRangeFrom == "" ? "0" : strRangeFrom);
            objField.DRangeTo = Convert.ToDecimal(strRangeTo == "" ? "0" : strRangeTo);
            objField.DStandwardWeight = Convert.ToDecimal(strStdWt == "" ? "0" : strStdWt);
            string strResult = objMaster.BL_InsertWeighingMaster_Range(objField);
        }
        foreach (GridViewRow gr in GridView2.Rows)
        {

            string strType = ((Label)gr.FindControl("lblPoint")).Text.Trim();
            string strStdWt = ((TextBox)gr.FindControl("txtstdwt")).Text.Trim();
            
            objField.strWeighingMode = "Corner Calibration";
            objField.strWeightType = strType;
            
            objField.DStandwardWeight = Convert.ToDecimal(strStdWt == "" ? "0" : strStdWt);
            objField.DRangeFrom = 0;
            objField.DRangeTo = 0;
            string strResult = objMaster.BL_InsertWeighingMaster_Range(objField);
        }

        foreach (GridViewRow gr in GridView3.Rows)
        {

            string strType = ((Label)gr.FindControl("lblPoint")).Text.Trim();
            string strStdWt = ((TextBox)gr.FindControl("txtstdwt")).Text.Trim();
        
            objField.strWeighingMode = "ReProducibility";
            objField.strWeightType = strType;

            objField.DRangeFrom = 0;
            objField.DRangeTo = 0;
            objField.DStandwardWeight = Convert.ToDecimal(strStdWt == "" ? "0" : strStdWt);
            string strResult = objMaster.BL_InsertWeighingMaster_Range(objField);
        }
        foreach (GridViewRow gr in GridView4.Rows)
        {

            string strType = ((Label)gr.FindControl("lblPoint")).Text.Trim();
            string strRangeFrom = ((TextBox)gr.FindControl("txtRangeFrom")).Text.Trim();
            string strRangeTo = ((TextBox)gr.FindControl("txtRangeTo")).Text.Trim();
            objField.strWeighingMode = "Daily";
            objField.strWeightType = strType;
            objField.DStandwardWeight = 0;
            objField.DRangeFrom = Convert.ToDecimal(strRangeFrom == "" ? "0" : strRangeFrom);
            objField.DRangeTo = Convert.ToDecimal(strRangeTo == "" ? "0" : strRangeTo);
            string strResult = objMaster.BL_InsertWeighingMaster_Range(objField);
        }

    }

    protected void imgSearch_Click(object sender, ImageClickEventArgs e)
    {
        BL_WeighingMaster objItems = new BL_WeighingMaster();
        try
        {
            clsStandards.populateGrid(objItems.BL_GetWeighingDisplayData(clsStandards.WhereStatement(0, cboSearch.SelectedValue, cboCriteria.SelectedValue, txtSearch.Text.Trim())), GrPlant, "REFNO");
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
    protected void GrPlant_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GrPlant.PageIndex = e.NewPageIndex;
        BL_WeighingMaster objMaster = new BL_WeighingMaster();
        try
        {
            clsStandards.populateGrid(objMaster.BL_GetWeighingDisplayData(clsStandards.WhereStatement_NoST(cboSearch.SelectedValue, cboCriteria.SelectedValue, txtSearch.Text.Trim())), GrPlant, "REFNO");
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

    public void clear()
    {
        //BL_DepartmentMaster objDepartment = new BL_DepartmentMaster();
        txtLeastCount.Enabled = true;
        txtMaxValue.Text = string.Empty;
        txtMinValue.Text = string.Empty;
        txtRemark.Text = string.Empty;
        txtWeighingCapacity.Text = string.Empty;
        txtWeighingCode.Text = string.Empty;
        txtWeighingIP.Text = string.Empty;
        txtWeighingPort.Text = string.Empty;
        txtWeightboxId.Text = string.Empty;
        txtCertificateNo.Text = string.Empty;
        //txtDepartment.Text = string.Empty;
        txtDueDays.Text = string.Empty;
        txtTolarance.Text = string.Empty;
        txtWorkingRange.Text = string.Empty;
        txtLeastCount.Text = string.Empty;
        txtSerialNo.Text = string.Empty;
        txtsopno.Text = string.Empty;

        try
        {
            //clsStandards.fillCombobox(ddDepartment, objDepartment.BL_GetDepartmentCode(), "DEPARTMENTCODE");
            ddPlantcode.SelectedValue = "Select";
            ddlBooth.SelectedValue = "Select";
            ddDepartment.SelectedValue = "Select";
        }
        catch (Exception ex)
        {
            clsStandards.WriteLog(this, ex, clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 0, true);
        }

    }
    protected void imgAdd1_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            VisibleRemarkFalse();
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
        BL_WeighingMaster objMaster = new BL_WeighingMaster();
        try
        {
            GrPlant.AllowPaging = false;
            GrPlant.AllowSorting = false;

            //clsxlsExport.ExportExcel(GrPlant, "Company.xls");
            // clsStandards.populateGrid(objMaster.GetUsersDisplayData(clsStandards.WhereStatement_NoST(string.Empty, string.Empty, string.Empty)), GrUser, "RECNO");
            clsxlsExport.ConvertToxls(objMaster.BL_GetWeighingDisplayData(clsStandards.WhereStatement_NoST(cboSearch.SelectedValue, cboCriteria.SelectedValue, txtSearch.Text.Trim())), "WeighingMaster");
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
    protected void GrPlant_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        BL_BinMaster objDepartment = new BL_BinMaster();
        if (e.CommandName == "Select")
        {
            int RowInd = ((GridViewRow)(((LinkButton)e.CommandSource).NamingContainer)).RowIndex;
            // GridViewRow gr = GrDept.Rows[Convert.ToInt32(e.CommandArgument)];
            DataTable dt = new DataTable();
            DataTable dt_Range = new DataTable();
            DataTable dt6Point = new DataTable();
            DataTable dtCorner = new DataTable();
            DataTable dtReproducibility = new DataTable();
            DataTable dtDaily = new DataTable();
            BL_WeighingMaster objMaster = new BL_WeighingMaster();
            PL_WeighingMaster objCon = new PL_WeighingMaster();
            BL_Chnage blChange = new BL_Chnage();
            try
            {
                dt = objMaster.BL_GetWeighingDisplayData(clsStandards.WhereStatement_NoST("[REFNO]", "Equal to", GrPlant.DataKeys[RowInd].Value.ToString()));
                dt_Range = objMaster.BL_GetWeighingRangeData(clsStandards.WhereStatement_NoST("[WeighingCode]", "Equal to", GrPlant.Rows[RowInd].Cells[1].Text.ToString()));
                if (dt.Rows.Count != 0)
                {
                    try
                    {
                        ddPlantcode.SelectedValue = blChange.Bl_get_strPlant_desc(dt.Rows[0]["PLANTCODE"].ToString());
                        ddDepartment.SelectedValue = blChange.Bl_get_strDept_desc(dt.Rows[0]["Department"].ToString());
                        //ddlBooth.SelectedValue = dt.Rows[0]["BOOTH"].ToString();
                        //ddModule.SelectedValue = dt.Rows[0]["MODULE"].ToString();
                        //ddType.SelectedValue = dt.Rows[0]["USER_TYPE"].ToString();
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }

                    try
                    {
                        ddlUom.SelectedValue = dt.Rows[0]["WEIGHINGUOM"].ToString();
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }

                    txtLeastCount.Text = dt.Rows[0]["LEASTCOUNT"].ToString();
                    txtMaxValue.Text = dt.Rows[0]["MAXVALUE"].ToString();
                    txtMinValue.Text = dt.Rows[0]["MINVALUE"].ToString();
                    txtRemark.Text = dt.Rows[0]["REMARK"].ToString();
                    txtWeighingCapacity.Text = dt.Rows[0]["WEIGHINGCAPACITY"].ToString();
                    txtWeighingCode.Text = dt.Rows[0]["WEIGHINGCODE"].ToString();
                    txtWeighingIP.Text = dt.Rows[0]["WEIGHINGIP"].ToString();
                    txtWeighingPort.Text = dt.Rows[0]["WEIGHINGPORT"].ToString();
                    txtCertificateNo.Text = dt.Rows[0]["CERTIFICATENO"].ToString();
                    txtsopno.Text = dt.Rows[0]["SopNo"].ToString();
                    txtSerialNo.Text = dt.Rows[0]["SerialNo"].ToString();
                    txtWorkingRange.Text = dt.Rows[0]["WorkingRange"].ToString();
                    txtWeightboxId.Text = dt.Rows[0]["WEIGHTBOXID"].ToString();
                    txtDueDays.Text = dt.Rows[0]["DUEDAYS"].ToString();
                    txtTolarance.Text = dt.Rows[0]["TOLARANCEDAYS"].ToString();
                    RBactivate.Checked = ((dt.Rows[0]["Status"].ToString() == "Activate") ? true : false);
                    RBdeactivate.Checked = ((dt.Rows[0]["Status"].ToString() == "Deactivate") ? true : false);

                    ViewState["REFNO"] = dt.Rows[0]["REFNO"].ToString();
                    strFlg = "EDIT";
                    imgSave.Enabled = true;
                    VisibleRemarkTrue();
                    MultiView1.SetActiveView(View2);


                    if (dt_Range.Rows.Count > 0)
                    {
                        dt6Point = dt_Range.Clone();
                        dtCorner = dt_Range.Clone();
                        dtReproducibility = dt_Range.Clone();
                        dtDaily = dt_Range.Clone();
                        dt6Point = dt_Range.Select("WEIGHINGMODE='4 Point Calibration'").CopyToDataTable();
                        dtCorner = dt_Range.Select("WEIGHINGMODE='Corner Calibration'").CopyToDataTable();
                        dtReproducibility = dt_Range.Select("WEIGHINGMODE='ReProducibility'").CopyToDataTable();
                        dtDaily = dt_Range.Select("WEIGHINGMODE='Daily'").CopyToDataTable();
                        GridView1.DataSource = dt6Point;
                        GridView1.DataBind();
                        GridView2.DataSource = dtCorner;
                        GridView2.DataBind();
                        GridView3.DataSource = dtReproducibility;
                        GridView3.DataBind();
                        GridView4.DataSource = dtDaily;
                        GridView4.DataBind();
                    }
                    else
                    {
                        RangeClear();
                    }
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
    }

    private void RangeClear()
    {
        foreach (GridViewRow gr in GridView1.Rows)
        {
            //TextBox t = (TextBox)gr.FindControl("txtQuan");
            TextBox strRangeFrom = (TextBox)gr.FindControl("txtRangeFrom");
            TextBox strRangeTo = (TextBox)gr.FindControl("txtRangeTo");
            TextBox strSdwt = (TextBox)gr.FindControl("txtstdwt");
            strRangeFrom.Text = "";
            strRangeTo.Text = "";
            strSdwt.Text = "";
            
        }
        foreach (GridViewRow gr in GridView2.Rows)
        {
         
            TextBox strSdwt = (TextBox)gr.FindControl("txtstdwt");
            strSdwt.Text = "";
        
            
        }
        foreach (GridViewRow gr in GridView3.Rows)
        {
           
            TextBox strSdwt = (TextBox)gr.FindControl("txtstdwt");
            strSdwt.Text = "";
        }
        foreach (GridViewRow gr in GridView4.Rows)
        {
            
            TextBox strRangeFrom = (TextBox)gr.FindControl("txtRangeFrom");
            TextBox strRangeTo = (TextBox)gr.FindControl("txtRangeTo");
            strRangeFrom.Text = "";
            strRangeTo.Text = "";
        }
    }

    //added by tejASWINI
    //Picking Plant Data From Excel and Populating in Grid
    protected void btnImp_Click(object sender, EventArgs e)
    {

        PL_PlantMastercs objPlant = new PL_PlantMastercs();
        BL_PlantMaster objBlPlant = new BL_PlantMaster();
        btnCancel.Visible = true;
        btnSave.Visible = true;
        grWeigning1.Visible = true;

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
                                    grWeigning1.DataSource = dtImport;
                                    grWeigning1.DataBind();
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



            }
            else
            {
                //ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "alert('File Format not supported');", true);
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "alert('Please Select File and Upload Again');", true);
                return;
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

    protected void btnTemplate_Click(object sender, EventArgs e)
    {
        try
        {
            Response.ContentType = "application/ms-excel";
            Response.AddHeader("content-disposition", "inline; filename=Weighing_Master.xlsx");
            Response.TransmitFile(Server.MapPath("~/PrnFile/Weighing_Master.xlsx"));
            Response.Flush();

        }
        catch (Exception)
        {
        }
    }


    //Populate data of seleced page index of grid grPlant1.
    protected void grWeigning_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            dtImport = ViewState["Import"] == null ? new DataTable() : (DataTable)ViewState["Import"];
            grWeigning1.PageIndex = e.NewPageIndex;
            grWeigning1.DataSource = dtImport;
            grWeigning1.DataBind();
        }
        catch (Exception ex)
        {
            clsStandards.WriteLog(this, ex, clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 0, true);
        }
        finally
        {
        }
    }

    //Close the grWeigning Master
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        grWeigning1.DataSource = null;
        grWeigning1.DataBind();
        dtImport = null;
        ViewState["Import"] = null;
        pnlImport.Visible = false;
    }

    //Bind the data to grid grWeigning with Page indexing
    protected void grWeigning_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[0].Text = "Page " + (grWeigning1.PageIndex + 1) + " of " + grWeigning1.PageCount;
            e.Row.Cells[1].Text = clsStandards.FooterInfo(grWeigning1);
        }

    }

    #region From Gridview
    //Inserting Plant Data which was imported from Excel.
    protected void btnSave_Click(object sender, EventArgs e)
    {
        int pass = 0;
        if (grWeigning1.Rows.Count > 0)
        {
            strFlg = "ADD";
            //PL_PlantMastercs obj = new PL_PlantMastercs();
            //BL_PlantMaster objPlant = new BL_PlantMaster();
            PL_WeighingMaster objField = new PL_WeighingMaster();
            BL_WeighingMaster objMaster = new BL_WeighingMaster();
            for (int i = 0; i < grWeigning1.Rows.Count; i++)
            {
                try
                {
                    string strResult = "";

                    objField.strWeighingCode = clsStandards.filter(grWeigning1.Rows[i].Cells[0].Text);//LOCATION
                    objField.strPlantCode = clsStandards.filter(grWeigning1.Rows[i].Cells[1].Text);//PLANT CODE
                    objField.strWeighingIP = clsStandards.filter(grWeigning1.Rows[i].Cells[2].Text);//PLANT DESC
                    objField.strPort = clsStandards.filter(grWeigning1.Rows[i].Cells[3].Text);//ASS PLANT
                    objField.DScaleCapacity = Convert.ToInt32(clsStandards.filter(grWeigning1.Rows[i].Cells[4].Text));//ASS PLANT DESC
                    objField.strUom = clsStandards.filter(grWeigning1.Rows[i].Cells[5].Text);//ASS PLANT DESC LEASTCOUNT
                    objField.ILeastCount = Convert.ToDecimal(clsStandards.filter(grWeigning1.Rows[i].Cells[6].Text));//ADD 1
                    objField.DMinValue = Convert.ToDecimal(clsStandards.filter(grWeigning1.Rows[i].Cells[7].Text));//ADD 2
                    objField.DMaxValue = Convert.ToDecimal(clsStandards.filter(grWeigning1.Rows[i].Cells[8].Text));//ADD 3
                    objField.strWeightboxId = clsStandards.filter(grWeigning1.Rows[i].Cells[9].Text);//ADD 3
                    objField.strCertificateNo = clsStandards.filter(grWeigning1.Rows[i].Cells[10].Text);//ADD 3
                    objField.IDueDays = Convert.ToInt32(clsStandards.filter(grWeigning1.Rows[i].Cells[11].Text));//ADD 3
                    objField.ITolaranceDays = Convert.ToInt32(clsStandards.filter(grWeigning1.Rows[i].Cells[12].Text));//ADD 3
                    objField.strDepartment = clsStandards.filter(grWeigning1.Rows[i].Cells[13].Text);//ADD 4
                    objField.strWorkingRange = clsStandards.filter(grWeigning1.Rows[i].Cells[14].Text);//ADD 4
                    objField.strRemark = clsStandards.filter(grWeigning1.Rows[i].Cells[15].Text);//ADD 4

                    objField.IStatus = Convert.ToInt32(clsStandards.filter(grWeigning1.Rows[i].Cells[16].Text));//ADD 6
                    objField.strUser = clsStandards.current_Username();
                    objField.strMode = "ADD";


                    strResult = objMaster.BL_InsertWeighingMaster(objField);
                    if (strResult.StartsWith("0") == true)
                    {
                        pass = pass + 1;

                        //ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "alert('" + strResult + "');", true);
                        // return;
                    }
                    else
                    {
                        grWeigning1.Rows[i].BackColor = System.Drawing.Color.Red;
                    }


                }
                catch (Exception ex)
                {
                    pass = pass + 0;
                    //clsStandards.WriteLog(this, ex, clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 0, true);
                    //return;
                }



            }
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "alert('" + pass + " Out of " + grWeigning1.Rows.Count + " Records Saved Sucessfully');", true);
            pass = 0;
            return;
        }

    }
    #endregion
}