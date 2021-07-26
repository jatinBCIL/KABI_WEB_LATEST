using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLayer;
using DataLayer;
using PropertyLayer;
using System.Data;
using System.Data.Sql;
using System.Net;
using System.Configuration;
using System.Xml.Linq;
using System.IO;
using System.Text;
using System.Globalization;

public partial class frmMaterialPosting : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            btnAdd_ValutionType.Visible = false;
            MultiView1.SetActiveView(View1);
        }
        catch (Exception ex)
        {

        }
    }

    protected void btnGet_Click(object sender, EventArgs e)
    {
        BL_GrnPrinting objBL = new BL_GrnPrinting();
        DataTable dtGrn = new DataTable();
        try
        {
            dtGrn = objBL.BL_GetPurchase_Postdata(txtDocumentNo.Text.Trim(), clsStandards.current_Plant());
            if (dtGrn.Rows.Count > 0)
            {
                GrGRNDetails.DataSource = dtGrn;

                GrGRNDetails.DataBind();
            }
            else
            {
                GrGRNDetails.DataSource = null;
                GrGRNDetails.DataBind();
                clsStandards.WriteLog(this, new Exception("No details found for document"), clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 0, true);
            }
        }
        catch (Exception ex)
        {
            clsStandards.WriteLog(this, ex, clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 0, true);
            throw new Exception(ex.ToString());
        }
        finally
        {

        }
    }

    protected void chkSelect_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox chk = (CheckBox)sender;
        GridViewRow gv = (GridViewRow)chk.NamingContainer;
        int rownumber = gv.RowIndex;

        //if (chk.Checked)
        //{
        //    int i;
        //    for (i = 0; i <= GrGRNDetails.Rows.Count - 1; i++)
        //    {
        //        if (i != rownumber)
        //        {
        //            CheckBox chkcheckbox = ((CheckBox)(GrGRNDetails.Rows[i].FindControl("chkSelect")));
        //            chkcheckbox.Checked = false;
        //        }
        //    }
        //}
    }

    private bool Chk_Checkbox()
    {
        bool resp = false;
        if (GrGRNDetails.Rows.Count > 0)
        {
            for (int i = 0; i < GrGRNDetails.Rows.Count; i++)
            {
                CheckBox cb = (CheckBox)GrGRNDetails.Rows[i].FindControl("chkSelect");
                if (cb.Checked == true)
                {
                    resp = true;
                }
            }
        }
        return resp;
    }

    private bool CheckManno(string strMaNNo, string strItemcode, string strSappbatch,string strLineItemNo, string strVendorBatch)
    {
        bool resp = true;
        string strResult = "";
        BL_GrnPrinting objBL = new BL_GrnPrinting();
        try
        {
            strResult = objBL.BL_Check_ManNo(strMaNNo, strItemcode, strSappbatch,strLineItemNo, strVendorBatch, clsStandards.current_Plant());
            if (strResult.Contains("NO"))
            {
                resp = true;
            }
            else
            {
                resp = false;
            }
        }
        catch (Exception ex)
        {
            resp = false;
        }
        return resp;
    }
    //protected void btnPost_Click(object sender, EventArgs e)
    //{
    //    PL_ManNo ObjPL = new PL_ManNo();
    //    BL_GrnPrinting objBL = new BL_GrnPrinting();
    //    string strManno = "";
    //    string Sapbatch = "";
    //    string strItemcode = "";
    //    string strLineItemNo = "";
    //    XDocument ResultXML = new XDocument();
    //    try
    //    {
    //        if (!Chk_Checkbox())
    //        {
    //            clsStandards.WriteLog(this, new Exception(" Please Check the Item from List "), clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 0, true);
    //            return;
    //        }
    //        if (GrGRNDetails.Rows.Count > 0)
    //        {
    //            STO_Post.Z_WS_MIGO_POST _ManNo = new STO_Post.Z_WS_MIGO_POST();
    //            STO_Post.ZDMF002 _InputParameters = new STO_Post.ZDMF002();
    //            STO_Post.ZDMF002Response _Response = new STO_Post.ZDMF002Response();
    //            STO_Post.ZDBMS006[] _OutputParameters1 = new STO_Post.ZDBMS006[0];
    //            STO_Post.ZDBMS011[] _OutputParameters2 = new STO_Post.ZDBMS011[0];
    //            STO_Post.ZDBMS012[] _OutputParameters3 = new STO_Post.ZDBMS012[0];
    //            STO_Post.BAPIRET2[] _Returnparameter = new STO_Post.BAPIRET2[0];
    //            _InputParameters.IM_HEADER = new STO_Post.ZDBMS011();

    //            _ManNo.Proxy = new WebProxy();
    //            _ManNo.Credentials = new NetworkCredential(txtSapUser.Text.Trim(), txtSapPass.Text.Trim());
    //            _ManNo.Proxy.Credentials = new NetworkCredential(txtSapUser.Text.Trim(), txtSapPass.Text.Trim());

    //            int TotalCheckboxPO = 0;
    //            for (int k = 0; k < GrGRNDetails.Rows.Count; k++)
    //            {
    //                CheckBox cbk = (CheckBox)GrGRNDetails.Rows[k].FindControl("chkSelect");
    //                if (cbk.Checked == true)
    //                {
    //                    TotalCheckboxPO++;
    //                }
    //            }
    //            _InputParameters.T_ITEM = new STO_Post.ZDBMS012[TotalCheckboxPO];
    //            _InputParameters.RETURN = new STO_Post.BAPIRET2[TotalCheckboxPO];
    //            clsStandards.WriteLog(this, new Exception(" started "), clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 0, true);

    //            #region
    //            _InputParameters.IM_HEADER.WAY_REQ = "";
    //            _InputParameters.IM_HEADER.AIRWAY_BILL_DATE = "";
    //            _InputParameters.IM_HEADER.AIRWAY_BILL_NO = "";
    //            _InputParameters.IM_HEADER.SALTAX = "";
    //            _InputParameters.IM_HEADER.WAY_DT = "";
    //            _InputParameters.IM_HEADER.WAY_REQ = "";
    //            _InputParameters.IM_HEADER.FNUM = "";
    //            _InputParameters.IM_HEADER.BILL_OF_LADING = "";
    //            _InputParameters.IM_HEADER.GR_GI_SLIP_NO = "";
    //            _InputParameters.IM_HEADER.DELIVERY_NOTE = "";
    //            _InputParameters.IM_HEADER.EXCISE_INVOICE = "";
    //            _InputParameters.IM_HEADER.EXCISE_INVOICED_DATE = "";
    //            _InputParameters.IM_HEADER.EXCISE_GROUP = "";
    //            _InputParameters.IM_HEADER.TR6 = "";
    //            _InputParameters.IM_HEADER.TRDAT = "";
    //            _InputParameters.IM_HEADER.AIRWAY_BILL_NO = "";
    //            _InputParameters.IM_HEADER.AIRWAY_BILL_DATE = "";
    //            _InputParameters.IM_HEADER.BILL_OF_LADING = "";
    //            _InputParameters.IM_HEADER.DELIVERY_NOTE = "";
    //            _InputParameters.IM_HEADER.EXCISE_GROUP = "";
    //            _InputParameters.IM_HEADER.EXCISE_INVOICE = "";
    //            _InputParameters.IM_HEADER.EXCISE_INVOICED_DATE = "";
    //            _InputParameters.IM_HEADER.FNUM = "";
    //            _InputParameters.IM_HEADER.GR_GI_SLIP_NO = "";
    //            _InputParameters.IM_HEADER.GR_RR_NO = "";
    //            _InputParameters.IM_HEADER.NO_OF_PACKS = "";
    //            _InputParameters.IM_HEADER.TR6 = "";
    //            _InputParameters.IM_HEADER.TRDAT = "";
    //            _InputParameters.IM_HEADER.WAY_DT = "";
    //            _InputParameters.IM_HEADER.WAY_REQ = "";

    //            #endregion

    //            int j = -1;
    //            for (int i = 0; i < GrGRNDetails.Rows.Count; i++)
    //            {
    //                CheckBox cb = (CheckBox)GrGRNDetails.Rows[i].FindControl("chkSelect");
    //                if (cb.Checked == true)
    //                {
    //                    j++;
    //                    #region
    //                    //clsStandards.WriteLog(this, new Exception("started 1 "), clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 0, true);

    //                    _InputParameters.T_ITEM[j] = new STO_Post.ZDBMS012();
    //                    _InputParameters.T_ITEM[j].MBLNR = "";
    //                    _InputParameters.T_ITEM[j].MJAHR = "";
    //                    _InputParameters.T_ITEM[j].ZEILE = "";
    //                    _InputParameters.T_ITEM[j].LINE_ID = "";
    //                    _InputParameters.T_ITEM[j].PARENT_ID = "";
    //                    _InputParameters.T_ITEM[j].LINE_DEPTH = "";
    //                    _InputParameters.T_ITEM[j].MAA_URZEI = "";
    //                    _InputParameters.T_ITEM[j].XAUTO = "";
    //                    _InputParameters.T_ITEM[j].LGORT = "";
    //                    _InputParameters.T_ITEM[j].CHARG = "";
    //                    _InputParameters.T_ITEM[j].INSMK = "";
    //                    _InputParameters.T_ITEM[j].ZUSCH = "";
    //                    _InputParameters.T_ITEM[j].ZUSTD = "";
    //                    _InputParameters.T_ITEM[j].LIFNR = "";
    //                    _InputParameters.T_ITEM[j].KUNNR = "";
    //                    _InputParameters.T_ITEM[j].KDAUF = "";
    //                    _InputParameters.T_ITEM[j].KDPOS = "";
    //                    _InputParameters.T_ITEM[j].KDEIN = "";
    //                    _InputParameters.T_ITEM[j].PLPLA = "";
    //                    _InputParameters.T_ITEM[j].SHKZG = "";
    //                    _InputParameters.T_ITEM[j].WAERS = "";
    //                    _InputParameters.T_ITEM[j].DMBTR = 0;
    //                    _InputParameters.T_ITEM[j].BNBTR = 0;
    //                    _InputParameters.T_ITEM[j].BUALT = 0;
    //                    _InputParameters.T_ITEM[j].SHKUM = "";
    //                    _InputParameters.T_ITEM[j].DMBUM = 0;
    //                    _InputParameters.T_ITEM[j].MENGE = 0;
    //                    _InputParameters.T_ITEM[j].MEINS = "";
    //                    _InputParameters.T_ITEM[j].BPMNG = 0;
    //                    _InputParameters.T_ITEM[j].BPRME = "";
    //                    _InputParameters.T_ITEM[j].LFBJA = "";
    //                    _InputParameters.T_ITEM[j].LFBNR = "";
    //                    _InputParameters.T_ITEM[j].LFPOS = "";
    //                    _InputParameters.T_ITEM[j].SJAHR = "";
    //                    _InputParameters.T_ITEM[j].SMBLN = "";
    //                    _InputParameters.T_ITEM[j].SMBLP = "";
    //                    _InputParameters.T_ITEM[j].ELIKZ = "";
    //                    _InputParameters.T_ITEM[j].EQUNR = "";
    //                    _InputParameters.T_ITEM[j].WEMPF = "";
    //                    _InputParameters.T_ITEM[j].ABLAD = "";
    //                    _InputParameters.T_ITEM[j].GSBER = "";
    //                    _InputParameters.T_ITEM[j].KOKRS = "";
    //                    _InputParameters.T_ITEM[j].PARGB = "";
    //                    _InputParameters.T_ITEM[j].PARBU = "";
    //                    _InputParameters.T_ITEM[j].KOSTL = "";
    //                    _InputParameters.T_ITEM[j].PROJN = "";
    //                    _InputParameters.T_ITEM[j].AUFNR = "";
    //                    _InputParameters.T_ITEM[j].ANLN1 = "";
    //                    _InputParameters.T_ITEM[j].ANLN2 = "";
    //                    _InputParameters.T_ITEM[j].XSKST = "";
    //                    _InputParameters.T_ITEM[j].XSAUF = "";
    //                    _InputParameters.T_ITEM[j].XSPRO = "";
    //                    _InputParameters.T_ITEM[j].XSERG = "";
    //                    _InputParameters.T_ITEM[j].XRUEM = "";
    //                    _InputParameters.T_ITEM[j].XRUEJ = "";
    //                    _InputParameters.T_ITEM[j].BUKRS = "";
    //                    _InputParameters.T_ITEM[j].BELNR = "";
    //                    _InputParameters.T_ITEM[j].BUZEI = "";
    //                    _InputParameters.T_ITEM[j].BELUM = "";
    //                    _InputParameters.T_ITEM[j].BUZUM = "";
    //                    _InputParameters.T_ITEM[j].RSNUM = "";
    //                    _InputParameters.T_ITEM[j].RSPOS = "";
    //                    _InputParameters.T_ITEM[j].KZEAR = "";
    //                    _InputParameters.T_ITEM[j].PBAMG = 0;
    //                    _InputParameters.T_ITEM[j].KZSTR = "";
    //                    _InputParameters.T_ITEM[j].UMMAT = "";
    //                    _InputParameters.T_ITEM[j].UMWRK = "";
    //                    _InputParameters.T_ITEM[j].UMLGO = "";
    //                    _InputParameters.T_ITEM[j].UMCHA = "";
    //                    _InputParameters.T_ITEM[j].UMZST = "";
    //                    _InputParameters.T_ITEM[j].UMZUS = "";
    //                    _InputParameters.T_ITEM[j].UMBAR = "";
    //                    _InputParameters.T_ITEM[j].UMSOK = "";
    //                    _InputParameters.T_ITEM[j].KZVBR = "";
    //                    _InputParameters.T_ITEM[j].KZZUG = "";
    //                    _InputParameters.T_ITEM[j].WEUNB = "";
    //                    _InputParameters.T_ITEM[j].PALAN = 0;
    //                    _InputParameters.T_ITEM[j].LGNUM = "";
    //                    _InputParameters.T_ITEM[j].LGTYP = "";
    //                    _InputParameters.T_ITEM[j].LGPLA = "";
    //                    _InputParameters.T_ITEM[j].BESTQ = "";
    //                    _InputParameters.T_ITEM[j].BWLVS = "";
    //                    _InputParameters.T_ITEM[j].TBNUM = "";
    //                    _InputParameters.T_ITEM[j].TBPOS = "";
    //                    _InputParameters.T_ITEM[j].XBLVS = "";
    //                    _InputParameters.T_ITEM[j].VSCHN = "";
    //                    _InputParameters.T_ITEM[j].NSCHN = "";
    //                    _InputParameters.T_ITEM[j].DYPLA = "";
    //                    _InputParameters.T_ITEM[j].UBNUM = "";
    //                    _InputParameters.T_ITEM[j].TBPRI = "";
    //                    _InputParameters.T_ITEM[j].TANUM = "";
    //                    _InputParameters.T_ITEM[j].WEANZ = "";
    //                    _InputParameters.T_ITEM[j].GRUND = "";
    //                    _InputParameters.T_ITEM[j].EVERS = "";
    //                    _InputParameters.T_ITEM[j].EVERE = "";
    //                    _InputParameters.T_ITEM[j].IMKEY = "";
    //                    _InputParameters.T_ITEM[j].KSTRG = "";
    //                    _InputParameters.T_ITEM[j].PAOBJNR = "";
    //                    _InputParameters.T_ITEM[j].PRCTR = "";
    //                    _InputParameters.T_ITEM[j].PS_PSP_PNR = "";
    //                    _InputParameters.T_ITEM[j].NPLNR = "";
    //                    _InputParameters.T_ITEM[j].AUFPL = "";
    //                    _InputParameters.T_ITEM[j].APLZL = "";
    //                    _InputParameters.T_ITEM[j].AUFPS = "";
    //                    _InputParameters.T_ITEM[j].VPTNR = "";
    //                    _InputParameters.T_ITEM[j].FIPOS = "";
    //                    _InputParameters.T_ITEM[j].SAKTO = "";
    //                    _InputParameters.T_ITEM[j].BSTMG = 0;
    //                    _InputParameters.T_ITEM[j].BSTME = "";
    //                    _InputParameters.T_ITEM[j].XWSBR = "";
    //                    _InputParameters.T_ITEM[j].EMLIF = "";
    //                    _InputParameters.T_ITEM[j].ZZ_REP_MAT = "";
    //                    _InputParameters.T_ITEM[j].EXBWR = 0;
    //                    _InputParameters.T_ITEM[j].VKWRT = 0;
    //                    _InputParameters.T_ITEM[j].AKTNR = "";
    //                    _InputParameters.T_ITEM[j].ZEKKN = "";
    //                    _InputParameters.T_ITEM[j].CUOBJ_CH = "";
    //                    _InputParameters.T_ITEM[j].EXVKW = j;
    //                    _InputParameters.T_ITEM[j].PPRCTR = "";
    //                    _InputParameters.T_ITEM[j].RSART = "";
    //                    _InputParameters.T_ITEM[j].GEBER = "";
    //                    _InputParameters.T_ITEM[j].FISTL = "";
    //                    _InputParameters.T_ITEM[j].MATBF = "";
    //                    _InputParameters.T_ITEM[j].UMMAB = "";
    //                    _InputParameters.T_ITEM[j].BUSTM = "";
    //                    _InputParameters.T_ITEM[j].BUSTW = "";
    //                    _InputParameters.T_ITEM[j].MENGU = "";
    //                    _InputParameters.T_ITEM[j].WERTU = "";
    //                    _InputParameters.T_ITEM[j].LBKUM = 0;
    //                    _InputParameters.T_ITEM[j].SALK3 = 0;
    //                    _InputParameters.T_ITEM[j].VPRSV = "";
    //                    _InputParameters.T_ITEM[j].FKBER = "";
    //                    _InputParameters.T_ITEM[j].DABRBZ = "";
    //                    _InputParameters.T_ITEM[j].VKWRA = 0;
    //                    _InputParameters.T_ITEM[j].DABRZ = "";
    //                    _InputParameters.T_ITEM[j].XBEAU = "";
    //                    _InputParameters.T_ITEM[j].LSMEH = "";
    //                    _InputParameters.T_ITEM[j].KZBWS = "";
    //                    _InputParameters.T_ITEM[j].QINSPST = "";
    //                    _InputParameters.T_ITEM[j].URZEI = "";
    //                    _InputParameters.T_ITEM[j].J_1BEXBASE = 0;
    //                    _InputParameters.T_ITEM[j].MWSKZ = "";
    //                    _InputParameters.T_ITEM[j].TXJCD = "";
    //                    _InputParameters.T_ITEM[j].EMATN = "";
    //                    _InputParameters.T_ITEM[j].J_1AGIRUPD = "";
    //                    _InputParameters.T_ITEM[j].VKMWS = "";
    //                    _InputParameters.T_ITEM[j].BERKZ = "";
    //                    _InputParameters.T_ITEM[j].MAT_KDAUF = "";
    //                    _InputParameters.T_ITEM[j].MAT_KDPOS = "";
    //                    _InputParameters.T_ITEM[j].MAT_PSPNR = "";
    //                    _InputParameters.T_ITEM[j].XWOFF = "";
    //                    _InputParameters.T_ITEM[j].BEMOT = "";
    //                    _InputParameters.T_ITEM[j].PRZNR = "";
    //                    _InputParameters.T_ITEM[j].LLIEF = "";
    //                    _InputParameters.T_ITEM[j].LSTAR = "";
    //                    _InputParameters.T_ITEM[j].XOBEW = "";
    //                    _InputParameters.T_ITEM[j].GRANT_NBR = "";
    //                    _InputParameters.T_ITEM[j].ZUSTD_T156M = "";
    //                    _InputParameters.T_ITEM[j].SPE_GTS_STOCK_TY = "";
    //                    _InputParameters.T_ITEM[j].KBLNR = "";
    //                    _InputParameters.T_ITEM[j].KBLPOS = "";
    //                    _InputParameters.T_ITEM[j].XMACC = "";
    //                    _InputParameters.T_ITEM[j].VGART_MKPF = "";
    //                    _InputParameters.T_ITEM[j].BUDAT_MKPF = "";
    //                    _InputParameters.T_ITEM[j].CPUDT_MKPF = "";
    //                    _InputParameters.T_ITEM[j].USNAM_MKPF = "";
    //                    _InputParameters.T_ITEM[j].XBLNR_MKPF = "";
    //                    _InputParameters.T_ITEM[j].TCODE2_MKPF = "";
    //                    _InputParameters.T_ITEM[j].VBELN_IM = "";
    //                    _InputParameters.T_ITEM[j].VBELP_IM = "";
    //                    _InputParameters.T_ITEM[j].SGT_SCAT = "";
    //                    _InputParameters.T_ITEM[j].SGT_UMSCAT = "";
    //                    _InputParameters.T_ITEM[j].SGT_RCAT = "";
    //                    _InputParameters.T_ITEM[j].ITEM = "";
    //                    _InputParameters.T_ITEM[j].MDSLIP = "";
    //                    _InputParameters.T_ITEM[j].R9NUM = "";
    //                    _InputParameters.T_ITEM[j].ENTRY_UOM_ISO = "";
    //                    _InputParameters.T_ITEM[j].MFG_DATE = "";
    //                    _InputParameters.T_ITEM[j].EXP_DATE = "";
    //                    _InputParameters.T_ITEM[j].VENDOR_BATCH_NUMBER = "";
    //                    _InputParameters.T_ITEM[j].ITEM_OK = "";
    //                    _InputParameters.T_ITEM[j].AR_NUMBER = "";
    //                    _InputParameters.T_ITEM[j].BIN_LOCATION = "";
    //                    _InputParameters.T_ITEM[j].ADV_LIC1 = "";
    //                    _InputParameters.T_ITEM[j].ADV_LIC2 = "";
    //                    _InputParameters.T_ITEM[j].ADV_MENGE1 = 0;
    //                    _InputParameters.T_ITEM[j].ADV_MENGE2 = 0;

    //                    _InputParameters.RETURN[j] = new STO_Post.BAPIRET2();
    //                    _InputParameters.RETURN[j].FIELD = "";
    //                    _InputParameters.RETURN[j].ID = "";
    //                    _InputParameters.RETURN[j].LOG_MSG_NO = "";
    //                    _InputParameters.RETURN[j].LOG_NO = "";
    //                    _InputParameters.RETURN[j].MESSAGE = "";
    //                    _InputParameters.RETURN[j].MESSAGE_V1 = "";
    //                    _InputParameters.RETURN[j].MESSAGE_V2 = "";
    //                    _InputParameters.RETURN[j].MESSAGE_V3 = "";
    //                    _InputParameters.RETURN[j].MESSAGE_V4 = "";
    //                    _InputParameters.RETURN[j].NUMBER = "";
    //                    _InputParameters.RETURN[j].PARAMETER = "";
    //                    _InputParameters.RETURN[j].ROW = 0;
    //                    _InputParameters.RETURN[j].SYSTEM = "";
    //                    _InputParameters.RETURN[j].TYPE = "";
    //                    #endregion
    //                    clsStandards.WriteLog(this, new Exception(" started 1"), clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 0, true);
    //                    _InputParameters.IM_HEADER.PLANT = clsStandards.current_Plant();
    //                    _InputParameters.IM_HEADER.PURCHASE_ORDER_NO = clsStandards.filter(GrGRNDetails.Rows[i].Cells[2].Text.Trim());
    //                    _InputParameters.IM_HEADER.DOC_DATE = System.DateTime.Now.ToString("yyyy-MM-dd");
    //                    _InputParameters.IM_HEADER.GATE_ENTRY_NO = clsStandards.filter(GrGRNDetails.Rows[i].Cells[12].Text.Trim());
    //                    _InputParameters.IM_HEADER.TDATE = clsStandards.filter(GrGRNDetails.Rows[i].Cells[31].Text.Trim());
    //                    _InputParameters.IM_HEADER.GM_CODE = clsStandards.filter(ConfigurationManager.AppSettings["GM_CODE"].ToString());
    //                    _InputParameters.IM_HEADER.GROSS_WT = clsStandards.filter(GrGRNDetails.Rows[i].Cells[21].Text.Trim());
    //                    _InputParameters.IM_HEADER.HEADER_TEXT = clsStandards.filter(GrGRNDetails.Rows[i].Cells[38].Text.Trim());
    //                    _InputParameters.IM_HEADER.LIC_NO = "11223";
    //                    _InputParameters.IM_HEADER.MODE_OF_TRANSPORT = clsStandards.filter(GrGRNDetails.Rows[i].Cells[16].Text.Trim().Substring(0, 4));
    //                    _InputParameters.IM_HEADER.NET_WT = clsStandards.filter(GrGRNDetails.Rows[i].Cells[23].Text.Trim());
    //                    _InputParameters.IM_HEADER.POSTING_DATE = System.DateTime.Now.ToString("yyyy-MM-dd");
    //                    _InputParameters.IM_HEADER.TARE_WT = clsStandards.filter(GrGRNDetails.Rows[i].Cells[22].Text.Trim());
    //                    _InputParameters.IM_HEADER.TRANSPORTER = clsStandards.filter(GrGRNDetails.Rows[i].Cells[11].Text.Trim());
    //                    _InputParameters.IM_HEADER.TRUCK = clsStandards.filter(GrGRNDetails.Rows[i].Cells[17].Text.Trim());
    //                    _InputParameters.IM_HEADER.UNAME = txtSapUser.Text.Trim();
    //                    if (!string.IsNullOrEmpty(clsStandards.filter(GrGRNDetails.Rows[i].Cells[8].Text.Trim())))
    //                    {
    //                        _InputParameters.IM_HEADER.AIRWAY_BILL_NO = clsStandards.filter(GrGRNDetails.Rows[i].Cells[8].Text.Trim());
    //                        _InputParameters.IM_HEADER.AIRWAY_BILL_DATE = clsStandards.filter(GrGRNDetails.Rows[i].Cells[10].Text.Trim());
    //                    }
    //                    else
    //                    {
    //                        _InputParameters.IM_HEADER.WAY_BILL_NOT_REQUIRED = "X";
    //                    }
                        
    //                    _InputParameters.IM_HEADER.MAN_TAX = "X";
    //                    _InputParameters.IM_HEADER.GR_GI_SLIP_NO = clsStandards.filter(GrGRNDetails.Rows[i].Cells[37].Text.Trim());
    //                    _InputParameters.IM_HEADER.GR_RR_NO = clsStandards.filter(GrGRNDetails.Rows[i].Cells[36].Text.Trim());
    //                    _InputParameters.IM_HEADER.DELIVERY_NOTE = clsStandards.filter(GrGRNDetails.Rows[i].Cells[39].Text.Trim());
    //                    _InputParameters.T_ITEM[j].XBLNR_MKPF = clsStandards.filter(GrGRNDetails.Rows[i].Cells[39].Text.Trim());
    //                    _InputParameters.T_ITEM[j].MATNR = clsStandards.filter(GrGRNDetails.Rows[i].Cells[5].Text.Trim());

    //                    _InputParameters.T_ITEM[j].BWART = ConfigurationManager.AppSettings["ManNo_MovementType"].ToString(); // MovementType
    //                    _InputParameters.T_ITEM[j].WERKS = clsStandards.current_Plant();
    //                    // _InputParameters.T_ITEM[i].LIFNR = ""; // Transporter Code
    //                    _InputParameters.T_ITEM[j].ERFMG = Convert.ToDecimal(clsStandards.filter(GrGRNDetails.Rows[i].Cells[13].Text.Trim())); //AcceptedQty
    //                    _InputParameters.T_ITEM[j].EBELN = txtDocumentNo.Text.Trim(); //Purchase order no
    //                    _InputParameters.T_ITEM[j].EBELP = clsStandards.filter(GrGRNDetails.Rows[i].Cells[4].Text.Trim()); //lineitem no
    //                    int lineitem = 0;
    //                    //clsStandards.WriteLog(this, new Exception(" started 2 "), clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 0, true);

    //                    if (j < 9)
    //                    {
    //                        lineitem = j;
    //                        lineitem++;
    //                        _InputParameters.T_ITEM[j].ITEM = "00000" + lineitem.ToString(); // clsStandards.filter(GrGRNDetails.Rows[i].Cells[4].Text.Trim());//000001
    //                        _InputParameters.T_ITEM[j].ZEILE = "000" + lineitem.ToString();
    //                    }
    //                    else
    //                    {
    //                        lineitem = j;
    //                        lineitem++;
    //                        _InputParameters.T_ITEM[j].ITEM = "0000" + lineitem.ToString(); // clsStandards.filter(GrGRNDetails.Rows[i].Cells[4].Text.Trim());//000001
    //                        _InputParameters.T_ITEM[j].ZEILE = "00" + lineitem.ToString();
    //                    }
    //                    _InputParameters.T_ITEM[j].ERFME = clsStandards.filter(GrGRNDetails.Rows[i].Cells[9].Text.Trim());
    //                    _InputParameters.T_ITEM[j].KZBEW = "B"; 
    //                    //_InputParameters.T_ITEM[i].PACK = ""; //No of Packs
    //                    _InputParameters.T_ITEM[j].VENDOR_BATCH_NO = clsStandards.filter(GrGRNDetails.Rows[i].Cells[26].Text.Trim());
    //                    _InputParameters.T_ITEM[j].HSDAT = System.DateTime.Now.ToString("yyyy-MM-dd");
    //                    _InputParameters.T_ITEM[j].KOSTL = clsStandards.filter(GrGRNDetails.Rows[i].Cells[33].Text.Trim());
    //                    _InputParameters.T_ITEM[j].GRUND = "0101";
    //                    _InputParameters.T_ITEM[j].LGORT = clsStandards.filter(GrGRNDetails.Rows[i].Cells[34].Text.Trim());

    //                    //_InputParameters.T_ITEM[i].LSMNG = Convert.ToDecimal(clsStandards.filter(GrGRNDetails.Rows[i].Cells[13].Text.Trim())); //AcceptedQty
    //                    clsStandards.WriteLog(this, new Exception(" started 2"), clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 0, true);
    //                    if (clsStandards.filter(GrGRNDetails.Rows[i].Cells[29].Text.Trim()).ToString() == "")
    //                    {
    //                        _InputParameters.T_ITEM[j].BWTAR = "";
    //                    }
    //                    else
    //                    {
    //                        _InputParameters.T_ITEM[j].BWTAR = clsStandards.filter(GrGRNDetails.Rows[i].Cells[29].Text.Trim());
    //                    }

    //                    _InputParameters.T_ITEM[j].VFDAT = clsStandards.filter(GrGRNDetails.Rows[i].Cells[15].Text.Trim());
    //                    _InputParameters.T_ITEM[j].ENTRY_UOM_ISO = clsStandards.filter(GrGRNDetails.Rows[i].Cells[9].Text.Trim());
                    
    //                    _InputParameters.T_ITEM[j].SGTXT = clsStandards.filter(GrGRNDetails.Rows[i].Cells[32].Text.Trim()); //Item Description
    //                    _InputParameters.T_ITEM[j].GJAHR = System.DateTime.Now.Year.ToString(); //year
    //                    _InputParameters.T_ITEM[j].MANUFACTURE_NAME = clsStandards.filter(GrGRNDetails.Rows[i].Cells[28].Text.Trim()); ;
    //                    _InputParameters.T_ITEM[j].PACK_QTY = Convert.ToDecimal(clsStandards.filter(GrGRNDetails.Rows[i].Cells[24].Text.Trim()));
    //                    _InputParameters.T_ITEM[j].MAKTX = clsStandards.filter(GrGRNDetails.Rows[i].Cells[32].Text.Trim()); // Item Desc
    //                    _InputParameters.T_ITEM[j].CPUTM_MKPF = "";
    //                    _InputParameters.T_ITEM[j].CERTIFICATE_ENCLOSED = "";
    //                    _InputParameters.IM_HEADER.NO_OF_PACKS = clsStandards.filter(GrGRNDetails.Rows[i].Cells[18].Text.Trim());
    //                    _InputParameters.T_ITEM[j].PACK = clsStandards.filter(GrGRNDetails.Rows[i].Cells[18].Text.Trim());
    //                    _InputParameters.T_ITEM[j].CONTAINER = Convert.ToDecimal(clsStandards.filter(GrGRNDetails.Rows[i].Cells[18].Text.Trim()));
    //                    _InputParameters.T_ITEM[j].ADV_LIC1 = clsStandards.filter(GrGRNDetails.Rows[i].Cells[40].Text.Trim());
    //                    _InputParameters.T_ITEM[j].ADV_LIC2 = clsStandards.filter(GrGRNDetails.Rows[i].Cells[41].Text.Trim());
    //                    _InputParameters.T_ITEM[j].ADV_MENGE1 = Convert.ToDecimal(clsStandards.filter(GrGRNDetails.Rows[i].Cells[13].Text.Trim())); 
    //                    _InputParameters.T_ITEM[j].ADV_MENGE2 = 0;

    //                }
    //            }
    //            clsStandards.WriteLog(this, new Exception(" started 3"), clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 0, true);
    //            var req = new MemoryStream();
    //            var reqxml = new System.Xml.Serialization.XmlSerializer(_InputParameters.GetType());
    //            reqxml.Serialize(req, _InputParameters);
    //            string strxml = Encoding.UTF8.GetString(req.ToArray());
    //            clsStandards.WriteSoapXML(strxml, "MaterialPosting - Request  ", clsStandards.GetCurrentPageName().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());

    //           //clsStandards.WriteLog(this, new Exception(strxml), clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 0, true);

    //            _Response = _ManNo.ZDMF002(_InputParameters);
    //            _OutputParameters1 = _Response.EX_MATERIAL_GRN;
    //            _OutputParameters3 = _Response.T_ITEM;
    //            _Returnparameter = _Response.RETURN;

    //            var res = new MemoryStream();
    //            var resxml = new System.Xml.Serialization.XmlSerializer(_Response.GetType());
    //            resxml.Serialize(res, _Response);
    //            string xml = Encoding.UTF8.GetString(res.ToArray());
    //            clsStandards.WriteSoapXML(xml, "MaterialPosting - Response", clsStandards.GetCurrentPageName().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());

    //           clsStandards.WriteLog(this, new Exception(xml), clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 0, true);

    //            string message = "";
    //            if (_Returnparameter.Length > 0)
    //            {
    //                for (int a = 0; a < _Returnparameter.Length; a++)
    //                {

    //                    string strPROCESSORDERNO = txtDocumentNo.Text.Trim();
    //                    string strMESSAGE = _Returnparameter[a].MESSAGE.ToString();
    //                    string strMESSAGE_V1 = _Returnparameter[a].MESSAGE_V1.ToString();
    //                    string strMESSAGE_V2 = _Returnparameter[a].MESSAGE_V2.ToString();
    //                    string strMESSAGE_V3 = _Returnparameter[a].MESSAGE_V3.ToString();
    //                    string strMESSAGE_V4 = _Returnparameter[a].MESSAGE_V4.ToString();
    //                    string strROW = _Returnparameter[a].ROW.ToString();
    //                    string strPLANT = clsStandards.current_Plant(); ;
    //                    string strCREATEDBY = clsStandards.current_Username();
    //                    objBL.BL__Dispensing_Posting_Error(strPROCESSORDERNO, strMESSAGE, strMESSAGE_V1, strMESSAGE_V2, strMESSAGE_V3, strMESSAGE_V4, strROW, strPLANT, strCREATEDBY, "MaNNO POST");
    //                    message += strMESSAGE;
    //                    if (message.ToUpper().Contains("VALUATION TYPE"))
    //                    {
    //                        btnAdd_ValutionType.Visible = true;
    //                    }
    //                }
    //                if (message.Length > 150)
    //                {
    //                    message = message.Substring(0, 150);
    //                }
    //            }
    //            else
    //            {
    //                message = "SUCCESS";
    //            }
    //            //if (_OutputParameters1.Length > 0)
    //            //{
    //            //    for (int m = 0; m < _OutputParameters1.Length; m++)
    //            //    {
    //            //        strManno = _OutputParameters1[m].MAN_NO;
    //            //        Sapbatch = _OutputParameters1[m].SAP_BATCH;
    //            //    }
    //            //}
    //            //else
    //            //{
    //            //    strManno = "";
    //            //    Sapbatch = "";
    //            //}

                
    //            clsStandards.WriteLog(this, new Exception("lenth :" + _OutputParameters1.Length), clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 0, false);

    //            for (int i = 0; i < GrGRNDetails.Rows.Count; i++)
    //            {
    //                CheckBox cb = (CheckBox)GrGRNDetails.Rows[i].FindControl("chkSelect");
    //                if (cb.Checked == true)
    //                {
    //                    ObjPL.strItemCode = clsStandards.filter(GrGRNDetails.Rows[i].Cells[5].Text.Trim());
    //                    ObjPL.strVendorBatchNo = clsStandards.filter(GrGRNDetails.Rows[i].Cells[26].Text.Trim());
    //                    ObjPL.strLineItemNo = clsStandards.filter(GrGRNDetails.Rows[i].Cells[4].Text.Trim());
    //                    if (_OutputParameters1.Length > 0)
    //                    {
    //                        for (int m = 0; m < _OutputParameters1.Length; m++)
    //                        {
    //                            strItemcode = _OutputParameters1[m].ITEM_CODE;
    //                            strLineItemNo = ObjPL.strLineItemNo;
    //                            if (strItemcode.Trim() == ObjPL.strItemCode.Trim())
    //                            {
    //                                if (CheckManno(_OutputParameters1[m].MAN_NO.Trim(), strItemcode.Trim(), _OutputParameters1[m].SAP_BATCH.Trim(), strLineItemNo, _OutputParameters1[m].VENDOR_BATCH_NO.Trim()))
    //                                {
    //                                    strManno = _OutputParameters1[m].MAN_NO.Trim();
    //                                    Sapbatch = _OutputParameters1[m].SAP_BATCH.Trim();
    //                                    break;
    //                                }
    //                                else
    //                                {
    //                                    strManno = "";
    //                                    Sapbatch = "";
    //                                }
    //                            }
    //                        }
    //                    }
    //                    else
    //                    {
    //                        strManno = "";
    //                        Sapbatch = "";
    //                    }
                        
    //                    ObjPL.strPlant = clsStandards.filter(GrGRNDetails.Rows[i].Cells[1].Text.Trim());
    //                    ObjPL.strPurchaseOrderNo = clsStandards.filter(GrGRNDetails.Rows[i].Cells[2].Text.Trim());
    //                    ObjPL.strPurchaseOrderDate = clsStandards.filter(GrGRNDetails.Rows[i].Cells[3].Text.Trim());
    //                    ObjPL.strVendorName = clsStandards.filter(GrGRNDetails.Rows[i].Cells[25].Text.Trim());
    //                    ObjPL.strItemType = clsStandards.filter(GrGRNDetails.Rows[i].Cells[6].Text.Trim());
    //                    ObjPL.strOrderQty = Convert.ToDecimal(clsStandards.filter(GrGRNDetails.Rows[i].Cells[7].Text.Trim()));
    //                    ObjPL.strUom = clsStandards.filter(GrGRNDetails.Rows[i].Cells[9].Text.Trim());
    //                    ObjPL.strAcceptedQty = Convert.ToDecimal(clsStandards.filter(GrGRNDetails.Rows[i].Cells[13].Text.Trim()));
    //                    ObjPL.strNoOfContainer = Convert.ToDecimal(clsStandards.filter(GrGRNDetails.Rows[i].Cells[18].Text.Trim()));
    //                    ObjPL.strPackQty = Convert.ToDecimal(clsStandards.filter(GrGRNDetails.Rows[i].Cells[24].Text.Trim()));
    //                    ObjPL.strGateEntryNo = clsStandards.filter(GrGRNDetails.Rows[i].Cells[12].Text.Trim());
    //                    ObjPL.strManfactureName = clsStandards.filter(GrGRNDetails.Rows[i].Cells[28].Text.Trim()); ;
    //                    ObjPL.strVendorBatchNo = clsStandards.filter(GrGRNDetails.Rows[i].Cells[26].Text.Trim());
    //                    ObjPL.strMFGDate = clsStandards.filter(GrGRNDetails.Rows[i].Cells[14].Text.Trim());
    //                    ObjPL.strEXPDate = clsStandards.filter(GrGRNDetails.Rows[i].Cells[15].Text.Trim());
    //                    ObjPL.strMANNo = strManno;
    //                    ObjPL.strSapBatchNo = Sapbatch;
    //                    ObjPL.strUser = clsStandards.current_Username();
    //                    ObjPL.strTareWt = clsStandards.filter(GrGRNDetails.Rows[i].Cells[22].Text.Trim()) == "" ? 0 : Convert.ToDecimal(clsStandards.filter(GrGRNDetails.Rows[i].Cells[22].Text.Trim()));
    //                    ObjPL.strGrossWt = clsStandards.filter(GrGRNDetails.Rows[i].Cells[21].Text.Trim()) == "" ? 0 : Convert.ToDecimal(clsStandards.filter(GrGRNDetails.Rows[i].Cells[21].Text.Trim()));
    //                    ObjPL.strNetWt = clsStandards.filter(GrGRNDetails.Rows[i].Cells[23].Text.Trim()) == "" ? 0 : Convert.ToDecimal(clsStandards.filter(GrGRNDetails.Rows[i].Cells[23].Text.Trim()));
    //                    ObjPL.Purchase_Ref = clsStandards.filter(GrGRNDetails.Rows[i].Cells[30].Text.Trim()) == "" ? 0 : Convert.ToInt32(clsStandards.filter(GrGRNDetails.Rows[i].Cells[30].Text.Trim()));
    //                    ObjPL.strMessgae = message;
    //                    ObjPL.strStroageLoc = clsStandards.filter(GrGRNDetails.Rows[i].Cells[34].Text.Trim()).ToUpper();

    //                    string Result = objBL.BL_Insert_ManNo(ObjPL);

    //                    GridViewRow gr = (GridViewRow)GrGRNDetails.Rows[i];
    //                    if (message.Contains("SUCCESS"))
    //                    {
    //                        cb.Enabled = false;
    //                        cb.Checked = false;
    //                        gr.BackColor = System.Drawing.Color.Yellow;
    //                        //clsStandards.WriteLog(this, new Exception("Material Posted Successfully , Document No :  " + mno), clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 0, true);
    //                    }
    //                    else
    //                    {
    //                        gr.BackColor = System.Drawing.Color.Red;
    //                        //clsStandards.WriteLog(this, new Exception(message), clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 0, true);
    //                    }
    //                }
    //            }
    //            if (message.Contains("SUCCESS"))
    //            {
    //                //gr.BackColor = System.Drawing.Color.Yellow;
    //                clsStandards.WriteLog(this, new Exception("Material Posted Successfully , Document No :  " + strManno), clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 0, true);
    //            }
    //            else
    //            {
    //                clsStandards.WriteLog(this, new Exception("SAP Message : " + message), clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 0, true);
    //            }
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        clsStandards.WriteLog(this, new Exception(ex.ToString()), clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 0, true);
    //        return;
    //    }
    //}

    protected void btnPost_Click(object sender, EventArgs e)
    {
        PL_ManNo ObjPL = new PL_ManNo();
        BL_GrnPrinting objBL = new BL_GrnPrinting();
        string strManno = "";
        string Sapbatch = "";
        string strItemcode = "";
        string strLineItemNo = "";
        XDocument ResultXML = new XDocument();
        try
        {
            if (!Chk_Checkbox())
            {
                clsStandards.WriteLog(this, new Exception(" Please Check the Item from List "), clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 0, true);
                return;
            }
            if (GrGRNDetails.Rows.Count > 0)
            {
                WS_ManNo.Z_WS_ZDMF002 _ManNo = new WS_ManNo.Z_WS_ZDMF002();
                WS_ManNo.ZDMF002 _InputParameters = new WS_ManNo.ZDMF002();
                WS_ManNo.ZDMF002Response _Response = new WS_ManNo.ZDMF002Response();
                WS_ManNo.ZDBMS006[] _OutputParameters1 = new WS_ManNo.ZDBMS006[0];
                WS_ManNo.ZDBMS011[] _OutputParameters2 = new WS_ManNo.ZDBMS011[0];
                WS_ManNo.ZDBMS012[] _OutputParameters3 = new WS_ManNo.ZDBMS012[0];
                WS_ManNo.BAPIRET2[] _Returnparameter = new WS_ManNo.BAPIRET2[0];
                _InputParameters.IM_HEADER = new WS_ManNo.ZDBMS011();

                _ManNo.Proxy = new WebProxy();
                _ManNo.Credentials = new NetworkCredential(txtSapUser.Text.Trim(), txtSapPass.Text.Trim());
                _ManNo.Proxy.Credentials = new NetworkCredential(txtSapUser.Text.Trim(), txtSapPass.Text.Trim());

                int TotalCheckboxPO = 0;
                for (int k = 0; k < GrGRNDetails.Rows.Count; k++)
                {
                    CheckBox cbk = (CheckBox)GrGRNDetails.Rows[k].FindControl("chkSelect");
                    if (cbk.Checked == true)
                    {
                        TotalCheckboxPO++;
                    }
                }
                _InputParameters.T_ITEM = new WS_ManNo.ZDBMS012[TotalCheckboxPO];
                _InputParameters.RETURN = new WS_ManNo.BAPIRET2[TotalCheckboxPO];
                //clsStandards.WriteLog(this, new Exception(" started "), clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 0, true);

                #region
                _InputParameters.IM_HEADER.WAY_REQ = "";
                _InputParameters.IM_HEADER.AIRWAY_BILL_DATE = "";
                _InputParameters.IM_HEADER.AIRWAY_BILL_NO = "";
                _InputParameters.IM_HEADER.SALTAX = "";
                _InputParameters.IM_HEADER.WAY_DT = "";
                _InputParameters.IM_HEADER.WAY_REQ = "";
                _InputParameters.IM_HEADER.FNUM = "";
                _InputParameters.IM_HEADER.BILL_OF_LADING = "";
                _InputParameters.IM_HEADER.GR_GI_SLIP_NO = "";
                _InputParameters.IM_HEADER.DELIVERY_NOTE = "";
                _InputParameters.IM_HEADER.EXCISE_INVOICE = "";
                _InputParameters.IM_HEADER.EXCISE_INVOICED_DATE = "";
                _InputParameters.IM_HEADER.EXCISE_GROUP = "";
                _InputParameters.IM_HEADER.TR6 = "";
                _InputParameters.IM_HEADER.TRDAT = "";
                _InputParameters.IM_HEADER.AIRWAY_BILL_NO = "";
                _InputParameters.IM_HEADER.AIRWAY_BILL_DATE = "";
                _InputParameters.IM_HEADER.BILL_OF_LADING = "";
                _InputParameters.IM_HEADER.DELIVERY_NOTE = "";
                _InputParameters.IM_HEADER.EXCISE_GROUP = "";
                _InputParameters.IM_HEADER.EXCISE_INVOICE = "";
                _InputParameters.IM_HEADER.EXCISE_INVOICED_DATE = "";
                _InputParameters.IM_HEADER.FNUM = "";
                _InputParameters.IM_HEADER.GR_GI_SLIP_NO = "";
                _InputParameters.IM_HEADER.GR_RR_NO = "";
                _InputParameters.IM_HEADER.NO_OF_PACKS = "";
                _InputParameters.IM_HEADER.TR6 = "";
                _InputParameters.IM_HEADER.TRDAT = "";
                _InputParameters.IM_HEADER.WAY_DT = "";
                _InputParameters.IM_HEADER.WAY_REQ = "";

                #endregion

                int j = -1;
                for (int i = 0; i < GrGRNDetails.Rows.Count; i++)
                {
                    CheckBox cb = (CheckBox)GrGRNDetails.Rows[i].FindControl("chkSelect");
                    if (cb.Checked == true)
                    {
                        j++;
                        #region
                        //clsStandards.WriteLog(this, new Exception("started 1 "), clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 0, true);

                        _InputParameters.T_ITEM[j] = new WS_ManNo.ZDBMS012();
                        _InputParameters.T_ITEM[j].MBLNR = "";
                        _InputParameters.T_ITEM[j].MJAHR = "";
                        _InputParameters.T_ITEM[j].ZEILE = "";
                        _InputParameters.T_ITEM[j].LINE_ID = "";
                        _InputParameters.T_ITEM[j].PARENT_ID = "";
                        _InputParameters.T_ITEM[j].LINE_DEPTH = "";
                        _InputParameters.T_ITEM[j].MAA_URZEI = "";
                        _InputParameters.T_ITEM[j].XAUTO = "";
                        _InputParameters.T_ITEM[j].LGORT = "";
                        _InputParameters.T_ITEM[j].CHARG = "";
                        _InputParameters.T_ITEM[j].INSMK = "";
                        _InputParameters.T_ITEM[j].ZUSCH = "";
                        _InputParameters.T_ITEM[j].ZUSTD = "";
                        _InputParameters.T_ITEM[j].LIFNR = "";
                        _InputParameters.T_ITEM[j].KUNNR = "";
                        _InputParameters.T_ITEM[j].KDAUF = "";
                        _InputParameters.T_ITEM[j].KDPOS = "";
                        _InputParameters.T_ITEM[j].KDEIN = "";
                        _InputParameters.T_ITEM[j].PLPLA = "";
                        _InputParameters.T_ITEM[j].SHKZG = "";
                        _InputParameters.T_ITEM[j].WAERS = "";
                        _InputParameters.T_ITEM[j].DMBTR = 0;
                        _InputParameters.T_ITEM[j].BNBTR = 0;
                        _InputParameters.T_ITEM[j].BUALT = 0;
                        _InputParameters.T_ITEM[j].SHKUM = "";
                        _InputParameters.T_ITEM[j].DMBUM = 0;
                        _InputParameters.T_ITEM[j].MENGE = 0;
                        _InputParameters.T_ITEM[j].MEINS = "";
                        _InputParameters.T_ITEM[j].BPMNG = 0;
                        _InputParameters.T_ITEM[j].BPRME = "";
                        _InputParameters.T_ITEM[j].LFBJA = "";
                        _InputParameters.T_ITEM[j].LFBNR = "";
                        _InputParameters.T_ITEM[j].LFPOS = "";
                        _InputParameters.T_ITEM[j].SJAHR = "";
                        _InputParameters.T_ITEM[j].SMBLN = "";
                        _InputParameters.T_ITEM[j].SMBLP = "";
                        _InputParameters.T_ITEM[j].ELIKZ = "";
                        _InputParameters.T_ITEM[j].EQUNR = "";
                        _InputParameters.T_ITEM[j].WEMPF = "";
                        _InputParameters.T_ITEM[j].ABLAD = "";
                        _InputParameters.T_ITEM[j].GSBER = "";
                        _InputParameters.T_ITEM[j].KOKRS = "";
                        _InputParameters.T_ITEM[j].PARGB = "";
                        _InputParameters.T_ITEM[j].PARBU = "";
                        _InputParameters.T_ITEM[j].KOSTL = "";
                        _InputParameters.T_ITEM[j].PROJN = "";
                        _InputParameters.T_ITEM[j].AUFNR = "";
                        _InputParameters.T_ITEM[j].ANLN1 = "";
                        _InputParameters.T_ITEM[j].ANLN2 = "";
                        _InputParameters.T_ITEM[j].XSKST = "";
                        _InputParameters.T_ITEM[j].XSAUF = "";
                        _InputParameters.T_ITEM[j].XSPRO = "";
                        _InputParameters.T_ITEM[j].XSERG = "";
                        _InputParameters.T_ITEM[j].XRUEM = "";
                        _InputParameters.T_ITEM[j].XRUEJ = "";
                        _InputParameters.T_ITEM[j].BUKRS = "";
                        _InputParameters.T_ITEM[j].BELNR = "";
                        _InputParameters.T_ITEM[j].BUZEI = "";
                        _InputParameters.T_ITEM[j].BELUM = "";
                        _InputParameters.T_ITEM[j].BUZUM = "";
                        _InputParameters.T_ITEM[j].RSNUM = "";
                        _InputParameters.T_ITEM[j].RSPOS = "";
                        _InputParameters.T_ITEM[j].KZEAR = "";
                        _InputParameters.T_ITEM[j].PBAMG = 0;
                        _InputParameters.T_ITEM[j].KZSTR = "";
                        _InputParameters.T_ITEM[j].UMMAT = "";
                        _InputParameters.T_ITEM[j].UMWRK = "";
                        _InputParameters.T_ITEM[j].UMLGO = "";
                        _InputParameters.T_ITEM[j].UMCHA = "";
                        _InputParameters.T_ITEM[j].UMZST = "";
                        _InputParameters.T_ITEM[j].UMZUS = "";
                        _InputParameters.T_ITEM[j].UMBAR = "";
                        _InputParameters.T_ITEM[j].UMSOK = "";
                        _InputParameters.T_ITEM[j].KZVBR = "";
                        _InputParameters.T_ITEM[j].KZZUG = "";
                        _InputParameters.T_ITEM[j].WEUNB = "";
                        _InputParameters.T_ITEM[j].PALAN = 0;
                        _InputParameters.T_ITEM[j].LGNUM = "";
                        _InputParameters.T_ITEM[j].LGTYP = "";
                        _InputParameters.T_ITEM[j].LGPLA = "";
                        _InputParameters.T_ITEM[j].BESTQ = "";
                        _InputParameters.T_ITEM[j].BWLVS = "";
                        _InputParameters.T_ITEM[j].TBNUM = "";
                        _InputParameters.T_ITEM[j].TBPOS = "";
                        _InputParameters.T_ITEM[j].XBLVS = "";
                        _InputParameters.T_ITEM[j].VSCHN = "";
                        _InputParameters.T_ITEM[j].NSCHN = "";
                        _InputParameters.T_ITEM[j].DYPLA = "";
                        _InputParameters.T_ITEM[j].UBNUM = "";
                        _InputParameters.T_ITEM[j].TBPRI = "";
                        _InputParameters.T_ITEM[j].TANUM = "";
                        _InputParameters.T_ITEM[j].WEANZ = "";
                        _InputParameters.T_ITEM[j].GRUND = "";
                        _InputParameters.T_ITEM[j].EVERS = "";
                        _InputParameters.T_ITEM[j].EVERE = "";
                        _InputParameters.T_ITEM[j].IMKEY = "";
                        _InputParameters.T_ITEM[j].KSTRG = "";
                        _InputParameters.T_ITEM[j].PAOBJNR = "";
                        _InputParameters.T_ITEM[j].PRCTR = "";
                        _InputParameters.T_ITEM[j].PS_PSP_PNR = "";
                        _InputParameters.T_ITEM[j].NPLNR = "";
                        _InputParameters.T_ITEM[j].AUFPL = "";
                        _InputParameters.T_ITEM[j].APLZL = "";
                        _InputParameters.T_ITEM[j].AUFPS = "";
                        _InputParameters.T_ITEM[j].VPTNR = "";
                        _InputParameters.T_ITEM[j].FIPOS = "";
                        _InputParameters.T_ITEM[j].SAKTO = "";
                        _InputParameters.T_ITEM[j].BSTMG = 0;
                        _InputParameters.T_ITEM[j].BSTME = "";
                        _InputParameters.T_ITEM[j].XWSBR = "";
                        _InputParameters.T_ITEM[j].EMLIF = "";
                        _InputParameters.T_ITEM[j].ZZ_REP_MAT = "";
                        _InputParameters.T_ITEM[j].EXBWR = 0;
                        _InputParameters.T_ITEM[j].VKWRT = 0;
                        _InputParameters.T_ITEM[j].AKTNR = "";
                        _InputParameters.T_ITEM[j].ZEKKN = "";
                        _InputParameters.T_ITEM[j].CUOBJ_CH = "";
                        _InputParameters.T_ITEM[j].EXVKW = j;
                        _InputParameters.T_ITEM[j].PPRCTR = "";
                        _InputParameters.T_ITEM[j].RSART = "";
                        _InputParameters.T_ITEM[j].GEBER = "";
                        _InputParameters.T_ITEM[j].FISTL = "";
                        _InputParameters.T_ITEM[j].MATBF = "";
                        _InputParameters.T_ITEM[j].UMMAB = "";
                        _InputParameters.T_ITEM[j].BUSTM = "";
                        _InputParameters.T_ITEM[j].BUSTW = "";
                        _InputParameters.T_ITEM[j].MENGU = "";
                        _InputParameters.T_ITEM[j].WERTU = "";
                        _InputParameters.T_ITEM[j].LBKUM = 0;
                        _InputParameters.T_ITEM[j].SALK3 = 0;
                        _InputParameters.T_ITEM[j].VPRSV = "";
                        _InputParameters.T_ITEM[j].FKBER = "";
                        _InputParameters.T_ITEM[j].DABRBZ = "";
                        _InputParameters.T_ITEM[j].VKWRA = 0;
                        _InputParameters.T_ITEM[j].DABRZ = "";
                        _InputParameters.T_ITEM[j].XBEAU = "";
                        _InputParameters.T_ITEM[j].LSMEH = "";
                        _InputParameters.T_ITEM[j].KZBWS = "";
                        _InputParameters.T_ITEM[j].QINSPST = "";
                        _InputParameters.T_ITEM[j].URZEI = "";
                        _InputParameters.T_ITEM[j].J_1BEXBASE = 0;
                        _InputParameters.T_ITEM[j].MWSKZ = "";
                        _InputParameters.T_ITEM[j].TXJCD = "";
                        _InputParameters.T_ITEM[j].EMATN = "";
                        _InputParameters.T_ITEM[j].J_1AGIRUPD = "";
                        _InputParameters.T_ITEM[j].VKMWS = "";
                        _InputParameters.T_ITEM[j].BERKZ = "";
                        _InputParameters.T_ITEM[j].MAT_KDAUF = "";
                        _InputParameters.T_ITEM[j].MAT_KDPOS = "";
                        _InputParameters.T_ITEM[j].MAT_PSPNR = "";
                        _InputParameters.T_ITEM[j].XWOFF = "";
                        _InputParameters.T_ITEM[j].BEMOT = "";
                        _InputParameters.T_ITEM[j].PRZNR = "";
                        _InputParameters.T_ITEM[j].LLIEF = "";
                        _InputParameters.T_ITEM[j].LSTAR = "";
                        _InputParameters.T_ITEM[j].XOBEW = "";
                        _InputParameters.T_ITEM[j].GRANT_NBR = "";
                        _InputParameters.T_ITEM[j].ZUSTD_T156M = "";
                        _InputParameters.T_ITEM[j].SPE_GTS_STOCK_TY = "";
                        _InputParameters.T_ITEM[j].KBLNR = "";
                        _InputParameters.T_ITEM[j].KBLPOS = "";
                        _InputParameters.T_ITEM[j].XMACC = "";
                        _InputParameters.T_ITEM[j].VGART_MKPF = "";
                        _InputParameters.T_ITEM[j].BUDAT_MKPF = "";
                        _InputParameters.T_ITEM[j].CPUDT_MKPF = "";
                        _InputParameters.T_ITEM[j].USNAM_MKPF = "";
                        _InputParameters.T_ITEM[j].XBLNR_MKPF = "";
                        _InputParameters.T_ITEM[j].TCODE2_MKPF = "";
                        _InputParameters.T_ITEM[j].VBELN_IM = "";
                        _InputParameters.T_ITEM[j].VBELP_IM = "";
                        _InputParameters.T_ITEM[j].SGT_SCAT = "";
                        _InputParameters.T_ITEM[j].SGT_UMSCAT = "";
                        _InputParameters.T_ITEM[j].SGT_RCAT = "";
                        _InputParameters.T_ITEM[j].ITEM = "";
                        _InputParameters.T_ITEM[j].MDSLIP = "";
                        _InputParameters.T_ITEM[j].R9NUM = "";
                        _InputParameters.T_ITEM[j].ENTRY_UOM_ISO = "";
                        _InputParameters.T_ITEM[j].MFG_DATE = "";
                        _InputParameters.T_ITEM[j].EXP_DATE = "";
                        _InputParameters.T_ITEM[j].VENDOR_BATCH_NUMBER = "";
                        _InputParameters.T_ITEM[j].ITEM_OK = "";
                        _InputParameters.T_ITEM[j].AR_NUMBER = "";
                        _InputParameters.T_ITEM[j].BIN_LOCATION = "";

                        _InputParameters.RETURN[j] = new WS_ManNo.BAPIRET2();
                        _InputParameters.RETURN[j].FIELD = "";
                        _InputParameters.RETURN[j].ID = "";
                        _InputParameters.RETURN[j].LOG_MSG_NO = "";
                        _InputParameters.RETURN[j].LOG_NO = "";
                        _InputParameters.RETURN[j].MESSAGE = "";
                        _InputParameters.RETURN[j].MESSAGE_V1 = "";
                        _InputParameters.RETURN[j].MESSAGE_V2 = "";
                        _InputParameters.RETURN[j].MESSAGE_V3 = "";
                        _InputParameters.RETURN[j].MESSAGE_V4 = "";
                        _InputParameters.RETURN[j].NUMBER = "";
                        _InputParameters.RETURN[j].PARAMETER = "";
                        _InputParameters.RETURN[j].ROW = 0;
                        _InputParameters.RETURN[j].SYSTEM = "";
                        _InputParameters.RETURN[j].TYPE = "";
                        #endregion

                        _InputParameters.IM_HEADER.PLANT = clsStandards.current_Plant();
                        _InputParameters.IM_HEADER.PURCHASE_ORDER_NO = clsStandards.filter(GrGRNDetails.Rows[i].Cells[2].Text.Trim());
                        _InputParameters.IM_HEADER.DOC_DATE = System.DateTime.Now.ToString("yyyy-MM-dd");
                        _InputParameters.IM_HEADER.GATE_ENTRY_NO = clsStandards.filter(GrGRNDetails.Rows[i].Cells[12].Text.Trim());
                        _InputParameters.IM_HEADER.TDATE = clsStandards.filter(GrGRNDetails.Rows[i].Cells[31].Text.Trim());
                        _InputParameters.IM_HEADER.GM_CODE = clsStandards.filter(ConfigurationManager.AppSettings["GM_CODE"].ToString());
                        _InputParameters.IM_HEADER.GROSS_WT = clsStandards.filter(GrGRNDetails.Rows[i].Cells[21].Text.Trim());
                        _InputParameters.IM_HEADER.HEADER_TEXT = clsStandards.filter(GrGRNDetails.Rows[i].Cells[38].Text.Trim());
                        _InputParameters.IM_HEADER.LIC_NO = "11223";
                        _InputParameters.IM_HEADER.MODE_OF_TRANSPORT = clsStandards.filter(GrGRNDetails.Rows[i].Cells[16].Text.Trim().Substring(0, 4));
                        _InputParameters.IM_HEADER.NET_WT = clsStandards.filter(GrGRNDetails.Rows[i].Cells[23].Text.Trim());
                        _InputParameters.IM_HEADER.POSTING_DATE = System.DateTime.Now.ToString("yyyy-MM-dd");
                        _InputParameters.IM_HEADER.TARE_WT = clsStandards.filter(GrGRNDetails.Rows[i].Cells[22].Text.Trim());
                        _InputParameters.IM_HEADER.TRANSPORTER = clsStandards.filter(GrGRNDetails.Rows[i].Cells[11].Text.Trim());
                        _InputParameters.IM_HEADER.TRUCK = clsStandards.filter(GrGRNDetails.Rows[i].Cells[17].Text.Trim());
                        _InputParameters.IM_HEADER.UNAME = txtSapUser.Text.Trim();
                        if (!string.IsNullOrEmpty(clsStandards.filter(GrGRNDetails.Rows[i].Cells[8].Text.Trim())))
                        {
                            _InputParameters.IM_HEADER.AIRWAY_BILL_NO = clsStandards.filter(GrGRNDetails.Rows[i].Cells[8].Text.Trim());
                            _InputParameters.IM_HEADER.AIRWAY_BILL_DATE = clsStandards.filter(GrGRNDetails.Rows[i].Cells[10].Text.Trim());
                        }
                        else
                        {
                            _InputParameters.IM_HEADER.WAY_BILL_NOT_REQUIRED = "X";
                        }

                        _InputParameters.IM_HEADER.MAN_TAX = "X";
                        _InputParameters.IM_HEADER.GR_GI_SLIP_NO = clsStandards.filter(GrGRNDetails.Rows[i].Cells[37].Text.Trim());
                        _InputParameters.IM_HEADER.GR_RR_NO = clsStandards.filter(GrGRNDetails.Rows[i].Cells[36].Text.Trim());
                        _InputParameters.IM_HEADER.DELIVERY_NOTE = clsStandards.filter(GrGRNDetails.Rows[i].Cells[39].Text.Trim());
                        _InputParameters.T_ITEM[j].XBLNR_MKPF = clsStandards.filter(GrGRNDetails.Rows[i].Cells[39].Text.Trim());
                        _InputParameters.T_ITEM[j].MATNR = clsStandards.filter(GrGRNDetails.Rows[i].Cells[5].Text.Trim());

                        _InputParameters.T_ITEM[j].BWART = ConfigurationManager.AppSettings["ManNo_MovementType"].ToString(); // MovementType
                        _InputParameters.T_ITEM[j].WERKS = clsStandards.current_Plant();
                        // _InputParameters.T_ITEM[i].LIFNR = ""; // Transporter Code
                        _InputParameters.T_ITEM[j].ERFMG = Convert.ToDecimal(clsStandards.filter(GrGRNDetails.Rows[i].Cells[13].Text.Trim())); //AcceptedQty
                        _InputParameters.T_ITEM[j].EBELN = txtDocumentNo.Text.Trim(); //Purchase order no
                        _InputParameters.T_ITEM[j].EBELP = clsStandards.filter(GrGRNDetails.Rows[i].Cells[4].Text.Trim()); //lineitem no
                        int lineitem = 0;
                        //clsStandards.WriteLog(this, new Exception(" started 2 "), clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 0, true);

                        if (j < 9)
                        {
                            lineitem = j;
                            lineitem++;
                            _InputParameters.T_ITEM[j].ITEM = "00000" + lineitem.ToString(); // clsStandards.filter(GrGRNDetails.Rows[i].Cells[4].Text.Trim());//000001
                            _InputParameters.T_ITEM[j].ZEILE = "000" + lineitem.ToString();
                        }
                        else
                        {
                            lineitem = j;
                            lineitem++;
                            _InputParameters.T_ITEM[j].ITEM = "0000" + lineitem.ToString(); // clsStandards.filter(GrGRNDetails.Rows[i].Cells[4].Text.Trim());//000001
                            _InputParameters.T_ITEM[j].ZEILE = "00" + lineitem.ToString();
                        }
                        _InputParameters.T_ITEM[j].ERFME = clsStandards.filter(GrGRNDetails.Rows[i].Cells[9].Text.Trim());
                        _InputParameters.T_ITEM[j].KZBEW = "B";
                        //_InputParameters.T_ITEM[i].PACK = ""; //No of Packs
                        _InputParameters.T_ITEM[j].VENDOR_BATCH_NO = clsStandards.filter(GrGRNDetails.Rows[i].Cells[26].Text.Trim());
                        _InputParameters.T_ITEM[j].HSDAT = clsStandards.filter(GrGRNDetails.Rows[i].Cells[14].Text.Trim());
                        _InputParameters.T_ITEM[j].KOSTL = clsStandards.filter(GrGRNDetails.Rows[i].Cells[33].Text.Trim());

                        //_InputParameters.T_ITEM[i].LSMNG = Convert.ToDecimal(clsStandards.filter(GrGRNDetails.Rows[i].Cells[13].Text.Trim())); //AcceptedQty

                        if (clsStandards.filter(GrGRNDetails.Rows[i].Cells[29].Text.Trim()).ToString() == "")
                        {
                            _InputParameters.T_ITEM[j].BWTAR = "";
                        }
                        else
                        {
                            _InputParameters.T_ITEM[j].BWTAR = clsStandards.filter(GrGRNDetails.Rows[i].Cells[29].Text.Trim());
                        }

                        _InputParameters.T_ITEM[j].VFDAT = clsStandards.filter(GrGRNDetails.Rows[i].Cells[15].Text.Trim());
                        _InputParameters.T_ITEM[j].SGTXT = clsStandards.filter(GrGRNDetails.Rows[i].Cells[32].Text.Trim()); //Item Description
                        _InputParameters.T_ITEM[j].GJAHR = System.DateTime.Now.Year.ToString(); //year
                        _InputParameters.T_ITEM[j].MANUFACTURE_NAME = clsStandards.filter(GrGRNDetails.Rows[i].Cells[28].Text.Trim()); ;
                        _InputParameters.T_ITEM[j].PACK_QTY = Convert.ToDecimal(clsStandards.filter(GrGRNDetails.Rows[i].Cells[24].Text.Trim()));
                        _InputParameters.T_ITEM[j].MAKTX = clsStandards.filter(GrGRNDetails.Rows[i].Cells[32].Text.Trim()); // Item Desc
                        _InputParameters.T_ITEM[j].CPUTM_MKPF = "";
                        _InputParameters.T_ITEM[j].CERTIFICATE_ENCLOSED = "";
                        _InputParameters.IM_HEADER.NO_OF_PACKS = clsStandards.filter(GrGRNDetails.Rows[i].Cells[18].Text.Trim());
                        _InputParameters.T_ITEM[j].PACK = clsStandards.filter(GrGRNDetails.Rows[i].Cells[18].Text.Trim());
                        _InputParameters.T_ITEM[j].CONTAINER = Convert.ToDecimal(clsStandards.filter(GrGRNDetails.Rows[i].Cells[18].Text.Trim()));

                    }
                }
                var req = new MemoryStream();
                var reqxml = new System.Xml.Serialization.XmlSerializer(_InputParameters.GetType());
                reqxml.Serialize(req, _InputParameters);
                string strxml = Encoding.UTF8.GetString(req.ToArray());
                clsStandards.WriteSoapXML(strxml, "MaterialPosting - Request  ", clsStandards.GetCurrentPageName().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());

                _Response = _ManNo.ZDMF002(_InputParameters);
                _OutputParameters1 = _Response.EX_MATERIAL_GRN;
                _OutputParameters3 = _Response.T_ITEM;
                _Returnparameter = _Response.RETURN;

                var res = new MemoryStream();
                var resxml = new System.Xml.Serialization.XmlSerializer(_Response.GetType());
                resxml.Serialize(res, _Response);
                string xml = Encoding.UTF8.GetString(res.ToArray());
                clsStandards.WriteSoapXML(xml, "MaterialPosting - Response", clsStandards.GetCurrentPageName().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());

                string message = "";
                if (_Returnparameter.Length > 0)
                {
                    for (int a = 0; a < _Returnparameter.Length; a++)
                    {

                        string strPROCESSORDERNO = txtDocumentNo.Text.Trim();
                        string strMESSAGE = _Returnparameter[a].MESSAGE.ToString();
                        string strMESSAGE_V1 = _Returnparameter[a].MESSAGE_V1.ToString();
                        string strMESSAGE_V2 = _Returnparameter[a].MESSAGE_V2.ToString();
                        string strMESSAGE_V3 = _Returnparameter[a].MESSAGE_V3.ToString();
                        string strMESSAGE_V4 = _Returnparameter[a].MESSAGE_V4.ToString();
                        string strROW = _Returnparameter[a].ROW.ToString();
                        string strPLANT = clsStandards.current_Plant(); ;
                        string strCREATEDBY = clsStandards.current_Username();
                        objBL.BL__Dispensing_Posting_Error(strPROCESSORDERNO, strMESSAGE, strMESSAGE_V1, strMESSAGE_V2, strMESSAGE_V3, strMESSAGE_V4, strROW, strPLANT, strCREATEDBY, "MaNNO POST");
                        message += strMESSAGE;
                        if (message.ToUpper().Contains("VALUATION TYPE"))
                        {
                            btnAdd_ValutionType.Visible = true;
                        }
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
                //if (_OutputParameters1.Length > 0)
                //{
                //    for (int m = 0; m < _OutputParameters1.Length; m++)
                //    {
                //        strManno = _OutputParameters1[m].MAN_NO;
                //        Sapbatch = _OutputParameters1[m].SAP_BATCH;
                //    }
                //}
                //else
                //{
                //    strManno = "";
                //    Sapbatch = "";
                //}


                clsStandards.WriteLog(this, new Exception("lenth :" + _OutputParameters1.Length), clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 0, false);

                for (int i = 0; i < GrGRNDetails.Rows.Count; i++)
                {
                    CheckBox cb = (CheckBox)GrGRNDetails.Rows[i].FindControl("chkSelect");
                    if (cb.Checked == true)
                    {
                        ObjPL.strItemCode = clsStandards.filter(GrGRNDetails.Rows[i].Cells[5].Text.Trim());
                        ObjPL.strVendorBatchNo = clsStandards.filter(GrGRNDetails.Rows[i].Cells[26].Text.Trim());
                        ObjPL.strLineItemNo = clsStandards.filter(GrGRNDetails.Rows[i].Cells[4].Text.Trim());
                        if (_OutputParameters1.Length > 0)
                        {
                            for (int m = 0; m < _OutputParameters1.Length; m++)
                            {
                                strItemcode = _OutputParameters1[m].ITEM_CODE;
                                strLineItemNo = ObjPL.strLineItemNo;
                                if (strItemcode.Trim() == ObjPL.strItemCode.Trim())
                                {
                                    if (CheckManno(_OutputParameters1[m].MAN_NO.Trim(), strItemcode.Trim(), _OutputParameters1[m].SAP_BATCH.Trim(), strLineItemNo, _OutputParameters1[m].VENDOR_BATCH_NO.Trim()))
                                    {
                                        strManno = _OutputParameters1[m].MAN_NO.Trim();
                                        Sapbatch = _OutputParameters1[m].SAP_BATCH.Trim();
                                        break;
                                    }
                                    else
                                    {
                                        strManno = "";
                                        Sapbatch = "";
                                    }
                                }
                            }
                        }
                        else
                        {
                            strManno = "";
                            Sapbatch = "";
                        }

                        ObjPL.strPlant = clsStandards.filter(GrGRNDetails.Rows[i].Cells[1].Text.Trim());
                        ObjPL.strPurchaseOrderNo = clsStandards.filter(GrGRNDetails.Rows[i].Cells[2].Text.Trim());
                        ObjPL.strPurchaseOrderDate = clsStandards.filter(GrGRNDetails.Rows[i].Cells[3].Text.Trim());
                        ObjPL.strVendorName = clsStandards.filter(GrGRNDetails.Rows[i].Cells[25].Text.Trim());
                        ObjPL.strItemType = clsStandards.filter(GrGRNDetails.Rows[i].Cells[6].Text.Trim());
                        ObjPL.strOrderQty = Convert.ToDecimal(clsStandards.filter(GrGRNDetails.Rows[i].Cells[7].Text.Trim()));
                        ObjPL.strUom = clsStandards.filter(GrGRNDetails.Rows[i].Cells[9].Text.Trim());
                        ObjPL.strAcceptedQty = Convert.ToDecimal(clsStandards.filter(GrGRNDetails.Rows[i].Cells[13].Text.Trim()));
                        ObjPL.strNoOfContainer = Convert.ToDecimal(clsStandards.filter(GrGRNDetails.Rows[i].Cells[18].Text.Trim()));
                        ObjPL.strPackQty = Convert.ToDecimal(clsStandards.filter(GrGRNDetails.Rows[i].Cells[24].Text.Trim()));
                        ObjPL.strGateEntryNo = clsStandards.filter(GrGRNDetails.Rows[i].Cells[12].Text.Trim());
                        ObjPL.strManfactureName = clsStandards.filter(GrGRNDetails.Rows[i].Cells[28].Text.Trim()); ;
                        ObjPL.strVendorBatchNo = clsStandards.filter(GrGRNDetails.Rows[i].Cells[26].Text.Trim());
                        ObjPL.strMFGDate = clsStandards.filter(GrGRNDetails.Rows[i].Cells[14].Text.Trim());
                        ObjPL.strEXPDate = clsStandards.filter(GrGRNDetails.Rows[i].Cells[15].Text.Trim());
                        ObjPL.strMANNo = strManno;
                        ObjPL.strSapBatchNo = Sapbatch;
                        ObjPL.strUser = clsStandards.current_Username();
                        ObjPL.strTareWt = clsStandards.filter(GrGRNDetails.Rows[i].Cells[22].Text.Trim()) == "" ? 0 : Convert.ToDecimal(clsStandards.filter(GrGRNDetails.Rows[i].Cells[22].Text.Trim()));
                        ObjPL.strGrossWt = clsStandards.filter(GrGRNDetails.Rows[i].Cells[21].Text.Trim()) == "" ? 0 : Convert.ToDecimal(clsStandards.filter(GrGRNDetails.Rows[i].Cells[21].Text.Trim()));
                        ObjPL.strNetWt = clsStandards.filter(GrGRNDetails.Rows[i].Cells[23].Text.Trim()) == "" ? 0 : Convert.ToDecimal(clsStandards.filter(GrGRNDetails.Rows[i].Cells[23].Text.Trim()));
                        ObjPL.Purchase_Ref = clsStandards.filter(GrGRNDetails.Rows[i].Cells[30].Text.Trim()) == "" ? 0 : Convert.ToInt32(clsStandards.filter(GrGRNDetails.Rows[i].Cells[30].Text.Trim()));
                        ObjPL.strMessgae = message;
                        ObjPL.strStroageLoc = clsStandards.filter(GrGRNDetails.Rows[i].Cells[34].Text.Trim()).ToUpper();

                        string Result = objBL.BL_Insert_ManNo(ObjPL);

                        GridViewRow gr = (GridViewRow)GrGRNDetails.Rows[i];
                        if (message.Contains("SUCCESS"))
                        {
                            cb.Enabled = false;
                            cb.Checked = false;
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
                    clsStandards.WriteLog(this, new Exception("Material Posted Successfully , Document No :  " + strManno), clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 0, true);
                }
                else
                {
                    clsStandards.WriteLog(this, new Exception("SAP Message : " + message), clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 0, true);
                }
            }
        }
        catch (Exception ex)
        {
            clsStandards.WriteLog(this, new Exception(ex.ToString()), clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 0, true);
            return;
        }
    }

    protected void btnADVPost_Click(object sender, EventArgs e)
    {
        PL_ManNo ObjPL = new PL_ManNo();
        BL_GrnPrinting objBL = new BL_GrnPrinting();
        string strManno = "";
        string Sapbatch = "";
        string strItemcode = "";
        string strLineItemNo = "";
        XDocument ResultXML = new XDocument();
        try
        {
            if (!Chk_Checkbox())
            {
                clsStandards.WriteLog(this, new Exception(" Please Check the Item from List "), clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 0, true);
                return;
            }
            if (GrGRNDetails.Rows.Count > 0)
            
            {
                ADV_POST.ZWS_ZDF002_GRN _ManNo = new ADV_POST.ZWS_ZDF002_GRN();
                ADV_POST.ZDMF002 _InputParameters = new ADV_POST.ZDMF002();
                ADV_POST.ZDMF002Response _Response = new ADV_POST.ZDMF002Response();
                ADV_POST.ZDBMS006[] _OutputParameters1 = new ADV_POST.ZDBMS006[0];
                ADV_POST.ZDBMS011[] _OutputParameters2 = new ADV_POST.ZDBMS011[0];
                ADV_POST.ZDBMS012[] _OutputParameters3 = new ADV_POST.ZDBMS012[0];
                ADV_POST.BAPIRET2[] _Returnparameter = new ADV_POST.BAPIRET2[0];
                _InputParameters.IM_HEADER = new ADV_POST.ZDBMS011();

                _ManNo.Proxy = new WebProxy();
                _ManNo.Credentials = new NetworkCredential(txtSapUser.Text.Trim(), txtSapPass.Text.Trim());
                _ManNo.Proxy.Credentials = new NetworkCredential(txtSapUser.Text.Trim(), txtSapPass.Text.Trim());

                int TotalCheckboxPO = 0;
                for (int k = 0; k < GrGRNDetails.Rows.Count; k++)
                {
                    CheckBox cbk = (CheckBox)GrGRNDetails.Rows[k].FindControl("chkSelect");
                    if (cbk.Checked == true)
                    {
                        TotalCheckboxPO++;
                    }
                }
                _InputParameters.T_ITEM = new ADV_POST.ZDBMS012[TotalCheckboxPO];
                _InputParameters.RETURN = new ADV_POST.BAPIRET2[TotalCheckboxPO];
                clsStandards.WriteLog(this, new Exception(" started "), clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 0, true);

                #region
                _InputParameters.IM_HEADER.WAY_REQ = "";
                _InputParameters.IM_HEADER.AIRWAY_BILL_DATE = "";
                _InputParameters.IM_HEADER.AIRWAY_BILL_NO = "";
                _InputParameters.IM_HEADER.SALTAX = "";
                _InputParameters.IM_HEADER.WAY_DT = "";
                _InputParameters.IM_HEADER.WAY_REQ = "";
                _InputParameters.IM_HEADER.FNUM = "";
                _InputParameters.IM_HEADER.BILL_OF_LADING = "";
                _InputParameters.IM_HEADER.GR_GI_SLIP_NO = "";
                _InputParameters.IM_HEADER.DELIVERY_NOTE = "";
                _InputParameters.IM_HEADER.EXCISE_INVOICE = "";
                _InputParameters.IM_HEADER.EXCISE_INVOICED_DATE = "";
                _InputParameters.IM_HEADER.EXCISE_GROUP = "";
                _InputParameters.IM_HEADER.TR6 = "";
                _InputParameters.IM_HEADER.TRDAT = "";
                _InputParameters.IM_HEADER.AIRWAY_BILL_NO = "";
                _InputParameters.IM_HEADER.AIRWAY_BILL_DATE = "";
                _InputParameters.IM_HEADER.BILL_OF_LADING = "";
                _InputParameters.IM_HEADER.DELIVERY_NOTE = "";
                _InputParameters.IM_HEADER.EXCISE_GROUP = "";
                _InputParameters.IM_HEADER.EXCISE_INVOICE = "";
                _InputParameters.IM_HEADER.EXCISE_INVOICED_DATE = "";
                _InputParameters.IM_HEADER.FNUM = "";
                _InputParameters.IM_HEADER.GR_GI_SLIP_NO = "";
                _InputParameters.IM_HEADER.GR_RR_NO = "";
                _InputParameters.IM_HEADER.NO_OF_PACKS = "";
                _InputParameters.IM_HEADER.TR6 = "";
                _InputParameters.IM_HEADER.TRDAT = "";
                _InputParameters.IM_HEADER.WAY_DT = "";
                _InputParameters.IM_HEADER.WAY_REQ = "";

                #endregion

                int j = -1;
                for (int i = 0; i < GrGRNDetails.Rows.Count; i++)
                {
                    CheckBox cb = (CheckBox)GrGRNDetails.Rows[i].FindControl("chkSelect");
                    if (cb.Checked == true)
                    {
                        j++;
                        #region
                        //clsStandards.WriteLog(this, new Exception("started 1 "), clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 0, true);

                        _InputParameters.T_ITEM[j] = new ADV_POST.ZDBMS012();
                        _InputParameters.T_ITEM[j].MBLNR = "";
                        _InputParameters.T_ITEM[j].MJAHR = "";
                        _InputParameters.T_ITEM[j].ZEILE = "";
                        _InputParameters.T_ITEM[j].LINE_ID = "";
                        _InputParameters.T_ITEM[j].PARENT_ID = "";
                        _InputParameters.T_ITEM[j].LINE_DEPTH = "";
                        _InputParameters.T_ITEM[j].MAA_URZEI = "";
                        _InputParameters.T_ITEM[j].XAUTO = "";
                        _InputParameters.T_ITEM[j].LGORT = "";
                        _InputParameters.T_ITEM[j].CHARG = "";
                        _InputParameters.T_ITEM[j].INSMK = "";
                        _InputParameters.T_ITEM[j].ZUSCH = "";
                        _InputParameters.T_ITEM[j].ZUSTD = "";
                        _InputParameters.T_ITEM[j].LIFNR = "";
                        _InputParameters.T_ITEM[j].KUNNR = "";
                        _InputParameters.T_ITEM[j].KDAUF = "";
                        _InputParameters.T_ITEM[j].KDPOS = "";
                        _InputParameters.T_ITEM[j].KDEIN = "";
                        _InputParameters.T_ITEM[j].PLPLA = "";
                        _InputParameters.T_ITEM[j].SHKZG = "";
                        _InputParameters.T_ITEM[j].WAERS = "";
                        _InputParameters.T_ITEM[j].DMBTR = 0;
                        _InputParameters.T_ITEM[j].BNBTR = 0;
                        _InputParameters.T_ITEM[j].BUALT = 0;
                        _InputParameters.T_ITEM[j].SHKUM = "";
                        _InputParameters.T_ITEM[j].DMBUM = 0;
                        _InputParameters.T_ITEM[j].MENGE = 0;
                        _InputParameters.T_ITEM[j].MEINS = "";
                        _InputParameters.T_ITEM[j].BPMNG = 0;
                        _InputParameters.T_ITEM[j].BPRME = "";
                        _InputParameters.T_ITEM[j].LFBJA = "";
                        _InputParameters.T_ITEM[j].LFBNR = "";
                        _InputParameters.T_ITEM[j].LFPOS = "";
                        _InputParameters.T_ITEM[j].SJAHR = "";
                        _InputParameters.T_ITEM[j].SMBLN = "";
                        _InputParameters.T_ITEM[j].SMBLP = "";
                        _InputParameters.T_ITEM[j].ELIKZ = "";
                        _InputParameters.T_ITEM[j].EQUNR = "";
                        _InputParameters.T_ITEM[j].WEMPF = "";
                        _InputParameters.T_ITEM[j].ABLAD = "";
                        _InputParameters.T_ITEM[j].GSBER = "";
                        _InputParameters.T_ITEM[j].KOKRS = "";
                        _InputParameters.T_ITEM[j].PARGB = "";
                        _InputParameters.T_ITEM[j].PARBU = "";
                        _InputParameters.T_ITEM[j].KOSTL = "";
                        _InputParameters.T_ITEM[j].PROJN = "";
                        _InputParameters.T_ITEM[j].AUFNR = "";
                        _InputParameters.T_ITEM[j].ANLN1 = "";
                        _InputParameters.T_ITEM[j].ANLN2 = "";
                        _InputParameters.T_ITEM[j].XSKST = "";
                        _InputParameters.T_ITEM[j].XSAUF = "";
                        _InputParameters.T_ITEM[j].XSPRO = "";
                        _InputParameters.T_ITEM[j].XSERG = "";
                        _InputParameters.T_ITEM[j].XRUEM = "";
                        _InputParameters.T_ITEM[j].XRUEJ = "";
                        _InputParameters.T_ITEM[j].BUKRS = "";
                        _InputParameters.T_ITEM[j].BELNR = "";
                        _InputParameters.T_ITEM[j].BUZEI = "";
                        _InputParameters.T_ITEM[j].BELUM = "";
                        _InputParameters.T_ITEM[j].BUZUM = "";
                        _InputParameters.T_ITEM[j].RSNUM = "";
                        _InputParameters.T_ITEM[j].RSPOS = "";
                        _InputParameters.T_ITEM[j].KZEAR = "";
                        _InputParameters.T_ITEM[j].PBAMG = 0;
                        _InputParameters.T_ITEM[j].KZSTR = "";
                        _InputParameters.T_ITEM[j].UMMAT = "";
                        _InputParameters.T_ITEM[j].UMWRK = "";
                        _InputParameters.T_ITEM[j].UMLGO = "";
                        _InputParameters.T_ITEM[j].UMCHA = "";
                        _InputParameters.T_ITEM[j].UMZST = "";
                        _InputParameters.T_ITEM[j].UMZUS = "";
                        _InputParameters.T_ITEM[j].UMBAR = "";
                        _InputParameters.T_ITEM[j].UMSOK = "";
                        _InputParameters.T_ITEM[j].KZVBR = "";
                        _InputParameters.T_ITEM[j].KZZUG = "";
                        _InputParameters.T_ITEM[j].WEUNB = "";
                        _InputParameters.T_ITEM[j].PALAN = 0;
                        _InputParameters.T_ITEM[j].LGNUM = "";
                        _InputParameters.T_ITEM[j].LGTYP = "";
                        _InputParameters.T_ITEM[j].LGPLA = "";
                        _InputParameters.T_ITEM[j].BESTQ = "";
                        _InputParameters.T_ITEM[j].BWLVS = "";
                        _InputParameters.T_ITEM[j].TBNUM = "";
                        _InputParameters.T_ITEM[j].TBPOS = "";
                        _InputParameters.T_ITEM[j].XBLVS = "";
                        _InputParameters.T_ITEM[j].VSCHN = "";
                        _InputParameters.T_ITEM[j].NSCHN = "";
                        _InputParameters.T_ITEM[j].DYPLA = "";
                        _InputParameters.T_ITEM[j].UBNUM = "";
                        _InputParameters.T_ITEM[j].TBPRI = "";
                        _InputParameters.T_ITEM[j].TANUM = "";
                        _InputParameters.T_ITEM[j].WEANZ = "";
                        _InputParameters.T_ITEM[j].GRUND = "";
                        _InputParameters.T_ITEM[j].EVERS = "";
                        _InputParameters.T_ITEM[j].EVERE = "";
                        _InputParameters.T_ITEM[j].IMKEY = "";
                        _InputParameters.T_ITEM[j].KSTRG = "";
                        _InputParameters.T_ITEM[j].PAOBJNR = "";
                        _InputParameters.T_ITEM[j].PRCTR = "";
                        _InputParameters.T_ITEM[j].PS_PSP_PNR = "";
                        _InputParameters.T_ITEM[j].NPLNR = "";
                        _InputParameters.T_ITEM[j].AUFPL = "";
                        _InputParameters.T_ITEM[j].APLZL = "";
                        _InputParameters.T_ITEM[j].AUFPS = "";
                        _InputParameters.T_ITEM[j].VPTNR = "";
                        _InputParameters.T_ITEM[j].FIPOS = "";
                        _InputParameters.T_ITEM[j].SAKTO = "";
                        _InputParameters.T_ITEM[j].BSTMG = 0;
                        _InputParameters.T_ITEM[j].BSTME = "";
                        _InputParameters.T_ITEM[j].XWSBR = "";
                        _InputParameters.T_ITEM[j].EMLIF = "";
                        _InputParameters.T_ITEM[j].ZZ_REP_MAT = "";
                        _InputParameters.T_ITEM[j].EXBWR = 0;
                        _InputParameters.T_ITEM[j].VKWRT = 0;
                        _InputParameters.T_ITEM[j].AKTNR = "";
                        _InputParameters.T_ITEM[j].ZEKKN = "";
                        _InputParameters.T_ITEM[j].CUOBJ_CH = "";
                        _InputParameters.T_ITEM[j].EXVKW = j;
                        _InputParameters.T_ITEM[j].PPRCTR = "";
                        _InputParameters.T_ITEM[j].RSART = "";
                        _InputParameters.T_ITEM[j].GEBER = "";
                        _InputParameters.T_ITEM[j].FISTL = "";
                        _InputParameters.T_ITEM[j].MATBF = "";
                        _InputParameters.T_ITEM[j].UMMAB = "";
                        _InputParameters.T_ITEM[j].BUSTM = "";
                        _InputParameters.T_ITEM[j].BUSTW = "";
                        _InputParameters.T_ITEM[j].MENGU = "";
                        _InputParameters.T_ITEM[j].WERTU = "";
                        _InputParameters.T_ITEM[j].LBKUM = 0;
                        _InputParameters.T_ITEM[j].SALK3 = 0;
                        _InputParameters.T_ITEM[j].VPRSV = "";
                        _InputParameters.T_ITEM[j].FKBER = "";
                        _InputParameters.T_ITEM[j].DABRBZ = "";
                        _InputParameters.T_ITEM[j].VKWRA = 0;
                        _InputParameters.T_ITEM[j].DABRZ = "";
                        _InputParameters.T_ITEM[j].XBEAU = "";
                        _InputParameters.T_ITEM[j].LSMEH = "";
                        _InputParameters.T_ITEM[j].KZBWS = "";
                        _InputParameters.T_ITEM[j].QINSPST = "";
                        _InputParameters.T_ITEM[j].URZEI = "";
                        _InputParameters.T_ITEM[j].J_1BEXBASE = 0;
                        _InputParameters.T_ITEM[j].MWSKZ = "";
                        _InputParameters.T_ITEM[j].TXJCD = "";
                        _InputParameters.T_ITEM[j].EMATN = "";
                        _InputParameters.T_ITEM[j].J_1AGIRUPD = "";
                        _InputParameters.T_ITEM[j].VKMWS = "";
                        _InputParameters.T_ITEM[j].BERKZ = "";
                        _InputParameters.T_ITEM[j].MAT_KDAUF = "";
                        _InputParameters.T_ITEM[j].MAT_KDPOS = "";
                        _InputParameters.T_ITEM[j].MAT_PSPNR = "";
                        _InputParameters.T_ITEM[j].XWOFF = "";
                        _InputParameters.T_ITEM[j].BEMOT = "";
                        _InputParameters.T_ITEM[j].PRZNR = "";
                        _InputParameters.T_ITEM[j].LLIEF = "";
                        _InputParameters.T_ITEM[j].LSTAR = "";
                        _InputParameters.T_ITEM[j].XOBEW = "";
                        _InputParameters.T_ITEM[j].GRANT_NBR = "";
                        _InputParameters.T_ITEM[j].ZUSTD_T156M = "";
                        _InputParameters.T_ITEM[j].SPE_GTS_STOCK_TY = "";
                        _InputParameters.T_ITEM[j].KBLNR = "";
                        _InputParameters.T_ITEM[j].KBLPOS = "";
                        _InputParameters.T_ITEM[j].XMACC = "";
                        _InputParameters.T_ITEM[j].VGART_MKPF = "";
                        _InputParameters.T_ITEM[j].BUDAT_MKPF = "";
                        _InputParameters.T_ITEM[j].CPUDT_MKPF = "";
                        _InputParameters.T_ITEM[j].USNAM_MKPF = "";
                        _InputParameters.T_ITEM[j].XBLNR_MKPF = "";
                        _InputParameters.T_ITEM[j].TCODE2_MKPF = "";
                        _InputParameters.T_ITEM[j].VBELN_IM = "";
                        _InputParameters.T_ITEM[j].VBELP_IM = "";
                        _InputParameters.T_ITEM[j].SGT_SCAT = "";
                        _InputParameters.T_ITEM[j].SGT_UMSCAT = "";
                        _InputParameters.T_ITEM[j].SGT_RCAT = "";
                        _InputParameters.T_ITEM[j].ITEM = "";
                        _InputParameters.T_ITEM[j].MDSLIP = "";
                        _InputParameters.T_ITEM[j].R9NUM = "";
                        _InputParameters.T_ITEM[j].ENTRY_UOM_ISO = "";
                        _InputParameters.T_ITEM[j].MFG_DATE = "";
                        _InputParameters.T_ITEM[j].EXP_DATE = "";
                        _InputParameters.T_ITEM[j].VENDOR_BATCH_NUMBER = "";
                        _InputParameters.T_ITEM[j].ITEM_OK = "";
                        _InputParameters.T_ITEM[j].AR_NUMBER = "";
                        _InputParameters.T_ITEM[j].BIN_LOCATION = "";
                        _InputParameters.T_ITEM[j].ADV_LIC1 = "";
                        _InputParameters.T_ITEM[j].ADV_LIC2 = "";
                        _InputParameters.T_ITEM[j].ADV_MENGE1 = 0;
                        _InputParameters.T_ITEM[j].ADV_MENGE2 = 0;

                        _InputParameters.RETURN[j] = new ADV_POST.BAPIRET2();
                        _InputParameters.RETURN[j].FIELD = "";
                        _InputParameters.RETURN[j].ID = "";
                        _InputParameters.RETURN[j].LOG_MSG_NO = "";
                        _InputParameters.RETURN[j].LOG_NO = "";
                        _InputParameters.RETURN[j].MESSAGE = "";
                        _InputParameters.RETURN[j].MESSAGE_V1 = "";
                        _InputParameters.RETURN[j].MESSAGE_V2 = "";
                        _InputParameters.RETURN[j].MESSAGE_V3 = "";
                        _InputParameters.RETURN[j].MESSAGE_V4 = "";
                        _InputParameters.RETURN[j].NUMBER = "";
                        _InputParameters.RETURN[j].PARAMETER = "";
                        _InputParameters.RETURN[j].ROW = 0;
                        _InputParameters.RETURN[j].SYSTEM = "";
                        _InputParameters.RETURN[j].TYPE = "";
                        #endregion
                        clsStandards.WriteLog(this, new Exception(" started 1"), clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 0, true);
                        _InputParameters.IM_HEADER.PLANT = clsStandards.current_Plant();
                        _InputParameters.IM_HEADER.PURCHASE_ORDER_NO = clsStandards.filter(GrGRNDetails.Rows[i].Cells[2].Text.Trim());
                        _InputParameters.IM_HEADER.DOC_DATE = System.DateTime.Now.ToString("yyyy-MM-dd");
                        _InputParameters.IM_HEADER.GATE_ENTRY_NO = clsStandards.filter(GrGRNDetails.Rows[i].Cells[12].Text.Trim());
                        _InputParameters.IM_HEADER.TDATE = clsStandards.filter(GrGRNDetails.Rows[i].Cells[31].Text.Trim());
                        _InputParameters.IM_HEADER.GM_CODE = clsStandards.filter(ConfigurationManager.AppSettings["GM_CODE"].ToString());
                        _InputParameters.IM_HEADER.GROSS_WT = clsStandards.filter(GrGRNDetails.Rows[i].Cells[21].Text.Trim());
                        _InputParameters.IM_HEADER.HEADER_TEXT = clsStandards.filter(GrGRNDetails.Rows[i].Cells[38].Text.Trim());
                        _InputParameters.IM_HEADER.LIC_NO = "11223";
                        _InputParameters.IM_HEADER.MODE_OF_TRANSPORT = clsStandards.filter(GrGRNDetails.Rows[i].Cells[16].Text.Trim().Substring(0, 4));
                        _InputParameters.IM_HEADER.NET_WT = clsStandards.filter(GrGRNDetails.Rows[i].Cells[23].Text.Trim());
                        _InputParameters.IM_HEADER.POSTING_DATE = System.DateTime.Now.ToString("yyyy-MM-dd");
                        _InputParameters.IM_HEADER.TARE_WT = clsStandards.filter(GrGRNDetails.Rows[i].Cells[22].Text.Trim());
                        _InputParameters.IM_HEADER.TRANSPORTER = clsStandards.filter(GrGRNDetails.Rows[i].Cells[11].Text.Trim());
                        _InputParameters.IM_HEADER.TRUCK = clsStandards.filter(GrGRNDetails.Rows[i].Cells[17].Text.Trim());
                        _InputParameters.IM_HEADER.UNAME = txtSapUser.Text.Trim();
                        if (!string.IsNullOrEmpty(clsStandards.filter(GrGRNDetails.Rows[i].Cells[8].Text.Trim())))
                        {
                            _InputParameters.IM_HEADER.AIRWAY_BILL_NO = clsStandards.filter(GrGRNDetails.Rows[i].Cells[8].Text.Trim());
                            _InputParameters.IM_HEADER.AIRWAY_BILL_DATE = clsStandards.filter(GrGRNDetails.Rows[i].Cells[10].Text.Trim());
                        }
                        else
                        {
                            _InputParameters.IM_HEADER.WAY_BILL_NOT_REQUIRED = "X";
                        }

                        _InputParameters.IM_HEADER.MAN_TAX = "X";
                        _InputParameters.IM_HEADER.GR_GI_SLIP_NO = clsStandards.filter(GrGRNDetails.Rows[i].Cells[37].Text.Trim());
                        _InputParameters.IM_HEADER.GR_RR_NO = clsStandards.filter(GrGRNDetails.Rows[i].Cells[36].Text.Trim());
                        _InputParameters.IM_HEADER.DELIVERY_NOTE = clsStandards.filter(GrGRNDetails.Rows[i].Cells[39].Text.Trim());
                        _InputParameters.T_ITEM[j].XBLNR_MKPF = clsStandards.filter(GrGRNDetails.Rows[i].Cells[39].Text.Trim());
                        _InputParameters.T_ITEM[j].MATNR = clsStandards.filter(GrGRNDetails.Rows[i].Cells[5].Text.Trim());

                        _InputParameters.T_ITEM[j].BWART = ConfigurationManager.AppSettings["ManNo_MovementType"].ToString(); // MovementType
                        _InputParameters.T_ITEM[j].WERKS = clsStandards.current_Plant();
                        // _InputParameters.T_ITEM[i].LIFNR = ""; // Transporter Code
                        _InputParameters.T_ITEM[j].ERFMG = Convert.ToDecimal(clsStandards.filter(GrGRNDetails.Rows[i].Cells[13].Text.Trim())); //AcceptedQty
                        _InputParameters.T_ITEM[j].EBELN = txtDocumentNo.Text.Trim(); //Purchase order no
                        _InputParameters.T_ITEM[j].EBELP = clsStandards.filter(GrGRNDetails.Rows[i].Cells[4].Text.Trim()); //lineitem no
                        int lineitem = 0;
                        //clsStandards.WriteLog(this, new Exception(" started 2 "), clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 0, true);

                        if (j < 9)
                        {
                            lineitem = j;
                            lineitem++;
                            _InputParameters.T_ITEM[j].ITEM = "00000" + lineitem.ToString(); // clsStandards.filter(GrGRNDetails.Rows[i].Cells[4].Text.Trim());//000001
                            _InputParameters.T_ITEM[j].ZEILE = "000" + lineitem.ToString();
                        }
                        else
                        {
                            lineitem = j;
                            lineitem++;
                            _InputParameters.T_ITEM[j].ITEM = "0000" + lineitem.ToString(); // clsStandards.filter(GrGRNDetails.Rows[i].Cells[4].Text.Trim());//000001
                            _InputParameters.T_ITEM[j].ZEILE = "00" + lineitem.ToString();
                        }
                        _InputParameters.T_ITEM[j].ERFME = clsStandards.filter(GrGRNDetails.Rows[i].Cells[9].Text.Trim());
                        _InputParameters.T_ITEM[j].KZBEW = "B";
                        //_InputParameters.T_ITEM[i].PACK = ""; //No of Packs
                        _InputParameters.T_ITEM[j].VENDOR_BATCH_NO = clsStandards.filter(GrGRNDetails.Rows[i].Cells[26].Text.Trim());
                        _InputParameters.T_ITEM[j].HSDAT = System.DateTime.Now.ToString("yyyy-MM-dd");
                        _InputParameters.T_ITEM[j].KOSTL = clsStandards.filter(GrGRNDetails.Rows[i].Cells[33].Text.Trim());
                        _InputParameters.T_ITEM[j].GRUND = "0101";
                        _InputParameters.T_ITEM[j].LGORT = clsStandards.filter(GrGRNDetails.Rows[i].Cells[34].Text.Trim());

                        //_InputParameters.T_ITEM[i].LSMNG = Convert.ToDecimal(clsStandards.filter(GrGRNDetails.Rows[i].Cells[13].Text.Trim())); //AcceptedQty
                        clsStandards.WriteLog(this, new Exception(" started 2"), clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 0, true);
                        if (clsStandards.filter(GrGRNDetails.Rows[i].Cells[29].Text.Trim()).ToString() == "")
                        {
                            _InputParameters.T_ITEM[j].BWTAR = "";
                        }
                        else
                        {
                            _InputParameters.T_ITEM[j].BWTAR = clsStandards.filter(GrGRNDetails.Rows[i].Cells[29].Text.Trim());
                        }

                        _InputParameters.T_ITEM[j].VFDAT = clsStandards.filter(GrGRNDetails.Rows[i].Cells[15].Text.Trim());
                        _InputParameters.T_ITEM[j].ENTRY_UOM_ISO = clsStandards.filter(GrGRNDetails.Rows[i].Cells[9].Text.Trim());

                        _InputParameters.T_ITEM[j].SGTXT = clsStandards.filter(GrGRNDetails.Rows[i].Cells[32].Text.Trim()); //Item Description
                        _InputParameters.T_ITEM[j].GJAHR = System.DateTime.Now.Year.ToString(); //year
                        _InputParameters.T_ITEM[j].MANUFACTURE_NAME = clsStandards.filter(GrGRNDetails.Rows[i].Cells[28].Text.Trim()); ;
                        _InputParameters.T_ITEM[j].PACK_QTY = Convert.ToDecimal(clsStandards.filter(GrGRNDetails.Rows[i].Cells[24].Text.Trim()));
                        _InputParameters.T_ITEM[j].MAKTX = clsStandards.filter(GrGRNDetails.Rows[i].Cells[32].Text.Trim()); // Item Desc
                        _InputParameters.T_ITEM[j].CPUTM_MKPF = "";
                        _InputParameters.T_ITEM[j].CERTIFICATE_ENCLOSED = "";
                        _InputParameters.IM_HEADER.NO_OF_PACKS = clsStandards.filter(GrGRNDetails.Rows[i].Cells[18].Text.Trim());
                        _InputParameters.T_ITEM[j].PACK = clsStandards.filter(GrGRNDetails.Rows[i].Cells[18].Text.Trim());
                        _InputParameters.T_ITEM[j].CONTAINER = Convert.ToDecimal(clsStandards.filter(GrGRNDetails.Rows[i].Cells[18].Text.Trim()));
                        _InputParameters.T_ITEM[j].ADV_LIC1 = clsStandards.filter(GrGRNDetails.Rows[i].Cells[40].Text.Trim());
                        _InputParameters.T_ITEM[j].ADV_LIC2 = clsStandards.filter(GrGRNDetails.Rows[i].Cells[41].Text.Trim());
                        _InputParameters.T_ITEM[j].ADV_MENGE1 = Convert.ToDecimal(clsStandards.filter(GrGRNDetails.Rows[i].Cells[13].Text.Trim()));
                        _InputParameters.T_ITEM[j].ADV_MENGE2 = 0;

                    }
                }
                clsStandards.WriteLog(this, new Exception(" started 3"), clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 0, true);
                var req = new MemoryStream();
                var reqxml = new System.Xml.Serialization.XmlSerializer(_InputParameters.GetType());
                reqxml.Serialize(req, _InputParameters);
                string strxml = Encoding.UTF8.GetString(req.ToArray());
               // clsStandards.WriteSoapXML(strxml, "MaterialPosting - Request  ", clsStandards.GetCurrentPageName().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());

                //clsStandards.WriteLog(this, new Exception(strxml), clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 0, true);

                _Response = _ManNo.ZDMF002(_InputParameters);
                _OutputParameters1 = _Response.EX_MATERIAL_GRN;
                _OutputParameters3 = _Response.T_ITEM;
                _Returnparameter = _Response.RETURN;

                var res = new MemoryStream();
                var resxml = new System.Xml.Serialization.XmlSerializer(_Response.GetType());
                resxml.Serialize(res, _Response);
                string xml = Encoding.UTF8.GetString(res.ToArray());
                clsStandards.WriteSoapXML(xml, "MaterialPosting - Response", clsStandards.GetCurrentPageName().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());

             //   clsStandards.WriteLog(this, new Exception(xml), clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 0, true);

                string message = "";
                if (_Returnparameter.Length > 0)
                {
                    for (int a = 0; a < _Returnparameter.Length; a++)
                    {

                        string strPROCESSORDERNO = txtDocumentNo.Text.Trim();
                        string strMESSAGE = _Returnparameter[a].MESSAGE.ToString();
                        string strMESSAGE_V1 = _Returnparameter[a].MESSAGE_V1.ToString();
                        string strMESSAGE_V2 = _Returnparameter[a].MESSAGE_V2.ToString();
                        string strMESSAGE_V3 = _Returnparameter[a].MESSAGE_V3.ToString();
                        string strMESSAGE_V4 = _Returnparameter[a].MESSAGE_V4.ToString();
                        string strROW = _Returnparameter[a].ROW.ToString();
                        string strPLANT = clsStandards.current_Plant(); ;
                        string strCREATEDBY = clsStandards.current_Username();
                        objBL.BL__Dispensing_Posting_Error(strPROCESSORDERNO, strMESSAGE, strMESSAGE_V1, strMESSAGE_V2, strMESSAGE_V3, strMESSAGE_V4, strROW, strPLANT, strCREATEDBY, "MaNNO POST");
                        message += strMESSAGE;
                        if (message.ToUpper().Contains("VALUATION TYPE"))
                        {
                            btnAdd_ValutionType.Visible = true;
                        }
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
                //if (_OutputParameters1.Length > 0)
                //{
                //    for (int m = 0; m < _OutputParameters1.Length; m++)
                //    {
                //        strManno = _OutputParameters1[m].MAN_NO;
                //        Sapbatch = _OutputParameters1[m].SAP_BATCH;
                //    }
                //}
                //else
                //{
                //    strManno = "";
                //    Sapbatch = "";
                //}


                clsStandards.WriteLog(this, new Exception("lenth :" + _OutputParameters1.Length), clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 0, false);

                for (int i = 0; i < GrGRNDetails.Rows.Count; i++)
                {
                    CheckBox cb = (CheckBox)GrGRNDetails.Rows[i].FindControl("chkSelect");
                    if (cb.Checked == true)
                    {
                        ObjPL.strItemCode = clsStandards.filter(GrGRNDetails.Rows[i].Cells[5].Text.Trim());
                        ObjPL.strVendorBatchNo = clsStandards.filter(GrGRNDetails.Rows[i].Cells[26].Text.Trim());
                        ObjPL.strLineItemNo = clsStandards.filter(GrGRNDetails.Rows[i].Cells[4].Text.Trim());
                        if (_OutputParameters1.Length > 0)
                        {
                            for (int m = 0; m < _OutputParameters1.Length; m++)
                            {
                                strItemcode = _OutputParameters1[m].ITEM_CODE;
                                strLineItemNo = ObjPL.strLineItemNo;
                                if (strItemcode.Trim() == ObjPL.strItemCode.Trim())
                                {
                                    if (CheckManno(_OutputParameters1[m].MAN_NO.Trim(), strItemcode.Trim(), _OutputParameters1[m].SAP_BATCH.Trim(), strLineItemNo, _OutputParameters1[m].VENDOR_BATCH_NO.Trim()))
                                    {
                                        strManno = _OutputParameters1[m].MAN_NO.Trim();
                                        Sapbatch = _OutputParameters1[m].SAP_BATCH.Trim();
                                        break;
                                    }
                                    else
                                    {
                                        strManno = "";
                                        Sapbatch = "";
                                    }
                                }
                            }
                        }
                        else
                        {
                            strManno = "";
                            Sapbatch = "";
                        }

                        ObjPL.strPlant = clsStandards.filter(GrGRNDetails.Rows[i].Cells[1].Text.Trim());
                        ObjPL.strPurchaseOrderNo = clsStandards.filter(GrGRNDetails.Rows[i].Cells[2].Text.Trim());
                        ObjPL.strPurchaseOrderDate = clsStandards.filter(GrGRNDetails.Rows[i].Cells[3].Text.Trim());
                        ObjPL.strVendorName = clsStandards.filter(GrGRNDetails.Rows[i].Cells[25].Text.Trim());
                        ObjPL.strItemType = clsStandards.filter(GrGRNDetails.Rows[i].Cells[6].Text.Trim());
                        ObjPL.strOrderQty = Convert.ToDecimal(clsStandards.filter(GrGRNDetails.Rows[i].Cells[7].Text.Trim()));
                        ObjPL.strUom = clsStandards.filter(GrGRNDetails.Rows[i].Cells[9].Text.Trim());
                        ObjPL.strAcceptedQty = Convert.ToDecimal(clsStandards.filter(GrGRNDetails.Rows[i].Cells[13].Text.Trim()));
                        ObjPL.strNoOfContainer = Convert.ToDecimal(clsStandards.filter(GrGRNDetails.Rows[i].Cells[18].Text.Trim()));
                        ObjPL.strPackQty = Convert.ToDecimal(clsStandards.filter(GrGRNDetails.Rows[i].Cells[24].Text.Trim()));
                        ObjPL.strGateEntryNo = clsStandards.filter(GrGRNDetails.Rows[i].Cells[12].Text.Trim());
                        ObjPL.strManfactureName = clsStandards.filter(GrGRNDetails.Rows[i].Cells[28].Text.Trim()); ;
                        ObjPL.strVendorBatchNo = clsStandards.filter(GrGRNDetails.Rows[i].Cells[26].Text.Trim());
                        ObjPL.strMFGDate = clsStandards.filter(GrGRNDetails.Rows[i].Cells[14].Text.Trim());
                        ObjPL.strEXPDate = clsStandards.filter(GrGRNDetails.Rows[i].Cells[15].Text.Trim());
                        ObjPL.strMANNo = strManno;
                        ObjPL.strSapBatchNo = Sapbatch;
                        ObjPL.strUser = clsStandards.current_Username();
                        ObjPL.strTareWt = clsStandards.filter(GrGRNDetails.Rows[i].Cells[22].Text.Trim()) == "" ? 0 : Convert.ToDecimal(clsStandards.filter(GrGRNDetails.Rows[i].Cells[22].Text.Trim()));
                        ObjPL.strGrossWt = clsStandards.filter(GrGRNDetails.Rows[i].Cells[21].Text.Trim()) == "" ? 0 : Convert.ToDecimal(clsStandards.filter(GrGRNDetails.Rows[i].Cells[21].Text.Trim()));
                        ObjPL.strNetWt = clsStandards.filter(GrGRNDetails.Rows[i].Cells[23].Text.Trim()) == "" ? 0 : Convert.ToDecimal(clsStandards.filter(GrGRNDetails.Rows[i].Cells[23].Text.Trim()));
                        ObjPL.Purchase_Ref = clsStandards.filter(GrGRNDetails.Rows[i].Cells[30].Text.Trim()) == "" ? 0 : Convert.ToInt32(clsStandards.filter(GrGRNDetails.Rows[i].Cells[30].Text.Trim()));
                        ObjPL.strMessgae = message;
                        ObjPL.strStroageLoc = clsStandards.filter(GrGRNDetails.Rows[i].Cells[34].Text.Trim()).ToUpper();
                        ObjPL.AdvanceLicense1 = clsStandards.filter(GrGRNDetails.Rows[i].Cells[40].Text.Trim());
                        ObjPL.AdvanceLicense2 = clsStandards.filter(GrGRNDetails.Rows[i].Cells[41].Text.Trim());

                        string Result = objBL.BL_Insert_ManNo(ObjPL);

                        GridViewRow gr = (GridViewRow)GrGRNDetails.Rows[i];
                        if (message.Contains("SUCCESS"))
                        {
                            cb.Enabled = false;
                            cb.Checked = false;
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
                    clsStandards.WriteLog(this, new Exception("Material Posted Successfully , Document No :  " + strManno), clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 0, true);
                }
                else
                {
                    clsStandards.WriteLog(this, new Exception("SAP Message : " + message), clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 0, true);
                }
            }
        }
        catch (Exception ex)
        {
            clsStandards.WriteLog(this, new Exception(ex.ToString()), clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 0, true);
            return;
        }
    }


    protected void btnSTOPost_Click(object sender, EventArgs e)
    {
        PL_ManNo ObjPL = new PL_ManNo();
        BL_GrnPrinting objBL = new BL_GrnPrinting();
        string strManno = "";
        string Sapbatch = "";
        string strItemcode = "";
        string strLineItemNo = "";
        XDocument ResultXML = new XDocument();
        try
        {
            if (!Chk_Checkbox())
            {
                clsStandards.WriteLog(this, new Exception(" Please Check the Item from List "), clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 0, true);
                return;
            }
            if (GrGRNDetails.Rows.Count > 0)
            {
                STO_OUT_DELIVERY.Z_WS_OUTBOUND_DELIVRY _STONo = new STO_OUT_DELIVERY.Z_WS_OUTBOUND_DELIVRY();
                //STO_Delivery.Z_WS_OUTBOUND_DELIVRY _STONo = new STO_Delivery.Z_WS_OUTBOUND_DELIVRY();
                STO_OUT_DELIVERY.ZDMF019 _InputParameters = new STO_OUT_DELIVERY.ZDMF019();
                STO_OUT_DELIVERY.ZDMF019Response _Response = new STO_OUT_DELIVERY.ZDMF019Response();
                STO_OUT_DELIVERY.ZDBMS033[] _OutputParameters1 = new STO_OUT_DELIVERY.ZDBMS033[0]; //Lbound
                STO_OUT_DELIVERY.ZDBMS034[] _OutputParameters2 = new STO_OUT_DELIVERY.ZDBMS034[0];  //exbound
                STO_OUT_DELIVERY.ZDBMS035[] _OutputParameters3 = new STO_OUT_DELIVERY.ZDBMS035[0];  //imbound
                STO_OUT_DELIVERY.BAPIRET2[] _Returnparameter = new STO_OUT_DELIVERY.BAPIRET2[0];
                _InputParameters.IM_OUTBOUND = new STO_OUT_DELIVERY.ZDBMS035();

                _STONo.Proxy = new WebProxy();
                _STONo.Credentials = new NetworkCredential(txtSapUser.Text.Trim(), txtSapPass.Text.Trim());
                _STONo.Proxy.Credentials = new NetworkCredential(txtSapUser.Text.Trim(), txtSapPass.Text.Trim());
                
                int TotalCheckboxPO = 0;
                for (int k = 0; k < GrGRNDetails.Rows.Count; k++)
                {
                    CheckBox cbk = (CheckBox)GrGRNDetails.Rows[k].FindControl("chkSelect");
                    if (cbk.Checked == true)
                    {
                        TotalCheckboxPO++;
                    }
                }
                _InputParameters.LT_OUTBOUND = new STO_OUT_DELIVERY.ZDBMS033[TotalCheckboxPO];
                _InputParameters.RETURN = new STO_OUT_DELIVERY.BAPIRET2[TotalCheckboxPO];
                

                #region
                _InputParameters.IM_OUTBOUND.DOCUMENT_NO = "";
                _InputParameters.IM_OUTBOUND.DOC_DATE = "";
                _InputParameters.IM_OUTBOUND.POSTING_DATE = "";
                _InputParameters.IM_OUTBOUND.DELIVERY_NOTE = "";
                _InputParameters.IM_OUTBOUND.GR_GI_SLIP_NO = "";
                _InputParameters.IM_OUTBOUND.BILL_OF_LANDING = "";
                _InputParameters.IM_OUTBOUND.HEADER_TEXT = "";
                _InputParameters.IM_OUTBOUND.GATEN = "";
                _InputParameters.IM_OUTBOUND.GATE_DATE = "";
                _InputParameters.IM_OUTBOUND.SALES_TAX = "";
                _InputParameters.IM_OUTBOUND.AIRWAY_BILL_NO = "";
                _InputParameters.IM_OUTBOUND.AIRWAY_BILL_DATE = "";
                _InputParameters.IM_OUTBOUND.WAY_BILL_NOT_REQUIRED = "";
                _InputParameters.IM_OUTBOUND.GROSS_WT = "";
                _InputParameters.IM_OUTBOUND.NET_WT = "";
                _InputParameters.IM_OUTBOUND.TARE_WT = "";
                _InputParameters.IM_OUTBOUND.TAR_WT_UNIT = "";
                _InputParameters.IM_OUTBOUND.PACK = "";
                _InputParameters.IM_OUTBOUND.MTAX = "";
                _InputParameters.IM_OUTBOUND.LICNO = "";
                _InputParameters.IM_OUTBOUND.LIFNR = "";
                _InputParameters.IM_OUTBOUND.TMODE =
                _InputParameters.IM_OUTBOUND.RECNUM = "";
                _InputParameters.IM_OUTBOUND.TRUCK = "";
                _InputParameters.IM_OUTBOUND.ITEM_OK = "";
                _InputParameters.IM_OUTBOUND.STORAGE_LOC =
                _InputParameters.IM_OUTBOUND.STORAGE_TYPE = "";
                _InputParameters.IM_OUTBOUND.VENDOR_BATCH = "";
                _InputParameters.IM_OUTBOUND.HSDAT = "";
                _InputParameters.IM_OUTBOUND.VFDAT = "";
                _InputParameters.IM_OUTBOUND.MFNAM = "";
                _InputParameters.IM_OUTBOUND.R9NUM = "";
                _InputParameters.IM_OUTBOUND.PACKS = "";
                _InputParameters.IM_OUTBOUND.PACK_QTY = 0;
                _InputParameters.IM_OUTBOUND.PO_NUMBER = "";
                _InputParameters.IM_OUTBOUND.GM_CODE = "";
                _InputParameters.IM_OUTBOUND.PLANT =
                _InputParameters.IM_OUTBOUND.MOVE_TYPE = "";
                _InputParameters.IM_OUTBOUND.PO_ITEM = "";
                _InputParameters.IM_OUTBOUND.DELIV_ITEM ="";

#region MyRegion
		 
	
                //_InputParameters.IM_OUTBOUND.ACTION = "";
                //_InputParameters.IM_OUTBOUND.ACTION_TYPE = "";
                //_InputParameters.IM_OUTBOUND.DOCUMENT_NO = "";
                //_InputParameters.IM_OUTBOUND.DOC_DATE = "";
                //_InputParameters.IM_OUTBOUND.POSTING_DATE = "";
                //_InputParameters.IM_OUTBOUND.DELIVERY_NOTE = "";
                //_InputParameters.IM_OUTBOUND.GR_GI_SLIP_NO = "";
                //_InputParameters.IM_OUTBOUND.BILL_OF_LANDING = "";
                //_InputParameters.IM_OUTBOUND.HEADER_TEXT = "";
                //_InputParameters.IM_OUTBOUND.GATEN = "";
                //_InputParameters.IM_OUTBOUND.GATE_DATE = "";
                //_InputParameters.IM_OUTBOUND.SALES_TAX = "";
                //_InputParameters.IM_OUTBOUND.AIRWAY_BILL_NO = "";
                //_InputParameters.IM_OUTBOUND.AIRWAY_BILL_DATE = "";
                //_InputParameters.IM_OUTBOUND.WAY_BILL_NOT_REQUIRED = "";
                //_InputParameters.IM_OUTBOUND.GROSS_WT = "";
                //_InputParameters.IM_OUTBOUND.NET_WT = "";
                //_InputParameters.IM_OUTBOUND.TARE_WT = "";
                //_InputParameters.IM_OUTBOUND.TAR_WT_UNIT = "";
                //_InputParameters.IM_OUTBOUND.PACK = "";
                //_InputParameters.IM_OUTBOUND.MTAX = "";
                //_InputParameters.IM_OUTBOUND.LICNO = "";
                //_InputParameters.IM_OUTBOUND.LIFNR = "";
                //_InputParameters.IM_OUTBOUND.TMODE = "";
                //_InputParameters.IM_OUTBOUND.RECNUM = "";
                //_InputParameters.IM_OUTBOUND.TRUCK = "";
                //_InputParameters.IM_OUTBOUND.ITEM_OK = "";
                //_InputParameters.IM_OUTBOUND.STORAGE_LOC = "";
                //_InputParameters.IM_OUTBOUND.STORAGE_TYPE = "";
                //_InputParameters.IM_OUTBOUND.VENDOR_BATCH = "";
                //_InputParameters.IM_OUTBOUND.HSDAT = "";
                //_InputParameters.IM_OUTBOUND.VFDAT = "";
                //_InputParameters.IM_OUTBOUND.MFNAM = "";
                //_InputParameters.IM_OUTBOUND.MATNR = "";
                //_InputParameters.IM_OUTBOUND.CHARG = "";
                //_InputParameters.IM_OUTBOUND.R9NUM = "";
                //_InputParameters.IM_OUTBOUND.PACKS = "";
                //_InputParameters.IM_OUTBOUND.PACK_QTY = 0;
                //_InputParameters.IM_OUTBOUND.PO_NUMBER = "";
                //_InputParameters.IM_OUTBOUND.GM_CODE = "";
                //_InputParameters.IM_OUTBOUND.PLANT = "";
                //_InputParameters.IM_OUTBOUND.MOVE_TYPE = "";
                //_InputParameters.IM_OUTBOUND.VAL_TYPE = "";
                //_InputParameters.IM_OUTBOUND.ENTRY_QNT = 0;
                //_InputParameters.IM_OUTBOUND.ENTRY_UOM = "";
                //_InputParameters.IM_OUTBOUND.PO_ITEM = "";
                //_InputParameters.IM_OUTBOUND.MVT_IND = "";
                //_InputParameters.IM_OUTBOUND.PROD_DATE = "";
                //_InputParameters.IM_OUTBOUND.EXPIRYDATE = "";
                //_InputParameters.IM_OUTBOUND.BILLNO = "";
                //_InputParameters.IM_OUTBOUND.MAKTX = "";
                //_InputParameters.IM_OUTBOUND.MDSLIP = "";
                //_InputParameters.IM_OUTBOUND.DELIV_ITEM = "";
                //_InputParameters.IM_OUTBOUND.QUANTITY = 0;
                //_InputParameters.IM_OUTBOUND.BASE_UOM = "";

#endregion

                #endregion
                
                

                int j = -1;
                for (int i = 0; i < GrGRNDetails.Rows.Count; i++)
                {
                    CheckBox cb = (CheckBox)GrGRNDetails.Rows[i].FindControl("chkSelect");
                    if (cb.Checked == true)
                    {
                        
                        j++;
                        #region


                        _InputParameters.LT_OUTBOUND[j] = new STO_OUT_DELIVERY.ZDBMS033();
                        _InputParameters.LT_OUTBOUND[j].ACTION = "";
                        _InputParameters.LT_OUTBOUND[j].ACTION_TYPE = "";
                        _InputParameters.LT_OUTBOUND[j].DOCUMENT_NO = "";
                        _InputParameters.LT_OUTBOUND[j].DOC_DATE = "";
                        _InputParameters.LT_OUTBOUND[j].POSTING_DATE = "";
                        _InputParameters.LT_OUTBOUND[j].DELIVERY_NOTE = "";
                        _InputParameters.LT_OUTBOUND[j].GR_GI_SLIP_NO = "";
                        _InputParameters.LT_OUTBOUND[j].BILL_OF_LANDING = "";
                        _InputParameters.LT_OUTBOUND[j].HEADER_TEXT = "";
                        _InputParameters.LT_OUTBOUND[j].GATEN = "";
                        _InputParameters.LT_OUTBOUND[j].GATE_DATE = "";
                        _InputParameters.LT_OUTBOUND[j].SALES_TAX = "";
                        _InputParameters.LT_OUTBOUND[j].AIRWAY_BILL_NO = "";
                        _InputParameters.LT_OUTBOUND[j].AIRWAY_BILL_DATE = "";
                        _InputParameters.LT_OUTBOUND[j].WAY_BILL_NOT_REQUIRED = "";
                        _InputParameters.LT_OUTBOUND[j].GROSS_WT = "";
                        _InputParameters.LT_OUTBOUND[j].NET_WT = "";
                        _InputParameters.LT_OUTBOUND[j].TARE_WT = "";
                        _InputParameters.LT_OUTBOUND[j].TAR_WT_UNIT = "";
                        _InputParameters.LT_OUTBOUND[j].PACK = "";
                        _InputParameters.LT_OUTBOUND[j].MTAX = "";
                        _InputParameters.LT_OUTBOUND[j].LICNO = "";
                        _InputParameters.LT_OUTBOUND[j].LIFNR = "";
                        _InputParameters.LT_OUTBOUND[j].TMODE = "";
                        _InputParameters.LT_OUTBOUND[j].RECNUM = "";
                        _InputParameters.LT_OUTBOUND[j].TRUCK = "";
                        _InputParameters.LT_OUTBOUND[j].ITEM_OK = "";
                        _InputParameters.LT_OUTBOUND[j].STORAGE_LOC = "";
                        _InputParameters.LT_OUTBOUND[j].STORAGE_TYPE = "";
                        _InputParameters.LT_OUTBOUND[j].VENDOR_BATCH = "";
                        _InputParameters.LT_OUTBOUND[j].HSDAT = "";
                        _InputParameters.LT_OUTBOUND[j].VFDAT = "";
                        _InputParameters.LT_OUTBOUND[j].MFNAM = "";
                        _InputParameters.LT_OUTBOUND[j].MATNR = "";
                        _InputParameters.LT_OUTBOUND[j].CHARG = "";
                        _InputParameters.LT_OUTBOUND[j].R9NUM = "";
                        _InputParameters.LT_OUTBOUND[j].PACKS = "";
                        _InputParameters.LT_OUTBOUND[j].PACK_QTY = 0;
                        _InputParameters.LT_OUTBOUND[j].PO_NUMBER = "";
                        _InputParameters.LT_OUTBOUND[j].GM_CODE = "";
                        _InputParameters.LT_OUTBOUND[j].PLANT = "";
                        _InputParameters.LT_OUTBOUND[j].MOVE_TYPE = "";
                        _InputParameters.LT_OUTBOUND[j].VAL_TYPE = "";
                        _InputParameters.LT_OUTBOUND[j].ENTRY_QNT = 0;
                        _InputParameters.LT_OUTBOUND[j].ENTRY_UOM = "";
                        _InputParameters.LT_OUTBOUND[j].PO_ITEM = "";
                        _InputParameters.LT_OUTBOUND[j].MVT_IND = "";
                        _InputParameters.LT_OUTBOUND[j].PROD_DATE = "";
                        _InputParameters.LT_OUTBOUND[j].EXPIRYDATE = "";
                        _InputParameters.LT_OUTBOUND[j].BILLNO = "";
                        _InputParameters.LT_OUTBOUND[j].MAKTX = "";
                        _InputParameters.LT_OUTBOUND[j].MDSLIP = "";
                        _InputParameters.LT_OUTBOUND[j].DELIV_ITEM = "";
                        _InputParameters.LT_OUTBOUND[j].QUANTITY = 0;
                        _InputParameters.LT_OUTBOUND[j].BASE_UOM = "";

                       
                        #endregion

                      
                       
                        _InputParameters.IM_OUTBOUND.DOCUMENT_NO = TxtDocNumber.Text.Trim();
                        _InputParameters.IM_OUTBOUND.DOC_DATE = DateTime.Now.ToString("yyyy-MM-dd"); 
                        _InputParameters.IM_OUTBOUND.POSTING_DATE = DateTime.Now.ToString("yyyy-MM-dd");
                        _InputParameters.IM_OUTBOUND.DELIVERY_NOTE = TxtDocNumber.Text.Trim(); 
                        _InputParameters.IM_OUTBOUND.GR_GI_SLIP_NO = clsStandards.filter(GrGRNDetails.Rows[i].Cells[37].Text.Trim());
                        _InputParameters.IM_OUTBOUND.BILL_OF_LANDING = DateTime.Now.ToString("yyyy-MM-dd"); 
                        _InputParameters.IM_OUTBOUND.HEADER_TEXT = "Test";
                        _InputParameters.IM_OUTBOUND.GATEN = clsStandards.filter(GrGRNDetails.Rows[i].Cells[12].Text.Trim()); 
                        _InputParameters.IM_OUTBOUND.GATE_DATE = clsStandards.filter(GrGRNDetails.Rows[i].Cells[31].Text.Trim()); 
                        _InputParameters.IM_OUTBOUND.SALES_TAX = "";
                        _InputParameters.IM_OUTBOUND.AIRWAY_BILL_NO = "";
                        _InputParameters.IM_OUTBOUND.AIRWAY_BILL_DATE = "";
                        _InputParameters.IM_OUTBOUND.WAY_BILL_NOT_REQUIRED = "";
                        _InputParameters.IM_OUTBOUND.GROSS_WT = "";
                        _InputParameters.IM_OUTBOUND.NET_WT = "";
                        _InputParameters.IM_OUTBOUND.TARE_WT = "";
                        _InputParameters.IM_OUTBOUND.TAR_WT_UNIT = "";
                        _InputParameters.IM_OUTBOUND.PACK = "";
                        _InputParameters.IM_OUTBOUND.MTAX = "";
                        _InputParameters.IM_OUTBOUND.LICNO = "";
                        _InputParameters.IM_OUTBOUND.LIFNR = "";
                        _InputParameters.IM_OUTBOUND.TMODE = "";
                        _InputParameters.IM_OUTBOUND.RECNUM = "";
                        _InputParameters.IM_OUTBOUND.TRUCK = "";
                        _InputParameters.IM_OUTBOUND.ITEM_OK = "";
                        _InputParameters.IM_OUTBOUND.STORAGE_LOC = clsStandards.filter(GrGRNDetails.Rows[i].Cells[34].Text.Trim()); 
                        _InputParameters.IM_OUTBOUND.STORAGE_TYPE = "";
                        _InputParameters.IM_OUTBOUND.VENDOR_BATCH = "";
                        _InputParameters.IM_OUTBOUND.HSDAT = "";
                        _InputParameters.IM_OUTBOUND.VFDAT = "";
                        _InputParameters.IM_OUTBOUND.MFNAM = "";
                       // _InputParameters.IM_OUTBOUND.MATNR = clsStandards.filter(GrGRNDetails.Rows[i].Cells[5].Text.Trim()); ;
                       // _InputParameters.IM_OUTBOUND.CHARG = "0000313986";
                        _InputParameters.IM_OUTBOUND.R9NUM = "";
                        _InputParameters.IM_OUTBOUND.PACKS = "";
                        _InputParameters.IM_OUTBOUND.PACK_QTY = 0;
                        _InputParameters.IM_OUTBOUND.PO_NUMBER = clsStandards.filter(GrGRNDetails.Rows[i].Cells[2].Text.Trim());
                        _InputParameters.IM_OUTBOUND.GM_CODE = clsStandards.filter(GrGRNDetails.Rows[i].Cells[27].Text.Trim()); 
                        _InputParameters.IM_OUTBOUND.PLANT = clsStandards.current_Plant(); 
                        _InputParameters.IM_OUTBOUND.MOVE_TYPE = "101";
                       /// _InputParameters.IM_OUTBOUND.VAL_TYPE = "0000313986";
                       // _InputParameters.IM_OUTBOUND.ENTRY_QNT = Convert.ToDecimal(clsStandards.filter(GrGRNDetails.Rows[i].Cells[24].Text.Trim())); 
                       // _InputParameters.IM_OUTBOUND.ENTRY_UOM = clsStandards.filter(GrGRNDetails.Rows[i].Cells[9].Text.Trim());
                        _InputParameters.IM_OUTBOUND.PO_ITEM = clsStandards.filter(GrGRNDetails.Rows[i].Cells[4].Text.Trim());
                        _InputParameters.IM_OUTBOUND.DELIV_ITEM = ""; 
                       // _InputParameters.IM_OUTBOUND.MVT_IND = "B";
                      //  _InputParameters.IM_OUTBOUND.PROD_DATE = DateTime.Now.ToString("yyyy-MM-dd");
                        //_InputParameters.IM_OUTBOUND.EXPIRYDATE = "";
                        //_InputParameters.IM_OUTBOUND.BILLNO = "";
                        //_InputParameters.IM_OUTBOUND.MAKTX = "";
                        //_InputParameters.IM_OUTBOUND.MDSLIP = "";
                        //_InputParameters.IM_OUTBOUND.DELIV_ITEM = clsStandards.filter(GrGRNDetails.Rows[i].Cells[4].Text.Trim()); 
                        //_InputParameters.IM_OUTBOUND.QUANTITY = Convert.ToDecimal(clsStandards.filter(GrGRNDetails.Rows[i].Cells[24].Text.Trim())); 
                        //_InputParameters.IM_OUTBOUND.BASE_UOM = clsStandards.filter(GrGRNDetails.Rows[i].Cells[9].Text.Trim());

                        _InputParameters.LT_OUTBOUND[j].ACTION = "";
                        _InputParameters.LT_OUTBOUND[j].ACTION_TYPE = "";
                        _InputParameters.LT_OUTBOUND[j].DOCUMENT_NO = TxtDocNumber.Text.Trim(); 
                        _InputParameters.LT_OUTBOUND[j].DOC_DATE = "";
                        _InputParameters.LT_OUTBOUND[j].POSTING_DATE = "";
                        _InputParameters.LT_OUTBOUND[j].DELIVERY_NOTE = "";
                        _InputParameters.LT_OUTBOUND[j].GR_GI_SLIP_NO = "";
                        _InputParameters.LT_OUTBOUND[j].BILL_OF_LANDING = "";
                        _InputParameters.LT_OUTBOUND[j].HEADER_TEXT = "";
                        _InputParameters.LT_OUTBOUND[j].GATEN = "";
                        _InputParameters.LT_OUTBOUND[j].GATE_DATE = "";
                        _InputParameters.LT_OUTBOUND[j].SALES_TAX = "";
                        _InputParameters.LT_OUTBOUND[j].AIRWAY_BILL_NO = "";
                        _InputParameters.LT_OUTBOUND[j].AIRWAY_BILL_DATE = "";
                        _InputParameters.LT_OUTBOUND[j].WAY_BILL_NOT_REQUIRED = "";
                        _InputParameters.LT_OUTBOUND[j].GROSS_WT = "";
                        _InputParameters.LT_OUTBOUND[j].NET_WT = "";
                        _InputParameters.LT_OUTBOUND[j].TARE_WT = "";
                        _InputParameters.LT_OUTBOUND[j].TAR_WT_UNIT = "";
                        _InputParameters.LT_OUTBOUND[j].PACK = "";
                        _InputParameters.LT_OUTBOUND[j].MTAX = "";
                        _InputParameters.LT_OUTBOUND[j].LICNO = "";
                        _InputParameters.LT_OUTBOUND[j].LIFNR = "";
                        _InputParameters.LT_OUTBOUND[j].TMODE = "";
                        _InputParameters.LT_OUTBOUND[j].RECNUM = "";
                        _InputParameters.LT_OUTBOUND[j].TRUCK = "";
                        _InputParameters.LT_OUTBOUND[j].ITEM_OK = "";
                        _InputParameters.LT_OUTBOUND[j].STORAGE_LOC = clsStandards.filter(GrGRNDetails.Rows[i].Cells[34].Text.Trim()); 
                        _InputParameters.LT_OUTBOUND[j].STORAGE_TYPE = "";
                        _InputParameters.LT_OUTBOUND[j].VENDOR_BATCH = "";
                        _InputParameters.LT_OUTBOUND[j].HSDAT = "";
                        _InputParameters.LT_OUTBOUND[j].VFDAT = "";
                        _InputParameters.LT_OUTBOUND[j].MFNAM = "";
                        _InputParameters.LT_OUTBOUND[j].MATNR = clsStandards.filter(GrGRNDetails.Rows[i].Cells[5].Text.Trim());  
                        _InputParameters.LT_OUTBOUND[j].CHARG = "";
                        _InputParameters.LT_OUTBOUND[j].R9NUM = "";
                        _InputParameters.LT_OUTBOUND[j].PACKS = "";
                        _InputParameters.LT_OUTBOUND[j].PACK_QTY = 0;
                        _InputParameters.LT_OUTBOUND[j].PO_NUMBER = clsStandards.filter(GrGRNDetails.Rows[i].Cells[2].Text.Trim());
                        _InputParameters.LT_OUTBOUND[j].GM_CODE = clsStandards.filter(GrGRNDetails.Rows[i].Cells[27].Text.Trim());
                        _InputParameters.LT_OUTBOUND[j].PLANT = clsStandards.current_Plant();
                        _InputParameters.LT_OUTBOUND[j].MOVE_TYPE = "101";
                        _InputParameters.LT_OUTBOUND[j].VAL_TYPE = clsStandards.filter(GrGRNDetails.Rows[i].Cells[26].Text.Trim());
                        //_InputParameters.LT_OUTBOUND[j].ENTRY_QNT = Convert.ToDecimal(GrGRNDetails.Rows[i].Cells[24].Text.Trim());
                         _InputParameters.LT_OUTBOUND[j].ENTRY_QNT = Convert.ToDecimal(clsStandards.filter(GrGRNDetails.Rows[i].Cells[13].Text.Trim()));
                        _InputParameters.LT_OUTBOUND[j].ENTRY_UOM = clsStandards.filter(GrGRNDetails.Rows[i].Cells[9].Text.Trim());
                        _InputParameters.LT_OUTBOUND[j].PO_ITEM = clsStandards.filter(GrGRNDetails.Rows[i].Cells[4].Text.Trim()); 
                        _InputParameters.LT_OUTBOUND[j].MVT_IND = "B";
                        _InputParameters.LT_OUTBOUND[j].PROD_DATE = DateTime.Now.ToString("yyyy-MM-dd"); 
                        _InputParameters.LT_OUTBOUND[j].EXPIRYDATE = "";
                        _InputParameters.LT_OUTBOUND[j].BILLNO = "";
                        _InputParameters.LT_OUTBOUND[j].MAKTX = "";
                        _InputParameters.LT_OUTBOUND[j].MDSLIP = "";
                        _InputParameters.LT_OUTBOUND[j].DELIV_ITEM = clsStandards.filter(GrGRNDetails.Rows[i].Cells[4].Text.Trim());
                       // _InputParameters.LT_OUTBOUND[j].QUANTITY = Convert.ToDecimal(GrGRNDetails.Rows[i].Cells[24].Text.Trim());
                        _InputParameters.LT_OUTBOUND[j].QUANTITY = Convert.ToDecimal(clsStandards.filter(GrGRNDetails.Rows[i].Cells[13].Text.Trim()));
                       _InputParameters.LT_OUTBOUND[j].BASE_UOM = clsStandards.filter(GrGRNDetails.Rows[i].Cells[9].Text.Trim()); 
                        
                        //_InputParameters.IM_HEADER.PLANT = clsStandards.current_Plant();
                        //_InputParameters.IM_HEADER.PURCHASE_ORDER_NO = clsStandards.filter(GrGRNDetails.Rows[i].Cells[2].Text.Trim());
                        //_InputParameters.IM_HEADER.DOC_DATE = System.DateTime.Now.ToString("yyyy-MM-dd");
                        //_InputParameters.IM_HEADER.GATE_ENTRY_NO = clsStandards.filter(GrGRNDetails.Rows[i].Cells[12].Text.Trim());
                        //_InputParameters.IM_HEADER.TDATE = clsStandards.filter(GrGRNDetails.Rows[i].Cells[31].Text.Trim());
                        //_InputParameters.IM_HEADER.GM_CODE = clsStandards.filter(ConfigurationManager.AppSettings["GM_CODE"].ToString());
                        //_InputParameters.IM_HEADER.GROSS_WT = clsStandards.filter(GrGRNDetails.Rows[i].Cells[21].Text.Trim());
                        //_InputParameters.IM_HEADER.HEADER_TEXT = clsStandards.filter(GrGRNDetails.Rows[i].Cells[38].Text.Trim());
                        //_InputParameters.IM_HEADER.LIC_NO = "11223";
                       
                        //if (!string.IsNullOrEmpty(clsStandards.filter(GrGRNDetails.Rows[i].Cells[8].Text.Trim())))
                        //{
                        //    _InputParameters.IM_HEADER.AIRWAY_BILL_NO = clsStandards.filter(GrGRNDetails.Rows[i].Cells[8].Text.Trim());
                        //    _InputParameters.IM_HEADER.AIRWAY_BILL_DATE = clsStandards.filter(GrGRNDetails.Rows[i].Cells[10].Text.Trim());
                        //}
                        //else
                        //{
                        //    _InputParameters.IM_HEADER.WAY_BILL_NOT_REQUIRED = "X";
                        //}
                       _InputParameters.RETURN[j] = new STO_OUT_DELIVERY.BAPIRET2();
                        _InputParameters.RETURN[j].FIELD = "";
                        _InputParameters.RETURN[j].ID = "";
                        _InputParameters.RETURN[j].LOG_MSG_NO = "";
                        _InputParameters.RETURN[j].LOG_NO = "";
                        _InputParameters.RETURN[j].MESSAGE = "";
                        _InputParameters.RETURN[j].MESSAGE_V1 = "";
                        _InputParameters.RETURN[j].MESSAGE_V2 = "";
                        _InputParameters.RETURN[j].MESSAGE_V3 = "";
                        _InputParameters.RETURN[j].MESSAGE_V4 = "";
                        _InputParameters.RETURN[j].NUMBER = "";
                        _InputParameters.RETURN[j].PARAMETER = "";
                        _InputParameters.RETURN[j].ROW = 0;
                        _InputParameters.RETURN[j].SYSTEM = "";
                        _InputParameters.RETURN[j].TYPE = "";

                      
                      
                    }
                }
                var req = new MemoryStream();
                var reqxml = new System.Xml.Serialization.XmlSerializer(_InputParameters.GetType());
                reqxml.Serialize(req, _InputParameters);
                string strxml = Encoding.UTF8.GetString(req.ToArray());
                clsStandards.WriteSoapXML(strxml, "MaterialPosting - Request  ", clsStandards.GetCurrentPageName().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());

                clsStandards.WriteLog(this, new Exception(strxml), clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 0, false);

                _Response = _STONo.ZDMF019(_InputParameters);
                _OutputParameters2 = _Response.EX_OUTBOUND;
                _OutputParameters1 = _Response.LT_OUTBOUND;
                _Returnparameter = _Response.RETURN;

                clsStandards.WriteLog(this, new Exception("Start 5 "), clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 0, false);
                var res = new MemoryStream();
                var resxml = new System.Xml.Serialization.XmlSerializer(_Response.GetType());
                resxml.Serialize(res, _Response);
                string xml = Encoding.UTF8.GetString(res.ToArray());

                clsStandards.WriteSoapXML(xml, "MaterialPosting - Response", clsStandards.GetCurrentPageName().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());

                clsStandards.WriteLog(this, new Exception(xml), clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 0, false);
                string message = "";
                if (_Returnparameter.Length > 0)
                {
                    for (int a = 0; a < _Returnparameter.Length; a++)
                    {

                        string strPROCESSORDERNO = txtDocumentNo.Text.Trim();
                        string strMESSAGE = _Returnparameter[a].MESSAGE.ToString();
                        string strMESSAGE_V1 = _Returnparameter[a].MESSAGE_V1.ToString();
                        string strMESSAGE_V2 = _Returnparameter[a].MESSAGE_V2.ToString();
                        string strMESSAGE_V3 = _Returnparameter[a].MESSAGE_V3.ToString();
                        string strMESSAGE_V4 = _Returnparameter[a].MESSAGE_V4.ToString();
                        string strROW = _Returnparameter[a].ROW.ToString();
                        string strPLANT = clsStandards.current_Plant(); ;
                        string strCREATEDBY = clsStandards.current_Username();
                        objBL.BL__Dispensing_Posting_Error(strPROCESSORDERNO, strMESSAGE, strMESSAGE_V1, strMESSAGE_V2, strMESSAGE_V3, strMESSAGE_V4, strROW, strPLANT, strCREATEDBY, "MaNNO POST");
                        message += strMESSAGE;
                        if (message.ToUpper().Contains("VALUATION TYPE"))
                        {
                            btnAdd_ValutionType.Visible = true;
                        }
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
                //if (_OutputParameters2.Length > 0)
                //{
                //    for (int m = 0; m < _OutputParameters1.Length; m++)
                //    {
                //        strManno = _OutputParameters2[m].MBLNR;
                //       // Sapbatch = _OutputParameters1[m].SAP_BATCH;
                //    }
                //}
                //else
                //{
                //    strManno = "";
                //   // Sapbatch = "";
                //}


                
                
                clsStandards.WriteLog(this, new Exception("lenth :" + _OutputParameters2.Length), clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 0, false);

                #region MyRegion

                for (int i = 0; i < GrGRNDetails.Rows.Count; i++)
                {
                    CheckBox cb = (CheckBox)GrGRNDetails.Rows[i].FindControl("chkSelect");
                    if (cb.Checked == true)
                    {
                        ObjPL.strItemCode = clsStandards.filter(GrGRNDetails.Rows[i].Cells[5].Text.Trim());
                        ObjPL.strVendorBatchNo = clsStandards.filter(GrGRNDetails.Rows[i].Cells[26].Text.Trim());
                        ObjPL.strLineItemNo = clsStandards.filter(GrGRNDetails.Rows[i].Cells[4].Text.Trim());
                        if (_OutputParameters1.Length > 0)
                        {
                            for (int m = 0; m < _OutputParameters1.Length; m++)
                            {
                                strItemcode = _OutputParameters2[m].MATNR;
                                strLineItemNo = ObjPL.strLineItemNo;
                                if (strItemcode.Trim() == ObjPL.strItemCode.Trim())
                                {
                                    if (CheckManno(_OutputParameters2[m].MBLNR.Trim(), strItemcode.Trim(), _OutputParameters2[m].CHARG.Trim(), strLineItemNo, _OutputParameters2[m].BWTAR.Trim()))
                                    {
                                        strManno = _OutputParameters2[m].MBLNR.Trim();
                                        Sapbatch = _OutputParameters2[m].CHARG.Trim();
                                        break;
                                    }
                                    else
                                    {
                                        strManno = "";
                                        Sapbatch = "";
                                    }
                                }
                            }
                        }
                        else
                        {
                            strManno = "";
                            Sapbatch = "";
                        }

                        ObjPL.strPlant = clsStandards.filter(GrGRNDetails.Rows[i].Cells[1].Text.Trim());
                        ObjPL.strPurchaseOrderNo = clsStandards.filter(GrGRNDetails.Rows[i].Cells[2].Text.Trim());
                        ObjPL.strPurchaseOrderDate = clsStandards.filter(GrGRNDetails.Rows[i].Cells[3].Text.Trim());
                        ObjPL.strVendorName = clsStandards.filter(GrGRNDetails.Rows[i].Cells[25].Text.Trim());
                        ObjPL.strItemType = clsStandards.filter(GrGRNDetails.Rows[i].Cells[6].Text.Trim());
                        ObjPL.strOrderQty = Convert.ToDecimal(clsStandards.filter(GrGRNDetails.Rows[i].Cells[7].Text.Trim()));
                        ObjPL.strUom = clsStandards.filter(GrGRNDetails.Rows[i].Cells[9].Text.Trim());
                        ObjPL.strAcceptedQty = Convert.ToDecimal(clsStandards.filter(GrGRNDetails.Rows[i].Cells[13].Text.Trim()));
                        ObjPL.strNoOfContainer = Convert.ToDecimal(clsStandards.filter(GrGRNDetails.Rows[i].Cells[18].Text.Trim()));
                        ObjPL.strPackQty = Convert.ToDecimal(clsStandards.filter(GrGRNDetails.Rows[i].Cells[24].Text.Trim()));
                        ObjPL.strGateEntryNo = clsStandards.filter(GrGRNDetails.Rows[i].Cells[12].Text.Trim());
                        ObjPL.strManfactureName = clsStandards.filter(GrGRNDetails.Rows[i].Cells[28].Text.Trim()); ;
                        ObjPL.strVendorBatchNo = clsStandards.filter(GrGRNDetails.Rows[i].Cells[26].Text.Trim());
                        ObjPL.strMFGDate = clsStandards.filter(GrGRNDetails.Rows[i].Cells[14].Text.Trim());
                        ObjPL.strEXPDate = clsStandards.filter(GrGRNDetails.Rows[i].Cells[15].Text.Trim());
                        ObjPL.strMANNo = strManno;
                        ObjPL.strSapBatchNo = Sapbatch;
                        ObjPL.strUser = clsStandards.current_Username();
                        ObjPL.strTareWt = clsStandards.filter(GrGRNDetails.Rows[i].Cells[22].Text.Trim()) == "" ? 0 : Convert.ToDecimal(clsStandards.filter(GrGRNDetails.Rows[i].Cells[22].Text.Trim()));
                        ObjPL.strGrossWt = clsStandards.filter(GrGRNDetails.Rows[i].Cells[21].Text.Trim()) == "" ? 0 : Convert.ToDecimal(clsStandards.filter(GrGRNDetails.Rows[i].Cells[21].Text.Trim()));
                        ObjPL.strNetWt = clsStandards.filter(GrGRNDetails.Rows[i].Cells[23].Text.Trim()) == "" ? 0 : Convert.ToDecimal(clsStandards.filter(GrGRNDetails.Rows[i].Cells[23].Text.Trim()));
                        ObjPL.Purchase_Ref = clsStandards.filter(GrGRNDetails.Rows[i].Cells[30].Text.Trim()) == "" ? 0 : Convert.ToInt32(clsStandards.filter(GrGRNDetails.Rows[i].Cells[30].Text.Trim()));
                        ObjPL.strMessgae = message;
                        ObjPL.AdvanceLicense1 = clsStandards.filter(GrGRNDetails.Rows[i].Cells[40].Text.Trim());
                        ObjPL.AdvanceLicense2 = clsStandards.filter(GrGRNDetails.Rows[i].Cells[41].Text.Trim());
                        ObjPL.strStroageLoc = clsStandards.filter(GrGRNDetails.Rows[i].Cells[34].Text.Trim()).ToUpper();

                        string Result = objBL.BL_Insert_ManNo(ObjPL);

                        GridViewRow gr = (GridViewRow)GrGRNDetails.Rows[i];
                        if (message.Contains("SUCCESS"))
                        {
                            cb.Enabled = false;
                            cb.Checked = false;
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
                
                #endregion

               
                if (message.Contains("SUCCESS"))
                {
                    //gr.BackColor = System.Drawing.Color.Yellow;
                    clsStandards.WriteLog(this, new Exception("Material Posted Successfully , Document No :  " + strManno), clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 0, true);
                }
                else
                {
                    clsStandards.WriteLog(this, new Exception("SAP Message : " + message), clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 0, true);
                }
            }
        }
        catch (Exception ex)
        {
            clsStandards.WriteLog(this, new Exception(ex.ToString()), clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 0, true);
            return;
        }
    }

    protected void btnClose_Click(object sender, EventArgs e)
    {
        try
        {
            Response.Redirect("~/Modules/frmMain.aspx", false);
        }
        catch (Exception ex)
        {
            clsStandards.WriteLog(this, new Exception(ex.Message), clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 0, true);
          
        }
        finally
        {

        }
    }

    protected void btnAdd_ValutionType_Click(object sender, EventArgs e)
    {

    }
    protected void BtnSaveAdd_Click(object sender, EventArgs e)
    {
        PL_PurchaseOrder objpl = new PL_PurchaseOrder();
        BL_PurchaseOrder objBL = new BL_PurchaseOrder();
        try
        {
            if (GrGRNDetails.Rows.Count > 0)
            {
                for (int i = 0; i < GrGRNDetails.Rows.Count; i++)
                {
                    CheckBox cb = (CheckBox)GrGRNDetails.Rows[i].FindControl("chkSelect");
                    if (cb.Checked == true)
                    {
                        objpl.strPurchaseOrder=clsStandards.filter(GrGRNDetails.Rows[i].Cells[2].Text.Trim());
                        objpl.strValuationType = txtValuationType.Text.Trim();
                        objpl.strLineNo = clsStandards.filter(GrGRNDetails.Rows[i].Cells[4].Text.Trim());
                        objpl.strPlantCode = clsStandards.current_Plant();
                        objpl.strRefno = clsStandards.filter(GrGRNDetails.Rows[i].Cells[30].Text.Trim());
                        objpl.strRRef = "0";

                        string result = objBL.BL_UpdateValuationType(objpl);
                        txtValuationType.Text = "";
                        btnGet_Click(sender, e);
                    }
                }
            }
        }
        catch (Exception ex)
        {

        }
    }
    protected void BtnCloseAdd_Click(object sender, EventArgs e)
    {

    }
}