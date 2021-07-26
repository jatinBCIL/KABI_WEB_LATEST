<%@ Page Title="" Language="C#" MasterPageFile="~/Modules/MasterPage.master" AutoEventWireup="true" CodeFile="frmStandardWT_Master.aspx.cs" Inherits="frmStandardWT_Master" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="../App_Themes/Styles/Style_Controls.css" rel="stylesheet" type="text/css" />
    <link href="../App_Themes/Styles/Style_Design.css" rel="stylesheet" type="text/css" />
    <link href="../App_Themes/Styles/Style_Grid.css" rel="stylesheet" type="text/css" />
    <link href="../App_Themes/Styles/Style_PopUp.css" rel="stylesheet" type="text/css" />
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
        .style19
        {
            height: 24px;
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
            text-align: left;
            font-family: calibri;
            font-style: normal;
            font-size: 14px;
            color: #000000;
            border: 1px solid black;
        }
        .style24
        {
            width: 485px;
            height: 200px;
        }
        .style26
        {
            width: 103%;
        }
        .style28
        {
            width: 199px;
        }
        .style29
        {
            height: 464px;
        }
        .style30
        {
            width: 199px;
            height: 24px;
        }
    </style>
    <script src="../Javascript/dw_tooltip_c.js" type="text/javascript"></script>
    <script type="text/javascript">

        dw_Tooltip.defaultProps = {
            //supportTouch: true, // set false by default
            hoverable: true
        }

        dw_Tooltip.content_vars = {
            L1: 'Field Name : Test' + '\n' + 'Field Name : Test',
            L3: 'Field Name : Test',
            L4: 'Field Name : Test',
            L5: 'Field Name : Test',
            L6: 'test'
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table cellpadding="0" cellspacing="0" width="100%">
                <tr>
                    <td style="height: 30px; background-image: url('../App_Themes/Styles/Images/Form_Title.JPG');
                        background-repeat: repeat-x">
                        <asp:Label ID="Label35" runat="server" CssClass="HeadingLabel" Text="&amp;nbsp;Standard Weight Master"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:MultiView ID="MultiView1" runat="server">
                            <asp:View ID="View2" runat="server">
                                <table cellpadding="0" cellspacing="0" class="style26">
                                    <tr>
                                        <td height="30" style="height: 30px; background-image: url('../App_Themes/Styles/Images/Menu_Out.png');
                                            background-repeat: repeat-x">
                                            <table cellpadding="0" cellspacing="0" width="100%">
                                                <tr>
                                                    <td height="30" style="height: 30px; background-image: url('../App_Themes/Styles/Images/Menu_Out.png');
                                                        background-repeat: repeat-x">
                                                        <table cellpadding="5">
                                                            <tr>
                                                                <td>
                                                                    <asp:ImageButton ID="imgCloseV1" runat="server" Height="24px" ImageUrl="~/App_Themes/Styles/Images/exit.ico"
                                                                        ToolTip="Close Page" Width="24px" PostBackUrl="~/Modules/frmMain.aspx" OnClick="imgCloseV1_Click" />
                                                                </td>
                                                                <td>
                                                                    <asp:ImageButton ID="imgSave" runat="server" Height="24px" ImageUrl="~/App_Themes/Styles/Images/media_floppy_green.jpg"
                                                                        ToolTip="Save Transaction" Width="24px" ValidationGroup="Save" OnClick="imgSave_Click" />
                                                                </td>
                                                                <td>
                                                                    <asp:ImageButton ID="imgDetails" runat="server" Width="27px" Height="27px" ImageUrl="~/App_Themes/Styles/Images/imgDtl.jpg"
                                                                        ToolTip="Click For Details" OnClick="imgDetails_Click" />
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
                                                                <td>
                                                                    &nbsp;
                                                                </td>
                                                                <td>
                                                                    &nbsp;
                                                                </td>
                                                                <td width="100%">
                                                                    &nbsp;
                                                                </td>
                                                                <td>
                                                                </td>
                                                            </tr>
                                                    </td>
                                                </tr>
                                            </table>
                                            <table cellpadding="5">
                                                <tr>
                                                    <td class="style29">
                                                        <table width="100%">
                                                            <tr valign="top">
                                                                <td style="width: 50%;">
                                                                    <asp:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0" EnableTheming="True"
                                                                        Height="500px" Width="500px">
                                                                        <asp:TabPanel ID="TabPanel1" runat="server" HeaderText="TabPanel1">
                                                                            <HeaderTemplate>
                                                                                General
                                                                            </HeaderTemplate>
                                                                            <ContentTemplate>
                                                                                <table class="style26">
                                                                                    <tr>
                                                                                        <td colspan="5" style="height: 10px; background-image: url('../App_Themes/Styles/Images/Form_Title.JPG');
                                                                                background-repeat: repeat-x; ">
                                                                                <asp:Label ID="Label22" runat="server" CssClass="HeadingLabel" Font-Size="15px" Text="&amp;nbsp;Import Excel"></asp:Label>
                                                                            </td>
                                                                                    </tr>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td colspan="3" class="style18">
                                                                                            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                                                                <ContentTemplate>
                                                                                                    <table>
                                                                                                        <tr>
                                                                                                            <td class="style21">
                                                                                                                &nbsp;&nbsp;
                                                                                                            </td>
                                                                                                            <td class="style18">
                                                                                                                <asp:Label ID="lblBrowse" runat="server" CssClass="Label" Text="Browse:"></asp:Label>
                                                                                                            </td>
                                                                                                            <td class="style18">
                                                                                                                <asp:FileUpload ID="FlUpload" runat="server" ForeColor="#009900" />
                                                                                                            </td>
                                                                                                            
                                                                                                            <td class="style21">
                                                                                                                <asp:Button ID="btnImp" runat="server" CssClass="Button" Text="Import" Width="60px"
                                                                                                                    OnClick="btnImp_Click" />
                                                                                                            </td>
                                                                                                             <td class="style21">
                                                                                                                <asp:Button ID="btnTemplate" runat="server" CssClass="Button" Style="margin-left: 10px"
                                                                                                                    Text="Template" Width="70px" OnClick="btnTemplate_Click" />
                                                                                                            </td>
                                                                                                    </table>
                                                                                                </ContentTemplate>
                                                                                                <Triggers>
                                                                                                    <asp:PostBackTrigger ControlID="btnImp" />
                                                                                                </Triggers>
                                                                                            </asp:UpdatePanel>
                                                                                        </td>
                                                                                        <tr>
                                                                                            <td colspan="5" style="height: 10px; background-image: url('../App_Themes/Styles/Images/Form_Title.JPG');
                                                                                background-repeat: repeat-x; ">
                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td class="style19">
                                                                                                <asp:Label ID="lblItemCode14" runat="server" CssClass="Label" Text="Plant Code :"></asp:Label>
                                                                                            </td>
                                                                                            <td class="style19">
                                                                                                <asp:DropDownList ID="DD_Plantcode" runat="server" AutoPostBack="True" CssClass="Combobox"
                                                                                                    Height="25px" Width="224px" 
                                                                                                    onselectedindexchanged="DD_Plantcode_SelectedIndexChanged">
                                                                                                </asp:DropDownList>
                                                                                                <td>
                                                                                                    <asp:Image ID="img_Str" runat="server" Height="10px" ImageUrl="~/App_Themes/Styles/Images/img_mand.png"
                                                                                                        title="Plant Name is required!!!" ToolTip="Mandatory Field!!!" Width="10px" />
                                                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" InitialValue="Select" runat="server"
                                                                                                        ControlToValidate="DD_Plantcode" ErrorMessage="RequiredFieldValidator" ToolTip="Plz select plant code is required!!!"
                                                                                                        ValidationGroup="Save"><img src="../App_Themes/Styles/Images/err1.png" 
                                                                                title="Plant Name is required!!!" /></asp:RequiredFieldValidator>
                                                                                                </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td>
                                                                                                <asp:Label ID="Label6" runat="server" CssClass="Label" Text="Plant Description :"></asp:Label>
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:TextBox ID="TxtPlantDESC" runat="server" CssClass="TextBox" Height="25px" MaxLength="500"
                                                                                                    SkinID="NAME" Width="223px" Enabled="False"></asp:TextBox>
                                                                                            </td>
                                                                                            <td>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td>
                                                                                                <asp:Label ID="lblItemCode13" runat="server" CssClass="Label" Text="Standard Weight No:"></asp:Label>
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:TextBox ID="txtStandardWT_No" runat="server" CssClass="TextBox" Height="25px"
                                                                                                    MaxLength="500" SkinID="NAME" Width="223px"></asp:TextBox>
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:Image ID="img_Str0" runat="server" Height="10px" ImageUrl="~/App_Themes/Styles/Images/img_mand.png"
                                                                                                    title="StandardWT_No is required!!!" ToolTip="Mandatory Field!!!" Width="10px" />
                                                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtStandardWT_No"
                                                                                                    ErrorMessage="RequiredFieldValidator" ToolTip="StandardWT_No is required!!!"
                                                                                                    ValidationGroup="Save"><img src="../App_Themes/Styles/Images/err1.png" 
                                                                                title="StandardWT_No is required!!!" /></asp:RequiredFieldValidator>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td>
                                                                                                <asp:Label ID="Label1" runat="server" CssClass="Label" Text="Department :"></asp:Label>
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:DropDownList ID="ddDepartment" runat="server" AutoPostBack="True" CssClass="Combobox"
                                                                                                    Height="31px" Width="230px">
                                                                                                    <asp:ListItem Value="0">Select</asp:ListItem>
                                                                                                </asp:DropDownList>
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:Image ID="Image3" runat="server" Height="10px" ImageUrl="~/App_Themes/Styles/Images/img_mand.png"
                                                                                                    title="Department ID is required!!!" ToolTip="Mandatory Field!!!" Width="10px" />
                                                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" InitialValue="Select" runat="server"
                                                                                                    ControlToValidate="ddDepartment" ErrorMessage="RequiredFieldValidator" ToolTip="Department ID is required!!!"
                                                                                                    ValidationGroup="Save"><img 
                                                                                    src="../App_Themes/Styles/Images/err1.png" title="Department ID is required!!!" /></asp:RequiredFieldValidator>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td>
                                                                                                <asp:Label ID="Label4" runat="server" CssClass="Label" Text="Capacity :"></asp:Label>
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:TextBox ID="txtCapacity" runat="server" CssClass="TextBox" Height="25px" MaxLength="500"
                                                                                                    onkeypress="return isDecimal(event)" SkinID="NAME" Width="150px"></asp:TextBox>
                                                                                                <asp:DropDownList ID="ddUnit" runat="server" Height="30px" Width="70px">
                                                                                                    <asp:ListItem Selected="True">Select</asp:ListItem>
                                                                                                    <asp:ListItem Value="KG">KG</asp:ListItem>
                                                                                                    <asp:ListItem Value="G">G</asp:ListItem>
                                                                                                    <asp:ListItem Value="MG">MG</asp:ListItem>
                                                                                                    <asp:ListItem Value="LTR">LTR</asp:ListItem>
                                                                                                    <asp:ListItem Value="ML">ML</asp:ListItem>
                                                                                                </asp:DropDownList>
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:Image ID="Image4" runat="server" Height="10px" ImageUrl="~/App_Themes/Styles/Images/img_mand.png"
                                                                                                    title="Capacity is required!!!" ToolTip="Mandatory Field!!!" Width="10px" />
                                                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator8" InitialValue="Select" runat="server"
                                                                                                    ControlToValidate="txtCapacity" ErrorMessage="RequiredFieldValidator" ToolTip="Capacity is required!!!"
                                                                                                    ValidationGroup="Save"><img 
                                                                                    src="../App_Themes/Styles/Images/err1.png" title="Capacity is required!!!" /></asp:RequiredFieldValidator>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td class="style28">
                                                                                                <asp:Label ID="Label36" runat="server" CssClass="Label" Text="Stamping Certificate No :"></asp:Label>
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:TextBox ID="txtStamp_Cert_No" runat="server" CssClass="TextBox" Height="25px"
                                                                                                    MaxLength="500" SkinID="NAME" Width="223px"></asp:TextBox>
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:Image ID="img_Str2" runat="server" Height="10px" ImageUrl="~/App_Themes/Styles/Images/img_mand.png"
                                                                                                    title="Printer Port is required!!!" ToolTip="Mandatory Field!!!" Width="10px" />
                                                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtStamp_Cert_No"
                                                                                                    ErrorMessage="RequiredFieldValidator" ToolTip="Stamping Certificate No is required!!!"
                                                                                                    ValidationGroup="Save"><img 
                                                                                    src="../App_Themes/Styles/Images/err1.png" title="Stamping Certificate No is required!!!" /></asp:RequiredFieldValidator>
                                                                                            </td>
                                                                                            <tr>
                                                                                                <td class="style28">
                                                                                                    <asp:Label ID="Label38" runat="server" CssClass="Label" Text="Stamping Done On :"></asp:Label>
                                                                                                </td>
                                                                                                <td>
                                                                                                    <asp:TextBox ID="txtStamp_DONE" runat="server" CssClass="TextBox" Height="25px" MaxLength="50"
                                                                                                        Width="223px"></asp:TextBox>
                                                                                                    <asp:CalendarExtender ID="CalendarExtender2" runat="server" Enabled="True" TargetControlID="txtStamp_DONE"
                                                                                                        Format="dd/MM/yyyy">
                                                                                                    </asp:CalendarExtender>
                                                                                                </td>
                                                                                                <td>
                                                                                                    <asp:Image ID="img_Str4" runat="server" Height="10px" ImageUrl="~/App_Themes/Styles/Images/img_mand.png"
                                                                                                        title="Cubicle is required!!!" ToolTip="Mandatory Field!!!" Width="10px" />
                                                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtStamp_DONE"
                                                                                                        ErrorMessage="RequiredFieldValidator" ToolTip="Stamping Done ON is required!!!"
                                                                                                        ValidationGroup="Save"><img 
                                                                                    src="../App_Themes/Styles/Images/err1.png" title="Stamping Done ON is required!!!" /></asp:RequiredFieldValidator>
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td class="style28">
                                                                                                    <asp:Label ID="Label3" runat="server" CssClass="Label" Text="Stamping Due On :"></asp:Label>
                                                                                                </td>
                                                                                                <td>
                                                                                                    <asp:TextBox ID="TxtStamp_Due_ON" runat="server" CssClass="TextBox" Height="25px"
                                                                                                        MaxLength="50" Width="223px"></asp:TextBox>
                                                                                                    <asp:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="True" TargetControlID="TxtStamp_Due_ON"
                                                                                                        Format="dd/MM/yyyy">
                                                                                                    </asp:CalendarExtender>
                                                                                                </td>
                                                                                                <td>
                                                                                                    <asp:Image ID="Image2" runat="server" Height="10px" ImageUrl="~/App_Themes/Styles/Images/img_mand.png"
                                                                                                        title="Cubicle is required!!!" ToolTip="Mandatory Field!!!" Width="10px" />
                                                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="TxtStamp_Due_ON"
                                                                                                        ErrorMessage="RequiredFieldValidator" ToolTip="Stamping Done ON is required!!!"
                                                                                                        ValidationGroup="Save"><img 
                                                                                    src="../App_Themes/Styles/Images/err1.png" title="Stamping Due ON is required!!!" /></asp:RequiredFieldValidator>
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td class="style30">
                                                                                                    <asp:Label ID="Label2" runat="server" CssClass="Label" Text="Area :"></asp:Label>
                                                                                                </td>
                                                                                                <td class="style19">
                                                                                                    <asp:DropDownList ID="ddlArea" runat="server" AutoPostBack="True" CssClass="Combobox"
                                                                                                        Height="31px" Width="230px" OnSelectedIndexChanged="ddlArea_SelectedIndexChanged">
                                                                                                        <asp:ListItem Value="0">Select</asp:ListItem>
                                                                                                    </asp:DropDownList>
                                                                                                </td>
                                                                                                <td class="style19">
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td>
                                                                                                    <asp:Label ID="Label8" runat="server" CssClass="Label" Text="Area  Description :"></asp:Label>
                                                                                                </td>
                                                                                                <td>
                                                                                                    <asp:TextBox ID="TxtAreaDesc" runat="server" CssClass="TextBox" Height="25px" MaxLength="500"
                                                                                                        SkinID="NAME" Width="223px" Enabled="False" ReadOnly="True"></asp:TextBox>
                                                                                                </td>
                                                                                                <td>
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td>
                                                                                                    <asp:Label ID="Re" runat="server" CssClass="Label" Text="Remark :"></asp:Label>
                                                                                                </td>
                                                                                                <td>
                                                                                                    <asp:TextBox ID="txtRemark" runat="server" CssClass="TextBox" Height="25px" MaxLength="500"
                                                                                                        SkinID="NAME" Width="223px"></asp:TextBox>
                                                                                                </td>
                                                                                                <td>
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td class="style28">
                                                                                                    &nbsp;<asp:Label ID="Label37" runat="server" CssClass="Label" Text="Status :"></asp:Label>
                                                                                                    &nbsp;&nbsp;
                                                                                                </td>
                                                                                                <td>
                                                                                                    <asp:RadioButton ID="RBactivate" Checked="True" Text="Activate" runat="server" ValidationGroup="A"
                                                                                                        GroupName="RDgrp" CssClass="OptionButton" />&nbsp;&nbsp;
                                                                                                    <asp:RadioButton ID="RBdeactivate" Text="De-Activate" runat="server" ValidationGroup="A"
                                                                                                        GroupName="RDgrp" CssClass="OptionButton" />
                                                                                                </td>
                                                                                                <td>
                                                                                                </td>
                                                                                            </tr>
                                                                                        </tr>
                                                                                    </tr>
                                                                                </table>
                                                                            </ContentTemplate>
                                                                        </asp:TabPanel>
                                                                    </asp:TabContainer>
                                                                </td>
                                                                <td style="width: 50%">
                                                                    <asp:Panel ID="pnlImport" runat="server" Height="400px" ScrollBars="Auto">
                                                                        <asp:GridView ID="GrStandard_Grid" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                                                                            CaptionAlign="Left" CellPadding="4" CssClass="style22" EmptyDataText="There are no items to show in this view."
                                                                            ShowFooter="True" Width="800px" OnPageIndexChanging="GrStandard_Grid_PageIndexChanging"
                                                                            OnRowDataBound="GrStandard_Grid_RowDataBound" AllowPaging="True">
                                                                            <Columns>
                                                                               <asp:BoundField DataField="PLANTCODE" HeaderText="PLANT CODE" SortExpression="PLANTCODE" />
                                                                            <asp:BoundField DataField="STANDARD_WT_NO" HeaderText="STANDARD WT NO" SortExpression="STANDARD_WT_NO" />
                                                                            <asp:BoundField DataField="DEPARTMENT" HeaderText="DEPARTMENT CODE" SortExpression="DEPARTMENT" />
                                                                            <asp:BoundField DataField="UOM" HeaderText="UOM" SortExpression="UOM" />
                                                                            <asp:BoundField DataField="CAPACITY" HeaderText="CAPACITY" SortExpression="CAPACITY" />
                                                                            <asp:BoundField DataField="STAMPING_CERTIFICATE_NO" HeaderText="STAMPING CERTIFICATE NO"
                                                                                SortExpression="STAMPING_CERTIFICATE_NO" />
                                                                            <asp:BoundField DataField="STAMPING_DONE_ON" HeaderText="STAMPING DONE ON" SortExpression="STAMPING_DONE_ON" />
                                                                            <asp:BoundField DataField="STAMPING_DUE_ON" HeaderText="STAMPING DUE ON" SortExpression="STAMPING_DUE_ON" />
                                                                            <asp:BoundField DataField="AREA" HeaderText="AREA" SortExpression="AREA" />
                                                                            <asp:BoundField DataField="STATUS" HeaderText="STATUS" SortExpression="Status" />
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
                                                                        <br />
                                                                        <asp:Button ID="btnSave" runat="server" CssClass="Button" Text="Save" Width="100px"
                                                                            OnClick="btnSave_Click" />
                                                                        &nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;
                                                                        <asp:Button ID="btnCancel" runat="server" CssClass="Button" Text="Cancel" Width="100px"
                                                                            OnClick="btnCancel_Click" />
                                                                    </asp:Panel>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="height: 30px;">
                                            &nbsp;
                                        </td>
                                    </tr>
                                </table>
                            </asp:View>
                            <asp:View ID="View1" runat="server">
                                <table cellpadding="0" cellspacing="0" width="100%">
                                    <tr>
                                        <td height="30" style="height: 30px; background-image: url('../App_Themes/Styles/Images/Menu_Out.png');
                                            background-repeat: repeat-x">
                                            <table cellpadding="0" cellspacing="0" width="100%">
                                                <tr>
                                                    <td height="30" style="height: 30px; background-image: url('../App_Themes/Styles/Images/Menu_Out.png');
                                                        background-repeat: repeat-x">
                                                        <table cellpadding="5">
                                                            <tr>
                                                                <td>
                                                                    <asp:ImageButton ID="imgCLose" runat="server" Height="24px" ImageUrl="~/App_Themes/Styles/Images/exit.ico"
                                                                        ToolTip="Close Page" Width="24px" OnClick="imgCLose_Click" />
                                                                </td>
                                                                <td>
                                                                    <asp:ImageButton ID="imgAdd1" runat="server" Height="24px" ImageUrl="~/App_Themes/Styles/Images/window_new.png"
                                                                        ToolTip="Add Transaction" Width="24px" OnClick="imgAdd1_Click" />
                                                                </td>
                                                                <td>
                                                                    <asp:ImageButton ID="imgExport" runat="server" Height="24px" ImageUrl="~/App_Themes/Styles/Images/excel.png"
                                                                        ToolTip="Download data in excel format" Width="24px" OnClick="imgExport_Click" />
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
                                                                <td>
                                                                    &nbsp;
                                                                </td>
                                                                <td>
                                                                    &nbsp;
                                                                </td>
                                                                <td width="100%">
                                                                    &nbsp;
                                                                </td>
                                                                <td>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td height="30" style="height: 30px;">
                                                        <table>
                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="lblSearch" runat="server" CssClass="Label" Text="Search Criteria"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:DropDownList ID="cboSearch" runat="server" CssClass="Combobox">
                                                                        <asp:ListItem Selected="True" Value="PLANTCODE">Plant Code</asp:ListItem>
                                                                        <asp:ListItem Value="STANDARD_WT_NO">Standard Weight No</asp:ListItem>
                                                                        <asp:ListItem Value="DEPARTMENT">Department</asp:ListItem>
                                                                        <%-- <asp:ListItem Value="AREA">Area</asp:ListItem>
                                                                        <asp:ListItem Value="CUBICLE_CODE">Cubicle</asp:ListItem>--%>
                                                                    </asp:DropDownList>
                                                                </td>
                                                                <td>
                                                                    <asp:DropDownList ID="cboCriteria" runat="server" CssClass="Combobox" Width="120px">
                                                                        <asp:ListItem Selected="True" Value="Equal to">Equal to</asp:ListItem>
                                                                        <asp:ListItem Value="Not equal to">Not equal to</asp:ListItem>
                                                                        <asp:ListItem Value="Contains">Contains</asp:ListItem>
                                                                        <asp:ListItem Value="Start with">Start with</asp:ListItem>
                                                                        <asp:ListItem Value="End with">End with</asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtSearch" runat="server" CssClass="TextBox" ValidationGroup="Search"
                                                                        Height="20px"></asp:TextBox>
                                                                </td>
                                                                <td>
                                                                    <asp:ImageButton ID="imgSearch" runat="server" ImageUrl="~/App_Themes/Styles/Images/search.png"
                                                                        OnClick="imgSearch_Click" />
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
                                            <table cellpadding="5" width="100%">
                                                <tr>
                                                    <td style="width: 100%;">
                                                        <table width="100%">
                                                            <tr valign="top">
                                                                <td style="width: 100%">
                                                                    <asp:GridView ID="GrStandardDTL" runat="server" AllowPaging="True" AllowSorting="True"
                                                                        AutoGenerateColumns="False" CaptionAlign="Left" CellPadding="4" CssClass="style22"
                                                                        EmptyDataText="There are no items to show in this view." DataKeyNames="REFNO"
                                                                        Width="100%" PageSize="20" ShowFooter="True" Height="16px" OnRowDataBound="GrStandardDTL_RowDataBound"
                                                                        OnPageIndexChanging="GrStandardDTL_PageIndexChanging" OnRowCommand="GrStandardDTL_RowCommand">
                                                                        <Columns>
                                                                            <asp:TemplateField>
                                                                                <ItemTemplate>
                                                                                    <asp:LinkButton ID="LinkButton1" CommandName="Select" Text="Select" runat="server" />
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:BoundField DataField="PLANTCODE" HeaderText="PLANT CODE" SortExpression="PLANTCODE" />
                                                                            <asp:BoundField DataField="STANDARD_WT_NO" HeaderText="STANDARD WT NO" SortExpression="STANDARD_WT_NO" />
                                                                            <asp:BoundField DataField="DEPARTMENT" HeaderText="DEPARTMENT CODE" SortExpression="DEPARTMENT" />
                                                                            <asp:BoundField DataField="CAPACITY" HeaderText="CAPACITY" SortExpression="CAPACITY" />
                                                                            <asp:BoundField DataField="STAMPING_CERTIFICATE_NO" HeaderText="STAMPING CERTIFICATE NO"
                                                                                SortExpression="STAMPING_CERTIFICATE_NO" />
                                                                            <asp:BoundField DataField="STAMPING_DONE_ON" HeaderText="STAMPING DONE ON" SortExpression="STAMPING_DONE_ON" />
                                                                            <asp:BoundField DataField="STAMPING_DUE_ON" HeaderText="STAMPING DUE ON" SortExpression="STAMPING_DUE_ON" />
                                                                            <asp:BoundField DataField="AREA" HeaderText="AREA" SortExpression="AREA" />
                                                                            <asp:BoundField DataField="STATUS" HeaderText="STATUS" SortExpression="Status" />
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
                                                                    &nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;
                                                                </td>
                                                            </tr>
                                                        </table>
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
            </td> </tr> </table> </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="imgExport" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>


