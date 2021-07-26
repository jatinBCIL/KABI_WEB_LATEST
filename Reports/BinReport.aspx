<%@ Page Title="" Language="C#" MasterPageFile="~/Modules/MasterPage.master" AutoEventWireup="true"
    CodeFile="BinReport.aspx.cs" Inherits="Reports_BinReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .hiddencol
        {
            display: none;
        }
        .calender
        {
            width: 400px;
        }
        .style4
        {
            width: 594px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table width="100%">
        <tr>
            <td style="width: 24px;">
                <asp:ImageButton ID="imgCloseV2" runat="server" Height="24px" ImageUrl="~/App_Themes/Styles/Images/exit.ico"
                    ToolTip="Close Page" Width="24px" PostBackUrl="~/Modules/frmMain.aspx" />
            </td>
            <td style="width: 100%; background-image: url('../App_Themes/Styles/Images/Form_Title.JPG')">
                <asp:Label ID="Label35" runat="server" CssClass="HeadingLabel" Text="Bin and Material Reports"></asp:Label>
            </td>
        </tr>
    </table>
    <table width="auto" id="tblmain" border="0" runat="server">
        <tr>
            <td class="style4">
                <table border="0" style="border-collapse: collapse; padding: 5px;">
                    <tr style="border: 1px solid black; border-collapse: collapse; padding: 5px;">
                        <th colspan="3" style="background-image: url('../App_Themes/Styles/Images/Form_Title.JPG');
                            height: 30px; ">
                        </th>
                    </tr>
                    <tr style="width: 70px; text-align: center;" id="trdepartment" runat="server">
                        <td style="border-collapse: collapse;">
                            <asp:Label ID="Label9" runat="server" CssClass="Label" Text="Bin : " Visible="true"
                                Width="100px"></asp:Label>
                        </td>
                        <td style="width: 170px; text-align: center; border-collapse: collapse;">
                            <asp:DropDownList ID="ddlbin" runat="server" CssClass="Combobox" Width="120px">
                            </asp:DropDownList>
                        </td>
                        <td style="text-align: center" class="modal-sm">
                            <asp:ImageButton ID="imgSearch1" OnClick="imgSearch1_Click" runat="server" ImageUrl="~/App_Themes/Styles/Images/search.png"
                                Style="height: 20px; width: 20px;" />
                        </td>
                    </tr>
                    <tr style="width: 70px; text-align: center;" id="tr1" runat="server">
                        <td style="border-collapse: collapse;">
                            <asp:Label ID="Label1" runat="server" CssClass="Label" Text="Material : " Visible="true"
                                Width="100px"></asp:Label>
                        </td>
                        <td style="width: 170px; text-align: center; border-collapse: collapse;">
                            <asp:DropDownList ID="ddlMaterial" runat="server" CssClass="Combobox" Width="120px">
                            </asp:DropDownList>
                        </td>
                     
                        <td style="text-align: center" class="modal-sm">
                            <asp:ImageButton ID="imgSearch2" runat="server" OnClick="imgSearch2_Click" ImageUrl="~/App_Themes/Styles/Images/search.png"
                                Style="height: 20px; width: 20px;" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3" style="background-image: url('../App_Themes/Styles/Images/Accordion_Leave.png');
                            height: 20px">
                        </td>
                    </tr>
                </table>
            </td>
            <td>
            </td>
        </tr>
    </table>
    <br />
    <table>
        <tr>
            <td>
            </td>
        </tr>

    </table>
    <table style="width: 100%" id="tblgrid" visible="false" runat="server">
        <tr>
            <td>
                <asp:Button ID="btnpdfGenerate" Text="Generate PDF" OnClick="btnpdfGenerate_click"
                    CssClass="Button" runat="server" />
            </td>
        </tr>
        <tr id="GridviewItem" runat="server">
            <td colspan="4">
                <asp:GridView ID="GrItemData" runat="server" AllowPaging="True" AllowSorting="True"
                    AutoGenerateColumns="false" CaptionAlign="Left" CellPadding="7" CssClass="GridViewStyle"
                    EmptyDataText="There are no items to show in this view." PageSize="20" ShowFooter="True"
                    DataKeyNames="RefNo" Width="100%">
                    <PagerSettings Position="TopAndBottom" />
                    <RowStyle CssClass="GridViewRowStyle" />
                    <FooterStyle CssClass="GridViewFooterStyle" />
                    <PagerStyle CssClass="GridViewPagerStyle" />
                    <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                    <HeaderStyle CssClass="GridViewHeaderStyle" />
                    <EditRowStyle CssClass="GridViewEditRowStyle" />
                    <EmptyDataRowStyle CssClass="EmptyDataRowStyle" />
                    <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                    <Columns>
                        <asp:TemplateField HeaderText="ID NO.">
                            <ItemTemplate>
                                <asp:Label ID="lblRefNo" Text='<%# Eval("RefNo") %>' runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Material Batch">
                            <ItemTemplate>
                                <asp:Label ID="lblRefNo" Text='<%# Eval("MaterialBatch") %>' runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Material Code">
                            <ItemTemplate>
                                <asp:Label ID="lblRefNo" Text='<%# Eval("MaterialCode") %>' runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Bin No.">
                            <ItemTemplate>
                                <asp:Label ID="lblRefNo" Text='<%# Eval("Bin") %>' runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Count">
                            <ItemTemplate>
                                <asp:Label ID="lblRefNo" Text='<%# Eval("Cont") %>' runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Qautity">
                            <ItemTemplate>
                                <asp:Label ID="lblRefNo" Text='<%# Eval("Qty") %>' runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Storage Bin">
                            <ItemTemplate>
                                <asp:Label ID="lblRefNo" Text='<%# Eval("SOURCE_STORAGE_BIN") %>' runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Status">
                            <ItemTemplate>
                                <asp:Label ID="lblRefNo" Text='<%# Eval("Status") %>' runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
    </table>
</asp:Content>
