
<%@ Page Title="" Language="C#" MasterPageFile="~/Modules/MasterPage.master" AutoEventWireup="true"  EnableEventValidation="false"
CodeFile="frmDispensingReprinting.aspx.cs" Inherits="frmDispensingReprinting" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
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
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table cellpadding="0" cellspacing="0" width="100%">
                <tr>
                    <td style="height: 30px; background-image: url('../App_Themes/Styles/Images/Form_Title.JPG');
                        background-repeat: repeat-x">
                        <asp:Label ID="Label35" runat="server" CssClass="HeadingLabel" Text="Dispensing Reprinting"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:MultiView ID="MultiView1" runat="server">
                            <asp:View ID="View1" runat="server">
                                <table cellpadding="0" cellspacing="0" width="100%">
                                    <tr>
                                        <td>
                                            <table cellpadding="5">
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="lblDocumentNo" runat="server" CssClass="Label" Text="Process Order No :"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtProcessOrderNo" runat="server" CssClass="TextBox" Height="25px"
                                                            SkinID="NAME" Width="220px"></asp:TextBox>
                                                        <asp:Image ID="Image1" runat="server" Height="10px" ImageUrl="../App_Themes/Styles/Images/img_mand.png"
                                                            ToolTip="Mandatory Field!!!" Width="10px" />
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtProcessOrderNo"
                                                            ErrorMessage="RequiredFieldValidator" ToolTip="Process order no. is Required"
                                                            ValidationGroup="Get"><img src="../App_Themes/Styles/Images/err1.png" title="Process order no. is required!!!" /></asp:RequiredFieldValidator>
                                                    </td>
                                                    <td>
                                                        <asp:Button ID="btnGet" runat="server" CssClass="Button" Text=" GET DATA " Width="75px"
                                                            ValidationGroup="Get" OnClick="btnGet_Click" />
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
                                                    EmptyDataText="There are no items to show in this view." PageSize="20" ShowFooter="True"
                                                    Width="100%" onpageindexchanging="GrGRNDetails_PageIndexChanging">
                                                    <Columns>
                                                        <asp:TemplateField ControlStyle-Width="15px" HeaderText="Select">
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="chkSelect" runat="server" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="BarcodeNo" HeaderText="BarcodeNo" SortExpression="BarcodeNo" />
                                                        <asp:BoundField DataField="ProcessOrder" HeaderText="Process Order" SortExpression="ProcessOrder" />
                                                        <asp:BoundField DataField="ProdCode" HeaderText="Product Code" SortExpression="ProdCode" />
                                                        <asp:BoundField DataField="ProductDesc" HeaderText="Product Desc" SortExpression="ProductDesc" />
                                                        <asp:BoundField DataField="ProdBatch" HeaderText="Product Batch" SortExpression="ProdBatch" />
                                                        <asp:BoundField DataField="LineItemNo" HeaderText="Line Item No" SortExpression="LineItemNo" />
                                                        <asp:BoundField DataField="ItemCode" HeaderText="Material Code" SortExpression="ItemCode" />
                                                        <asp:BoundField DataField="IteemDesc" HeaderText="Material Desc" SortExpression="IteemDesc" />
                                                        <asp:BoundField DataField="MaterialBatch" HeaderText="Material Batch" SortExpression="MaterialBatch" />
                                                        <asp:BoundField DataField="GrossWt" HeaderText="Gross Wt" SortExpression="GrossWt" />
                                                        <asp:BoundField DataField="NetWt" HeaderText="Net Wt" SortExpression="NetWt" />
                                                        <asp:BoundField DataField="TareWt" HeaderText="Tare Wt" SortExpression="TareWt" />
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
                                                        <asp:Button ID="btnPrint" runat="server" Text="Print" CssClass="Button"  Width="75px"
                                                            onclick="btnPrint_Click" />
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

