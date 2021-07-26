<%@ Page Title="" Language="C#" MasterPageFile="~/Modules/MasterPage.master" AutoEventWireup="true" CodeFile="frmMaterialPosting.aspx.cs" Inherits="frmMaterialPosting" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="../App_Themes/Styles/Style_Controls.css" rel="stylesheet" type="text/css" />
    <link href="../App_Themes/Styles/Style_Design.css" rel="stylesheet" type="text/css" />
    <link href="../App_Themes/Styles/Style_Grid.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        div#tipDiv
        {
            font-size: 13px;
            line-height: 1.2;
            color: #3333CC;
            background-color: #33CCCC;
            border: 1px solid #667295;
            padding: 4px;
            width: 270px;
            font-family: calibri;
        }
        .style18
        {
            height: 46px;
        }
        .modal-dialog
        {
            -webkit-transform: translate(0,-25%);
            -ms-transform: translate(0,-25%);
            -o-transform: translate(0,-25%);
            transform: translate(0,-25%);
            -webkit-transition: -webkit-transform .3s ease-out;
            -o-transition: -o-transform .3s ease-out;
            transition: -webkit-transform .3s ease-out;
            transition: transform .3s ease-out;
            transition: transform .3s ease-out,-webkit-transform .3s ease-out,-o-transform .3s ease-out;
        }
        .modal.in .modal-dialog
        {
            -webkit-transform: translate(0,0);
            -ms-transform: translate(0,0);
            -o-transform: translate(0,0);
            transform: translate(0,0);
        }
        .modal-open .modal
        {
            overflow-x: hidden;
            overflow-y: auto;
        }
        .modal-dialog
        {
            position: relative;
            width: auto;
            margin: 10px;
        }
        .modal-content
        {
            position: relative;
            background-color: #fff;
            background-clip: padding-box;
            border: 1px solid #999;
            border: 1px solid rgba(0,0,0,.2);
            border-radius: 6px;
            -webkit-box-shadow: 0 3px 9px rgba(0,0,0,.5);
            box-shadow: 0 3px 9px rgba(0,0,0,.5);
            outline: 0;
        }
        .modal-backdrop
        {
            position: fixed;
            top: 0;
            right: 0;
            bottom: 0;
            left: 0;
            z-index: 1040;
            background-color: #000;
        }
        .modal-backdrop.fade
        {
            filter: alpha(opacity=0);
            opacity: 0;
        }
        .modal-backdrop.in
        {
            filter: alpha(opacity=50);
            opacity: .5;
        }
        .modal-header
        {
            padding: 15px;
            border-bottom: 1px solid #e5e5e5;
        }
        .modal-header .close
        {
            margin-top: -2px;
        }
        .modal-title
        {
            margin: 0;
            line-height: 1.42857143;
        }
        .modal-body
        {
            position: relative;
            padding: 15px;
        }
        .modal-footer
        {
            padding: 15px;
            text-align: right;
            border-top: 1px solid #e5e5e5;
        }
        .modal-footer .btn + .btn
        {
            margin-bottom: 0;
            margin-left: 5px;
        }
        .modal-footer .btn-group .btn + .btn
        {
            margin-left: -1px;
        }
        .modal-footer .btn-block + .btn-block
        {
            margin-left: 0;
        }
        .modal-scrollbar-measure
        {
            position: absolute;
            top: -9999px;
            width: 50px;
            height: 50px;
            overflow: scroll;
        }
        @media (min-width:768px)
        {
            .modal-dialog
            {
                width: 600px;
                margin: 30px auto;
            }
            .modal-content
            {
                -webkit-box-shadow: 0 5px 15px rgba(0,0,0,.5);
                box-shadow: 0 5px 15px rgba(0,0,0,.5);
            }
            .modal-sm
            {
                width: 300px;
            }
        }
    </style>
    <script src="../Masters/Javascript/dw_tooltip_c.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table cellpadding="0" cellspacing="0" width="100%">
                <%--<tr>
                    <td style="height: 30px; background-image: url('../App_Themes/Styles/Images/Form_Title.JPG');
                        background-repeat: repeat-x">
                        <asp:Label ID="Label35" runat="server" CssClass="HeadingLabel" Text="&amp;nbsp;User Master"></asp:Label>
                    </td>
                </tr>--%>
                <tr>
                    <td>
                        <asp:MultiView ID="MultiView1" runat="server">
                            <asp:View ID="View1" runat="server">
                                <table cellpadding="0" cellspacing="0" width="100%">
                                    <tr>
                                        <td height="30" style="height: 30px; background-image: url('../App_Themes/Styles/Images/Menu_Out.png');
                                            background-repeat: repeat-x">
                                            <asp:Label ID="Label35" runat="server" CssClass="HeadingLabel" Text="&amp;nbsp;Purchase order Posting"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <table cellpadding="5">
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="lblDocumentNo" runat="server" CssClass="Label" Text="Purchase Order No :"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtDocumentNo" runat="server" CssClass="TextBox" Height="25px" SkinID="NAME"
                                                            Width="220px"></asp:TextBox>
                                                        <asp:Image ID="Image1" runat="server" Height="10px" ImageUrl="../App_Themes/Styles/Images/img_mand.png"
                                                            ToolTip="Mandatory Field!!!" Width="10px" />
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtDocumentNo"
                                                            ErrorMessage="RequiredFieldValidator" ToolTip="Document is Required" ValidationGroup="Get"><img 
                                                                                src="../App_Themes/Styles/Images/err1.png" 
                                                                                title="Document is required!!!" /></asp:RequiredFieldValidator>
                                                    </td>
                                                    <td>
                                                        <asp:Button ID="btnGet" runat="server" CssClass="Button" Text="GET" Width="75px"
                                                            ValidationGroup="Get" onclick="btnGet_Click"   />
                                                    </td>
                                                    <td>
                                                        <asp:Button ID="btnAdd_ValutionType" runat="server" CssClass="Button" Text="Add Valution Type" Width="150px"
                                                            onclick="btnAdd_ValutionType_Click"   />
                                                    </td>
                                                    <%-- <td>
                                                        
                                                            <asp:c ID="TextBox1" runat="server" CssClass="TextBox" Height="25px" SkinID="NAME"
                                                            Width="220px"></asp:TextBox>
                                                            
                                                    </td>--%>
                                                     <td>
                                                        
                                                            <asp:TextBox ID="TxtDocNumber" runat="server" CssClass="TextBox" Height="25px" SkinID="NAME"
                                                            Width="220px"></asp:TextBox>
                                                            
                                                    </td>

                                                    <td>
                                                        &nbsp;
                                                    </td>
                                                    <td>
                                                        &nbsp;
                                                    </td>
                                                    <td>
                                                        &nbsp;
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                        <div style=" width:100vw">
                                            <asp:Panel ID="Panel1" runat="server" Height="480px" ScrollBars="Auto">
                                                <asp:GridView ID="GrGRNDetails" runat="server" AllowPaging="True" AllowSorting="True"
                                                    AutoGenerateColumns="False" CaptionAlign="Left" CellPadding="4" CssClass="GridViewStyle"
                                                    EmptyDataText="There are no items to show in this view."  ShowFooter="True"
                                                    Width="100%" >
                                                    <Columns>
                                                        <asp:TemplateField ControlStyle-Width="15px" HeaderText="Select">
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="chkSelect" runat="server" AutoPostBack="true"  OnCheckedChanged="chkSelect_CheckedChanged" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="Plant" HeaderText="Plant" SortExpression="Plant" />
                                                        <asp:BoundField DataField="PurchaseOrderNo" HeaderText="PurchaseOrderNo" SortExpression="PurchaseOrderNo" />
                                                        <asp:BoundField DataField="PurchaseOrderDate" HeaderText="PurchaseOrderDate" SortExpression="PurchaseOrderDate" />
                                                        <asp:BoundField DataField="LineItemNo" HeaderText="LineItemNo" SortExpression="LineItemNo" />
                                                        <asp:BoundField DataField="ItemCode" HeaderText="ItemCode" SortExpression="ItemCode" />
                                                        <asp:BoundField DataField="ItemType" HeaderText="ItemType" SortExpression="ItemType" />
                                                        <asp:BoundField DataField="OrderQty" HeaderText="OrderQty" SortExpression="OrderQty" />
                                                        <asp:BoundField DataField="EwayBillNo" HeaderText="EwayBillNo" SortExpression="EwayBillNo" />
                                                        <asp:BoundField DataField="Uom" HeaderText="Uom" SortExpression="Uom" />
                                                        <asp:BoundField DataField="EwayBillDate" HeaderText="EwayBillDate" SortExpression="EwayBillDate" />
                                                        <asp:BoundField DataField="Transporter" HeaderText="Transporter" SortExpression="Transporter" />
                                                        <asp:BoundField DataField="GateEntryNo" HeaderText="GateEntryNo" SortExpression="GateEntryNo" />
                                                        <asp:BoundField DataField="QtyAccepted" HeaderText="QtyAccepted" SortExpression="QtyAccepted" />
                                                        <asp:BoundField DataField="MFGDate" HeaderText="MFGDate" SortExpression="MFGDate" />
                                                        <asp:BoundField DataField="EXPDate" HeaderText="EXPDate" SortExpression="EXPDate" />
                                                        <asp:BoundField DataField="TransportMode" HeaderText="TransportMode" SortExpression="TransportMode" />
                                                        <asp:BoundField DataField="TruckNo" HeaderText="TruckNo" SortExpression="TruckNo" />
                                                        <asp:BoundField DataField="NoOfContainersRec" HeaderText="NoOfContainersRec" SortExpression="NoOfContainersRec" />
                                                        <asp:BoundField DataField="TotalContainers" HeaderText="TotalContainers" SortExpression="TotalContainers" />
                                                        <asp:BoundField DataField="WeighingCode" HeaderText="WeighingCode" SortExpression="WeighingCode" />
                                                        <asp:BoundField DataField="GrossWt" HeaderText="GrossWt" SortExpression="GrossWt" />
                                                        <asp:BoundField DataField="TareWt" HeaderText="TareWt" SortExpression="TareWt" />
                                                        <asp:BoundField DataField="NetWt" HeaderText="NetWt" SortExpression="NetWt" />
                                                        <asp:BoundField DataField="PackQty" HeaderText="PackQty" SortExpression="PackQty" />
                                                        <asp:BoundField DataField="VendorName" HeaderText="VendorName" SortExpression="VendorName" />
                                                        <asp:BoundField DataField="VendorBatchNo" HeaderText="VenderBatchNo" SortExpression="VenderBatchNo" />
                                                        <asp:BoundField DataField="GM_Code" HeaderText="GM_Code" SortExpression="GM_Code" />
                                                        <asp:BoundField DataField="ManfName" HeaderText="ManfName" SortExpression="ManfName" />
                                                        <asp:BoundField DataField="ValuationType" HeaderText="ValuationType" SortExpression="ValuationType" />
                                                        <asp:BoundField DataField="ARef" HeaderText="ARef" SortExpression="ARef" />
                                                        <asp:BoundField DataField="GateEntryDate" HeaderText="GateEntryDate" SortExpression="GateEntryDate" />
                                                        <asp:BoundField DataField="MaterialDesc" HeaderText="MaterialDesc" SortExpression="MaterialDesc" />
                                                        <asp:BoundField DataField="CostCenter" HeaderText="CostCenter" SortExpression="CostCenter" />
                                                        <asp:BoundField DataField="StorageLocation" HeaderText="StorageLocation" SortExpression="StorageLocation" />
                                                        <asp:BoundField DataField="GI_AccountNo" HeaderText="GI_AccountNo" SortExpression="GI_AccountNo" />
                                                        <asp:BoundField DataField="GR_RR_NO" HeaderText="GR_RR_NO" SortExpression="GR_RR_NO" />
                                                        <asp:BoundField DataField="GR_GI_SLIP_NO" HeaderText="GR_GI_SLIP_NO" SortExpression="GR_GI_SLIP_NO" />
                                                        <asp:BoundField DataField="HeaderText" HeaderText="HeaderText" SortExpression="HeaderText" />
                                                        <asp:BoundField DataField="InvoiceNo" HeaderText="InvoiceNo" SortExpression="InvoiceNo" />
                                                        <asp:BoundField DataField="AdvanceLicense1" HeaderText="AdvanceLicense1" SortExpression="AdvanceLicense1" />
                                                        <asp:BoundField DataField="AdvanceLicense2" HeaderText="AdvanceLicense2" SortExpression="AdvanceLicense2" />
                                                        <%--<asp:BoundField DataField="ManuFactureName" HeaderText="ManuFactureName" SortExpression="ManuFactureName" />--%>
                                                    </Columns>
                                                    <PagerSettings Position="TopAndBottom" />
                                                    <RowStyle CssClass="GridViewRowStyle" />
                                                    <FooterStyle BackColor="#EAF2F8" />
                                                    <PagerStyle CssClass="GridViewPagerStyle" />
                                                    <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                                                    <HeaderStyle CssClass="GridViewHeaderStyle" />
                                                    <EditRowStyle CssClass="GridViewEditRowStyle" />
                                                    <EmptyDataRowStyle CssClass="EmptyDataRowStyle" />
                                                    <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                                                </asp:GridView>
                                            </asp:Panel>
                                            </div>
                                        </td>
                                    </tr>
                                             
                                    <tr>
                                        <td>
                                            <table cellpadding="5">
                                                <tr>
                                                     <td>
                                                        <asp:Label ID="lblsapuser" runat="server" CssClass="Label" Text="SAP User Id :"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtSapUser" runat="server" CssClass="TextBox" Height="25px" SkinID="NAME"
                                                            Width="220px"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblsappwd" runat="server" CssClass="Label" Text="SAP Password :"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtSapPass" runat="server" CssClass="TextBox" Height="25px" SkinID="NAME" TextMode="Password"
                                                            Width="220px"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <asp:Button ID="Button1" runat="server" CssClass="Button" Text=" POST"  OnClientClick="return getConfirmation(this, 'Please confirm','Are you sure you want to delete?');"
                                                            Width="150px" onclick="btnPost_Click"/>
                                                    </td>
                                                    <td>
                                                        <asp:Button ID="btnPost" runat="server" CssClass="Button" Text=" POST ADV"  OnClientClick="return getConfirmation(this, 'Please confirm','Are you sure you want to delete?');"
                                                            Width="150px" onclick="btnADVPost_Click"/>
                                                    </td>
                                                    
                                                     <td>
                                                    <asp:Button ID="Button2" runat="server" CssClass="Button" Text=" POST STO "
                                                            Width="150px" ValidationGroup="Get" OnClick="btnSTOPost_Click" />
                                                   </td>
                                                   <td>
                                                     <asp:Button ID="btnClose" runat="server" CssClass="Button" Text="CLOSE" 
                                                            Width="75px" onclick="btnClose_Click" />

                                                    </td> 
                                                    <td>
                                                        &nbsp;
                                                    </td>
                                                    <td>
                                                        &nbsp;
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </asp:View>
                        </asp:MultiView>
                    </td>
                </tr>
            </table>
            <cc1:ModalPopupExtender ID="ModalPopupExtender2" runat="server" PopupControlID="Panel3"
        TargetControlID="btnAdd_ValutionType" CancelControlID="BtnCloseAdd" BackgroundCssClass="modalBackground" >
        </cc1:ModalPopupExtender>
             <asp:Panel ID="Panel3" runat="server"  style="width:auto; height:auto; display: none"
              align="center" Width="500px">
            
             <div id="exampleModal" tabindex="-1" role="dialog" runat="server" style="width: 100%;
                 height: 100%">
                 <div  class ="modal-dialog">
                     <div class="modal-content">
                         <div class="modal-header">
                             <h4 class="modal-title" id="exampleModalLabel">
                                 Add Valution type</h4>
                         
                             </button>
                         </div>
                         <div class="modal-body">
                             <asp:Label ID="Label1" runat="server" CssClass="Label" Text="Valuation Type :"></asp:Label>
                             <asp:TextBox ID="txtValuationType" runat="server" CssClass="TextBox" Height="25px" SkinID="NAME"
                                  Width="220px"></asp:TextBox>
                         </div>
                         <div class="modal-footer">
                             <asp:Button ID="BtnSaveAdd" runat="server" CssClass="Button" Text="Save" Width="150px"
                                 OnClick="BtnSaveAdd_Click" />
                             <asp:Button ID="BtnCloseAdd" runat="server" CssClass="Button" Text="Close" Width="150px"
                                 OnClick="BtnCloseAdd_Click" />
                         </div>
                     </div>
                 </div>
             </div>
                
             </asp:Panel>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnGet" />
        </Triggers>
    </asp:UpdatePanel>
  
</asp:Content>

