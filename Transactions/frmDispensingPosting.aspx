<%@ Page Title="" Language="C#" MasterPageFile="~/Modules/MasterPage.master" AutoEventWireup="true" 
CodeFile="frmDispensingPosting.aspx.cs" Inherits="frmDispensingPosting" EnableEventValidation="false" %>

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
    </style>
    <script src="../Masters/Javascript/dw_tooltip_c.js" type="text/javascript"></script>
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table cellpadding="0" cellspacing="0" width="100%">
                <tr>              <td>
                        <asp:MultiView ID="MultiView1" runat="server">
                            <asp:View ID="View1" runat="server">
                                <table cellpadding="0" cellspacing="0" width="100%">
                                    <tr>
                                        <td height="30" style="height: 30px; background-image: url('../App_Themes/Styles/Images/Menu_Out.png');
                                            background-repeat: repeat-x">
                                            <asp:Label ID="Label35" runat="server" CssClass="HeadingLabel" Text="&amp;nbsp;Dispensing Data Posting"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <table cellpadding="5">
                                                <tr>
                                                    <td style="width: 50%; text-align: right;">
                                                        <asp:CheckBox ID="chkPONo" runat="server" Text="  PO No" CssClass="Label" Style="margin-right: 50px;
                                                            font-size: 15px" OnCheckedChanged="chkPONo_CheckedChanged" AutoPostBack="True" />
                                                    </td>
                                                    <td>
                                                        <asp:CheckBox ID="chkReservation" runat="server" Text="  Reservation No" CssClass="Label"
                                                            Font-Size="15px" OnCheckedChanged="chkReservation_CheckedChanged" AutoPostBack="True" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <table cellpadding="5">
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="lblDocumentNo" runat="server" CssClass="Label" Text="Process Order No :"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtDocumentNo"  runat="server" CssClass="TextBox" Height="25px" SkinID="NAME"
                                                            Width="220px"></asp:TextBox>
                                                        <asp:Image ID="Image1" runat="server" Height="10px" ImageUrl="../App_Themes/Styles/Images/img_mand.png"
                                                            ToolTip="Mandatory Field!!!" Width="10px" />
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtDocumentNo"
                                                            ErrorMessage="RequiredFieldValidator" ToolTip="Document is Required" ValidationGroup="Get"><img 
                                                                                src="../App_Themes/Styles/Images/err1.png" 
                                                                                title="Document is required!!!" /></asp:RequiredFieldValidator>
                                                    </td>
                                                    <td>
                                                        <asp:Button ID="btnGet" runat="server" CssClass="Button" Text="GET DATA" Width="120px"
                                                            ValidationGroup="Get" onclick="btnGet_Click"   />
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
                                    <td id="DivHeader" runat="server">
                                         <table cellpadding="6">
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="Label1" runat="server" CssClass="Label" Text="HeaderText :"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtheadertext" runat="server" CssClass="TextBox" Height="25px" SkinID="NAME"
                                                            Width="220px"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="Label2" runat="server" CssClass="Label" Text="GR/GI Slip No :"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtGRGINO" runat="server" CssClass="TextBox" Height="25px" SkinID="NAME"
                                                            Width="220px"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <asp:Button ID="btnUpdate" runat="server" CssClass="Button" Text="UPDATE" 
                                                            Width="120px" onclick="btnUpdate_Click" />
                                                    </td>
                                                    
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="height: 30px">
                                        <div style=" width:100vw ;">
                                           <asp:Panel ID="Panel1" runat="server"  Height="400px" ScrollBars="Auto">
                                                <asp:GridView ID="GrDispenDataPO" runat="server" AllowPaging="True" AllowSorting="True"
                                                    AutoGenerateColumns="False" CaptionAlign="Left" CellPadding="4" CssClass="GridViewStyle"
                                                    EmptyDataText="There are no items to show in this view." PageSize="40" ShowFooter="True"
                                                    Width="100%" onpageindexchanging="GrDispenDataPO_PageIndexChanging"  >
                                                    <Columns>
                                                        <asp:TemplateField ControlStyle-Width="15px" HeaderText="Select">
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="chkSelect" runat="server" AutoPostBack="true"  />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="ProcessOrderNo" HeaderText="ProcessOrderNo" SortExpression="ProcessOrderNo" />
                                                        <asp:BoundField DataField="ItemCode" HeaderText="ItemCode" SortExpression="ItemCode" />
                                                        <asp:BoundField DataField="ItemDesc" HeaderText="ItemDesc" SortExpression="ItemDesc" />
                                                        <asp:BoundField DataField="SAPBatchNo" HeaderText="SAPBatchNo" SortExpression="SAPBatchNo" />
                                                        <asp:BoundField DataField="Uom" HeaderText="Uom" SortExpression="Uom" />
                                                        <asp:BoundField DataField="Quantity" HeaderText="Quantity" SortExpression="Quantity" />
                                                        <asp:BoundField DataField="GrossWt" HeaderText="GrossWt" SortExpression="GrossWt" />
                                                        <asp:BoundField DataField="TareWt" HeaderText="TareWt" SortExpression="TareWt" />
                                                        <asp:BoundField DataField="NetWt" HeaderText="NetWt" SortExpression="NetWt" />
                                                        <asp:BoundField DataField="STORAGELOC" HeaderText="STORAGELOC" SortExpression="STORAGELOC" />
                                                        <asp:BoundField DataField="LineItemNo" HeaderText="LineItemNo" SortExpression="LineItemNo" />
                                                        <asp:BoundField DataField="ProductCode" HeaderText="ProductCode" SortExpression="ProductCode" />
                                                        <asp:BoundField DataField="ProductDesc" HeaderText="ProductDesc" SortExpression="ProductDesc" />
                                                        <asp:BoundField DataField="RMQuantity" HeaderText="RMQuantity" SortExpression="RMQuantity" />
                                                        <asp:BoundField DataField="RMUOM" HeaderText="RMUOM" SortExpression="RMUOM" />
                                                        <asp:BoundField DataField="ARNo" HeaderText="ARNo" SortExpression="ARNo" />
                                                        <asp:BoundField DataField="BinCode" HeaderText="BinCode" SortExpression="BinCode" />
                                                        <asp:BoundField DataField="HeaderText" HeaderText="HeaderText" SortExpression="HeaderText" />
                                                        <asp:BoundField DataField="GIGRSlipNo" HeaderText="GIGRSlipNo" SortExpression="GIGRSlipNo" />
                                                        <asp:BoundField DataField="ItemNO" HeaderText="ItemNO" SortExpression="ItemNO" />
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
                                            
                                           
                                                <asp:GridView ID="GrDispenDataRe" runat="server" AllowPaging="True" AllowSorting="True"
                                                    AutoGenerateColumns="False" CaptionAlign="Left" CellPadding="4" CssClass="GridViewStyle"
                                                    EmptyDataText="There are no items to show in this view." PageSize="20" ShowFooter="True"
                                                    Width="100%" onpageindexchanging="GrDispenDataRe_PageIndexChanging"  >
                                                    <Columns>
                                                        <asp:TemplateField ControlStyle-Width="15px" HeaderText="Select">
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="chkSelect" runat="server" AutoPostBack="true" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="ReservationNo" HeaderText="ReservationNo" SortExpression="ReservationNo" />
                                                        <asp:BoundField DataField="ItemCode" HeaderText="ItemCode" SortExpression="ItemCode" />
                                                        <asp:BoundField DataField="ItemDesc" HeaderText="ItemDesc" SortExpression="ItemDesc" />                                                        
                                                        <asp:BoundField DataField="SAPBatchNo" HeaderText="SAPBatchNo" SortExpression="SAPBatchNo" />
                                                        <asp:BoundField DataField="Uom" HeaderText="Uom" SortExpression="Uom" />
                                                        <asp:BoundField DataField="Quantity" HeaderText="Quantity" SortExpression="Quantity" />
                                                        <asp:BoundField DataField="GrossWt" HeaderText="GrossWt" SortExpression="GrossWt" />
                                                        <asp:BoundField DataField="TareWt" HeaderText="TareWt" SortExpression="TareWt" />
                                                        <asp:BoundField DataField="NetWt" HeaderText="NetWt" SortExpression="NetWt" />
                                                        <asp:BoundField DataField="STORAGELOC" HeaderText="STORAGELOC" SortExpression="STORAGELOC" />
                                                        <asp:BoundField DataField="LineItemNo" HeaderText="LineItemNo" SortExpression="LineItemNo" />
                                                        <asp:BoundField DataField="ARNo" HeaderText="ARNo" SortExpression="ARNo" />
                                                        <asp:BoundField DataField="CostCenter" HeaderText="CostCenter" SortExpression="CostCenter" />
                                                        <asp:BoundField DataField="HeaderText" HeaderText="HeaderText" SortExpression="HeaderText" />
                                                        <asp:BoundField DataField="GIGRSlipNo" HeaderText="GIGRSlipNo" SortExpression="GIGRSlipNo" />
                                                        <asp:BoundField DataField="ItemNO" HeaderText="ItemNO" SortExpression="ItemNO" />
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


                                        
                                        </td>
                                    </tr>
                                    <tr><td></td></tr>
                                    </tr>              
                                    <tr>
                                        <td>
                                            <asp:CheckBox ID="btnSelectAll" runat="server" AutoPostBack="true" 
                                                oncheckedchanged="btnSelectAll_CheckedChanged" Text="Select All" />
                                        </td>
                                        <tr>
                                            <td>
                                                <table cellpadding="5">
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="lblsapuser" runat="server" CssClass="Label" Text="SAP User Id :"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtSapUser" runat="server" CssClass="TextBox" Height="25px" 
                                                                SkinID="NAME" Width="220px"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblsappwd" runat="server" CssClass="Label" Text="SAP Password :"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtSapPass" runat="server" CssClass="TextBox" Height="25px" 
                                                                SkinID="NAME" TextMode="Password" Width="220px"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:Button ID="btnPost" runat="server" CssClass="Button" 
                                                                onclick="btnPost_Click" Text="POST DATA TO SAP" Width="200px" />
                                                        </td>
                                                        <td>
                                                            <asp:Button ID="btnClose" runat="server" CssClass="Button" Text="CLOSE" 
                                                                Width="75px" />
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
                                    </tr>
                                </table>
                            </asp:View>
                        </asp:MultiView>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnGet" />
            <asp:AsyncPostBackTrigger ControlID="btnUpdate" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>

