<%@ Page Title="" Language="C#" MasterPageFile="~/Modules/MasterPage.master" AutoEventWireup="true" 
CodeFile="FrmStatusLabelPrint.aspx.cs" Inherits="FrmStatusLabelPrint" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="../App_Themes/Styles/Style_Controls.css" rel="stylesheet" type="text/css" />
    <link href="../App_Themes/Styles/Style_Design.css" rel="stylesheet" type="text/css" />
    <link href="../App_Themes/Styles/Style_Grid.css" rel="stylesheet" type="text/css" />
   <script src="../Masters/Javascript/JScript.js" type="text/javascript"></script>
   
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

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

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
                                            <asp:Label ID="Label35" runat="server" CssClass="HeadingLabel" Text="&amp;nbsp;Status Label Printing"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <table cellpadding="5">
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="lblDocumentNo" runat="server" CssClass="Label" Text="MAN No :"></asp:Label>
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
                                                        <asp:Button ID="btnGet" runat="server" CssClass="Button" Text="Get" Width="75px"
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
                                        <td style="height: 30px;">
                                            <asp:Panel ID="Panel1" runat="server" Height="400px" ScrollBars="Auto">
                                                <asp:GridView ID="GrGRNDetails" runat="server" AllowPaging="True" AllowSorting="True"
                                                    AutoGenerateColumns="False" CaptionAlign="Left" CellPadding="4" CssClass="GridViewStyle"
                                                    EmptyDataText="There are no items to show in this view."  ShowFooter="True"
                                                    Width="100%"  >
                                                    <Columns>
                                                        <asp:TemplateField ControlStyle-Width="15px" HeaderText="Select">
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="chkSelect" runat="server" AutoPostBack="true" OnCheckedChanged="ChkSelect_CheckedChanged" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="Plant" HeaderText="Plant" SortExpression="Plant" />
                                                        <asp:BoundField DataField="MANNo" HeaderText="MANNo" SortExpression="MANNo" />
                                                        <asp:BoundField DataField="LineItemNo" HeaderText="LineItemNo" SortExpression="LineItemNo" />
                                                        <asp:BoundField DataField="ItemCode" HeaderText="ItemCode" SortExpression="ItemCode" />
                                                        <%--<asp:BoundField DataField="MaterialCode" HeaderText="Material Code" SortExpression="Material Code" />--%>
                                                        <asp:BoundField DataField="ItemType" HeaderText="ItemType" SortExpression="ItemType" />
                                                        <asp:BoundField DataField="PurchaseOrderNo" HeaderText="PurchaseOrderNo" SortExpression="PurchaseOrderNo" />
                                                        <asp:BoundField DataField="PurchaseOrderDate" HeaderText="PurchaseOrderDate" SortExpression="PurchaseOrderDate" />
                                                        <asp:BoundField DataField="OrderQty" HeaderText="OrderQty" SortExpression="OrderQty" />
                                                        <asp:BoundField DataField="Uom" HeaderText="Uom" SortExpression="Uom" />
                                                        <asp:BoundField DataField="AcceptedQty" HeaderText="AcceptedQty" SortExpression="AcceptedQty" />
                                                        <asp:BoundField DataField="NoOfContainer" HeaderText="NoOfContainer" SortExpression="NoOfContainer" />
                                                        <asp:BoundField DataField="PackQty" HeaderText="PackQty" SortExpression="PackQty" />
                                                        <asp:BoundField DataField="GateEntryNo" HeaderText="GateEntryNo" SortExpression="GateEntryNo" />
                                                        <asp:BoundField DataField="SAPBatchNo" HeaderText="SapBatchNo" SortExpression="SapBatchNo" />
                                                        <asp:BoundField DataField="MFGDate" HeaderText="MFGDate" SortExpression="MFGDate" />
                                                        <asp:BoundField DataField="EXPDate" HeaderText="EXPDate" SortExpression="EXPDate" />
                                                        <asp:BoundField DataField="Purchase_Ref" HeaderText="Purchase_Ref" SortExpression="Purchase_Ref" />
                                                        <asp:BoundField DataField="RefNo" HeaderText="RefNo" SortExpression="RefNo" />

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
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Panel ID="pnlPackQty" runat="server" Visible="false">
                                                <table>
                                                    <tr>
                                                        <td>
                                                            &nbsp;
                                                        </td>
                                                        <td>
                                                            &nbsp;
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
                                            <table>
                                               
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="lblPrintername" runat="server" CssClass="Label" Text="Printer&amp;nbsp;Name:"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:DropDownList ID="ddlPrinterName" runat="server" CssClass="Combobox">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td class="style18">
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" InitialValue="Select" runat="server"
                                                            ControlToValidate="ddlPrinterName" ErrorMessage="RequiredFieldValidator" ToolTip="Printer Name is required!!!"
                                                            ValidationGroup="PrintLabel"><img src="../App_Themes/Styles/Images/err1.png" 
                                                                                          title="Printer Name is required!!!" /></asp:RequiredFieldValidator>
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
                                                        <asp:Button ID="btnPrint" runat="server" CssClass="Button" Text="Print" Width="75px"
                                                            ValidationGroup="PrintLabel" OnClick="btnPrint_Click" />
                                                    </td>
                                                    <td>
                                                        <asp:Button ID="btnClose" runat="server" CssClass="Button" Text="Close" Width="75px"
                                                            OnClick="btnClose_Click" />
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
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnGet" />
             <asp:AsyncPostBackTrigger ControlID="btnPrint" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>

