<%@ Page Title="" Language="C#" MasterPageFile="~/Modules/MasterPage.master" AutoEventWireup="true" 
CodeFile="frmMRNLabelRePrinting.aspx.cs"  EnableEventValidation="false"
Inherits="frmMRNLabelRePrinting" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="../App_Themes/Styles/Style_Controls.css" rel="stylesheet" type="text/css" />
    <link href="../App_Themes/Styles/Style_Design.css" rel="stylesheet" type="text/css" />
    <link href="../App_Themes/Styles/Style_Grid.css" rel="stylesheet" type="text/css" />
   <script src="../App_Themes/Styles/jquery.min.js" type="text/javascript"></script>
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
                                            <asp:Label ID="Label35" runat="server" CssClass="HeadingLabel" Text="&amp;nbsp;MRN Label Re-Printing"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <table cellpadding="5">
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="lblManNo" runat="server" CssClass="Label" Text="Material Doc No. / Barcode serial no. :"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtMatDocNo" runat="server" CssClass="TextBox" Height="25px" SkinID="NAME"
                                                            Width="200px"></asp:TextBox>
                                                       <%-- <asp:Image ID="Image1" runat="server" Height="10px" ImageUrl="../App_Themes/Styles/Images/img_mand.png"
                                                            ToolTip="Mandatory Field!!!" Width="10px" />
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtMatDocNo"
                                                            ErrorMessage="RequiredFieldValidator" ToolTip="Document is Required" ValidationGroup="Get"><img 
                                                                                src="../App_Themes/Styles/Images/err1.png" 
                                                                                title="Document is required!!!" /></asp:RequiredFieldValidator>--%>
                                                    </td>
                                                     <td>
                                                        <asp:Label ID="Label2" runat="server" CssClass="Label" Text="Process Order No :"></asp:Label>
                                                    </td>
                                                     <td>
                                                        <asp:TextBox ID="txtProcessOrder" runat="server" CssClass="TextBox" Height="25px" SkinID="NAME"
                                                            Width="220px"></asp:TextBox>
                                                       <%-- <asp:Image ID="Image2" runat="server" Height="10px" ImageUrl="../App_Themes/Styles/Images/img_mand.png"
                                                            ToolTip="Mandatory Field!!!" Width="10px" />
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtProcessOrder"
                                                            ErrorMessage="RequiredFieldValidator" ToolTip="Document is Required" ValidationGroup="Get"><img 
                                                                                src="../App_Themes/Styles/Images/err1.png" 
                                                                                title="Document is required!!!" /></asp:RequiredFieldValidator>--%>
                                                    </td>
                                                    <td>
                                                        <asp:Button ID="btnGet" runat="server" CssClass="Button" Text="Get" Width="75px"
                                                            ValidationGroup="Get" OnClick="btnGet_Click" />
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
                                                    EmptyDataText="There are no items to show in this view." PageSize="20" ShowFooter="True"
                                                    Width="100%" >
                                                    <Columns>
                                                        <asp:TemplateField ControlStyle-Width="15px" HeaderText="Select">
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="chkSelect" runat="server" AutoPostBack="true"/>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                       <asp:BoundField DataField="BarcodeNo" HeaderText="BarcodeNo" SortExpression="BarcodeNo" />
                                                       <asp:BoundField DataField="Plant" HeaderText="Plant" SortExpression="Plant" />
                                                        <asp:BoundField DataField="PurchaseOrderNo" HeaderText="MaterialDocNo" SortExpression="PurchaseOrderNo" />
                                                        <asp:BoundField DataField="PO_Res_No" HeaderText="ProcessOrderNo" SortExpression="PO_Res_No" />
                                                        <asp:BoundField DataField="ItemCode" HeaderText="ItemCode" SortExpression="ItemCode" />
                                                        <asp:BoundField DataField="ItemDesc" HeaderText="ItemDesc" SortExpression="ItemDesc" />
                                                        <asp:BoundField DataField="AcceptedQty" HeaderText="Quantity" SortExpression="AcceptedQty" />
                                                        <asp:BoundField DataField="UOM" HeaderText="UOM" SortExpression="UOM" />
                                                        <%--<asp:BoundField DataField="NoofContainers" HeaderText="NoofContainers" SortExpression="NoofContainers" />--%>
                                                        <asp:BoundField DataField="ContainerQty" HeaderText="ContainerQty" SortExpression="ContainerQty" />
                                                        <asp:BoundField DataField="SAPBatchNo" HeaderText="SAPBatchNo" SortExpression="SAPBatchNo" />
                                                        <asp:BoundField DataField="ARNo" HeaderText="ARNo" SortExpression="ARNo" />  
                                                        <asp:BoundField DataField="productcode" HeaderText="productcode" SortExpression="productcode" />  
                                                        <asp:BoundField DataField="productdesc" HeaderText="productdesc" SortExpression="productdesc" />  
                                                        <asp:BoundField DataField="storageLoc" HeaderText="storageLoc" SortExpression="storageLoc" />                  
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
                                                        <asp:Label ID="Label1" runat="server" CssClass="Label" Text="Reason :&amp;nbsp;"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:DropDownList ID="ddlReason" runat="server" CssClass="Combobox">
                                                            <asp:ListItem>
                                                         Select
                                                            </asp:ListItem>
                                                            <asp:ListItem>
                                                         Label Damage
                                                            </asp:ListItem>
                                                            <asp:ListItem>
                                                         Label Missing
                                                            </asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td class="style18">
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" InitialValue="Select" runat="server"
                                                            ControlToValidate="ddlReason" ErrorMessage="RequiredFieldValidator" ToolTip="Reason is required!!!"
                                                            ValidationGroup="PrintLabel"><img src="../App_Themes/Styles/Images/err1.png" 
                                                                                          title="Reason is required!!!" /></asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>
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

