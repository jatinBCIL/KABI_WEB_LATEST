using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using BusinessLayer;
using PropertyLayer;
using System.Net;
using System.Globalization;
using System.Configuration;
using System.IO;
using System.Text;

public partial class frmDispensingPosting : System.Web.UI.Page
{
    BL_Dispensing objBL = new BL_Dispensing();
    PL_ProcessReservation objPl = new PL_ProcessReservation();
    DataTable dt = new DataTable();

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            MultiView1.SetActiveView(View1);
            DivHeader.Visible = false;
            ViewState["data"] = null;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.ToString());
        }
        finally
        {

        }
    }
    protected void chkPONo_CheckedChanged(object sender, EventArgs e)
    {
        lblDocumentNo.Text = "Process Order No :";
        if (chkReservation.Checked)
        {
            chkReservation.Checked = false;
            if (!string.IsNullOrEmpty(txtDocumentNo.Text.Trim()))
            {
                txtDocumentNo.Text = "";
            }
            if (!string.IsNullOrEmpty(txtSapUser.Text.Trim()))
            {
                txtSapUser.Text = "";
            }
            if (!string.IsNullOrEmpty(txtSapPass.Text.Trim()))
            {
                txtSapPass.Text = "";
            }
            GrDispenDataRe.Visible = false;
            GrDispenDataPO.Visible = false;
        }

    }

    protected void chkReservation_CheckedChanged(object sender, EventArgs e)
    {
        if (chkPONo.Checked)
        {
            lblDocumentNo.Text = "Reservation No :";
            chkPONo.Checked = false;
            if (!string.IsNullOrEmpty(txtDocumentNo.Text.Trim()))
            {
                txtDocumentNo.Text = "";
            }
            if (!string.IsNullOrEmpty(txtSapUser.Text.Trim()))
            {
                txtSapUser.Text = "";
            }
            if (!string.IsNullOrEmpty(txtSapPass.Text.Trim()))
            {
                txtSapPass.Text = "";
            }
            GrDispenDataRe.Visible = false;
            GrDispenDataPO.Visible = false;
        }

    }
    protected void btnGet_Click(object sender, EventArgs e)
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        string strvalue = "";
        dynamic strCheck = "";
        try
        {
            if ((chkPONo.Checked != true) && (chkReservation.Checked != true))
            {
                clsStandards.WriteLog(this, new Exception(" Please select Process Order No / Reservation No "), clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 0, true);
                return;
            }
            else if (string.IsNullOrEmpty(txtDocumentNo.Text.Trim()))
            {
                clsStandards.WriteLog(this, new Exception(" Please Enter Process Order No / Reservation No "), clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 0, true);
                txtDocumentNo.Focus();
                return;
            }

            ///Process order
            if (chkPONo.Checked)
            {
                strvalue = txtDocumentNo.Text.Trim();
                strCheck = strvalue[0];
                if (strCheck.ToString() != "9")
                {
                    clsStandards.WriteLog(this, new Exception(" Processor order number start with 9 "), clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 0, true);
                    txtDocumentNo.Focus();
                    return;
                }
                else
                {
                    txtDocumentNo.Text = "0" + txtDocumentNo.Text.Trim();
                }
                ds = objBL.DL_GetDispPost_Data(clsStandards.current_Plant(), txtDocumentNo.Text.Trim(), "PROCESSORDER");
                if (ds.Tables[0].Rows.Count > 0)
                {
                    dt = ds.Tables[0];

                    if (dt.Rows.Count > 0)
                    {
                        string strHeader = dt.Rows[0]["HeaderText"].ToString();
                        if (string.IsNullOrEmpty(strHeader))
                        {
                            DivHeader.Visible = true;
                        }
                        else
                        {
                            DivHeader.Visible = false;
                        }

                        GrDispenDataRe.Visible = false;
                        GrDispenDataRe.DataSource = null;
                        GrDispenDataPO.Visible = true;
                        GrDispenDataPO.DataSource = dt;
                        GrDispenDataPO.DataBind();
                        ViewState["data"] = dt;
                    }
                    else
                    {
                        GrDispenDataPO.DataSource = null;
                        GrDispenDataPO.DataBind();
                        clsStandards.WriteLog(this, new Exception(" No data found / Posting done "), clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 0, true);
                    }
                }

            }

            ///Reservation Data
            else if (chkReservation.Checked)
            {
                strvalue = txtDocumentNo.Text.Trim();
                strCheck = strvalue[0];
                if (strCheck.ToString() == "9")
                {
                    clsStandards.WriteLog(this, new Exception(" Reservation order number start with 0 "), clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 0, true);
                    txtDocumentNo.Focus();
                    return;
                }
                ds = objBL.DL_GetDispPost_Data(clsStandards.current_Plant(), txtDocumentNo.Text.Trim(), "RESERVATION");
                if (ds.Tables[0].Rows.Count > 0)
                {
                    dt = ds.Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        string strHeader = dt.Rows[0]["HeaderText"].ToString();
                        if (string.IsNullOrEmpty(strHeader))
                        {
                            DivHeader.Visible = true;
                        }
                        else
                        {
                            DivHeader.Visible = false;
                        }
                        GrDispenDataPO.Visible = false;
                        GrDispenDataPO.DataSource = null;
                        GrDispenDataRe.Visible = true;
                        GrDispenDataRe.DataSource = dt;
                        GrDispenDataRe.DataBind();
                        ViewState["data"] = dt;
                    }
                    else
                    {
                        GrDispenDataRe.DataSource = null;
                        GrDispenDataRe.DataBind();
                        clsStandards.WriteLog(this, new Exception("  No data found / Posting done  "), clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 0, true);
                    }
                }

            }
            else
            {

            }

        }
        catch (Exception ex)
        {
            clsStandards.WriteLog(this, new Exception(ex.Message), clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 0, true);
        }
    }

    protected void btnPost_Click(object sender, EventArgs e)
    {
        objPl = new PL_ProcessReservation();
        objBL = new BL_Dispensing();
        int Ist = 0;
        string mno = "";
        string message = "";
        try
        {
            if (string.IsNullOrEmpty(txtSapUser.Text.Trim()))
            {
                clsStandards.WriteLog(this, new Exception(" Please Enter SAP Username "), clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 0, true);
                txtSapUser.Focus();
                return;
            }
            else if (string.IsNullOrEmpty(txtSapPass.Text.Trim()))
            {
                clsStandards.WriteLog(this, new Exception(" Please Enter SAP User Password "), clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 0, true);
                txtSapPass.Focus();
                return;
            }

            #region Process Order data Post
            if (chkPONo.Checked == true)
            {
                WsDispen_PO.z_ws_process_order_string _PO_No = new WsDispen_PO.z_ws_process_order_string();
                WsDispen_PO.ZDMF008 _InputParameters = new WsDispen_PO.ZDMF008();
                WsDispen_PO.ZDMF008Response _Response = new WsDispen_PO.ZDMF008Response();
                WsDispen_PO.ZDBMS012[] _OutputParameters1 = new WsDispen_PO.ZDBMS012[0];
                WsDispen_PO.ZDBMS015[] _OutputParameters2 = new WsDispen_PO.ZDBMS015[0];
                WsDispen_PO.ZDBMS016[] _OutputParameters3 = new WsDispen_PO.ZDBMS016[0];
                WsDispen_PO.BAPIRET2[] _ReturnParameter = new WsDispen_PO.BAPIRET2[0];
                _InputParameters.IM_HEADER = new WsDispen_PO.ZDBMS015();

                _PO_No.Proxy = new WebProxy();
                _PO_No.Credentials = new NetworkCredential(txtSapUser.Text.Trim(), txtSapPass.Text.Trim());
                _PO_No.Proxy.Credentials = new NetworkCredential(txtSapUser.Text.Trim(), txtSapPass.Text.Trim());

                int TotalCheckboxPO = 0;
                for (int i = 0; i < GrDispenDataPO.Rows.Count; i++)
                {
                    CheckBox cb = (CheckBox)GrDispenDataPO.Rows[i].FindControl("chkSelect");
                    if (cb.Checked == true)
                    {
                        TotalCheckboxPO++;
                    }
                }
                _InputParameters.T_ITEM = new WsDispen_PO.ZDBMS012[TotalCheckboxPO];
                _InputParameters.RETURN = new WsDispen_PO.BAPIRET2[TotalCheckboxPO];
                _ReturnParameter = new WsDispen_PO.BAPIRET2[TotalCheckboxPO];
                
               
                #region parameters
                _InputParameters.IM_HEADER.AIRWAY_BILL_DATE = "";
                _InputParameters.IM_HEADER.AIRWAY_BILL_NO = "";
                _InputParameters.IM_HEADER.BILL_OF_LADING = "";
                _InputParameters.IM_HEADER.BILLDAT = "";
                _InputParameters.IM_HEADER.BILLNO = "";
                _InputParameters.IM_HEADER.DELIVERY_NOTE = "";
                _InputParameters.IM_HEADER.DOC_DATE = System.DateTime.Now.ToString("yyyy-MM-dd");
                _InputParameters.IM_HEADER.EXCISE_GROUP = "";
                _InputParameters.IM_HEADER.EXCISE_INVOICE = "";
                _InputParameters.IM_HEADER.EXCISE_INVOICED_DATE = "";
                _InputParameters.IM_HEADER.FNUM = "";
                _InputParameters.IM_HEADER.GATE_ENTRY_NO = "";
                _InputParameters.IM_HEADER.GM_CODE = "03";
                _InputParameters.IM_HEADER.GR_GI_SLIP_NO = "";
                _InputParameters.IM_HEADER.GR_RR_NO = "";
                _InputParameters.IM_HEADER.GROSS_WT = "";
                _InputParameters.IM_HEADER.HEADER_TEXT = "";
                _InputParameters.IM_HEADER.LIC_NO = "";
                _InputParameters.IM_HEADER.MAN_TAX = "";
                _InputParameters.IM_HEADER.MODE_OF_TRANSPORT = "";
                _InputParameters.IM_HEADER.NET_WT = "";
                _InputParameters.IM_HEADER.NO_OF_PACKS = "";
                _InputParameters.IM_HEADER.PLANT = clsStandards.current_Plant();
                _InputParameters.IM_HEADER.POSTING_DATE = System.DateTime.Now.ToString("yyyy-MM-dd");
                _InputParameters.IM_HEADER.PROCES_ORDER_NUMBER = txtDocumentNo.Text.Trim();
                _InputParameters.IM_HEADER.PURCHASE_ORDER_NO = "";
                _InputParameters.IM_HEADER.SALTAX = "";
                _InputParameters.IM_HEADER.TARE_WT = "";
                _InputParameters.IM_HEADER.TDATE = System.DateTime.Now.ToString("yyyy-MM-dd");
                _InputParameters.IM_HEADER.TR6 = "";
                _InputParameters.IM_HEADER.TRANSPORTER = "";
                _InputParameters.IM_HEADER.TRDAT = "";
                _InputParameters.IM_HEADER.TRUCK = "";
                _InputParameters.IM_HEADER.UNAME = "";
                _InputParameters.IM_HEADER.WAY_BILL_NOT_REQUIRED = "X";
                _InputParameters.IM_HEADER.WAY_DT = "";
                _InputParameters.IM_HEADER.WAY_REQ = "";
                #endregion

                int k = -1;
                if (GrDispenDataPO.Rows.Count > 0)
                {
                    for (int i = 0; i < GrDispenDataPO.Rows.Count; i++)
                    {
                        CheckBox cb = (CheckBox)GrDispenDataPO.Rows[i].FindControl("chkSelect");
                        if (cb.Checked == true)
                        {
                             k++;

                             #region
                             _InputParameters.T_ITEM[k] = new WsDispen_PO.ZDBMS012();
                             _InputParameters.T_ITEM[k].ABLAD = "";
                             _InputParameters.T_ITEM[k].AKTNR = "";
                             _InputParameters.T_ITEM[k].ANLN1 = "";
                             _InputParameters.T_ITEM[k].ANLN2 = "";
                             _InputParameters.T_ITEM[k].APLZL = "";
                             _InputParameters.T_ITEM[k].AR_NUMBER = "";
                             _InputParameters.T_ITEM[k].AUFNR = "";
                             _InputParameters.T_ITEM[k].AUFPL = "";
                             _InputParameters.T_ITEM[k].AUFPS = "";
                             _InputParameters.T_ITEM[k].BELNR = "";
                             _InputParameters.T_ITEM[k].BELUM = "";
                             _InputParameters.T_ITEM[k].BEMOT = "";
                             _InputParameters.T_ITEM[k].BERKZ = "";
                             _InputParameters.T_ITEM[k].BESTQ = "";
                             _InputParameters.T_ITEM[k].BIN_LOCATION = "";
                             _InputParameters.T_ITEM[k].BNBTR = 0;
                             _InputParameters.T_ITEM[k].BPMNG = 0;
                             _InputParameters.T_ITEM[k].BPRME = "";
                             _InputParameters.T_ITEM[k].BSTME = "";
                             _InputParameters.T_ITEM[k].BSTMG = 0;
                             _InputParameters.T_ITEM[k].BUALT = 0;
                             _InputParameters.T_ITEM[k].BUDAT_MKPF = "";
                             _InputParameters.T_ITEM[k].BUKRS = "";
                             _InputParameters.T_ITEM[k].BUSTM = "";
                             _InputParameters.T_ITEM[k].BUSTW = "";
                             _InputParameters.T_ITEM[k].BUZEI = "";
                             _InputParameters.T_ITEM[k].BUZUM = "";
                             _InputParameters.T_ITEM[k].BWART = "";
                             _InputParameters.T_ITEM[k].BWLVS = "";
                             _InputParameters.T_ITEM[k].BWTAR = "";
                             _InputParameters.T_ITEM[k].CERTIFICATE_ENCLOSED = "";
                             _InputParameters.T_ITEM[k].CHARG = "";
                             _InputParameters.T_ITEM[k].CPUDT_MKPF = "";
                             _InputParameters.T_ITEM[k].CPUTM_MKPF = "";
                             _InputParameters.T_ITEM[k].CUOBJ_CH = "";
                             _InputParameters.T_ITEM[k].DABRBZ = "";
                             _InputParameters.T_ITEM[k].DABRZ = "";
                             _InputParameters.T_ITEM[k].DMBTR = 0;
                             _InputParameters.T_ITEM[k].DMBUM = 0;
                             _InputParameters.T_ITEM[k].DYPLA = "";
                             _InputParameters.T_ITEM[k].EBELN = "";
                             _InputParameters.T_ITEM[k].EBELP = "";
                             _InputParameters.T_ITEM[k].ELIKZ = "";
                             _InputParameters.T_ITEM[k].EMATN = "";
                             _InputParameters.T_ITEM[k].EMLIF = "";
                             _InputParameters.T_ITEM[k].ENTRY_UOM_ISO = "";
                             _InputParameters.T_ITEM[k].EQUNR = "";
                             _InputParameters.T_ITEM[k].ERFME = "";
                             _InputParameters.T_ITEM[k].ERFMG = 0;
                             _InputParameters.T_ITEM[k].EVERE = "";
                             _InputParameters.T_ITEM[k].EVERS = "";
                             _InputParameters.T_ITEM[k].EXBWR = 0;
                             _InputParameters.T_ITEM[k].EXP_DATE = "";
                             _InputParameters.T_ITEM[k].EXVKW = 0;
                             _InputParameters.T_ITEM[k].FIPOS = "";
                             _InputParameters.T_ITEM[k].FISTL = "";
                             _InputParameters.T_ITEM[k].FKBER = "";
                             _InputParameters.T_ITEM[k].GEBER = "";
                             _InputParameters.T_ITEM[k].GJAHR = "";
                             _InputParameters.T_ITEM[k].GRANT_NBR = "";
                             _InputParameters.T_ITEM[k].GRUND = "";
                             _InputParameters.T_ITEM[k].GSBER = "";
                             _InputParameters.T_ITEM[k].HSDAT = "";
                             _InputParameters.T_ITEM[k].IMKEY = "";
                             _InputParameters.T_ITEM[k].INSMK = "";
                             _InputParameters.T_ITEM[k].ITEM = "";
                             _InputParameters.T_ITEM[k].ITEM_OK = "";
                             _InputParameters.T_ITEM[k].J_1AGIRUPD = "";
                             _InputParameters.T_ITEM[k].J_1BEXBASE = 0;
                             _InputParameters.T_ITEM[k].KBLNR = "";
                             _InputParameters.T_ITEM[k].KBLPOS = "";
                             _InputParameters.T_ITEM[k].KDAUF = "";
                             _InputParameters.T_ITEM[k].KDEIN = "";
                             _InputParameters.T_ITEM[k].KDPOS = "";
                             _InputParameters.T_ITEM[k].KOKRS = "";
                             _InputParameters.T_ITEM[k].KOSTL = "";
                             _InputParameters.T_ITEM[k].KSTRG = "";
                             _InputParameters.T_ITEM[k].KUNNR = "";
                             _InputParameters.T_ITEM[k].KZBEW = "";
                             _InputParameters.T_ITEM[k].KZBWS = "";
                             _InputParameters.T_ITEM[k].KZEAR = "";
                             _InputParameters.T_ITEM[k].KZSTR = "";
                             _InputParameters.T_ITEM[k].KZVBR = "";
                             _InputParameters.T_ITEM[k].KZZUG = "";
                             _InputParameters.T_ITEM[k].LBKUM = 0;
                             _InputParameters.T_ITEM[k].LFBJA = "";
                             _InputParameters.T_ITEM[k].LFBNR = "";
                             _InputParameters.T_ITEM[k].LFPOS = "";
                             _InputParameters.T_ITEM[k].LGNUM = "";
                             _InputParameters.T_ITEM[k].LGORT = "";
                             _InputParameters.T_ITEM[k].LGPLA = "";
                             _InputParameters.T_ITEM[k].LGTYP = "";
                             _InputParameters.T_ITEM[k].LIFNR = "";
                             _InputParameters.T_ITEM[k].LINE_DEPTH = "";
                             _InputParameters.T_ITEM[k].LINE_ID = "";
                             _InputParameters.T_ITEM[k].LLIEF = "";
                             _InputParameters.T_ITEM[k].LSMEH = "";
                             _InputParameters.T_ITEM[k].LSMNG = 0;
                             _InputParameters.T_ITEM[k].LSTAR = "";
                             _InputParameters.T_ITEM[k].MAA_URZEI = "";
                             _InputParameters.T_ITEM[k].MAKTX = "";
                             _InputParameters.T_ITEM[k].MANUFACTURE_NAME = "";
                             _InputParameters.T_ITEM[k].MAT_KDAUF = "";
                             _InputParameters.T_ITEM[k].MAT_KDPOS = "";
                             _InputParameters.T_ITEM[k].MAT_PSPNR = "";
                             _InputParameters.T_ITEM[k].MATBF = "";
                             _InputParameters.T_ITEM[k].MATNR = "";
                             _InputParameters.T_ITEM[k].MBLNR = "";
                             _InputParameters.T_ITEM[k].MDSLIP = "";
                             _InputParameters.T_ITEM[k].MEINS = "";
                             _InputParameters.T_ITEM[k].MENGE = 0;
                             _InputParameters.T_ITEM[k].MENGU = "";
                             _InputParameters.T_ITEM[k].MFG_DATE = "";
                             _InputParameters.T_ITEM[k].MJAHR = "";
                             _InputParameters.T_ITEM[k].MWSKZ = "";
                             _InputParameters.T_ITEM[k].NPLNR = "";
                             _InputParameters.T_ITEM[k].NSCHN = "";
                             _InputParameters.T_ITEM[k].PACK = "";
                             _InputParameters.T_ITEM[k].PACK_QTY = 0;
                             _InputParameters.T_ITEM[k].PALAN = 0;
                             _InputParameters.T_ITEM[k].PAOBJNR = "";
                             _InputParameters.T_ITEM[k].PARBU = "";
                             _InputParameters.T_ITEM[k].PARENT_ID = "";
                             _InputParameters.T_ITEM[k].PARGB = "";
                             _InputParameters.T_ITEM[k].PBAMG = 0;
                             _InputParameters.T_ITEM[k].PLPLA = "";
                             _InputParameters.T_ITEM[k].PPRCTR = "";
                             _InputParameters.T_ITEM[k].PRCTR = "";
                             _InputParameters.T_ITEM[k].PROJN = "";
                             _InputParameters.T_ITEM[k].PRZNR = "";
                             _InputParameters.T_ITEM[k].PS_PSP_PNR = "";
                             _InputParameters.T_ITEM[k].QINSPST = "";
                             _InputParameters.T_ITEM[k].R9NUM = "";
                             _InputParameters.T_ITEM[k].RSART = "";
                             _InputParameters.T_ITEM[k].RSNUM = "";
                             _InputParameters.T_ITEM[k].RSPOS = "";
                             _InputParameters.T_ITEM[k].SAKTO = "";
                             _InputParameters.T_ITEM[k].SALK3 = 0;
                             _InputParameters.T_ITEM[k].SGT_RCAT = "";
                             _InputParameters.T_ITEM[k].SGT_SCAT = "";
                             _InputParameters.T_ITEM[k].SGT_UMSCAT = "";
                             _InputParameters.T_ITEM[k].SGTXT = "";
                             _InputParameters.T_ITEM[k].SHKUM = "";
                             _InputParameters.T_ITEM[k].SHKZG = "";
                             _InputParameters.T_ITEM[k].SJAHR = "";
                             _InputParameters.T_ITEM[k].SMBLN = "";
                             _InputParameters.T_ITEM[k].SMBLP = "";
                             _InputParameters.T_ITEM[k].SPE_GTS_STOCK_TY = "";
                             _InputParameters.T_ITEM[k].TANUM = "";
                             _InputParameters.T_ITEM[k].TBNUM = "";
                             _InputParameters.T_ITEM[k].TBPOS = "";
                             _InputParameters.T_ITEM[k].TBPRI = "";
                             _InputParameters.T_ITEM[k].TCODE2_MKPF = "";
                             _InputParameters.T_ITEM[k].TXJCD = "";
                             _InputParameters.T_ITEM[k].UBNUM = "";
                             _InputParameters.T_ITEM[k].UMBAR = "";
                             _InputParameters.T_ITEM[k].UMCHA = "";
                             _InputParameters.T_ITEM[k].UMLGO = "";
                             _InputParameters.T_ITEM[k].UMMAB = "";
                             _InputParameters.T_ITEM[k].UMMAT = "";
                             _InputParameters.T_ITEM[k].UMSOK = "";
                             _InputParameters.T_ITEM[k].UMWRK = "";
                             _InputParameters.T_ITEM[k].UMZST = "";
                             _InputParameters.T_ITEM[k].UMZUS = "";
                             _InputParameters.T_ITEM[k].URZEI = "";
                             _InputParameters.T_ITEM[k].USNAM_MKPF = "";
                             _InputParameters.T_ITEM[k].VBELN_IM = "";
                             _InputParameters.T_ITEM[k].VBELP_IM = "";
                             _InputParameters.T_ITEM[k].VENDOR_BATCH_NO = "";
                             _InputParameters.T_ITEM[k].VENDOR_BATCH_NUMBER = "";
                             _InputParameters.T_ITEM[k].VFDAT = "";
                             _InputParameters.T_ITEM[k].VGART_MKPF = "";
                             _InputParameters.T_ITEM[k].VKMWS = "";
                             _InputParameters.T_ITEM[k].VKWRA = 0;
                             _InputParameters.T_ITEM[k].VKWRT = 0;
                             _InputParameters.T_ITEM[k].VPRSV = "";
                             _InputParameters.T_ITEM[k].VPTNR = "";
                             _InputParameters.T_ITEM[k].VSCHN = "";
                             _InputParameters.T_ITEM[k].WAERS = "";
                             _InputParameters.T_ITEM[k].WEANZ = "";
                             _InputParameters.T_ITEM[k].WEMPF = "";
                             _InputParameters.T_ITEM[k].WERKS = "";
                             _InputParameters.T_ITEM[k].WERTU = "";
                             _InputParameters.T_ITEM[k].WEUNB = "";
                             _InputParameters.T_ITEM[k].XAUTO = "";
                             _InputParameters.T_ITEM[k].XBEAU = "";
                             _InputParameters.T_ITEM[k].XBLNR_MKPF = "";
                             _InputParameters.T_ITEM[k].XBLVS = "";
                             _InputParameters.T_ITEM[k].XMACC = "";
                             _InputParameters.T_ITEM[k].XOBEW = "";
                             _InputParameters.T_ITEM[k].XRUEJ = "";
                             _InputParameters.T_ITEM[k].XRUEM = "";
                             _InputParameters.T_ITEM[k].XSAUF = "";
                             _InputParameters.T_ITEM[k].XSERG = "";
                             _InputParameters.T_ITEM[k].XSKST = "";
                             _InputParameters.T_ITEM[k].XSPRO = "";
                             _InputParameters.T_ITEM[k].XWOFF = "";
                             _InputParameters.T_ITEM[k].XWSBR = "";
                             _InputParameters.T_ITEM[k].ZEILE = "";
                             _InputParameters.T_ITEM[k].ZEKKN = "";
                             _InputParameters.T_ITEM[k].ZUSCH = "";
                             _InputParameters.T_ITEM[k].ZUSTD = "";
                             _InputParameters.T_ITEM[k].ZUSTD_T156M = "";
                             _InputParameters.T_ITEM[k].ZZ_REP_MAT = "";
                             #endregion

                             _InputParameters.T_ITEM[k].MJAHR = System.DateTime.Now.Year.ToString();
                             _InputParameters.T_ITEM[k].MATNR = clsStandards.filter(GrDispenDataPO.Rows[i].Cells[2].Text.Trim());
                             _InputParameters.T_ITEM[k].BWART = ConfigurationManager.AppSettings["Dispen_postData_MovementType"].ToString();
                             _InputParameters.T_ITEM[k].WERKS = clsStandards.current_Plant();
                             _InputParameters.T_ITEM[k].ERFMG = clsStandards.filter(GrDispenDataPO.Rows[i].Cells[9].Text.Trim()) == "" ? 0 : Convert.ToDecimal(clsStandards.filter(GrDispenDataPO.Rows[i].Cells[9].Text.Trim()));
                             _InputParameters.T_ITEM[k].ERFME = clsStandards.filter(GrDispenDataPO.Rows[i].Cells[15].Text.Trim());
                             _InputParameters.T_ITEM[k].LGORT = clsStandards.filter(GrDispenDataPO.Rows[i].Cells[10].Text.Trim());
                             _InputParameters.T_ITEM[k].CHARG = clsStandards.filter(GrDispenDataPO.Rows[i].Cells[4].Text.Trim());
                             _InputParameters.T_ITEM[k].AUFNR = txtDocumentNo.Text.Trim();
                             _InputParameters.T_ITEM[k].CPUTM_MKPF = DateTime.Now.ToString("HH:mm:ss");
                             _InputParameters.IM_HEADER.GR_GI_SLIP_NO = clsStandards.filter(GrDispenDataPO.Rows[k].Cells[19].Text.Trim());
                             _InputParameters.IM_HEADER.HEADER_TEXT = clsStandards.filter(GrDispenDataPO.Rows[k].Cells[18].Text.Trim());
                             _InputParameters.T_ITEM[k].EBELP = clsStandards.filter(GrDispenDataPO.Rows[i].Cells[11].Text.Trim());
                             _InputParameters.T_ITEM[k].PROJN = clsStandards.filter(GrDispenDataPO.Rows[i].Cells[20].Text.Trim());
                             int lineitem = 0;
                             if (k < 9)
                             {
                                 lineitem = k;
                                 lineitem++;
                                 _InputParameters.T_ITEM[k].ITEM = "00000" + lineitem.ToString(); // clsStandards.filter(GrGRNDetails.Rows[i].Cells[4].Text.Trim());//000001
                             }
                             else
                             {
                                 lineitem = k;
                                 lineitem++;
                                 _InputParameters.T_ITEM[k].ITEM = "0000" + lineitem.ToString(); // clsStandards.filter(GrGRNDetails.Rows[i].Cells[4].Text.Trim());//000001
                             }                         
                        }
                    }
                    var req = new MemoryStream();
                    var reqxml = new System.Xml.Serialization.XmlSerializer(_InputParameters.GetType());
                    reqxml.Serialize(req, _InputParameters);
                    string strxml = Encoding.UTF8.GetString(req.ToArray());
                    clsStandards.WriteSoapXML(strxml, "ProcessPost - Request", clsStandards.GetCurrentPageName().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());

                    _Response = _PO_No.ZDMF008(_InputParameters);
                    _OutputParameters3 = _Response.EX_MATERIAL_GRN;
                    _OutputParameters1 = _Response.T_ITEM;
                    _ReturnParameter = _Response.RETURN;

                    var res = new MemoryStream();
                    var resxml = new System.Xml.Serialization.XmlSerializer(_Response.GetType());
                    resxml.Serialize(res, _Response);
                    string xml = Encoding.UTF8.GetString(res.ToArray());
                    clsStandards.WriteSoapXML(xml, "ProcessPost - Response", clsStandards.GetCurrentPageName().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());

                    
                    if (_ReturnParameter.Length > 0)
                    {

                        for (int a = 0; a < _ReturnParameter.Length-1; a++)
                        {
                            string strPROCESSORDERNO = txtDocumentNo.Text.Trim();
                            string strMESSAGE = _ReturnParameter[a].MESSAGE.ToString();
                            string strMESSAGE_V1 = _ReturnParameter[a].MESSAGE_V1.ToString();
                            string strMESSAGE_V2 = _ReturnParameter[a].MESSAGE_V2.ToString();
                            string strMESSAGE_V3 = _ReturnParameter[a].MESSAGE_V3.ToString();
                            string strMESSAGE_V4 = _ReturnParameter[a].MESSAGE_V4.ToString();
                            string strROW = _ReturnParameter[a].ROW.ToString();
                            string strPLANT = clsStandards.current_Plant(); ;
                            string strCREATEDBY = clsStandards.current_Username();
                            string StrProcess = "Disp_ProcessOrder";
                            objBL.BL__Dispensing_Posting_Error(strPROCESSORDERNO, strMESSAGE, strMESSAGE_V1, strMESSAGE_V2, strMESSAGE_V3, strMESSAGE_V4, strROW, strPLANT, strCREATEDBY, StrProcess);
                            message += strMESSAGE;
                        }
                        if (message.Length > 150)
                        {
                            message = message.Substring(0, 150);
                        }
                    }
                    else
                    {
                       message = "SUCCESS";
                    }
                    if(_OutputParameters3.Length > 0)
                    {
                        for (int j = 0; j < _OutputParameters3.Length; j++)
                        {
                            mno = _OutputParameters3[j].MAN_NO;
                        }
                    }
                    else
                    {
                         mno = "";
                    }
                    
                        for (int b = 0; b < GrDispenDataPO.Rows.Count; b++)
                        {
                            CheckBox cb = (CheckBox)GrDispenDataPO.Rows[b].FindControl("chkSelect");
                            if (cb.Checked == true)
                            {
                                objPl.strPlant = clsStandards.current_Plant();
                                objPl.strProcess_Reservation_No = clsStandards.filter(GrDispenDataPO.Rows[b].Cells[1].Text.Trim());
                                objPl.strLineItemNo = clsStandards.filter(GrDispenDataPO.Rows[b].Cells[11].Text.Trim());
                                objPl.strProductCode = clsStandards.filter(GrDispenDataPO.Rows[b].Cells[12].Text.Trim());
                                objPl.strProductDesc = clsStandards.filter(GrDispenDataPO.Rows[b].Cells[13].Text.Trim());
                                objPl.strItemCode = clsStandards.filter(GrDispenDataPO.Rows[b].Cells[2].Text.Trim());
                                objPl.strItemDesc = clsStandards.filter(GrDispenDataPO.Rows[b].Cells[3].Text.Trim());
                                objPl.strQuantity = clsStandards.filter(GrDispenDataPO.Rows[b].Cells[9].Text.Trim()) == "" ? 0 : Convert.ToDecimal(clsStandards.filter(GrDispenDataPO.Rows[b].Cells[9].Text.Trim()));
                                objPl.strUOM = clsStandards.filter(GrDispenDataPO.Rows[b].Cells[5].Text.Trim());
                                objPl.strSAPBatchNo = clsStandards.filter(GrDispenDataPO.Rows[b].Cells[4].Text.Trim());
                                objPl.strRMQuantity = clsStandards.filter(GrDispenDataPO.Rows[b].Cells[14].Text.Trim()) == "" ? 0 : Convert.ToDecimal(clsStandards.filter(GrDispenDataPO.Rows[b].Cells[14].Text.Trim()));
                                objPl.strRMUOM = clsStandards.filter(GrDispenDataPO.Rows[b].Cells[15].Text.Trim());
                                objPl.strARNo = clsStandards.filter(GrDispenDataPO.Rows[b].Cells[16].Text.Trim());
                                objPl.strSTORAGELOC = clsStandards.filter(GrDispenDataPO.Rows[b].Cells[10].Text.Trim());
                                objPl.strBinCode = clsStandards.filter(GrDispenDataPO.Rows[b].Cells[17].Text.Trim());
                                objPl.strCostCenter = "";
                                objPl.strGM_Code = "";
                                objPl.strUser = clsStandards.current_Username();
                                objPl.strMaterialDocumentNo = mno;
                                objPl.strType = "PO";
                                objPl.strManufacture = "";
                                objPl.strSupplier = "";
                                objPl.strResult = message;
                                string Result = objBL.BL_Insert_MaterialDoc(objPl);
                                GridViewRow gr = (GridViewRow)GrDispenDataPO.Rows[b];
                                if (message.Contains("SUCCESS"))
                                {
                                    gr.BackColor = System.Drawing.Color.Yellow;
                                    //clsStandards.WriteLog(this, new Exception("Material Posted Successfully , Document No :  " + mno), clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 0, true);
                                }
                                else
                                {
                                    gr.BackColor = System.Drawing.Color.Red;
                                    //clsStandards.WriteLog(this, new Exception(message), clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 0, true);
                                }
                            }
                        }
                        if (message.Contains("SUCCESS"))
                        {
                            //gr.BackColor = System.Drawing.Color.Yellow;
                            clsStandards.WriteLog(this, new Exception("Material Posted Successfully , Document No :  " + mno), clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 0, true);
                        }
                        else
                        {
                           // gr.BackColor = System.Drawing.Color.Red;
                            clsStandards.WriteLog(this, new Exception("SAP Message : " + message), clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 0, true);
                        }
                    }
                }
            #endregion

            #region  Reservation Data post
            else if (chkReservation.Checked == true)
            {
                WsDispen_RS.z_ws_reservation_string _RS_NO = new WsDispen_RS.z_ws_reservation_string();
                WsDispen_RS.ZDMF010 _InputParameters = new WsDispen_RS.ZDMF010();
                WsDispen_RS.ZDMF010Response _Response = new WsDispen_RS.ZDMF010Response();
                WsDispen_RS.ZDBMS018[] _OutputParameters1 = new WsDispen_RS.ZDBMS018[0];
                WsDispen_RS.ZDBMS015[] _OutputParameters2 = new WsDispen_RS.ZDBMS015[0];
                WsDispen_RS.ZDBMS019[] _OutputParameters3 = new WsDispen_RS.ZDBMS019[0];
                WsDispen_RS.BAPIRET2[] _ReturnParameter = new WsDispen_RS.BAPIRET2[0];
                _InputParameters.IM_HEADER = new WsDispen_RS.ZDBMS015();

                _RS_NO.Proxy = new WebProxy();
                _RS_NO.Credentials = new NetworkCredential(txtSapUser.Text.Trim(), txtSapPass.Text.Trim());
                _RS_NO.Proxy.Credentials = new NetworkCredential(txtSapUser.Text.Trim(), txtSapPass.Text.Trim());

                int TotalCheckboxRS = 0;
                for (int i = 0; i < GrDispenDataRe.Rows.Count; i++)
                {
                    CheckBox cb = (CheckBox)GrDispenDataRe.Rows[i].FindControl("chkSelect");
                    if (cb.Checked == true)
                    {
                        TotalCheckboxRS++;
                    }
                }
                _InputParameters.T_ITEM = new WsDispen_RS.ZDBMS019[TotalCheckboxRS];
                _InputParameters.RETURN = new WsDispen_RS.BAPIRET2[TotalCheckboxRS];
                _ReturnParameter = new WsDispen_RS.BAPIRET2[TotalCheckboxRS];

                #region common parameters
                _InputParameters.IM_HEADER.AIRWAY_BILL_DATE = "";
                _InputParameters.IM_HEADER.AIRWAY_BILL_NO = "";
                _InputParameters.IM_HEADER.BILL_OF_LADING = "";
                _InputParameters.IM_HEADER.BILLDAT = "";
                _InputParameters.IM_HEADER.BILLNO = "";
                _InputParameters.IM_HEADER.DELIVERY_NOTE = "";
                _InputParameters.IM_HEADER.DOC_DATE = System.DateTime.Now.ToString("yyyy-MM-dd");
                _InputParameters.IM_HEADER.EXCISE_GROUP = "";
                _InputParameters.IM_HEADER.EXCISE_INVOICE = "";
                _InputParameters.IM_HEADER.EXCISE_INVOICED_DATE = "";
                _InputParameters.IM_HEADER.FNUM = "";
                _InputParameters.IM_HEADER.GATE_ENTRY_NO = "";
                _InputParameters.IM_HEADER.GM_CODE = "03";
                _InputParameters.IM_HEADER.GR_GI_SLIP_NO = "";
                _InputParameters.IM_HEADER.GR_RR_NO = "";
                _InputParameters.IM_HEADER.GROSS_WT = "";
                _InputParameters.IM_HEADER.HEADER_TEXT = "";
                _InputParameters.IM_HEADER.LIC_NO = "";
                _InputParameters.IM_HEADER.MAN_TAX = "";
                _InputParameters.IM_HEADER.MODE_OF_TRANSPORT = "";
                _InputParameters.IM_HEADER.NET_WT = "";
                _InputParameters.IM_HEADER.NO_OF_PACKS = "";
                _InputParameters.IM_HEADER.PLANT = clsStandards.current_Plant();
                _InputParameters.IM_HEADER.POSTING_DATE = System.DateTime.Now.ToString("yyyy-MM-dd");
                _InputParameters.IM_HEADER.PROCES_ORDER_NUMBER = txtDocumentNo.Text.Trim();
                _InputParameters.IM_HEADER.PURCHASE_ORDER_NO = "";
                _InputParameters.IM_HEADER.SALTAX = "";
                _InputParameters.IM_HEADER.TARE_WT = "";
                _InputParameters.IM_HEADER.TDATE = System.DateTime.Now.ToString("yyyy-MM-dd");
                _InputParameters.IM_HEADER.TR6 = "";
                _InputParameters.IM_HEADER.TRANSPORTER = "";
                _InputParameters.IM_HEADER.TRDAT = "";
                _InputParameters.IM_HEADER.TRUCK = "";
                _InputParameters.IM_HEADER.UNAME = "";
                _InputParameters.IM_HEADER.WAY_BILL_NOT_REQUIRED = "X";
                _InputParameters.IM_HEADER.WAY_DT = "";
                _InputParameters.IM_HEADER.WAY_REQ = "";
                #endregion

                int c = -1;
                if (GrDispenDataRe.Rows.Count > 0)
                {
                    
                   
                    for (int i = 0; i < GrDispenDataRe.Rows.Count; i++)
                    {
                        CheckBox cb = (CheckBox)GrDispenDataRe.Rows[i].FindControl("chkSelect");
                        if (cb.Checked == true)
                        {
                            c++;
                            #region
                            _InputParameters.T_ITEM[c] = new WsDispen_RS.ZDBMS019();
                            _InputParameters.T_ITEM[c].ABLAD = "";
                            _InputParameters.T_ITEM[c].AKTNR = "";
                            _InputParameters.T_ITEM[c].ANLN1 = "";
                            _InputParameters.T_ITEM[c].ANLN2 = "";
                            _InputParameters.T_ITEM[c].APLZL = "";
                            _InputParameters.T_ITEM[c].AR_NUMBER = "";
                            _InputParameters.T_ITEM[c].AUFNR = "";
                            _InputParameters.T_ITEM[c].AUFPL = "";
                            _InputParameters.T_ITEM[c].AUFPS = "";
                            _InputParameters.T_ITEM[c].BELNR = "";
                            _InputParameters.T_ITEM[c].BELUM = "";
                            _InputParameters.T_ITEM[c].BEMOT = "";
                            _InputParameters.T_ITEM[c].BERKZ = "";
                            _InputParameters.T_ITEM[c].BESTQ = "";
                            _InputParameters.T_ITEM[c].BIN_LOCATION = "";
                            _InputParameters.T_ITEM[c].BNBTR = 0;
                            _InputParameters.T_ITEM[c].BPMNG = 0;
                            _InputParameters.T_ITEM[c].BPRME = "";
                            _InputParameters.T_ITEM[c].BSTME = "";
                            _InputParameters.T_ITEM[c].BSTMG = 0;
                            _InputParameters.T_ITEM[c].BUALT = 0;
                            _InputParameters.T_ITEM[c].BUDAT_MKPF = "";
                            _InputParameters.T_ITEM[c].BUKRS = "";
                            _InputParameters.T_ITEM[c].BUSTM = "";
                            _InputParameters.T_ITEM[c].BUSTW = "";
                            _InputParameters.T_ITEM[c].BUZEI = "";
                            _InputParameters.T_ITEM[c].BUZUM = "";
                            _InputParameters.T_ITEM[c].BWART = "";
                            _InputParameters.T_ITEM[c].BWLVS = "";
                            _InputParameters.T_ITEM[c].BWTAR = "";
                            _InputParameters.T_ITEM[c].CERTIFICATE_ENCLOSED = "";
                            _InputParameters.T_ITEM[c].CHARG = "";
                            _InputParameters.T_ITEM[c].CPUDT_MKPF = "";
                            _InputParameters.T_ITEM[c].CPUTM_MKPF = "";
                            _InputParameters.T_ITEM[c].CUOBJ_CH = "";
                            _InputParameters.T_ITEM[c].DABRBZ = "";
                            _InputParameters.T_ITEM[c].DABRZ = "";
                            _InputParameters.T_ITEM[c].DMBTR = 0;
                            _InputParameters.T_ITEM[c].DMBUM = 0;
                            _InputParameters.T_ITEM[c].DYPLA = "";
                            _InputParameters.T_ITEM[c].EBELN = "";
                            _InputParameters.T_ITEM[c].EBELP = "";
                            _InputParameters.T_ITEM[c].ELIKZ = "";
                            _InputParameters.T_ITEM[c].EMATN = "";
                            _InputParameters.T_ITEM[c].EMLIF = "";
                            _InputParameters.T_ITEM[c].ENTRY_UOM_ISO = "";
                            _InputParameters.T_ITEM[c].EQUNR = "";
                            _InputParameters.T_ITEM[c].ERFME = "";
                            _InputParameters.T_ITEM[c].ERFMG = 0;
                            _InputParameters.T_ITEM[c].EVERE = "";
                            _InputParameters.T_ITEM[c].EVERS = "";
                            _InputParameters.T_ITEM[c].EXBWR = 0;
                            _InputParameters.T_ITEM[c].EXP_DATE = "";
                            _InputParameters.T_ITEM[c].EXVKW = 0;
                            _InputParameters.T_ITEM[c].FIPOS = "";
                            _InputParameters.T_ITEM[c].FISTL = "";
                            _InputParameters.T_ITEM[c].FKBER = "";
                            _InputParameters.T_ITEM[c].GEBER = "";
                            _InputParameters.T_ITEM[c].GJAHR = "";
                            _InputParameters.T_ITEM[c].GRANT_NBR = "";
                            _InputParameters.T_ITEM[c].GRUND = "";
                            _InputParameters.T_ITEM[c].GSBER = "";
                            _InputParameters.T_ITEM[c].HSDAT = "";
                            _InputParameters.T_ITEM[c].IMKEY = "";
                            _InputParameters.T_ITEM[c].INSMK = "";
                            _InputParameters.T_ITEM[c].ITEM = "";
                            _InputParameters.T_ITEM[c].ITEM_OK = "";
                            _InputParameters.T_ITEM[c].J_1AGIRUPD = "";
                            _InputParameters.T_ITEM[c].J_1BEXBASE = 0;
                            _InputParameters.T_ITEM[c].KBLNR = "";
                            _InputParameters.T_ITEM[c].KBLPOS = "";
                            _InputParameters.T_ITEM[c].KDAUF = "";
                            _InputParameters.T_ITEM[c].KDEIN = "";
                            _InputParameters.T_ITEM[c].KDPOS = "";
                            _InputParameters.T_ITEM[c].KOKRS = "";
                            _InputParameters.T_ITEM[c].KOSTL = "";
                            _InputParameters.T_ITEM[c].KSTRG = "";
                            _InputParameters.T_ITEM[c].KUNNR = "";
                            _InputParameters.T_ITEM[c].KZBEW = "";
                            _InputParameters.T_ITEM[c].KZBWS = "";
                            _InputParameters.T_ITEM[c].KZEAR = "";
                            _InputParameters.T_ITEM[c].KZSTR = "";
                            _InputParameters.T_ITEM[c].KZVBR = "";
                            _InputParameters.T_ITEM[c].KZZUG = "";
                            _InputParameters.T_ITEM[c].LBKUM = 0;
                            _InputParameters.T_ITEM[c].LFBJA = "";
                            _InputParameters.T_ITEM[c].LFBNR = "";
                            _InputParameters.T_ITEM[c].LFPOS = "";
                            _InputParameters.T_ITEM[c].LGNUM = "";
                            _InputParameters.T_ITEM[c].LGORT = "";
                            _InputParameters.T_ITEM[c].LGPLA = "";
                            _InputParameters.T_ITEM[c].LGTYP = "";
                            _InputParameters.T_ITEM[c].LIFNR = "";
                            _InputParameters.T_ITEM[c].LINE_DEPTH = "";
                            _InputParameters.T_ITEM[c].LINE_ID = "";
                            _InputParameters.T_ITEM[c].LLIEF = "";
                            _InputParameters.T_ITEM[c].LSMEH = "";
                            _InputParameters.T_ITEM[c].LSMNG = 0;
                            _InputParameters.T_ITEM[c].LSTAR = "";
                            _InputParameters.T_ITEM[c].MAA_URZEI = "";
                            _InputParameters.T_ITEM[c].MAKTX = "";
                            _InputParameters.T_ITEM[c].MANUFACTURE_NAME = "";
                            _InputParameters.T_ITEM[c].MAT_KDAUF = "";
                            _InputParameters.T_ITEM[c].MAT_KDPOS = "";
                            _InputParameters.T_ITEM[c].MAT_PSPNR = "";
                            _InputParameters.T_ITEM[c].MATBF = "";
                            _InputParameters.T_ITEM[c].MATNR = "";
                            _InputParameters.T_ITEM[c].MBLNR = "";
                            _InputParameters.T_ITEM[c].MDSLIP = "";
                            _InputParameters.T_ITEM[c].MEINS = "";
                            _InputParameters.T_ITEM[c].MENGE = 0;
                            _InputParameters.T_ITEM[c].MENGU = "";
                            _InputParameters.T_ITEM[c].MFG_DATE = "";
                            _InputParameters.T_ITEM[c].MJAHR = "";
                            _InputParameters.T_ITEM[c].MWSKZ = "";
                            _InputParameters.T_ITEM[c].NPLNR = "";
                            _InputParameters.T_ITEM[c].NSCHN = "";
                            _InputParameters.T_ITEM[c].PACK = "";
                            _InputParameters.T_ITEM[c].PACK_QTY = 0;
                            _InputParameters.T_ITEM[c].PALAN = 0;
                            _InputParameters.T_ITEM[c].PAOBJNR = "";
                            _InputParameters.T_ITEM[c].PARBU = "";
                            _InputParameters.T_ITEM[c].PARENT_ID = "";
                            _InputParameters.T_ITEM[c].PARGB = "";
                            _InputParameters.T_ITEM[c].PBAMG = 0;
                            _InputParameters.T_ITEM[c].PLPLA = "";
                            _InputParameters.T_ITEM[c].PPRCTR = "";
                            _InputParameters.T_ITEM[c].PRCTR = "";
                            _InputParameters.T_ITEM[c].PROJN = "";
                            _InputParameters.T_ITEM[c].PRZNR = "";
                            _InputParameters.T_ITEM[c].PS_PSP_PNR = "";
                            _InputParameters.T_ITEM[c].QINSPST = "";
                            _InputParameters.T_ITEM[c].R9NUM = "";
                            _InputParameters.T_ITEM[c].RSART = "";
                            _InputParameters.T_ITEM[c].RSNUM = "";
                            _InputParameters.T_ITEM[c].RSPOS = "";
                            _InputParameters.T_ITEM[c].SAKTO = "";
                            _InputParameters.T_ITEM[c].SALK3 = 0;
                            _InputParameters.T_ITEM[c].SGT_RCAT = "";
                            _InputParameters.T_ITEM[c].SGT_SCAT = "";
                            _InputParameters.T_ITEM[c].SGT_UMSCAT = "";
                            _InputParameters.T_ITEM[c].SGTXT = "";
                            _InputParameters.T_ITEM[c].SHKUM = "";
                            _InputParameters.T_ITEM[c].SHKZG = "";
                            _InputParameters.T_ITEM[c].SJAHR = "";
                            _InputParameters.T_ITEM[c].SMBLN = "";
                            _InputParameters.T_ITEM[c].SMBLP = "";
                            _InputParameters.T_ITEM[c].SPE_GTS_STOCK_TY = "";
                            _InputParameters.T_ITEM[c].TANUM = "";
                            _InputParameters.T_ITEM[c].TBNUM = "";
                            _InputParameters.T_ITEM[c].TBPOS = "";
                            _InputParameters.T_ITEM[c].TBPRI = "";
                            _InputParameters.T_ITEM[c].TCODE2_MKPF = "";
                            _InputParameters.T_ITEM[c].TXJCD = "";
                            _InputParameters.T_ITEM[c].UBNUM = "";
                            _InputParameters.T_ITEM[c].UMBAR = "";
                            _InputParameters.T_ITEM[c].UMCHA = "";
                            _InputParameters.T_ITEM[c].UMLGO = "";
                            _InputParameters.T_ITEM[c].UMMAB = "";
                            _InputParameters.T_ITEM[c].UMMAT = "";
                            _InputParameters.T_ITEM[c].UMSOK = "";
                            _InputParameters.T_ITEM[c].UMWRK = "";
                            _InputParameters.T_ITEM[c].UMZST = "";
                            _InputParameters.T_ITEM[c].UMZUS = "";
                            _InputParameters.T_ITEM[c].URZEI = "";
                            _InputParameters.T_ITEM[c].USNAM_MKPF = "";
                            _InputParameters.T_ITEM[c].VBELN_IM = "";
                            _InputParameters.T_ITEM[c].VBELP_IM = "";
                            _InputParameters.T_ITEM[c].VENDOR_BATCH_NO = "";
                            _InputParameters.T_ITEM[c].VENDOR_BATCH_NUMBER = "";
                            _InputParameters.T_ITEM[c].VFDAT = "";
                            _InputParameters.T_ITEM[c].VGART_MKPF = "";
                            _InputParameters.T_ITEM[c].VKMWS = "";
                            _InputParameters.T_ITEM[c].VKWRA = 0;
                            _InputParameters.T_ITEM[c].VKWRT = 0;
                            _InputParameters.T_ITEM[c].VPRSV = "";
                            _InputParameters.T_ITEM[c].VPTNR = "";
                            _InputParameters.T_ITEM[c].VSCHN = "";
                            _InputParameters.T_ITEM[c].WAERS = "";
                            _InputParameters.T_ITEM[c].WEANZ = "";
                            _InputParameters.T_ITEM[c].WEMPF = "";
                            _InputParameters.T_ITEM[c].WERKS = "";
                            _InputParameters.T_ITEM[c].WERTU = "";
                            _InputParameters.T_ITEM[c].WEUNB = "";
                            _InputParameters.T_ITEM[c].XAUTO = "";
                            _InputParameters.T_ITEM[c].XBEAU = "";
                            _InputParameters.T_ITEM[c].XBLNR_MKPF = "";
                            _InputParameters.T_ITEM[c].XBLVS = "";
                            _InputParameters.T_ITEM[c].XMACC = "";
                            _InputParameters.T_ITEM[c].XOBEW = "";
                            _InputParameters.T_ITEM[c].XRUEJ = "";
                            _InputParameters.T_ITEM[c].XRUEM = "";
                            _InputParameters.T_ITEM[c].XSAUF = "";
                            _InputParameters.T_ITEM[c].XSERG = "";
                            _InputParameters.T_ITEM[c].XSKST = "";
                            _InputParameters.T_ITEM[c].XSPRO = "";
                            _InputParameters.T_ITEM[c].XWOFF = "";
                            _InputParameters.T_ITEM[c].XWSBR = "";
                            _InputParameters.T_ITEM[c].ZEILE = "";
                            _InputParameters.T_ITEM[c].ZEKKN = "";
                            _InputParameters.T_ITEM[c].ZUSCH = "";
                            _InputParameters.T_ITEM[c].ZUSTD = "";
                            _InputParameters.T_ITEM[c].ZUSTD_T156M = "";
                            _InputParameters.T_ITEM[c].ZZ_REP_MAT = "";
                            #endregion

                            _InputParameters.T_ITEM[c].MJAHR = System.DateTime.Now.Year.ToString(); //current date
                            _InputParameters.T_ITEM[c].MATNR = clsStandards.filter(GrDispenDataRe.Rows[i].Cells[2].Text.Trim()); //Item Code
                            _InputParameters.T_ITEM[c].BWART = ConfigurationManager.AppSettings["Dispen_postData_MovementType"].ToString(); // MovementType
                            _InputParameters.T_ITEM[c].WERKS = clsStandards.current_Plant();
                            _InputParameters.T_ITEM[c].ERFMG = clsStandards.filter(GrDispenDataRe.Rows[i].Cells[9].Text.Trim()) == "" ? 0 : Convert.ToDecimal(clsStandards.filter(GrDispenDataRe.Rows[i].Cells[9].Text.Trim())); // Quantity
                            _InputParameters.T_ITEM[c].ERFME = clsStandards.filter(GrDispenDataRe.Rows[i].Cells[5].Text.Trim()); // Uom
                            _InputParameters.T_ITEM[c].LGORT = clsStandards.filter(GrDispenDataRe.Rows[i].Cells[10].Text.Trim()); // stroage loc
                            _InputParameters.T_ITEM[c].CHARG = clsStandards.filter(GrDispenDataRe.Rows[i].Cells[4].Text.Trim()); // Sap batch no
                            _InputParameters.T_ITEM[c].CPUTM_MKPF = DateTime.Now.ToString("HH:mm:ss");
                            _InputParameters.T_ITEM[c].RSNUM = txtDocumentNo.Text.Trim(); // Reservaton No
                            _InputParameters.T_ITEM[c].RSPOS = clsStandards.filter(GrDispenDataRe.Rows[i].Cells[11].Text.Trim());
                            _InputParameters.IM_HEADER.GR_GI_SLIP_NO = clsStandards.filter(GrDispenDataRe.Rows[i].Cells[15].Text.Trim());
                            _InputParameters.IM_HEADER.HEADER_TEXT = clsStandards.filter(GrDispenDataRe.Rows[i].Cells[14].Text.Trim());
                            _InputParameters.T_ITEM[c].PROJN = clsStandards.filter(GrDispenDataPO.Rows[i].Cells[16].Text.Trim());
                            int lineitem = 0;
                            if (c < 9)
                            {
                                lineitem = c;
                                lineitem++;
                                _InputParameters.T_ITEM[c].ITEM = "00000" + lineitem.ToString(); // clsStandards.filter(GrGRNDetails.Rows[i].Cells[4].Text.Trim());//000001
                            }
                            else
                            {
                                lineitem = c;
                                lineitem++;
                                _InputParameters.T_ITEM[c].ITEM = "0000" + lineitem.ToString(); // clsStandards.filter(GrGRNDetails.Rows[i].Cells[4].Text.Trim());//000001
                            }

                            #region return_parameter
                            _InputParameters.RETURN[c] = new WsDispen_RS.BAPIRET2();
                            _InputParameters.RETURN[c].FIELD = "";
                            _InputParameters.RETURN[c].ID = "";
                            _InputParameters.RETURN[c].LOG_MSG_NO = "";
                            _InputParameters.RETURN[c].LOG_NO = "";
                            _InputParameters.RETURN[c].MESSAGE = "";
                            _InputParameters.RETURN[c].MESSAGE_V1 = "";
                            _InputParameters.RETURN[c].MESSAGE_V2 = "";
                            _InputParameters.RETURN[c].MESSAGE_V3 = "";
                            _InputParameters.RETURN[c].MESSAGE_V4 = "";
                            _InputParameters.RETURN[c].NUMBER = "";
                            _InputParameters.RETURN[c].PARAMETER = "";
                            _InputParameters.RETURN[c].ROW = 0;
                            _InputParameters.RETURN[c].SYSTEM = "";
                            _InputParameters.RETURN[c].TYPE = "";
                            #endregion
                        }
                    }

                    var req = new MemoryStream();
                    var reqxml = new System.Xml.Serialization.XmlSerializer(_InputParameters.GetType());
                    reqxml.Serialize(req, _InputParameters);
                    string strxml = Encoding.UTF8.GetString(req.ToArray());
                    clsStandards.WriteSoapXML(strxml, "ReservationPost - Request", clsStandards.GetCurrentPageName().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());

                    _Response = _RS_NO.ZDMF010(_InputParameters);
                    _OutputParameters1 = _Response.EX_MATERIAL_GRN_RESE;
                    _OutputParameters3 = _Response.T_ITEM;
                    _ReturnParameter = _Response.RETURN;

                    var res = new MemoryStream();
                    var resxml = new System.Xml.Serialization.XmlSerializer(_Response.GetType());
                    resxml.Serialize(res, _Response);
                    string xml = Encoding.UTF8.GetString(res.ToArray());
                    clsStandards.WriteSoapXML(xml, "ReservationPost - Response", clsStandards.GetCurrentPageName().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());

                   
                    if (_ReturnParameter.Length > 0)
                    {

                        for (int a = 0; a < _ReturnParameter.Length - 1; a++)
                        {
                            string strPROCESSORDERNO = txtDocumentNo.Text.Trim();
                            string strMESSAGE = _ReturnParameter[a].MESSAGE.ToString();
                            string strMESSAGE_V1 = _ReturnParameter[a].MESSAGE_V1.ToString();
                            string strMESSAGE_V2 = _ReturnParameter[a].MESSAGE_V2.ToString();
                            string strMESSAGE_V3 = _ReturnParameter[a].MESSAGE_V3.ToString();
                            string strMESSAGE_V4 = _ReturnParameter[a].MESSAGE_V4.ToString();
                            string strROW = _ReturnParameter[a].ROW.ToString();
                            string strPLANT = clsStandards.current_Plant(); ;
                            string strCREATEDBY = clsStandards.current_Username();
                            string StrProcess = "Disp_Reservation";
                            objBL.BL__Dispensing_Posting_Error(strPROCESSORDERNO, strMESSAGE, strMESSAGE_V1, strMESSAGE_V2, strMESSAGE_V3,
                                strMESSAGE_V4, strROW, strPLANT, strCREATEDBY, StrProcess);
                            message += strMESSAGE;
                        }
                        if (message.Length > 150)
                        {
                            message = message.Substring(0, 150);
                        }
                    }
                    else
                    {
                       message = "SUCCESS";
                    }
                    if(_OutputParameters1.Length > 0)
                    {
                        for (int j = 0; j < _OutputParameters1.Length; j++)
                        {
                            mno = _OutputParameters1[j].MAN_NO;
                        }
                    }
                    else
                    {
                         mno = "";
                    }
                    for (int d = 0; d < GrDispenDataRe.Rows.Count; d++)
                    {
                        CheckBox cb = (CheckBox)GrDispenDataRe.Rows[d].FindControl("chkSelect");
                        if (cb.Checked == true)
                        {

                            objPl.strPlant = clsStandards.current_Plant();
                            objPl.strProcess_Reservation_No = clsStandards.filter(GrDispenDataRe.Rows[d].Cells[1].Text.Trim());
                            objPl.strLineItemNo = clsStandards.filter(GrDispenDataRe.Rows[d].Cells[11].Text.Trim());
                            objPl.strProductCode = "";
                            objPl.strProductDesc = "";
                            objPl.strItemCode = clsStandards.filter(GrDispenDataRe.Rows[d].Cells[2].Text.Trim());
                            objPl.strItemDesc = clsStandards.filter(GrDispenDataRe.Rows[d].Cells[3].Text.Trim());
                            objPl.strQuantity = clsStandards.filter(GrDispenDataRe.Rows[d].Cells[6].Text.Trim()) == "" ? 0 : Convert.ToDecimal(clsStandards.filter(GrDispenDataRe.Rows[d].Cells[6].Text.Trim()));
                            objPl.strUOM = clsStandards.filter(GrDispenDataRe.Rows[d].Cells[5].Text.Trim());
                            objPl.strSAPBatchNo = clsStandards.filter(GrDispenDataRe.Rows[d].Cells[4].Text.Trim());
                            objPl.strRMQuantity = 0;
                            objPl.strRMUOM = "";
                            objPl.strARNo = clsStandards.filter(GrDispenDataRe.Rows[d].Cells[12].Text.Trim());
                            objPl.strSTORAGELOC = clsStandards.filter(GrDispenDataRe.Rows[d].Cells[10].Text.Trim());
                            objPl.strBinCode = "";
                            objPl.strCostCenter = clsStandards.filter(GrDispenDataRe.Rows[d].Cells[13].Text.Trim());
                            objPl.strGM_Code = "03";
                            objPl.strUser = clsStandards.current_Username();
                            objPl.strMaterialDocumentNo = mno;
                            objPl.strType = "RS";
                            objPl.strManufacture = "";
                            objPl.strSupplier = "";
                            objPl.strResult = message;
                            string Result = objBL.BL_Insert_MaterialDoc(objPl);

                            cb.Enabled = false;
                            cb.Checked = false;
                            GridViewRow gr = (GridViewRow)GrDispenDataRe.Rows[d];
                            if (message.Contains("SUCCESS"))
                            {
                                gr.BackColor = System.Drawing.Color.Yellow;
                                //clsStandards.WriteLog(this, new Exception("Material Posted Successfully , Document No :  " + mno), clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 0, true);
                            }
                            else
                            {
                                gr.BackColor = System.Drawing.Color.Red;
                                //clsStandards.WriteLog(this, new Exception(message), clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 0, true);
                            }
                        }
                    }
                    if (message.Contains("SUCCESS"))
                    {
                        //gr.BackColor = System.Drawing.Color.Yellow;
                        clsStandards.WriteLog(this, new Exception("Material Posted Successfully , Document No :  " + mno), clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 0, true);
                    }
                    else
                    {
                        //gr.BackColor = System.Drawing.Color.Red;
                        clsStandards.WriteLog(this, new Exception("SAP Message : "+message), clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 0, true);
                    }
                }
            }
            #endregion
        }
        catch (Exception ex)
        {
            clsStandards.WriteLog(this, new Exception(ex.Message), clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 0, true);
        }
    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        objBL = new BL_Dispensing();
        try
        {
            if (string.IsNullOrEmpty(txtheadertext.Text))
            {
                clsStandards.WriteLog(this, new Exception("Please enter Header Text"), clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 0, true);
                return;
            }
            else if (string.IsNullOrEmpty(txtGRGINO.Text))
            {
                clsStandards.WriteLog(this, new Exception("Please enter GRGINO"), clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 0, true);
                return;
            }
            objBL.BL_UpdateHeader(clsStandards.current_Plant(), txtDocumentNo.Text.Trim(), txtheadertext.Text.Trim(), txtGRGINO.Text.Trim());
            btnGet_Click(sender, e);
        }
        catch (Exception ex)
        {
            clsStandards.WriteLog(this, new Exception(ex.Message), clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 0, true);
        }
        finally
        {
            objBL = null;
        }
    }
    protected void GrDispenDataPO_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        DataTable dtpo = new DataTable();
        dtpo = (DataTable)ViewState["data"];
        GrDispenDataPO.PageIndex = e.NewPageIndex;
        GrDispenDataPO.DataSource = dtpo;
        GrDispenDataPO.DataBind();
    }
    protected void GrDispenDataRe_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        DataTable dtpo = new DataTable();
        dtpo = (DataTable)ViewState["data"];
        GrDispenDataRe.PageIndex = e.NewPageIndex;
        GrDispenDataRe.DataSource = dtpo;
        GrDispenDataRe.DataBind();
    }

    protected void btnSelectAll_CheckedChanged(object sender, EventArgs e)
    {
        // int i = 0;
        if (GrDispenDataRe.Rows.Count > 0)
        {
            if (btnSelectAll.Checked)
            {
                foreach (GridViewRow gvr in GrDispenDataRe.Rows)
                {
                    CheckBox chkSelect = gvr.FindControl("chkSelect") as CheckBox;
                    if (chkSelect != null)
                    {
                        chkSelect.Checked = true;
                        //i++;
                    }
                }
                //i++;
            }
        }

        else if (GrDispenDataPO.Rows.Count > 0)
        {
            if (btnSelectAll.Checked)
            {
                foreach (GridViewRow gvr in GrDispenDataPO.Rows)
                {
                    CheckBox chkSelect = gvr.FindControl("chkSelect") as CheckBox;
                    if (chkSelect != null)
                    {
                        chkSelect.Checked = true;
                        //i++;
                    }
                }
                //i++;
            }
        }
    }
}