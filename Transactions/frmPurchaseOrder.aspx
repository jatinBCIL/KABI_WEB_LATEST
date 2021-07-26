<%@ Page Title="" Language="C#" MasterPageFile="~/Modules/MasterPage.master" AutoEventWireup="true"
    CodeFile="frmPurchaseOrder.aspx.cs" Inherits="frmPurchaseOrder" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="../App_Themes/Styles/Style_Controls.css" rel="stylesheet" type="text/css" />
    <link href="../App_Themes/Styles/Style_Design.css" rel="stylesheet" type="text/css" />
    <link href="../App_Themes/Styles/Style_Grid.css" rel="stylesheet" type="text/css" />
    <script src="../App_Themes/Styles/jquery.min.js" type="text/javascript"></script>
    <script type="text/javascript">

        function isNumber(evt) {
            evt = (evt) ? evt : window.event;
            var charCode = (evt.which) ? evt.which : evt.keyCode;
            if (charCode > 31 && (charCode < 48 || charCode > 57)) {
                return false;
            }
            return true;
        }

        function numericOnly(elementRef) {
            var keyCodeEntered = (event.which) ? event.which : (window.event.keyCode) ? window.event.keyCode : -1;
            if ((keyCodeEntered >= 48) && (keyCodeEntered <= 57)) {
                return true;
            }
            else if (keyCodeEntered == 46) {
                if ((elementRef.value) && (elementRef.value.indexOf('.') >= 0))
                    return false;
                else
                    return true;
            }
            return false;
        }
    </script>
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
        .style15
        {
            width: 12px;
        }
        .style18
        {
            height: 46px;
        }
        .style20
        {
            width: 133px;
            height: 46px;
        }
        .style21
        {
            width: 8px;
            height: 46px;
        }
        .style22
        {
            width: 32px;
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
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table cellpadding="0" cellspacing="0" width="100%">
                
                <tr>
                    <td>
                        <asp:MultiView ID="MultiView1" runat="server">
                            <asp:View ID="View1" runat="server">
                                <table cellpadding="0" cellspacing="0" width="100%">
                                    <tr>
                                        <td height="30" style="height: 30px; background-image: url('../App_Themes/Styles/Images/Menu_Out.png');
                                            background-repeat: repeat-x">
                                            <asp:Label ID="Label35" runat="server" CssClass="HeadingLabel" Text="&amp;nbsp;Purchase Order"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <table cellpadding="5">
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="lblDocumentNo" runat="server" CssClass="Label" Text="Purchase order No :"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtPurchaseOrder" runat="server" CssClass="TextBox" Height="25px"
                                                            SkinID="NAME" Width="220px"></asp:TextBox>
                                                        <asp:Image ID="Image1" runat="server" Height="10px" ImageUrl="../App_Themes/Styles/Images/img_mand.png"
                                                            ToolTip="Mandatory Field!!!" Width="10px" />
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtPurchaseOrder"
                                                            ErrorMessage="RequiredFieldValidator" ToolTip="Purchase order is Required" ValidationGroup="Get"><img 
                                                                                src="../App_Themes/Styles/Images/err1.png" 
                                                                                title="Purchase order is required!!!" /></asp:RequiredFieldValidator>
                                                    </td>
                                                    <td>
                                                        <asp:Button ID="btnGet" runat="server" CssClass="Button" Text=" GET DATA FROM SAP "
                                                            Width="150px" ValidationGroup="Get" OnClick="btnGet_Click" />
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
                                                    <td>
                                                        &nbsp;
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="height: 30px;">
                                            <asp:Panel ID="Panel1" runat="server" Height="400px" ScrollBars="Auto">
                                                <asp:GridView ID="grPurchaseOrder" runat="server" AllowPaging="True" AllowSorting="True"
                                                    AutoGenerateColumns="False" CaptionAlign="Left" CellPadding="4" CssClass="GridViewStyle"
                                                    EmptyDataText="There are no items to show in this view." PageSize="20" ShowFooter="True"
                                                    Width="100%" onpageindexchanging="grPurchaseOrder_PageIndexChanging" >
                                                    <Columns>
                                                        <asp:TemplateField ControlStyle-Width="15px" HeaderText="Select">
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="chkSelect" runat="server" AutoPostBack="true" OnCheckedChanged="chkSelect_CheckedChanged" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="Plant" HeaderText="Plant" SortExpression="Plant" />
                                                        <asp:BoundField DataField="PurchaseOrderNo" HeaderText="Purchase Order No" SortExpression="PurchaseOrderNo" />
                                                        <asp:BoundField DataField="PurchaseOrderDate" HeaderText="Purchase Order Date" SortExpression="PurchaseOrderDate" />
                                                        <asp:BoundField DataField="LineItemNo" HeaderText="Line Item No" SortExpression="LineItemNo" />
                                                        <asp:BoundField DataField="VendorName" HeaderText="Vendor Name" SortExpression="VendorName" />
                                                        <asp:BoundField DataField="ItemCode" HeaderText="Item Code" SortExpression="ItemCode" />
                                                        <asp:BoundField DataField="ItemDesc" HeaderText="Item Description" SortExpression="ItemDesc" />
                                                        <asp:BoundField DataField="ItemType" HeaderText="Item Type" SortExpression="ItemType" />
                                                        <asp:BoundField DataField="OrderQty" HeaderText="Order Quantity" SortExpression="OrderQty" />
                                                        <asp:BoundField DataField="Uom" HeaderText="Uom" SortExpression="Uom" />
                                                        <asp:BoundField DataField="EwayBillNo" HeaderText="Eway Bill No" SortExpression="EwayBillNo" />
                                                        <asp:BoundField DataField="EwayBillDate" HeaderText="Eway Bill Date" SortExpression="EwayBillDate" />
                                                        <asp:BoundField DataField="TransPorter" HeaderText="TransPorter" SortExpression="TransPorter" />
                                                        <asp:BoundField DataField="TransPortMode" HeaderText="Transport Mode" SortExpression="TransPortMode" />
                                                        <asp:BoundField DataField="TruckNo" HeaderText="Truck No" SortExpression="TruckNo" />
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
                                        </td>
                                    </tr>
                                    <tr>
                                        <td runat="server" id="tblbtn">
                                            <table cellpadding="5">
                                                <tr>
                                                  <td>
                                                        <asp:Button ID="btnClose" runat="server" CssClass="Button" Text="CLOSE" Width="100px"
                                                            PostBackUrl="~/Modules/frmMain.aspx" OnClick="btnClose_Click" />
                                                    </td>
                                                    <td>
                                                        <asp:Button ID="btnPrint" runat="server" CssClass="Button" Text="ENTER GENERAL DETAILS"
                                                            Width="200px" OnClick="btnPrint_Click" Visible="false" />
                                                    </td>
                                                    <td>
                                                        <asp:Button ID="btnNext" runat="server" CssClass="Button" Text="NEXT" Width="75px"
                                                            OnClick="btnNext_Click" Visible="false" />
                                                    </td>
                                                  
                                                    <td>
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
                                                    <td>
                                                        &nbsp;
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </asp:View>
                            <asp:View ID="View2" runat="server">
                                <table cellpadding="0" cellspacing="0" width="100%">
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label27" runat="server" CssClass="Label" Text="General Details :  "
                                                Font-Bold="true"></asp:Label>
                                                <asp:Label ID="Label65" runat="server" CssClass="Label" Text="(IF  FIELD  VALUE NOT  REQUIRED THEN KEEP  IT  BLANK) "
                                                BackColor="Yellow" Width="600PX" ForeColor="Red" Font-Bold="true" ></asp:Label>
                                            <asp:Panel ID="Panel2" runat="server" BorderStyle="Solid" BorderColor="Black" BorderWidth="1px">
                                                <table cellpadding="5">
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label3" runat="server" CssClass="Label" Text="Purchase order No :"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblPurchaseOrderNo" runat="server" CssClass="Label" Text=""></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="Label2" runat="server" CssClass="Label" Text="EWay Bill No./Date :"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <%--<asp:TextBox ID="lblEwayBillNo" runat="server" CssClass="Label" Text=""></asp:TextBox>--%>
                                                            <asp:TextBox ID="lblEwayBillNo" runat="server" Width="105px" CssClass="TextBox" MaxLength="12"></asp:TextBox>
                                                            <asp:TextBox ID="txtEwayBillDate" runat="server" Width="90px" CssClass="TextBox"
                                                                Style="margin-left: 5px"></asp:TextBox>
                                                          
                                                            <asp:CalendarExtender ID="CalendarExtender5" runat="server" Enabled="True" TargetControlID="txtEwayBillDate"
                                                                Format="dd-MM-yyyy">
                                                            </asp:CalendarExtender>
                                                             <asp:RegularExpressionValidator ID="RegularExpressionValidator6" runat="server" ControlToValidate="txtEwayBillDate"
                                                             ValidationExpression="^(0[1-9]|[12][0-9]|3[01])[-](0[1-9]|1[012])[-](19|20)\d\d$" 
                                                              Display="Dynamic" SetFocusOnError="true" ErrorMessage="Date Format Invalid" ></asp:RegularExpressionValidator>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="Label6" runat="server" CssClass="Label" Text="Mode of transport:"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList ID="lblModeOfTransport" runat="server" CssClass="Combobox" 
                                                                Width="200px">
                                                                <asp:ListItem Value="Vehicle">Vehicle</asp:ListItem>
                                                                <asp:ListItem Value="Hand">Hand</asp:ListItem>
                                                                <asp:ListItem Value="Road">Road</asp:ListItem>
                                                                <asp:ListItem Value="Others">Others</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="Label39" runat="server" CssClass="Label" Text="Line Item No:"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lbllineItemNo_Gen" runat="server" CssClass="Label" Text=""></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label7" runat="server" CssClass="Label" Text="Order Date :"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblOrderDate" runat="server" CssClass="Label" Text="" Width="200px"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="Label9" runat="server" CssClass="Label" Text="Transporter. :"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="lblTransporter" runat="server" CssClass="Label" Text="" Width="200px"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="Label11" runat="server" CssClass="Label" Text="Truck No:"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="lblTruckNo" runat="server" CssClass="Label" Text="" Width="200px"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="Label42" runat="server" CssClass="Label" Text="Material code:"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblitemcode_gen" runat="server" CssClass="Label" Text="" Width="200px"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </asp:Panel>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label28" runat="server" CssClass="Label" Text="Verification of documents :"
                                                Font-Bold="true"></asp:Label>
                                            <asp:Panel ID="Panel3" runat="server" BorderStyle="Solid" BorderColor="Black" BorderWidth="1px">
                                                <table cellpadding="5">
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label8" runat="server" CssClass="Label" Text="Certificate of Analysis is available :"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:RadioButton ID="rbCertificateAvaliableY" runat="server" GroupName="1" Text="Yes" /><asp:RadioButton
                                                                ID="rbCertificateAvaliableN" runat="server" GroupName="1" Text="No" Visible="false" />
                                                            <asp:Image ID="Image2" runat="server" Height="10px" ImageUrl="../App_Themes/Styles/Images/img_mand.png"
                                                                ToolTip="Mandatory Field!!!" Width="10px"  />
                                                        </td>
                                                        <td>
                                                        </td>
                                                        <td>
                                                        </td>
                                                        <td>
                                                        </td>
                                                        <td>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label30" runat="server" CssClass="Label" Text="Check whether manfacturer is approved :"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:RadioButton ID="rbManfApprovedY" runat="server" GroupName="2" Text="Yes" /><asp:RadioButton
                                                                ID="rbManfApprovedN" runat="server" GroupName="2" Text="No" Visible="false"  />
                                                            <asp:Image ID="Image3" runat="server" Height="10px" ImageUrl="../App_Themes/Styles/Images/img_mand.png"
                                                                ToolTip="Mandatory Field!!!" Width="10px"   />
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="Label32" runat="server" CssClass="Label" Text="Name of manfacturer:"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtManfName" runat="server" Width="200px" CssClass="TextBox"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" ControlToValidate="txtManfName"
                                                                ErrorMessage="RequiredFieldValidator" ToolTip="ManfName is required" ValidationGroup="DataSave"><img 
                                                                src="../App_Themes/Styles/Images/err1.png" 
                                                                title="ManfName is required!!!" />
                                                            </asp:RequiredFieldValidator>
                                                        </td>
                                                        <td>
                                                        </td>
                                                        <td>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label16" runat="server" CssClass="Label" Text="Check whether supplier is approved :"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:RadioButton ID="rbSupplierApprovedY" runat="server" GroupName="3" Text="Yes" /><asp:RadioButton
                                                                ID="rbSupplierApprovedN" runat="server" GroupName="3" Text="No"  Visible="false" />
                                                            <asp:Image ID="Image4" runat="server" Height="10px" ImageUrl="../App_Themes/Styles/Images/img_mand.png"
                                                                ToolTip="Mandatory Field!!!" Width="10px"   />
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="Label18" runat="server" CssClass="Label" Text="Name of supplier. :"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtSupplierName" runat="server" Width="200px" CssClass="TextBox"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator16" runat="server" ControlToValidate="txtSupplierName"
                                                                ErrorMessage="RequiredFieldValidator" ToolTip="SupplierName is required" ValidationGroup="DataSave"><img 
                                                                src="../App_Themes/Styles/Images/err1.png" 
                                                                title="SupplierName is required!!!" />
                                                            </asp:RequiredFieldValidator>
                                                        </td>
                                                        <td>
                                                        </td>
                                                        <td>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label22" runat="server" CssClass="Label" Text="Gate Entry / Date:"></asp:Label>
                                                        </td>
                                                        <td>
                                                           
                                                            <asp:TextBox ID="txtEntryGate" runat="server" Width="90px" CssClass="TextBox"></asp:TextBox>
                                                            <asp:TextBox ID="txtEntryGateDate" runat="server" Width="105px" CssClass="TextBox"
                                                                Style="margin-left: 5px"></asp:TextBox>
                                                            <asp:Image ID="Image5" runat="server" Height="10px" ImageUrl="../App_Themes/Styles/Images/img_mand.png"
                                                                ToolTip="Mandatory Field!!!" Width="10px" />
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator17" runat="server" ControlToValidate="txtEntryGate"
                                                                ErrorMessage="RequiredFieldValidator" ToolTip="EntryGate is required" ValidationGroup="DataSave"><img 
                                                                src="../App_Themes/Styles/Images/err1.png" 
                                                                title="EntryGate is required!!!" />
                                                            </asp:RequiredFieldValidator>
                                                            <asp:CalendarExtender ID="CalendarExtender6" runat="server" Enabled="True" TargetControlID="txtEntryGateDate"
                                                                Format="dd-MM-yyyy">
                                                            </asp:CalendarExtender>
                                                             <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtEntryGateDate"
                                                             ValidationExpression="^(0[1-9]|[12][0-9]|3[01])[-](0[1-9]|1[012])[-](19|20)\d\d$" 
                                                              Display="Dynamic" SetFocusOnError="true" ErrorMessage="Date Format Invalid" ></asp:RegularExpressionValidator>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="Label24" runat="server" CssClass="Label" Text="Delivery challan no. /Date :"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtChallan" runat="server" Width="90px" CssClass="TextBox"></asp:TextBox>
                                                            <asp:TextBox ID="txtChallanNo" runat="server" Width="105px" CssClass="TextBox" Style="margin-left: 5px"></asp:TextBox>
                                                            <asp:CalendarExtender ID="CalendarExtender4" runat="server" Enabled="True" TargetControlID="txtChallanNo"
                                                                Format="dd-MM-yyyy">
                                                            </asp:CalendarExtender>
                                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtChallanNo"
                                                             ValidationExpression="^(0[1-9]|[12][0-9]|3[01])[-](0[1-9]|1[012])[-](19|20)\d\d$" 
                                                              Display="Dynamic" SetFocusOnError="true" ErrorMessage="Date Format Invalid" ></asp:RegularExpressionValidator>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="Label26" runat="server" CssClass="Label" Text="Invoice No./ Date:"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtInvoiceNo" runat="server" Width="90px" CssClass="TextBox"></asp:TextBox>
                                                            <asp:TextBox ID="txtInvoiceDate" runat="server" Width="105px" CssClass="TextBox"
                                                                Style="margin-left: 5px"></asp:TextBox>
                                                            <asp:Image ID="Image7" runat="server" Height="10px" ImageUrl="../App_Themes/Styles/Images/img_mand.png"
                                                                ToolTip="Mandatory Field!!!" Width="10px" />
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator19" runat="server" ControlToValidate="txtInvoiceDate"
                                                                ErrorMessage="RequiredFieldValidator" ToolTip="InvoiceDate is required" ValidationGroup="DataSave"><img 
                                                                src="../App_Themes/Styles/Images/err1.png" 
                                                                title="InvoiceDate is required!!!" />
                                                            </asp:RequiredFieldValidator>
                                                            <asp:CalendarExtender ID="CalendarExtender3" runat="server" Enabled="True" TargetControlID="txtInvoiceDate"
                                                                Format="dd-MM-yyyy">
                                                            </asp:CalendarExtender>
                                                             <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="txtInvoiceDate"
                                                             ValidationExpression="^(0[1-9]|[12][0-9]|3[01])[-](0[1-9]|1[012])[-](19|20)\d\d$" 
                                                              Display="Dynamic" SetFocusOnError="true" ErrorMessage="Date Format Invalid" ></asp:RegularExpressionValidator>

                                                        </td>
                                                    </tr>
                                                     <tr>
                                                        <td>
                                                            <asp:Label ID="Label59" runat="server" CssClass="Label" Text="GL Account Number"></asp:Label>
                                                        </td>
                                                        <td>
                                                           
                                                            <asp:TextBox ID="txtGLAcctNo" runat="server" Width="220px" CssClass="TextBox"></asp:TextBox>
                                                            
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="Label60" runat="server" CssClass="Label" Text="GR NO :"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtGRNO" runat="server" Width="220px" CssClass="TextBox" MaxLength="10"></asp:TextBox>
                                                            <asp:Image ID="Image20" runat="server" Height="10px" ImageUrl="../App_Themes/Styles/Images/img_mand.png"
                                                                ToolTip="Mandatory Field!!!" Width="10px"  />
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txtGRNO"
                                                                ErrorMessage="RequiredFieldValidator" ToolTip="GRNO is required" ValidationGroup="DataSave"><img 
                                                                src="../App_Themes/Styles/Images/err1.png" 
                                                                title="GRNO  is required!!!" />
                                                            </asp:RequiredFieldValidator>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="Label61" runat="server" CssClass="Label" Text="GR_GI_SLIP_NO"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtGRGISLIPNO" runat="server" Width="220px" CssClass="TextBox" MaxLength="10" ></asp:TextBox>
                                                             
                                                        </td>
                                                    </tr>
                                                      <tr>
                                                        <td>
                                                            <asp:Label ID="Label63" runat="server" CssClass="Label" Text="Header Text"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtHeaderText" runat="server" Width="220px" CssClass="TextBox" MaxLength="25"></asp:TextBox>
                                                            <asp:Image ID="Image22" runat="server" Height="10px" ImageUrl="../App_Themes/Styles/Images/img_mand.png"
                                                                ToolTip="Mandatory Field!!!" Width="10px" />
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="txtHeaderText"
                                                                ErrorMessage="RequiredFieldValidator" ToolTip="HeaderText is required" ValidationGroup="DataSave"><img 
                                                                src="../App_Themes/Styles/Images/err1.png" 
                                                                title="HeaderText  is required!!!" />
                                                            </asp:RequiredFieldValidator>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="Label64" runat="server" CssClass="Label" Text="Storage Location :"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtStorageLoc" runat="server" Width="220px" CssClass="TextBox"></asp:TextBox>
                                                            <asp:Image ID="Image6" runat="server" Height="10px" ImageUrl="../App_Themes/Styles/Images/img_mand.png"
                                                                ToolTip="Mandatory Field!!!" Width="10px" />
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtStorageLoc"
                                                                ErrorMessage="RequiredFieldValidator" ToolTip="Storage Location is required" ValidationGroup="DataSave"><img 
                                                                src="../App_Themes/Styles/Images/err1.png" 
                                                                title="Storage Location  is required!!!" />
                                                            </asp:RequiredFieldValidator>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="Label66" runat="server" CssClass="Label" Text="Cost Center :"></asp:Label>
                                                        </td>
                                                        <td>
                                                             <asp:TextBox ID="txtCostCenter" runat="server" Width="220px" CssClass="TextBox"></asp:TextBox>
                                                            
                                                        </td>
                                                    </tr>

                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label68" runat="server" CssClass="Label" Text="Adv licence"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtAdvLic1" runat="server" Width="220px" CssClass="TextBox" MaxLength="25"></asp:TextBox>
                                                           
                                                           
                                                        </td>
                                                         <td>
                                                            <asp:Label ID="Label69" runat="server" CssClass="Label" Text="Adv licence 2"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtAdvLic2" runat="server" Width="220px" CssClass="TextBox" MaxLength="25"></asp:TextBox>
                                                            
                                                        </td>
                                                       
                                                    </tr>
                                                </table>
                                            </asp:Panel>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label29" runat="server" CssClass="Label" Text="Vehicle inspection :"
                                                Font-Bold="true"></asp:Label>
                                            <asp:Panel ID="Panel4" runat="server" BorderStyle="Solid" BorderColor="Black" BorderWidth="1px">
                                                <table cellpadding="5">
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label10" runat="server" CssClass="Label" Text="Check whether vehicle is clean :"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:RadioButton ID="rbVehicleCleanY" Text="Yes" runat="server" GroupName="4" /><asp:RadioButton
                                                                ID="rbVehicleCleanN" Text="No" runat="server" GroupName="4" />
                                                            <%--<asp:RadioButton ID="rbVehicleCleanNA" Text="NA" runat="server" GroupName="4" />--%>
                                                            <asp:Image ID="Image8" runat="server" Height="10px" ImageUrl="../App_Themes/Styles/Images/img_mand.png"
                                                                ToolTip="Mandatory Field!!!" Width="10px" />
                                                        </td>
                                                        <td>
                                                        </td>
                                                        <td>
                                                        </td>
                                                        <td>
                                                        </td>
                                                        <td>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2">
                                                            <asp:Label ID="Label4" runat="server" CssClass="Label" Text="Check whether vehicle is properly closed and protected from heat , rain and Dirt etc :"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:RadioButton ID="rbProtedFromRainY" Text="Yes" runat="server" GroupName="5" /><asp:RadioButton
                                                                ID="rbProtedFromRainN" Text="No" runat="server" GroupName="5" />
                                                            <%--<asp:RadioButton ID="rbProtedFromRainNA" Text="NA" runat="server" GroupName="4" />--%>
                                                            <asp:Image ID="Image9" runat="server" Height="10px" ImageUrl="../App_Themes/Styles/Images/img_mand.png"
                                                                ToolTip="Mandatory Field!!!" Width="10px" />
                                                        </td>
                                                        <td>
                                                        </td>
                                                        <td>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="3">
                                                            <asp:Label ID="Label5" runat="server" CssClass="Label" Text="Is Vehicle carrying any odorous , volatile material or dies, paints , pesticides,Engineering item and corrosive Materials  :"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:RadioButton ID="rbCarryingAroundY" Text="Yes" runat="server" GroupName="6" /><asp:RadioButton
                                                                ID="rbCarryingAroundN" Text="No" runat="server" GroupName="6" />
                                                            <%--<asp:RadioButton ID="rbCarryingAroundNA" Text="NA" runat="server" GroupName="4" />--%>
                                                            <asp:Image ID="Image10" runat="server" Height="10px" ImageUrl="../App_Themes/Styles/Images/img_mand.png"
                                                                ToolTip="Mandatory Field!!!" Width="10px" />
                                                        </td>
                                                        <td>
                                                        </td>
                                                        <td>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </asp:Panel>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label31" runat="server" CssClass="Label" Text="Material Verification :"
                                                Font-Bold="true"></asp:Label>
                                            <asp:Panel ID="Panel5" runat="server" BorderStyle="Solid" BorderColor="Black" BorderWidth="1px">
                                                <table cellpadding="5">
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label12" runat="server" CssClass="Label" Text="Check whether supplier label is in order for containers:"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:RadioButton ID="rbInOrderY" Text="Yes" runat="server" GroupName="7" /><asp:RadioButton
                                                                ID="rbInOrderN" Text="No" runat="server" GroupName="7" />
                                                            <asp:Image ID="Image11" runat="server" Height="10px" ImageUrl="../App_Themes/Styles/Images/img_mand.png"
                                                                ToolTip="Mandatory Field!!!" Width="10px" />
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="Label19" runat="server" CssClass="Label" Text="If Not enter the container No.:"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtContainerNo" runat="server" Width="200px" CssClass="TextBox"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator20" runat="server" ControlToValidate="txtContainerNo"
                                                                ErrorMessage="RequiredFieldValidator" ToolTip="ContainerNo is required" ValidationGroup="DataSave"><img 
                                                    src="../App_Themes/Styles/Images/err1.png" 
                                                    title="ContainerNo is required!!!" />
                                                            </asp:RequiredFieldValidator>
                                                        </td>
                                                        <td>
                                                        </td>
                                                        <td>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label13" runat="server" CssClass="Label" Text="Check whether containers are damaged :"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:RadioButton ID="rbContainerDamagedY" Text="Yes" runat="server" GroupName="8" /><asp:RadioButton
                                                                ID="rbContainerDamagedN" Text="No" runat="server" GroupName="8" />
                                                            <asp:Image ID="Image12" runat="server" Height="10px" ImageUrl="../App_Themes/Styles/Images/img_mand.png"
                                                                ToolTip="Mandatory Field!!!" Width="10px" />
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="Label20" runat="server" CssClass="Label" Text="If Yes enter the container No.:"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtContainerNoDamaged" runat="server" Width="200px" CssClass="TextBox"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator21" runat="server" ControlToValidate="txtContainerNoDamaged"
                                                                ErrorMessage="RequiredFieldValidator" ToolTip="ContainerNoDamaged is required"
                                                                ValidationGroup="DataSave"><img 
                                                                src="../App_Themes/Styles/Images/err1.png" 
                                                                title="ContainerNoDamaged is required!!!" />
                                                            </asp:RequiredFieldValidator>
                                                        </td>
                                                        <td>
                                                        </td>
                                                        <td>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label14" runat="server" CssClass="Label" Text="Check whether all containers are properly sealed  :"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:RadioButton ID="rbSealedY" Text="Yes" runat="server" GroupName="9" /><asp:RadioButton
                                                                ID="rbSealedN" Text="No" runat="server" GroupName="9" />
                                                            <asp:Image ID="Image13" runat="server" Height="10px" ImageUrl="../App_Themes/Styles/Images/img_mand.png"
                                                                ToolTip="Mandatory Field!!!" Width="10px" />
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="Label21" runat="server" CssClass="Label" Text="If Not enter the container No. :"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtContainerSealed" runat="server" Width="200px" CssClass="TextBox"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator22" runat="server" ControlToValidate="txtContainerSealed"
                                                                ErrorMessage="RequiredFieldValidator" ToolTip="ContainerSealed is required" ValidationGroup="DataSave"><img 
                                                                src="../App_Themes/Styles/Images/err1.png" 
                                                                title="ContainerSealed is required!!!" />
                                                            </asp:RequiredFieldValidator>
                                                        </td>
                                                        <td>
                                                        </td>
                                                        <td>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label15" runat="server" CssClass="Label" Text="Check whether seal no of containers matches with containers No. mantained in vendor documents :"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:RadioButton ID="rbContainerMatchedY" Text="Yes" runat="server" GroupName="10" /><asp:RadioButton
                                                                ID="rbContainerMatchedN" Text="No" runat="server" GroupName="10" />
                                                            <asp:RadioButton ID="rbContainerMatchedNA" Text="NA" runat="server" GroupName="10" />
                                                            <asp:Image ID="Image14" runat="server" Height="10px" ImageUrl="../App_Themes/Styles/Images/img_mand.png"
                                                                ToolTip="Mandatory Field!!!" Width="10px" />
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="Label23" runat="server" CssClass="Label" Text="If Not enter the container No.:"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtContainerMatched" runat="server" Width="200px" CssClass="TextBox"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator23" runat="server" ControlToValidate="txtContainerMatched"
                                                                ErrorMessage="RequiredFieldValidator" ToolTip="ContainerMatched is required"
                                                                ValidationGroup="DataSave"><img 
                                                                src="../App_Themes/Styles/Images/err1.png" 
                                                                title="ContainerMatched is required!!!" />
                                                            </asp:RequiredFieldValidator>
                                                        </td>
                                                        <td>
                                                        </td>
                                                        <td>
                                                        </td>
                                                    </tr>
                                                    <tr style="visibility: hidden">
                                                        <td>
                                                            <asp:Label ID="Label17" runat="server" CssClass="Label" Text="No. mantained in vendor documents  :"></asp:Label>
                                                        </td>
                                                        <td style="visibility: hidden">
                                                            <asp:RadioButton ID="rbVendorDocumentsY" Text="Yes" runat="server" GroupName="11" /><asp:RadioButton
                                                                ID="rbVendorDocumentsN" Text="No" runat="server" GroupName="11" />
                                                            <asp:Image ID="Image15" runat="server" Height="10px" ImageUrl="../App_Themes/Styles/Images/img_mand.png"
                                                                ToolTip="Mandatory Field!!!" Width="10px" />
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="Label25" runat="server" CssClass="Label" Text="If Not enter the container No.:"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtVendorDocument" runat="server" Width="200px" CssClass="TextBox"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator24" runat="server" ControlToValidate="txtVendorDocument"
                                                                ErrorMessage="RequiredFieldValidator" ToolTip="VendorDocument is required" ValidationGroup="DataSave">
                                                                <img  src="../App_Themes/Styles/Images/err1.png"  title="VendorDocument is required!!!" />
                                                            </asp:RequiredFieldValidator>
                                                        </td>
                                                        <td>
                                                        </td>
                                                        <td>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </asp:Panel>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <table cellpadding="5">
                                                <tr>
                                                    <td>
                                                        <asp:Button ID="btnSaveGeneralDetails" runat="server" CssClass="Button" Text="SAVE"
                                                            ValidationGroup="DataSave1" Width="75px" OnClick="btnSaveGeneralDetails_Click" OnClientClick="return confirm('Please check all the details , Are you sure you want to Save');"  />
                                                    </td>
                                                    <td>
                                                        <asp:Button ID="btnCloseGeneralDetails" runat="server" CssClass="Button" Text="CLOSE"
                                                            Width="75px" OnClick="btnCloseGeneralDetails_Click" />
                                                    </td>
                                                    <td>
                                                    </td>
                                                    <td>
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
                                                    <td>
                                                        &nbsp;
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </asp:View>
                            <asp:View ID="View3" runat="server">
                              <asp:Label ID="Label47" runat="server" CssClass="Label" Text=" Important Details :  "
                                                Font-Bold="true"></asp:Label>
                                                <asp:Label ID="Label56" runat="server" CssClass="Label" Text="(IF  FIELD  VALUE NOT  REQUIRED THEN KEEP  IT  BLANK) "
                                                BackColor="Yellow" Width="600PX" ForeColor="Red" Font-Bold="true" ></asp:Label>
                                <table cellpadding="0" cellspacing="0" width="100%">
                                    <tr>
                                        <td>

                                            <asp:Panel ID="Panel6" runat="server" BorderStyle="Solid" BorderColor="Black" BorderWidth="1px">
                                                <table cellpadding="5">
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label34" runat="server" CssClass="Label" Text="Purchase order No :"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblPurchaseOrderRec" runat="server" CssClass="Label" Text=""></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="Label38" runat="server" CssClass="Label" Text="Line No. :"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblLineNo" runat="server" CssClass="Label" Text=""></asp:Label>
                                                        </td>
                                                        <td>
                                                        </td>
                                                        <td>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label37" runat="server" CssClass="Label" Text="Material Code :"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblMaterialCode" runat="server" CssClass="Label" Text=""></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="Label43" runat="server" CssClass="Label" Text="Material Desc. :"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblMaterialDesc" runat="server" CssClass="Label" Text=""></asp:Label>
                                                        </td>
                                                        <td>
                                                        </td>
                                                        <td>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </asp:Panel>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Panel ID="Panel7" runat="server" BorderStyle="Solid" BorderColor="Black" BorderWidth="1px">
                                                <table cellpadding="5">
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label49" runat="server" CssClass="Label" Text="Vendor Batch No. :"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtVendorBatchNo" runat="server" Width="200px" CssClass="TextBox" MaxLength="15"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="Label50" runat="server" CssClass="Label" Text="MFG Date:"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtMfgDate" runat="server" Width="200px" CssClass="DateTimeText"></asp:TextBox>
                                                            <asp:Image ID="Image16" runat="server" Height="10px" ImageUrl="../App_Themes/Styles/Images/img_mand.png"
                                                                ToolTip="Mandatory Field!!!" Width="10px" />
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtMfgDate"
                                                                ErrorMessage="RequiredFieldValidator" ToolTip="Mfg Date is required" ValidationGroup="Get">
                                                             <img src="../App_Themes/Styles/Images/err1.png"  title="Plant Code is required!!!" />
                                                            </asp:RequiredFieldValidator>
                                                            <asp:CalendarExtender ID="CalendarExtender2" runat="server" Enabled="True" TargetControlID="txtMfgDate"
                                                                Format="dd-MM-yyyy">
                                                            </asp:CalendarExtender>
                                                              <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ControlToValidate="txtMfgDate"
                                                             ValidationExpression="^(0[1-9]|[12][0-9]|3[01])[-](0[1-9]|1[012])[-](19|20)\d\d$" 
                                                              Display="Dynamic" SetFocusOnError="true" ErrorMessage="Date Format Invalid" ></asp:RegularExpressionValidator>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="Label40" runat="server" CssClass="Label" Text="EXP Date:"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtExpDate" runat="server" Width="200px" CssClass="DateTimeText"></asp:TextBox>
                                                            <asp:Image ID="Image17" runat="server" Height="10px" ImageUrl="../App_Themes/Styles/Images/img_mand.png"
                                                                ToolTip="Mandatory Field!!!" Width="10px" />
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtExpDate"
                                                                ErrorMessage="RequiredFieldValidator" ToolTip="Exp date is required" ValidationGroup="Get">
                                                                <img src="../App_Themes/Styles/Images/err1.png" title="Exp date is required!!!" />
                                                            </asp:RequiredFieldValidator>
                                                            <asp:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="True" TargetControlID="txtExpDate"
                                                                Format="dd-MM-yyyy">
                                                            </asp:CalendarExtender>
                                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" ControlToValidate="txtExpDate"
                                                             ValidationExpression="^(0[1-9]|[12][0-9]|3[01])[-](0[1-9]|1[012])[-](19|20)\d\d$" 
                                                              Display="Dynamic" SetFocusOnError="true" ErrorMessage="Date Format Invalid" ></asp:RegularExpressionValidator>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label51" runat="server" CssClass="Label" Text="Balance Life (Months/Percentage) :"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtBalanceLife" runat="server" Width="200px" CssClass="TextBox"></asp:TextBox>
                                                            <asp:Image ID="Image18" runat="server" Height="10px" ImageUrl="../App_Themes/Styles/Images/img_mand.png"
                                                                ToolTip="Mandatory Field!!!" Width="10px" />
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtBalanceLife"
                                                                ErrorMessage="RequiredFieldValidator" ToolTip="Balance Life is required" ValidationGroup="Get">
                                                                <img src="../App_Themes/Styles/Images/err1.png" title="Exp date is required!!!" />
                                                            </asp:RequiredFieldValidator>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="Label52" runat="server" CssClass="Label" Text="Order Qty. :"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblOrderQty" runat="server" CssClass="Label" Text="" BackColor="#ccff66"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="Label41" runat="server" CssClass="Label" Text="Qty Accepted:"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtQtyAccepted" runat="server" Width="200px" CssClass="TextBox"
                                                                onkeypress="return numericOnly(this);" OnTextChanged="txtQtyAccepted_TextChanged"
                                                                AutoPostBack="true"></asp:TextBox>
                                                            <asp:Image ID="Image19" runat="server" Height="10px" ImageUrl="../App_Themes/Styles/Images/img_mand.png"
                                                                ToolTip="Mandatory Field!!!" Width="10px" />
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtQtyAccepted"
                                                                ErrorMessage="RequiredFieldValidator" ToolTip="QtyAccepted is required" ValidationGroup="Get">
                                                                <img alt="" src="../App_Themes/Styles/Images/err1.png" title="Exp date is required!!!" />
                                                            </asp:RequiredFieldValidator>
                                                            <asp:Label ID="Label67" runat="server" CssClass="Label" Text="Remaining Accepted Qty: " BackColor="YellowGreen" Font-Bold="true" ></asp:Label>
                                                             <asp:Label ID="lblRemainingQty" runat="server" CssClass="Label" Text="" BackColor="YellowGreen"  Font-Bold="true" ></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label54" runat="server" CssClass="Label" Text="Total Containers "></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtTotalContainers" runat="server" Width="200px" CssClass="TextBox"
                                                                onkeypress="return numericOnly(this);"></asp:TextBox>
                                                            <asp:Image ID="Image21" runat="server" Height="10px" ImageUrl="../App_Themes/Styles/Images/img_mand.png"
                                                                ToolTip="Mandatory Field!!!" Width="10px" />
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtTotalContainers"
                                                                ErrorMessage="RequiredFieldValidator" ToolTip="TotalContainers is required" ValidationGroup="Get">
                                                                <img src="../App_Themes/Styles/Images/err1.png" title="Exp date is required!!!" />
                                                            </asp:RequiredFieldValidator>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="Label62" runat="server" CssClass="Label" Text="UOM :"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblUOM" runat="server" CssClass="Label" Text="" BackColor="#ccff66"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="Label33" runat="server" CssClass="Label" Text="LIC_NO:"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtLicNo" runat="server" Width="200px" CssClass="TextBox"></asp:TextBox>   
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label45" runat="server" CssClass="Label" Text="Enter scan weighing code:"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtBalanceCode" runat="server" Width="200px" CssClass="TextBox"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="Label1" runat="server" CssClass="Label" Text="GM_Code:"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtGmCode" runat="server" Width="200px" CssClass="TextBox"></asp:TextBox>
                                                            <asp:Image ID="Image24" runat="server" Height="10px" ImageUrl="../App_Themes/Styles/Images/img_mand.png"
                                                                ToolTip="Mandatory Field!!!" Width="10px" />
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="txtGmCode"
                                                                ErrorMessage="RequiredFieldValidator" ToolTip="Exp date is required" ValidationGroup="Get">
                                                                <img src="../App_Themes/Styles/Images/err1.png" title="GmCode is required!!!" />
                                                            </asp:RequiredFieldValidator>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="Label36" runat="server" CssClass="Label" Text="Pack_Qty:"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtPackQty" runat="server" Width="200px" CssClass="TextBox" onkeypress="return numericOnly(this);"></asp:TextBox>
                                                            <asp:Image ID="Image25" runat="server" Height="10px" ImageUrl="../App_Themes/Styles/Images/img_mand.png"
                                                                ToolTip="Mandatory Field!!!" Width="10px" />
                                                        </td>
                                                    </tr>
                                                    <tr id="trWeight" runat="server">
                                                        <td>
                                                            <asp:Label ID="Label46" runat="server" CssClass="Label" Text="Gross Wt:"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtGrossWt" runat="server" Width="160px" CssClass="TextBox" onkeypress="return numericOnly(this);"></asp:TextBox>
                                                            <asp:Button ID="btnStartWeigh1" runat="server" class="btn-primary" Text="Scan" Width="40px"
                                                                Height="30px" float="right" OnClick="btnStartWeigh1_Click" CssClass="Button" />
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="Label48" runat="server" CssClass="Label" Text="Net Wt:"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtNetWt" runat="server" Width="200px" CssClass="TextBox" 
                                                                onkeypress="return numericOnly(this);" ontextchanged="txtNetWt_TextChanged"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="Label55" runat="server" CssClass="Label" Text="Tare Wt:"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtTareWt" runat="server" Width="200px" CssClass="TextBox" onkeypress="return numericOnly(this);"></asp:TextBox>
                                                           

                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="4" style="background-color: Yellow">
                                                            <asp:Label ID="Label44" runat="server" CssClass="Label" Text="For Adding Multiple Weights and PackSize , Click on Add Values Button. "></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Button ID="btnAdd_Wt" runat="server" CssClass="Button" Text="ADD VALUES" Width="100px"
                                                                OnClick="btnAdd_Wt_Click" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </asp:Panel>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <table cellpadding="5">
                                                <tr>
                                                    <td>
                                                        <asp:Button ID="btnSaveReceiving" runat="server" CssClass="Button" Text=" SAVE " Width="75px"
                                                            ValidationGroup="Get" OnClick="btnSaveReceiving_Click" OnClientClick="return confirm('Please check all the details , Are you sure you want to Save');"
                                                             />
                                                    </td>
                                                    <td>
                                                        <asp:Button ID="btnCloseReceiving" runat="server" CssClass="Button" Text=" CLOSE "
                                                            Width="75px" OnClick="btnCloseReceiving_Click" />
                                                    </td>
                                                    <td>
                                                    </td>
                                                    <td>
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
            <div>
            <asp:Button ID="btnSelect" runat="server" Text="Select" CssClass="hidden"  Width="1px"  Height="0px" BackColor="Transparent" BorderColor="Transparent" />
            <asp:ModalPopupExtender ID="ModalPopupExtender1" runat="server" PopupControlID="PnlAddWeight"
                TargetControlID="btnSelect" BackgroundCssClass="modalBackground" CancelControlID="btnclose_wt">
            </asp:ModalPopupExtender>
            <asp:Panel ID="PnlAddWeight" runat="server" align="center" Style="width: auto; height: auto;
                display: none">
                <div id="Div1" tabindex="-1" role="dialog" runat="server" style="width: 100%; height: 100%">
                    <div class="modal-dialog-centered">
                        <div class="modal-content">
                            <div class="modal-header">
                                <h4 class="modal-title" id="H1">
                                    Add Weights / Add Packsize</h4>
                            </div>
                            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                <ContentTemplate>
                                    <div class="modal-body">
                                        <asp:RadioButton ID="rdWeight" runat="server" Text="Weights" AutoPostBack="true"
                                            OnCheckedChanged="rdWeight_CheckedChanged" Visible="false" />
                                        <asp:RadioButton ID="rdPack" runat="server" Text="Pack Size" AutoPostBack="true"
                                            OnCheckedChanged="rdPack_CheckedChanged" Style="margin-left: 10px" />
                                        <asp:RadioButton ID="rdboth" runat="server" Text="Both" AutoPostBack="true" OnCheckedChanged="rdboth_CheckedChanged"
                                            Style="margin-left: 10px" />
                                    </div>
                                    <div class="modal-body" style="width: 800px; height: auto; border-bottom: 1px solid #bfbfbf"
                                        id="divValue" runat="server">
                                        <asp:Label ID="lblgross" runat="server" CssClass="Label" Text="Gross wt :"></asp:Label>
                                        <asp:TextBox ID="txtgross" runat="server" CssClass="TextBox" Height="25px" SkinID="NAME"
                                            Width="50px" onkeypress="return numericOnly(this);"> </asp:TextBox>
                                        <asp:Button ID="btnStartWeigh2" runat="server" class="btn-primary" Text="Scan" Width="40px"
                                            Height="30px" float="right" OnClick="btnStartWeigh2_Click" CssClass="Button" />
                                        <asp:Label ID="lblnet" runat="server" CssClass="Label" Text="Net wt :"></asp:Label>
                                        <asp:TextBox ID="txtnet" runat="server" CssClass="TextBox" Height="25px" SkinID="NAME"
                                            Width="50px" onkeypress="return numericOnly(this);" 
                                            ontextchanged="txtnet_TextChanged" AutoPostBack ="true"></asp:TextBox>
                                        <asp:Label ID="lbltare" runat="server" CssClass="Label" Text="Tare wt :"></asp:Label>
                                        <asp:TextBox ID="txttare" runat="server" CssClass="TextBox" Height="25px" SkinID="NAME"
                                            Width="50px"></asp:TextBox>
                                        <asp:Label ID="lblpacksize" runat="server" CssClass="Label" Text="Pack Size :"></asp:Label>
                                        <asp:TextBox ID="txtpacksize" runat="server" CssClass="TextBox" Height="25px" SkinID="NAME"
                                            Width="50px" onkeypress="return numericOnly(this);"></asp:TextBox>
                                        <asp:Label ID="lblnoofcontainer" runat="server" CssClass="Label" Text="No of Containers :"></asp:Label>
                                        <asp:TextBox ID="txtNoOfContainers" runat="server" Width="50px" CssClass="TextBox"
                                            onkeypress="return numericOnly(this);"></asp:TextBox>
                                        <asp:Button ID="btnAdd" runat="server" CssClass="Button" Text=" ADD " Width="50px"
                                            OnClick="btnAdd_Click" />
                                    </div>
                                    <div class="modal-body" id="Divmodify" runat="server" style="width: 75%; border-bottom: 1px solid #bfbfbf">
                                        <asp:Label ID="Label53" runat="server" Width="100px" CssClass="Label" Text="Total Container" />
                                        <asp:Label ID="lbltotalcontainersNo" runat="server" Width="50px" CssClass="Label"
                                            BackColor="Yellow" />
                                        <asp:Label ID="Label57" runat="server" Width="100px" CssClass="Label" Text="Total PackSize" />
                                        <asp:Label ID="lblTotalpacksize" runat="server" Width="50px" CssClass="Label" BackColor="Yellow" />
                                        <asp:Label ID="Label58" runat="server" Width="100px" CssClass="Label" Text="Total Quantity" />
                                        <asp:Label ID="lblTotalQuantity" runat="server" Width="50px" CssClass="Label" BackColor="Yellow" />
                                    </div>
                                    <div class="modal-body" style="border-bottom: 1px solid #bfbfbf">
                                        <asp:GridView ID="GridView1" runat="server" CssClass="GridViewStyle" AllowPaging="true"
                                            PageSize="10" Style="width: 75%; text-align: center" OnPageIndexChanging="GridView1_PageIndexChanging"
                                            OnRowDeleting="GridView1_RowDeleting">
                                            <Columns>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:Button ID="btnDeleteWt" runat="server" Text="Remove" Width="80px" CommandName="Delete" />
                                                        <%--<asp:CheckBox ID="Chk_weight" runat="server" AutoPostBack="true" OnCheckedChanged="Chk_weight_CheckedChanged" />--%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                            <PagerSettings Position="TopAndBottom" />
                                            <RowStyle CssClass="GridViewRowStyle" />
                                            <FooterStyle CssClass="GridViewFooterStyle" />
                                            <PagerStyle CssClass="GridViewPagerStyle" />
                                            <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                                            <HeaderStyle CssClass="GridViewHeaderStyle" />
                                            <EditRowStyle CssClass="GridViewEditRowStyle" />
                                            <EmptyDataRowStyle CssClass="EmptyDataRowStyle" />
                                            <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                                        </asp:GridView>
                                    </div>
                                    <div class="modal-body">
                                        <asp:Button ID="btnclose_wt" runat="server" CssClass="Button" Text="CLOSE" Width="150px"
                                            OnClick="btnclose_Click" UseSubmitBehavior="False" />
                                        <asp:Button ID="btnSave_wt" runat="server" CssClass="Button" Text="SAVE DETAILS"
                                            Width="150px" OnClick="btnSave_wt_Click" UseSubmitBehavior="False" />
                                    </div>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="btnclose_wt" />
                                    <asp:AsyncPostBackTrigger ControlID="rdWeight" />
                                    <asp:AsyncPostBackTrigger ControlID="rdPack" />
                                    <asp:AsyncPostBackTrigger ControlID="rdboth" />
                                    <asp:AsyncPostBackTrigger ControlID="btnAdd" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                </div>
            </asp:Panel>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnGet" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
