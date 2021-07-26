<%@ Page Title="" Language="C#" MasterPageFile="~/Modules/MasterPage.master" AutoEventWireup="true"
    CodeFile="frmAuditTrailReport.aspx.cs" Inherits="Reports_frmAuditTrailReport" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script language="javascript" src="../Masters/Javascript/JScript.js" type="text/javascript"></script>
    <style type="text/css">
        .hiddencol
        {
            display: none;
        }
        .calender
        {
            width: 400px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
         <asp:MultiView ID="MultiView1" runat="server">
            <asp:View ID="View1" runat="server">
            <table class="Dock_Fill">
                <tr>
                    <td class="FormHeading">
                        <table width="100%">
                            <tr>
                                <td style="width: 24px;">
                                    <asp:ImageButton ID="imgCloseV2" runat="server" Height="24px" ImageUrl="~/App_Themes/Styles/Images/exit.ico"
                                        ToolTip="Close Page" Width="24px" PostBackUrl="~/Modules/frmMain.aspx" />
                                </td>
                                <td style="width: 100%; background-image: url('../App_Themes/Styles/Images/Form_Title.JPG')">
                                    <asp:Label ID="Label35" runat="server" CssClass="HeadingLabel" Text="Audit Trail Report"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                                <table>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblSearch0" runat="server" CssClass="Label" Text="Report Name "></asp:Label>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlrpttype" runat="server" AutoPostBack="True" CssClass="Combobox"
                                                OnSelectedIndexChanged="ddlrpttype_SelectedIndexChanged">
                                                <asp:ListItem>Select</asp:ListItem>
                                                <asp:ListItem>User_Master</asp:ListItem>
                                                <asp:ListItem>Printer_Master</asp:ListItem>
                                                <asp:ListItem>Plant_Master</asp:ListItem>
                                                <asp:ListItem>Weighing_Master</asp:ListItem>
                                                <asp:ListItem>Purchase_Order</asp:ListItem>
                                                <asp:ListItem>Purchase_Order_Weight</asp:ListItem>
                                                <asp:ListItem>Purchase_Order_GeneralDetails</asp:ListItem>
                                                <asp:ListItem>Purchase_Order_ReceivingDetails</asp:ListItem>
                                                <asp:ListItem>ManNo_Creation</asp:ListItem>
                                                <asp:ListItem>ManNo_Transaction(Barcode)</asp:ListItem>
                                                <asp:ListItem>Allocation</asp:ListItem>
                                                <asp:ListItem>PickList</asp:ListItem>
                                                <asp:ListItem>Reservation</asp:ListItem>
                                                <asp:ListItem>Dispensing</asp:ListItem>
                                                <asp:ListItem>Material_Document(Dispensing_Posting)</asp:ListItem>
                                                <asp:ListItem>Dispensing_Posting_Error</asp:ListItem>
                                                <asp:ListItem>Sample_Transaction</asp:ListItem>
                                                <asp:ListItem>Weighing_Calibration_Month</asp:ListItem>
                                                <asp:ListItem>Weighing_Calibration_Daily</asp:ListItem>
                                                <asp:ListItem>Scheduler_INSPECTIONLOT</asp:ListItem>
                                                <asp:ListItem>Scheduler_UDCODE</asp:ListItem>
                                                <asp:ListItem>Scheduler_RETEST</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                </table>
                           
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:GridView ID="grSearch" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                            CaptionAlign="Left" CellPadding="4" CssClass="GridViewStyle" EmptyDataText="There are no items to show in this view."
                            PageSize="20">
                            <Columns>
                                <asp:BoundField DataField="COLUMN_NAME" HeaderText="Field Name" SortExpression="COLUMN_NAME" />
                                <asp:BoundField DataField="DATA_TYPE" HeaderStyle-CssClass="hiddencol" HeaderText="Data Type"
                                    ItemStyle-CssClass="hiddencol" SortExpression="DATA_TYPE">
                                    <HeaderStyle CssClass="hiddencol" />
                                    <ItemStyle CssClass="hiddencol" />
                                </asp:BoundField>
                                <asp:TemplateField HeaderStyle-Width="150px" HeaderText="Search Criteria">
                                    <ItemTemplate>
                                        <asp:DropDownList ID="cboCriteria" runat="server" CssClass="Combobox" Width="120px">
                                            <asp:ListItem Selected="True" Value="=">Equal to</asp:ListItem>
                                            <asp:ListItem Value="&lt;&gt;">Not equal to</asp:ListItem>
                                            <asp:ListItem Value="like">Contains</asp:ListItem>
                                            <asp:ListItem Value="likestart">Start with</asp:ListItem>
                                            <asp:ListItem Value="likeend">End with</asp:ListItem>
                                        </asp:DropDownList>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Search Value">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtSearch" runat="server" CssClass="TextBox" ValidationGroup="Search"></asp:TextBox>
                                        <table id="tblDate" runat="server" visible="false">
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblSearch" runat="server" CssClass="Label" Text="From Date"></asp:Label>
                                                    <asp:TextBox ID="txtFrom" runat="server" CssClass="TextBox" Width="150px" AutoComplete="off"></asp:TextBox>
                                                    <asp:CalendarExtender ID="txtShelf_CalendarExtender" CssClass="calender" runat="server"
                                                        Enabled="True" TargetControlID="txtFrom">
                                                    </asp:CalendarExtender>
                                                </td>
                                                <td>
                                                    <asp:Label ID="Label1" runat="server" CssClass="Label" Text="To Date"></asp:Label>
                                                    <asp:TextBox ID="txtTo" runat="server" CssClass="TextBox" Width="150px" AutoComplete="off"></asp:TextBox>
                                                    <asp:CalendarExtender ID="CalendarExtender1" CssClass="calender" runat="server" Enabled="True"
                                                        TargetControlID="txtTo">
                                                    </asp:CalendarExtender>
                                                </td>
                                            </tr>
                                        </table>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <RowStyle CssClass="GridViewRowStyle" />
                            <FooterStyle CssClass="GridViewFooterStyle" />
                            <PagerStyle CssClass="GridViewPagerStyle" />
                            <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                            <HeaderStyle CssClass="GridViewHeaderStyle" />
                            <EditRowStyle CssClass="GridViewEditRowStyle" />
                            <EmptyDataRowStyle CssClass="EmptyDataRowStyle" />
                            <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                        </asp:GridView>
                    </td>
                </tr>
                <tr>
                    <td>
                        <table>
                            <tr>
                                <td>
                                    <asp:Button ID="btnSearch" runat="server" CssClass="Button" Height="23px" OnClick="btnSearch_Click"
                                        Text="Search" ValidationGroup="Search" />
                                </td>
                                <td>
                                    <asp:Button ID="btnViewAll" runat="server" CssClass="Button" Height="23px" OnClick="btnViewAll_Click"
                                        Text="View All" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
            </asp:View>
            <asp:View ID="View2" runat="server">
                <table>
                    <tr>
                        <td>
                            <asp:Button ID="btnBack" runat="server" CssClass="Button" Height="23px" OnClick="btnBack_Click"
                                Text="Back To Search" />
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblModule" runat="server" CssClass="Label" Text="" Font-Size="20px"
                                BackColor="#0063be" ForeColor="White"> </asp:Label>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                </table>
                <asp:Panel ID="Panel1" runat="server" ScrollBars="Auto" Width="100%">
                    <%-- <div ID="grdCharges" runat="server" 
                        style="border: 1px solid #999999; overflow: auto; height: 600px; width:1280px">--%>
                    <asp:GridView ID="GrPlants" runat="server" CaptionAlign="Left" CellPadding="4" CssClass="GridViewStyle"
                        EmptyDataText="There are no items to show in this view." OnRowDataBound="GrPlants_RowDataBound"
                        PageSize="50" ShowFooter="True" AllowPaging="true" OnPageIndexChanging="GrPlants_PageIndexChanging"
                        OnRowCreated="GrPlants_RowCreated" Width="100%" BackColor="#704F98" OnSelectedIndexChanged="GrPlants_SelectedIndexChanged">
                        <RowStyle CssClass="GridViewRowStyle" />
                        <FooterStyle CssClass="GridViewFooterStyle" />
                        <PagerStyle CssClass="GridViewPagerStyle" />
                        <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                        <HeaderStyle CssClass="GridViewHeaderStyle" />
                        <EditRowStyle CssClass="GridViewEditRowStyle" />
                        <EmptyDataRowStyle CssClass="EmptyDataRowStyle" />
                        <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                    </asp:GridView>
                    <table>
                        <tr>
                            <td>
                                <asp:Button ID="btnPost" runat="server" CausesValidation="true" CssClass="Button"
                                    Text="Download PDF" ValidationGroup="Post" OnClick="btnPost_Click" />
                            </td>
                              <td> 
                                  <asp:Button ID="BtnExcel" runat="server" CausesValidation="true" CssClass="Button"
                                    Text="Download Excel" onclick="BtnExcel_Click"   /></td>
                            <td>
                                <asp:Button ID="btnCancel" runat="server" CssClass="Button" Text="Cancel" OnClick="btnCancel_Click" />
                            </td>
                          
                        </tr>
                    </table>
                </asp:Panel>
                <%--</div>--%>
            </asp:View>
            </asp:MultiView>
        </ContentTemplate>
        <Triggers>
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
