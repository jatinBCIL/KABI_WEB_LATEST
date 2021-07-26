<%@ Page Title="e-Track-Plant Master" Language="C#" MasterPageFile="~/Modules/MasterPage.master" AutoEventWireup="true"
    CodeFile="frmPlantMasters.aspx.cs" Inherits="Masters_frmPlantMasters" %>

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
        .style15
        {
            height: 29px;
        }
        .style22
        {
            width: 87px;
        }
        .style26
        {
            width: 32px;
        }
    </style>
    <script src="Javascript/dw_tooltip_c.js" type="text/javascript"></script>
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
                        <asp:Label ID="Label35" runat="server" CssClass="HeadingLabel" Text="&amp;nbsp;Plant Master"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:MultiView ID="MultiView1" runat="server">
                            <asp:View ID="View2" runat="server">
                                <table cellpadding="0" cellspacing="0" width="100%">
                                    <tr>
                                        <td height="30" style="height: 30px; background-image: url('../App_Themes/Styles/Images/Menu_Out.png');
                                            background-repeat: repeat-x">
                                            <table cellpadding="5">
                                                <tr>
                                                    <td class="style26">
                                                        <asp:ImageButton ID="imgCloseV2" runat="server" Height="24px" ImageUrl="~/App_Themes/Styles/Images/exit.ico"
                                                            ToolTip="Close Page" Width="24px" OnClick="imgCloseV2_Click" PostBackUrl="~/Modules/frmMain.aspx" />
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
                                                    <td class="style15">
                                                        &nbsp;
                                                    </td>
                                                    <td>
                                                        &nbsp;
                                                    </td>
                                                    <td class="style15">
                                                        &nbsp;
                                                    </td>
                                                    <td>
                                                        &nbsp;
                                                    </td>
                                                    <td width="100%">
                                                        &nbsp;
                                                    </td>
                                                    <td>
                                                        &nbsp;
                                                    </td>
                                                </tr>
                                            </table>
                                            <table cellpadding="5">
                                                <tr>
                                                    <td>
                                                        <table width="100%">
                                                            <tr valign="top">
                                                                <td style="width: 35%;">
                                                                    <asp:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0" EnableTheming="True"
                                                                        Height="100%" Width="500px">
                                                                        <asp:TabPanel ID="TabPanel1" runat="server" HeaderText="TabPanel1">
                                                                            <HeaderTemplate>
                                                                                General
                                                                            </HeaderTemplate>
                                                                            <ContentTemplate>
                                                                                <table style="width: 100%">
                                                                                    <tr>
                                                                                        <td colspan="5" style="height: 10px; background-image: url('../App_Themes/Styles/Images/Form_Title.JPG');
                                                                                            background-repeat: repeat-x">
                                                                                            <asp:Label ID="Label5" runat="server" CssClass="HeadingLabel" Font-Size="15px" Text="&amp;nbsp;Import Excel"></asp:Label>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td colspan="5" class="style18">
                                                                                            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                                                                <ContentTemplate>
                                                                                                    <table width="100%">
                                                                                                        <tr>
                                                                                                            <td>
                                                                                                                <asp:Label ID="lblBrowse" runat="server" CssClass="Label" Text="Browse:"></asp:Label>
                                                                                                            </td>
                                                                                                            <td colspan="2">
                                                                                                                <asp:FileUpload ID="FlUpload" runat="server" style="width: 180px;" />
                                                                                                                <asp:RequiredFieldValidator ID="rfqUpload" runat="server" ControlToValidate="FlUpload"
                                                                                                                    ErrorMessage="RequiredFieldValidator" ToolTip="Plant Code is required" ValidationGroup="Upload"><img 
                                                                                                                    src="../App_Themes/Styles/Images/err1.png" title="Plant Code is required!!!" /></asp:RequiredFieldValidator>
                                                                                                            </td>
                                                                
                                                                                                            <td>
                                                                                                                <asp:Button ID="btnImp" runat="server" CssClass="Button" ValidationGroup="Upload"
                                                                                                                    OnClick="btnImp_Click"  Text="Import" Width="50px" />
                                                                                                            </td>
                                                                                                            <td>
                                                                                                             <asp:Button ID="btnTemplate" runat="server" CssClass="Button" style=" margin-left:10px"
                                                                                                        Text="Template" Width="70px" onclick="btnTemplate_Click"  /></td>

                                                                                                        </tr>
                                                                                                    </table>
                                                                                                </ContentTemplate>
                                                                                                <Triggers>
                                                                                                    <asp:PostBackTrigger ControlID="btnImp" />
                                                                                                </Triggers>
                                                                                            </asp:UpdatePanel>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td colspan="5
                                                                                        " style="height: 10px; background-image: url('../App_Themes/Styles/Images/Form_Title.JPG');
                                                                                            background-repeat: repeat-x">
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td>
                                                                                            <asp:Label ID="lblplantID" runat="server" CssClass="Label" Text="Location :"></asp:Label>
                                                                                        </td>
                                                                                        <td>
                                                                                            <asp:TextBox ID="txtLoc" runat="server" CssClass="TextBox" Height="25px" MaxLength="500"
                                                                                                Width="220px"></asp:TextBox>
                                                                                            <asp:Image ID="Image2" runat="server" Height="10px" ImageUrl="../App_Themes/Styles/Images/img_mand.png"
                                                                                                ToolTip="Mandatory Field!!!" Width="10px" />
                                                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtLoc"
                                                                                                ErrorMessage="RequiredFieldValidator" ToolTip="Plant Code is required" ValidationGroup="Save"><img 
                                                                                src="../App_Themes/Styles/Images/err1.png" title="Plant Code is required!!!" /></asp:RequiredFieldValidator>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td>
                                                                                            <asp:Label ID="lblPlatnCode" runat="server" CssClass="Label" Text="Plant Code :"></asp:Label>
                                                                                        </td>
                                                                                        <td>
                                                                                            <asp:TextBox ID="txtPlantCode" runat="server" CssClass="TextBox" Height="25px" MaxLength="5"
                                                                                                SkinID="NAME" Width="220px"></asp:TextBox>
                                                                                            <asp:Image ID="Image1" runat="server" Height="10px" ImageUrl="../App_Themes/Styles/Images/img_mand.png"
                                                                                                ToolTip="Mandatory Field!!!" Width="10px" />
                                                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtPlantCode"
                                                                                                ErrorMessage="RequiredFieldValidator" ToolTip="Plant Location is Required" ValidationGroup="Save"><img 
                                                                                src="../App_Themes/Styles/Images/err1.png" 
                                                                                title="Plant Location is required!!!" /></asp:RequiredFieldValidator>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td>
                                                                                            <asp:Label ID="lblPlantDesc" runat="server" CssClass="Label" Text="Plant Description :"></asp:Label>
                                                                                        </td>
                                                                                        <td>
                                                                                            <asp:TextBox ID="txtPlantDesc" runat="server" CssClass="TextBox" Height="25px" MaxLength="500"
                                                                                                SkinID="NAME" Width="220px"></asp:TextBox>
                                                                                            <asp:Image ID="Image6" runat="server" Height="10px" ImageUrl="../App_Themes/Styles/Images/img_mand.png"
                                                                                                ToolTip="Mandatory Field!!!" Width="10px" />
                                                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtPlantDesc"
                                                                                                ErrorMessage="RequiredFieldValidator" ToolTip="Address is required" ValidationGroup="Save"><img 
                                                                                src="../App_Themes/Styles/Images/err1.png" title="Address is required!!!" /></asp:RequiredFieldValidator>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td>
                                                                                            <asp:Label ID="lblAddress" runat="server" Text="Address Line 1 : "></asp:Label>
                                                                                        </td>
                                                                                        <td>
                                                                                            <asp:TextBox ID="txtAddress1" runat="server" CssClass="TextBox" Height="25px" MaxLength="60"
                                                                                                SkinID="NAME" Width="220px"></asp:TextBox>
                                                                                         
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
                                                                                        <td>
                                                                                            &nbsp;
                                                                                        </td>
                                                                                        <td>
                                                                                            &nbsp;
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td>
                                                                                            <asp:Label ID="lbladdress2" runat="server" CssClass="Label" Text="Address Line 2 : "></asp:Label>
                                                                                            

                                                                                        </td>
                                                                                        <td>
                                                                                            <asp:TextBox ID="txtaddress2" runat="server" CssClass="TextBox" Height="25px" MaxLength="60"
                                                                                                SkinID="NAME" Width="220px"></asp:TextBox>
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
                                                                                        <td>
                                                                                            &nbsp;
                                                                                        </td>
                                                                                        <td>
                                                                                            &nbsp;
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td>
                                                                                            <asp:Label ID="lbladdress3" runat="server" CssClass="Label" Text="Address Line 3 :"></asp:Label>
                                                                                        </td>
                                                                                        <td>
                                                                                            <asp:TextBox ID="txtaddress3" runat="server" CssClass="TextBox" Height="25px" MaxLength="60"
                                                                                                SkinID="NAME" Width="220px"></asp:TextBox>
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
                                                                                        <td>
                                                                                            &nbsp;
                                                                                        </td>
                                                                                        <td>
                                                                                            &nbsp;
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td>
                                                                                            <asp:Label ID="lbladdress4" runat="server" CssClass="Label" Text="Address Line 4 :"></asp:Label>
                                                                                        </td>
                                                                                        <td>
                                                                                            <asp:TextBox ID="txtaddress4" runat="server" CssClass="TextBox" Height="25px" MaxLength="60"
                                                                                                SkinID="NAME" Width="220px"></asp:TextBox>
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
                                                                                        <td>
                                                                                            &nbsp;
                                                                                        </td>
                                                                                        <td>
                                                                                            &nbsp;
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td class="style15">
                                                                                            <asp:Label ID="lblstate" runat="server" CssClass="Label" Text="State :"></asp:Label>
                                                                                        </td>
                                                                                        <td class="style15">
                                                                                            <asp:TextBox ID="txtstate" runat="server" CssClass="TextBox" Height="25px" MaxLength="60"
                                                                                                SkinID="NAME" Width="220px"></asp:TextBox>
                                                                                        </td>
                                                                                        <td class="style15">
                                                                                            &nbsp;
                                                                                        </td>
                                                                                        <td class="style15">
                                                                                            &nbsp;
                                                                                        </td>
                                                                                        <td class="style15">
                                                                                            &nbsp;
                                                                                        </td>
                                                                                        <td class="style15">
                                                                                            &nbsp;
                                                                                        </td>
                                                                                        <td class="style15">
                                                                                            &nbsp;
                                                                                        </td>
                                                                                        <td class="style15">
                                                                                            &nbsp;
                                                                                        </td>
                                                                                        <td class="style15">
                                                                                            &nbsp;
                                                                                        </td>
                                                                                        <td class="style15">
                                                                                            &nbsp;
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td>
                                                                                            <asp:Label ID="lblcity" runat="server" CssClass="Label" Text="City :"></asp:Label>
                                                                                        </td>
                                                                                        <td>
                                                                                            <asp:TextBox ID="txtcity" runat="server" CssClass="TextBox" Height="25px" MaxLength="60"
                                                                                                SkinID="NAME" Width="220px"></asp:TextBox>
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
                                                                                        <td>
                                                                                            &nbsp;
                                                                                        </td>
                                                                                        <td>
                                                                                            &nbsp;
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td>
                                                                                            <asp:Label ID="lblpincode" runat="server" CssClass="Label" Text="Pincode :"></asp:Label>
                                                                                        </td>
                                                                                        <td>
                                                                                            <asp:TextBox ID="txtpincode" runat="server" CssClass="TextBox" Height="25px" MaxLength="60"
                                                                                                SkinID="NAME" Width="220px"></asp:TextBox>
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
                                                                                        <td>
                                                                                            &nbsp;
                                                                                        </td>
                                                                                        <td>
                                                                                            &nbsp;
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td>
                                                                                            <asp:Label ID="lblPlantType" runat="server" CssClass="Label" Text="Plant Type :"></asp:Label>
                                                                                        </td>
                                                                                        <td>
                                                                                            <asp:DropDownList ID="ddlPlantType" runat="server" CssClass="Combobox" Height="31px"
                                                                                                Width="224px">
                                                                                                <asp:ListItem Selected="True">Select</asp:ListItem>
                                                                                                <asp:ListItem>API</asp:ListItem>
                                                                                                <asp:ListItem>FORMULATION</asp:ListItem>
                                                                                            </asp:DropDownList>
                                                                                            <asp:Image ID="Image5" runat="server" Height="10px" ImageUrl="../App_Themes/Styles/Images/img_mand.png"
                                                                                                ToolTip="Mandatory Field!!!" Width="10px" />
                                                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="ddlPlantType"
                                                                                                ErrorMessage="RequiredFieldValidator" InitialValue="Select" ToolTip="Address is required"
                                                                                                ValidationGroup="Save"><img 
                                                                                src="../App_Themes/Styles/Images/err1.png" title="Address is required!!!" /></asp:RequiredFieldValidator>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td>
                                                                                            &nbsp;<asp:Label ID="lblStatus" runat="server" CssClass="lblstatus" Text="Status :"></asp:Label>
                                                                                        </td>
                                                                                        <td>
                                                                                            <asp:RadioButton ID="RBactivate" Checked="True" Text="Activate" runat="server" ValidationGroup="A"
                                                                                                GroupName="RDgrp" />&nbsp;&nbsp;
                                                                                            <asp:RadioButton ID="RBdeactivate" Text="De-Activate" runat="server" ValidationGroup="A"
                                                                                                GroupName="RDgrp" />
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td>
                                                                                            <asp:Label ID="lblRemark" runat="server" Visible="False" CssClass="Label" 
                                                                                                Text="Remark :"></asp:Label>
                                                                                        </td>
                                                                                        <td>
                                                                                            <asp:TextBox ID="txtRemark" runat="server" Visible="False" CssClass="TextBox" Height="25px"
                                                                                                MaxLength="500" SkinID="NAME" Width="220px"></asp:TextBox>
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                                </td> </tr> </table>
                                                                                
                                                                            </ContentTemplate>
                                                                        </asp:TabPanel>
                                                                    </asp:TabContainer>
                                                                </td>
                                                                <td style="width: 65%;">
                                                                <table style="width: 100%;">
                                                                        <tr>
                                                                        <td style="width: 100%; margin-left: 10px; text-align:left" valign="top">
                                                                                <asp:Panel ID="pnlImport" runat="server" ScrollBars="Auto" Height="400px" Style=" width:100%">
                                                                                    <asp:GridView ID="grPlant1" runat="server" AllowPaging="True" AllowSorting="True"
                                                                                        AutoGenerateColumns="False" CaptionAlign="Left" CellPadding="4" CssClass="style22"
                                                                                        Height="16px" PageSize="10" ShowFooter="True" Width="920px" OnPageIndexChanging="grPlant1_PageIndexChanging"
                                                                                        OnRowDataBound="grPlant1_RowDataBound">
                                                                            <Columns>
                                                                                <asp:BoundField DataField="LOCATION" HeaderText="LOCATION" SortExpression="PLANT ID" />
                                                                                <asp:BoundField DataField="PLANTCODE" HeaderText="PLANT CODE" SortExpression="PLANT LOCATION" />
                                                                                <asp:BoundField DataField="PLANTDESC" HeaderText="PLANT DESC" SortExpression="ADDRESS" />
                                                                                <asp:BoundField DataField="ASSOCIATEPLANTCODE" HeaderText="ASC PLANT CODE" SortExpression="ASSOCIATE_PLANT_CODE" />
                                                                                <asp:BoundField DataField="ASSOCIATEPLANTDESC" HeaderText="ASC PLANT DESC" SortExpression="ASSOCIATE_PLANT_DESC" />
                                                                              
                                                                                <asp:BoundField DataField="ADDRESS1" HeaderText="ADDRESS1" SortExpression="ADDRESS" />
                                                                                <asp:BoundField DataField="ADDRESS2" HeaderText="ADDRESS2" SortExpression="HOLD_DAYS" />
                                                                                <asp:BoundField DataField="ADDRESS3" HeaderText="ADDRESS3" SortExpression="PLANT_TYPE" />
                                                                                <asp:BoundField DataField="ADDRESS4" HeaderText="ADDRESS4" SortExpression="STATUS" />
                                                                                <asp:BoundField DataField="STATE" HeaderText="STATE" SortExpression="STATE" />
                                                                                <asp:BoundField DataField="CITY" HeaderText="CITY" SortExpression="CITY" />
                                                                              
                                                                               <asp:BoundField DataField="HOLDDAYS" HeaderText="HOLD DAYS" SortExpression="REMARK" />
                                                                                    <asp:BoundField DataField="PLANTTYPE" HeaderText="PLANT TYPE" SortExpression="REMARK" />
                                                                                    <asp:BoundField DataField="STATUS" HeaderText="STATUS" SortExpression="STATUS" />
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
                                                                        <asp:Button ID="btnSave" runat="server" CssClass="Button" OnClick="btnSave_Click"
                                                                            Text="Save" Width="60px" />
                                                                        &nbsp;&nbsp;
                                                                        <asp:Button ID="btnCancel" runat="server" CssClass="Button" Text="Cancel" Width="60px"
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
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </asp:View>
                            <asp:View ID="View1" runat="server">
                                <table cellpadding="0" cellspacing="0" width="100%">
                                    <tr>
                                        <td height="30" style="height: 30px; background-image: url('../App_Themes/Styles/Images/Menu_Out.png');
                                            background-repeat: repeat-x">
                                            <table cellpadding="5">
                                                <tr>
                                                    <td>
                                                        <asp:ImageButton ID="imgCloseV1" runat="server" Height="24px" ImageUrl="~/App_Themes/Styles/Images/exit.ico"
                                                            ToolTip="Close Page" Width="24px" OnClick="imgCloseV1_Click" />
                                                    </td>
                                                    <td>
                                                        <asp:ImageButton ID="imgAdd1" runat="server" Height="24px" ImageUrl="~/App_Themes/Styles/Images/window_new.png"
                                                            ToolTip="Add Transaction" Width="24px" OnClick="imgAdd1_Click1" />
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
                                                    <td>
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
                                                        <asp:TextBox ID="txtSearch" runat="server" CssClass="TextBox" ValidationGroup="Search"></asp:TextBox>
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
                                    <tr>
                                        <td style="height: 30px;">
                                            <asp:GridView ID="GrPlant" runat="server" AllowPaging="True" AllowSorting="True"
                                                AutoGenerateColumns="False" CaptionAlign="Left" CellPadding="4" CssClass="GridViewStyle"
                                                EmptyDataText="There are no items to show in this view." PageSize="20" ShowFooter="True"
                                                Width="100%" OnRowCommand="GrPlant_RowCommand" DataKeyNames="REFNO" OnPageIndexChanging="GrPlant_PageIndexChanging"
                                                OnRowDataBound="GrPlant_RowDataBound" 
                                                onselectedindexchanged="GrPlant_SelectedIndexChanged">
                                                <Columns>
                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="LinkButton1" CommandName="Select" Text="Select" runat="server" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="PLANTCODE" HeaderText="PLANT CODE" SortExpression="PLANT LOCATION" />
                                                    <asp:BoundField DataField="LOCATION" HeaderText="LOCATION" SortExpression="PLANT ID" />
                                                    <asp:BoundField DataField="PLANTDESC" HeaderText="PLANT DESC" SortExpression="ADDRESS" />
                                                <asp:BoundField DataField="ASSOCIATEPLANTCODE" HeaderText="ASC PLANT CODE" SortExpression="ASSOCIATE_PLANT_CODE" />
                                                    <asp:BoundField DataField="ASSOCIATEPLANTDESC" HeaderText="ASC PLANT DESC" SortExpression="ASSOCIATE_PLANT_DESC" />
                                                    <asp:BoundField DataField="ADDRESS1" HeaderText="ADDRESS1" SortExpression="ADDRESS1" />
                                                    <asp:BoundField DataField="ADDRESS2" HeaderText="ADDRESS2" SortExpression="ADDRESS2" />
                                                    <asp:BoundField DataField="ADDRESS3" HeaderText="ADDRESS3" SortExpression="ADDRESS3" />
                                                    <asp:BoundField DataField="ADDRESS4" HeaderText="ADDRESS4" SortExpression="ADDRESS4" />
                                                    <asp:BoundField DataField="STATE" HeaderText="STATE" SortExpression="STATE" />
                                                    <asp:BoundField DataField="CITY" HeaderText="CITY" SortExpression="CITY" />
                                                    <%--<asp:BoundField DataField="ADDRESS7" HeaderText="ADDRESS7" SortExpression="PINCODE" />--%>
                                                 <asp:BoundField DataField="HOLDDAYS" HeaderText="HOLD_DAYS" SortExpression="HOLD_DAYS" />
                                                    <asp:BoundField DataField="PLANTTYPE" HeaderText="PLANT TYPE" SortExpression="PLANT_TYPE" />
                                                    <asp:BoundField DataField="STATUS" HeaderText="STATUS" SortExpression="STATUS" />
                                                    <asp:BoundField DataField="REMARK" HeaderText="REMARK" SortExpression="REMARK" />
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
            <asp:PostBackTrigger ControlID="imgExport" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
