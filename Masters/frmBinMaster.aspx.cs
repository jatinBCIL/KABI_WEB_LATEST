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
using System.Net;
using System.Text;

public partial class frmBin_Master : System.Web.UI.Page
{

    public static string strFlg = "";
    DataTable dtImport = new DataTable();

    protected void Page_Load(object sender, EventArgs e)
    {
        

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
             
                grPlant1.DataSource = null;
                grPlant1.DataBind();
                pnlImport.Visible = true;
                btnSave.Visible = false;
                btnCancel.Visible = false;
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
            Response.ContentType = "application/ms-excel";
            Response.AddHeader("content-disposition", "inline; filename=BinMaster.xlsx");
            Response.TransmitFile(Server.MapPath("~/PrnFile/BinMaster.xlsx"));
            Response.Flush();

        }
        catch (Exception)
        {
        }
    }
    protected void imgDetails_Click(object sender, ImageClickEventArgs e)
    {
        BL_BinMaster objMaster = new BL_BinMaster();

        try
        {

            cboSearch.Items.Clear();
            cboSearch.Items.Add(new ListItem("Location ID", "LOCID"));
            cboSearch.Items.Add(new ListItem("Location Name", "LOCNM"));
            cboSearch.Items.Add(new ListItem("Warehouse Type", "WH_TYPE"));
            clsStandards.populateGrid(objMaster.GetBinDisplayData(clsStandards.WhereStatement_NoST(string.Empty, string.Empty, string.Empty)), GrUser, "REFNO");
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
    protected void imgSave_Click(object sender, ImageClickEventArgs e)
    {
        PL_BinMaster objField = new PL_BinMaster();
        BL_BinMaster objMaster = new BL_BinMaster();
        try
        {
            objField.strLocationID = txtbarcodeid.Text.Trim();
            objField.strLocationName = txtbindesc.Text.Trim();
            objField.strWarehouseType = ddWHType.SelectedItem.ToString().Trim();
            objField.strPlantCode = ddPlantcode.SelectedItem.ToString().Trim().Split('-').GetValue(0).ToString();
            objField.strStorgeLoc = txtstorgeLoc.Text.Trim();
            objField.strStorageType = txtstorageType.Text.Trim();
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
           
            objField.strRemark = txtRemark.Text.Trim();
            string strResult = objMaster.BL_Save_Bin(objField);
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
        PL_BinMaster objField = new PL_BinMaster();
        BL_BinMaster objMaster = new BL_BinMaster();
        string strResult = "";
        dtImport = (DataTable)ViewState["Import"];
        tcount = dtImport.Rows.Count;
        try
        {
            if (dtImport.Rows.Count > 0)
            {
                for (int i = 0; i < dtImport.Rows.Count; i++)
                {
                    objField.strLocationID = clsStandards.filter(grPlant1.Rows[i].Cells[0].Text);
                    objField.strLocationName = clsStandards.filter(grPlant1.Rows[i].Cells[1].Text);
                    objField.strPlantCode = clsStandards.filter(grPlant1.Rows[i].Cells[2].Text);
                    objField.strWarehouseType = clsStandards.filter(grPlant1.Rows[i].Cells[3].Text);

                    objField.strMODE = strFlg;
                    objField.strRemark = clsStandards.filter(grPlant1.Rows[i].Cells[5].Text);
                    //  objField.intStatus = clsStandards.ActivationStatus(chkStatus);
                    objField.intStatus = Convert.ToInt32(clsStandards.filter(grPlant1.Rows[i].Cells[6].Text));
                    objField.strCreatedBy = clsStandards.current_Username();

                    strResult = objMaster.BL_Save_Bin(objField);
                    if (strResult.StartsWith("0") == true)
                    {
                        //clsStandards.WriteLog(this, new Exception(strResult.Split('|').GetValue(1).ToString()), clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 0, true);
                        pass++;
                        grPlant1.Rows[i].BackColor = System.Drawing.Color.Red;
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
        BL_BinMaster objMaster = new BL_BinMaster();
        try
        {
            GrUser.AllowPaging = false;
            GrUser.AllowSorting = false;

            //clsxlsExport.ExportExcel(GrPlant, "Company.xls");
            // clsStandards.populateGrid(objMaster.GetUsersDisplayData(clsStandards.WhereStatement_NoST(string.Empty, string.Empty, string.Empty)), GrUser, "RECNO");
            clsxlsExport.ConvertToxls(objMaster.GetBinDisplayData(clsStandards.WhereStatement_NoST(cboSearch.SelectedValue, cboCriteria.SelectedValue, txtSearch.Text.Trim())), "User_Master");
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
        BL_BinMaster objItems = new BL_BinMaster();
        try
        {
            clsStandards.populateGrid(objItems.GetBinDisplayData(clsStandards.WhereStatement(0, cboSearch.SelectedValue, cboCriteria.SelectedValue, txtSearch.Text.Trim())), GrUser, "REFNO");
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
        BL_BinMaster objMaster = new BL_BinMaster();
        try
        {
            clsStandards.populateGrid(objMaster.GetBinDisplayData(clsStandards.WhereStatement_NoST(cboSearch.SelectedValue, cboCriteria.SelectedValue, txtSearch.Text.Trim())), GrUser, "REFNO");
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
        BL_BinMaster objDepartment = new BL_BinMaster();
        if (e.CommandName == "Select")
        {
            int RowInd = ((GridViewRow)(((LinkButton)e.CommandSource).NamingContainer)).RowIndex;
            DataTable dt = new DataTable();
            BL_BinMaster objMaster = new BL_BinMaster();
            PL_BinMaster objCon = new PL_BinMaster();
            BL_Chnage blChange = new BL_Chnage();
            try
            {
                dt = objMaster.GetBinDisplayData(clsStandards.WhereStatement_NoST("[REFNO]", "Equal to", GrUser.DataKeys[RowInd].Value.ToString()));
                if (dt.Rows.Count != 0)
                {
                    try
                    {
                        ddPlantcode.SelectedValue = blChange.Bl_get_strPlant_desc(dt.Rows[0]["PLANTID"].ToString());
                    }
                    catch
                    {

                    }
                    txtbarcodeid.Text = dt.Rows[0]["LOCID"].ToString();
                    txtbindesc.Text = dt.Rows[0]["LOCNM"].ToString();
                    ddWHType.Text = dt.Rows[0]["WH_TYPE"].ToString();
                    RBactivate.Checked = ((dt.Rows[0]["Status"].ToString() == "Activate") ? true : false);
                    RBdeactivate.Checked = ((dt.Rows[0]["Status"].ToString() == "Deactivate") ? true : false);

                    ViewState["REFNO"] = dt.Rows[0]["REFNO"].ToString();
                    strFlg = "EDIT";

                    txtbarcodeid.Enabled = false;
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
        txtbarcodeid.Enabled = true;
        txtbarcodeid.Text = string.Empty;
        txtbindesc.Text = string.Empty;
        txtRemark.Text = string.Empty;
        txtstorageType.Text = string.Empty;
        txtstorgeLoc.Text = string.Empty;
        try
        {
            //clsStandards.fillCombobox(ddDepartment, objDepartment.BL_GetDepartmentCode(), "DEPARTMENTCODE");
            ddPlantcode.SelectedValue = "Select";
            ddWHType.SelectedIndex = 0;

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
        BL_BinMaster objMaster = new BL_BinMaster();
        try
        {
            clsStandards.populateGrid(objMaster.GetBinDisplayData(clsStandards.WhereStatement_NoST(cboSearch.SelectedValue, cboCriteria.SelectedValue, txtSearch.Text.Trim())), GrUser, e.SortExpression.ToString());
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
    protected void btnGetSAPData_Click(object sender, EventArgs e)
    {
        try
        {

            WS_BinMaster.Z_WS_UPDATE_BIN _Bin = new WS_BinMaster.Z_WS_UPDATE_BIN();
            WS_BinMaster.ZDMF005 _Item = new WS_BinMaster.ZDMF005();
            WS_BinMaster.ZDMF005Response _Res = new WS_BinMaster.ZDMF005Response();
            WS_BinMaster.ZDBMS010[] _ITFinal = new WS_BinMaster.ZDBMS010[0];

            _Bin.Proxy = new WebProxy();


            _Bin.Credentials = new NetworkCredential(System.Configuration.ConfigurationManager.AppSettings["SapUser"].ToString(), System.Configuration.ConfigurationManager.AppSettings["SapPass"].ToString());
            _Bin.Proxy.Credentials = new NetworkCredential(System.Configuration.ConfigurationManager.AppSettings["SapUser"].ToString(), System.Configuration.ConfigurationManager.AppSettings["SapPass"].ToString());

            _Item.IM_BIN = txtbarcodeid.Text.Trim();
            _Item.IM_PLANT = clsStandards.current_Plant().ToString();


              var req = new MemoryStream();
              var reqxml = new System.Xml.Serialization.XmlSerializer(_Item.GetType());
              reqxml.Serialize(req, _Item);
              string strxml = Encoding.UTF8.GetString(req.ToArray());
              clsStandards.WriteSoapXML(strxml, "BinMaster - Request", clsStandards.GetCurrentPageName().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());

            _Res = _Bin.ZDMF005(_Item);
            _ITFinal = _Res.EX_BIN;

            var res = new MemoryStream();
            var resxml = new System.Xml.Serialization.XmlSerializer(_Res.GetType());
            resxml.Serialize(res, _Res);
            string xml = Encoding.UTF8.GetString(res.ToArray());
            clsStandards.WriteSoapXML(xml, "BinMaster - Response", clsStandards.GetCurrentPageName().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());

            ddWHType.Items.Insert(0, "Select");
            for (int i = 0; i < _ITFinal.Length; i++)
            {
                txtbindesc.Text = _ITFinal[i].BIN_DESCRIPTION;
                txtarea.Text = _ITFinal[i].PACKING_AREA;
                ddWHType.Items.Insert((i+1), _ITFinal[i].WH_NO);
                txtstorgeLoc.Text = _ITFinal[i].STORAGE_LOCATIOMN;
                txtstorageType.Text = _ITFinal[i].STORAGE_TYPE;
            }
        }
        catch (Exception ex)
        {
            clsStandards.WriteLog(this, new Exception(ex.Message), clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(),0, false);
        }
    }
}