<%@ Page Title="" Language="C#" MasterPageFile="~/Modules/MasterPage.master" AutoEventWireup="true" CodeFile="Masters_frmCubical.aspx.cs" Inherits="Masters_frmCubical" %>

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
        .style22
        {
            width: 421px;
            height: 179px;
        }
        .style24
        {
            width: 55%;
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
                        <asp:Label ID="Label35" runat="server" CssClass="HeadingLabel" Text="&amp;nbsp;Cubicle/Area Master"></asp:Label>
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
                                            <table cellpadding="0" cellspacing="0" width="100%">
                                                <tr>
                                                    <td height="30" style="height: 30px; background-image: url('../App_Themes/Styles/Images/Menu_Out.png');
                                                        background-repeat: repeat-x">
                                                        <table cellpadding="5">
                                                            <tr>
                                                                <td class="style26">
                                                                    <asp:ImageButton ID="imgCloseV2" runat="server" Height="24px" ImageUrl="~/App_Themes/Styles/Images/exit.ico"
                                                                        ToolTip="Close Page" Width="24px" PostBackUrl="~/Modules/frmMain.aspx" />
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
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <%--<td height="30" style="height: 30px; background-image: url('../App_Themes/Styles/Images/Menu_Out.png');
                                                        background-repeat: repeat-x;">
                                                        <table>
                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="lblSearch" runat="server" CssClass="Label" Text="Search Criteria"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:DropDownList ID="cboSearch" runat="server" CssClass="Combobox">
                                                                        <asp:ListItem Selected="True" Value="PLANT_ID">Plant ID</asp:ListItem>
                                                                        <asp:ListItem Value="DEPARTMENT_ID">DEPARTMENT ID</asp:ListItem>
                                                                        <asp:ListItem Value="DEPARTMENT_NAME">DEPARTMENT NAME</asp:ListItem>
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
                                                                    <asp:ImageButton ID="imgSearch" runat="server" ImageUrl="~/App_Themes/Styles/Images/search.png" />
                                                                </td>
                                                                <td>
                                                                    &nbsp;
                                                                </td>
                                                                <td>
                                                                    &nbsp;
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>--%>
                                                </tr>
                                            </table>
                                            <table cellpadding="5">
                                                <tr>
                                                    <td>
                                                        <table width="100%">
                                                            <tr valign="top">
                                                                <td class="style24" style="width: 50%">
                                                                    <asp:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0" EnableTheming="True"
                                                                        Height="550px" Width="600px">
                                                                        <asp:TabPanel ID="TabPanel1" runat="server" HeaderText="TabPanel1">
                                                                            <HeaderTemplate>
                                                                                General
                                                                            </HeaderTemplate>
                                                                            <ContentTemplate>
                                                                                <table width="100%">
                                                                                    <tr>
                                                                                        <td style="height: 10px; background-image: url('../App_Themes/Styles/Images/Form_Title.JPG');
                                                                                            background-repeat: repeat-x">
                                                                                            <asp:Label ID="Label5" runat="server" CssClass="HeadingLabel" Font-Size="15px" Text="&amp;nbsp;Import Excel"></asp:Label>
                                                                                        </td>
                                                                                    </tr>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td class="style18">
                                                                                            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                                                                <ContentTemplate>
                                                                                                    <table>
                                                                                                        <tr>
                                                                                                            <td>
                                                                                                                <asp:Label ID="lblBrowse" runat="server" CssClass="Label" Text="Browse:"></asp:Label>
                                                                                                            </td>
                                                                                                            <td>
                                                                                                                <asp:FileUpload ID="FlUpload" runat="server" ForeColor="#009900" />
                                                                                                            </td>
                                                                                                            <td>
                                                                                                                &nbsp;
                                                                                                            </td>
                                                                                                            <td>
                                                                                                                <asp:Button ID="btnImp" runat="server" CssClass="Button" Text="Import" Width="60px"
                                                                                                                    OnClick="btnImp_Click" />
                                                                                                            </td>
                                                                                                           <td><asp:Button ID="btnTemplate" runat="server" CssClass="Button" Style="margin-left: 10px"
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
                                                                                            <td style="height: 10px; background-image: url('../App_Themes/Styles/Images/Form_Title.JPG');
                                                                                                background-repeat: repeat-x">
                                                                                            </td>
                                                                                        </tr>
                                                                                </table>
                                                                                <table>
                                                                                    <tr>
                                                                                        <td>
                                                                                            <asp:Label ID="lblItemCode14" runat="server" CssClass="Label" Text="Plant Code :"></asp:Label>
                                                                                        </td>
                                                                                        <td>
                                                                                            <asp:DropDownList ID="ddlPlantCode" runat="server" AutoPostBack="True" CssClass="Combobox"
                                                                                                Width="210px" OnSelectedIndexChanged="ddlPlantCode_SelectedIndexChanged">
                                                                                                <asp:ListItem Selected="True">Select</asp:ListItem>
                                                                                            </asp:DropDownList>
                                                                                        </td>
                                                                                        <td>
                                                                                            <asp:Image ID="img_Str" runat="server" Height="10px" ImageUrl="~/App_Themes/Styles/Images/img_mand.png"
                                                                                                title="Plant Name is required!!!" ToolTip="Mandatory Field!!!" Width="10px" />
                                                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlPlantCode"
                                                                                                ErrorMessage="RequiredFieldValidator" InitialValue="Select" ToolTip="Plz select plant code is required!!!"
                                                                                                ValidationGroup="Save"><img src="../App_Themes/Styles/Images/err1.png" 
                                                                                title="Plant Name is required!!!" /></asp:RequiredFieldValidator>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td>
                                                                                            <asp:Label ID="Label4" runat="server" CssClass="Label" Text="Department Code :"></asp:Label>
                                                                                        </td>
                                                                                        <td>
                                                                                            <asp:DropDownList ID="ddlDepartmentCode" runat="server" AutoPostBack="True" CssClass="Combobox"
                                                                                                Width="210px" >
                                                                                                <asp:ListItem Selected="True">Select</asp:ListItem>
                                                                                            </asp:DropDownList>
                                                                                        </td>
                                                                                        <td>
                                                                                            <asp:Image ID="Image1" runat="server" Height="10px" ImageUrl="~/App_Themes/Styles/Images/img_mand.png"
                                                                                                title="Department ID is required!!!" ToolTip="Mandatory Field!!!" Width="10px" />
                                                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="ddlDepartmentCode"
                                                                                                ErrorMessage="RequiredFieldValidator" InitialValue="Select" ToolTip="Department ID is required"
                                                                                                ValidationGroup="Save"><img src="../App_Themes/Styles/Images/err1.png" 
                                                                                title="Department ID is required!!!" /></asp:RequiredFieldValidator>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td>
                                                                                            <asp:Label ID="lblItemCode13" runat="server" CssClass="Label" Text="Area/Cubicle Code :"></asp:Label>
                                                                                        </td>
                                                                                        <td>
                                                                                            <asp:TextBox ID="txtCubicleCode" runat="server" CssClass="TextBox" MaxLength="500"
                                                                                                Width="200px"></asp:TextBox>
                                                                                        </td>
                                                                                        <td>
                                                                                            <asp:Image ID="img_Str0" runat="server" Height="10px" ImageUrl="~/App_Themes/Styles/Images/img_mand.png"
                                                                                                title="Cubicle ID is required!!!" ToolTip="Mandatory Field!!!" Width="10px" />
                                                                                            <asp:RequiredFieldValidator ID="rvCubicleId" runat="server" ControlToValidate="txtCubicleCode"
                                                                                                ErrorMessage="Enter Cubicle ID" ToolTip="Cubicle ID is Required!!!" ValidationGroup="Save"><img src="../App_Themes/Styles/Images/err1.png" 
                                                                                                title="Cubicle ID is Required!!!" /></asp:RequiredFieldValidator>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td>
                                                                                            <asp:Label ID="Label8" runat="server" CssClass="Label" Text="Cubicle Description :"></asp:Label>
                                                                                        </td>
                                                                                        <td>
                                                                                            <asp:TextBox ID="txtCubicleDesc" runat="server" CssClass="TextBox" MaxLength="500"
                                                                                                Width="200px"></asp:TextBox>
                                                                                        </td>
                                                                                        <td>
                                                                                            <asp:Image ID="Image5" runat="server" Height="10px" ImageUrl="~/App_Themes/Styles/Images/img_mand.png"
                                                                                                title="Cubicle Description is required!!!" ToolTip="Mandatory Field!!!" Width="10px" />
                                                                                            <asp:RequiredFieldValidator ID="rvDescp" runat="server" ControlToValidate="txtCubicleDesc"
                                                                                                ErrorMessage="Enter Cubicle Description" ToolTip="Cubicle Description is Required!!!"
                                                                                                ValidationGroup="Save"><img src="../App_Themes/Styles/Images/err1.png" 
                                                                                                title="Cubicle Description is Required!!!" /></asp:RequiredFieldValidator>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <asp:Panel ID="Panel1" Visible="False" runat="server">
                                                                                        <tr>
                                                                                            <td>
                                                                                                <asp:Label ID="lblHoldPeriod" runat="server" CssClass="Label" Text="Hold Period For Clean:"></asp:Label>
                                                                                            </td>
                                                                                            <td colspan="2">
                                                                                                <asp:TextBox ID="txtHoldPeriod" runat="server" CssClass="TextBox" MaxLength="500"
                                                                                                    Width="200px"></asp:TextBox>
                                                                                                <asp:FilteredTextBoxExtender ID="filterPrinterIp" runat="server" FilterType="Numbers"
                                                                                                    TargetControlID="txtHoldPeriod" Enabled="True">
                                                                                                </asp:FilteredTextBoxExtender>
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:Image ID="img_Str1" runat="server" Height="10px" ImageUrl="~/App_Themes/Styles/Images/img_mand.png"
                                                                                                    title="Hold Period is required!!!" ToolTip="Mandatory Field!!!" Width="10px"
                                                                                                    Visible="False" />
                                                                                                <asp:RequiredFieldValidator ID="rvTxtHoldPeriod" runat="server" ControlToValidate="txtHoldPeriod"
                                                                                                    ErrorMessage="Enter Hold Period" Enabled="False" ToolTip="Hold Period is Required!!!"
                                                                                                    ValidationGroup="Save"><img src="../App_Themes/Styles/Images/err1.png" 
                                                                                                title="Hold Period is Required!!!" /></asp:RequiredFieldValidator>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td>
                                                                                                <asp:Label ID="lblHoldPeriodCleanUnit" runat="server" CssClass="Label" Text="Hold Period Clean Unit :"></asp:Label>
                                                                                                <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterType="Numbers"
                                                                                                    TargetControlID="txtHoldPeriodToBeClean" Enabled="True">
                                                                                                </asp:FilteredTextBoxExtender>
                                                                                            </td>
                                                                                            <td colspan="2">
                                                                                                <asp:DropDownList ID="ddlHoldClean" runat="server" AutoPostBack="True" CssClass="Combobox"
                                                                                                    Width="210px">
                                                                                                    <asp:ListItem Selected="True">Select</asp:ListItem>
                                                                                                    <asp:ListItem>DAYS</asp:ListItem>
                                                                                                    <asp:ListItem>HOURS</asp:ListItem>
                                                                                                </asp:DropDownList>
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:Image ID="Image6" runat="server" Height="10px" ImageUrl="~/App_Themes/Styles/Images/img_mand.png"
                                                                                                    title="Hold Period Unit is required!!!" ToolTip="Mandatory Field!!!" Width="10px"
                                                                                                    Visible="False" />
                                                                                                <asp:RequiredFieldValidator Enabled="False" ID="rvHoldUnit" InitialValue="Select"
                                                                                                    runat="server" ControlToValidate="ddlHoldClean" ErrorMessage="Enter Hold Period Unit"
                                                                                                    ToolTip="Hold Period Unit is required!!!" ValidationGroup="Save"><img src="../App_Themes/Styles/Images/err1.png"
                                                                                                title="Hold Period Unit is required!!!" /></asp:RequiredFieldValidator>
                                                                                            </td>
                                                                                        </tr>
                                                                                    </asp:Panel>
                                                                                    <asp:Panel ID="Panel2" runat="server" Visible="False">
                                                                                    <tr>
                                                                                        <td>
                                                                                            <asp:Label ID="Label3" runat="server" CssClass="Label" Text="Hold Period For to be Clean Status :"></asp:Label>
                                                                                        </td>
                                                                                        <td colspan="2">
                                                                                            <asp:TextBox ID="txtHoldPeriodToBeClean" runat="server" CssClass="TextBox" MaxLength="500"
                                                                                                Width="200px"></asp:TextBox>
                                                                                        </td>
                                                                                        <td>
                                                                                            <asp:Image ID="Image2" runat="server" Height="10px" ImageUrl="~/App_Themes/Styles/Images/img_mand.png"
                                                                                                title="Hold Period To be Clean is required!!!" ToolTip="Mandatory Field!!!" Width="10px"
                                                                                                Visible="False" />
                                                                                            <asp:RequiredFieldValidator Enabled="False" ID="rvHoldPeriodClean" runat="server"
                                                                                                ControlToValidate="txtHoldPeriodToBeClean" ErrorMessage="Hold Period is Reguired"
                                                                                                ToolTip="Hold Period To be Clean is required!!!" ><img src="../App_Themes/Styles/Images/err1.png" /></asp:RequiredFieldValidator>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td>
                                                                                            <asp:Label ID="Label6" runat="server" CssClass="Label" Text="Hold Period to Be Clean Unit :"></asp:Label>
                                                                                        </td>
                                                                                        <td colspan="2">
                                                                                            <asp:DropDownList ID="ddlHoldPeriodUnit" runat="server" AutoPostBack="True" CssClass="Combobox"
                                                                                                Width="210px">
                                                                                                <asp:ListItem Selected="True">Select</asp:ListItem>
                                                                                                <asp:ListItem>HOURS</asp:ListItem>
                                                                                                <asp:ListItem>DAYS</asp:ListItem>
                                                                                            </asp:DropDownList>
                                                                                        </td>
                                                                                        <td>
                                                                                            <asp:Image ID="Image3" runat="server" Height="10px" ImageUrl="~/App_Themes/Styles/Images/img_mand.png"
                                                                                                title="Hold Period Unit is required!!!" ToolTip="Mandatory Field!!!" Width="10px"
                                                                                                Visible="False" />
                                                                                            <asp:RequiredFieldValidator Enabled="False" ID="rvHoldePeriod" InitialValue="Select"
                                                                                                runat="server" ControlToValidate="ddlHoldPeriodUnit" ErrorMessage="Hold Period To be Clean Unit is required"
                                                                                                ToolTip="Hold Period To be Clean Unit is required!!!" ><img src="../App_Themes/Styles/Images/err1.png" 
                                                                                                /></asp:RequiredFieldValidator>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td>
                                                                                            <asp:Label ID="Label1" runat="server" CssClass="Label" Text="Cleaning SOP No . :"></asp:Label>
                                                                                        </td>
                                                                                        <td colspan="2">
                                                                                            <asp:TextBox ID="txtCLenaingSOP" runat="server" CssClass="TextBox" MaxLength="500"
                                                                                                Width="200px"></asp:TextBox>
                                                                                        </td>
                                                                                        <td>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td>
                                                                                            <asp:Label ID="Label18" runat="server" CssClass="Label" Text="Equipment Time . :"></asp:Label>
                                                                                        </td>
                                                                                        <td colspan="2">
                                                                                            <asp:TextBox ID="txtRLAFtime" runat="server" CssClass="TextBox" MaxLength="500" Width="200px"></asp:TextBox>
                                                                                        </td>
                                                                                        <td>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td>
                                                                                            <asp:Label ID="Label2" runat="server" CssClass="Label" Text="Pre-Filter Reading :"></asp:Label>
                                                                                        </td>
                                                                                        <td>
                                                                                            <asp:TextBox ID="txtReadingPref" runat="server" CssClass="TextBox" MaxLength="500"
                                                                                                SkinID="NAME" Width="100px"></asp:TextBox>
                                                                                            &nbsp;<asp:Label ID="Label17" runat="server" CssClass="lblstatus" Text="To"></asp:Label>
                                                                                            &nbsp;
                                                                                            <asp:TextBox ID="txtReadingPrefTo" runat="server" CssClass="TextBox" SkinID="NAME"
                                                                                                Width="100px"></asp:TextBox>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td>
                                                                                            <asp:Label ID="Label9" runat="server" CssClass="Label" Text="IntermediateFilter/ Chember Reading :"></asp:Label>
                                                                                        </td>
                                                                                        <td>
                                                                                            <asp:TextBox ID="txtInterMediateFilterFrom" runat="server" CssClass="TextBox" MaxLength="500"
                                                                                                SkinID="NAME" Width="100px"></asp:TextBox>
                                                                                            &nbsp;<asp:Label ID="Label16" runat="server" CssClass="lblstatus" Text="To"></asp:Label>
                                                                                            &nbsp;
                                                                                            <asp:TextBox ID="txtInterMediateFilterTo" runat="server" CssClass="TextBox" SkinID="NAME"
                                                                                                Width="100px"></asp:TextBox>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td>
                                                                                            <asp:Label ID="Label10" runat="server" CssClass="Label" Text="Hepa Filter Reading :"></asp:Label>
                                                                                        </td>
                                                                                        <td>
                                                                                            <asp:TextBox ID="txtHepaFilterReadingFrom" runat="server" CssClass="TextBox" MaxLength="500"
                                                                                                SkinID="NAME" Width="100px"></asp:TextBox>
                                                                                            &nbsp;<asp:Label ID="Label15" runat="server" CssClass="lblstatus" Text="To"></asp:Label>
                                                                                            &nbsp;
                                                                                            <asp:TextBox ID="txtHepaFilterReadingTo" runat="server" CssClass="TextBox" SkinID="NAME"
                                                                                                Width="100px"></asp:TextBox>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td>
                                                                                            <asp:Label ID="Label11" runat="server" CssClass="Label" Text="Dispensing Temp :"></asp:Label>
                                                                                        </td>
                                                                                        <td>
                                                                                            <asp:TextBox ID="txtDispTempFrom" runat="server" CssClass="TextBox" MaxLength="500"
                                                                                                SkinID="NAME" Width="100px"></asp:TextBox>
                                                                                            &nbsp;<asp:Label ID="Label14" runat="server" CssClass="lblstatus" Text="To"></asp:Label>
                                                                                            &nbsp;
                                                                                            <asp:TextBox ID="txtDispTempTo" runat="server" CssClass="TextBox" SkinID="NAME" Width="100px"></asp:TextBox>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td>
                                                                                            <asp:Label ID="Label12" runat="server" CssClass="Label" Text="Dispensing Humidity :"></asp:Label>
                                                                                        </td>
                                                                                        <td>
                                                                                            <asp:TextBox ID="txtDispHumidityFrom" runat="server" CssClass="TextBox" SkinID="NAME"
                                                                                                Width="100px"></asp:TextBox>
                                                                                            &nbsp;<asp:Label ID="Label13" runat="server" CssClass="lblstatus" Text="To"></asp:Label>
                                                                                            &nbsp;
                                                                                            <asp:TextBox ID="txtDispHumidiyTo" runat="server" CssClass="TextBox" SkinID="NAME"
                                                                                                Width="100px"></asp:TextBox>
                                                                                        </td>
                                                                                    </tr>
                                                                                    
                                                                                    </asp:Panel>
                                                                                    <tr>
                                                                                        <td>
                                                                                            &nbsp;&nbsp;<asp:Label ID="lblStatus" runat="server" CssClass="lblstatus" Text="Status :"></asp:Label>
                                                                                        </td>
                                                                                        <td>
                                                                                            <asp:RadioButton ID="RBactivate" Checked="True" Text="Activate" runat="server" ValidationGroup="A"
                                                                                                GroupName="RDgrp" />&nbsp;&nbsp;
                                                                                            <asp:RadioButton ID="RBdeactivate" Text="De-Activate" runat="server" ValidationGroup="A"
                                                                                                GroupName="RDgrp" />
                                                                                        </td>
                                                                                        <td>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td>
                                                                                            <asp:Label ID="lblRemark" runat="server" CssClass="Label" Text="Remark :"></asp:Label>
                                                                                        </td>
                                                                                        <td>
                                                                                            <asp:TextBox ID="txtRemark" runat="server" CssClass="TextBox" MaxLength="500" SkinID="NAME"
                                                                                                Width="200px"></asp:TextBox>
                                                                                        </td>
                                                                                    </tr>
                                                                                   <tr style=" visibility:hidden">
                                                                                        <td>
                                                                                            <asp:Label ID="Label19" runat="server" CssClass="Label" Text="Module No :"></asp:Label>
                                                                                        </td>
                                                                                        <td>
                                                                                            <asp:TextBox ID="txtModuleNo" runat="server" CssClass="TextBox" MaxLength="500" SkinID="NAME"
                                                                                                Width="200px"></asp:TextBox>
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                            </ContentTemplate>
                                                                        </asp:TabPanel>
                                                                    </asp:TabContainer>
                                                                </td>
                                                                <td style="width: 50%;" valign="top">
                                                                    <asp:Panel ID="pnlImport" runat="server" ScrollBars="Auto" Height="500px">
                                                                        <asp:GridView ID="GRCUBICLE_GRID" runat="server" AllowPaging="True" AllowSorting="True"
                                                                            AutoGenerateColumns="False" CaptionAlign="Left" CellPadding="4" CssClass="style22"
                                                                            EmptyDataText="There are no items to show in this view." Height="15px" PageSize="10"
                                                                            ShowFooter="True" Width="825px" OnPageIndexChanging="GRCUBICLE_GRID_PageIndexChanging"
                                                                            OnRowDataBound="GRCUBICLE_GRID_RowDataBound">
                                                                            <Columns>
                                                                                <asp:BoundField DataField="PLANTCODE" HeaderText="PLANT CODE" SortExpression="PLANTCODE" />
                                                                                <asp:BoundField DataField="DEPARTMENTDESC" HeaderText="DEPARTMENT DESC" SortExpression="DEPARTMENTDESC" />
                                                                                <asp:BoundField DataField="CUBICLECODE" HeaderText="CUBICLE CODE" SortExpression="CUBICLECODE" />
                                                                                <asp:BoundField DataField="CUBICLEDESC" HeaderText="CUBICLE DESC"
                                                                                    SortExpression="CUBICLEDESC" />
                                                                                <asp:BoundField DataField="HOLDPERIODCLEAN" HeaderText="HOLD PERIOD CLEAN"
                                                                                    SortExpression="HOLDPERIODCLEAN" />
                                                                                <asp:BoundField DataField="HOLDPERIODCLEANUNIT" HeaderText="HOLD PERIOND CLEAN UNIT"
                                                                                    SortExpression="HOLDPERIODCLEANUNIT" />
                                                                                <asp:BoundField DataField="HOLDPERIODTOBECLEAN" HeaderText="HOLD PERIOD TO BE CLEAN "
                                                                                    SortExpression="HOLDPERIODTOBECLEAN" />
                                                                                <asp:BoundField DataField="HOLDPERIODTOBECLEANUNIT" HeaderText="HOLD PERIOD TOBECLEAN UNIT"
                                                                                    SortExpression="HOLDPERIODTOBECLEANUNIT" />
                                                                                <asp:BoundField DataField="CL_SOP_NO" HeaderText="CLEANING SOP NO" SortExpression="CL_SOP_NO" />
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
                                                                        &nbsp;&nbsp;&nbsp;&nbsp;
                                                                        <asp:Button ID="btnSave" runat="server" CssClass="Button" OnClick="btnSave_Click"
                                                                            Text="Save" Width="60px" />
                                                                        &nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;
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
                                    <tr>
                                        <td style="height: 30px;">
                                            &nbsp;
                                        </td>
                                    </tr>
                                </table>
                            </asp:View>
                            <asp:View ID="View1" runat="server">
                                <table style="width: 100%">
                                    <tr>
                                        <td height="30" style="height: 30px; background-image: url('../App_Themes/Styles/Images/Menu_Out.png');
                                            background-repeat: repeat-x">
                                            <table cellpadding="5">
                                                <tr>
                                                    <td>
                                                        <asp:ImageButton ID="ImageButton1" runat="server" Height="24px" ImageUrl="~/App_Themes/Styles/Images/exit.ico"
                                                            ToolTip="Close Page" Width="24px" OnClick="imgCloseV1_Click" />
                                                    </td>
                                                    <td>
                                                        <asp:ImageButton ID="ImageButton2" runat="server" Height="24px" ImageUrl="~/App_Themes/Styles/Images/window_new.png"
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
                                                            <asp:ListItem Value="DEPARTMENTCODE">Department Code</asp:ListItem>
                                                            <asp:ListItem Value="DEPARTMENTDESC">Department Description</asp:ListItem>
                                                            <asp:ListItem Value="CUBICLECODE">Cubicle Code</asp:ListItem>
                                                            <asp:ListItem Value="CUBICLEDESC">Cubicle Description</asp:ListItem>
                                                            <asp:ListItem Value="STATUS">Status</asp:ListItem>
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
                                        <td style="width: 100%;" valign="top">
                                            <asp:GridView ID="GRCUBICLE" runat="server" AllowPaging="True" AllowSorting="True"
                                                AutoGenerateColumns="False" CaptionAlign="Left" CellPadding="4" CssClass="style22"
                                                Width="100%" EmptyDataText="There are no items to show in this view." Height="16px"
                                                PageSize="20" ShowFooter="True" DataKeyNames="REFNO" OnPageIndexChanging="GRCUBICLE_PageIndexChanging"
                                                OnRowCommand="GRCUBICLE_RowCommand" OnRowDataBound="GRCUBICLE_RowDataBound">
                                                <Columns>
                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="LinkButton1" CommandName="Select" Text="Select" runat="server" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="PLANTCODE" HeaderText="PLANT CODE" SortExpression="PLANT ID" />
                                                    <asp:BoundField DataField="DEPARTMENTCODE" HeaderText="DEPARTMENT CODE" SortExpression="DEPARTMENTCODE" />
                                                    <asp:BoundField DataField="DEPARTMENTDESC" HeaderText="DEPARTMENT DESC" SortExpression="PLANT LOCATION" />
                                                    <asp:BoundField DataField="CUBICLECODE" HeaderText="CUBICLE CODE" SortExpression="ADDRESS" />
                                                    <asp:BoundField DataField="CUBICLEDESC" HeaderText="CUBICLE DESC" SortExpression="ASSOCIATE_PLANT_CODE" />
                                                    <asp:BoundField DataField="HOLDPERIODCLEAN" HeaderText="HOLD PERIOD CLEAN" SortExpression="ASSOCIATE_PLANT_DESC" />
                                                    <asp:BoundField DataField="HOLDPERIODCLEANUNIT" HeaderText="HOLD PERIOD CLEAN UNIT"
                                                        SortExpression="HOLDPERIODCLEANUNIT" />
                                                    <asp:BoundField DataField="HOLDPERIODTOBECLEAN" HeaderText="HOLD PERIOD TO BE CLEAN "
                                                        SortExpression="HOLDPERIODTOBECLEAN" />
                                                    <asp:BoundField DataField="HOLDPERIODTOBECLEANUNIT" HeaderText="HOLD PERIOD TO BE CLEAN UNIT"
                                                        SortExpression="HOLDPERIODTOBECLEANUNIT" />
                                                    <asp:BoundField DataField="CL_SOP_NO" HeaderText="CLEANING SOP NO" SortExpression="CL_SOP_NO" />
                                                    <asp:BoundField DataField="ST" HeaderText="STATUS" SortExpression="ST" />
                                                    <asp:BoundField DataField="REMARK" HeaderText="Remark" SortExpression="REMARK" />
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
                                            &nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;
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

