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

public partial class frmDepartmentMaster : System.Web.UI.Page
{

    public static string strFlg = "";
    DataTable dtImport = new DataTable();

    //On Page Load, Fill Plant Code ComboBox
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);
            scriptManager.RegisterPostBackControl(this.btnTemplate);
            VisibleRemarkFalse();
            BL_DepartmentMaster objDept = new BL_DepartmentMaster();
            BL_PlantMaster objPlant = new BL_PlantMaster();

            try
            {


                clsStandards.FillMultiCheckList(chkPlants, objPlant.BL_Get_Plant(clsStandards.current_Plant().ToString().Trim()), "PlantCode");

                // clsStandards.fillCombobox(DD_Plantcode, objPlant.BL_Get_Plant(), "PlantCode");

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
                objDept = null;
            }
            MultiView1.SetActiveView(View2);
            strFlg = "ADD";
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

    //Populate Department Master Data in Grid
    protected void imgDetails_Click(object sender, ImageClickEventArgs e)
    {
        BL_DepartmentMaster objMaster = new BL_DepartmentMaster();
        try
        {
            clsStandards.populateGrid(objMaster.BL_Get_Department_Data(clsStandards.WhereStatement_NoST(string.Empty, string.Empty, string.Empty), clsStandards.current_Plant()), GrDept, "Refno");
            MultiView1.SetActiveView(View1);
        }
        catch (Exception ex)
        {
            clsStandards.WriteLog(this, ex, clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 0, true);
        }
        finally { objMaster = null; }
    }

    //Close the Detail View and Populates the Entry View of Department Master
    protected void imgCloseV1_Click(object sender, ImageClickEventArgs e)
    {
        MultiView1.SetActiveView(View2);
    }


    protected void imgCloseV2_Click(object sender, ImageClickEventArgs e)
    {

    }

    //Clear All Controls 
    public void clear()
    {

        txtDeptName.Text = string.Empty;
        try
        {
            txtPlant.Enabled = true;
            txtDeptName.Enabled = true;
            txtDept_ID.Text = string.Empty;
            clsStandards.ClearCheckbox(chkPlants);
            txtPlant.Text = string.Empty;
            pnlImport.Visible = false;


            //try { DD_Plantcode.SelectedValue = "Select"; }
            //catch { }
        }
        catch (Exception ex)
        {
            clsStandards.WriteLog(this, ex, clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 0, true);
        }
        RBactivate.Checked = true;
        RBdeactivate.Checked = false;

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


    //Inserting and Updating Department Master Data
    protected void imgSave_Click(object sender, ImageClickEventArgs e)
    {
        PL_DepartmentMaster objField = new PL_DepartmentMaster();
        BL_DepartmentMaster objMaster = new BL_DepartmentMaster();
        try
        {
            string[] strPlant = txtPlant.Text.Split('-');


            objField.strPlantCode = txtPlant.Text.Trim();
            objField.strDeptCode = txtDept_ID.Text.Trim();
            objField.strDeptDesc = txtDeptName.Text.Trim();
            objField.strMODE = strFlg;
            objField.intStatus = status();
            objField.strUsername = clsStandards.current_Username();
            if (strFlg == "EDIT" && ViewState["REFNO"].ToString() != "")
            {

                if (txtRemark.Text == "" || txtRemark.Text == null)
                {
                    clsStandards.WriteLog(this, new Exception("Remark Field Is Empty "), clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 2, true);
                    return;
                }
                objField.REFNO = Convert.ToInt32(ViewState["REFNO"].ToString());
            }
            else
            {
                objField.REFNO = 0;
            }
            objField.strRemark = txtRemark.Text.Trim();
            //  objField.strUsername = clsStandards.current_Username();

            string strResult = objMaster.BL_InsertDepartment(objField);
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
                //DD_Plantcode.Focus();
                //clsStandards.populateGrid(objMaster.GetConsigneeSelect(clsStandards.WhereStatement_NoST(string.Empty, string.Empty, string.Empty)), GrPlants, "BCIL_ID");
                //MultiView1.SetActiveView(View1);
            }
            catch (Exception ex)
            {
                clsStandards.WriteLog(this, ex, clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 0, true);
            }
            txtRemark.Text = "";
            VisibleRemarkFalse();
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

    //Import Data from excel and populate that in Grid grPlant1
    protected void btnImp_Click(object sender, EventArgs e)
    {

        PL_DepartmentMaster objPlant = new PL_DepartmentMaster();
        BL_DepartmentMaster objBlPlant = new BL_DepartmentMaster();
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
    protected void GrDept11_RowCommand(object sender, GridViewCommandEventArgs e)
    {

    }

    //Save the Data which was Imported from Excel
    protected void btnSave_Click(object sender, EventArgs e)
    {
        int pass = 0;

        if (grPlant1.Rows.Count > 0)
        {
            BL_DepartmentMaster objDept = new BL_DepartmentMaster();


            strFlg = "ADD";
            PL_DepartmentMaster objField = new PL_DepartmentMaster();
            BL_DepartmentMaster objMaster = new BL_DepartmentMaster();
            string strResult = "";
            for (int i = 0; i < grPlant1.Rows.Count; i++)
            {
                try
                {

                    if (clsStandards.filter(grPlant1.Rows[i].Cells[0].Text) == clsStandards.current_Plant())
                    {

                        string STRcODE = objDept.BL_Get_DepartmentCode();
                        objField.strPlantCode = clsStandards.filter(grPlant1.Rows[i].Cells[0].Text);
                        objField.strDeptCode = STRcODE.Trim();
                        objField.strDeptDesc = clsStandards.filter(grPlant1.Rows[i].Cells[1].Text);
                        objField.strMODE = strFlg;




                        objField.intStatus = Convert.ToInt32((grPlant1.Rows[i].Cells[2].Text));





                        objField.strUsername = clsStandards.current_Username();
                        objField.strRemark = "";
                        objField.REFNO = 0;


                        strResult = objMaster.BL_InsertDepartment(objField);
                        if (strResult.StartsWith("0") == true)
                        {
                            pass = pass + 1;
                            //clsStandards.WriteLog(this, new Exception(strResult.Split('|').GetValue(1).ToString()), clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 0, true);
                        }
                        else
                        {
                            grPlant1.Rows[i].BackColor = System.Drawing.Color.Red;
                        }
                    }
                    else
                    {
                        grPlant1.Rows[i].BackColor = System.Drawing.Color.Red;
                    }



                }

                catch (Exception ex)
                {
                    // clsStandards.WriteLog(this, ex, clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 0, true);
                    pass = pass + 0;
                }
            }
            clsStandards.WriteLog(this, new Exception(pass + " Out Of " + grPlant1.Rows.Count + " Records Save Sucessfully "), clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 0, true);
            pass = 0;
            strFlg = "ADD";

            return;

        }
    }

    //Clear the grid
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        grPlant1.DataSource = null;
        grPlant1.DataBind();
        dtImport = null;
        ViewState["Import"] = null;
        pnlImport.Visible = false;
    }

    //Navigating from Detailed view to Entry View.
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

    //Export Department Master Data in Excel
    protected void imgExport_Click(object sender, ImageClickEventArgs e)
    {
        BL_DepartmentMaster objMaster = new BL_DepartmentMaster();
        try
        {
            GrDept.AllowPaging = false;
            GrDept.AllowSorting = false;

            //clsxlsExport.ExportExcel(GrPlant, "Company.xls");

            clsxlsExport.ConvertToxls(objMaster.BL_Get_Department_Data(clsStandards.WhereStatement_NoST(cboSearch.SelectedValue, cboCriteria.SelectedValue, txtSearch.Text.Trim()), clsStandards.current_Plant()), "Department_Master");
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

    //Populate Data in Grid against the Serach Criteria.
    protected void imgSearch_Click(object sender, ImageClickEventArgs e)
    {
        BL_DepartmentMaster objItems = new BL_DepartmentMaster();
        try
        {
            clsStandards.populateGrid(objItems.BL_Get_Department_Data(clsStandards.WhereStatement_NoST(cboSearch.SelectedValue, cboCriteria.SelectedValue, txtSearch.Text.Trim()), clsStandards.current_Plant()), GrDept, "Refno");
            MultiView1.SetActiveView(View1);
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
    protected void GrDept_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

    }

    //Bind Data in Grid with Page indexing
    protected void GrDept_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[0].Text = "Page " + (GrDept.PageIndex + 1) + " of " + GrDept.PageCount;
            e.Row.Cells[1].Text = clsStandards.FooterInfo(GrDept);
        }
    }

    //Populate data of seleced page index of grid GrDept.
    protected void GrDept_PageIndexChanging1(object sender, GridViewPageEventArgs e)
    {
        GrDept.PageIndex = e.NewPageIndex;
        BL_DepartmentMaster objMaster = new BL_DepartmentMaster();
        try
        {
            clsStandards.populateGrid(objMaster.BL_Get_Department_Data(clsStandards.WhereStatement_NoST(string.Empty, string.Empty, string.Empty), clsStandards.current_Plant()), GrDept, "Refno");
            MultiView1.SetActiveView(View1);
        }
        catch (Exception ex)
        {
            clsStandards.WriteLog(this, ex, clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 0, true);
        }
        finally { objMaster = null; }
    }

    //Sort Data of Grid
    protected void GrDept_Sorting(object sender, GridViewSortEventArgs e)
    {
        BL_DepartmentMaster objMaster = new BL_DepartmentMaster();
        try
        {
            clsStandards.populateGrid(objMaster.BL_Get_Department_Data(clsStandards.WhereStatement_NoST(cboSearch.SelectedValue, cboCriteria.SelectedValue, txtSearch.Text.Trim()), clsStandards.current_Plant()), GrDept, e.SortExpression.ToString());
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

    //Fill the selected data into controls which was selected from Grid.
    protected void GrDept_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Select")
        {
            int RowInd = ((GridViewRow)(((LinkButton)e.CommandSource).NamingContainer)).RowIndex;
            // GridViewRow gr = GrDept.Rows[Convert.ToInt32(e.CommandArgument)];
            DataTable dt = new DataTable();
            BL_DepartmentMaster objMaster = new BL_DepartmentMaster();
            PL_DepartmentMaster objCon = new PL_DepartmentMaster();
            try
            {
                dt = objMaster.BL_Get_Department_Data(clsStandards.WhereStatement_NoST("[Refno]", "Equal to", GrDept.DataKeys[RowInd].Value.ToString()), clsStandards.current_Plant());
                if (dt.Rows.Count != 0)
                {
                    try
                    {
                        txtPlant.Text = dt.Rows[0]["PlantCode"].ToString();
                        //DD_Plantcode.SelectedValue = dt.Rows[0]["PlantCode"].ToString();
                    }
                    catch
                    {
                    }
                    txtDept_ID.Text = dt.Rows[0]["DepartmentCode"].ToString();
                    txtDeptName.Text = dt.Rows[0]["DepartmentDesc"].ToString();
                    txtRemark.Text = dt.Rows[0]["REMARK"].ToString();




                    RBactivate.Checked = ((dt.Rows[0]["Status"].ToString().Trim() == "ACTIVATE") ? true : false);
                    RBdeactivate.Checked = ((dt.Rows[0]["Status"].ToString().Trim() == "DEACTIVATE") ? true : false);

                    ViewState["REFNO"] = dt.Rows[0]["Refno"].ToString();
                    VisibleRemarkTrue();
                    MultiView1.SetActiveView(View2);
                    strFlg = "EDIT";
                    pnlImport.Visible = false;

                    txtPlant.Enabled = false;
                    txtDeptName.Enabled = false;
                }
                else
                {
                    clsStandards.WriteLog(this, new Exception("Problem fetching details for selected Department."), clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 0, true);
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

    //Populate data of seleced page index of grid grPlant1.
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

    //Bind the data to grid grPlant1 with Page indexing
    protected void grPlant1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[0].Text = "Page " + (grPlant1.PageIndex + 1) + " of " + grPlant1.PageCount;
            e.Row.Cells[1].Text = clsStandards.FooterInfo(grPlant1);
        }
    }




    //Generat New Department ID
    protected void chkPlants_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtPlant.Text = clsStandards.GetSelectedItemsSplitbydash(chkPlants);
        try
        {
            if (txtDept_ID.Text == string.Empty)
            {
                BL_DepartmentMaster objDept = new BL_DepartmentMaster();
                txtDept_ID.Text = objDept.BL_Get_DepartmentCode();

                objDept = null;
            }
        }
        catch (Exception ex)
        {
            clsStandards.WriteLog(this, ex, clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 0, true);
        }

    }


    //addded by tejaswini

    protected void btnTemplate_Click(object sender, EventArgs e)
    {
        try
        {
            //Response.ContentType = "application/ms-excel";
            //Response.AddHeader("content-disposition", "inline; filename=BoothMaster.xlsx");
            //Response.TransmitFile(Server.MapPath("~/PrnFile/BoothMaster.xlsx"));
            //Response.Flush();

            Response.ContentType = "application/ms-excel";
            Response.AddHeader("content-disposition", "inline; filename=DepartmentMaster.xlsx");
            Response.TransmitFile(Server.MapPath("~/PrnFile/DepartmentMaster.xlsx"));
            Response.Flush();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }



}