<%@ Page Title="" Language="C#" MasterPageFile="~/Modules/MasterPage.master" AutoEventWireup="true" CodeFile="frmMasterBarcodePrinting.aspx.cs" Inherits="frmMasterBarcodePrinting" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table cellpadding="0" cellspacing="0" width="100%">
                <tr>
                    <td style="height: 30px; background-image: url('../App_Themes/Styles/Images/Form_Title.JPG');
                        background-repeat: repeat-x">
                        <asp:Label ID="lblHeader" runat="server" CssClass="HeadingLabel" Text="&amp;nbsp;Master Barcode Re-Printing"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <table>
                            <tr>
                                <td>
                                    <asp:RadioButton ID="RBPrint" runat="Server" Text="Print Master Barcode" AutoPostBack="True"
                                        GroupName="RadioButton" OnCheckedChanged="RBPrint_CheckedChanged" Width="100%" />
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td class="style8" colspan="2">
                                    <asp:RadioButton ID="RBReprint" runat="Server" Text="Re-Print Master Barcode" AutoPostBack="True"
                                        GroupName="RadioButton" OnCheckedChanged="RBReprint_CheckedChanged" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblItemCode14" runat="server" CssClass="Label" Text="Plant Code :"
                                        Width="100%"></asp:Label>
                                </td>
                                <td>
                                </td>
                                <td class="style19" colspan="2">
                                    <asp:DropDownList ID="DD_Plantcode" runat="server" AutoPostBack="True" CssClass="Combobox"
                                        Height="25px" Width="224px" 
                                        onselectedindexchanged="DD_Plantcode_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label5" runat="server" CssClass="Label" Text="Select Module :" Width="100px"></asp:Label>
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td class="style8" colspan="2">
                                    <asp:DropDownList ID="DDModule" Width="220px" Height="28px" runat="server" OnSelectedIndexChanged="DDModule_SelectedIndexChanged"
                                        AutoPostBack="True" Enabled="False">
                                        <asp:ListItem Selected="True">Select</asp:ListItem>
                                        <asp:ListItem Value="VW_CUBICLEMASTER">Cubicle Master</asp:ListItem>
                                        <asp:ListItem Value="VW_MasterWeighing">Weighing Balance</asp:ListItem>
                                        <asp:ListItem Value="VW_PRINTERMASTER">Printer Master</asp:ListItem>
                                       <asp:ListItem Value="VW_BINMASTER">Bin Master</asp:ListItem>
                                      
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblCubicleCode" runat="server" Visible="false" CssClass="Label" Text="Select Cubicle Code :" Width="100px"></asp:Label>
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td class="style8" colspan="2">
                                    <asp:DropDownList ID="ddCubicleCode" Visible="false" Width="220px" 
                                        Height="28px" runat="server" OnSelectedIndexChanged="ddCubicleCode_SelectedIndexChanged"
                                        AutoPostBack="True">
                                       
                                    </asp:DropDownList>
                                </td>
                            </tr>

                            <caption>
                                <br />
                                <tr>
                                    <td colspan="4">
                                        <br />
                                        <div id="grdCharges" runat="server" style="border: 1px dotted #999999; width: 600px;
                                            overflow: auto; height: 200px;">
                                            <asp:GridView ID="GrMasterBarcode" runat="server" AutoGenerateColumns="False" CaptionAlign="Left"
                                                CellPadding="4" CssClass="GridViewStyle" EmptyDataText="There are no items to show in this view."
                                                PageSize="20" Width="100%">
                                                <Columns>
                                                    <asp:TemplateField ControlStyle-Width="2px" HeaderText="Select">
                                                        <ItemTemplate>
                                                            <asp:CheckBox Width="5px" ID="chkSelect" runat="server" AutoPostBack="false" />
                                                        </ItemTemplate>
                                                        <ControlStyle Width="2px" />
                                                        <ItemStyle Width="5px" />
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="CODE" HeaderText="CODE" SortExpression="CODE" >
                                                        <ControlStyle Width="100px" />
                                                        <ItemStyle Width="100px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="DESCRIPTION" HeaderText="DESCRIPTION" SortExpression="DESCRIPTION">
                                                    </asp:BoundField>
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
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <br />
                                        <asp:CheckBox ID="chkAll" runat="server" Text="Select All" AutoPostBack="True" OnCheckedChanged="CheckBox1_CheckedChanged" />
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="style8" colspan="3">
                                        <asp:Label ID="lblReson" runat="server" CssClass="Label" Text="Select Reason:" Width="150px"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="DDReson" runat="server" CssClass="Combobox" Width="250px">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="style8" colspan="3">
                                        <asp:Label ID="Label3" runat="server" CssClass="Label" Text="Select Printer:" Width="150px"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="DDPrinter" runat="server" CssClass="Combobox" Width="250px">
                                            <asp:ListItem>Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="RequiredFieldValidator"
                                            ControlToValidate="DDPrinter" InitialValue="Select" ValidationGroup="VGSave"> <img src="../App_Themes/Styles/Images/err1.png" 
                                                                                          title="Printer Name is required!!!" /></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="style8" colspan="3">
                                        <br />
                                        <asp:Button ID="btnPrint" runat="server" CssClass="Button" Text="Re-Print" ValidationGroup="VGSave"
                                            Width="80px" OnClick="btnPrint_Click" />
                                    </td>
                                    <td class="style8">
                                        <br />
                                        <asp:Button ID="brnCode" runat="server" CssClass="Button" Text="Close" ValidationGroup="Save"
                                            Width="80px" PostBackUrl="~/Modules/frmMain.aspx" />
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="style8" colspan="3">
                                        &nbsp;
                                    </td>
                                    <td class="style5">
                                        &nbsp;
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td class="style8" colspan="3">
                                        &nbsp;
                                    </td>
                                    <td class="style5">
                                        &nbsp;
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                            </caption>
                        </table>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
        <Triggers>
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
