using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Configuration;
using System.Data.OleDb;
using System.Data;
using DataLayer;
using PropertyLayer;
using BusinessLayer;
using System.Text.RegularExpressions;

/// <summary>
/// Ref No : 
/// Purpose : Plant Related Transactions
/// Created By : 
/// Created On : 
/// Modified By : Jagdish Joshi
/// Modified On : 29 July 2016.
/// Comment : Presentation Layer functions of Plant Master Module
/// </summary>


public partial class Masters_frmPlantMasters : System.Web.UI.Page
{
    public static string strFlag = "";

    DataTable dtImport = new DataTable();

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


    //Binding Existing Data of Plant to Grid
    protected void Page_Load(object sender, EventArgs e)
    {
        ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);
        scriptManager.RegisterPostBackControl(this.btnTemplate);
        //VisibleRemarkFalse();
        if (!IsPostBack)
        {
            BL_PlantMaster objPlant = new BL_PlantMaster();
            try
            {

                clsStandards.populateGrid(objPlant.BL_GetPlantDtl_Master(clsStandards.WhereStatement_NoST(cboSearch.SelectedValue, cboCriteria.SelectedValue, txtSearch.Text.Trim())), GrPlant, "PLANTCODE");
                MultiView1.SetActiveView(View2);
                //grAscPlant.DataSource = null;
                //grAscPlant.DataBind();
                grPlant1.DataSource = null;
                grPlant1.DataBind();
                pnlImport.Visible = false;
                strFlag = "ADD";
                // ChkActivate.Checked = true;
                string plantype = Plant_type();

                if (Plant_type() == "API")
                {
                    //lblHoldDays.Visible = true;
                    //txtHoldDays.Visible = true;
                    //txtHoldDays.Text = "";
                    ddlPlantType.SelectedIndex = 1;
                    //ddlPlantType.Items.Clear();
                    //ddlPlantType.Items.Add(new ListItem("Select"));
                    //ddlPlantType.Items.Add(new ListItem("API"));
                }
                else if (Plant_type() == "FORMULATION")
                {
                    //ddlPlantType.Items.Clear();
                    //ddlPlantType.Items.Add(new ListItem("Select"));
                    //ddlPlantType.Items.Add(new ListItem("FROMULATION"));
                    //add1
                    ddlPlantType.SelectedIndex = 2;
                    //lblAddress.Visible = false;
                    //txtAddress1.Visible = false;
                    //txtAddress1.Text = "";

                    ////add2
                    //lbladdress2.Visible = false;
                    //txtaddress2.Visible = false;
                    //txtaddress2.Text = "";

                    ////add3
                    //lbladdress3.Visible = false;
                    //txtaddress3.Visible = false;
                    //txtaddress3.Text = "";

                    ////add4
                    //lbladdress4.Visible = false;
                    //txtaddress4.Visible = false;
                    //txtaddress4.Text = "";

                    ////state
                    //lblstate.Visible = false;
                    //txtstate.Visible = false;
                    //txtstate.Text = "";

                    ////city
                    //lblcity.Visible = false;
                    //txtcity.Visible = false;
                    //txtcity.Text = "";

                    ////pincode
                    //lblpincode.Visible = false;
                    //txtpincode.Visible = false;
                    //txtpincode.Text = "";

                }


            }
            catch (Exception ex)
            {
                clsStandards.WriteLog(this, ex, clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 0, true);
            }
        }
    }
    //check status
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



    //Inserting and Updateing Data of Plant
    public void InsertPlantMaster()
    {


        string strResult = "";
        try
        {
            PL_PlantMastercs obj = new PL_PlantMastercs();
            BL_PlantMaster objPlant = new BL_PlantMaster();



            obj.strLoc = txtLoc.Text.Trim();
            obj.strPlantCode = txtPlantCode.Text.Trim();
            obj.strPlantDesc = txtPlantDesc.Text.Trim();

            /// address modification

            obj.strAddress1 = txtAddress1.Text.Trim();
            obj.strAddress2 = txtaddress2.Text.Trim();
            obj.strAddress3 = txtaddress3.Text.Trim();
            obj.strAddress4 = txtaddress4.Text.Trim();
            obj.strState = txtstate.Text.Trim();
            obj.strCity = txtcity.Text.Trim();
            obj.strPincode = txtpincode.Text.Trim();

            //


            /////////////
            if (Plant_type() == "API")
            {


                if (txtAddress1.Text == null || txtAddress1.Text == "")
                {

                    clsStandards.WriteLog(this, new Exception("Address Field Empty"), clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 0, true);
                    return;
                }

                //if (txtHoldDays.Text.Trim() == "" || txtHoldDays.Text == "")
                //{

                //    clsStandards.WriteLog(this, new Exception("Hold Days Empty"), clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 0, true);
                //    return;
                //}
            }
            else if (Plant_type() == "FORMULATION")
            {
                //lblAddress.Visible = false;
                //txtAddress1.Visible = false;
                //txtAddress1.Text = "";
                //if (txtHoldDays.Text == null || txtHoldDays.Text == "")
                //{
                //    clsStandards.WriteLog(this, new Exception(" Hold Days Not Present"), clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 0, true);
                //    return;
                //}
                //if (!Regex.IsMatch(txtHoldDays.Text.Trim(), @"(^([0-9]*|\d*\d{1}?\d*)$)"))
                //{
                //    clsStandards.WriteLog(this, new Exception(" Enter  Numeric Value In Hold Days "), clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 0, true);
                //    return;
                //}
                //else
                //{
                //    obj.strHoldDays = Convert.ToInt32(txtHoldDays.Text);
                //}
                obj.strHoldDays = 0;

            }

            ////////////


            ////
            //obj.strAddress = txtAddress1.Text;

            ////
            // obj.strHoldDays = Convert.ToInt32( (txtHoldDays.Text.Trim()));
            obj.strHoldDays = 0;
            obj.strPlantType = ddlPlantType.Text.Trim();
            obj.strCreatedBy = clsStandards.current_Username();
            obj.strModifyBy = clsStandards.current_Username();
            obj.iSt = status();
            obj.strFlag = strFlag;
            if (strFlag == "EDIT" && ViewState["REFNO"].ToString() != "")
            {
                if (txtRemark.Text == "" || txtRemark.Text == null)
                {
                    clsStandards.WriteLog(this, new Exception("Remark Field Empty"), clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 0, true);
                    return;
                    //ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "alert('Remark Field Empty');", true);
                    //return;
                }

                obj.REFNO = Convert.ToInt32(ViewState["REFNO"].ToString());
            }
            else
            {
                obj.REFNO = 0;
            }
            obj.strRemark = txtRemark.Text.Trim();

            //if (grAscPlant.Rows.Count > 0)
            //{
            //    for (int i = 0; i < grAscPlant.Rows.Count; i++)
            //    {
            //        obj.strAscPlant = grAscPlant.Rows[i].Cells[0].Text;
            //        obj.strAscPlantDesc = grAscPlant.Rows[i].Cells[1].Text;
            //        strResult = objPlant.BL_InsertPlant(obj);

            //    }

            //}
            //else
            //{
            //    obj.strAscPlant = txtAscPlant.Text;
            //    obj.strAscPlantDesc = txtAscPlantDesc.Text;
            //    strResult = objPlant.BL_InsertPlant(obj);

            //}
            obj.strAscPlant = "";
            obj.strAscPlantDesc = "";
            strResult = objPlant.BL_InsertPlant(obj);
            if (strResult.StartsWith("0"))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "alert('" + strResult.Split('|').GetValue(1).ToString() + "');", true);
                lblRemark.Visible = false;
                txtRemark.Visible = false;
                Clear();
            }
            else if (strResult.StartsWith("1"))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "alert('" + strResult.Split('|').GetValue(1).ToString() + "');", true);
                return;

            }

        }
        catch (Exception ex)
        {
            clsStandards.WriteLog(this, ex, clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 0, true);
            return;
        }



    }

    ///clear view Search Control
    ///

    public void view_Control_Clear()
    {
        cboSearch.SelectedIndex = 0;
        cboCriteria.SelectedIndex = 0;
        txtSearch.Text = string.Empty;
    }

    //Inserting and Updateing Data of Plant
    protected void imgSave_Click(object sender, ImageClickEventArgs e)
    {
        try
        {

            InsertPlantMaster();
            //VisibleRemarkFalse();
            txtPlantCode.Enabled = true;
            txtRemark.Text = "";
        }
        catch (Exception ex)
        {
            clsStandards.WriteLog(this, ex, clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 0, true);
        }
    }

    //Populating Data of Plant in Grid
    protected void imgDetails_Click(object sender, ImageClickEventArgs e)
    {
        view_Control_Clear();
        BL_PlantMaster objPlant = new BL_PlantMaster();
        try
        {
            clsStandards.populateGrid(objPlant.BL_GetPlantDtl_Master(clsStandards.WhereStatement_NoST(cboSearch.SelectedValue, cboCriteria.SelectedValue, txtSearch.Text.Trim())), GrPlant, "PLANTCODE");
            MultiView1.SetActiveView(View1);

        }
        catch (Exception ex)
        {
            clsStandards.WriteLog(this, ex, clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 0, true);
        }
    }

    //Picking Plant Data From Excel and Populating in Grid
    protected void btnImp_Click(object sender, EventArgs e)
    {

        PL_PlantMastercs objPlant = new PL_PlantMastercs();
        BL_PlantMaster objBlPlant = new BL_PlantMaster();
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

    //Adding Entered Data Into Grid
    //protected void btnAdd_Click(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        if (grAscPlant.Rows.Count > 0)
    //        {
    //            DataTable dt_new = new DataTable();
    //            DataTable dt_Old = new DataTable();
    //            dt_Old = (DataTable)ViewState["Data"];
    //            dt_new.Columns.AddRange(new DataColumn[2] { new DataColumn("ASCPLANT"), new DataColumn("ASCDESC") });
    //            DataRow dr = dt_new.NewRow();
    //            for (int i = 0; i < dt_Old.Rows.Count; i++)
    //            {
    //                if (txtAscPlant.Text == dt_Old.Rows[i][0].ToString())
    //                {
    //                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "alert('Asc Plant can not duplicated');", true);
    //                    return;
    //                }
    //            }
    //            dr["ASCPLANT"] = txtAscPlant.Text;
    //            dr["ASCDESC"] = txtAscPlantDesc.Text;
    //            dt_new.Rows.Add(dr);
    //            dt_new.AcceptChanges();

    //            dt_Old.Merge(dt_new);
    //            dt_Old.AcceptChanges();
    //            grAscPlant.DataSource = dt_Old;
    //            grAscPlant.DataBind();

    //        }
    //        else
    //        {
    //            DataTable dt = new DataTable();
    //            dt.Columns.AddRange(new DataColumn[2] { new DataColumn("ASCPLANT"), new DataColumn("ASCDESC") });
    //            DataRow dr_ = dt.NewRow();
    //            dr_["ASCPLANT"] = txtAscPlant.Text;
    //            dr_["ASCDESC"] = txtAscPlantDesc.Text;
    //            dt.Rows.Add(dr_);
    //            dt.AcceptChanges();
    //            grAscPlant.DataSource = dt;
    //            grAscPlant.DataBind();
    //            ViewState["Data"] = dt;
    //        }
    //        txtAscPlant.Text = "";
    //        txtAscPlantDesc.Text = "";
    //    }
    //    catch (Exception ex)
    //    {
    //        clsStandards.WriteLog(this, ex, clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 0, true);
    //    }

    //}

    #region From Gridview
    //Inserting Plant Data which was imported from Excel.
    protected void btnSave_Click(object sender, EventArgs e)
    {
        int pass = 0;
        if (grPlant1.Rows.Count > 0)
        {
            strFlag = "ADD";
            PL_PlantMastercs obj = new PL_PlantMastercs();
            BL_PlantMaster objPlant = new BL_PlantMaster();
            for (int i = 0; i < grPlant1.Rows.Count; i++)
            {
                try
                {
                    string strResult = "";


                    obj.strLoc = clsStandards.filter(grPlant1.Rows[i].Cells[0].Text);//LOCATION
                    obj.strPlantCode = clsStandards.filter(grPlant1.Rows[i].Cells[1].Text);//PLANT CODE
                    obj.strPlantDesc = clsStandards.filter(grPlant1.Rows[i].Cells[2].Text);//PLANT DESC
                    obj.strAscPlant = clsStandards.filter(grPlant1.Rows[i].Cells[3].Text);//ASS PLANT
                    obj.strAscPlantDesc = clsStandards.filter(grPlant1.Rows[i].Cells[4].Text);//ASS PLANT DESC
                    obj.strAddress1 = clsStandards.filter(grPlant1.Rows[i].Cells[5].Text);//ADD 1
                    obj.strAddress2 = clsStandards.filter(grPlant1.Rows[i].Cells[6].Text);//ADD 2
                    obj.strAddress3 = clsStandards.filter(grPlant1.Rows[i].Cells[7].Text);//ADD 3
                    obj.strAddress4 = clsStandards.filter(grPlant1.Rows[i].Cells[8].Text);//ADD 4
                    obj.strState = clsStandards.filter(grPlant1.Rows[i].Cells[9].Text);//ADD 5
                    obj.strCity = clsStandards.filter(grPlant1.Rows[i].Cells[10].Text);//ADD 6
                    obj.strPincode = clsStandards.filter("");//ADD 7
                    obj.strHoldDays = Convert.ToInt32(grPlant1.Rows[i].Cells[11].Text);//ADD HOLD DAYS
                    obj.strPlantType = clsStandards.filter(grPlant1.Rows[i].Cells[12].Text);//PLANT TYPE
                    obj.strCreatedBy = clsStandards.filter(clsStandards.current_Username());
                    obj.strModifyBy = clsStandards.filter(clsStandards.current_Username());
                    obj.iSt = Convert.ToInt32(grPlant1.Rows[i].Cells[13].Text);
                    obj.strFlag = "ADD";
                    obj.strRemark = "";



                    strResult = objPlant.BL_InsertPlant(obj);
                    if (strResult.StartsWith("0") == true)
                    {
                        pass = pass + 1;

                        //ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "alert('" + strResult + "');", true);
                        // return;
                    }
                    else
                    {
                        grPlant1.Rows[i].BackColor = System.Drawing.Color.Red;
                    }


                }
                catch (Exception ex)
                {
                    pass = pass + 0;
                    //clsStandards.WriteLog(this, ex, clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 0, true);
                    //return;
                }



            }
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "alert('" + pass + " Out of " + grPlant1.Rows.Count + " Records Saved Sucessfully');", true);
            pass = 0;
            return;
        }

    }
    #endregion
    //public override void VerifyRenderingInServerForm(Control control)
    //{
    //    /* Verifies that the control is rendered */
    //}

    //Exports Plant Data into Excel
    protected void imgExport_Click(object sender, ImageClickEventArgs e)
    {
        BL_PlantMaster objMaster = new BL_PlantMaster();
        try
        {
            GrPlant.AllowPaging = false;
            GrPlant.AllowSorting = false;

            //clsxlsExport.ExportExcel(GrPlant, "Company.xls");

            clsxlsExport.ConvertToxls(objMaster.BL_GetPlantDtl_Master(clsStandards.WhereStatement_NoST(cboSearch.SelectedValue, cboCriteria.SelectedValue, txtSearch.Text.Trim())), "PlantMaster");
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

    //Populates Data in Grid Against Search Condition.
    protected void imgSearch_Click(object sender, ImageClickEventArgs e)
    {
        BL_PlantMaster objMaster = new BL_PlantMaster();
        try
        {
            clsStandards.populateGrid(objMaster.BL_GetPlantDtl_Master(clsStandards.WhereStatement_NoST(cboSearch.SelectedValue, cboCriteria.SelectedValue, txtSearch.Text.Trim())), GrPlant, "PLANTCODE");
            MultiView1.SetActiveView(View1);
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
    protected void GrPlant_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        //grAscPlant.DataSource = null;
        //grAscPlant.DataBind();


        if (e.CommandName == "Select")
        {


            int RowInd = ((GridViewRow)(((LinkButton)e.CommandSource).NamingContainer)).RowIndex;
            DataTable dt = new DataTable();
            BL_PlantMaster objMaster = new BL_PlantMaster();
            try
            {
                dt = objMaster.BL_GetPlantDtl_Master(clsStandards.WhereStatement_NoST("[REFNO]", "Equal To", GrPlant.DataKeys[RowInd].Value.ToString()));
                if (dt.Rows.Count != 0)
                {
                    txtLoc.Text = dt.Rows[0]["LOCATION"].ToString();
                    txtPlantCode.Text = dt.Rows[0]["PLANTCODE"].ToString();
                    txtPlantDesc.Text = dt.Rows[0]["PLANTDESC"].ToString();
                    //txtAscPlant.Text = dt.Rows[0]["ASSOCIATEPLANTCODE"].ToString();
                    // txtAscPlantDesc.Text = dt.Rows[0]["ASSOCIATEPLANTDESC"].ToString();

                    // txtHoldDays.Text = dt.Rows[0]["HOLDDAYS"].ToString();

                    strFlag = "EDIT";
                    ViewState["REFNO"] = dt.Rows[0]["REFNO"].ToString();
                    txtRemark.Text = dt.Rows[0]["Remark"].ToString();
                    RBactivate.Checked = ((dt.Rows[0]["Status"].ToString() == "ACTIVATE") ? true : false);
                    RBdeactivate.Checked = ((dt.Rows[0]["Status"].ToString() == "DEACTIVATE") ? true : false);

                    txtAddress1.Text = dt.Rows[0]["ADDRESS1"].ToString();
                    txtaddress2.Text = dt.Rows[0]["ADDRESS2"].ToString();
                    txtaddress3.Text = dt.Rows[0]["ADDRESS3"].ToString();
                    txtaddress4.Text = dt.Rows[0]["ADDRESS4"].ToString();
                    txtstate.Text = dt.Rows[0]["STATE"].ToString();
                    txtcity.Text = dt.Rows[0]["CITY"].ToString();
                    txtpincode.Text = dt.Rows[0]["PINCODE"].ToString();
                    //btnAdd.Visible = false;
                    // txtHoldDays.Visible = true;
                    // txtpincode.Text = dt.Rows[0]["ADDRESS7"].ToString();



                    try { ddlPlantType.SelectedValue = dt.Rows[0]["PLANTTYPE"].ToString(); }
                    catch { }
                    //if (dt.Rows[0]["STATUS"].ToString() == "1")
                    //{
                    //    RBactivate.Checked = true;
                    //    RBdeactivate.Checked = false;
                    //}
                    //else
                    //{
                    //    RBactivate.Checked = false;
                    //    RBdeactivate.Checked = true;
                    //}
                    VisibleRemarkTrue();
                    MultiView1.SetActiveView(View2);
                    txtPlantCode.Enabled = false;
                    pnlImport.Visible = false;
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

    //to clear all fields
    public void Clear()
    {

        txtLoc.Text = string.Empty;
        txtPlantCode.Text = string.Empty;
        txtPlantDesc.Text = string.Empty;
        //txtAscPlant.Text = string.Empty;
        //txtAscPlantDesc.Text = string.Empty;
        //grAscPlant.DataSource = null;
        //grAscPlant.DataBind();

        //txtHoldDays.Text = string.Empty;
        try { ddlPlantType.SelectedValue = "Select"; }
        catch { }
        RBactivate.Checked = true;
        RBdeactivate.Checked = false;
        txtPlantCode.Enabled = true;
        txtAddress1.Text = string.Empty;
        txtaddress2.Text = string.Empty;
        txtaddress3.Text = string.Empty;
        txtaddress4.Text = string.Empty;
        txtstate.Text = string.Empty;
        txtcity.Text = string.Empty;
        txtpincode.Text = string.Empty;
        //btnAdd.Visible = true;

        strFlag = "ADD";

    }

    //Populates the Entry view of Plant Master
    protected void imgAdd1_Click1(object sender, ImageClickEventArgs e)
    {
        try
        {
            VisibleRemarkFalse();
            MultiView1.SetActiveView(View2);
            Clear();
            pnlImport.Visible = false;
            strFlag = "ADD";
        }
        catch (Exception ex)
        {
            clsStandards.WriteLog(this, ex, clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 0, true);
        }
    }

    //Close the Detail View
    protected void imgCloseV1_Click(object sender, ImageClickEventArgs e)
    {
        MultiView1.SetActiveView(View2);
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

    //Bind the data to grid GrPlant with Page indexing
    protected void GrPlant_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[0].Text = "Page " + (GrPlant.PageIndex + 1) + " of " + GrPlant.PageCount;
            e.Row.Cells[1].Text = clsStandards.FooterInfo(GrPlant);
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

    //Populate data of seleced page index of grid GrPlant.
    protected void GrPlant_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        BL_PlantMaster objPlant = new BL_PlantMaster();
        try
        {

            GrPlant.PageIndex = e.NewPageIndex;
            clsStandards.populateGrid(objPlant.BL_GetPlantDtl_Master(clsStandards.WhereStatement_NoST(cboSearch.SelectedValue, cboCriteria.SelectedValue, txtSearch.Text.Trim())), GrPlant, "PLANTCODE");
        }
        catch (Exception ex)
        {
            clsStandards.WriteLog(this, ex, clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 0, true);
        }
        finally
        {
            objPlant = null;
        }
    }

    //Close the Plant Master
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        grPlant1.DataSource = null;
        grPlant1.DataBind();
        dtImport = null;
        ViewState["Import"] = null;
        pnlImport.Visible = false;
    }
    protected void imgCloseV2_Click(object sender, ImageClickEventArgs e)
    {

    }
    //protected void txtHoldDays_TextChanged(object sender, EventArgs e)
    //{
    //    
    //}
    protected void GrPlant_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void grAscPlant_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void btnTemplate_Click(object sender, EventArgs e)
    {
        try
        {
            Response.ContentType = "application/ms-excel";
            Response.AddHeader("content-disposition", "inline; filename=PlantMaster.xlsx");
            Response.TransmitFile(Server.MapPath("~/PrnFile/PlantMaster.xlsx"));
            Response.Flush();

        }
        catch (Exception)
        {
        }
    }
}
