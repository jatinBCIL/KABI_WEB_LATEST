using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using BusinessLayer;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using System.Web.UI.HtmlControls;

public partial class Reports_frmAuditTrailReport : System.Web.UI.Page
{

    string intPreviousRowID = string.Empty;
    string intPreviousBatch = string.Empty;
    string intPreviousBin = string.Empty;
    // To keep track the Index of Group Total
    int intSubTotalIndex = 1;

    // To temporarily store Group Total
    double dblSubTotalDirectRevenue = 0;

    // To temporarily store Grand Total
    double dblGrandTotalDirectRevenue = 0;
    int PackageNo = 0;
    BL_For_Reports objReports = new BL_For_Reports();
    int _intRows;
    int _intTotalQty;
    double _dblValue;
    [System.Web.Services.WebMethod]
    [System.Web.Script.Services.ScriptMethod]

    protected void Page_Load(object sender, EventArgs e)
    {
        ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);
        scriptManager.RegisterPostBackControl(this.btnPost);

      
        if (!Page.IsPostBack)
        {
            MultiView1.SetActiveView(View1);
            try
            {
               
            }
            catch (Exception ex)
            {
            }
            finally
            {
            }
            ViewState["Query"] = string.Empty;
        }
    }

    protected void ddlrpttype_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlrpttype.Text.Trim() == "User_Master")
            {
                ViewState["Filters"] = "'USER_ID','USER_NAME','DEPARTMENT','PLANTCODE','Doneby','Doneon',";
                objReports.BindSearchGrid(grSearch, "VW_AT_TBLMASTER_USER", ViewState["Filters"].ToString()); 
            }
            else if (ddlrpttype.Text.Trim() == "Plant_Master")
            {
                ViewState["Filters"] = "'CREATEDBY','CREATEDON','PLANTCODE','LOCATION',";
                objReports.BindSearchGrid(grSearch, "VW_PLANTMASTER", ViewState["Filters"].ToString()); 
            }
            else if (ddlrpttype.Text.Trim() == "Printer_Master")
            {
                ViewState["Filters"] = "'PLANTCODE','PRINTERCODE','DONEON','DONEBY',";
                objReports.BindSearchGrid(grSearch, "VW_AT_PPPrinter_M", ViewState["Filters"].ToString());
            }
            else if (ddlrpttype.Text.Trim() == "Weighing_Master")
            {
                ViewState["Filters"] = "'PLANTCODE','WEIGHINGCODE','DONEON','DONEBY',";
                objReports.BindSearchGrid(grSearch, "VW_Weighing_Audit", ViewState["Filters"].ToString());
            }
            else if (ddlrpttype.Text.Trim() == "Purchase_Order")
            {
                ViewState["Filters"] = "'Plant','PurchaseOrderNo','CreatedOn','CreaatedBy',";
                objReports.BindSearchGrid(grSearch, "VW_PurchaseOrder", ViewState["Filters"].ToString());
            }
            else if (ddlrpttype.Text.Trim() == "Purchase_Order_Weight")
            {
                ViewState["Filters"] = "'Plant','PurchaseOrderNo',";
                objReports.BindSearchGrid(grSearch, "VW_PurchaseOrder_Weights", ViewState["Filters"].ToString());
            }
            else if (ddlrpttype.Text.Trim() == "Purchase_Order_GeneralDetails")
            {
                ViewState["Filters"] = "'Plant','PurchaseOrderNo','ItemCode','Createdby','Createdon',";
                objReports.BindSearchGrid(grSearch, "VW_PurchaseOrderGeneral", ViewState["Filters"].ToString());
            }
            else if (ddlrpttype.Text.Trim() == "Purchase_Order_ReceivingDetails")
            {
                ViewState["Filters"] = "'Plant','PurchaseOrderNo','MaterialCode','Createdby','Createdon',";
                objReports.BindSearchGrid(grSearch, "VW_PurchaseOrderReciving", ViewState["Filters"].ToString());
            }
            else if (ddlrpttype.Text.Trim() == "ManNo_Creation")
            {
                ViewState["Filters"] = "'Plant','PurchaseOrderNo','ItemCode','MANNo','Createdby','CreatedOn',";
                objReports.BindSearchGrid(grSearch, "VW_AT_ManNo", ViewState["Filters"].ToString()); 
            }
            else if (ddlrpttype.Text.Trim() == "ManNo_Transaction(Barcode)")
            {
                ViewState["Filters"] = "'BarcodeNo','Plant','PurchaseOrderNo','DONEON','DONEBY',";
                objReports.BindSearchGrid(grSearch, "VW_AT_ManNo_Trans", ViewState["Filters"].ToString());
            }
            else if (ddlrpttype.Text.Trim() == "Allocation")
            {
                ViewState["Filters"] = "'BarcodeNo','LocationCode','PlantCode','Process','DONEON','DONEBY',";
                objReports.BindSearchGrid(grSearch, "VW_AT_STR_Allocation_T", ViewState["Filters"].ToString());
            }
            else if (ddlrpttype.Text.Trim() == "PickList")
            {
                ViewState["Filters"] = "'PickListNo','ProcessOrderNo','ProductCode','DONEON','DONEBY',";
                objReports.BindSearchGrid(grSearch, "VW_AT_PickList", ViewState["Filters"].ToString());
            }
            else if (ddlrpttype.Text.Trim() == "Dispensing")
            {
                ViewState["Filters"] = "'ProcessOrder','MatCode','BatchNo','DONEON','DONEBY',";
                objReports.BindSearchGrid(grSearch, "VW_AT_Reservation", ViewState["Filters"].ToString());
            }         
            else if (ddlrpttype.Text.Trim() == "Reservation")
            {
                ViewState["Filters"] = "'ReservationNo','ItemCode','Createdby','CreatedOn',";
                objReports.BindSearchGrid(grSearch, "VW_AT_Reservation", ViewState["Filters"].ToString());
            }
            else if (ddlrpttype.Text.Trim() == "Material_Document(Dispensing_Posting)")
            {
                ViewState["Filters"] = "'Plant','ProductCode','ItemCode','Createdby','CreatedOn',";
                objReports.BindSearchGrid(grSearch, "VW_AT_MaterialDocument", ViewState["Filters"].ToString());
            }
            else if (ddlrpttype.Text.Trim() == "Dispensing_Posting_Error")
            {
                ViewState["Filters"] = "'PROCESSORDERNO','PLANTCODE','Createdby','CreatedOn',";
                objReports.BindSearchGrid(grSearch, "VW_DISPENSINGPOSTING_ERROR", ViewState["Filters"].ToString());
            }
            else if (ddlrpttype.Text.Trim() == "Sample_Transaction")
            {
                ViewState["Filters"] = "'Plant','BarcodeNo','Doneby','DoneOn',";
                objReports.BindSearchGrid(grSearch, "VW_AT_Sample_Trans", ViewState["Filters"].ToString());
            }
            else if (ddlrpttype.Text.Trim() == "Weighing_Calibration_Month")
            {
                ViewState["Filters"] = "'WeighingBarcode','PlantCode','DONEON','DONEBY',";
                objReports.BindSearchGrid(grSearch, "VW_WeighingCalibration_Monthlys", ViewState["Filters"].ToString());
            }
            else if (ddlrpttype.Text.Trim() == "Weighing_Calibration_Daily")
            {
                ViewState["Filters"] = "'WeighingBarcode','PlantCode','DONEON','DONEBY',";
                objReports.BindSearchGrid(grSearch, "VW_WeighingCalibration_Daily", ViewState["Filters"].ToString());
            }
            else if (ddlrpttype.Text.Trim() == "Scheduler_INSPECTIONLOT")
            {
                ViewState["Filters"] = "'ItemCode','BatchNo','INSPECTIONLOT','Plant','CreatedOn',";
                objReports.BindSearchGrid(grSearch, "VW_Scheduler_INSPECTIONLOT", ViewState["Filters"].ToString());
            }
            else if (ddlrpttype.Text.Trim() == "Scheduler_UDCODE")
            {
                ViewState["Filters"] = "'ItemCode','BatchNo','INSPECTIONLOT','ARNO','Plant','CreatedOn',";
                objReports.BindSearchGrid(grSearch, "VW_Scheduler_UDCODE", ViewState["Filters"].ToString());
            }
            else if (ddlrpttype.Text.Trim() == "Scheduler_RETEST")
            {
                ViewState["Filters"] = "'ItemCode','BatchNo','INSPECTIONLOT','ARNO','Plant','CreatedOn',";
                objReports.BindSearchGrid(grSearch, "VW_Scheduler_RETEST", ViewState["Filters"].ToString());
            }
            else
            {
                grSearch.DataSource = null;
                grSearch.DataBind();
            }
            for (int i = 0; i < grSearch.Rows.Count; i++)
            {
                if (grSearch.Rows[i].Cells[1].Text == "datetime" || grSearch.Rows[i].Cells[1].Text == "date")
                {
                    ((DropDownList)grSearch.Rows[i].Cells[2].FindControl("cboCriteria")).Visible = false;
                    ((TextBox)grSearch.Rows[i].Cells[3].FindControl("txtSearch")).Visible = false;
                    ((System.Web.UI.HtmlControls.HtmlTable)grSearch.Rows[i].Cells[3].FindControl("tblDate")).Visible = true;
                }
            }
        }
        catch (Exception ex)
        {
            clsStandards.WriteLog(this, ex, clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 0, true);
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        string strQuery = string.Empty;
        DataSet ds = new DataSet();

        try
        {
            string strWhere = "";
            lblModule.Text = ddlrpttype.Text.Trim().ToString();
            for (int j = 0; j < grSearch.Rows.Count; j++)
            {
                if (grSearch.Rows[j].Cells[1].Text == "datetime")
                {
                    if (((TextBox)grSearch.Rows[j].Cells[3].FindControl("txtFrom")).Text != "" || ((TextBox)grSearch.Rows[j].Cells[3].FindControl("txtTo")).Text != "")
                    {
                        if (strWhere == "")
                        {
                            strWhere = createDateCondition(grSearch.Rows[j].Cells[0].Text, ((TextBox)grSearch.Rows[j].Cells[3].FindControl("txtFrom")).Text, ((TextBox)grSearch.Rows[j].Cells[3].FindControl("txtTo")).Text);
                        }
                        else
                        {
                            strWhere = strWhere + "AND " + createDateCondition(grSearch.Rows[j].Cells[0].Text, ((TextBox)grSearch.Rows[j].Cells[3].FindControl("txtFrom")).Text, ((TextBox)grSearch.Rows[j].Cells[3].FindControl("txtTo")).Text);
                        }
                    }
                }
                else if (grSearch.Rows[j].Cells[1].Text == "date")
                {
                    if (((TextBox)grSearch.Rows[j].Cells[3].FindControl("txtFrom")).Text != "" || ((TextBox)grSearch.Rows[j].Cells[3].FindControl("txtTo")).Text != "")
                    {
                        if (strWhere == "")
                        {
                            strWhere = createDateCondition(grSearch.Rows[j].Cells[0].Text, ((TextBox)grSearch.Rows[j].Cells[3].FindControl("txtFrom")).Text, ((TextBox)grSearch.Rows[j].Cells[3].FindControl("txtTo")).Text);
                        }
                        else
                        {
                            strWhere = strWhere + "AND " + createDateCondition(grSearch.Rows[j].Cells[0].Text, ((TextBox)grSearch.Rows[j].Cells[3].FindControl("txtFrom")).Text, ((TextBox)grSearch.Rows[j].Cells[3].FindControl("txtTo")).Text);
                        }
                    }
                }
                else
                {
                    if (((TextBox)grSearch.Rows[j].Cells[3].FindControl("txtSearch")).Text != "")
                    {
                        if (strWhere == "")
                        {
                            strWhere = createCondition(grSearch.Rows[j].Cells[0].Text, ((DropDownList)grSearch.Rows[j].Cells[2].FindControl("cboCriteria")).SelectedValue, ((TextBox)grSearch.Rows[j].Cells[3].FindControl("txtSearch")).Text);
                        }
                        else
                        {
                            strWhere = strWhere + "AND " + createCondition(grSearch.Rows[j].Cells[0].Text, ((DropDownList)grSearch.Rows[j].Cells[2].FindControl("cboCriteria")).SelectedValue, ((TextBox)grSearch.Rows[j].Cells[3].FindControl("txtSearch")).Text);
                        }
                    }
                }
            }
            {

                if (strWhere.Length != 0)
                {
                    strWhere = "Where " + strWhere;
                }

                if (ddlrpttype.Text.Trim() == "User_Master")
                {
                    ds = objReports.BL_Report_Genrater("User_Master", strWhere, strQuery);
                    GrPlants.DataSource = RemoveColum(ds);
                    GrPlants.DataBind();
                    MultiView1.SetActiveView(View2);
                }
                else if (ddlrpttype.Text.Trim() == "Plant_Master")
                {
                    ds = objReports.BL_Report_Genrater("Plant_Master", strWhere, strQuery);
                    GrPlants.DataSource = RemoveColum(ds);
                    GrPlants.DataBind();
                    MultiView1.SetActiveView(View2);
                }
                else if (ddlrpttype.Text.Trim() == "Printer_Master")
                {
                    ds = objReports.BL_Report_Genrater("Printer_Master", strWhere, strQuery);
                    GrPlants.DataSource = RemoveColum(ds);
                    GrPlants.DataBind();
                    MultiView1.SetActiveView(View2);
                }
                else if (ddlrpttype.Text.Trim() == "Weighing_Master")
                {
                    ds = objReports.BL_Report_Genrater("Weighing_Master", strWhere, strQuery);
                    GrPlants.DataSource = RemoveColum(ds);
                    GrPlants.DataBind();
                    MultiView1.SetActiveView(View2);
                }
                else if (ddlrpttype.Text.Trim() == "Purchase_Order")
                {
                    ds = objReports.BL_Report_Genrater("Purchase_Order", strWhere, strQuery);
                    GrPlants.DataSource = RemoveColum(ds);
                    GrPlants.DataBind();
                    MultiView1.SetActiveView(View2);
                }
                else if (ddlrpttype.Text.Trim() == "Purchase_Order_Weight")
                {
                    ds = objReports.BL_Report_Genrater("Purchase_Order_Weight", strWhere, strQuery);
                    GrPlants.DataSource = RemoveColum(ds);
                    GrPlants.DataBind();
                    MultiView1.SetActiveView(View2);
                }
                else if (ddlrpttype.Text.Trim() == "Purchase_Order_GeneralDetails")
                {
                    ds = objReports.BL_Report_Genrater("Purchase_Order_GeneralDetails", strWhere, strQuery);
                    GrPlants.DataSource = RemoveColum(ds);
                    GrPlants.DataBind();
                    MultiView1.SetActiveView(View2);
                }
                else if (ddlrpttype.Text.Trim() == "Purchase_Order_ReceivingDetails")
                {
                    ds = objReports.BL_Report_Genrater("Purchase_Order_ReceivingDetails", strWhere, strQuery);
                    GrPlants.DataSource = RemoveColum(ds);
                    GrPlants.DataBind();
                    MultiView1.SetActiveView(View2);
                }
                else if (ddlrpttype.Text.Trim() == "ManNo_Creation")
                {
                    ds = objReports.BL_Report_Genrater("ManNo_Creation", strWhere, strQuery);
                    GrPlants.DataSource = RemoveColum(ds);
                    GrPlants.DataBind();
                    MultiView1.SetActiveView(View2);
                }
                else if (ddlrpttype.Text.Trim() == "ManNo_Transaction(Barcode)")
                {
                    ds = objReports.BL_Report_Genrater("ManNo_Transaction(Barcode)", strWhere, strQuery);
                    GrPlants.DataSource = RemoveColum(ds);
                    GrPlants.DataBind();
                    MultiView1.SetActiveView(View2);
                }
                else if (ddlrpttype.Text.Trim() == "Allocation")
                {
                    ds = objReports.BL_Report_Genrater("Allocation", strWhere, strQuery);
                    GrPlants.DataSource = RemoveColum(ds);
                    GrPlants.DataBind();
                    MultiView1.SetActiveView(View2);
                }
                else if (ddlrpttype.Text.Trim() == "PickList")
                {
                    ds = objReports.BL_Report_Genrater("PickList", strWhere, strQuery);
                    GrPlants.DataSource = RemoveColum(ds);
                    GrPlants.DataBind();
                    MultiView1.SetActiveView(View2);
                }            
                else if (ddlrpttype.Text.Trim() == "Reservation")
                {
                    ds = objReports.BL_Report_Genrater("Reservation", strWhere, strQuery);
                    GrPlants.DataSource = RemoveColum(ds);
                    GrPlants.DataBind();
                    MultiView1.SetActiveView(View2);
                }
                else if (ddlrpttype.Text.Trim() == "Dispensing")
                {
                    ds = objReports.BL_Report_Genrater("Dispensing", strWhere, strQuery);
                    GrPlants.DataSource = RemoveColum(ds);
                    GrPlants.DataBind();
                    MultiView1.SetActiveView(View2);
                }
                else if (ddlrpttype.Text.Trim() == "Material_Document(Dispensing_Posting)")
                {
                    ds = objReports.BL_Report_Genrater("Material_Document(Dispensing_Posting)", strWhere, strQuery);
                    GrPlants.DataSource = RemoveColum(ds);
                    GrPlants.DataBind();
                    MultiView1.SetActiveView(View2);
                }
                else if (ddlrpttype.Text.Trim() == "Dispensing_Posting_Error")
                {
                    ds = objReports.BL_Report_Genrater("Dispensing_Posting_Error", strWhere, strQuery);
                    GrPlants.DataSource = RemoveColum(ds);
                    GrPlants.DataBind();
                    MultiView1.SetActiveView(View2);
                }
                else if (ddlrpttype.Text.Trim() == "Sample_Transaction")
                {
                    ds = objReports.BL_Report_Genrater("Sample_Transaction", strWhere, strQuery);
                    GrPlants.DataSource = RemoveColum(ds);
                    GrPlants.DataBind();
                    MultiView1.SetActiveView(View2);
                }
                else if (ddlrpttype.Text.Trim() == "Weighing_Calibration_Month")
                {
                    ds = objReports.BL_Report_Genrater("Weighing_Calibration_Month", strWhere, strQuery);
                    GrPlants.DataSource = RemoveColum(ds);
                    GrPlants.DataBind();
                    MultiView1.SetActiveView(View2);
                }
                else if (ddlrpttype.Text.Trim() == "Weighing_Calibration_Daily")
                {
                    ds = objReports.BL_Report_Genrater("Weighing_Calibration_Daily", strWhere, strQuery);
                    GrPlants.DataSource = RemoveColum(ds);
                    GrPlants.DataBind();
                    MultiView1.SetActiveView(View2);
                }
                else if (ddlrpttype.Text.Trim() == "Scheduler_INSPECTIONLOT")
                {
                    ds = objReports.BL_Report_Genrater("Scheduler_INSPECTIONLOT", strWhere, strQuery);
                    GrPlants.DataSource = RemoveColum(ds);
                    GrPlants.DataBind();
                    MultiView1.SetActiveView(View2);
                }
                else if (ddlrpttype.Text.Trim() == "Scheduler_UDCODE")
                {
                    ds = objReports.BL_Report_Genrater("Scheduler_UDCODE", strWhere, strQuery);
                    GrPlants.DataSource = RemoveColum(ds);
                    GrPlants.DataBind();
                    MultiView1.SetActiveView(View2);
                }
                else if (ddlrpttype.Text.Trim() == "Scheduler_RETEST")
                {
                    ds = objReports.BL_Report_Genrater("Scheduler_RETEST", strWhere, strQuery);
                    GrPlants.DataSource = RemoveColum(ds);
                    GrPlants.DataBind();
                    MultiView1.SetActiveView(View2);
                }
                else if (ddlrpttype.Text == "Select")
                {
                    clsStandards.WriteLog(this, new Exception("Please Select Report Name"), clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 0, true);
                }
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {

                    btnPost.Visible = true;
                    btnCancel.Visible = true;
                }
                else
                {

                    btnPost.Visible = false;
                    btnCancel.Visible = false;
                }
                ViewState["Query"] = strQuery;
                ViewState["Where"] = strWhere;


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

    protected void btnViewAll_Click(object sender, EventArgs e)
    {
        string strWhere = "";
        string strQuery = string.Empty;
        DataSet ds = new DataSet();
        try
        {
            lblModule.Text = ddlrpttype.Text.Trim().ToString();

            if (ddlrpttype.Text.Trim() == "User_Master")
            {
                ds = objReports.BL_Report_Genrater("User_Master", strWhere, strQuery);
                GrPlants.DataSource = RemoveColum(ds);
                GrPlants.DataBind();
                MultiView1.SetActiveView(View2);
            }
            else if (ddlrpttype.Text.Trim() == "Plant_Master")
            {
                ds = objReports.BL_Report_Genrater("Plant_Master", strWhere, strQuery);
                GrPlants.DataSource = RemoveColum(ds);
                GrPlants.DataBind();
                MultiView1.SetActiveView(View2);
            }
            else if (ddlrpttype.Text.Trim() == "Printer_Master")
            {
                ds = objReports.BL_Report_Genrater("Printer_Master", strWhere, strQuery);
                GrPlants.DataSource = RemoveColum(ds);
                GrPlants.DataBind();
                MultiView1.SetActiveView(View2);
            }
            else if (ddlrpttype.Text.Trim() == "Weighing_Master")
            {
                ds = objReports.BL_Report_Genrater("Weighing_Master", strWhere, strQuery);
                GrPlants.DataSource = RemoveColum(ds);
                GrPlants.DataBind();
                MultiView1.SetActiveView(View2);
            }
            else if (ddlrpttype.Text.Trim() == "Purchase_Order")
            {
                ds = objReports.BL_Report_Genrater("Purchase_Order", strWhere, strQuery);
                GrPlants.DataSource = RemoveColum(ds);
                GrPlants.DataBind();
                MultiView1.SetActiveView(View2);
            }
            else if (ddlrpttype.Text.Trim() == "Purchase_Order_Weight")
            {
                ds = objReports.BL_Report_Genrater("Purchase_Order_Weight", strWhere, strQuery);
                GrPlants.DataSource = RemoveColum(ds);
                GrPlants.DataBind();
                MultiView1.SetActiveView(View2);
            }
            else if (ddlrpttype.Text.Trim() == "Purchase_Order_GeneralDetails")
            {
                ds = objReports.BL_Report_Genrater("Purchase_Order_GeneralDetails", strWhere, strQuery);
                GrPlants.DataSource = RemoveColum(ds);
                GrPlants.DataBind();
                MultiView1.SetActiveView(View2);
            }
            else if (ddlrpttype.Text.Trim() == "Purchase_Order_ReceivingDetails")
            {
                ds = objReports.BL_Report_Genrater("Purchase_Order_ReceivingDetails", strWhere, strQuery);
                GrPlants.DataSource = RemoveColum(ds);
                GrPlants.DataBind();
                MultiView1.SetActiveView(View2);
            }
            else if (ddlrpttype.Text.Trim() == "ManNo_Creation")
            {
                ds = objReports.BL_Report_Genrater("ManNo_Creation", strWhere, strQuery);
                GrPlants.DataSource = RemoveColum(ds);
                GrPlants.DataBind();
                MultiView1.SetActiveView(View2);
            }
            else if (ddlrpttype.Text.Trim() == "ManNo_Transaction(Barcode)")
            {
                ds = objReports.BL_Report_Genrater("ManNo_Transaction(Barcode)", strWhere, strQuery);
                GrPlants.DataSource = RemoveColum(ds);
                GrPlants.DataBind();
                MultiView1.SetActiveView(View2);
            }
            else if (ddlrpttype.Text.Trim() == "Allocation")
            {
                ds = objReports.BL_Report_Genrater("Allocation", strWhere, strQuery);
                GrPlants.DataSource = RemoveColum(ds);
                GrPlants.DataBind();
                MultiView1.SetActiveView(View2);
            }
            else if (ddlrpttype.Text.Trim() == "PickList")
            {
                ds = objReports.BL_Report_Genrater("PickList", strWhere, strQuery);
                GrPlants.DataSource = RemoveColum(ds);
                GrPlants.DataBind();
                MultiView1.SetActiveView(View2);
            }
            else if (ddlrpttype.Text.Trim() == "Reservation")
            {
                ds = objReports.BL_Report_Genrater("Reservation", strWhere, strQuery);
                GrPlants.DataSource = RemoveColum(ds);
                GrPlants.DataBind();
                MultiView1.SetActiveView(View2);
            }
            else if (ddlrpttype.Text.Trim() == "Dispensing")
            {
                ds = objReports.BL_Report_Genrater("Dispensing", strWhere, strQuery);
                GrPlants.DataSource = RemoveColum(ds);
                GrPlants.DataBind();
                MultiView1.SetActiveView(View2);
            }
            else if (ddlrpttype.Text.Trim() == "Material_Document(Dispensing_Posting)")
            {
                ds = objReports.BL_Report_Genrater("Material_Document(Dispensing_Posting)", strWhere, strQuery);
                GrPlants.DataSource = RemoveColum(ds);
                GrPlants.DataBind();
                MultiView1.SetActiveView(View2);
            }
            else if (ddlrpttype.Text.Trim() == "Dispensing_Posting_Error")
            {
                ds = objReports.BL_Report_Genrater("Dispensing_Posting_Error", strWhere, strQuery);
                GrPlants.DataSource = RemoveColum(ds);
                GrPlants.DataBind();
                MultiView1.SetActiveView(View2);
            }
            else if (ddlrpttype.Text.Trim() == "Sample_Transaction")
            {
                ds = objReports.BL_Report_Genrater("Sample_Transaction", strWhere, strQuery);
                GrPlants.DataSource = RemoveColum(ds);
                GrPlants.DataBind();
                MultiView1.SetActiveView(View2);
            }
            else if (ddlrpttype.Text.Trim() == "Weighing_Calibration_Month")
            {
                ds = objReports.BL_Report_Genrater("Weighing_Calibration_Month", strWhere, strQuery);
                GrPlants.DataSource = RemoveColum(ds);
                GrPlants.DataBind();
                MultiView1.SetActiveView(View2);
            }
            else if (ddlrpttype.Text.Trim() == "Weighing_Calibration_Daily")
            {
                ds = objReports.BL_Report_Genrater("Weighing_Calibration_Daily", strWhere, strQuery);
                GrPlants.DataSource = RemoveColum(ds);
                GrPlants.DataBind();
                MultiView1.SetActiveView(View2);
            }
            else if (ddlrpttype.Text.Trim() == "Scheduler_INSPECTIONLOT")
            {
                ds = objReports.BL_Report_Genrater("Scheduler_INSPECTIONLOT", strWhere, strQuery);
                GrPlants.DataSource = RemoveColum(ds);
                GrPlants.DataBind();
                MultiView1.SetActiveView(View2);
            }
            else if (ddlrpttype.Text.Trim() == "Scheduler_UDCODE")
            {
                ds = objReports.BL_Report_Genrater("Scheduler_UDCODE", strWhere, strQuery);
                GrPlants.DataSource = RemoveColum(ds);
                GrPlants.DataBind();
                MultiView1.SetActiveView(View2);
            }
            else if (ddlrpttype.Text.Trim() == "Scheduler_RETEST")
            {
                ds = objReports.BL_Report_Genrater("Scheduler_RETEST", strWhere, strQuery);
                GrPlants.DataSource = RemoveColum(ds);
                GrPlants.DataBind();
                MultiView1.SetActiveView(View2);
            }
            else if (ddlrpttype.Text == "Select")
            {
                clsStandards.WriteLog(this, new Exception("Please Select Report Name"), clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 0, true);
            }
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                btnPost.Visible = true;
                btnCancel.Visible = true;
            }
            else
            {
                btnPost.Visible = false;
                btnCancel.Visible = false;
            }
            
            ViewState["Query"] = strQuery;
            ViewState["Where"] = ""; 
        }
        catch (Exception ex)
        {
            clsStandards.WriteLog(this, ex, clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 0, true);
        }
        finally
        {
            strQuery = string.Empty;
        }
    }

    public DataSet RemoveColum(DataSet ds)
    {
        if (ds.Tables[0].Columns.Contains("DONE ON"))
        {
            ds.Tables[0].Columns.Remove("DONE ON");
            ds.AcceptChanges();
            return ds;
        }

        return ds;
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        MultiView1.SetActiveView(View1);
    }

    protected void GrPlants_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        string strWhere = "";
        DataSet ds = new DataSet();
        GrPlants.PageIndex = e.NewPageIndex;
        string strQuery = string.Empty;
        try
        {            
            lblModule.Text = ddlrpttype.Text.Trim().ToString();
            for (int j = 0; j < grSearch.Rows.Count; j++)
            {
                if (grSearch.Rows[j].Cells[1].Text == "datetime")
                {
                    if (((TextBox)grSearch.Rows[j].Cells[3].FindControl("txtFrom")).Text != "" || ((TextBox)grSearch.Rows[j].Cells[3].FindControl("txtTo")).Text != "")
                    {
                        if (strWhere == "")
                        {
                            strWhere = createDateCondition(grSearch.Rows[j].Cells[0].Text, ((TextBox)grSearch.Rows[j].Cells[3].FindControl("txtFrom")).Text, ((TextBox)grSearch.Rows[j].Cells[3].FindControl("txtTo")).Text);
                        }
                        else
                        {
                            strWhere = strWhere + "AND " + createDateCondition(grSearch.Rows[j].Cells[0].Text, ((TextBox)grSearch.Rows[j].Cells[3].FindControl("txtFrom")).Text, ((TextBox)grSearch.Rows[j].Cells[3].FindControl("txtTo")).Text);
                        }
                    }
                }
                else if (grSearch.Rows[j].Cells[1].Text == "date")
                {
                    if (((TextBox)grSearch.Rows[j].Cells[3].FindControl("txtFrom")).Text != "" || ((TextBox)grSearch.Rows[j].Cells[3].FindControl("txtTo")).Text != "")
                    {
                        if (strWhere == "")
                        {
                            strWhere = createDateCondition(grSearch.Rows[j].Cells[0].Text, ((TextBox)grSearch.Rows[j].Cells[3].FindControl("txtFrom")).Text, ((TextBox)grSearch.Rows[j].Cells[3].FindControl("txtTo")).Text);
                        }
                        else
                        {
                            strWhere = strWhere + "AND " + createDateCondition(grSearch.Rows[j].Cells[0].Text, ((TextBox)grSearch.Rows[j].Cells[3].FindControl("txtFrom")).Text, ((TextBox)grSearch.Rows[j].Cells[3].FindControl("txtTo")).Text);
                        }
                    }
                }
                else
                {
                    if (((TextBox)grSearch.Rows[j].Cells[3].FindControl("txtSearch")).Text != "")
                    {
                        if (strWhere == "")
                        {
                            strWhere = createCondition(grSearch.Rows[j].Cells[0].Text, ((DropDownList)grSearch.Rows[j].Cells[2].FindControl("cboCriteria")).SelectedValue, ((TextBox)grSearch.Rows[j].Cells[3].FindControl("txtSearch")).Text);
                        }
                        else
                        {
                            strWhere = strWhere + "AND " + createCondition(grSearch.Rows[j].Cells[0].Text, ((DropDownList)grSearch.Rows[j].Cells[2].FindControl("cboCriteria")).SelectedValue, ((TextBox)grSearch.Rows[j].Cells[3].FindControl("txtSearch")).Text);
                        }
                    }
                }
            }
            if (strWhere.Length != 0)
            {
                strWhere = "Where " + strWhere;
            }


            if (ddlrpttype.Text.Trim() == "User_Master")
            {
                ds = objReports.BL_Report_Genrater("User_Master", strWhere, strQuery);
                GrPlants.DataSource = RemoveColum(ds);
                GrPlants.DataBind();
            }
            else if (ddlrpttype.Text.Trim() == "Plant_Master")
            {
                ds = objReports.BL_Report_Genrater("Plant_Master", strWhere, strQuery);
                GrPlants.DataSource = RemoveColum(ds);
                GrPlants.DataBind();
            }
            else if (ddlrpttype.Text.Trim() == "Printer_Master")
            {
                ds = objReports.BL_Report_Genrater("Printer_Master", strWhere, strQuery);
                GrPlants.DataSource = RemoveColum(ds);
                GrPlants.DataBind();
            }
            else if (ddlrpttype.Text.Trim() == "Weighing_Master")
            {
                ds = objReports.BL_Report_Genrater("Weighing_Master", strWhere, strQuery);
                GrPlants.DataSource = RemoveColum(ds);
                GrPlants.DataBind();
            }
            else if (ddlrpttype.Text.Trim() == "Purchase_Order")
            {
                ds = objReports.BL_Report_Genrater("Purchase_Order", strWhere, strQuery);
                GrPlants.DataSource = RemoveColum(ds);
                GrPlants.DataBind();
            }
            else if (ddlrpttype.Text.Trim() == "Purchase_Order_Weight")
            {
                ds = objReports.BL_Report_Genrater("Purchase_Order_Weight", strWhere, strQuery);
                GrPlants.DataSource = RemoveColum(ds);
                GrPlants.DataBind();
            }
            else if (ddlrpttype.Text.Trim() == "Purchase_Order_GeneralDetails")
            {
                ds = objReports.BL_Report_Genrater("Purchase_Order_GeneralDetails", strWhere, strQuery);
                GrPlants.DataSource = RemoveColum(ds);
                GrPlants.DataBind();
            }
            else if (ddlrpttype.Text.Trim() == "Purchase_Order_ReceivingDetails")
            {
                ds = objReports.BL_Report_Genrater("Purchase_Order_ReceivingDetails", strWhere, strQuery);
                GrPlants.DataSource = RemoveColum(ds);
                GrPlants.DataBind();
            }
            else if (ddlrpttype.Text.Trim() == "ManNo_Creation")
            {
                ds = objReports.BL_Report_Genrater("ManNo_Creation", strWhere, strQuery);
                GrPlants.DataSource = RemoveColum(ds);
                GrPlants.DataBind();
            }
            else if (ddlrpttype.Text.Trim() == "ManNo_Transaction(Barcode)")
            {
                ds = objReports.BL_Report_Genrater("ManNo_Transaction(Barcode)", strWhere, strQuery);
                GrPlants.DataSource = RemoveColum(ds);
                GrPlants.DataBind();
            }
            else if (ddlrpttype.Text.Trim() == "Allocation")
            {
                ds = objReports.BL_Report_Genrater("Allocation", strWhere, strQuery);
                GrPlants.DataSource = RemoveColum(ds);
                GrPlants.DataBind();
            }
            else if (ddlrpttype.Text.Trim() == "PickList")
            {
                ds = objReports.BL_Report_Genrater("PickList", strWhere, strQuery);
                GrPlants.DataSource = RemoveColum(ds);
                GrPlants.DataBind();
            }
            else if (ddlrpttype.Text.Trim() == "Reservation")
            {
                ds = objReports.BL_Report_Genrater("Reservation", strWhere, strQuery);
                GrPlants.DataSource = RemoveColum(ds);
                GrPlants.DataBind();
            }
            else if (ddlrpttype.Text.Trim() == "Dispensing")
            {
                ds = objReports.BL_Report_Genrater("Dispensing", strWhere, strQuery);
                GrPlants.DataSource = RemoveColum(ds);
                GrPlants.DataBind();
            }
            else if (ddlrpttype.Text.Trim() == "Material_Document(Dispensing_Posting)")
            {
                ds = objReports.BL_Report_Genrater("Material_Document(Dispensing_Posting)", strWhere, strQuery);
                GrPlants.DataSource = RemoveColum(ds);
                GrPlants.DataBind();
            }
            else if (ddlrpttype.Text.Trim() == "Dispensing_Posting_Error")
            {
                ds = objReports.BL_Report_Genrater("Dispensing_Posting_Error", strWhere, strQuery);
                GrPlants.DataSource = RemoveColum(ds);
                GrPlants.DataBind();
                MultiView1.SetActiveView(View2);
            }
            else if (ddlrpttype.Text.Trim() == "Sample_Transaction")
            {
                ds = objReports.BL_Report_Genrater("Sample_Transaction", strWhere, strQuery);
                GrPlants.DataSource = RemoveColum(ds);
                GrPlants.DataBind();
            }
            else if (ddlrpttype.Text.Trim() == "Weighing_Calibration_Month")
            {
                ds = objReports.BL_Report_Genrater("Weighing_Calibration_Month", strWhere, strQuery);
                GrPlants.DataSource = RemoveColum(ds);
                GrPlants.DataBind();
            }
            else if (ddlrpttype.Text.Trim() == "Weighing_Calibration_Daily")
            {
                ds = objReports.BL_Report_Genrater("Weighing_Calibration_Daily", strWhere, strQuery);
                GrPlants.DataSource = RemoveColum(ds);
                GrPlants.DataBind();
            }
            else if (ddlrpttype.Text.Trim() == "Scheduler_INSPECTIONLOT")
            {
                ds = objReports.BL_Report_Genrater("Scheduler_INSPECTIONLOT", strWhere, strQuery);
                GrPlants.DataSource = RemoveColum(ds);
                GrPlants.DataBind();
            }
            else if (ddlrpttype.Text.Trim() == "Scheduler_UDCODE")
            {
                ds = objReports.BL_Report_Genrater("Scheduler_UDCODE", strWhere, strQuery);
                GrPlants.DataSource = RemoveColum(ds);
                GrPlants.DataBind();
            }
            else if (ddlrpttype.Text.Trim() == "Scheduler_RETEST")
            {
                ds = objReports.BL_Report_Genrater("Scheduler_RETEST", strWhere, strQuery);
                GrPlants.DataSource = RemoveColum(ds);
                GrPlants.DataBind();
            }
            else if (ddlrpttype.Text == "Select")
            {
                clsStandards.WriteLog(this, new Exception("Please Select Report Type"), clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 0, true);
            }
            ViewState["Query"] = strQuery;
            ViewState["Where"] = strWhere;
        }
        catch (Exception ex)
        {
            clsStandards.WriteLog(this, ex, clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 0, true);
        }
    }

    private string createCondition(string strColumn, string strPattern, string strValue)
    {
        string strReturn = "";
        //strColumn = "\"" + strColumn + "\"";
        if (strValue != "")
        {
            if (strPattern == "=")
            {
                strReturn = "[" + strColumn + "] = '" + strValue + "'";
            }
            else if (strPattern == "<>")
            {
                strReturn = "[" + strColumn + "] <> '" + strValue + "'";
            }
            else if (strPattern == "like")
            {
                strReturn = "[" + strColumn + "] like " + "'%" + strValue + "%'";
            }
            else if (strPattern == "likestart")
            {
                strReturn = "[" + strColumn + "] like '" + strValue + "%'";
            }
            else if (strPattern == "likeend")
            {
                strReturn = "[" + strColumn + "] like " + "'%" + strValue + "'";
            }
        }
        return strReturn;
    }

    private string createDateCondition(string strColumn, string dtstart, string dtEnd)
    {
        //return strColumn + " = " + strValue;
        string dtcon = "";
        if (dtstart.Trim() != "")
            dtcon = "CONVERT(Datetime, \"" + strColumn + "\", 120) >= CONVERT(Datetime, REPLACE('" + dtstart + "', ' 12:00:00 AM', ''), 120)";
        if (dtEnd.Trim() != "")
        {
            if (dtcon.Trim() == "")
                dtcon = dtcon + " CONVERT(Datetime, \"" + strColumn + "\", 120) <=  CONVERT(Datetime, REPLACE('" + dtEnd + "', ' 12:00:00 AM', ''), 120)+1";
            else
                dtcon = dtcon + "AND CONVERT(Datetime, \"" + strColumn + "\", 120) <=  CONVERT(Datetime, REPLACE('" + dtEnd + "', ' 12:00:00 AM', ''), 120)+1";
        }
        return dtcon;
    }

    protected void GrPlants_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (ddlrpttype.Text.Trim() == "Bin Stock Report")
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //PackageNo = PackageNo + 1;
                intPreviousRowID = Convert.ToString(e.Row.Cells[2].Text);
                intPreviousBatch = Convert.ToString(e.Row.Cells[4].Text);
                intPreviousBin = Convert.ToString(e.Row.Cells[7].Text);
                double dblDirectRevenue = Convert.ToDouble(e.Row.Cells[5].Text.ToString());

                dblSubTotalDirectRevenue += dblDirectRevenue;

                dblGrandTotalDirectRevenue += dblDirectRevenue;
            }
            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                PackageNo = 1;
            }
        }





    }

    protected void btnClosePage_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Main/frmMain.aspx");
    }

    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Verifies that the control is rendered */
    }

    protected void GrPlants_RowCreated(object sender, GridViewRowEventArgs e)
    {

        //e.Row.Cells[8].Visible = false;

        //if (ddlrpttype.Text.Trim() == "Bin Stock Report")
        //{
        //    bool IsTotalRowNeedToAdd = false;

        //    if (((intPreviousRowID != string.Empty) && (DataBinder.Eval(e.Row.DataItem, "Material Code") != null)) || ((intPreviousBatch != string.Empty) && (DataBinder.Eval(e.Row.DataItem, "Batch") != null)) || ((intPreviousBin != string.Empty) && (DataBinder.Eval(e.Row.DataItem, "Bin") != null)))
        //        if (intPreviousRowID != DataBinder.Eval(e.Row.DataItem, "Material Code").ToString() || intPreviousBatch != DataBinder.Eval(e.Row.DataItem, "Batch").ToString() || intPreviousBin != DataBinder.Eval(e.Row.DataItem, "Bin").ToString())

        //        { IsTotalRowNeedToAdd = true; }

        //    if (((intPreviousRowID != string.Empty) && (DataBinder.Eval(e.Row.DataItem, "Material Code") == null)) || ((intPreviousBatch != string.Empty) && (DataBinder.Eval(e.Row.DataItem, "Batch") == null)) || ((intPreviousBin != string.Empty) && (DataBinder.Eval(e.Row.DataItem, "Bin") == null)))
        //    {
        //        IsTotalRowNeedToAdd = true;
        //        intSubTotalIndex = 0;
        //    }

        //    if (IsTotalRowNeedToAdd)
        //    {
        //        GridView grdViewProducts = (GridView)sender;

        //        // Creating a Row
        //        GridViewRow SubTotalRow = new GridViewRow(0, 0, DataControlRowType.DataRow, DataControlRowState.Insert);

        //        //Adding Total Cell 
        //        TableCell HeaderCell = new TableCell();
        //        HeaderCell.Text = "Total Stock Against Material : " + intPreviousRowID + ", Batch : " + intPreviousBatch + ", Bin : " + intPreviousBin;
        //        HeaderCell.HorizontalAlign = HorizontalAlign.Left;
        //        HeaderCell.ColumnSpan = 5; // For merging first, second row cells to one
        //        HeaderCell.CssClass = "selectedtab";
        //        SubTotalRow.Cells.Add(HeaderCell);

        //        //Adding Total Revenue Column
        //        HeaderCell = new TableCell();
        //        HeaderCell.Text = string.Format("{0:0.00} ", +dblSubTotalDirectRevenue);
        //        HeaderCell.HorizontalAlign = HorizontalAlign.Left;
        //        HeaderCell.ColumnSpan = 5;
        //        HeaderCell.CssClass = "selectedtab";
        //        SubTotalRow.Cells.Add(HeaderCell);

        //        //Adding the Row at the RowIndex position in the Grid
        //        if (PackageNo == 0)
        //        {
        //            grdViewProducts.Controls[0].Controls.AddAt(e.Row.RowIndex + intSubTotalIndex, SubTotalRow);
        //            intSubTotalIndex++;
        //            dblSubTotalDirectRevenue = 0;
        //        }
        //        //dblSubTotalReferralRevenue = 0;
        //        //dblSubTotalTotalRevenue = 0;
        //        //PackageNo = 0;
        //    }
        //}
    }

    protected void GrPlants_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Modules/frmMain.aspx");
    }

    protected void btnPost_Click(object sender, EventArgs e)
    {
        try
        {
            string strFilename = ddlrpttype.SelectedValue + DateTime.Now;
            clsxlsExport.ExportPDF(GrPlants, strFilename);
        }
        catch (Exception ex)
        {
            throw ex;
        }

     }

    protected void BtnExcel_Click(object sender, EventArgs e)
    {
        try
        {
            string strFilename = ddlrpttype.SelectedValue + DateTime.Now;
            clsxlsExport.ExportExcel(GrPlants, strFilename);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
}