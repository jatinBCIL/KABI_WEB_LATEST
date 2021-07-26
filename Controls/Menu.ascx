<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Menu.ascx.cs" Inherits="Controls_Menu" %>
<link href="../App_Themes/Styles/MenubarSS.css" rel="stylesheet" type="text/css" />
<script src="../App_Themes/Styles/jquery.min.js" type="text/javascript"></script>
<script src="../App_Themes/Styles/window.js" type="text/javascript"></script>
<script src="../Masters/Javascript/JScript.js" type="text/javascript"></script>
 <script type="text/javascript">
     $(document)
  .ajaxStart(function () {
      $('#DisableDiv').fadeIn('slow', .8);
      $('#DisableDiv').append('<div class="form-group" style="position: fixed; text-align: center; height: 100%; width: 100%; top:0; right: 0; left: 0; background-color: #DEE9F1  ;z-index: 9999999; opacity:0.8"  align="center"><img src="../App_Themes/Styles/Images/round.gif" style="padding: 10px;position:fixed;top:20%;left:35%;"  /></div>');
      document.getElementById("DisableDiv").style.visibility = 'visible';

  })
  .ajaxStop(function () {
      document.getElementById("DisableDiv").style.visibility = 'hidden';
  });
 </script>
<style type="text/css">
    body
    {
        font: 13px 'trebuchet MS' , Arial, Helvetica;
    }
    
    h2, p
    {
        text-align: center;
        color: black;
        text-shadow: 0 1px 0 #fff;
    }
    
    a
    {
        color: black;
    }
    
    /* You don't need the above styles, they are demo-specific ----------- */
    
    #menu, #menu ul
    {
        margin: 0;
        padding: 0;
        list-style: none;
    }
    
    ul
    {
        <%--border: 1px solid black;--%>
    }
    
    #menu
    {
        width: 100%;
        margin: 0px auto;
        border: 0px solid #222;
        background-color:#C0C0C0;
        <%--background-color: #FFFFFF;
        background-image: -moz-linear-gradient(#FFFFFF, #FFFFFF);
        background-image: -webkit-gradient(linear, left top, left bottom, from(#FFFFFF), to(#FFFFFF));
        background-image: -webkit-linear-gradient(#FFFFFF, #FFFFFF);
        background-image: -o-linear-gradient(#FFFFFF, #FFFFFF);
        background-image: -ms-linear-gradient(#FFFFFF, #FFFFFF);
        background-image: linear-gradient(#FFFFFF, #FFFFFF);--%>
        <%---moz-border-radius: 6px;
        -webkit-border-radius: 6px;
        border-radius: 6px;
        -moz-box-shadow: 0 1px 1px #777, 0 1px 0 #666 inset;
        -webkit-box-shadow: 0 1px 1px #777, 0 1px 0 #666 inset;
        box-shadow: 0 1px 1px #777, 0 1px 0 #666 inset;--%>
    }
    
    #menu:before, #menu:after
    {
        content: "";
        display: table;
    }
    
    #menu:after
    {
        clear: both;
    }
    
    #menu
    {
        zoom: 1;
    }
    
    #menu li
    {
        float: left;
        <%--border-right: 1px solid #222;--%>
       <%-- -moz-box-shadow: 1px 0 0 #FFFFFF;
        -webkit-box-shadow: 1px 0 0 #FFFFFF;
        box-shadow: 1px 0 0 #FFFFFF;--%>
        position: relative;
    }
    
    #menu a
    {
        float: left;
        padding: 12px 30px;
        color: black;
        text-transform: uppercase;
        font: bold 13px Arial, Helvetica;
        text-decoration: none;
        text-shadow: 0 1px 0 #000;
    }
    
    #menu li:hover > a
    {
        color: black;
    }
    
    *html #menu li a:hover
    {
        /* IE6 only */
        color: Red;
    }
    
    #menu ul
    {
        margin: 20px 0 0 0;
        _margin: 0; /*IE6 only*/
        opacity: 0;
        color: Red;
        visibility: hidden;
        position: absolute;
        top: 38px;
        left: 0;
        z-index: 1;
        background-color:#C0C0C0;
        <%--background: white;
        background: -moz-linear-gradient(white, white);
        background-image: -webkit-gradient(linear, left top, left bottom, from(white), to(white));
        background: -webkit-linear-gradient(white, white);
        background: -o-linear-gradient(white, white);
        background: -ms-linear-gradient(white, white);
        background: linear-gradient(white, white);--%>
        -moz-box-shadow: 0 -1px rgba(255,255,255,.3);
        -webkit-box-shadow: 0 -1px 0 rgba(255,255,255,.3);
        box-shadow: 0 -1px 0 rgba(255,255,255,.3);
        -moz-border-radius: 3px;
        -webkit-border-radius: 3px;
        border-radius: 3px;
        -webkit-transition: all .2s ease-in-out;
        -moz-transition: all .2s ease-in-out;
        -ms-transition: all .2s ease-in-out;
        -o-transition: all .2s ease-in-out;
        transition: all .2s ease-in-out;
    }
    
    #menu li:hover > ul
    {
        opacity: 1;
        visibility: visible;
        margin: 0;
        font-size:14px;
    }
    
     #menu li:hover 
     { 
     	font-size:14px;
     }
    
    #menu ul:hover 
    {
    	font-size:14px;
    }
    
    #menu ul ul
    {
        top: 0;
        left: 150px;
        margin: 0 0 0 20px;
        _margin: 0; /*IE6 only*/
        -moz-box-shadow: -1px 0 0 rgba(255,255,255,.3);
        -webkit-box-shadow: -1px 0 0 rgba(255,255,255,.3);
        box-shadow: -1px 0 0 rgba(255,255,255,.3);
    }
    
    #menu ul li
    {
        float: none;
        display: block;
        border: 0;
        _line-height: 0; /*IE6 only*/
        -moz-box-shadow: 0 1px 0 #FFFFFF, 0 2px 0 #666;
        -webkit-box-shadow: 0 1px 0 #FFFFFF, 0 2px 0 #666;
        box-shadow: 0 1px 0 #FFFFFF, 0 2px 0 #666;
    }
    
    #menu ul li:last-child
    {
        -moz-box-shadow: none;
        -webkit-box-shadow: none;
        box-shadow: none;
    }
    
    #menu ul a
    {
        padding: 5px;
        width: auto;
        _height: 10px; /*IE6 only*/
        display: block;
        white-space: nowrap;
        float: none;
        text-transform: none;
    }
    
    #menu ul a:hover
    {
        background-color: #FFFFFF;
        font-size:14px;
        <%--background-image: -moz-linear-gradient(#04acec,  #0186ba);
        background-image: -webkit-gradient(linear, left top, left bottom, from(#04acec), to(#0186ba));
        background-image: -webkit-linear-gradient(#04acec, #0186ba);
        background-image: -o-linear-gradient(#04acec, #0186ba);
        background-image: -ms-linear-gradient(#04acec, #0186ba);
        background-image: linear-gradient(#04acec, #0186ba);--%>
    }
    
    #menu ul li:first-child > a
    {
        -moz-border-radius: 3px 3px 0 0;
        -webkit-border-radius: 3px 3px 0 0;
        border-radius: 3px 3px 0 0;
    }
    
    #menu ul li:first-child > a:after
    {
        content: '';
        position: absolute;
        left: 40px;
        top: -6px;
        border-left: 6px solid transparent;
        border-right: 6px solid transparent;
        border-bottom: 6px solid #FFFFFF;
    }
    
    #menu ul ul li:first-child a:after
    {
        left: -6px;
        top: 50%;
        margin-top: -6px;
        border-left: 0;
        border-bottom: 6px solid transparent;
        border-top: 6px solid transparent;
        border-right: 6px solid #3b3b3b;
    }
    
    #menu ul li:first-child a:hover:after
    {
        border-bottom-color: #04acec;
    }
    
    #menu ul ul li:first-child a:hover:after
    {
        border-right-color: #0299d3;
        border-bottom-color: transparent;
    }
    
    #menu ul li:last-child > a
    {
        -moz-border-radius: 0 0 3px 3px;
        -webkit-border-radius: 0 0 3px 3px;
        border-radius: 0 0 3px 3px;
    }
    
    /* Mobile */
    #menu-trigger
    {
        display: none;
    }
    
    @media screen and (max-width: 600px)
    {
    
        /* nav-wrap */
        #menu-wrap
        {
            position: relative;
        }
    
        #menu-wrap *
        {
            -moz-box-sizing: border-box;
            -webkit-box-sizing: border-box;
            box-sizing: border-box;
        }
    
    
    
        /* main nav */
        #menu
        {
            margin: 0;
            padding: 0px;
            position: absolute;
            top: 40px;
            width: 100%;
            z-index: 1;
            background-color: #FFFFFF;
            display: none;
            -moz-box-shadow: none;
            -webkit-box-shadow: none;
            box-shadow: none;
        }
    
        #menu:after
        {
            content: '';
            position: absolute;
            left: 25px;
            top: -8px;
            border-left: 8px solid transparent;
            border-right: 8px solid transparent;
            border-bottom: 8px solid #FFFFFF;
        }
    
        #menu ul
        {
            position: static;
            visibility: visible;
            opacity: 1;
            margin: 0;
            background: none;
            -moz-box-shadow: none;
            -webkit-box-shadow: none;
            box-shadow: none;
        }
    
        #menu ul ul
        {
            margin: 0 0 0 20px !important;
            -moz-box-shadow: none;
            -webkit-box-shadow: none;
            box-shadow: none;
        }
    
        #menu li
        {
            position: static;
            display: block;
            float: none;
            border: 0;
            margin: 5px;
            -moz-box-shadow: none;
            -webkit-box-shadow: none;
            box-shadow: none;
        }
    
        #menu ul li
        {
            margin-left: 0px;
            -moz-box-shadow: none;
            -webkit-box-shadow: none;
            box-shadow: none;
        }
    
        #menu a
        {
            display: block;
            float: none;
            padding: 0;
            color: black;
        }
    
        #menu a:hover
        {
            color: black;
        }
    
        #menu ul a
        {
            padding: 0;
            width: auto;
        }
    
        #menu ul a:hover
        {
            background: none;
        }
    
        #menu ul li:first-child a:after, #menu ul ul li:first-child a:after
        {
            border: 0;
        }
    
    }
    
    @media screen and (min-width: 600px)
    {
        #menu
        {
            display: block !important;
        }
    }
    
    /* iPad */
    .no-transition
    {
        -webkit-transition: none;
        -moz-transition: none;
        -ms-transition: none;
        -o-transition: none;
        transition: none;
        opacity: 1;
        visibility: visible;
        display: none;
    }
    
    #menu li:hover > .no-transition
    {
        display: block;
    }
</style>
<nav id="menu-wrap">    
    
    <ul id="menu">
        <li class="active" style="text-align: center;">
            <asp:HyperLink runat="server" ID="MenuHome" Text="Home" NavigateUrl="~/Modules/frmMain.aspx"></asp:HyperLink></li>
        <li style="color: #000000">
         <a>Masters</a>
            <ul style="width: auto; height: auto; border: 1px solid black">
                <li id="Menu101A" runat="server" visible="false">
                    <asp:HyperLink runat="server" ID="Menu101" Text="User Master" NavigateUrl="~/Masters/frmUserMaster.aspx"></asp:HyperLink></li>
                <li id="Menu102A" runat="server"  visible="false">
                    <asp:HyperLink runat="server" ID="Menu102" Text="User Roles" NavigateUrl="~/Masters/frmUserRole.aspx"></asp:HyperLink></li>
                <li id="Menu103A" runat="server"  visible="false">
                    <asp:HyperLink runat="server" ID="Menu103" Text="Plant Master " NavigateUrl="../Masters/frmPlantMasters.aspx"></asp:HyperLink></li>
                <li id="Menu104A" runat="server"  visible="false">
                    <asp:HyperLink runat="server" ID="Menu104" Text="Bin Master" NavigateUrl="../Masters/frmBinMaster.aspx"></asp:HyperLink></li>
                               <li id="Menu105A" runat="server" visible="true">
                    <asp:HyperLink runat="server" ID="Menu105" Text="Printer Master" NavigateUrl="../Masters/FrmPrinter.aspx"></asp:HyperLink></li>
                      <li id="Menu106A" runat="server"  visible="false">
                    <asp:HyperLink runat="server" ID="Menu106" Text="Weighing Master" NavigateUrl="~/Masters/frmWeigningMaster.aspx"></asp:HyperLink></li>
                     <li id="Menu107A" runat="server" visible="false">
                    <asp:HyperLink runat="server" ID="Menu107" Text="Area / Booth Master" NavigateUrl="~/Masters/frmBoothMaster.aspx"></asp:HyperLink></li>
                   <%-- //added by tejaswini--%>
                   <%--<li id="Menu108A" runat="server" visible="true">
                    <asp:HyperLink runat="server" ID="Menu108" Text="Department Master" NavigateUrl="~/Masters/frmDepartmentMaster.aspx"></asp:HyperLink></li>
                       <li id="Menu109A" runat="server" visible="true">
                    <asp:HyperLink runat="server" ID="Menu109" Text="Cubicle Master" NavigateUrl="~/Masters/Masters_frmCubical.aspx"></asp:HyperLink></li>--%>
                     <li id="Menu110A" runat="server"  visible="false">
                    <asp:HyperLink runat="server" ID="Menu110" Text="Standard Weight Master" NavigateUrl="~/Masters/frmStandardWT_Master.aspx"></asp:HyperLink></li>
                       <li id="Menu111A" runat="server"  visible="false">
                    <asp:HyperLink runat="server" ID="Menu111" Text="Master Barcode Printing" NavigateUrl="~/Masters/frmMasterBarcodePrinting.aspx"></asp:HyperLink></li>

            </ul>
        </li>
        <li style="color: #000000"><a>Warehouse</a>

            <ul style="width: auto; height: auto; border: 1px solid black">

                     <li id="Menu201A" runat="server"  visible="false">
                    <asp:HyperLink runat="server" ID="Menu201" Text="Purchase order"
                        NavigateUrl="~/Transactions/frmPurchaseOrder.aspx"></asp:HyperLink></li>

                        <li id="Menu202A" runat="server"  visible="false">
                    <asp:HyperLink runat="server" ID="Menu202" Text="Material Posting (MANNo)"
                        NavigateUrl="~/Transactions/frmMaterialPosting.aspx"></asp:HyperLink></li>
                        
                        <li id="Menu203A" runat="server"  visible="false">
                    <asp:HyperLink runat="server" ID="Menu203" Text="Status Label Printing"
                        NavigateUrl="~/Transactions/FrmStatusLabelPrint.aspx"></asp:HyperLink></li>

                          <li id="Menu204A" runat="server"  visible="false">
                    <asp:HyperLink runat="server" ID="Menu204" Text="Status Label Re-Printing"
                        NavigateUrl="~/Transactions/frmStatuLabelRePrinting.aspx"></asp:HyperLink></li>


                     <li id="Menu205A" runat="server"  visible="false">
                    <asp:HyperLink runat="server" ID="Menu205" Text="Dispensing Posting"
                        NavigateUrl="~/Transactions/frmDispensingPosting.aspx"></asp:HyperLink></li>

                          <li id="Menu206A" runat="server"  visible="false">
                    <asp:HyperLink runat="server" ID="Menu206" Text="Dispensing RePrinting"
                        NavigateUrl="~/Transactions/frmDispensingReprinting.aspx"></asp:HyperLink></li>

                         <li id="Menu207A" runat="server"  visible="false">
                    <asp:HyperLink runat="server" ID="Menu207" Text="MRN Label Printing"
                        NavigateUrl="~/Transactions/frmMRNLabelPrinting.aspx"></asp:HyperLink></li>

                           <li id="Menu208A" runat="server"  visible="false">
                    <asp:HyperLink runat="server" ID="Menu208" Text="MRN Label Re-Printing"
                        NavigateUrl="~/Transactions/frmMRNLabelRePrinting.aspx"></asp:HyperLink></li>

                            <li id="Menu209A" runat="server"  visible="false">
                    <asp:HyperLink runat="server" ID="Menu209" Text="Purchase Order View"
                        NavigateUrl="~/Transactions/frmPurchaseOrderView.aspx"></asp:HyperLink></li>
                             
               
            </ul>
        </li>
         <li style="color: #000000"><a>Reports</a>
          <ul style="width: auto; height: auto; border: 1px solid black">

                     	<li id="Menu302A" runat="server" visible="true">
                    <asp:HyperLink runat="server" ID="Menu302" Text="Weighing Reports"
                        NavigateUrl="~/Reports/Reports.aspx"></asp:HyperLink></li>
	
						<li id="Menu303A" runat="server" visible="true">
                    <asp:HyperLink runat="server" ID="Menu303" Text="Audit Trail Report"
                        NavigateUrl="~/Reports/frmAuditTrailReport.aspx"></asp:HyperLink></li>

                          <li id="Menu308A" runat="server" visible="true">
                    <asp:HyperLink runat="server" ID="HyperLink1" Text="Schedular Report"
                        NavigateUrl="~/Reports/SchedularReport.aspx"></asp:HyperLink></li>


            </ul>
        </li>
    </ul>
</nav>
<div id="DisableDiv"></div>
