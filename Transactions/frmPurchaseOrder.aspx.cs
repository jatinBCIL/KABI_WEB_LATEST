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
using System.Web.Services.Description;
using System.Net;
using System.Globalization;
using System.Text;

public partial class frmPurchaseOrder : System.Web.UI.Page
{
    BL_PurchaseOrder objBL = new BL_PurchaseOrder();
    PL_PurchaseOrder objPL = new PL_PurchaseOrder();
    PL_ItemMaster objPLItem = new PL_ItemMaster();
    public static string strFlg = "";
    DataTable dtImport = new DataTable();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                MultiView1.SetActiveView(View1);
                strFlg = "ADD";
                tblbtn.Visible = false;
                Session["REFNO_P"] = "";
                Session["Gen_Saved"] = "";
                Session["Rec_Saved"] = "";
              
                ViewState["table"] = null;
                ViewState["POtable"] = null;
                ViewState["PGrossWt"] = null;
                ViewState["PNetWt"] = null;
                ViewState["PTareWt"] = null;
                rdWeight.Visible = false;
                txtGrossWt.Attributes.Add("onkeypress", "button_click(this,'" + this.btnStartWeigh1.ClientID + "'); return DisableSplChars(event);");
                txtgross.Attributes.Add("onkeypress", "button_click(this,'" + this.btnStartWeigh2.ClientID + "'); return DisableSplChars(event);");
                Close();
            }
            catch(Exception ex)
            {

            }
            //mp1.Show();
            return;

        }
        
    }

    protected void btnGet_Click(object sender, EventArgs e)
    {
        objBL = new BL_PurchaseOrder();
        objPL = new PL_PurchaseOrder();
        DataTable dtGrn = new DataTable();
        objPLItem = new PL_ItemMaster();

        dtGrn.Columns.Add("PurchaseOrderNo");
        dtGrn.Columns.Add("Plant");
        dtGrn.Columns.Add("PurchaseOrderDate");
        dtGrn.Columns.Add("LineItemNo");
        dtGrn.Columns.Add("VendorName");
        dtGrn.Columns.Add("ItemCode");
        dtGrn.Columns.Add("ItemDesc");
        dtGrn.Columns.Add("ItemType");
        dtGrn.Columns.Add("OrderQty");
        dtGrn.Columns.Add("Uom");
        dtGrn.Columns.Add("EwayBillNo");
        dtGrn.Columns.Add("EwayBillDate");
        dtGrn.Columns.Add("TransPorter");
        dtGrn.Columns.Add("TransPortMode");
        dtGrn.Columns.Add("TruckNo");

        try
        {

            WebReference.z_ws_po_details _PO = new WebReference.z_ws_po_details();
            WebReference.ZDMF001 _Item = new WebReference.ZDMF001();
            WebReference.ZDMF001Response _Res = new WebReference.ZDMF001Response();
            WebReference.ZDBMS003[] _ITFinal = new WebReference.ZDBMS003[0];
            WebReference.ZDBMS004[] _IT_EWAY = new WebReference.ZDBMS004[0];

            _PO.Proxy = new WebProxy();

            _PO.Credentials = new NetworkCredential(System.Configuration.ConfigurationManager.AppSettings["SapUser"].ToString(), System.Configuration.ConfigurationManager.AppSettings["SapPass"].ToString());
            _PO.Proxy.Credentials = new NetworkCredential(System.Configuration.ConfigurationManager.AppSettings["SapUser"].ToString(), System.Configuration.ConfigurationManager.AppSettings["SapPass"].ToString());

            _Item.PURCHASEORDER = txtPurchaseOrder.Text.Trim();
            _Item.PLANT = clsStandards.current_Plant().ToString();
            _Item.IT_EWAY = _IT_EWAY;
            _Item.IT_FINAL = _ITFinal;

            var req = new MemoryStream();
            var reqxml = new System.Xml.Serialization.XmlSerializer(_Item.GetType());
            reqxml.Serialize(req, _Item);
            string strxml = Encoding.UTF8.GetString(req.ToArray());
            clsStandards.WriteSoapXML(strxml, "PurchaseOrder - Request", clsStandards.GetCurrentPageName().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());

            _Res = _PO.ZDMF001(_Item);
            _ITFinal = _Res.IT_FINAL;
            _IT_EWAY = _Res.IT_EWAY;

            var res = new MemoryStream();
            var resxml = new System.Xml.Serialization.XmlSerializer(_Res.GetType());
            resxml.Serialize(res, _Res);
            string xml = Encoding.UTF8.GetString(res.ToArray());
            clsStandards.WriteSoapXML(xml, "PurchaseOrder - Response", clsStandards.GetCurrentPageName().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());


            for (int i = 0; i < _ITFinal.Length; i++)
            {
                tblbtn.Visible = true;
                DataRow dr = dtGrn.NewRow();

                dr["PurchaseOrderNo"] = _ITFinal[i].PO_NUMBER;
                dr["Plant"] = _ITFinal[i].PLANT;
                dr["PurchaseOrderDate"] = _ITFinal[i].DOC_DATE;
                dr["LineItemNo"] = _ITFinal[i].PO_ITEM;
                dr["VendorName"] = _ITFinal[i].NAME;
                dr["ItemCode"] = _ITFinal[i].MATERIAL;
                dr["ItemDesc"] = _ITFinal[i].SHORT_TEXT;
                dr["ItemType"] = _ITFinal[i].MATL_TYPE;
                dr["OrderQty"] = _ITFinal[i].QUANTITY;
                dr["Uom"] = _ITFinal[i].UNIT;
                dr["EwayBillNo"] = "";
                dr["EwayBillDate"] = "";
                dr["TransPorter"] = "";
                dr["TransPortMode"] = "";
                dr["TruckNo"] = "";

                dtGrn.Rows.Add(dr);
                dtGrn.AcceptChanges();
            }


            Insert_ItemMaster(dtGrn);
            ViewState["POtable"] = dtGrn;
            grPurchaseOrder.DataSource = dtGrn;
            grPurchaseOrder.DataBind();

        }
        catch (Exception ex)
        {
            clsStandards.WriteLog(this, new Exception(ex.Message), clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 0, true);
        }
        finally
        {
            objBL = null;
            objPL = null;
            objPLItem = null;
        }

    }

    protected void btnNet12_Click(object sender, EventArgs e)
    {
        objBL = new BL_PurchaseOrder();
        objPL = new PL_PurchaseOrder();
        DataTable dtGrn = new DataTable();
        objPLItem = new PL_ItemMaster();

        dtGrn.Columns.Add("PurchaseOrderNo");
        dtGrn.Columns.Add("Plant");
        dtGrn.Columns.Add("PurchaseOrderDate");
        dtGrn.Columns.Add("LineItemNo");
        dtGrn.Columns.Add("VendorName");
        dtGrn.Columns.Add("ItemCode");
        dtGrn.Columns.Add("ItemDesc");
        dtGrn.Columns.Add("ItemType");
        dtGrn.Columns.Add("OrderQty");
        dtGrn.Columns.Add("Uom");
        dtGrn.Columns.Add("EwayBillNo");
        dtGrn.Columns.Add("EwayBillDate");
        dtGrn.Columns.Add("TransPorter");
        dtGrn.Columns.Add("TransPortMode");
        dtGrn.Columns.Add("TruckNo");
        dtGrn.Columns.Add("AdvanceLicense");
        dtGrn.Columns.Add("Sap_BatchNo");

        try
        {
           
                WebReference.z_ws_po_details _PO = new WebReference.z_ws_po_details();
                WebReference.ZDMF001 _Item = new WebReference.ZDMF001();
                WebReference.ZDMF001Response _Res = new WebReference.ZDMF001Response();
                WebReference.ZDBMS003[] _ITFinal = new WebReference.ZDBMS003[0];
                WebReference.ZDBMS004[] _IT_EWAY = new WebReference.ZDBMS004[0];

                _PO.Proxy = new WebProxy();

                _PO.Credentials = new NetworkCredential(System.Configuration.ConfigurationManager.AppSettings["SapUser"].ToString(), System.Configuration.ConfigurationManager.AppSettings["SapPass"].ToString());
                _PO.Proxy.Credentials = new NetworkCredential(System.Configuration.ConfigurationManager.AppSettings["SapUser"].ToString(), System.Configuration.ConfigurationManager.AppSettings["SapPass"].ToString());

                _Item.PURCHASEORDER = txtPurchaseOrder.Text.Trim();
                _Item.PLANT = clsStandards.current_Plant().ToString();
                _Item.IT_EWAY = _IT_EWAY;
                _Item.IT_FINAL = _ITFinal;

                var req = new MemoryStream();
                var reqxml = new System.Xml.Serialization.XmlSerializer(_Item.GetType());
                reqxml.Serialize(req, _Item);
                string strxml = Encoding.UTF8.GetString(req.ToArray());
                clsStandards.WriteSoapXML(strxml, "PurchaseOrder - Request", clsStandards.GetCurrentPageName().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());

                _Res = _PO.ZDMF001(_Item);
                _ITFinal = _Res.IT_FINAL;
                _IT_EWAY = _Res.IT_EWAY;
                //_IT_EWAY = _Res.IT_EWAY.;

                var res = new MemoryStream();
                var resxml = new System.Xml.Serialization.XmlSerializer(_Res.GetType());
                resxml.Serialize(res, _Res);
                string xml = Encoding.UTF8.GetString(res.ToArray());
                clsStandards.WriteSoapXML(xml, "PurchaseOrder - Response", clsStandards.GetCurrentPageName().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());

               // clsStandards.WriteLog(this, new Exception(xml), clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 0, true);

                //clsStandards.WriteLog(this, new Exception(xml), clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 0, true);

                for (int i = 0; i < _IT_EWAY.Length; i++)
                {
                    clsStandards.WriteLog(this, new Exception(_IT_EWAY[i].CHARG), clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 0, true);
                }
               //_IT_EWAY[0].CHARG 
                for (int i = 0; i < _ITFinal.Length; i++)
                {
                    tblbtn.Visible = true;
                    DataRow dr = dtGrn.NewRow();

                    dr["PurchaseOrderNo"] = _ITFinal[i].PO_NUMBER;
                    dr["Plant"] = _ITFinal[i].PLANT;
                    dr["PurchaseOrderDate"] = _ITFinal[i].DOC_DATE;
                    dr["LineItemNo"] = _ITFinal[i].PO_ITEM;
                    dr["VendorName"] = _ITFinal[i].NAME;
                    dr["ItemCode"] = _ITFinal[i].MATERIAL;
                    dr["ItemDesc"] = _ITFinal[i].SHORT_TEXT;
                    dr["ItemType"] = _ITFinal[i].MATL_TYPE;
                    dr["OrderQty"] = _ITFinal[i].QUANTITY;
                    dr["Uom"] = _ITFinal[i].UNIT;
                    dr["EwayBillNo"] = "";
                    dr["EwayBillDate"] = "";
                    dr["TransPorter"] = "";
                    dr["TransPortMode"] = "";
                    dr["TruckNo"] = "";
                    dr["AdvanceLicense"] = "";
                    dr["Sap_BatchNo"] = _IT_EWAY[0].CHARG;


                    dtGrn.Rows.Add(dr);
                    dtGrn.AcceptChanges();
                }


                Insert_ItemMaster(dtGrn);
                ViewState["POtable"] = dtGrn;
                grPurchaseOrder.DataSource = dtGrn;
                grPurchaseOrder.DataBind();

            
            
            

        }
        catch (Exception ex)
        {
            clsStandards.WriteLog(this, new Exception(ex.Message), clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 0, true);
        }
        finally
        {
            objBL = null;
            objPL = null;
            objPLItem = null;
        }

    }

    #region MyRegion


    //protected void btnGet_Click(object sender, EventArgs e)
    //{
    //    objBL = new BL_PurchaseOrder();
    //    objPL = new PL_PurchaseOrder();
    //    DataTable dtGrn = new DataTable();
    //    objPLItem = new PL_ItemMaster();

    //    dtGrn.Columns.Add("PurchaseOrderNo");
    //    dtGrn.Columns.Add("Plant");
    //    dtGrn.Columns.Add("PurchaseOrderDate");
    //    dtGrn.Columns.Add("LineItemNo");
    //    dtGrn.Columns.Add("VendorName");
    //    dtGrn.Columns.Add("ItemCode");
    //    dtGrn.Columns.Add("ItemDesc");
    //    dtGrn.Columns.Add("ItemType");
    //    dtGrn.Columns.Add("OrderQty");
    //    dtGrn.Columns.Add("Uom");
    //    dtGrn.Columns.Add("EwayBillNo");
    //    dtGrn.Columns.Add("EwayBillDate");
    //    dtGrn.Columns.Add("TransPorter");
    //    dtGrn.Columns.Add("TransPortMode");
    //    dtGrn.Columns.Add("TruckNo");
    //    dtGrn.Columns.Add("AdvanceLicense");

    //    try
    //    {
    //        if (RadioButton1.Checked == true)
    //        {
    //            WebReference.z_ws_po_details _PO = new WebReference.z_ws_po_details();
    //            WebReference.ZDMF001 _Item = new WebReference.ZDMF001();
    //            WebReference.ZDMF001Response _Res = new WebReference.ZDMF001Response();
    //            WebReference.ZDBMS003[] _ITFinal = new WebReference.ZDBMS003[0];
    //            WebReference.ZDBMS004[] _IT_EWAY = new WebReference.ZDBMS004[0];

    //            _PO.Proxy = new WebProxy();

    //            _PO.Credentials = new NetworkCredential(System.Configuration.ConfigurationManager.AppSettings["SapUser"].ToString(), System.Configuration.ConfigurationManager.AppSettings["SapPass"].ToString());
    //            _PO.Proxy.Credentials = new NetworkCredential(System.Configuration.ConfigurationManager.AppSettings["SapUser"].ToString(), System.Configuration.ConfigurationManager.AppSettings["SapPass"].ToString());

    //            _Item.PURCHASEORDER = txtPurchaseOrder.Text.Trim();
    //            _Item.PLANT = clsStandards.current_Plant().ToString();
    //            _Item.IT_EWAY = _IT_EWAY;
    //            _Item.IT_FINAL = _ITFinal;

    //            var req = new MemoryStream();
    //            var reqxml = new System.Xml.Serialization.XmlSerializer(_Item.GetType());
    //            reqxml.Serialize(req, _Item);
    //            string strxml = Encoding.UTF8.GetString(req.ToArray());
    //            clsStandards.WriteSoapXML(strxml, "PurchaseOrder - Request", clsStandards.GetCurrentPageName().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());

    //            _Res = _PO.ZDMF001(_Item);
    //            _ITFinal = _Res.IT_FINAL;
    //            _IT_EWAY = _Res.IT_EWAY;

    //            var res = new MemoryStream();
    //            var resxml = new System.Xml.Serialization.XmlSerializer(_Res.GetType());
    //            resxml.Serialize(res, _Res);
    //            string xml = Encoding.UTF8.GetString(res.ToArray());
    //            clsStandards.WriteSoapXML(xml, "PurchaseOrder - Response", clsStandards.GetCurrentPageName().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());


    //            for (int i = 0; i < _ITFinal.Length; i++)
    //            {
    //                tblbtn.Visible = true;
    //                DataRow dr = dtGrn.NewRow();

    //                dr["PurchaseOrderNo"] = _ITFinal[i].PO_NUMBER;
    //                dr["Plant"] = _ITFinal[i].PLANT;
    //                dr["PurchaseOrderDate"] = _ITFinal[i].DOC_DATE;
    //                dr["LineItemNo"] = _ITFinal[i].PO_ITEM;
    //                dr["VendorName"] = _ITFinal[i].NAME;
    //                dr["ItemCode"] = _ITFinal[i].MATERIAL;
    //                dr["ItemDesc"] = _ITFinal[i].SHORT_TEXT;
    //                dr["ItemType"] = _ITFinal[i].MATL_TYPE;
    //                dr["OrderQty"] = _ITFinal[i].QUANTITY;
    //                dr["Uom"] = _ITFinal[i].UNIT;
    //                dr["EwayBillNo"] = "";
    //                dr["EwayBillDate"] = "";
    //                dr["TransPorter"] = "";
    //                dr["TransPortMode"] = "";
    //                dr["TruckNo"] = "";
    //                dr["AdvanceLicense"] = "";


    //                dtGrn.Rows.Add(dr);
    //                dtGrn.AcceptChanges();
    //            }


    //            Insert_ItemMaster(dtGrn);
    //            ViewState["POtable"] = dtGrn;
    //            grPurchaseOrder.DataSource = dtGrn;
    //            grPurchaseOrder.DataBind();

    //        }
    //        else if ((RadioButton2.Checked == true))
    //        {
    //            #region MyRegion




    //           // STO_Delivery.Z_WS_OUT_DELIVRY _STO_No = new STO_Delivery.Z_WS_OUT_DELIVRY();
    //           // STO_Delivery.ZDMF019 _InputParameters = new STO_Delivery.ZDMF019();
    //           // STO_Delivery.ZDMF019Response _Response = new STO_Delivery.ZDMF019Response();
    //           // STO_Delivery.ZDBMS034[] _OutputParameters = new STO_Delivery.ZDBMS034[0];
    //           // STO_Delivery.ZDBMS033 _OutputParameters2 = new STO_Delivery.ZDBMS033();
    //           // //_InputParameters.RETURN = new STO_Delivery.BAPIRET2[1];
    //           // //_InputParameters.RETURN[0] = new STO_Delivery.BAPIRET2();

    //           // clsStandards.WriteLog(this, new Exception("start 1"), clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 0, true);
    //           // _STO_No.Proxy = new WebProxy();
    //           // _STO_No.Credentials = new NetworkCredential(System.Configuration.ConfigurationManager.AppSettings["SapUser"].ToString(), System.Configuration.ConfigurationManager.AppSettings["SapPass"].ToString());
    //           // _STO_No.Proxy.Credentials = new NetworkCredential(System.Configuration.ConfigurationManager.AppSettings["SapUser"].ToString(), System.Configuration.ConfigurationManager.AppSettings["SapPass"].ToString());

    //           // clsStandards.WriteLog(this, new Exception("start 2"), clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 0, true);

    //           // //_InputParameters.IM_OUTBOUND.PO_NUMBER = txtPurchaseOrder.Text.Trim();
    //           // //_InputParameters.IM_OUTBOUND.PLANT = clsStandards.current_Plant();

    //           // //_OutputParameters2.PO_NUMBER = txtPurchaseOrder.Text.Trim();
    //           //// _OutputParameters2.PLANT = clsStandards.current_Plant();
    //           // _InputParameters.IM_OUTBOUND = _OutputParameters2;

    //           // clsStandards.WriteLog(this, new Exception("start IM"), clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 0, true);

    //           // _InputParameters.IM_OUTBOUND.PO_NUMBER = txtPurchaseOrder.Text.Trim();
    //           // _InputParameters.IM_OUTBOUND.PLANT = clsStandards.current_Plant();



    //           // clsStandards.WriteLog(this, new Exception("start 3"), clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 0, true);

    //           // //_InputParameters.IM_OUTBOUND.PO_NUMBER = string.Empty;
    //           // //_InputParameters.IM_OUTBOUND.PLANT = string.Empty;
    //           // //_InputParameters.IM_OUTBOUND.DOC_DATE = string.Empty;
    //           // //_InputParameters.IM_OUTBOUND.PO_ITEM = string.Empty;
    //           // //_InputParameters.IM_OUTBOUND.VENDOR_BATCH = string.Empty;
    //           // //_InputParameters.IM_OUTBOUND.DELIV_ITEM = string.Empty;
    //           // //_InputParameters.IM_OUTBOUND.GM_CODE = string.Empty;
    //           // //_InputParameters.IM_OUTBOUND.VAL_TYPE = string.Empty;
    //           // //_InputParameters.IM_OUTBOUND.ENTRY_QNT = 0;
    //           // //_InputParameters.IM_OUTBOUND.ENTRY_UOM = string.Empty;



    //           // clsStandards.WriteLog(this, new Exception("start 4"), clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 0, true);

    //           // var req = new MemoryStream();
    //           // var reqxml = new System.Xml.Serialization.XmlSerializer(_InputParameters.GetType());
    //           // reqxml.Serialize(req, _InputParameters);
    //           // string strxml = Encoding.UTF8.GetString(req.ToArray());
    //           // clsStandards.WriteSoapXML(strxml, "ProcessOrderPicking - Request", clsStandards.GetCurrentPageName().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());

    //           // clsStandards.WriteLog(this, new Exception(strxml), clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 0, true);
    //           // //_InputParameters = _STO_No.ZDMF017(_Response);
    //           // //_OutputParameters = _InputParameters.;
    //           // clsStandards.WriteLog(this, new Exception("start 5"), clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 0, true);
    //           // _Response = _STO_No.ZDMF019(_InputParameters);
    //           //  _OutputParameters = _Response.EX_OUTBOUND;

    //           // var res = new MemoryStream();
    //           // var resxml = new System.Xml.Serialization.XmlSerializer(_Response.GetType());
    //           // resxml.Serialize(res, _Response);
    //           // string xml = Encoding.UTF8.GetString(res.ToArray());
    //           // clsStandards.WriteSoapXML(xml, "ProcessOrderPicking - Response", clsStandards.GetCurrentPageName().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());

    //           // clsStandards.WriteLog(this, new Exception(xml), clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 0, true);

    //           // for (int i = 0; i < _OutputParameters.Length; i++)
    //           // {
    //           //     tblbtn.Visible = true;
    //           //     DataRow dr = dtGrn.NewRow();

    //           //     dr["PurchaseOrderNo"] = _OutputParameters[i].PO_NUMBER;
    //           //     dr["Plant"] = _OutputParameters[i].PLANT;
    //           //     dr["PurchaseOrderDate"] = _OutputParameters[i].DOC_DATE;
    //           //     dr["LineItemNo"] = _OutputParameters[i].PO_ITEM;
    //           //     dr["VendorName"] = _OutputParameters[i].BWTAR;
    //           //     dr["ItemCode"] = _OutputParameters[i].MATNR;
    //           //     dr["ItemDesc"] = _OutputParameters[i].MAKTX;
    //           //     dr["ItemType"] = _OutputParameters[i].MTART;
    //           //     dr["OrderQty"] = _OutputParameters[i].ENTRY_QNT;
    //           //     dr["Uom"] = _OutputParameters[i].ENTRY_UOM;
    //           //     dr["EwayBillNo"] = "";
    //           //     dr["EwayBillDate"] = "";
    //           //     dr["TransPorter"] = "";
    //           //     dr["TransPortMode"] = "";
    //           //     dr["TruckNo"] = "";

    //           //     dtGrn.Rows.Add(dr);
    //           //     dtGrn.AcceptChanges();
    //            //  }

    //            #endregion

    //            #region OLd


    //            //STO_Delivery.Z_WS_OUT_DELIVERY _STO = new STO_Delivery.Z_WS_OUT_DELIVERY();
    //            //STO_Delivery.ZDMF017 _Item = new STO_Delivery.ZDMF017();
    //            //STO_Delivery.ZDMF017Response _Res = new STO_Delivery.ZDMF017Response();
    //            //STO_Delivery.ZDBMS033 IM_OUTBOUND = new STO_Delivery.ZDBMS033();
    //            //STO_Delivery.BAPIRET2 RETURN = new STO_Delivery.BAPIRET2();

    //            //_STO.Proxy = new WebProxy();

    //            //_STO.Credentials = new NetworkCredential(System.Configuration.ConfigurationManager.AppSettings["SapUser"].ToString(), System.Configuration.ConfigurationManager.AppSettings["SapPass"].ToString());
    //            //_STO.Proxy.Credentials = new NetworkCredential(System.Configuration.ConfigurationManager.AppSettings["SapUser"].ToString(), System.Configuration.ConfigurationManager.AppSettings["SapPass"].ToString());

    //            //IM_OUTBOUND.PO_NUMBER = txtPurchaseOrder.Text.Trim();
    //            //IM_OUTBOUND.PLANT = clsStandards.current_Plant().ToString();
    //            ////_Item.PURCHASEORDER = txtPurchaseOrder.Text.Trim();
    //            ////_Item.PLANT = clsStandards.current_Plant().ToString();
    //            ////_Item.IT_EWAY = _IT_EWAY;
    //            ////_Item.IT_FINAL = _ITFinal;

    //            //var req = new MemoryStream();
    //            //var reqxml = new System.Xml.Serialization.XmlSerializer(IM_OUTBOUND.GetType());
    //            //reqxml.Serialize(req, IM_OUTBOUND);
    //            //string strxml = Encoding.UTF8.GetString(req.ToArray());
    //            //clsStandards.WriteSoapXML(strxml, "PurchaseOrder - Request", clsStandards.GetCurrentPageName().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());

    //            //// var newitem = _STO.ZDMF017(_Item);
    //            //// _Res = _STO.ZDMF017(_Item);
    //            //// IM_OUTBOUND = _Res.;
    //            //// RETURN = _Res.RETURN[0];

    //            //// IM_OUTBOUND = _Item.IM_OUTBOUND;
    //            //// RETURN = _Item.RETURN[0];

    //            //var res = new MemoryStream();
    //            //var resxml = new System.Xml.Serialization.XmlSerializer(IM_OUTBOUND.GetType());
    //            //resxml.Serialize(res, IM_OUTBOUND);
    //            //string xml = Encoding.UTF8.GetString(res.ToArray());
    //            //clsStandards.WriteSoapXML(xml, "PurchaseOrder - Response", clsStandards.GetCurrentPageName().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());


    //            ////for (int i = 0; i < IM_OUTBOUND.Length; i++)
    //            ////{
    //            ////    tblbtn.Visible = true;
    //            ////    DataRow dr = dtGrn.NewRow();

    //            ////    dr["PurchaseOrderNo"] = IM_OUTBOUND[i].PO_NUMBER;
    //            ////    dr["Plant"] = IM_OUTBOUND[i].PLANT;
    //            ////    dr["PurchaseOrderDate"] = IM_OUTBOUND[i].DOC_DATE;
    //            ////    dr["LineItemNo"] = IM_OUTBOUND[i].PO_ITEM;
    //            ////    dr["VendorName"] = IM_OUTBOUND[i].NAME;
    //            ////    dr["ItemCode"] = IM_OUTBOUND[i].MATERIAL;
    //            ////    dr["ItemDesc"] = IM_OUTBOUND[i].SHORT_TEXT;
    //            ////    dr["ItemType"] = IM_OUTBOUND[i].MATL_TYPE;
    //            ////    dr["OrderQty"] = IM_OUTBOUND[i].QUANTITY;
    //            ////    dr["Uom"] = IM_OUTBOUND[i].UNIT;
    //            ////    dr["EwayBillNo"] = "";
    //            ////    dr["EwayBillDate"] = "";
    //            ////    dr["TransPorter"] = "";
    //            ////    dr["TransPortMode"] = "";
    //            ////    dr["TruckNo"] = "";

    //            ////    dtGrn.Rows.Add(dr);
    //            ////    dtGrn.AcceptChanges();
    //            //// }



    //            //tblbtn.Visible = true;
    //            //DataRow dr = dtGrn.NewRow();

    //            //dr["PurchaseOrderNo"] = IM_OUTBOUND.PO_NUMBER;
    //            //dr["Plant"] = IM_OUTBOUND.PLANT;
    //            //dr["PurchaseOrderDate"] = IM_OUTBOUND.DOC_DATE;
    //            //dr["LineItemNo"] = IM_OUTBOUND.PO_ITEM;
    //            //dr["VendorName"] = IM_OUTBOUND.VENDOR_BATCH;
    //            //dr["ItemCode"] = IM_OUTBOUND.GM_CODE;
    //            //dr["ItemDesc"] = IM_OUTBOUND.DOCUMENT_NO;
    //            //dr["ItemType"] = IM_OUTBOUND.VAL_TYPE;
    //            //dr["OrderQty"] = IM_OUTBOUND.QUANTITY;
    //            //dr["Uom"] = IM_OUTBOUND.ENTRY_UOM;
    //            //dr["EwayBillNo"] = "";
    //            //dr["EwayBillDate"] = "";
    //            //dr["TransPorter"] = "";
    //            //dr["TransPortMode"] = "";
    //            //dr["TruckNo"] = "";
    //            //dr["AdvanceLicense"] = "";

    //            //dtGrn.Rows.Add(dr);
    //            //dtGrn.AcceptChanges();
    //            #endregion

    //            #region Test
    //            //STO_Delivery.Z_WS_OUT_DELIVRY _PO = new STO_Delivery.Z_WS_OUT_DELIVRY();
    //            //STO_Delivery.ZDMF019 _Item = new STO_Delivery.ZDMF019();
    //            //STO_Delivery.ZDMF019Response _Res = new STO_Delivery.ZDMF019Response();
    //            //STO_Delivery.ZDBMS033 IM_OUTBOUND = new STO_Delivery.ZDBMS033();
    //            //STO_Delivery.ZDBMS034[] EX_OUTBOUND = new STO_Delivery.ZDBMS034[0];
    //            //STO_Delivery.BAPIRET2[] RETURN = new STO_Delivery.BAPIRET2[0];
    //            //_Item.IM_OUTBOUND = new STO_Delivery.ZDBMS033();

    //            //_PO.Proxy = new WebProxy();

    //            //_PO.Credentials = new NetworkCredential(System.Configuration.ConfigurationManager.AppSettings["SapUser"].ToString(), System.Configuration.ConfigurationManager.AppSettings["SapPass"].ToString());
    //            //_PO.Proxy.Credentials = new NetworkCredential(System.Configuration.ConfigurationManager.AppSettings["SapUser"].ToString(), System.Configuration.ConfigurationManager.AppSettings["SapPass"].ToString());

    //            //clsStandards.WriteLog(this, new Exception("start 1"), clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 0, true);

    //            //_Item.IM_OUTBOUND.DOCUMENT_NO = txtPurchaseOrder.Text.Trim();
    //            //_Item.IM_OUTBOUND.PLANT = clsStandards.current_Plant().ToString();
    //            //clsStandards.WriteLog(this, new Exception("start 2"), clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 0, true);

    //            //_Item.IM_OUTBOUND = IM_OUTBOUND;
    //            //_Item.RETURN = RETURN;

    //            //clsStandards.WriteLog(this, new Exception("start 3"), clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 0, true);

    //            //var req = new MemoryStream();
    //            //var reqxml = new System.Xml.Serialization.XmlSerializer(_Item.GetType());
    //            //reqxml.Serialize(req, _Item);
    //            //string strxml = Encoding.UTF8.GetString(req.ToArray());
    //            //clsStandards.WriteSoapXML(strxml, "PurchaseOrder - Request", clsStandards.GetCurrentPageName().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());

    //            //clsStandards.WriteLog(this, new Exception(strxml), clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 0, true);

    //            //clsStandards.WriteLog(this, new Exception("start 4"), clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 0, true);
    //            //_Res = _PO.ZDMF019(_Item);
    //            //EX_OUTBOUND = _Res.EX_OUTBOUND;
    //            //RETURN = _Res.RETURN;

    //            //var res = new MemoryStream();
    //            //var resxml = new System.Xml.Serialization.XmlSerializer(_Res.GetType());
    //            //resxml.Serialize(res, _Res);
    //            //string xml = Encoding.UTF8.GetString(res.ToArray());
    //            //clsStandards.WriteSoapXML(xml, "PurchaseOrder - Response", clsStandards.GetCurrentPageName().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());

    //            //clsStandards.WriteLog(this, new Exception(xml), clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 0, true);

    //            //for (int i = 0; i < EX_OUTBOUND.Length; i++)
    //            //{
    //            //    tblbtn.Visible = true;
    //            //    DataRow dr = dtGrn.NewRow();

    //            //    dr["PurchaseOrderNo"] = EX_OUTBOUND[i].PO_NUMBER;
    //            //    dr["Plant"] = EX_OUTBOUND[i].PLANT;
    //            //    dr["PurchaseOrderDate"] = EX_OUTBOUND[i].DOC_DATE;
    //            //    dr["LineItemNo"] = EX_OUTBOUND[i].PO_ITEM;
    //            //    dr["VendorName"] = EX_OUTBOUND[i].PO_ITEM;
    //            //    dr["ItemCode"] = EX_OUTBOUND[i].BWTAR;
    //            //    dr["ItemDesc"] = EX_OUTBOUND[i].MATNR;
    //            //    dr["ItemType"] = EX_OUTBOUND[i].MTART;
    //            //    dr["OrderQty"] = EX_OUTBOUND[i].ENTRY_QNT;
    //            //    dr["Uom"] = EX_OUTBOUND[i].ENTRY_UOM;
    //            //    dr["EwayBillNo"] = "";
    //            //    dr["EwayBillDate"] = "";
    //            //    dr["TransPorter"] = "";
    //            //    dr["TransPortMode"] = "";
    //            //    dr["TruckNo"] = "";
    //            //    dr["AdvanceLicense"] = "";


    //            //    dtGrn.Rows.Add(dr);
    //            //    dtGrn.AcceptChanges();
    //            //}

    //            #endregion

    //            STO_Delivery.Z_WS_OUT_DELIVRY _PO = new STO_Delivery.Z_WS_OUT_DELIVRY();
    //            STO_Delivery.ZDMF019 _Item = new STO_Delivery.ZDMF019();
    //            STO_Delivery.ZDMF019Response _Res = new STO_Delivery.ZDMF019Response();
    //            STO_Delivery.ZDBMS033 IM_OUTBOUND = new STO_Delivery.ZDBMS033();
    //            STO_Delivery.ZDBMS034[] EX_OUTBOUND = new STO_Delivery.ZDBMS034[0];
    //            STO_Delivery.BAPIRET2[] RETURN = new STO_Delivery.BAPIRET2[0];
    //            _Item.IM_OUTBOUND = new STO_Delivery.ZDBMS033();
    //            _Item.RETURN = new STO_Delivery.BAPIRET2[1];
    //            _Item.RETURN[0] = new STO_Delivery.BAPIRET2();

    //            _PO.Proxy = new WebProxy();

    //            _PO.Credentials = new NetworkCredential(System.Configuration.ConfigurationManager.AppSettings["SapUser"].ToString(), System.Configuration.ConfigurationManager.AppSettings["SapPass"].ToString());
    //            _PO.Proxy.Credentials = new NetworkCredential(System.Configuration.ConfigurationManager.AppSettings["SapUser"].ToString(), System.Configuration.ConfigurationManager.AppSettings["SapPass"].ToString());

    //            clsStandards.WriteLog(this, new Exception("start 1"), clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 0, true);

    //           // _Item.IM_OUTBOUND.DOCUMENT_NO = txtPurchaseOrder.Text.Trim();
    //          //  _Item.IM_OUTBOUND.PLANT = clsStandards.current_Plant().ToString();

    //            clsStandards.WriteLog(this, new Exception("start 2"), clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 0, true);

    //            _Item.IM_OUTBOUND = IM_OUTBOUND;
    //            _Item.IM_OUTBOUND.DOCUMENT_NO = IM_OUTBOUND.DOCUMENT_NO;
    //           // _Item.IM_OUTBOUND.DELIVERY_NOTE = IM_OUTBOUND.DELIVERY_NOTE;
    //            _Item.RETURN = RETURN;

    //            //clsStandards.WriteLog(this, new Exception(_Item.IM_OUTBOUND.DOCUMENT_NO), clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 0, true);

    //            clsStandards.WriteLog(this, new Exception("start 3"), clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 0, true);

    //            var req = new MemoryStream();
    //            var reqxml = new System.Xml.Serialization.XmlSerializer(_Item.GetType());
    //            reqxml.Serialize(req, _Item);
    //            string strxml = Encoding.UTF8.GetString(req.ToArray());
    //            clsStandards.WriteSoapXML(strxml, "PurchaseOrder - Request", clsStandards.GetCurrentPageName().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());

    //            clsStandards.WriteLog(this, new Exception(strxml), clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 0, true);

    //            clsStandards.WriteLog(this, new Exception("start 4"), clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 0, true);
    //            _Res = _PO.ZDMF019(_Item);
    //            EX_OUTBOUND = _Res.EX_OUTBOUND;
    //           // RETURN = _Res.RETURN;

    //            var res = new MemoryStream();
    //            var resxml = new System.Xml.Serialization.XmlSerializer(_Res.GetType());
    //            resxml.Serialize(res, _Res);
    //            string xml = Encoding.UTF8.GetString(res.ToArray());
    //            clsStandards.WriteSoapXML(xml, "PurchaseOrder - Response", clsStandards.GetCurrentPageName().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());

    //            clsStandards.WriteLog(this, new Exception(xml), clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 0, true);

    //            for (int i = 0; i < EX_OUTBOUND.Length; i++)
    //            {
    //                tblbtn.Visible = true;
    //                DataRow dr = dtGrn.NewRow();

    //                dr["PurchaseOrderNo"] = EX_OUTBOUND[i].PO_NUMBER;
    //                dr["Plant"] = EX_OUTBOUND[i].PLANT;
    //                dr["PurchaseOrderDate"] = EX_OUTBOUND[i].DOC_DATE;
    //                dr["LineItemNo"] = EX_OUTBOUND[i].PO_ITEM;
    //                dr["VendorName"] = EX_OUTBOUND[i].PO_ITEM;
    //                dr["ItemCode"] = EX_OUTBOUND[i].BWTAR;
    //                dr["ItemDesc"] = EX_OUTBOUND[i].MATNR;
    //                dr["ItemType"] = EX_OUTBOUND[i].MTART;
    //                dr["OrderQty"] = EX_OUTBOUND[i].ENTRY_QNT;
    //                dr["Uom"] = EX_OUTBOUND[i].ENTRY_UOM;
    //                dr["EwayBillNo"] = "";
    //                dr["EwayBillDate"] = "";
    //                dr["TransPorter"] = "";
    //                dr["TransPortMode"] = "";
    //                dr["TruckNo"] = "";
    //                dr["AdvanceLicense"] = "";


    //                dtGrn.Rows.Add(dr);
    //                dtGrn.AcceptChanges();
    //            }

    //            Insert_ItemMaster(dtGrn);
    //            ViewState["POtable"] = dtGrn;
    //            grPurchaseOrder.DataSource = dtGrn;
    //            grPurchaseOrder.DataBind();
    //        }

    //    }
    //    catch (Exception ex)
    //    {
    //        clsStandards.WriteLog(this, new Exception(ex.Message), clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 0, true);
    //    }
    //    finally
    //    {
    //        objBL = null;
    //        objPL = null;
    //        objPLItem = null;
    //    }

    //}


    //protected void btnSTOGet_Click(object sender, EventArgs e)
    //{
    //    objBL = new BL_PurchaseOrder();
    //    objPL = new PL_PurchaseOrder();
    //    DataTable dtGrn = new DataTable();
    //    objPLItem = new PL_ItemMaster();

    //    dtGrn.Columns.Add("PurchaseOrderNo");
    //    dtGrn.Columns.Add("Plant");
    //    dtGrn.Columns.Add("PurchaseOrderDate");
    //    dtGrn.Columns.Add("LineItemNo");
    //    dtGrn.Columns.Add("VendorName");
    //    dtGrn.Columns.Add("ItemCode");
    //    dtGrn.Columns.Add("ItemDesc");
    //    dtGrn.Columns.Add("ItemType");
    //    dtGrn.Columns.Add("OrderQty");
    //    dtGrn.Columns.Add("Uom");
    //    dtGrn.Columns.Add("EwayBillNo");
    //    dtGrn.Columns.Add("EwayBillDate");
    //    dtGrn.Columns.Add("TransPorter");
    //    dtGrn.Columns.Add("TransPortMode");
    //    dtGrn.Columns.Add("TruckNo");
    //    dtGrn.Columns.Add("AdvanceLicense");

    //    try
    //    {
    //        //if (RadioButton1.Checked == true)
    //        //{
    //        WebReference.z_ws_po_details _PO = new WebReference.z_ws_po_details();
    //        WebReference.ZDMF001 _Item1 = new WebReference.ZDMF001();
    //        WebReference.ZDMF001Response _Res1 = new WebReference.ZDMF001Response();
    //        WebReference.ZDBMS003[] _ITFinal = new WebReference.ZDBMS003[0];
    //        WebReference.ZDBMS004[] _IT_EWAY = new WebReference.ZDBMS004[0];

    //        _PO.Proxy = new WebProxy();

    //        _PO.Credentials = new NetworkCredential(System.Configuration.ConfigurationManager.AppSettings["SapUser"].ToString(), System.Configuration.ConfigurationManager.AppSettings["SapPass"].ToString());
    //        _PO.Proxy.Credentials = new NetworkCredential(System.Configuration.ConfigurationManager.AppSettings["SapUser"].ToString(), System.Configuration.ConfigurationManager.AppSettings["SapPass"].ToString());

    //        _Item1.PURCHASEORDER = txtPurchaseOrder.Text.Trim();
    //        _Item1.PLANT = clsStandards.current_Plant().ToString();
    //        _Item1.IT_EWAY = _IT_EWAY;
    //        _Item1.IT_FINAL = _ITFinal;

    //        var req = new MemoryStream();
    //        var reqxml = new System.Xml.Serialization.XmlSerializer(_Item1.GetType());
    //        reqxml.Serialize(req, _Item1);
    //        string strxml = Encoding.UTF8.GetString(req.ToArray());
    //        clsStandards.WriteSoapXML(strxml, "PurchaseOrder - Request", clsStandards.GetCurrentPageName().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());

    //        _Res1 = _PO.ZDMF001(_Item1);
    //        _ITFinal = _Res1.IT_FINAL;
    //        _IT_EWAY = _Res1.IT_EWAY;

    //        var res = new MemoryStream();
    //        var resxml = new System.Xml.Serialization.XmlSerializer(_Res1.GetType());
    //        resxml.Serialize(res, _Res1);
    //        string xml = Encoding.UTF8.GetString(res.ToArray());
    //        clsStandards.WriteSoapXML(xml, "PurchaseOrder - Response", clsStandards.GetCurrentPageName().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());


    //        for (int i = 0; i < _ITFinal.Length; i++)
    //        {
    //            tblbtn.Visible = true;
    //            DataRow dr = dtGrn.NewRow();

    //            dr["PurchaseOrderNo"] = _ITFinal[i].PO_NUMBER;
    //            dr["Plant"] = _ITFinal[i].PLANT;
    //            dr["PurchaseOrderDate"] = _ITFinal[i].DOC_DATE;
    //            dr["LineItemNo"] = _ITFinal[i].PO_ITEM;
    //            dr["VendorName"] = _ITFinal[i].NAME;
    //            dr["ItemCode"] = _ITFinal[i].MATERIAL;
    //            dr["ItemDesc"] = _ITFinal[i].SHORT_TEXT;
    //            dr["ItemType"] = _ITFinal[i].MATL_TYPE;
    //            dr["OrderQty"] = _ITFinal[i].QUANTITY;
    //            dr["Uom"] = _ITFinal[i].UNIT;
    //            dr["EwayBillNo"] = "";
    //            dr["EwayBillDate"] = "";
    //            dr["TransPorter"] = "";
    //            dr["TransPortMode"] = "";
    //            dr["TruckNo"] = "";
    //            dr["AdvanceLicense"] = "";


    //            dtGrn.Rows.Add(dr);
    //            dtGrn.AcceptChanges();
    //        }


    //        // Insert_ItemMaster(dtGrn);
    //        ViewState["POtable"] = dtGrn;
    //        grPurchaseOrder.DataSource = dtGrn;
    //        grPurchaseOrder.DataBind();

    //        //}
    //        //else if ((RadioButton2.Checked == true))
    //        //{
    //        clsStandards.WriteLog(this, new Exception("sSTO Start"), clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 0, true);

    //            STO_Delivery.Z_WS_OUT_DELIVRY _STO = new STO_Delivery.Z_WS_OUT_DELIVRY();
    //            STO_Delivery.ZDMF019 _Item = new STO_Delivery.ZDMF019();
    //            STO_Delivery.ZDMF019Response _Res = new STO_Delivery.ZDMF019Response();
    //            STO_Delivery.ZDBMS033 IM_OUTBOUND = new STO_Delivery.ZDBMS033();
    //            STO_Delivery.ZDBMS034[] EX_OUTBOUND = new STO_Delivery.ZDBMS034[0];
    //            STO_Delivery.BAPIRET2[] RETURN = new STO_Delivery.BAPIRET2[0];
    //            _Item.IM_OUTBOUND = new STO_Delivery.ZDBMS033();
    //            _Item.RETURN = new STO_Delivery.BAPIRET2[1];
    //            _Item.RETURN[0] = new STO_Delivery.BAPIRET2();

    //            _STO.Proxy = new WebProxy();

    //            _STO.Credentials = new NetworkCredential(System.Configuration.ConfigurationManager.AppSettings["SapUser"].ToString(), System.Configuration.ConfigurationManager.AppSettings["SapPass"].ToString());
    //            _STO.Proxy.Credentials = new NetworkCredential(System.Configuration.ConfigurationManager.AppSettings["SapUser"].ToString(), System.Configuration.ConfigurationManager.AppSettings["SapPass"].ToString());

    //            clsStandards.WriteLog(this, new Exception("start 1"), clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 0, true);

    //            #region MyRegion
    //            _Item.IM_OUTBOUND.ACTION = "";
    //            _Item.IM_OUTBOUND.ACTION_TYPE = "";
    //           // _Item.IM_OUTBOUND.DOCUMENT_NO = txtSTONumber.Text.Trim();
    //            _Item.IM_OUTBOUND.DOC_DATE = System.DateTime.Now.ToString("yyyy-MM-dd"); ;
    //            _Item.IM_OUTBOUND.POSTING_DATE = System.DateTime.Now.ToString("yyyy-MM-dd");
    //           // _Item.IM_OUTBOUND.DELIVERY_NOTE = txtSTONumber.Text.Trim(); ;
    //            _Item.IM_OUTBOUND.GR_GI_SLIP_NO = "";
    //            _Item.IM_OUTBOUND.BILL_OF_LANDING = "";
    //            _Item.IM_OUTBOUND.HEADER_TEXT = "Test";
    //            _Item.IM_OUTBOUND.GATEN = "";
    //            _Item.IM_OUTBOUND.GATE_DATE = "";
    //            _Item.IM_OUTBOUND.SALES_TAX = "";
    //            _Item.IM_OUTBOUND.AIRWAY_BILL_NO = "";
    //            _Item.IM_OUTBOUND.AIRWAY_BILL_DATE = "";
    //            _Item.IM_OUTBOUND.WAY_BILL_NOT_REQUIRED = "";
    //            _Item.IM_OUTBOUND.GROSS_WT = "";
    //            _Item.IM_OUTBOUND.NET_WT = "";
    //            _Item.IM_OUTBOUND.TARE_WT = "";
    //            _Item.IM_OUTBOUND.TAR_WT_UNIT = "";
    //            _Item.IM_OUTBOUND.PACK = "";
    //            _Item.IM_OUTBOUND.MTAX = "";
    //            _Item.IM_OUTBOUND.LICNO = "";
    //            _Item.IM_OUTBOUND.LIFNR = "";
    //            _Item.IM_OUTBOUND.TMODE = "";
    //            _Item.IM_OUTBOUND.RECNUM = "";
    //            _Item.IM_OUTBOUND.TRUCK = "";
    //            _Item.IM_OUTBOUND.ITEM_OK = "";
    //            _Item.IM_OUTBOUND.STORAGE_LOC = "";
    //            _Item.IM_OUTBOUND.STORAGE_TYPE = "";
    //            _Item.IM_OUTBOUND.VENDOR_BATCH = "";
    //            _Item.IM_OUTBOUND.HSDAT = "";
    //            _Item.IM_OUTBOUND.VFDAT = "";
    //            _Item.IM_OUTBOUND.MFNAM = "";
    //            _Item.IM_OUTBOUND.MATNR = clsStandards.filter(grPurchaseOrder.Rows[0].Cells[6].Text.Trim());
    //            _Item.IM_OUTBOUND.CHARG = "";
    //            _Item.IM_OUTBOUND.R9NUM = "";
    //            _Item.IM_OUTBOUND.PACKS = "";
    //            _Item.IM_OUTBOUND.PACK_QTY = 0;
    //            _Item.IM_OUTBOUND.PO_NUMBER = lblDocumentNo.Text.Trim();
    //            _Item.IM_OUTBOUND.GM_CODE = "";
    //            _Item.IM_OUTBOUND.PLANT = clsStandards.current_Plant();
    //            _Item.IM_OUTBOUND.MOVE_TYPE = "";
    //            _Item.IM_OUTBOUND.VAL_TYPE = "";
    //            _Item.IM_OUTBOUND.ENTRY_QNT = Convert.ToDecimal(clsStandards.filter(grPurchaseOrder.Rows[0].Cells[9].Text.Trim()));
    //            _Item.IM_OUTBOUND.ENTRY_UOM = clsStandards.filter(grPurchaseOrder.Rows[10].Cells[10].Text.Trim());
    //            _Item.IM_OUTBOUND.PO_ITEM = "";
    //            _Item.IM_OUTBOUND.MVT_IND = "";
    //            _Item.IM_OUTBOUND.PROD_DATE = "";
    //            _Item.IM_OUTBOUND.EXPIRYDATE = "";
    //            _Item.IM_OUTBOUND.BILLNO = "";
    //            _Item.IM_OUTBOUND.MAKTX = "";
    //            _Item.IM_OUTBOUND.MDSLIP = "";
    //            _Item.IM_OUTBOUND.DELIV_ITEM = clsStandards.filter(grPurchaseOrder.Rows[0].Cells[4].Text.Trim());
    //            _Item.IM_OUTBOUND.QUANTITY = 0;
    //            _Item.IM_OUTBOUND.BASE_UOM = clsStandards.filter(grPurchaseOrder.Rows[0].Cells[12].Text.Trim());


    //            #endregion



    //            clsStandards.WriteLog(this, new Exception("start 2"), clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 0, true);

    //           // _Item.IM_OUTBOUND = IM_OUTBOUND;
    //           // _Item.IM_OUTBOUND.DOCUMENT_NO = IM_OUTBOUND.DOCUMENT_NO;
    //            // _Item.IM_OUTBOUND.DELIVERY_NOTE = IM_OUTBOUND.DELIVERY_NOTE;
    //            _Item.RETURN = RETURN;

    //            //clsStandards.WriteLog(this, new Exception(_Item.IM_OUTBOUND.DOCUMENT_NO), clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 0, true);

    //            clsStandards.WriteLog(this, new Exception("start 3"), clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 0, true);

    //            var req1 = new MemoryStream();
    //            var reqxml1 = new System.Xml.Serialization.XmlSerializer(_Item.GetType());
    //            reqxml1.Serialize(req1, _Item);
    //            string strxml1 = Encoding.UTF8.GetString(req1.ToArray());
    //            clsStandards.WriteSoapXML(strxml1, "PurchaseOrder - Request", clsStandards.GetCurrentPageName().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());

    //            clsStandards.WriteLog(this, new Exception(strxml), clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 0, true);

    //            clsStandards.WriteLog(this, new Exception("start 4"), clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 0, true);
    //            _Res = _STO.ZDMF019(_Item);
    //            EX_OUTBOUND = _Res.EX_OUTBOUND;
    //             RETURN = _Res.RETURN;

    //            var res1 = new MemoryStream();
    //            var resxml1 = new System.Xml.Serialization.XmlSerializer(_Res.GetType());
    //            resxml1.Serialize(res1, _Res);
    //            string xml1 = Encoding.UTF8.GetString(res1.ToArray());
    //            clsStandards.WriteSoapXML(xml1, "PurchaseOrder - Response", clsStandards.GetCurrentPageName().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());

    //            clsStandards.WriteLog(this, new Exception(xml), clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 0, true);

    //            for (int i = 0; i < EX_OUTBOUND.Length; i++)
    //            {
    //                tblbtn.Visible = true;
    //                DataRow dr = dtGrn.NewRow();

    //                dr["PurchaseOrderNo"] = EX_OUTBOUND[i].PO_NUMBER;
    //                dr["Plant"] = EX_OUTBOUND[i].PLANT;
    //                dr["PurchaseOrderDate"] = EX_OUTBOUND[i].DOC_DATE;
    //                dr["LineItemNo"] = EX_OUTBOUND[i].PO_ITEM;
    //                dr["VendorName"] = EX_OUTBOUND[i].PO_ITEM;
    //                dr["ItemCode"] = EX_OUTBOUND[i].BWTAR;
    //                dr["ItemDesc"] = EX_OUTBOUND[i].MATNR;
    //                dr["ItemType"] = EX_OUTBOUND[i].MTART;
    //                dr["OrderQty"] = EX_OUTBOUND[i].ENTRY_QNT;
    //                dr["Uom"] = EX_OUTBOUND[i].ENTRY_UOM;
    //                dr["EwayBillNo"] = "";
    //                dr["EwayBillDate"] = "";
    //                dr["TransPorter"] = "";
    //                dr["TransPortMode"] = "";
    //                dr["TruckNo"] = "";
    //                dr["AdvanceLicense"] = "";


    //                dtGrn.Rows.Add(dr);
    //                dtGrn.AcceptChanges();
    //            }

    //            //Insert_ItemMaster(dtGrn);
    //            ViewState["POtable"] = dtGrn;
    //            grPurchaseOrder.DataSource = dtGrn;
    //            grPurchaseOrder.DataBind();
    //        }

    //  //  }
    //    catch (Exception ex)
    //    {
    //        clsStandards.WriteLog(this, new Exception(ex.Message), clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 0, true);
    //    }
    //    finally
    //    {
    //        objBL = null;
    //        objPL = null;
    //        objPLItem = null;
    //    }

    //}
    #endregion


   

    private void GetItemCodeValidate()
    {
        objBL = new BL_PurchaseOrder();
        DataTable dtItemcode = new DataTable();
        try
        {
            dtItemcode = objBL.BL_GetItemCodeValidate();
            Session["Itemcodetable"] = dtItemcode;
        }
        catch (Exception ex)
        {

        }
    }

    private string Check_PurchasOrderEnter(PL_PurchaseOrder objFeilds)
    {
        objBL = new BL_PurchaseOrder();
        try
        {
            string strResult = objBL.BL_Check_PurchasOrderEnter(objFeilds);
            return strResult;
        }
        catch (Exception ex)
        {
            clsStandards.WriteLog(this, new Exception(ex.Message), clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 0, true);
            throw ex;
        }
        finally
        {
            objBL = null;
        }
    }

    protected void btnPrint_Click(object sender, EventArgs e)
    {
        objPL = new PL_PurchaseOrder();
        DataTable getdt = new DataTable();

        try
        {
            if (!Chk_Checkbox())
            {
                clsStandards.WriteLog(this, new Exception(" Please Check the Item from List "), clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 0, true);
                return;
            }

            if (grPurchaseOrder.Rows.Count > 0)
            {
                for (int i = 0; i < grPurchaseOrder.Rows.Count; i++)
                {
                    CheckBox cb = (CheckBox)grPurchaseOrder.Rows[i].FindControl("chkSelect");
                    if (cb.Checked == true)
                    {

                        objPL.strPlantCode = clsStandards.filter(grPurchaseOrder.Rows[i].Cells[1].Text.Trim());
                        objPL.strPurchaseOrder = clsStandards.filter(grPurchaseOrder.Rows[i].Cells[2].Text.Trim());
                        objPL.strLineNo = clsStandards.filter(grPurchaseOrder.Rows[i].Cells[4].Text.Trim());
                        objPL.strOrderDate = clsStandards.filter(grPurchaseOrder.Rows[i].Cells[3].Text.Trim());
                        objPL.strVendorName = clsStandards.filter(grPurchaseOrder.Rows[i].Cells[5].Text.Trim());
                        objPL.strItemCode = clsStandards.filter(grPurchaseOrder.Rows[i].Cells[6].Text.Trim());
                        objPL.strItemType = clsStandards.filter(grPurchaseOrder.Rows[i].Cells[8].Text.Trim());
                        objPL.dOrderQty = Convert.ToDecimal(clsStandards.filter(grPurchaseOrder.Rows[i].Cells[9].Text.Trim()));
                        objPL.strUom = clsStandards.filter(grPurchaseOrder.Rows[i].Cells[10].Text.Trim());
                        objPL.strEwayBill = clsStandards.filter(grPurchaseOrder.Rows[i].Cells[11].Text.Trim());
                        objPL.strEwayBillDate = clsStandards.filter(grPurchaseOrder.Rows[i].Cells[12].Text.Trim());
                        objPL.strTransporter = clsStandards.filter(grPurchaseOrder.Rows[i].Cells[13].Text.Trim());
                        //objPL.strTransportMode = clsStandards.filter(grPurchaseOrder.Rows[i].Cells[14].Text.Trim());
                        objPL.strTruckNo = clsStandards.filter(grPurchaseOrder.Rows[i].Cells[15].Text.Trim());
                        lblPurchaseOrderNo.Text = clsStandards.filter(grPurchaseOrder.Rows[i].Cells[2].Text.Trim());
                        lblEwayBillNo.Text = clsStandards.filter(grPurchaseOrder.Rows[i].Cells[11].Text.Trim());
                        //lblModeOfTransport.Text = clsStandards.filter(grPurchaseOrder.Rows[i].Cells[14].Text.Trim());
                        lblOrderDate.Text = clsStandards.ConvertDateTime(clsStandards.filter(grPurchaseOrder.Rows[i].Cells[3].Text.Trim()), "yyyy-MM-dd", "yyyy-MM-dd");
                        lblTransporter.Text = clsStandards.filter(grPurchaseOrder.Rows[i].Cells[13].Text.Trim());
                        lblTruckNo.Text = clsStandards.filter(grPurchaseOrder.Rows[i].Cells[15].Text.Trim());
                        lbllineItemNo_Gen.Text = clsStandards.filter(grPurchaseOrder.Rows[i].Cells[4].Text.Trim());
                        lblitemcode_gen.Text = clsStandards.filter(grPurchaseOrder.Rows[i].Cells[6].Text.Trim());

                        if (Insert_PurchaseOrder(objPL))
                        {
                            if (!AssignDetails("GENERAL", objPL.strPurchaseOrder, objPL.strLineNo))
                            {

                                MultiView1.SetActiveView(View2);
                            }
                            else
                            {
                                MultiView1.SetActiveView(View2);
                            }
                        }
                        else
                        {
                            MultiView1.SetActiveView(View1);
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            clsStandards.WriteLog(this, new Exception(ex.Message), clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 0, true);
        }
        finally
        {
            objPL = null;
        }
    }

    protected void btnNext_Click(object sender, EventArgs e)
    {
        objPL = new PL_PurchaseOrder();
        DataTable getdt = new DataTable();

        try
        {
            ViewState["Weight"] = null;
            ViewState["table"] = null;
            ViewState["SaveWeight"] = null;
            Session["Itemcodetable"] = null;
            bool blItem = false;
            if (!Chk_Checkbox())
            {
                clsStandards.WriteLog(this, new Exception(" Please Check the Item from List "), clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 0, true);
                return;
            }

            if (grPurchaseOrder.Rows.Count > 0)
            {
                for (int i = 0; i < grPurchaseOrder.Rows.Count; i++)
                {
                    CheckBox cb = (CheckBox)grPurchaseOrder.Rows[i].FindControl("chkSelect");
                    if (cb.Checked == true)
                    {
                        objPL.strPlantCode = clsStandards.filter(grPurchaseOrder.Rows[i].Cells[1].Text.Trim());
                        objPL.strPurchaseOrder = clsStandards.filter(grPurchaseOrder.Rows[i].Cells[2].Text.Trim());
                        objPL.strOrderDate = clsStandards.filter(grPurchaseOrder.Rows[i].Cells[3].Text.Trim());
                        objPL.strLineNo = clsStandards.filter(grPurchaseOrder.Rows[i].Cells[4].Text.Trim());
                        objPL.strVendorName = clsStandards.filter(grPurchaseOrder.Rows[i].Cells[5].Text.Trim());
                        objPL.strItemCode = clsStandards.filter(grPurchaseOrder.Rows[i].Cells[6].Text.Trim());
                        objPL.strItemType = clsStandards.filter(grPurchaseOrder.Rows[i].Cells[8].Text.Trim());
                        objPL.dOrderQty = Convert.ToDecimal(clsStandards.filter(grPurchaseOrder.Rows[i].Cells[9].Text.Trim()));
                        objPL.strUom = clsStandards.filter(grPurchaseOrder.Rows[i].Cells[10].Text.Trim());
                        objPL.strEwayBill = clsStandards.filter(grPurchaseOrder.Rows[i].Cells[11].Text.Trim());
                        objPL.strEwayBillDate = clsStandards.filter(grPurchaseOrder.Rows[i].Cells[12].Text.Trim());
                        objPL.strTransporter = clsStandards.filter(grPurchaseOrder.Rows[i].Cells[13].Text.Trim());
                        objPL.strTransportMode = clsStandards.filter(grPurchaseOrder.Rows[i].Cells[14].Text.Trim());
                        objPL.strTruckNo = clsStandards.filter(grPurchaseOrder.Rows[i].Cells[15].Text.Trim());
                        lblPurchaseOrderRec.Text = clsStandards.filter(grPurchaseOrder.Rows[i].Cells[2].Text.Trim());
                        lblMaterialCode.Text = clsStandards.filter(grPurchaseOrder.Rows[i].Cells[6].Text.Trim());
                        lblMaterialDesc.Text = clsStandards.filter(grPurchaseOrder.Rows[i].Cells[7].Text.Trim());
                        lblLineNo.Text = clsStandards.filter(grPurchaseOrder.Rows[i].Cells[4].Text.Trim());
                        lblOrderQty.Text = clsStandards.filter(grPurchaseOrder.Rows[i].Cells[9].Text.Trim());
                        lblUOM.Text = clsStandards.filter(grPurchaseOrder.Rows[i].Cells[10].Text.Trim());

                        GetItemCodeValidate();
                        DataTable dtItemcode = new DataTable();
                        dtItemcode = (DataTable)Session["Itemcodetable"];
                        if (dtItemcode.Rows.Count > 0)
                        {
                            for (int itm = 0; itm < dtItemcode.Rows.Count; itm++)
                            {
                                if (lblMaterialCode.Text.Trim() == dtItemcode.Rows[itm]["Itemcode"].ToString().Trim())
                                {
                                    blItem = true;
                                    break;
                                }
                            }
                        }

                        if (lblUOM.Text.Trim().ToUpper() == "KG" || lblUOM.Text.Trim().ToUpper() == "G" || lblUOM.Text.Trim().ToUpper() == "L" || lblUOM.Text.Trim().ToUpper() == "ML")
                        {

                            if (blItem == true)
                            {
                                trWeight.Visible = false;
                            }
                            else
                            {
                                trWeight.Visible = true;
                            }
                        }
                        else
                        {
                            trWeight.Visible = false;
                        }
                        if (!AssignDetails("RECEIVING", objPL.strPurchaseOrder, objPL.strLineNo))
                        {

                            MultiView1.SetActiveView(View3);
                        }
                        else
                        {
                            MultiView1.SetActiveView(View3);
                        }
                    }
                }
            }

        }
        catch (Exception ex)
        {
            clsStandards.WriteLog(this, new Exception(ex.Message), clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 0, true);

            throw new Exception(ex.ToString());
        }
        finally
        {
            objPL = null;
        }
    }

    private bool AssignDetails(string strType, string strpurchaseNo, string strLineNo)
    {
        DataTable getdt = new DataTable();
        PL_PurchaseOrder objFields = new PL_PurchaseOrder();
        bool blItem = false;
        getdt = GetPurchaseOrderData(strType, strpurchaseNo, clsStandards.current_Plant(), strLineNo);
        if (getdt.Rows.Count > 0)
        {
            if (strType.ToUpper().Contains("GENERAL"))
            {
                lblPurchaseOrderNo.Text = clsStandards.filter(getdt.Rows[0]["PurchaseOrderNo"].ToString());
                lblEwayBillNo.Text = clsStandards.filter(getdt.Rows[0]["EwayBillno"].ToString());
                lblModeOfTransport.SelectedValue = clsStandards.filter(getdt.Rows[0]["TransportMode"].ToString());
                lblOrderDate.Text = clsStandards.filter(getdt.Rows[0]["APurchaseOrderDate"].ToString());
                lblTransporter.Text = clsStandards.filter(getdt.Rows[0]["TransPorter"].ToString());
                lblTruckNo.Text = clsStandards.filter(getdt.Rows[0]["TruckNo"].ToString());
                //lbllineItemNo_Gen.Text = clsStandards.filter(getdt.Rows[0]["LineItemNo"].ToString());
                //lblitemcode_gen.Text = clsStandards.filter(getdt.Rows[0]["Itemcode"].ToString());
                txtManfName.Text = clsStandards.filter(getdt.Rows[0]["ManfName"].ToString());
                txtSupplierName.Text = clsStandards.filter(getdt.Rows[0]["SupplierName"].ToString());
                txtEntryGate.Text = clsStandards.filter(getdt.Rows[0]["GateEntryNo"].ToString());
                txtGLAcctNo.Text = clsStandards.filter(getdt.Rows[0]["GI_AccountNo"].ToString());
                txtGRNO.Text = clsStandards.filter(getdt.Rows[0]["GR_RR_NO"].ToString());
                txtHeaderText.Text = clsStandards.filter(getdt.Rows[0]["HeaderText"].ToString());
                txtStorageLoc.Text = clsStandards.filter(getdt.Rows[0]["StorageLocation"].ToString());
                txtCostCenter.Text = clsStandards.filter(getdt.Rows[0]["CostCenter"].ToString());
                txtGRGISLIPNO.Text = clsStandards.filter(getdt.Rows[0]["GR_GI_SLIP_NO"].ToString());
                clsStandards.WriteLog(this, new Exception(getdt.Rows[0]["AGateEntryDate"].ToString()), clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 0, false);

                clsStandards.WriteLog(this, new Exception(getdt.Rows[0]["BEwayBillDate"].ToString()), clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 0, false);

                clsStandards.WriteLog(this, new Exception(getdt.Rows[0]["APurchaseOrderDate"].ToString()), clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 0, false);

                clsStandards.WriteLog(this, new Exception(getdt.Rows[0]["GateEntryDate"].ToString()), clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 0, false);

                if (!string.IsNullOrEmpty(getdt.Rows[0]["APurchaseOrderDate"].ToString()))
                {
                    lblOrderDate.Text = clsStandards.ConvertDateTime(clsStandards.filter(getdt.Rows[0]["APurchaseOrderDate"].ToString()), "yyyy-MM-dd", "yyyy-MM-dd");
                }
                else
                {
                    lblOrderDate.Text = "";
                }
                if (!string.IsNullOrEmpty(getdt.Rows[0]["AGateEntryDate"].ToString()))
                {
                    txtEntryGateDate.Text = clsStandards.ConvertDateTime(clsStandards.filter(getdt.Rows[0]["AGateEntryDate"].ToString()), "yyyy-MM-dd", "dd-MM-yyyy");
                }
                else
                {
                    txtEntryGateDate.Text = "";
                }
                if (!string.IsNullOrEmpty(getdt.Rows[0]["BEwayBillDate"].ToString()))
                {
                    txtEwayBillDate.Text = clsStandards.ConvertDateTime(clsStandards.filter(getdt.Rows[0]["BEwayBillDate"].ToString()), "yyyy-MM-dd", "dd-MM-yyyy");
                }
                else
                {
                    txtEwayBillDate.Text = "";
                }
                txtChallan.Text = clsStandards.filter(getdt.Rows[0]["DeliveryChallanNo"].ToString());
                if (!string.IsNullOrEmpty(getdt.Rows[0]["DeliveryChallanDate"].ToString()))
                {
                    txtChallanNo.Text = clsStandards.ConvertDateTime(clsStandards.filter(getdt.Rows[0]["DeliveryChallanDate"].ToString()), "dd-MM-yyyy", "dd-MM-yyyy");
                }
                else
                {
                    txtChallanNo.Text = "";
                }
                if (!string.IsNullOrEmpty(getdt.Rows[0]["InvoiceDate"].ToString()))
                {
                    txtInvoiceDate.Text = clsStandards.ConvertDateTime(clsStandards.filter(getdt.Rows[0]["InvoiceDate"].ToString()), "dd-MM-yyyy", "dd-MM-yyyy");
                }
                else
                {
                    txtInvoiceDate.Text = "";
                }
                txtInvoiceNo.Text = clsStandards.filter(getdt.Rows[0]["InvoiceNo"].ToString());
                txtSupplierName.Text = clsStandards.filter(getdt.Rows[0]["SupplierName"].ToString());
                txtContainerSealed.Text = clsStandards.filter(getdt.Rows[0]["ProperlySealedContainer"].ToString());
                txtContainerNoDamaged.Text = clsStandards.filter(getdt.Rows[0]["ContainersDamagedContainer"].ToString());
                txtContainerMatched.Text = clsStandards.filter(getdt.Rows[0]["MatchedWithSealContainer"].ToString());

                if (clsStandards.filter(getdt.Rows[0]["CertificateAvaliable"].ToString()) == "1")
                {
                    rbCertificateAvaliableY.Checked = true;
                }
                else
                {
                    rbCertificateAvaliableN.Checked = true;
                }
                if (clsStandards.filter(getdt.Rows[0]["ManfApproved"].ToString()) == "1")
                {
                    rbManfApprovedY.Checked = true;
                }
                else
                {
                    rbManfApprovedN.Checked = true;
                }
                if (clsStandards.filter(getdt.Rows[0]["SupplierApproved"].ToString()) == "1")
                {
                    rbSupplierApprovedY.Checked = true;
                }
                else
                {
                    rbSupplierApprovedN.Checked = true;
                }
                if (clsStandards.filter(getdt.Rows[0]["VehicleClean"].ToString()) == "1")
                {
                    rbVehicleCleanY.Checked = true;
                }
                else
                {
                    rbVehicleCleanN.Checked = true;
                }
                if (clsStandards.filter(getdt.Rows[0]["ProperlyClosed"].ToString()) == "1")
                {
                    rbProtedFromRainY.Checked = true;
                }
                else
                {
                    rbProtedFromRainN.Checked = true;
                } if (clsStandards.filter(getdt.Rows[0]["CarryingOdorous"].ToString()) == "1")
                {
                    rbCarryingAroundY.Checked = true;
                }
                else
                {
                    rbCarryingAroundN.Checked = true;
                } if (clsStandards.filter(getdt.Rows[0]["LabelInOrder"].ToString()) == "1")
                {
                    rbInOrderY.Checked = true;
                }
                else
                {
                    rbInOrderN.Checked = true;
                } if (clsStandards.filter(getdt.Rows[0]["ContainersDamaged"].ToString()) == "1")
                {
                    rbContainerDamagedY.Checked = true;
                }
                else
                {
                    rbContainerDamagedN.Checked = true;
                } if (clsStandards.filter(getdt.Rows[0]["ProperlySealed"].ToString()) == "1")
                {
                    rbSealedY.Checked = true;
                }
                else
                {
                    rbSealedN.Checked = true;
                } 
                if (clsStandards.filter(getdt.Rows[0]["MatchedWithSeal"].ToString()) == "1")
                {
                    rbContainerMatchedY.Checked = true;
                }
                else if (clsStandards.filter(getdt.Rows[0]["MatchedWithSeal"].ToString()) == "2")
                {
                    rbContainerMatchedNA.Checked = true;
                }
                else
                {
                    rbContainerMatchedN.Checked = true;
                }
                //if (clsStandards.filter(getdt.Rows[0]["VendorDocuments"].ToString()) == "1")
                //{
                //    rbVendorDocumentsY.Checked = true;
                //}
                //else
                //{
                //    rbVendorDocumentsN.Checked = true;
                //}
            }
            else
            {
                lblPurchaseOrderRec.Text = clsStandards.filter(getdt.Rows[0]["PurchaseOrderNo"].ToString());
                //lblMaterialCode.Text = clsStandards.filter(getdt.Rows[0]["MaterialCode"].ToString());
                //lblMaterialDesc.Text = clsStandards.filter(getdt.Rows[0]["MaterialDesc"].ToString());
                //lblLineNo.Text = clsStandards.filter(getdt.Rows[0]["LineItemNo"].ToString());
                txtVendorBatchNo.Text = clsStandards.filter(getdt.Rows[0]["VendorBatchNo"].ToString());
                txtBalanceLife.Text = clsStandards.filter(getdt.Rows[0]["BalanceShelfLife"].ToString());
                //lblOrderQty.Text = clsStandards.filter(getdt.Rows[0]["OrderQty"].ToString());
                txtTotalContainers.Text = clsStandards.filter(getdt.Rows[0]["TotalContainers"].ToString());
                txtBalanceCode.Text = clsStandards.filter(getdt.Rows[0]["WeighingCode"].ToString());
                lblUOM.Text = clsStandards.filter(getdt.Rows[0]["Uom"].ToString());
                txtLicNo.Text = clsStandards.filter(getdt.Rows[0]["LIC_No"].ToString());
                txtGmCode.Text = clsStandards.filter(getdt.Rows[0]["GM_Code"].ToString());
                if (!string.IsNullOrEmpty(getdt.Rows[0]["AMFGDate"].ToString()))
                {
                    txtMfgDate.Text = clsStandards.ConvertDateTime(clsStandards.filter(getdt.Rows[0]["AMFGDate"].ToString()), "dd-MM-yyyy", "dd-MM-yyyy");
                }
                else
                {
                    txtMfgDate.Text = "";
                }
                if (!string.IsNullOrEmpty(getdt.Rows[0]["AExpDate"].ToString()))
                {
                    txtExpDate.Text = clsStandards.ConvertDateTime(clsStandards.filter(getdt.Rows[0]["AExpDate"].ToString()), "dd-MM-yyyy", "dd-MM-yyyy");
                }
                else
                {
                    txtExpDate.Text = "";
                }

                GetItemCodeValidate();
                DataTable dtItemcode = new DataTable();
                dtItemcode = (DataTable)Session["Itemcodetable"];
                if (dtItemcode.Rows.Count > 0)
                {
                    for (int itm = 0; itm < dtItemcode.Rows.Count; itm++)
                    {
                        if (lblMaterialCode.Text.Trim() == dtItemcode.Rows[itm]["Itemcode"].ToString().Trim())
                        {
                            blItem = true;
                            break;
                        }
                    }
                }


                if (lblUOM.Text.Trim().ToUpper() == "KG" || lblUOM.Text.Trim().ToUpper() == "G" || lblUOM.Text.Trim().ToUpper() == "L" || lblUOM.Text.Trim().ToUpper() == "ML")
                {
                    if (blItem == true)
                    {
                        trWeight.Visible = false;
                    }
                    else
                    {
                        trWeight.Visible = true;
                    }
                }
                else
                {
                    trWeight.Visible = false;
                }
            }
            return true;
        }
        else
        {
            return false;
        }
    }

    private bool Insert_PurchaseOrder(PL_PurchaseOrder ObjFields)
    {
        objBL = new BL_PurchaseOrder();
        bool resp = true;
        try
        {
            objPL.strUserId = clsStandards.current_Username();
            string result = objBL.BL_Insert_PurchaseOrder(objPL);

            Session["REFNO_P"] = result.Split('|').GetValue(0).ToString();
            lblRemainingQty.Text = result.Split('|').GetValue(2).ToString();

            if (Convert.ToDecimal(lblRemainingQty.Text) <= 0)
            {
                resp = false;
                clsStandards.WriteLog(this, new Exception("Total Material Receiving is Done . "), clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 0, true);
            }
            else
            {
                resp = true;
            }
        }
        catch (Exception ex)
        {
            clsStandards.WriteLog(this, new Exception(ex.Message), clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 0, true);
        }
        finally
        {
            objBL = null;
        }
        return resp;
    }

    private bool FieldsVaidation(string Type)
    {
        bool resp = true;
        try
        {
            if (Type.Contains("SaveGeneralDetails"))
            {

                if (!rbCertificateAvaliableY.Checked)
                {
                    resp = false;
                    clsStandards.WriteLog(this, new Exception("Please check  YES for Cerificate Analysis "), clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 0, true);
                }
                else if (!rbManfApprovedY.Checked)
                {
                    clsStandards.WriteLog(this, new Exception("Please check YES for Manufacture Approved"), clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 0, true);
                    resp = false;
                }
                else if (rbManfApprovedY.Checked && string.IsNullOrEmpty(txtManfName.Text))
                {
                    clsStandards.WriteLog(this, new Exception("Please enter Manufacture Name"), clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 0, true);
                    txtManfName.Focus();
                    resp = false;
                }
                else if (!rbSupplierApprovedY.Checked)
                {
                    clsStandards.WriteLog(this, new Exception("Please check YES for Supplier Approved"), clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 0, true);
                    resp = false;
                }
                else if (rbSupplierApprovedY.Checked && string.IsNullOrEmpty(txtSupplierName.Text))
                {
                    clsStandards.WriteLog(this, new Exception("Please Enter Supplier Name"), clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 0, true);
                    txtSupplierName.Focus();
                    resp = false;
                }
                else if (string.IsNullOrEmpty(txtEntryGate.Text))
                {
                    clsStandards.WriteLog(this, new Exception("Please Entry Gate"), clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 0, true);
                    txtEntryGate.Focus();
                    resp = false;
                }
                else if (string.IsNullOrEmpty(txtInvoiceDate.Text))
                {
                    clsStandards.WriteLog(this, new Exception("Please Enter Invoice Date"), clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 0, true);
                    txtInvoiceDate.Focus();
                    resp = false;
                }
                else if (string.IsNullOrEmpty(txtStorageLoc.Text))
                {
                    clsStandards.WriteLog(this, new Exception("Please Enter StorageLoc"), clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 0, true);
                    txtStorageLoc.Focus();
                    resp = false;
                }
                else if (string.IsNullOrEmpty(txtHeaderText.Text))
                {
                    clsStandards.WriteLog(this, new Exception("Please Enter HeaderText"), clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 0, true);
                    txtHeaderText.Focus();
                    resp = false;
                }
                else if (string.IsNullOrEmpty(txtGRNO.Text))
                {
                    clsStandards.WriteLog(this, new Exception("Please Enter GRNO"), clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 0, true);
                    txtGRNO.Focus();
                    resp = false;
                }
                else if (!rbVehicleCleanN.Checked && !rbVehicleCleanY.Checked)
                {
                    clsStandards.WriteLog(this, new Exception("Please check Vehicle Clean  or Not"), clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 0, true);
                    resp = false;
                }
                else if (!rbProtedFromRainN.Checked && !rbProtedFromRainY.Checked)
                {
                    clsStandards.WriteLog(this, new Exception("Please check Protected from Rain  or Not"), clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 0, true);
                    resp = false;
                }
                else if (!rbCarryingAroundN.Checked && !rbCarryingAroundY.Checked)
                {
                    clsStandards.WriteLog(this, new Exception("Please check Carrying Around  or Not"), clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 0, true);
                    resp = false;
                }
                else if (!rbInOrderN.Checked && !rbInOrderY.Checked)
                {
                    clsStandards.WriteLog(this, new Exception("Please check Container IN  or Not"), clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 0, true);
                    resp = false;
                }
                else if (rbInOrderN.Checked && string.IsNullOrEmpty(txtContainerNo.Text))
                {
                    clsStandards.WriteLog(this, new Exception("Please Enter Container No"), clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 0, true);
                    txtContainerNo.Focus();
                    resp = false;
                }
                else if (!rbContainerDamagedN.Checked && !rbContainerDamagedY.Checked)
                {
                    clsStandards.WriteLog(this, new Exception("Please check Container Damaged  or Not"), clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 0, true);
                    resp = false;
                }
                else if (rbContainerDamagedY.Checked && string.IsNullOrEmpty(txtContainerNoDamaged.Text))
                {
                    clsStandards.WriteLog(this, new Exception("Please Enter Container Damaged"), clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 0, true);
                    txtContainerNo.Focus();
                    resp = false;
                }
                else if (!rbSealedN.Checked && !rbSealedY.Checked)
                {
                    clsStandards.WriteLog(this, new Exception("Please check Container Sealed  or Not"), clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 0, true);
                    resp = false;
                }
                else if (rbSealedN.Checked && string.IsNullOrEmpty(txtContainerSealed.Text))
                {
                    clsStandards.WriteLog(this, new Exception("Please Enter Container Sealed"), clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 0, true);
                    txtContainerNo.Focus();
                    resp = false;
                }
                else if (!rbContainerMatchedY.Checked && !rbContainerMatchedN.Checked && !rbContainerMatchedNA.Checked)
                {
                    clsStandards.WriteLog(this, new Exception("Please check Container Matched  or Not"), clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 0, true);
                    resp = false;
                }
                else if (rbContainerMatchedN.Checked && string.IsNullOrEmpty(txtContainerMatched.Text))
                {
                    clsStandards.WriteLog(this, new Exception("Please Enter Container Matched"), clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 0, true);
                    txtContainerNo.Focus();
                    resp = false;
                }
                //else if (!rbVendorDocumentsN.Checked && !rbVendorDocumentsY.Checked)
                //{
                //    clsStandards.WriteLog(this, new Exception("Please check Vendor Document  or Not"), clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 0, true);
                //    resp = false;
                //}
                //else if (rbVendorDocumentsN.Checked && string.IsNullOrEmpty(txtVendorDocument.Text))
                //{
                //    clsStandards.WriteLog(this, new Exception("Please Enter Vendor Document"), clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 0, true);
                //    txtContainerNo.Focus();
                //    resp = false;
                //}

            }
            else
            {
                //if (string.IsNullOrEmpty(txtVendorBatchNo.Text))
                //{
                //    clsStandards.WriteLog(this, new Exception("Please Enter VendorBatchNo"), clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 0, true);
                //    txtVendorBatchNo.Focus();
                //    resp = false;
                //}
                if (string.IsNullOrEmpty(txtMfgDate.Text))
                {
                    clsStandards.WriteLog(this, new Exception("Please Enter Manufacturing Date"), clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 0, true);
                    txtMfgDate.Focus();
                    resp = false;
                }
                else if (string.IsNullOrEmpty(txtExpDate.Text))
                {
                    clsStandards.WriteLog(this, new Exception("Please Enter Expiry Date"), clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 0, true);
                    txtExpDate.Focus();
                    resp = false;
                }
                else if (string.IsNullOrEmpty(txtBalanceLife.Text))
                {
                    clsStandards.WriteLog(this, new Exception("Please Enter Balance Life"), clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 0, true);
                    txtBalanceLife.Focus();
                    resp = false;
                }
                else if (string.IsNullOrEmpty(txtQtyAccepted.Text))
                {
                    clsStandards.WriteLog(this, new Exception("Please Enter Quantity Accpted"), clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 0, true);
                    txtQtyAccepted.Focus();
                    resp = false;
                }
                //else if (string.IsNullOrEmpty(txtNoOfContainers.Text))
                //{
                //    clsStandards.WriteLog(this, new Exception("Please Enter Invoice Date"), clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 0, true);
                //    txtNoOfContainers.Focus();
                //    resp = false;
                //}
                else if (string.IsNullOrEmpty(txtTotalContainers.Text))
                {
                    clsStandards.WriteLog(this, new Exception("Please Enter total Containers"), clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 0, true);
                    txtTotalContainers.Focus();
                    resp = false;
                }
                //else if (string.IsNullOrEmpty(txtLicNo.Text))
                //{
                //    clsStandards.WriteLog(this, new Exception("Please Enter LIC NO"), clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 0, true);
                //    txtLicNo.Focus();
                //    resp = false;
                //}
                //else if (string.IsNullOrEmpty(txtBalanceCode.Text))
                //{
                //    clsStandards.WriteLog(this, new Exception("Please Scan Balance Weighing code"), clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 0, true);
                //    txtBalanceCode.Focus();
                //    resp = false;
                //}
                else if (string.IsNullOrEmpty(txtGmCode.Text))
                {
                    clsStandards.WriteLog(this, new Exception("Please Enter GM Code"), clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 0, true);
                    txtGmCode.Focus();
                    resp = false;
                }
                //else if (string.IsNullOrEmpty(txtPackQty.Text))
                //{
                //    clsStandards.WriteLog(this, new Exception("Please Enter Pack Qty"), clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 0, true);
                //    txtPackQty.Focus();
                //    resp = false;
                //}

                if (txtMfgDate.Text.Trim() != "")
                {
                    if (DateTime.ParseExact(txtMfgDate.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture) > DateTime.UtcNow.Date)
                    {
                        clsStandards.WriteLog(this, new Exception("MFG date Should not Greater Than Current date"), clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 0, true);
                        resp = false;
                    }
                }


                if (txtExpDate.Text.Trim() != "")
                {
                    if (DateTime.ParseExact(txtExpDate.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture) < DateTime.UtcNow.Date)
                    {
                        clsStandards.WriteLog(this, new Exception("EXP date Should not less Than Current date"), clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 0, true);
                        resp = false;
                    }
                }

            }
        }
        catch (Exception ex)
        {
            clsStandards.WriteLog(this, new Exception(ex.Message.ToString()  ), clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 0, true);
        }
        return resp;
    }

    private DataTable GetPurchaseOrderData(string strType, string purchase, string plant, string lineno)
    {
        DataTable dt = new DataTable();
        objBL = new BL_PurchaseOrder();
        dt = objBL.BL_GetPurchaseOrderData(strType, purchase, plant, lineno);
        return dt;
    }

    private void CheckSave_details()
    {
        if (!string.IsNullOrEmpty(Session["Rec_Saved"].ToString()) && !string.IsNullOrEmpty(Session["Gen_Saved"].ToString()))
        {
            if (grPurchaseOrder.Rows.Count > 0)
            {
                for (int i = 0; i < grPurchaseOrder.Rows.Count; i++)
                {
                    CheckBox cb = (CheckBox)grPurchaseOrder.Rows[i].FindControl("chkSelect");
                    if (cb.Checked == true)
                    {
                        cb.Enabled = false;
                        GridViewRow gr = (GridViewRow)grPurchaseOrder.Rows[i];
                        gr.BackColor = System.Drawing.Color.Yellow;
                    }
                }
            }
        }
    }

    protected void chkSelect_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox chk = (CheckBox)sender;
        GridViewRow gv = (GridViewRow)chk.NamingContainer;
        int rownumber = gv.RowIndex;
        if (chk.Checked)
        {
            int i;
            for (i = 0; i <= grPurchaseOrder.Rows.Count - 1; i++)
            {
                if (i != rownumber)
                {
                    CheckBox chkcheckbox = ((CheckBox)(grPurchaseOrder.Rows[i].FindControl("chkSelect")));
                    chkcheckbox.Checked = false;
                }
            }

            btnPrint_Click(sender, e);
        }
    }

    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Verifies that the control is rendered */
    }

    private void ClearGeneralDetails()
    {
        rbCertificateAvaliableY.Checked = false;
        rbCertificateAvaliableN.Checked = false;
        rbManfApprovedY.Checked = false;
        rbManfApprovedN.Checked = false;
        rbSupplierApprovedY.Checked = false;
        rbSupplierApprovedN.Checked = false;
        txtManfName.Text = string.Empty;
        txtSupplierName.Text = string.Empty;
        txtChallanNo.Text = string.Empty;
        txtInvoiceDate.Text = string.Empty;
        rbVehicleCleanY.Checked = false;
        rbVehicleCleanN.Checked = false;
        rbProtedFromRainY.Checked = false;
        rbProtedFromRainN.Checked = false;
        rbCarryingAroundY.Checked = false;
        rbCarryingAroundN.Checked = false;

        rbInOrderY.Checked = false;
        rbInOrderN.Checked = false;
        rbContainerDamagedY.Checked = false;
        rbContainerDamagedN.Checked = false;
        rbSealedY.Checked = false;
        rbSealedN.Checked = false;
        rbContainerMatchedY.Checked = false;
        rbContainerMatchedN.Checked = false;
        rbVendorDocumentsY.Checked = false;
        rbVendorDocumentsN.Checked = false;
        txtContainerNo.Text = string.Empty;
        txtContainerNoDamaged.Text = string.Empty;
        txtContainerSealed.Text = string.Empty;
        txtContainerMatched.Text = string.Empty;
        txtVendorDocument.Text = string.Empty;


    }

    private void ClearMaterialReceiving()
    {

        lblPurchaseOrderRec.Text = string.Empty;
        lblMaterialCode.Text = string.Empty;
        lblMaterialDesc.Text = string.Empty;
        lblLineNo.Text = string.Empty;
        txtVendorBatchNo.Text = string.Empty;
        txtMfgDate.Text = string.Empty;
        txtExpDate.Text = string.Empty;
        txtBalanceLife.Text = string.Empty;
        lblOrderQty.Text = string.Empty;
        txtQtyAccepted.Text = string.Empty;
        txtNoOfContainers.Text = string.Empty;
        txtTotalContainers.Text = string.Empty;
        txtLicNo.Text = string.Empty;
        txtBalanceCode.Text = string.Empty;
        txtGmCode.Text = string.Empty;
        txtPackQty.Text = string.Empty;
        txtGrossWt.Text = string.Empty;
        txtNetWt.Text = string.Empty;
        txtTareWt.Text = string.Empty;
        lblUOM.Text = string.Empty;
        ViewState["table"] = null;
        ViewState["Weight"] = null;
        Session["REFNO_P"] = "";
        Session["Gen_Saved"] = "";
        Session["Rec_Saved"] = "";
    }

    private bool Chk_Checkbox()
    {
        bool resp = false;
        if (grPurchaseOrder.Rows.Count > 0)
        {
            for (int i = 0; i < grPurchaseOrder.Rows.Count; i++)
            {
                CheckBox cb = (CheckBox)grPurchaseOrder.Rows[i].FindControl("chkSelect");
                if (cb.Checked == true)
                {
                    resp = true;
                }
            }
        }
        return resp;
    }

    private void Insert_ItemMaster(DataTable dtItem)
    {
        objBL = new BL_PurchaseOrder();
        try
        {
            if (dtItem.Rows.Count > 0)
            {
                for (int i = 0; i < dtItem.Rows.Count; i++)
                {
                    objPLItem.strItemCode = dtItem.Rows[i]["ItemCode"].ToString();
                    objPLItem.strItemDesc = dtItem.Rows[i]["ItemDesc"].ToString();
                    objBL.BL_Insert_ItemMaster(objPLItem);
                }
            }
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

    private int GetRadioButtonValue(RadioButton rb)
    {
        try
        {
            if (rb.Checked == true)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.ToString());
        }
        finally
        {
        }
    }

    protected void btnSaveGeneralDetails_Click(object sender, EventArgs e)
    {
        objBL = new BL_PurchaseOrder();
        objPL = new PL_PurchaseOrder();
        try
        {
            if (!FieldsVaidation("SaveGeneralDetails"))
            {
                return;
            }
            else
            {
                objPL.strPurchaseOrder = lblPurchaseOrderNo.Text.Trim();
                objPL.strEwayBill = lblEwayBillNo.Text.Trim();
                DateTime OrderDate = DateTime.ParseExact(lblOrderDate.Text, "yyyy-MM-dd", CultureInfo.InvariantCulture);
                objPL.strOrderDate = OrderDate.ToString("yyyy-MM-dd");
                objPL.strTransportMode = lblModeOfTransport.SelectedItem.ToString();
                objPL.strTruckNo = lblTruckNo.Text.Trim();
                objPL.strTransporter = lblTransporter.Text.Trim();

                objPL.strManfName = txtManfName.Text.Trim();
                objPL.strSupplierName = txtSupplierName.Text.Trim();
                objPL.strGateEntryNo = txtEntryGate.Text.Trim();
                objPL.strDeliveryChallanNo = txtChallan.Text.Trim();
                objPL.strDeliveryChallanDate = txtChallanNo.Text.Trim();
                objPL.strInvoiceDate = txtInvoiceDate.Text.Trim();
                objPL.strInvoiceNo = txtInvoiceNo.Text.Trim();
                objPL.ICertificateAvailable = rbCertificateAvaliableY.Checked == true ? 1 : 0;
                objPL.IManfApproved = rbManfApprovedY.Checked == true ? 1 : 0;
                objPL.ISupplierApproved = rbSupplierApprovedY.Checked == true ? 1 : 0;
                objPL.IVehicleClean = rbVehicleCleanY.Checked == true ? 1 : 0;
                objPL.IProperlyClosed = rbProtedFromRainY.Checked == true ? 1 : 0;
                objPL.ICarryingOdorous = rbCarryingAroundY.Checked == true ? 1 : 0;
                objPL.ISupplierLabel = rbInOrderY.Checked == true ? 1 : 0;
                objPL.strSupplierContainer = txtSupplierName.Text.Trim();
                objPL.IProperlySealed = rbSealedY.Checked == true ? 1 : 0;
                objPL.strProperlySealed = txtContainerSealed.Text.Trim();
                objPL.IContainersDamaged = rbContainerDamagedY.Checked == true ? 1 : 0;
                objPL.strDamagedContainer = txtContainerNoDamaged.Text.Trim();
                if (rbContainerMatchedY.Checked)
                {
                    objPL.IMatchedWithSeal = 1;
                }
                else if (rbContainerMatchedN.Checked)
                {
                    objPL.IMatchedWithSeal = 0;
                }
                else if (rbContainerMatchedNA.Checked)
                {
                    objPL.IMatchedWithSeal = 2;
                }
                objPL.strMatchedWithSeal = txtContainerMatched.Text.Trim();
                DateTime GATEDT = DateTime.ParseExact(txtEntryGateDate.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture);
                objPL.strGateEntryDate = GATEDT.ToString("yyyy-MM-dd");
                //objPL.IVendorDocuments = rbVendorDocumentsY.Checked == true ? 1 : 0;
                objPL.strLineNo = lbllineItemNo_Gen.Text.Trim();
                objPL.strMode = "ADD";
                objPL.strPlantCode = clsStandards.current_Plant().ToString();
                objPL.strUserId = clsStandards.current_Username().ToString();
                objPL.strRefno = Session["REFNO_P"].ToString();
                objPL.strItemCode = lblitemcode_gen.Text.Trim();
                objPL.strCostCenter = txtCostCenter.Text.Trim();
                objPL.strGR_GI_SLIP_NO = txtGRGISLIPNO.Text.Trim();
                objPL.strHeaderText = txtHeaderText.Text.Trim();
                objPL.strGR_RR_NO = txtGRNO.Text.Trim();
                objPL.strGI_AccountNo = txtGLAcctNo.Text.Trim();
                objPL.strHeaderText = txtHeaderText.Text.Trim();
                objPL.strStorageLocation = txtStorageLoc.Text.Trim();
                if (!string.IsNullOrEmpty(txtEwayBillDate.Text.Trim()))
                {
                    DateTime EWAYDT = DateTime.ParseExact(txtEwayBillDate.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture);
                    objPL.strEwayBillDate = EWAYDT.ToString("yyyy-MM-dd");
                }
                else
                {
                    objPL.strEwayBillDate = "";
                }

                var Result = objBL.BL_SavePurchaseOrder_GeneralDetails(objPL);

                if (Result.IErrorId == 1)
                {
                    clsStandards.WriteLog(this, new Exception(Result.strResponse.ToString()), clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 0, true);
                    ClearGeneralDetails();
                    Session["Gen_Saved"] = "Saved";
                    btnNext_Click(sender, e);
                }
                else
                {
                    clsStandards.WriteLog(this, new Exception(Result.strResponse.ToString()), clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 0, true);
                }
            }
        }
        catch (Exception ex)
        {
            clsStandards.WriteLog(this, new Exception(ex.Message), clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 0, true);
        }
        finally
        {
            objBL = null;
            objPL = null;
        }
    }

    protected void btnSaveReceiving_Click(object sender, EventArgs e)
    {
        try
        {
            if (ViewState["Weight"] == null)
            {
                clsStandards.WriteLog(this, new Exception("Data has not added for PackSize , Please Add Values"), clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 0, true);
                return;
            }
            if (string.IsNullOrEmpty(Convert.ToString(Session["Rec_Saved"])))
            {
                SaveReceivingDetails();
                ClearMaterialReceiving();
                Close();
            }
            else
            {
                ClearMaterialReceiving();
                Close();
            }
            clsStandards.WriteLog(this, new Exception("Material Receiving Data Saved Successfully"), clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 0, true);

            if (grPurchaseOrder.Rows.Count > 0)
            {
                for (int i = 0; i < grPurchaseOrder.Rows.Count; i++)
                {
                    CheckBox cb = (CheckBox)grPurchaseOrder.Rows[i].FindControl("chkSelect");
                    if (cb.Checked == true)
                    {
                        cb.Enabled = false;
                        GridViewRow gr = (GridViewRow)grPurchaseOrder.Rows[i];
                        gr.BackColor = System.Drawing.Color.Yellow;
                    }
                }
            }
            MultiView1.SetActiveView(View1);
        }
        catch (Exception ex)
        {

        }
    }

    private void SaveReceivingDetails()
    {
        objBL = new BL_PurchaseOrder();
        objPL = new PL_PurchaseOrder();
        dynamic strResult = "";
        try
        {
            if (!FieldsVaidation("SaveReceiving"))
            {
                return;
            }

            if (ViewState["table"] == null)
            {
                objPL.strPurchaseOrder = lblPurchaseOrderRec.Text.Trim();
                objPL.strMaterialCode = lblMaterialCode.Text.Trim();
                objPL.strMaterialDesc = lblMaterialDesc.Text.Trim();
                objPL.strLineNo = lblLineNo.Text.Trim();
                objPL.strVendorBatch = txtVendorBatchNo.Text.Trim();
                DateTime Mfgdt = DateTime.ParseExact(txtMfgDate.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture);
                objPL.strMfgDate = Mfgdt;
                DateTime ExpDate = DateTime.ParseExact(txtExpDate.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture);
                objPL.strExpDate = ExpDate;
                objPL.strBalanceShelfLife = txtBalanceLife.Text.Trim();
                objPL.dOrderQty = lblOrderQty.Text.Trim() == "" ? 0 : Convert.ToDecimal(lblOrderQty.Text.Trim());
                objPL.dAcceptedQty = txtQtyAccepted.Text.Trim() == "" ? 0 : Convert.ToDecimal(txtQtyAccepted.Text.Trim());
                //objPL.INoOfContainers = Convert.ToInt32(txtNoOfContainers.Text.Trim());
                objPL.ITotalContainers = txtTotalContainers.Text.Trim() == "" ? 0 : Convert.ToInt32(txtTotalContainers.Text.Trim());
                objPL.strWeighingCode = txtBalanceCode.Text.Trim();
                if (txtGrossWt.Text.Trim().Contains(","))
                {
                    objPL.dGrossWt = Convert.ToDecimal(txtGrossWt.Text.Trim().Split(',').GetValue(0).ToString());
                }
                else
                {
                    objPL.dGrossWt = txtGrossWt.Text.Trim() == "" ? 0 : Convert.ToDecimal(txtGrossWt.Text.Trim());
                }
                objPL.dTareWt = txtTareWt.Text.Trim() == "" ? 0 : Convert.ToDecimal(txtTareWt.Text.Trim());
                objPL.dNetWt = txtNetWt.Text.Trim() == "" ? 0 : Convert.ToDecimal(txtNetWt.Text.Trim());
                objPL.strAllGrossWt = txtGrossWt.Text.Trim();
                objPL.strPack_qty = txtPackQty.Text.Trim() == "" ? 0 : Convert.ToDecimal(txtPackQty.Text.Trim());
                objPL.INoOfContainers = txtNoOfContainers.Text.Trim() == "" ? Convert.ToInt32(txtTotalContainers.Text.Trim()) : Convert.ToInt32(txtNoOfContainers.Text.Trim());
                objPL.strUom = lblUOM.Text.Trim();
                objPL.strMode = "ADD";
                objPL.strPlantCode = clsStandards.current_Plant().ToString();
                objPL.strUserId = clsStandards.current_Username().ToString();
                objPL.strLic_No = txtLicNo.Text.Trim();
                objPL.strGm_code = txtGmCode.Text.Trim();
                objPL.strRefno = Session["REFNO_P"].ToString();
                objPL.AdvanceLicense1 = txtAdvLic1.Text.Trim();
                objPL.AdvanceLicense2 = txtAdvLic2.Text.Trim();

                strResult = objBL.BL_SavePurchaseOrder_ReceivingDetails(objPL);
            }
            else
            {
                DataTable dtpurchase = new DataTable();
                dtpurchase = (DataTable)ViewState["table"];
                if (dtpurchase.Rows.Count > 0)
                {
                    for (int i = 0; i < dtpurchase.Rows.Count; i++)
                    {
                        objPL.strPurchaseOrder = lblPurchaseOrderRec.Text.Trim();
                        objPL.strMaterialCode = lblMaterialCode.Text.Trim();
                        objPL.strMaterialDesc = lblMaterialDesc.Text.Trim();
                        objPL.strLineNo = lblLineNo.Text.Trim();
                        objPL.strVendorBatch = txtVendorBatchNo.Text.Trim();
                        DateTime Mfgdt = DateTime.ParseExact(txtMfgDate.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture);
                        objPL.strMfgDate = Mfgdt;
                        DateTime ExpDate = DateTime.ParseExact(txtExpDate.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture);
                        objPL.strExpDate = ExpDate;
                        objPL.strBalanceShelfLife = txtBalanceLife.Text.Trim();
                        objPL.dOrderQty = lblOrderQty.Text.Trim() == "" ? 0 : Convert.ToDecimal(lblOrderQty.Text.Trim());
                        objPL.dAcceptedQty = txtQtyAccepted.Text.Trim() == "" ? 0 : Convert.ToDecimal(txtQtyAccepted.Text.Trim());
                        //objPL.INoOfContainers = Convert.ToInt32(txtNoOfContainers.Text.Trim());
                        objPL.ITotalContainers = txtTotalContainers.Text.Trim() == "" ? 0 : Convert.ToInt32(txtTotalContainers.Text.Trim());
                        objPL.strWeighingCode = txtBalanceCode.Text.Trim();
                        if (rdboth.Checked)
                        {
                            objPL.dGrossWt = dtpurchase.Rows[i]["GrossWt"].ToString() == "" ? 0 : Convert.ToDecimal(dtpurchase.Rows[i]["GrossWt"].ToString());
                            objPL.dTareWt = dtpurchase.Rows[i]["TareWt"].ToString() == "" ? 0 : Convert.ToDecimal(dtpurchase.Rows[i]["TareWt"].ToString());
                            objPL.dNetWt = dtpurchase.Rows[i]["NetWt"].ToString() == "" ? 0 : Convert.ToDecimal(dtpurchase.Rows[i]["NetWt"].ToString());
                            objPL.strAllGrossWt = txtGrossWt.Text.Trim();
                            objPL.strPack_qty = dtpurchase.Rows[i]["PackSize"].ToString() == "" ? 0 : Convert.ToDecimal(dtpurchase.Rows[i]["PackSize"].ToString());
                            objPL.INoOfContainers = dtpurchase.Rows[i]["NoofContainer"].ToString() == "" ? 0 : Convert.ToInt32(dtpurchase.Rows[i]["NoofContainer"].ToString());
                        }
                        else if (rdWeight.Checked)
                        {
                            objPL.dGrossWt = dtpurchase.Rows[i]["GrossWt"].ToString() == "" ? 0 : Convert.ToDecimal(dtpurchase.Rows[i]["GrossWt"].ToString());
                            objPL.dTareWt = dtpurchase.Rows[i]["TareWt"].ToString() == "" ? 0 : Convert.ToDecimal(dtpurchase.Rows[i]["TareWt"].ToString());
                            objPL.dNetWt = dtpurchase.Rows[i]["NetWt"].ToString() == "" ? 0 : Convert.ToDecimal(dtpurchase.Rows[i]["NetWt"].ToString());
                            objPL.strAllGrossWt = txtGrossWt.Text.Trim();
                            objPL.strPack_qty = txtPackQty.Text.Trim() == "" ? 0 : Convert.ToDecimal(txtPackQty.Text.Trim());
                            objPL.INoOfContainers = txtNoOfContainers.Text.Trim() == "" ? Convert.ToInt32(txtTotalContainers.Text.Trim()) : Convert.ToInt32(txtNoOfContainers.Text.Trim());
                        }

                        else if (rdPack.Checked)
                        {
                            objPL.dGrossWt = txtGrossWt.Text.Trim() == "" ? 0 : Convert.ToDecimal(txtGrossWt.Text.Trim());
                            objPL.dTareWt = txtTareWt.Text.Trim() == "" ? 0 : Convert.ToDecimal(txtTareWt.Text.Trim());
                            objPL.dNetWt = txtNetWt.Text.Trim() == "" ? 0 : Convert.ToDecimal(txtNetWt.Text.Trim());
                            objPL.strAllGrossWt = txtGrossWt.Text.Trim();
                            objPL.strPack_qty = dtpurchase.Rows[i]["PackSize"].ToString() == "" ? 0 : Convert.ToDecimal(dtpurchase.Rows[i]["PackSize"].ToString());
                            objPL.INoOfContainers = dtpurchase.Rows[i]["NoofContainer"].ToString() == "" ? 0 : Convert.ToInt32(dtpurchase.Rows[i]["NoofContainer"].ToString());
                        }

                        objPL.strUom = lblUOM.Text.Trim();
                        objPL.strMode = "ADD";
                        objPL.strPlantCode = clsStandards.current_Plant().ToString();

                        objPL.strUserId = clsStandards.current_Username().ToString();
                        objPL.strLic_No = txtLicNo.Text.Trim();
                        objPL.strGm_code = txtGmCode.Text.Trim();
                        objPL.strRefno = Session["REFNO_P"].ToString();
                        objPL.AdvanceLicense1 = txtAdvLic1.Text.Trim();
                        objPL.AdvanceLicense2 = txtAdvLic2.Text.Trim();

                        strResult = objBL.BL_SavePurchaseOrder_ReceivingDetails(objPL);
                    }
                }
            }

            if (strResult.IErrorId == 1)
            {
                clsStandards.WriteLog(this, new Exception("Added Data"), clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 0, true);
                Session["Rec_Saved"] = "Saved";
            }
            else
            {
                clsStandards.WriteLog(this, new Exception(strResult.strResponse.ToString()), clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 0, true);
            }

        }

        catch (Exception ex)
        {
            clsStandards.WriteLog(this, new Exception(ex.Message), clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 0, true);

        }
        finally
        {
            objPL = null;
            objBL = null;
        }
    }

    protected void btnCloseGeneralDetails_Click(object sender, EventArgs e)
    {
        try
        {
            MultiView1.SetActiveView(View1);
        }
        catch (Exception ex)
        {
            clsStandards.WriteLog(this, new Exception(ex.Message), clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 0, true);
            throw new Exception(ex.ToString());
        }
        finally
        {


        }
    }

    protected void btnCloseReceiving_Click(object sender, EventArgs e)
    {
        try
        {
            MultiView1.SetActiveView(View1);
        }
        catch (Exception ex)
        {
            clsStandards.WriteLog(this, new Exception(ex.Message), clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 0, true);

        }
        finally
        {
        }
    }

    private decimal CalculateQty(decimal ddOrderQty)
    {
        decimal ddAcceptedQty = 0;
        ddAcceptedQty = ddOrderQty + (ddOrderQty * 10 / 100);
        return ddAcceptedQty;
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
            throw new Exception(ex.ToString());
        }
        finally
        {

        }
    }

    protected void btnAdd_Wt_Click(object sender, EventArgs e)
    {
        try
        {
            bool blItem = false;
            if (!FieldsVaidation("SaveReceiving"))
            {
                clsStandards.WriteLog(this, new Exception("Please Enter all the details"), clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 0, true);
                return;
            }
            Close();

            if (lblUOM.Text.Trim().ToUpper() == "KG" || lblUOM.Text.Trim().ToUpper() == "G" || lblUOM.Text.Trim().ToUpper() == "L")
            {
                GetItemCodeValidate();
                DataTable dtItemcode = new DataTable();
                dtItemcode = (DataTable)Session["Itemcodetable"];
                if (dtItemcode.Rows.Count > 0)
                {
                    for (int itm = 0; itm < dtItemcode.Rows.Count; itm++)
                    {
                        if (lblMaterialCode.Text.Trim() == dtItemcode.Rows[itm]["Itemcode"].ToString().Trim())
                        {
                            blItem = true;
                            break;
                        }
                    }
                }

                if (blItem == true)
                {
                    rdboth.Visible = false;
                    rdWeight.Visible = false;
                }
                else
                {
                    if (string.IsNullOrEmpty(txtBalanceCode.Text))
                    {
                        clsStandards.WriteLog(this, new Exception("Please Scan Balance Weighing code"), clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 0, true);
                        txtBalanceCode.Focus();
                        return;
                    }
                    rdboth.Visible = true;
                    rdWeight.Visible = true;
                }
            }
            else
            {
                rdboth.Visible = false;
                rdWeight.Visible = false;
            }
        }
        catch (Exception ex)
        {
            clsStandards.WriteLog(this, new Exception(ex.Message.ToString()), clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 0, true);
        }

        ModalPopupExtender1.Show();

        
    }

    protected void rdWeight_CheckedChanged(object sender, EventArgs e)
    {
        ModalPopupExtender1.Show();

        if (rdboth.Checked)
        {
            rdboth.Checked = false;
        }

        if (rdPack.Checked)
        {
            rdPack.Checked = false;
        }
        if (rdWeight.Checked)
        {
            Visibility(true, "Weight");
            Visibility(false, "Pack");
            if (lblUOM.Text.Trim().ToUpper() == "KG" || lblUOM.Text.Trim().ToUpper() == "G" || lblUOM.Text.Trim().ToUpper() == "L")
            {
                rdWeight.Visible = true;
                rdboth.Visible = true;
            }
            else
            {
                rdWeight.Visible = false;
                rdboth.Visible = false;
                btnStartWeigh2.Visible = false;
            }
        }
        else
        {
            Visibility(false, "Weight");
            Visibility(false, "Pack");
        }
    }

    protected void rdPack_CheckedChanged(object sender, EventArgs e)
    {
        ModalPopupExtender1.Show();
        if (rdboth.Checked)
        {
            rdboth.Checked = false;
        }
        if (rdWeight.Checked)
        {
            rdWeight.Checked = false;
        }
        if (rdPack.Checked)
        {
            Visibility(true, "Pack");
            Visibility(false, "Weight");
        }
        else
        {
            Visibility(false, "Pack");
            Visibility(false, "Weight");
        }
    }

    protected void rdboth_CheckedChanged(object sender, EventArgs e)
    {
        ModalPopupExtender1.Show();
        if (rdWeight.Checked)
        {
            rdWeight.Checked = false;
        }
        if (rdPack.Checked)
        {
            rdPack.Checked = false;
        }
        if (rdboth.Checked)
        {
            Visibility(true, "Pack");
            Visibility(true, "Weight");
        }
        else
        {
            Visibility(false, "Pack");
            Visibility(false, "Weight");
        }
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        ModalPopupExtender1.Show();
        Validate_empty();
        ViewState["SaveWeight"] = "Add";
        bool blItem = false;

        GetItemCodeValidate();
        DataTable dtItemcode = new DataTable();
        dtItemcode = (DataTable)Session["Itemcodetable"];
        if (dtItemcode.Rows.Count > 0)
        {
            for (int itm = 0; itm < dtItemcode.Rows.Count; itm++)
            {
                if (lblMaterialCode.Text.Trim() == dtItemcode.Rows[itm]["Itemcode"].ToString().Trim())
                {
                    blItem = true;
                    break;
                }
            }
        }


        if (lblUOM.Text.Trim().ToUpper() == "KG" || lblUOM.Text.Trim().ToUpper() == "G" || lblUOM.Text.Trim().ToUpper() == "L" || lblUOM.Text.Trim().ToUpper() == "ML")
        {
            if (blItem == false)
            {
                if (string.IsNullOrEmpty(txtGrossWt.Text) && string.IsNullOrEmpty(txtgross.Text.Trim()))
                {
                    clsStandards.WriteLog(this, new Exception("Please Enter Gross Weight"), clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 0, true);
                    txtGrossWt.Focus();
                    return;
                }
                else if (string.IsNullOrEmpty(txtNetWt.Text) && string.IsNullOrEmpty(txtnet.Text.Trim()))
                {
                    clsStandards.WriteLog(this, new Exception("Please Enter Net Weight"), clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 0, true);
                    txtNetWt.Focus();
                    return;
                }
                else if (string.IsNullOrEmpty(txtTareWt.Text) && string.IsNullOrEmpty(txttare.Text.Trim()))
                {
                    clsStandards.WriteLog(this, new Exception("Please Enter Tare Weight"), clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 0, true);
                    txtTareWt.Focus();
                    return;
                }
            }
        }
        Binddata();
        Clear();
    }

    private void Validate_empty()
    {
        if (rdWeight.Checked)
        {
            if (string.IsNullOrEmpty(txtgross.Text.Trim()))
            {
                return;
            }
            else if (string.IsNullOrEmpty(txtgross.Text.Trim()))
            {
                return;
            }
            else if (string.IsNullOrEmpty(txtgross.Text.Trim()))
            {
                return;
            }
        }
        else if (rdPack.Checked)
        {
            if (string.IsNullOrEmpty(txtpacksize.Text.Trim()))
            {
                return;
            }

        }
        else if (rdboth.Checked)
        {
            if (string.IsNullOrEmpty(txtgross.Text.Trim()))
            {
                return;
            }
            else if (string.IsNullOrEmpty(txtgross.Text.Trim()))
            {
                return;
            }
            else if (string.IsNullOrEmpty(txtgross.Text.Trim()))
            {
                return;
            }
            else if (string.IsNullOrEmpty(txtpacksize.Text.Trim()))
            {
                return;
            }
        }

    }

    private void Binddata()
    {
        DataTable dt = new DataTable();
        DataRow dr = dt.NewRow();

        if (ViewState["table"] == null)
        {
            if (rdPack.Checked)
            {
                dt.Columns.Add("PackSize", typeof(string));
                dt.Columns.Add("NoofContainer", typeof(string));
                dr["PackSize"] = txtpacksize.Text.Trim();
                dr["NoofContainer"] = txtNoOfContainers.Text.Trim();
            }

            else if (rdWeight.Checked)
            {
                dt.Columns.Add("GrossWt", typeof(string));
                dt.Columns.Add("TareWt", typeof(string));
                dt.Columns.Add("NetWt", typeof(string));

                dr["GrossWt"] = txtgross.Text.Trim();
                dr["TareWt"] = txttare.Text.Trim();
                dr["NetWt"] = txtnet.Text.Trim();

            }
            else if (rdboth.Checked)
            {
                dt.Columns.Add("PackSize", typeof(string));
                dt.Columns.Add("NoofContainer", typeof(string));
                dt.Columns.Add("GrossWt", typeof(string));
                dt.Columns.Add("TareWt", typeof(string));
                dt.Columns.Add("NetWt", typeof(string));

                dr["PackSize"] = txtpacksize.Text.Trim();
                dr["NoofContainer"] = txtNoOfContainers.Text.Trim();
                dr["GrossWt"] = txtgross.Text.Trim();
                dr["TareWt"] = txttare.Text.Trim();
                dr["NetWt"] = txtnet.Text.Trim();
            }
            dt.Rows.Add(dr);
            ViewState["table"] = dt;
            dt.Dispose();
        }
        else
        {
            dt = (DataTable)ViewState["table"];
            if (dt.Rows.Count > 20)
            {
                clsStandards.WriteLog(this, new Exception("  Maximum 20 rows has already been added   "), clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 0, true);
                return;
            }
            else
            {
                dr = dt.NewRow();
                if (rdPack.Checked)
                {
                    dr["PackSize"] = txtpacksize.Text.Trim();
                    dr["NoofContainer"] = txtNoOfContainers.Text.Trim();
                }
                else if (rdWeight.Checked)
                {
                    dr["GrossWt"] = txtgross.Text.Trim();
                    dr["TareWt"] = txttare.Text.Trim();
                    dr["NetWt"] = txtnet.Text.Trim();

                }
                else if (rdboth.Checked)
                {
                    dr["PackSize"] = txtpacksize.Text.Trim();
                    dr["NoofContainer"] = txtNoOfContainers.Text.Trim();
                    dr["GrossWt"] = txtgross.Text.Trim();
                    dr["TareWt"] = txttare.Text.Trim();
                    dr["NetWt"] = txtnet.Text.Trim();
                }

                dt.Rows.Add(dr);
            }
        }

        if (GetToatlCount(dt) == true)
        {
            ViewState["table"] = dt;
            dt.Dispose();
            GridView1.DataSource = dt;
            GridView1.DataBind();
        }
        else
        {
            dt.Rows.Remove(dr);
        }
    }

    private bool GetToatlCount(DataTable dt)
    {
        Decimal DPackSize = 0;
        Decimal DContainer = 0;
        Decimal DTotalQty_Column = 0;
        try
        {
            bool resp = true;
            if (rdPack.Checked || rdboth.Checked)
            {
                if (dt.Rows.Count > 0)
                {
                    for (int j = 0; j < dt.Rows.Count; j++)
                    {
                        DPackSize = DPackSize + Convert.ToDecimal((dt.Rows[j]["PackSize"].ToString()) == "&nbsp;" ? "0" : dt.Rows[j]["PackSize"].ToString());
                        DContainer = DContainer + Convert.ToDecimal((dt.Rows[j]["NoofContainer"].ToString()) == "&nbsp;" ? "0" : dt.Rows[j]["NoofContainer"].ToString());
                        DTotalQty_Column = DTotalQty_Column + Convert.ToDecimal(dt.Rows[j]["PackSize"].ToString()) * Convert.ToDecimal(dt.Rows[j]["NoofContainer"].ToString());
                    }
                }
                else
                {
                    Divmodify.Visible = false;
                }
            }
            if (DTotalQty_Column > Convert.ToDecimal(txtQtyAccepted.Text.Trim()))
            {
                clsStandards.WriteLog(this, new Exception("Quanity Exceed , Please enter proper quantity "), clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 0, true);
                resp = false;
            }
            else
            {
                Divmodify.Visible = true;
                lblTotalpacksize.Text = DPackSize.ToString();
                lbltotalcontainersNo.Text = DContainer.ToString();
                lblTotalQuantity.Text = DTotalQty_Column.ToString();
                resp = true;
            }
            return resp;
        }
        catch (Exception ex)
        {
            clsStandards.WriteLog(this, new Exception(ex.Message), clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 0, true);
            return false;
        }
    }

    private void Visibility(bool bValue, string strType)
    {
        btnAdd.Visible = true;
        if (strType.ToUpper().Contains("WEIGHT"))
        {
            lblgross.Visible = bValue;
            lblnet.Visible = bValue;
            lbltare.Visible = bValue;
            txtgross.Visible = bValue;
            txtnet.Visible = bValue;
            txttare.Visible = bValue;
            btnStartWeigh2.Visible = bValue;
        }
        else
        {
            lblpacksize.Visible = bValue;
            txtpacksize.Visible = bValue;
            lblnoofcontainer.Visible = bValue;
            txtNoOfContainers.Visible = bValue;
        }
    }

    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        ModalPopupExtender1.Show();
        DataTable dtpo = new DataTable();
        dtpo = (DataTable)ViewState["table"];
        GridView1.PageIndex = e.NewPageIndex;
        GridView1.DataSource = dtpo;
        GridView1.DataBind();
    }

    protected void txtQtyAccepted_TextChanged(object sender, EventArgs e)
    {
        decimal ActQtyValue = Convert.ToDecimal(txtQtyAccepted.Text.Trim());
        //decimal ActQty = CalculateQty(Convert.ToDecimal(lblOrderQty.Text.Trim()));
        decimal ActQty = Convert.ToDecimal(lblRemainingQty.Text.Trim());
        if (ActQtyValue > ActQty)
        {
            clsStandards.WriteLog(this, new Exception("Quanity Exceed , Please enter proper quantity "), clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 0, true);
            return;
        }
    }

    private void Clear()
    {
        txtgross.Text = string.Empty;
        txtnet.Text = string.Empty;
        txttare.Text = string.Empty;
        txtpacksize.Text = string.Empty;
        txtNoOfContainers.Text = string.Empty;
    }

    protected void btnclose_Click(object sender, EventArgs e)
    {
        Close();
        ModalPopupExtender1.Hide();
    }

    protected void btnSave_wt_Click(object sender, EventArgs e)
    {
        if (ViewState["SaveWeight"] == null)
        {
            clsStandards.WriteLog(this, new Exception("Please Add data to Save"), clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 0, true);
            ModalPopupExtender1.Show();
            return;
        }
        else
        {
            ViewState["Weight"] = "Save";
            SaveReceivingDetails();
        }
    }

    private void Close()
    {
        Clear();
        ViewState["table"] = null;
        GridView1.DataSource = null;
        GridView1.DataBind();
        rdboth.Checked = false;
        rdPack.Checked = false;
        rdWeight.Checked = false;
        Visibility(false, "Pack");
        Visibility(false, "Weight");
        btnAdd.Visible = false;
        Divmodify.Visible = false;
        btnStartWeigh2.Visible = false;
    }

    protected void btnStartWeigh1_Click(object sender, EventArgs e)
    {
        objBL = new BL_PurchaseOrder();
        try
        {
            Clear();
            string strResult = objBL.BL_CheckWeighExists(txtBalanceCode.Text, clsStandards.current_Plant());
            if (strResult != "")
            {
                txtGrossWt.Text = clsStandards.getWeight(strResult.Split('=').GetValue(0).ToString(), strResult.Split('=').GetValue(1).ToString(), "1");
                //txtGrossWt.Text = "12";
            }
            else
            {
                clsStandards.WriteLog(this, new Exception("Invalid balance scale id scanned"), clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 0, true);
            }
        }
        catch (Exception ex)
        {
            clsStandards.WriteLog(this, ex, clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 0, true);
        }
        finally
        {
            objBL = null;
        }
    }

    protected void btnStartWeigh2_Click(object sender, EventArgs e)
    {
        objBL = new BL_PurchaseOrder();
        try
        {
            ModalPopupExtender1.Show();
            Clear();
            string strResult = objBL.BL_CheckWeighExists(txtBalanceCode.Text, clsStandards.current_Plant());
            if (strResult != "")
            {
                txtgross.Text = clsStandards.getWeight(strResult.Split('=').GetValue(0).ToString(), strResult.Split('=').GetValue(1).ToString(), "1");
                //txtgross.Text = "12";

            }
            else
            {
                clsStandards.WriteLog(this, new Exception("Invalid balance scale id scanned"), clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 0, true);
            }
        }
        catch (Exception ex)
        {
            clsStandards.WriteLog(this, ex, clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 0, true);
        }
        finally
        {
            objBL = null;
        }
    }

    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        DataTable dtDelete = new DataTable();
        try
        {
            int index = Convert.ToInt32(e.RowIndex);
            dtDelete = (DataTable)ViewState["table"];
            dtDelete.Rows[index].Delete();
            dtDelete.AcceptChanges();

            if (dtDelete.Rows.Count == 0)
            {
                GridView1.DataSource = null;
                GridView1.DataBind();
                Divmodify.Visible = false;
            }
            else
            {
                if (GetToatlCount(dtDelete) == true)
                {
                    ViewState["table"] = dtDelete;
                    dtDelete.Dispose();
                    GridView1.DataSource = dtDelete;
                    GridView1.DataBind();
                }
            }
            ModalPopupExtender1.Show();
        }
        catch (Exception ex)
        {
            clsStandards.WriteLog(this, ex, clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 0, true);
        }
        finally
        {
            dtDelete = null;
        }
    }

    protected void grPurchaseOrder_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        DataTable dtpo = new DataTable();
        dtpo = (DataTable)ViewState["POtable"];
        grPurchaseOrder.PageIndex = e.NewPageIndex;
        grPurchaseOrder.DataSource = dtpo;
        grPurchaseOrder.DataBind();
    }

    protected void txtnet_TextChanged(object sender, EventArgs e)
    {
        try
        {
            ModalPopupExtender1.Show();
            decimal grosswt = 0;
            decimal netwt = 0;
            decimal tarewt = 0;

            grosswt = Convert.ToDecimal(txtgross.Text.Trim());
           // clsStandards.WriteLog(this, new Exception(txtgross.Text), clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 0, true);
            netwt = Convert.ToDecimal(txtnet.Text);
          //  clsStandards.WriteLog(this, new Exception(txtnet.Text), clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 0, true);
            tarewt = grosswt - netwt;
            txttare.Text = tarewt.ToString();
         //   clsStandards.WriteLog(this, new Exception(txttare.Text), clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 0, true);

            //ViewState["PGrossWt"] = grosswt.ToString();
            //ViewState["PNetWt"] = netwt.ToString();
            //ViewState["PTareWt"] = tarewt.ToString();
            //ModalPopupExtender1.Show();
        }
        catch (Exception ex)
        {
            clsStandards.WriteLog(this, ex, clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 0, true);
        }
    }
    protected void txtNetWt_TextChanged(object sender, EventArgs e)
    {
        try
        {
            decimal grosswt = 0;
            decimal netwt = 0;
            decimal tarewt = 0;

            grosswt = Convert.ToDecimal(txtGrossWt.Text);
            netwt = Convert.ToDecimal(txtNetWt.Text);
            tarewt = grosswt - netwt;
            txtTareWt.Text = tarewt.ToString();
        }
        catch (Exception ex)
        {

        }
    }
}

