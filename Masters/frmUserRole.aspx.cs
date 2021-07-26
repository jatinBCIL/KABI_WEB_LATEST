using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using BusinessLayer;
using PropertyLayer;

public partial class Masters_frmUserRole : System.Web.UI.Page
{
    DataTable dt_Menu = new DataTable();
    public static string strFlg = "";
    DataTable dtImport = new DataTable();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //GrPlant.DataSource = GetDummyData();
            //GrPlant.DataBind();
            //GrPlantTree.

            try
            {
                dt_Menu = clsBLCommon.BL_getModules();

                ViewState["right"] = "";

                if (dt_Menu.Rows.Count != 0)
                {
                    int i = 0;
                    int j = 0;
                    foreach (DataRow dr in dt_Menu.Rows)
                    {

                        if (dr["CHLD"].ToString() == "0")
                        {
                            TreeNode tn = new TreeNode();
                            tn.ToolTip = dr["MNUC"].ToString();
                            tn.Value = dr["CAP"].ToString();
                            tn.Text = dr["CAP"].ToString();
                            trUserRoles.Nodes.Add(tn);
                            j = i;
                            i = i + 1;
                        }
                        else
                        {
                            TreeNode tn1 = new TreeNode();
                            tn1.ToolTip = dr["MNUC"].ToString();
                            tn1.Value = dr["CAP"].ToString();
                            tn1.Text = dr["CAP"].ToString();
                            trUserRoles.Nodes[j].ChildNodes.Add(tn1);
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                clsStandards.WriteLog(this, ex, clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 0, true);
            }
            MultiView1.SetActiveView(View1);
            strFlg = "ADD";
        }

    }

    private DataTable GetTempData()
    {
        DataTable dt = new DataTable();
        return dt;
    }

    private DataTable GetDummyData()
    {
        DataTable dt = new DataTable();
        dt.Columns.AddRange(new DataColumn[] { new DataColumn("Module", typeof(string)) });
        DataRow dr = dt.NewRow();
        dr["Module"] = "Generation";
        dt.Rows.Add(dr);

        DataRow dr2 = dt.NewRow();
        dr2["Module"] = "Printing";
        dt.Rows.Add(dr2);

        DataRow dr3 = dt.NewRow();
        dr3["Module"] = "Picking";
        dt.Rows.Add(dr3);

        DataRow dr4 = dt.NewRow();
        dr4["Module"] = "Allocation";
        dt.Rows.Add(dr4);

        DataRow dr5 = dt.NewRow();
        dr5["Module"] = "Material Verification";
        dt.Rows.Add(dr5);

        DataRow dr6 = dt.NewRow();
        dr6["Module"] = "Consumption";
        dt.Rows.Add(dr6);

        DataRow dr7 = dt.NewRow();
        dr7["Module"] = "EMRN";
        dt.Rows.Add(dr7);

        DataRow dr8 = dt.NewRow();
        dr8["Module"] = "ORLN";
        dt.Rows.Add(dr8);

        DataRow dr9 = dt.NewRow();
        dr9["Module"] = "Cubicle to Cubicle Transfer";
        dt.Rows.Add(dr9);

        dt.AcceptChanges();
        return dt;
    }

    protected void ddUserType_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            ddTransactionType.SelectedIndex = -1;
            dt_Menu = clsBLCommon.BL_getModules();
            clsStandards.RemoveItemTree(trUserRoles);
            if (ddUserType.Text.Trim() == "Administrator")
            {
                if (dt_Menu.Rows.Count != 0)
                {
                    int i = 0;
                    int j = 0;
                    foreach (DataRow dr in dt_Menu.Rows)
                    {
                        //if (dr["SRNO"].ToString().Trim().StartsWith("608")==false)
                        //{

                        if (dr["CHLD"].ToString() == "0")
                        {
                            TreeNode tn = new TreeNode();
                            tn.ToolTip = dr["MNUC"].ToString();
                            tn.Value = dr["CAP"].ToString();
                            tn.Text = dr["CAP"].ToString();
                            trUserRoles.Nodes.Add(tn);
                            j = i;
                            i = i + 1;
                        }
                        else
                        {
                            TreeNode tn1 = new TreeNode();
                            tn1.ToolTip = dr["MNUC"].ToString();
                            tn1.Value = dr["CAP"].ToString();
                            tn1.Text = dr["CAP"].ToString();
                            trUserRoles.Nodes[j].ChildNodes.Add(tn1);
                        }
                        //}
                    }

                    if (ViewState["right"].ToString().Trim() != "")
                    {
                        clsStandards.setRights(trUserRoles, ViewState["right"].ToString().Trim());
                    }
                }
            }
            else
            {
                if (dt_Menu.Rows.Count != 0)
                {
                    int i = 0;
                    int j = 0;
                    foreach (DataRow dr in dt_Menu.Rows)
                    {
                        if (dr["SRNO"].ToString().Trim().StartsWith("1") == false)
                        {
                            if (dr["CHLD"].ToString() == "0")
                            {
                                TreeNode tn = new TreeNode();
                                tn.ToolTip = dr["MNUC"].ToString();
                                tn.Value = dr["CAP"].ToString();
                                tn.Text = dr["CAP"].ToString();
                                trUserRoles.Nodes.Add(tn);
                                j = i;
                                i = i + 1;
                            }
                            else
                            {
                                TreeNode tn1 = new TreeNode();
                                tn1.ToolTip = dr["MNUC"].ToString();
                                tn1.Value = dr["CAP"].ToString();
                                tn1.Text = dr["CAP"].ToString();
                                trUserRoles.Nodes[j].ChildNodes.Add(tn1);
                            }
                        }
                    }
                    if (ViewState["right"].ToString().Trim() != "")
                    {
                        clsStandards.setRights(trUserRoles, ViewState["right"].ToString().Trim());
                    }
                }
            }
        }
        catch (Exception ex)
        {
            clsStandards.WriteLog(this, ex, clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 0, true);
        }
    }

    protected void btnClose_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Modules/frmMain.aspx");
    }

    protected void imgCloseV1_Click(object sender, ImageClickEventArgs e)
    {
        MultiView1.SetActiveView(View1);
    }

    protected void imgAdd1_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            MultiView1.SetActiveView(View1);
            strFlg = "ADD";
        }
        catch (Exception ex)
        {
            clsStandards.WriteLog(this, ex, clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 0, true);
        }
    }

    protected void imgCloseV2_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("~/Modules/frmMain.aspx");
    }

    protected void imgSearch_Click(object sender, ImageClickEventArgs e)
    {
        BL_UserMaster objItems = new BL_UserMaster();
        try
        {
            GrUserRole.DataSource = null;
            GrUserRole.DataBind();
            clsStandards.populateGrid(objItems.BL_GetUserRoles(clsStandards.WhereStatement(0, cboSearch.SelectedValue, cboCriteria.SelectedValue, txtSearch.Text.Trim())), GrUserRole, "Refno");
        }
        catch (Exception ex)
        {
            clsStandards.WriteLog(this, ex, clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 0, true);
        }
        finally
        {
            objItems = null;
        }
        MultiView1.SetActiveView(View2);
    }

    protected void GrUserRole_PageIndexChanging1(object sender, GridViewPageEventArgs e)
    {
        GrUserRole.PageIndex = e.NewPageIndex;
        BL_UserMaster objMaster = new BL_UserMaster();
        try
        {
            clsStandards.populateGrid(objMaster.BL_GetUserRoles(clsStandards.WhereStatement_NoST(cboSearch.SelectedValue, cboCriteria.SelectedValue, txtSearch.Text.Trim())), GrUserRole, "Refno");
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

    protected void GrUserRole_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        PL_UserRoles objPL = new PL_UserRoles();
        if (e.CommandName == "Select")
        {
            int RowInd = ((GridViewRow)(((LinkButton)e.CommandSource).NamingContainer)).RowIndex;
            // GridViewRow gr = GrDept.Rows[Convert.ToInt32(e.CommandArgument)];
            DataTable dt = new DataTable();
            BL_UserMaster objMaster = new BL_UserMaster();
            try
            {
                dt = objMaster.BL_GetUserRoles(clsStandards.WhereStatement_NoST("[Refno]", "Equal to", GrUserRole.DataKeys[RowInd].Value.ToString()));
                if (dt.Rows.Count != 0)
                {
                    clsStandards.ClearTree(trUserRoles);
                    txtRole.Text = dt.Rows[0]["Roles"].ToString();
                    try
                    {
                        ddUserType.SelectedValue = dt.Rows[0]["UserType"].ToString();
                        ddUserType_SelectedIndexChanged(sender, e);
                    }
                    catch
                    {
                    }
                    try
                    {
                        ddTransactionType.SelectedValue = dt.Rows[0]["TransType"].ToString();
                        ddTransactionType_SelectedIndexChanged(sender, e);
                    }
                    catch
                    {
                    }
                    RBactivate.Checked = ((dt.Rows[0]["Status"].ToString().ToUpper() == "ACTIVATE") ? true : false);
                    RBdeactivate.Checked = ((dt.Rows[0]["Status"].ToString().ToUpper() == "DEACTIVATE") ? true : false);

                    ViewState["REFNO"] = dt.Rows[0]["Refno"].ToString();
                    clsStandards.setRights(trUserRoles, dt.Rows[0]["Rights"].ToString());
                    ViewState["right"] = dt.Rows[0]["Rights"].ToString();

                    txtRole.Enabled = false;

                    MultiView1.SetActiveView(View1);

                    strFlg = "EDIT";


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

    protected void GrUserRole_Sorting(object sender, GridViewSortEventArgs e)
    {
        BL_UserMaster objMaster = new BL_UserMaster();
        try
        {
            clsStandards.populateGrid(objMaster.BL_GetUserRoles(clsStandards.WhereStatement_NoST(cboSearch.SelectedValue, cboCriteria.SelectedValue, txtSearch.Text.Trim())), GrUserRole, e.SortExpression.ToString());
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

    protected void GrUserRole_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[0].Text = "Page " + (GrUserRole.PageIndex + 1) + " of " + GrUserRole.PageCount;
            e.Row.Cells[1].Text = clsStandards.FooterInfo(GrUserRole);
        }
    }

    protected void imgDetails_Click(object sender, ImageClickEventArgs e)
    {
        BL_UserMaster objMaster = new BL_UserMaster();
        try
        {
            clsStandards.populateGrid(objMaster.BL_GetUserRoles(clsStandards.WhereStatement_NoST(string.Empty, string.Empty, string.Empty)), GrUserRole, "Refno");
            MultiView1.SetActiveView(View2);
        }
        catch (Exception ex)
        {
            clsStandards.WriteLog(this, ex, clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 0, true);
        }
        finally { objMaster = null; }
    }

    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Verifies that the control is rendered */
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
        PL_UserRoles objField = new PL_UserRoles();
        BL_UserMaster objMaster = new BL_UserMaster();
        string strResult = "";
        try
        {
            objField.strRole = txtRole.Text.Trim();
            objField.strUserType = ddUserType.Text.Trim();
            objField.strTransType = ddTransactionType.Text.Trim();
            objField.strMODE = strFlg;
            objField.intStatus = status();
            objField.strCreatedBy = clsStandards.current_Username();
            objField.strRight = clsStandards.getCheckedTree(trUserRoles);
            if (objField.strRight.ToString().Trim() == "")
            {
                clsStandards.WriteLog(this, new Exception("no role is selected against role name"), clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 2, true);
                return;
            }
            if (strFlg == "EDIT" && ViewState["REFNO"].ToString() != "")
            {
                objField.REFNO = Convert.ToInt32(ViewState["REFNO"].ToString());
            }
            else
            {
                objField.REFNO = 0;
            }
            //  objField.strUsername = clsStandards.current_Username();

            strResult = objMaster.BL_InsertUserRoles(objField);
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
                txtRole.Focus();
                //clsStandards.populateGrid(objMaster.GetConsigneeSelect(clsStandards.WhereStatement_NoST(string.Empty, string.Empty, string.Empty)), GrPlants, "BCIL_ID");
                //MultiView1.SetActiveView(View1);
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
            strResult = null;
        }
    }

    public void clear()
    {

        txtRole.Text = string.Empty;
        txtRole.Enabled = true;
        ddUserType.Enabled = true;
        ddTransactionType.Enabled = true;
        try
        {
            ddUserType.SelectedIndex = -1;
        }
        catch
        {
        }
        try
        {
            ddTransactionType.SelectedIndex = -1;
        }
        catch
        {
        }
        RBactivate.Checked = true;
        RBdeactivate.Checked = false;
        clsStandards.ClearTree(trUserRoles);
    }

    protected void trUserRoles_TreeNodeCheckChanged(object sender, TreeNodeEventArgs e)
    {
        //foreach (TreeNode objNode in trUserRoles.Nodes)
        //{
        //    if (objNode.Checked == true)
        //    {
        //        foreach (TreeNode objNode1 in objNode.ChildNodes)
        //        {
        //            objNode1.Checked = true;
        //        }
        //    }
        //}

    }

    protected void imgExport_Click(object sender, ImageClickEventArgs e)
    {
        BL_UserMaster objMaster = new BL_UserMaster();
        try
        {
            GrUserRole.AllowPaging = false;
            GrUserRole.AllowSorting = false;

            //clsxlsExport.ExportExcel(GrPlant, "Company.xls");

            clsxlsExport.ConvertToxls(objMaster.BL_GetUserRoles(clsStandards.WhereStatement(0, cboSearch.SelectedValue, cboCriteria.SelectedValue, txtSearch.Text.Trim())), "UserRoles");
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

    protected void ddTransactionType_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            clsStandards.RemoveItemTree(trUserRoles);
            if (ddTransactionType.SelectedValue == "Display")
            {

                dt_Menu = clsBLCommon.BL_getModules();
                

                if (dt_Menu.Rows.Count != 0)
                {
                    int i = 0;
                    int j = 0;
                    foreach (DataRow dr in dt_Menu.Rows)
                    {
                        if (dr["SRNO"].ToString().Trim().StartsWith("3") == true || dr["SRNO"].ToString().Trim().StartsWith("6") == true || dr["SRNO"].ToString().Trim().StartsWith("9") == true)
                        {

                            if (dr["CHLD"].ToString() == "0")
                            {
                                TreeNode tn = new TreeNode();
                                tn.ToolTip = dr["MNUC"].ToString();
                                tn.Value = dr["CAP"].ToString();
                                tn.Text = dr["CAP"].ToString();
                                trUserRoles.Nodes.Add(tn);
                                j = i;
                                i = i + 1;
                            }
                            else
                            {
                                TreeNode tn1 = new TreeNode();
                                tn1.ToolTip = dr["MNUC"].ToString();
                                tn1.Value = dr["CAP"].ToString();
                                tn1.Text = dr["CAP"].ToString();
                                trUserRoles.Nodes[j].ChildNodes.Add(tn1);
                            }
                        }
                    }

                    if (ViewState["right"].ToString().Trim() != "")
                    {
                        clsStandards.setRights(trUserRoles, ViewState["right"].ToString().Trim());
                    }
                }
            }
            else
            {
                dt_Menu = clsBLCommon.BL_getModules();
               
                if (ddUserType.Text.Trim() == "Administrator")
                {
                    if (dt_Menu.Rows.Count != 0)
                    {
                        int i = 0;
                        int j = 0;
                        foreach (DataRow dr in dt_Menu.Rows)
                        {
                            if (dr["SRNO"].ToString().Trim().StartsWith("3") == false && dr["SRNO"].ToString().Trim().StartsWith("6") == false && dr["SRNO"].ToString().Trim().StartsWith("9") == false)
                            {

                                if (dr["CHLD"].ToString() == "0")
                                {
                                    TreeNode tn = new TreeNode();
                                    tn.ToolTip = dr["MNUC"].ToString();
                                    tn.Value = dr["CAP"].ToString();
                                    tn.Text = dr["CAP"].ToString();
                                    trUserRoles.Nodes.Add(tn);
                                    j = i;
                                    i = i + 1;
                                }
                                else
                                {
                                    TreeNode tn1 = new TreeNode();
                                    tn1.ToolTip = dr["MNUC"].ToString();
                                    tn1.Value = dr["CAP"].ToString();
                                    tn1.Text = dr["CAP"].ToString();
                                    trUserRoles.Nodes[j].ChildNodes.Add(tn1);
                                }
                            }
                        }

                        if (ViewState["right"].ToString().Trim() != "")
                        {
                            clsStandards.setRights(trUserRoles, ViewState["right"].ToString().Trim());
                        }
                    }
                }
                else
                {
                    if (dt_Menu.Rows.Count != 0)
                    {
                        int i = 0;
                        int j = 0;

                        foreach (DataRow dr in dt_Menu.Rows)
                        {
                            if (dr["SRNO"].ToString().Trim().StartsWith("1") == false)
                            {
                                if (dr["CHLD"].ToString() == "0")
                                {
                                    TreeNode tn = new TreeNode();
                                    tn.ToolTip = dr["MNUC"].ToString();
                                    tn.Value = dr["CAP"].ToString();
                                    tn.Text = dr["CAP"].ToString();
                                    trUserRoles.Nodes.Add(tn);
                                    j = i;
                                    i = i + 1;
                                }
                                else
                                {
                                    TreeNode tn1 = new TreeNode();
                                    tn1.ToolTip = dr["MNUC"].ToString();
                                    tn1.Value = dr["CAP"].ToString();
                                    tn1.Text = dr["CAP"].ToString();
                                    trUserRoles.Nodes[j].ChildNodes.Add(tn1);
                                }
                            }
                        }
                        if (ViewState["right"].ToString().Trim() != "")
                        {
                            clsStandards.setRights(trUserRoles, ViewState["right"].ToString().Trim());
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            clsStandards.WriteLog(this, ex, clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 0, true);
        }
    }
}