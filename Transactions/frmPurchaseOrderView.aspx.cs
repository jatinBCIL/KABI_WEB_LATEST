using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLayer;
using System.Data;
using System.Globalization;
using PropertyLayer;

public partial class frmPurchaseOrderView : System.Web.UI.Page
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
                Session["Receiving_REFNO"] = "";
                ViewState["Edit"] = "";
                ViewState["Save"] = "";
                //txtGrossWt.Attributes.Add("onkeypress", "button_click(this,'" + this.btnStartWeigh1.ClientID + "'); return DisableSplChars(event);");
                //txtgross.Attributes.Add("onkeypress", "button_click(this,'" + this.btnStartWeigh2.ClientID + "'); return DisableSplChars(event);");
                Close();
            }
            catch (Exception ex)
            {

            }
            return;
        }
    }

    protected void btnGet_Click(object sender, EventArgs e)
    {
        objBL = new BL_PurchaseOrder();
        objPL = new PL_PurchaseOrder();
        DataTable dt = new DataTable();
        objPLItem = new PL_ItemMaster();

        try
        {
            string PO = txtPurchaseOrder.Text.Trim();
            dt = GetPurchaseOrderData("Purchase Order", PO, clsStandards.current_Plant(), null);
            if (dt.Rows.Count > 0)
            {
                grPurchaseOrder.DataSource = dt;
                grPurchaseOrder.DataBind();
            }
            else
            {
                clsStandards.WriteLog(this, new Exception("  No data found  "), clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 0, true);
            }
        }
        catch (Exception ex)
        {
            clsStandards.WriteLog(this, new Exception(ex.Message), clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 0, true);
        }
        finally
        {
            objPL = null;
            dt = null;
            objPLItem = null;
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
                        string strPurchase_RefNo = clsStandards.filter(grPurchaseOrder.Rows[i].Cells[15].Text.Trim());
                        objPL.strPurchaseOrder = clsStandards.filter(grPurchaseOrder.Rows[i].Cells[2].Text.Trim());
                        Session["REFNO_P"] = strPurchase_RefNo;
                        AssignDetails("GENERAL", objPL.strPurchaseOrder, strPurchase_RefNo);
                        MultiView1.SetActiveView(View2);
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
            getdt = null;
        }
    }

    protected void btnNext_Click(object sender, EventArgs e)
    {
        objPL = new PL_PurchaseOrder();
        DataTable getdt = new DataTable();

        try
        {
            if (grPurchaseOrder.Rows.Count > 0)
            {
                for (int i = 0; i < grPurchaseOrder.Rows.Count; i++)
                {
                    CheckBox cb = (CheckBox)grPurchaseOrder.Rows[i].FindControl("chkSelect");
                    if (cb.Checked == true)
                    {
                        string strPurchase_RefNo = clsStandards.filter(grPurchaseOrder.Rows[i].Cells[15].Text.Trim());
                        objPL.strPurchaseOrder = clsStandards.filter(grPurchaseOrder.Rows[i].Cells[2].Text.Trim());
                        AssignDetails("RECEIVING", objPL.strPurchaseOrder, strPurchase_RefNo);
                        MultiView1.SetActiveView(View3);
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
            getdt = null;
        }
    }

    private void AssignDetails(string strType, string strpurchaseNo, string strLineNo)
    {
        DataTable getdt = new DataTable();
        PL_PurchaseOrder objFields = new PL_PurchaseOrder();

        if (strType.ToUpper().Contains("GENERAL"))
        {
            getdt = GetPurchaseOrderData(strType, strpurchaseNo, clsStandards.current_Plant(), strLineNo);
            if (getdt.Rows.Count > 0)
            {
                lblPurchaseOrderNo.Text = clsStandards.filter(getdt.Rows[0]["PurchaseOrderNo"].ToString());
                lblEwayBillNo.Text = clsStandards.filter(getdt.Rows[0]["EwayBillno"].ToString());
                lblModeOfTransport.SelectedValue = clsStandards.filter(getdt.Rows[0]["TransportMode"].ToString());
                lblTransporter.Text = clsStandards.filter(getdt.Rows[0]["TransPorter"].ToString());
                lblTruckNo.Text = clsStandards.filter(getdt.Rows[0]["TruckNo"].ToString());
                lbllineItemNo_Gen.Text = clsStandards.filter(getdt.Rows[0]["LineItemNo"].ToString());
                lblitemcode_gen.Text = clsStandards.filter(getdt.Rows[0]["Itemcode"].ToString());
                txtManfName.Text = clsStandards.filter(getdt.Rows[0]["ManfName"].ToString());
                txtSupplierName.Text = clsStandards.filter(getdt.Rows[0]["SupplierName"].ToString());
                txtEntryGate.Text = clsStandards.filter(getdt.Rows[0]["GateEntryNo"].ToString());
                txtGLAcctNo.Text = clsStandards.filter(getdt.Rows[0]["GI_AccountNo"].ToString());
                txtGRNO.Text = clsStandards.filter(getdt.Rows[0]["GR_RR_NO"].ToString());
                txtHeaderText.Text = clsStandards.filter(getdt.Rows[0]["HeaderText"].ToString());
                txtStorageLoc.Text = clsStandards.filter(getdt.Rows[0]["StorageLocation"].ToString());
                txtCostCenter.Text = clsStandards.filter(getdt.Rows[0]["CostCenter"].ToString());
                txtGRGISLIPNO.Text = clsStandards.filter(getdt.Rows[0]["GR_GI_SLIP_NO"].ToString());

                if (!string.IsNullOrEmpty(getdt.Rows[0]["APurchaseOrderDate"].ToString()))
                {
                    lblOrderDate.Text = clsStandards.ConvertDateTime(clsStandards.filter(getdt.Rows[0]["APurchaseOrderDate"].ToString()), "yyyy-MM-dd", "dd-MM-yyyy");
                }
                else
                {
                    lblOrderDate.Text = "";
                }
                if (!string.IsNullOrEmpty(getdt.Rows[0]["BEwayBillDate"].ToString()))
                {
                    txtEwayBillDate.Text = clsStandards.ConvertDateTime(clsStandards.filter(getdt.Rows[0]["BEwayBillDate"].ToString()), "yyyy-MM-dd", "dd-MM-yyyy");
                }
                else
                {
                    txtEwayBillDate.Text = "";
                }
                if (!string.IsNullOrEmpty(getdt.Rows[0]["AGateEntryDate"].ToString()))
                {
                    txtEntryGateDate.Text = clsStandards.ConvertDateTime(clsStandards.filter(getdt.Rows[0]["AGateEntryDate"].ToString()), "yyyy-MM-dd", "dd-MM-yyyy");
                }
                else
                {
                    txtEntryGateDate.Text = "";
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
            }
            else
            {
            }
        }
        else
        {
            getdt = GetPurchaseOrderData(strType, strpurchaseNo, clsStandards.current_Plant(), strLineNo);
            if (getdt.Rows.Count > 0)
            {
                lblPurchaseOrderRec.Text = clsStandards.filter(getdt.Rows[0]["PurchaseOrderNo"].ToString());
                lblMaterialCode.Text = clsStandards.filter(getdt.Rows[0]["MaterialCode"].ToString());
                lblMaterialDesc.Text = clsStandards.filter(getdt.Rows[0]["MaterialDesc"].ToString());
                lblLineNo.Text = clsStandards.filter(getdt.Rows[0]["LineItemNo"].ToString());
                txtVendorBatchNo.Text = clsStandards.filter(getdt.Rows[0]["VendorBatchNo"].ToString());
                txtBalanceLife.Text = clsStandards.filter(getdt.Rows[0]["BalanceShelfLife"].ToString());
                lblOrderQty.Text = clsStandards.filter(getdt.Rows[0]["OrderQty"].ToString());
                txtQtyAccepted.Text = clsStandards.filter(getdt.Rows[0]["QtyAccepted"].ToString());
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
                if (lblUOM.Text.Trim().ToUpper() == "KG" || lblUOM.Text.Trim().ToUpper() == "G" || lblUOM.Text.Trim().ToUpper() == "L" || lblUOM.Text.Trim().ToUpper() == "ML")
                {
                    trWeight.Visible = true;
                }
                else
                {
                    trWeight.Visible = false;
                }
                ViewState["table"] = getdt;
                GridView1.DataSource = getdt;
                GridView1.DataBind();
            }
            else
            {
                if (grPurchaseOrder.Rows.Count > 0)
                {
                    for (int i = 0; i < grPurchaseOrder.Rows.Count; i++)
                    {
                        CheckBox cb = (CheckBox)grPurchaseOrder.Rows[i].FindControl("chkSelect");
                        if (cb.Checked == true)
                        {
                            lblPurchaseOrderRec.Text = clsStandards.filter(grPurchaseOrder.Rows[i].Cells[2].Text.Trim());
                            lblMaterialCode.Text = clsStandards.filter(grPurchaseOrder.Rows[i].Cells[6].Text.Trim());
                            lblMaterialDesc.Text = clsStandards.filter(grPurchaseOrder.Rows[i].Cells[7].Text.Trim());
                            lblLineNo.Text = clsStandards.filter(grPurchaseOrder.Rows[i].Cells[4].Text.Trim());
                            lblOrderQty.Text = clsStandards.filter(grPurchaseOrder.Rows[i].Cells[9].Text.Trim());
                            lblUOM.Text = clsStandards.filter(grPurchaseOrder.Rows[i].Cells[10].Text.Trim());
                        }
                    }
                }
            }
        }
    }

    private bool FieldsVaidation(string Type)
    {
        bool resp = true;
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

        }
        else
        {
            if (string.IsNullOrEmpty(txtVendorBatchNo.Text))
            {
                clsStandards.WriteLog(this, new Exception("Please Enter VendorBatchNo"), clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 0, true);
                txtVendorBatchNo.Focus();
                resp = false;
            }
            else if (string.IsNullOrEmpty(txtMfgDate.Text))
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

            else if (string.IsNullOrEmpty(txtTotalContainers.Text))
            {
                clsStandards.WriteLog(this, new Exception("Please Enter total Containers"), clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 0, true);
                txtTotalContainers.Focus();
                resp = false;
            }

            else if (string.IsNullOrEmpty(txtGmCode.Text))
            {
                clsStandards.WriteLog(this, new Exception("Please Enter GM Code"), clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 0, true);
                txtGmCode.Focus();
                resp = false;
            }


            if (txtMfgDate.Text.Trim() != "")
            {
                if (DateTime.Parse(txtMfgDate.Text.Trim()) > DateTime.UtcNow.Date)
                {
                    clsStandards.WriteLog(this, new Exception("MFG date Should not Greater Than Current date"), clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 0, true);
                    resp = false;
                }
            }


            if (txtExpDate.Text.Trim() != "")
            {
                if (DateTime.Parse(txtExpDate.Text.Trim())< DateTime.UtcNow.Date)
                {
                    clsStandards.WriteLog(this, new Exception("EXP date Should not less Than Current date"), clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 0, true);
                    resp = false;
                }
            }
 

        }
        return resp;
    }

    private DataTable GetPurchaseOrderData(string strType, string purchase, string plant, string Purchase_RefNo)
    {
        DataTable dt = new DataTable();
        objBL = new BL_PurchaseOrder();
        try
        {

            dt = objBL.BL_GetPurchaseOrderData_CheckBy(strType, purchase, plant, Purchase_RefNo);
            ViewState["POtable"] = dt;
            return dt;
        }
        catch (Exception ex)
        {
            clsStandards.WriteLog(this, new Exception(ex.Message), clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 0, true);
        }
        finally
        {
            dt = null;
            objBL = null;
        }
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

        //Session["REFNO_P"] = "";
        //Session["Gen_Saved"] = "";
        //Session["Rec_Saved"] = "";
        //ViewState["table"] = null;
        //ViewState["POtable"] = null;
        //Session["Receiving_REFNO"] = "";
        //ViewState["Edit"] = "";
        //ViewState["Save"] = "";
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
                //objPL.strOrderDate = lblOrderDate.Text.Trim();
                DateTime OrderDate = DateTime.ParseExact(txtEwayBillDate.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture);
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
                objPL.strMode = "";
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
                DateTime EWAYDT = DateTime.ParseExact(txtEwayBillDate.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture);
                objPL.strEwayBillDate = GATEDT.ToString("yyyy-MM-dd");

                var Result = objBL.BL_UpdatePurchaseOrder_GeneralDetails(objPL);

                if (Result.IErrorId == 1 || Result.IErrorId == -1)
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
            ViewState["Save"] = "Save";
            ViewState["Edit"] = "Edit";
            SaveReceivingDetails();
            ClearMaterialReceiving();

            clsStandards.WriteLog(this, new Exception("Material Receiving Data Updated Successfully"), clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 0, true);

            if (grPurchaseOrder.Rows.Count > 0)
            {
                for (int i = 0; i < grPurchaseOrder.Rows.Count; i++)
                {
                    CheckBox cb = (CheckBox)grPurchaseOrder.Rows[i].FindControl("chkSelect");
                    if (cb.Checked == true)
                    {
                        cb.Enabled = false;
                        cb.Checked = false;
                        GridViewRow gr = (GridViewRow)grPurchaseOrder.Rows[i];
                        gr.BackColor = System.Drawing.Color.Yellow;
                    }
                }
            }
            MultiView1.SetActiveView(View1);
        }
        catch (Exception ex)
        {
            clsStandards.WriteLog(this, new Exception(ex.Message), clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 0, true);

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
            if (ViewState["Edit"] == "Edit" && ViewState["Save"] == "Save")
            {
                if (GridView1.Rows.Count > 0)
                {
                    for (int i = 0; i < GridView1.Rows.Count; i++)
                    {
                        objPL.strPurchaseOrder = lblPurchaseOrderRec.Text.Trim();
                        objPL.strMaterialCode = lblMaterialCode.Text.Trim();
                        objPL.strMaterialDesc = lblMaterialDesc.Text.Trim();
                        objPL.strLineNo = lblLineNo.Text.Trim();
                        objPL.strVendorBatch = txtVendorBatchNo.Text.Trim();
                        DateTime Mfgdt = DateTime.Parse(txtMfgDate.Text.Trim());//, "dd-MM-yyyy", CultureInfo.InvariantCulture);
                        objPL.strMfgDate = Mfgdt;
                        DateTime ExpDate = DateTime.Parse(txtExpDate.Text.Trim());//, "dd-MM-yyyy", CultureInfo.InvariantCulture);
                        objPL.strExpDate = ExpDate;
                        objPL.strBalanceShelfLife = txtBalanceLife.Text.Trim();
                        objPL.dOrderQty = lblOrderQty.Text.Trim() == "" ? 0 : Convert.ToDecimal(lblOrderQty.Text.Trim());
                        objPL.dAcceptedQty = txtQtyAccepted.Text.Trim() == "" ? 0 : Convert.ToDecimal(txtQtyAccepted.Text.Trim());
                        objPL.ITotalContainers = txtTotalContainers.Text.Trim() == "" ? 0 : Convert.ToInt32(txtTotalContainers.Text.Trim());
                        objPL.strWeighingCode = txtBalanceCode.Text.Trim();

                        objPL.dGrossWt = Convert.ToDecimal(clsStandards.filter(GridView1.Rows[i].Cells[3].Text));
                        objPL.dTareWt = Convert.ToDecimal(clsStandards.filter(GridView1.Rows[i].Cells[4].Text));
                        objPL.dNetWt = Convert.ToDecimal(clsStandards.filter(GridView1.Rows[i].Cells[5].Text));
                        objPL.strAllGrossWt = txtGrossWt.Text.Trim();
                        objPL.strPack_qty = Convert.ToDecimal(clsStandards.filter(GridView1.Rows[i].Cells[1].Text));
                        objPL.INoOfContainers = Convert.ToInt32(clsStandards.filter(GridView1.Rows[i].Cells[2].Text));
                        //////reference no (RECEIVING)
                        objPL.strRefno = clsStandards.filter(GridView1.Rows[i].Cells[6].Text);

                        objPL.strUom = lblUOM.Text.Trim();
                        objPL.strMode = "ADD";
                        objPL.strPlantCode = clsStandards.current_Plant().ToString();
                        objPL.strUserId = clsStandards.current_Username().ToString();
                        objPL.strLic_No = txtLicNo.Text.Trim();
                        objPL.strGm_code = txtGmCode.Text.Trim();
                        objPL.strUserId = clsStandards.current_Username();

                       
 

                        strResult = objBL.BL_UpdatePurchaseOrder_ReceivingDetails(objPL);
                    }
                }
            }
            else
            {
                if (ViewState["Edit"] == "Edit")
                {

                    objPL.strPurchaseOrder = lblPurchaseOrderRec.Text.Trim();
                    objPL.strMaterialCode = lblMaterialCode.Text.Trim();
                    objPL.strMaterialDesc = lblMaterialDesc.Text.Trim();
                    objPL.strLineNo = lblLineNo.Text.Trim();
                    objPL.strVendorBatch = txtVendorBatchNo.Text.Trim();
                    DateTime Mfgdt = DateTime.Parse(txtMfgDate.Text.Trim());//, "dd-MM-yyyy", CultureInfo.InvariantCulture);
                    objPL.strMfgDate = Mfgdt;
                    DateTime ExpDate = DateTime.Parse(txtExpDate.Text.Trim());//, "dd-MM-yyyy", CultureInfo.InvariantCulture);
                    objPL.strExpDate = ExpDate;
                    objPL.strBalanceShelfLife = txtBalanceLife.Text.Trim();
                    objPL.dOrderQty = lblOrderQty.Text.Trim() == "" ? 0 : Convert.ToDecimal(lblOrderQty.Text.Trim());
                    objPL.dAcceptedQty = txtQtyAccepted.Text.Trim() == "" ? 0 : Convert.ToDecimal(txtQtyAccepted.Text.Trim());
                    objPL.ITotalContainers = txtTotalContainers.Text.Trim() == "" ? 0 : Convert.ToInt32(txtTotalContainers.Text.Trim());
                    objPL.strWeighingCode = txtBalanceCode.Text.Trim();

                    objPL.strUom = lblUOM.Text.Trim();
                    objPL.strMode = "ADD";
                    objPL.strPlantCode = clsStandards.current_Plant().ToString();
                    objPL.strUserId = clsStandards.current_Username().ToString();
                    objPL.strLic_No = txtLicNo.Text.Trim();
                    objPL.strGm_code = txtGmCode.Text.Trim();
                    //////reference no (RECEIVING)
                    objPL.strRefno = Session["Receiving_REFNO"].ToString();

                    objPL.dGrossWt = Convert.ToDecimal(txtgross.Text.Trim());
                    objPL.dTareWt = Convert.ToDecimal(txttare.Text.Trim());
                    objPL.dNetWt = Convert.ToDecimal(txtnet.Text.Trim());
                    objPL.strAllGrossWt = txtGrossWt.Text.Trim();
                    objPL.strPack_qty = Convert.ToDecimal(txtpacksize.Text.Trim());
                    objPL.INoOfContainers = Convert.ToInt32(txtNoOfContainers.Text.Trim());
                    objPL.strUserId = clsStandards.current_Username();

                    if (txtMfgDate.Text.Trim() != "")
                    {
                        if (DateTime.Parse(txtMfgDate.Text.Trim()) > DateTime.UtcNow.Date)
                        {
                            clsStandards.WriteLog(this, new Exception("MFG date Should not Greater Than Current date"), clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 0, true);
                            return;
                        }
                    }


                    if (txtExpDate.Text.Trim() != "")
                    {
                        if (DateTime.Parse(txtExpDate.Text.Trim()) < DateTime.UtcNow.Date)
                        {
                            clsStandards.WriteLog(this, new Exception("EXP date Should not less Than Current date"), clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 0, true);
                            return;
                        }
                    }

                    strResult = objBL.BL_UpdatePurchaseOrder_ReceivingDetails(objPL);
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
        objBL = new BL_PurchaseOrder();
        try
        {
            if (Session["REFNO_P"] == null)
            {
                clsStandards.WriteLog(this, new Exception("Session expired , Please Login again"), clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 0, true);
                return;
            }
            objBL.BL_Upate_Viewby(lblPurchaseOrderRec.Text.Trim(), Session["REFNO_P"].ToString(), clsStandards.current_Username(), clsStandards.current_Plant());
            if (grPurchaseOrder.Rows.Count > 0)
            {
                for (int i = 0; i < grPurchaseOrder.Rows.Count; i++)
                {
                    CheckBox cb = (CheckBox)grPurchaseOrder.Rows[i].FindControl("chkSelect");
                    if (cb.Checked == true)
                    {
                        cb.Enabled = false;
                        cb.Checked = false;
                        GridViewRow gr = (GridViewRow)grPurchaseOrder.Rows[i];
                        gr.BackColor = System.Drawing.Color.Yellow;
                    }
                }
            }
            MultiView1.SetActiveView(View1);
            //    Session["REFNO_P"] = "";
            //    Session["Gen_Saved"] = "";
            //    Session["Rec_Saved"] = "";
            //    ViewState["table"] = null;
            //    ViewState["POtable"] = null;
            //    Session["Receiving_REFNO"] = "";
            //    ViewState["Edit"] = "" ;
            //    ViewState["Save"] = "";
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

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        PL_PurchaseOrder objFields = new PL_PurchaseOrder();
        ViewState["Edit"] = "Edit";
        SaveReceivingDetails();
        if (grPurchaseOrder.Rows.Count > 0)
        {
            for (int i = 0; i < grPurchaseOrder.Rows.Count; i++)
            {
                CheckBox cb = (CheckBox)grPurchaseOrder.Rows[i].FindControl("chkSelect");
                if (cb.Checked == true)
                {
                    string strPurchase_RefNo = clsStandards.filter(grPurchaseOrder.Rows[i].Cells[15].Text.Trim());
                    objFields.strPurchaseOrder = clsStandards.filter(grPurchaseOrder.Rows[i].Cells[2].Text.Trim());

                    DataTable getdt = new DataTable();

                    getdt = GetPurchaseOrderData("RECEIVING", objFields.strPurchaseOrder, clsStandards.current_Plant(),strPurchase_RefNo);
                    if (getdt.Rows.Count > 0)
                    {
                        GridView1.DataSource = getdt;
                        GridView1.DataBind();
                    }
                }
            }
        }
    }

    private void Binddata()
    {

    }

    private bool GetToatlCount(DataTable dt)
    {
        Decimal DPackSize = 0;
        Decimal DContainer = 0;
        Decimal DTotalQty_Column = 0;

        try
        {
            bool resp = true;

            if (dt.Rows.Count > 0)
            {
                for (int j = 0; j < dt.Rows.Count; j++)
                {
                    DPackSize = DPackSize + Convert.ToDecimal((dt.Rows[j]["PackSize"].ToString()) == "&nbsp;" ? "0" : dt.Rows[j]["PackSize"].ToString());
                    DContainer = DContainer + Convert.ToDecimal((dt.Rows[j]["NoofContainer"].ToString()) == "&nbsp;" ? "0" : dt.Rows[j]["NoofContainer"].ToString());
                    DTotalQty_Column = DTotalQty_Column + Convert.ToDecimal(dt.Rows[j]["PackSize"].ToString()) * Convert.ToDecimal(dt.Rows[j]["NoofContainer"].ToString());
                }

            }
            if (DTotalQty_Column > Convert.ToDecimal(txtQtyAccepted.Text.Trim()))
            {
                clsStandards.WriteLog(this, new Exception("Quanity Exceed , Please enter proper quantity "), clsStandards.GetCurrentPageName(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), 0, true);
                resp = false;
            }
            else
            {
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

    //private void Visibility(bool bValue, string strType)
    //{
    //    btnAdd.Visible = true;
    //    if (strType.ToUpper().Contains("WEIGHT"))
    //    {
    //        lblgross.Visible = bValue;
    //        lblnet.Visible = bValue;
    //        lbltare.Visible = bValue;
    //        txtgross.Visible = bValue;
    //        txtnet.Visible = bValue;
    //        txttare.Visible = bValue;
    //        ///btnStartWeigh2.Visible = bValue;
    //    }
    //    else
    //    {
    //        lblpacksize.Visible = bValue;
    //        txtpacksize.Visible = bValue;
    //        lblnoofcontainer.Visible = bValue;
    //        txtNoOfContainers.Visible = bValue;
    //    }
    //}

    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

        DataTable dtpo = new DataTable();
        dtpo = (DataTable)ViewState["table"];
        GridView1.PageIndex = e.NewPageIndex;
        GridView1.DataSource = dtpo;
        GridView1.DataBind();
    }

    protected void txtQtyAccepted_TextChanged(object sender, EventArgs e)
    {
        decimal ActQtyValue = Convert.ToDecimal(txtQtyAccepted.Text.Trim());
        decimal ActQty = CalculateQty(Convert.ToDecimal(lblOrderQty.Text.Trim()));
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

    }

    protected void btnSave_wt_Click(object sender, EventArgs e)
    {
        SaveReceivingDetails();
    }

    private void Close()
    {
        Clear();
        GridView1.DataSource = null;
        ViewState["table"] = null;
        GridView1.DataBind();

        //Visibility(false, "Pack");
        //Visibility(false, "Weight");
        btnAdd.Visible = false;
        //Divmodify.Visible = false;
        //btnStartWeigh2.Visible = false;
    }

    protected void GridView1_RowEditing1(object sender, GridViewEditEventArgs e)
    {
        ViewState["Edit"] = "Edit";
        // divValue.Visible = true;
        try
        {
            int index = Convert.ToInt32(e.NewEditIndex);

            txtgross.Text = clsStandards.filter(GridView1.Rows[index].Cells[3].ToString());
            txtnet.Text = clsStandards.filter(GridView1.Rows[index].Cells[5].ToString());
            txttare.Text = clsStandards.filter(GridView1.Rows[index].Cells[4].ToString());
            txtpacksize.Text = clsStandards.filter(GridView1.Rows[index].Cells[1].ToString());
            txtNoOfContainers.Text = clsStandards.filter(GridView1.Rows[index].Cells[2].ToString());
            string refno = clsStandards.filter(GridView1.Rows[index].Cells[6].ToString());
            Session["Receiving_REFNO"] = refno;

        }
        catch (Exception ex)
        {

        }
    }

    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {
        ViewState["Edit"] = "Edit";
        Panel9.Visible = true;
        btnAdd.Visible = true;

        try
        {
            GridViewRow row = (GridView1.SelectedRow);

            txtgross.Text = clsStandards.filter(row.Cells[3].Text);
            txtnet.Text = clsStandards.filter(row.Cells[5].Text);
            txttare.Text = clsStandards.filter(row.Cells[4].Text);
            txtpacksize.Text = clsStandards.filter(row.Cells[1].Text);
            txtNoOfContainers.Text = clsStandards.filter(row.Cells[2].Text);
            string refno = clsStandards.filter(row.Cells[6].Text);
            Session["Receiving_REFNO"] = refno;
        }
        catch (Exception ex)
        {

            throw;
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
}