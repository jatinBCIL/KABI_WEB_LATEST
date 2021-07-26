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

public partial class frmBooth_Master : System.Web.UI.Page
{

    public static string strFlg = "";
    DataTable dtImport = new DataTable();

    protected void Page_Load(object sender, EventArgs e)
    {
        ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);
        scriptManager.RegisterPostBackControl(this.btnTemplate);
        if (!IsPostBack)
        {
            ViewState["EDIT"] = null;
            BL_PlantMaster objPlant = new BL_PlantMaster();

            BL_UserMaster objMaster = new BL_UserMaster();
            DataTable dt = new DataTable();
            DataTable dt_Plant = new DataTable();
            DataTable dt_AscPlant = new DataTable();
            DataSet ds = new DataSet();
            try
            {
                clsStandards.fillCombobox(ddPlantcode, objPlant.BL_Get_Plant_with_Desc(), "PLANTCODE");

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
            grPlant1.DataSource = null;
            grPlant1.DataBind();
            pnlImport.Visible = true;
            btnSave.Visible = false;
            btnCancel.Visible = false;
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
    protected void btnTemplate_Click(object sender, EventArgs e)
    {
        try
        {
            //Response.ContentType = "application/ms-excel";
            //Response.AddHeader("content-disposition", "inline; filename=BoothMaster.xlsx");
            //Response.TransmitFile(Server.MapPath("~/PrnFile/BoothMaster.xlsx"));
            //Response.Flush();

            Response.ContentType = "application/ms-excel";
            Response.AddHeader("content-disposition", "inline; filename=BoothMaster.xlsx");
            Response.TransmitFile(Server.MapPath("~/PrnFile/BoothMaster.xlsx"));
            Response.Flush();
        }
        catch (Exception)
        {
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
    protected void imgDetails_Click(object sender, ImageClickEventArgs e)
    {
        BL_BoothMaster objMaster = new BL_BoothMaster();
        try
        {

            cboSearch.Items.Clear();
            cboSearch.Items.Add(new ListItem("Booth Code", "BOOTHCODE"));
            cboSearch.Items.Add(new ListItem("Booth Desc", "BOOTHDESC"));
            cboSearch.Items.Add(new ListItem("Storage Location", "STORAGELOC"));
            clsStandards.populateGrid(objMaster.GetBoothDisplayData(clsStandards.WhereStatement_NoST(string.Empty, string.Empty, string.Empty)), GrUser, "REFNO");
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
    protected void imgSave_Click(object sender, ImageClickEventArgs e)
    {

        PL_BoothMaster objField = new PL_BoothMaster();
        BL_BoothMaster objMaster = new BL_BoothMaster();
        try
        {
            objField.strBoothCode = txtBoothCode.Text.Trim();
            objField.strBoothDesc = txtBoothDesc.Text.Trim();
            objField.strStorageLocation = txtStorageLocation.Text.Trim();
            objField.strPlantCode = ddPlantcode.SelectedItem.ToString().Trim().Split('-').GetValue(0).ToString();

            objField.strMODE = strFlg;
            objField.intStatus = status();
            objField.strCreatedBy = clsStandards.current_Username();
            if (strFlg == "EDIT" && ViewState["REFNO"].ToString() != "")
            {
                if (txtRemark.Text == "" || txtRemark.Text == null)
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
            string strResult = objMaster.BL_Save_BoothMaster(objField);
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
        PL_BoothMaster objField = new PL_BoothMaster();
        BL_BoothMaster objMaster = new BL_BoothMaster();
        string strResult = "";
        dtImport = (DataTable)ViewState["Import"];
        tcount = dtImport.Rows.Count;
        try
        {
            if (dtImport.Rows.Count > 0)
            {
                for (int i = 0; i < dtImport.Rows.Count; i++)
                {

                    objField.strBoothCode = clsStandards.filter(dtImport.Rows[i][0].ToString()); ;
                    objField.strBoothDesc = clsStandards.filter(dtImport.Rows[i][1].ToString());
                    objField.strPlantCode = clsStandards.filter(dtImport.Rows[i][2].ToString());
                    objField.strStorageLocation = clsStandards.filter(dtImport.Rows[i][3].ToString()); ;


                    objField.strMODE = strFlg;

                    //  objField.intStatus = clsStandards.ActivationStatus(chkStatus);
                    objField.intStatus = Convert.ToInt32(clsStandards.filter(dtImport.Rows[i][4].ToString()));
                    objField.strCreatedBy = clsStandards.current_Username();
                    if (strFlg == "EDIT" && ViewState["REFNO"].ToString() != "")
                    {
                        //if (txtRemark.Text == "" || txtRemark.Text == null)
                        //{
                        //    clsStandards.WriteLog(this, new Exception("Remark Field Empty"), clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 0, true);
                        //    return;
                        //    //ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "alert('Remark Field Empty');", true);
                        //    //return;
                        //}
                        objField.REFNO = Convert.ToInt32(ViewState["REFNO"].ToString());
                    }
                    else
                    {
                        objField.REFNO = 0;
                    }
                    //  objField.strUsername = clsStandards.current_Username();
                    objField.strRemark = clsStandards.filter(dtImport.Rows[i][5].ToString());
                    strResult = objMaster.BL_Save_BoothMaster(objField);
                    if (strResult.StartsWith("0") == true)
                    {
                        //clsStandards.WriteLog(this, new Exception(strResult.Split('|').GetValue(1).ToString()), clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 0, true);
                        grPlant1.Rows[i].BackColor = System.Drawing.Color.Red;
                        pass++;
                    }
                    else
                    {

                        //grPlant1.Rows[i].BackColor = System.Drawing.Color.Red;
                        //grPlant1.Rows[i].ForeColor = System.Drawing.Color.Wheat;

                    }

                }

                clsStandards.WriteLog(this, new Exception(tcount + " Out Of " + pass + " Record Inserted "), clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 2, true);
                tcount = pass = 0;

                // clear();

            }


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
        BL_BoothMaster objMaster = new BL_BoothMaster();
        try
        {
            GrUser.AllowPaging = false;
            GrUser.AllowSorting = false;

            //clsxlsExport.ExportExcel(GrPlant, "Company.xls");
            // clsStandards.populateGrid(objMaster.GetUsersDisplayData(clsStandards.WhereStatement_NoST(string.Empty, string.Empty, string.Empty)), GrUser, "RECNO");
            clsxlsExport.ConvertToxls(objMaster.GetBoothDisplayData(clsStandards.WhereStatement_NoST(cboSearch.SelectedValue, cboCriteria.SelectedValue, txtSearch.Text.Trim())), "BoothMaster");

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
        BL_BoothMaster objItems = new BL_BoothMaster();
        try
        {
            clsStandards.populateGrid(objItems.GetBoothDisplayData(clsStandards.WhereStatement(0, cboSearch.SelectedValue, cboCriteria.SelectedValue, txtSearch.Text.Trim())), GrUser, "REFNO");
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
        BL_BoothMaster objMaster = new BL_BoothMaster();
        try
        {
            clsStandards.populateGrid(objMaster.GetBoothDisplayData(clsStandards.WhereStatement_NoST(cboSearch.SelectedValue, cboCriteria.SelectedValue, txtSearch.Text.Trim())), GrUser, "REFNO");
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
        BL_BoothMaster objDepartment = new BL_BoothMaster();
        if (e.CommandName == "Select")
        {
            int RowInd = ((GridViewRow)(((LinkButton)e.CommandSource).NamingContainer)).RowIndex;
            DataTable dt = new DataTable();
            BL_BoothMaster objMaster = new BL_BoothMaster();
            PL_BoothMaster objCon = new PL_BoothMaster();
            BL_Chnage blChange = new BL_Chnage();
            try
            {
                dt = objMaster.GetBoothDisplayData(clsStandards.WhereStatement_NoST("[REFNO]", "Equal to", GrUser.DataKeys[RowInd].Value.ToString()));
                if (dt.Rows.Count != 0)
                {
                    try
                    {
                        ddPlantcode.SelectedValue = blChange.Bl_get_strPlant_desc(dt.Rows[0]["PLANTID"].ToString());
                    }
                    catch
                    {
                    }
                    txtBoothCode.Text = dt.Rows[0]["BOOTHCODE"].ToString();
                    txtBoothDesc.Text = dt.Rows[0]["BOOTHDESC"].ToString();
                    txtStorageLocation.Text = dt.Rows[0]["STORAGELOC"].ToString();
                    //added by tejaswini s
                    RBactivate.Checked = ((dt.Rows[0]["Status"].ToString() == "1") ? true : false);
                    RBdeactivate.Checked = ((dt.Rows[0]["Status"].ToString() == "0") ? true : false);
                    txtRemark.Text = dt.Rows[0]["REMARK"].ToString();
                    ///endede by tejaswini
                    //RBactivate.Checked = ((dt.Rows[0]["Status"].ToString() == "Activate") ? true : false);
                    //RBdeactivate.Checked = ((dt.Rows[0]["Status"].ToString() == "Deactivate") ? true : false);

                    ViewState["REFNO"] = dt.Rows[0]["REFNO"].ToString();
                    strFlg = "EDIT";
                    txtBoothCode.Enabled = false;
                    imgSave.Enabled = true;
                    VisibleRemarkTrue();
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
        txtBoothCode.Enabled = true;
        txtBoothCode.Text = string.Empty;
        txtBoothDesc.Text = string.Empty;
        txtStorageLocation.Text = string.Empty;
        txtRemark.Text = string.Empty;
        try
        {
            //clsStandards.fillCombobox(ddDepartment, objDepartment.BL_GetDepartmentCode(), "DEPARTMENTCODE");
            ddPlantcode.SelectedValue = "Select";


            //ddDepartment.SelectedValue = "Select";
        }
        catch (Exception ex)
        {
            clsStandards.WriteLog(this, ex, clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 0, true);
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
        BL_BoothMaster objMaster = new BL_BoothMaster();
        try
        {
            clsStandards.populateGrid(objMaster.GetBoothDisplayData(clsStandards.WhereStatement_NoST(cboSearch.SelectedValue, cboCriteria.SelectedValue, txtSearch.Text.Trim())), GrUser, e.SortExpression.ToString());
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
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        grPlant1.DataSource = null;
        grPlant1.DataBind();
        dtImport = null;
        ViewState["Import"] = null;
        pnlImport.Visible = false;
    }
       
}