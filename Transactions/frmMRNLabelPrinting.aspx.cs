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
using System.Globalization;

public partial class frmMRNLabelPrinting : System.Web.UI.Page
{

    public static string strFlg = "";
    DataTable dtImport = new DataTable();
    BL_SamplePrinting objBL = new BL_SamplePrinting();
    DataTable dtPRint = new DataTable();
    BL_GrnPrinting objGrn = new BL_GrnPrinting();
    BL_PrinterMaster objPrint = new BL_PrinterMaster();
    BL_Dispensing objdis = new BL_Dispensing();
    DataTable dt = new DataTable();
    DataTable dt_Plant = new DataTable();
    DataTable dt_AscPlant = new DataTable();
    DataSet ds = new DataSet();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            objPrint = new BL_PrinterMaster();

            try
            {
                clsStandards.fillCombobox(ddlPrinterName, objPrint.BL_PrinterCodes(clsStandards.current_Plant()), "PRINTERCODE");
                MaterialData.Visible = false;
            }
            catch (Exception ex)
            {
                clsStandards.WriteLog(this, ex, clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 0, true);
            }
            finally
            {
                objPrint = null;
            }
            //.SetActiveView(View1);
            strFlg = "ADD";
        }

    }

    protected void btnGet_Click(object sender, EventArgs e)
    {
        Clear();
        objBL = new BL_SamplePrinting();
        DataTable dtGrn = new DataTable();
        objdis = new BL_Dispensing();
        PL_ProcessReservation objPO = new PL_ProcessReservation();
        ddlMaterialCode.Items.Insert(0, "Select");
        ddlMaterialBatch.Items.Insert(0, "Select");
        lblTotalQuantity.Items.Insert(0, "Select");
        lblUom.Items.Insert(0, "Select");
        try
        {
            if (!chkMRN.Checked && !chkINTERMEDIATE.Checked && !chkCodetocode.Checked)
            {
                return;
            }

            MRN_WS.z_ws_mrn _MRN = new MRN_WS.z_ws_mrn();
            MRN_WS.ZDMF009 _InputParameters = new MRN_WS.ZDMF009();
            MRN_WS.ZDMF009Response _Response = new MRN_WS.ZDMF009Response();
            MRN_WS.ZDBMS017[] _OutputParameters = new MRN_WS.ZDBMS017[0];
            _InputParameters.RETURN = new MRN_WS.BAPIRET2[1];
            _InputParameters.RETURN[0] = new MRN_WS.BAPIRET2();

            _MRN.Proxy = new WebProxy();
            _MRN.Credentials = new NetworkCredential(System.Configuration.ConfigurationManager.AppSettings["SapUser"].ToString(), System.Configuration.ConfigurationManager.AppSettings["SapPass"].ToString());
            _MRN.Proxy.Credentials = new NetworkCredential(System.Configuration.ConfigurationManager.AppSettings["SapUser"].ToString(), System.Configuration.ConfigurationManager.AppSettings["SapPass"].ToString());


            _InputParameters.IM_MAT_DOC = txtMaterialDoc.Text.Trim();
            _InputParameters.IM_WERKS = clsStandards.current_Plant();
            _InputParameters.RETURN[0].FIELD = String.Empty;
            _InputParameters.RETURN[0].ID = String.Empty;
            _InputParameters.RETURN[0].LOG_MSG_NO = String.Empty;
            _InputParameters.RETURN[0].LOG_NO = String.Empty;
            _InputParameters.RETURN[0].MESSAGE = String.Empty;
            _InputParameters.RETURN[0].MESSAGE_V1 = String.Empty;
            _InputParameters.RETURN[0].MESSAGE_V2 = String.Empty;
            _InputParameters.RETURN[0].MESSAGE_V3 = String.Empty;
            _InputParameters.RETURN[0].MESSAGE_V4 = String.Empty;
            _InputParameters.RETURN[0].NUMBER = String.Empty;
            _InputParameters.RETURN[0].PARAMETER = String.Empty;
            _InputParameters.RETURN[0].ROW = 0;
            _InputParameters.RETURN[0].SYSTEM = String.Empty;
            _InputParameters.RETURN[0].TYPE = String.Empty;

            var req = new MemoryStream();
            var reqxml = new System.Xml.Serialization.XmlSerializer(_InputParameters.GetType());
            reqxml.Serialize(req, _InputParameters);
            string strxml = Encoding.UTF8.GetString(req.ToArray());
            clsStandards.WriteSoapXML(strxml, "MRN - Request", clsStandards.GetCurrentPageName().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());

            _Response = _MRN.ZDMF009(_InputParameters);
            _OutputParameters = _Response.EX_FINAL;

            var res = new MemoryStream();
            var resxml = new System.Xml.Serialization.XmlSerializer(_Response.GetType());
            resxml.Serialize(res, _Response);
            string xml = Encoding.UTF8.GetString(res.ToArray());
            clsStandards.WriteSoapXML(xml, "MRN - Response", clsStandards.GetCurrentPageName().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());


            for (int i = 0; i < _OutputParameters.Length; i++)
            {
                MaterialData.Visible = true;
                ddlMaterialCode.Items.Insert(i + 1, new ListItem(_OutputParameters[i].RAW_MATERIAL_CODE, (i + 1).ToString()));
                ddlMaterialBatch.Items.Insert(i + 1, new ListItem(_OutputParameters[i].RAW_MATERIAL_BATCH, (i + 1).ToString()));
                lblTotalQuantity.Items.Insert(i + 1, new ListItem(_OutputParameters[i].QUANTITY.ToString(), (i + 1).ToString()));
                lblUom.Items.Insert(i + 1, new ListItem(_OutputParameters[i].UOM, (i + 1).ToString()));

            }
        }
        catch (Exception ex)
        {
            clsStandards.WriteLog(this, ex, clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 0, true);

        }
        finally
        {
            objBL = null;
            dtGrn = null;
            objdis = null;
            objPO = null;
        }

    }

    protected void ddlMaterialBatch_SelectedIndexChanged(object sender, EventArgs e)
    {
        objdis = new BL_Dispensing();
        DataTable dtGrn = new DataTable();
        objdis = new BL_Dispensing();
        PL_ProcessReservation objPO = new PL_ProcessReservation();
        try
        {
            dtGrn = objBL.BL_GetMaterialDoc_Data(txtMaterialDoc.Text.Trim(), ddlMaterialCode.SelectedItem.Text, ddlMaterialBatch.SelectedItem.Text, clsStandards.current_Plant());
            if (dtGrn.Rows.Count > 0)
            {
                lblProductCode.Text = dtGrn.Rows[0]["ProductCode"].ToString();
                lblProdDescription.Text = dtGrn.Rows[0]["ProductDesc"].ToString();
                lblARNo.Text = dtGrn.Rows[0]["ARNo"].ToString();
                lblStrgLoc.Text = dtGrn.Rows[0]["STORAGELOC"].ToString();
                lblTotalQuantity.Text = dtGrn.Rows[0]["Quantity"].ToString();
                lblUom.Text = dtGrn.Rows[0]["UOM"].ToString();
            }
            else
            {
                //clsStandards.WriteLog(this, new Exception("No details found for AR no."), clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 0, true);
                lblProductCode.Text = "";
                lblProdDescription.Text = "";
                lblARNo.Text = "";
                lblStrgLoc.Text = "";
            }
        }
        catch (Exception ex)
        {
        }
        finally
        {
            objBL = null;
            dtGrn = null;
            objdis = null;
            objPO = null;
        }
    }

    protected void chkMRN_CheckedChanged(object sender, EventArgs e)
    {
        txtNoofContainers.Enabled = false;
        if (chkINTERMEDIATE.Checked)
        {
            chkINTERMEDIATE.Checked = false;
        }
        if (chkCodetocode.Checked)
        {
            chkCodetocode.Checked = false;
        }
    }

    protected void chkINTERMEDIATE_CheckedChanged(object sender, EventArgs e)
    {
        txtNoofContainers.Enabled = true;
        if (chkMRN.Checked)
        {
            chkMRN.Checked = false;
        }
        if (chkCodetocode.Checked)
        {
            chkCodetocode.Checked = false;
        }
    }

    private void InsertData()
    {
        objdis = new BL_Dispensing();
        DataTable dtGrn = new DataTable();
        objdis = new BL_Dispensing();
        PL_ProcessReservation objPO = new PL_ProcessReservation();
        try
        {

            objPO.strItemCode = ddlMaterialCode.SelectedItem.ToString();
            objPO.strSAPBatchNo = ddlMaterialBatch.SelectedItem.ToString();
            objPO.strQuantity = Convert.ToDecimal(lblTotalQuantity.SelectedItem.ToString());
            objPO.strUOM = lblUom.SelectedItem.ToString();
            objPO.strProductCode = lblProductCode.Text.Trim();
            objPO.strProductDesc = lblProdDescription.Text.Trim();
            objPO.strRMQuantity = Convert.ToDecimal(lblTotalQuantity.SelectedItem.ToString());
            objPO.strRMUOM = lblUom.SelectedItem.ToString();
            objPO.strARNo = lblARNo.Text.Trim();
            objPO.strSTORAGELOC = lblStrgLoc.Text.Trim();
            objPO.strBinCode = "";
            objPO.strCostCenter = "";
            objPO.strGM_Code = "03";
            objPO.strUser = clsStandards.current_Username();
            objPO.strMaterialDocumentNo = txtMaterialDoc.Text.Trim();
            objPO.strPlant = clsStandards.current_Plant();
            objPO.strProcess_Reservation_No = "";
            objPO.strLineItemNo = "";
            objPO.strManufacture = txtManufacture.Text.Trim();
            objPO.strSupplier = txtSupplier.Text.Trim();
            objPO.strVendor = txtVendor.Text.Trim();
            objPO.strResult = "SUCCESS";
            if (chkMRN.Checked)
            {
                objPO.strType = "MRN";
            }
            else if (chkINTERMEDIATE.Checked)
            {
                objPO.strType = "INT";
            }
            else
            {
                objPO.strType = "TRANS";
            }

            string result = objdis.BL_Insert_MaterialDoc(objPO);
        }
        catch (Exception ex)
        {
            clsStandards.WriteLog(this, new Exception(ex.Message), clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 0, true);
        }
        finally
        {

        }
    }
    protected void btnPrint_Click(object sender, EventArgs e)
    {
        DataTable dtPRint = new DataTable();
        objBL = new BL_SamplePrinting();
        BL_GrnPrinting objGrn = new BL_GrnPrinting();
        bool flag = true;
        string value = "";
        try
        {
            if (chkMRN.Checked)
            {
                value = "MRN";
            }
            else if (chkINTERMEDIATE.Checked)
            {
                value = "INT";
            }
            else
            {
                value = "TRANS";
            }
            if (ddlMaterialCode.Text.Trim().ToUpper().Contains("SELECT"))
            {
                clsStandards.WriteLog(this, new Exception("Please select Item code"), clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 0, true);
                return;
            }
            InsertData();
            if (ddlPrinterName.Text.Trim().ToUpper().Contains("SELECT"))
            {
                clsStandards.WriteLog(this, new Exception("Please select printer"), clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 0, true);
                return;
            }
            clsStandards.WriteLog(this, new Exception("started"), clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 0, false);

            DateTime Mfgdt = new DateTime();
            DateTime ExpDate = new DateTime();
            if (!string.IsNullOrEmpty(txtmfgdate.Text))
            {
                Mfgdt = DateTime.ParseExact(txtmfgdate.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture);
            }
            else
            {
                Mfgdt = DateTime.ParseExact("01-01-1900", "dd-MM-yyyy", CultureInfo.InvariantCulture);
            }
            if (!string.IsNullOrEmpty(txtmfgdate.Text))
            {
                ExpDate = DateTime.ParseExact(txtexpate.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture);
            }
            else
            {
                ExpDate = DateTime.ParseExact("01-01-1900", "dd-MM-yyyy", CultureInfo.InvariantCulture);
            }

            dtPRint = objBL.BL_GetMRN_BarcodeNo(txtMaterialDoc.Text.Trim(), ddlMaterialCode.SelectedItem.Text.Trim(), ddlMaterialBatch.SelectedItem.Text.Trim(), lblARNo.Text.Trim(),
                lblTotalQuantity.SelectedItem.ToString().Trim(), lblProductCode.Text.Trim(), lblProdDescription.Text.Trim(), txtNoofContainers.Text.Trim(), txtContainerQty.Text.Trim(),
                clsStandards.current_Plant().ToString(), clsStandards.current_Username().ToString(), "PRINT", "", "", lblStrgLoc.Text.Trim(),
                lblUom.SelectedItem.ToString().ToUpper() == "KG" || lblUom.SelectedItem.ToString().ToUpper() == "G" ? Convert.ToDecimal(lblTotalQuantity.SelectedItem.ToString().Trim()) : 0,
                lblUom.SelectedItem.ToString().ToUpper() == "KG" || lblUom.SelectedItem.ToString().ToUpper() == "G" ? 0 : 0,
                lblUom.SelectedItem.ToString().ToUpper() == "KG" || lblUom.SelectedItem.ToString().ToUpper() == "G" ? 0 : 0,
                value, Mfgdt, ExpDate);

            clsStandards.WriteLog(this, new Exception("ended"), clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 0, false);

            string strPrinter = objGrn.BL_GetPrinterIP(ddlPrinterName.Text.Trim(), clsStandards.current_Plant());
            if (PrintDirect.CheckConnection(strPrinter.Split('=').GetValue(0).ToString(), strPrinter.Split('=').GetValue(1).ToString()) == false)
            {
                clsStandards.WriteLog(this, new Exception("Printed IP not Connected"), clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 0, true);
                return;
            }

            flag = PrintDirect.Print_MRNLabel(ddlPrinterName.Text.Trim(), clsStandards.current_User().ToString(), System.DateTime.Now.ToString("yyyy-MM-dd"),
                dtPRint, strPrinter.Split('=').GetValue(0).ToString(), strPrinter.Split('=').GetValue(1).ToString(), value);
            if (flag == true)
            {
                Clear();
                txtMaterialDoc.Text = "";
                clsStandards.WriteLog(this, new Exception("Barcode Printed Successfully"), clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 0, true);
            }

        }
        catch (Exception ex)
        {
            clsStandards.WriteLog(this, new Exception(ex.Message), clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 0, true);
        }
        finally
        {
            dtPRint = null;
        }
    }


    private void Clear()
    {
        lblARNo.Text = "";
        //txtNoofContainers.Text = "";
        txtContainerQty.Text = "";
        lblStrgLoc.Text = "";
        lblProductCode.Text = "";
        lblProdDescription.Text = "";
        ddlMaterialCode.Items.Clear();
        ddlMaterialBatch.Items.Clear();
        lblTotalQuantity.Items.Clear();
        lblUom.Items.Clear();
        txtmfgdate.Text = "";
        txtexpate.Text = "";
        txtSupplier.Text = "";
        txtVendor.Text = "";
        txtManufacture.Text = "";
        ddlPrinterName.SelectedIndex = 0;
    }

    protected void btnClose_Click(object sender, EventArgs e)
    {
        try
        {
            HttpContext.Current.Response.Redirect(ResolveUrl("~/Modules/frmMain.aspx"), false);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.ToString());
        }
        finally
        {

        }
    }

    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Verifies that the control is rendered */
    }

    protected void chkCodetocode_CheckedChanged(object sender, EventArgs e)
    {
        txtNoofContainers.Enabled = true;
        if (chkMRN.Checked)
        {
            chkMRN.Checked = false;
        }
        if (chkINTERMEDIATE.Checked)
        {
            chkINTERMEDIATE.Checked = false;
        }
    }
}