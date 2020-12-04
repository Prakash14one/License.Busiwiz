<%@ Page Language="C#" MasterPageFile="~/Master/LicenseMaster.master" AutoEventWireup="true" CodeFile="CompanyRegistrationList.aspx.cs" Inherits="CustomerList" Title="Conform Payment /Approval company" %>
   
   
   <%@ Register Assembly="Winthusiasm.HtmlEditor" Namespace="Winthusiasm.HtmlEditor"    TagPrefix="cc2" %>
    
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>    
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script language="javascript" type="text/javascript">
        function CallPrint(strid) {
            var prtContent = document.getElementById('<%= pnlgrid.ClientID %>');
            var WinPrint = window.open('', '', 'left=0,top=0,width=900,height=600,toolbar=1,scrollbars=1,status=0');
            WinPrint.document.write(prtContent.innerHTML, '<link rel="stylesheet" type="text/css" href="css/forms.css" media="screen" />');
            WinPrint.document.close();
            WinPrint.focus();
            WinPrint.print();
            WinPrint.close();

        } 
    </script>
    <style type="text/css">
        .open
        {
            display: block;
        }
        .closed
        {
            display: none;
        }
                    .mGridcss
            {
	            width: 100%;
	            background-color: #fff;
	            margin: 5px 0 10px 0;
	            border: solid 1px #525252;
	            border-collapse: collapse;
	            font-size: 13px !important;
            }
            .mGridcss a
            {
	            font-size: 15px !important;
	            color: White;
            }
            .mGridcss a:hover
            {
	            font-size: 15px !important;
	            color: White;
	            text-decoration: underline;
            }
            .mGridcss td
            {
	            padding: 0px;
	            border: solid 1px #c1c1c1;
	            color: #717171;
            }
            .mGridcss th
            {
	            padding: 4px 2px;
	            color: #fff;
	            background-color: ;
	            border-left: solid 1px #525252;
	            font-size: 14px !important;
            }
            .mGridcss .alt
            {
	            background: #fcfcfc url(grd_alt.png) repeat-x top;
            }
            .mGridcss .pgr
            {
	            background-color: #416271;
            }
            .mGridcss .ftr
            {
	            background-color: #416271;
	            font-size: 15px !important;
	            color: White;
	            border: solid 1px #525252;
            }
            .mGridcss .pgr table
            {
	            margin: 5px 0;
            }
            .mGridcss .pgr td
            {
	            border-width: 0;
	            padding: 0 6px;
	            border-left: solid 1px #666;
	            font-weight: bold;
	            color: #fff;
	            line-height: 12px;
            }
            .mGridcss .pgr a
            {
	            color: Gray;
	            text-decoration: none;
            }
            .mGridcss .pgr a:hover
            {
	            color: #000;
	            text-decoration: none;
            }
            .mGridcss input[type="checkbox"]
            {
	            margin-top: 5px !important;
	            width: 10px !important;
	            float: left !important;
            }
            .mGridcss input[type="radio"]
            {
	            margin-top: 5px !important;
	            width: 100px !important;
	            float: left !important;
            }
    </style>
    <table width="100%">
    <tr>
    <td width="100%">
        <div style="float: left;">
                <asp:Label ID="lblmsg" runat="server" ForeColor="Red" Font-Size="16px"></asp:Label>
            </div>
    </td>
    </tr>
    <tr>
    <td width="100%">
    
   <fieldset>
     
        <legend>
            <asp:Label ID="Label1" runat="server" Text="Company List"></asp:Label>
        </legend>
        <div style="float: right">
            <asp:Button ID="Button1" runat="server" CssClass="btnSubmit" Text="Printable Version" Visible="false"   OnClick="Button1_Click1" />
            <input id="Button2" runat="server" class="btnSubmit" onclick="document.getElementById('mydiv').className='open';javascript:CallPrint('divPrint');document.getElementById('mydiv').className='closed';" style="width: 51px;" type="button" value="Print" visible="false" />
        </div>
        <div style="clear: both;">
        </div>
        <table  width="100%">
            <tr>
            <td>
             <label>
                    <asp:Label ID="Label5" runat="server" Text="Select Filter Value" Width="190px"></asp:Label>
                        <asp:DropDownList ID="ddlfilters" runat="server" AutoPostBack="false" OnSelectedIndexChanged="ddlActive_SelectedIndexChanged">
                            <asp:ListItem>---Show All Record---</asp:ListItem>
                            <asp:ListItem Selected="True"  Value="1">Active filter only</asp:ListItem>
                            <asp:ListItem Value="0">Inactive filter only</asp:ListItem>
                        </asp:DropDownList>
                    </label> 
                     <label>
                        <asp:Label ID="Label6" runat="server" Text="Filter By Server Name"></asp:Label>
                        <asp:DropDownList ID="ddlfillservernm" runat="server" Width="200px" AutoPostBack="false" OnSelectedIndexChanged="ddlfillservernm_SelectedIndexChanged">                            
                        </asp:DropDownList>
                    </label>
                    <label>
                        <asp:Label ID="Label3" runat="server" Text="Filter By  Portal"></asp:Label>
                        <asp:DropDownList ID="ddlportal" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlportal_SelectedIndexChanged" Width="200px">
                        </asp:DropDownList>
                    </label>
                    <label>
                        <asp:Label ID="Label4" runat="server" Text="Filter By Plan"></asp:Label>
                        <asp:DropDownList ID="ddlsortPlan" runat="server" Width="200px">
                        </asp:DropDownList>
                    </label>                   
                    <label>
                      <asp:Label ID="Label2" runat="server" Text="Filter By Active"></asp:Label>
                            <asp:DropDownList ID="ddlActive" runat="server" AutoPostBack="false" OnSelectedIndexChanged="ddlActive_SelectedIndexChanged" Width="200px">
                            <asp:ListItem>---Select All---</asp:ListItem>
                            <asp:ListItem Selected="True"  Value="1">Active</asp:ListItem>
                            <asp:ListItem Value="0">Inactive </asp:ListItem>
                        </asp:DropDownList>
                    </label>   
            </td>
            </tr>
           
                 <tr>
                <td>
                 
                    <label style="width:250px">
                        Search By Company Name
                        </label>
                        <label>
                            <asp:TextBox ID="txtsortsearch" runat="server" > </asp:TextBox>
                        </label>
                </td>
            </tr>
            <tr>
                <td>
                    <label style="width:250px">
                         Select Companies Registered Date
                    </label>  
                    <label style="width:150px;">
                    
                                    <asp:DropDownList ID="ddl_Companies_registered" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddl_Companies_registered_SelectedIndexChanged" Width="150px" >
                                      <asp:ListItem Value="0" Text="--Select All--"></asp:ListItem>
                                        <asp:ListItem Value="1" Text="Today"></asp:ListItem>
                                        <asp:ListItem Value="2" Text="This Week"></asp:ListItem>
                                        <asp:ListItem Value="3" Text="This Month"></asp:ListItem>
                                        <asp:ListItem Value="4" Text="This Year"></asp:ListItem>
                                        <asp:ListItem Value="5" Text="Custom"></asp:ListItem>                                        
                                    </asp:DropDownList>
                    </label> 
                        <asp:Panel ID="pnlcompanyregisterdate" runat="server"  Visible="false">
                                                                    <label style="width:80px;">
                                                                        <asp:Label ID="Label18" runat="server" Text="Start Date"></asp:Label>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtfrom"  ErrorMessage="*" ValidationGroup="3" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                                    </label>
                                                                    <label style="width:80px;">
                                                                        <asp:TextBox ID="txtfrom" Width="80px" runat="server"></asp:TextBox>
                                                                        <cc1:CalendarExtender ID="CalendarExtender2" runat="server" Format="MM/dd/yyyy" PopupButtonID="imgbtncalfrom" TargetControlID="txtfrom">
                                                                        </cc1:CalendarExtender>
                                                                    </label>
                                                                    <label style="width:40px;">
                                                                        <asp:ImageButton ID="imgbtncalfrom" runat="server" ImageUrl="~/Account/images/cal_actbtn.jpg" />
                                                                    </label>
                                                                    <label style="width:80px;">
                                                                        <asp:Label ID="Label19" runat="server" Text="End Date"></asp:Label>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtto"  ErrorMessage="*" ValidationGroup="3" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                                    </label>
                                                                    <label style="width:80px;">
                                                                        <asp:TextBox ID="txtto" Width="80px" runat="server"></asp:TextBox>
                                                                        <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Format="MM/dd/yyyy" PopupButtonID="imgbtnto"  TargetControlID="txtto">
                                                                        </cc1:CalendarExtender>
                                                                    </label>
                                                                    <label style="width:40px;">
                                                                        <asp:ImageButton ID="imgbtnto" runat="server" ImageUrl="~/Account/images/cal_actbtn.jpg" />
                                                                    </label>
                      </asp:Panel>
                </td>
                </tr>
                <tr>
                <td>
              

                 <label style="width:250px">
                    Select Company License Start Date
                 </label> 
                    <label style="width:150px;">                    
                                    <asp:DropDownList ID="ddllicensestart" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddllicensestart_SelectedIndexChanged" Width="150px" >
                                      <asp:ListItem Value="0" Text="--Select All--"></asp:ListItem>
                                        <asp:ListItem Value="1" Text="Today"></asp:ListItem>
                                        <asp:ListItem Value="2" Text="This Week"></asp:ListItem>
                                        <asp:ListItem Value="3" Text="This Month"></asp:ListItem>
                                        <asp:ListItem Value="4" Text="This Year"></asp:ListItem>
                                        <asp:ListItem Value="5" Text="Custom"></asp:ListItem>                                        
                                    </asp:DropDownList>
                    </label> 
                     <asp:Panel ID="pnllicensedate" runat="server" Visible="false">
                                                                    <label style="width:80px;">
                                                                        <asp:Label ID="Label7" runat="server" Text="Start Date"></asp:Label>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtfrom"  ErrorMessage="*" ValidationGroup="3" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                                    </label>
                                                                    <label style="width:80px;">
                                                                        <asp:TextBox ID="TextBox1" Width="80px" runat="server"></asp:TextBox>
                                                                        <cc1:CalendarExtender ID="CalendarExtender3" runat="server" Format="MM/dd/yyyy" PopupButtonID="ImageButton1" TargetControlID="TextBox1">
                                                                        </cc1:CalendarExtender>
                                                                    </label>
                                                                    <label style="width:40px;">
                                                                        <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Account/images/cal_actbtn.jpg" />
                                                                    </label>
                                                                    <label style="width:80px;">
                                                                        <asp:Label ID="Label8" runat="server" Text="End Date"></asp:Label>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="TextBox2" ErrorMessage="*" ValidationGroup="3" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                                    </label>
                                                                    <label style="width:80px;">
                                                                        <asp:TextBox ID="TextBox2" Width="80px" runat="server"></asp:TextBox>
                                                                        <cc1:CalendarExtender ID="CalendarExtender4" runat="server" Format="MM/dd/yyyy" PopupButtonID="ImageButton2"  TargetControlID="TextBox2">
                                                                        </cc1:CalendarExtender>
                                                                    </label>
                                                                    <label style="width:40px;">
                                                                        <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/Account/images/cal_actbtn.jpg" />
                                                                    </label>
                      </asp:Panel>
                </td>
                </tr>
               
            <tr>
            <td>
                     
                     <label>
                        <asp:Button ID="btngodate" runat="server" CssClass="btnSubmit" Text="Go" OnClick="btngodate_Click" />
                    </label>
            </td>
            </tr>
        </table>
        <asp:Panel ID="pnlgrid" runat="server" Width="100%">
            <table width="100%">
               
               
                <tr>
                    <td>
                        <div id="mydiv" class="closed">
                            <table width="850Px">
                                <tr align="center">
                                    <td align="center" style="text-align: center; font-size: 20px; font-weight: bold;">
                                        <asp:Label ID="lblCompany" runat="server" ForeColor="Black" Font-Italic="true"></asp:Label>
                                    </td>
                                </tr>
                                <%--<tr align="center">
                                    <td align="center" style="text-align: center; font-size: 20px; font-weight: bold;">
                                        <asp:Label ID="Label67" runat="server" Text="Business : " Font-Italic="true"></asp:Label>
                                        <asp:Label ID="lblBusiness" runat="server" ForeColor="Black" Font-Italic="true"></asp:Label>
                                    </td>
                                </tr>--%>
                                <tr align="center">
                                    <td align="center" style="text-align: center; font-size: 18px; font-weight: bold;">
                                        <asp:Label ID="Label24" runat="server" Font-Italic="true" Text="List of Customers"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td colspan="4" align="left">
                        <asp:Panel ID="Panel11" runat="server" Width="100%" Height="600px" ScrollBars="Horizontal">
                       
                                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false" DataKeyNames="CompanyId" EmptyDataText="No Record Found." AllowPaging="True" Width="100%" Height="500px" PageSize="15" 
                                        OnPageIndexChanging="GridView1_PageIndexChanging" OnRowCommand="GridView1_RowCommand" OnRowDeleting="GridView1_RowDeleting" OnRowEditing="GridView1_RowEditing1" OnRowDataBound="GridView1_RowDataBound">
                                        <Columns>               
                                               <asp:TemplateField ItemStyle-Width="100%" HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <table width="100%" cellpadding="0" cellspacing="0">
                                                        <tr style="height:8px;border:1px solid black;">
                                                            <td align="center" style="background:#ffe793;font-size:25px;font-weight:bold;">
                                                                
                                                            </td>
                                                            <td align="center" style="background:#ffe793;font-size:25px;font-weight:bold;">
                                                             <asp:Label ID="lblcompanlogin" runat="server" Text='<%# Bind("CompanyLoginId")%>'></asp:Label>
                                                            </td>
                                                        </tr>     
                                                         <tr style="height:8px;border:1px solid black;">
                                                            <td style="width:20%;">
                                                                <label>
                                                                 Portal Name
                                                                 </label> 
                                                            </td>
                                                            <td>
                                                                 <asp:Label ID="Label10" runat="server" Text='<%# Bind("PortalName")%>' Visible="false"></asp:Label>                                                               
                                                            </td>
                                                        </tr>                                                                                                          
                                                        <tr style="height:8px;border:1px solid black;">
                                                            <td style="width:20%;">
                                                                <label>
                                                                 Priceplan Name
                                                                 </label> 
                                                            </td>
                                                            <td>
                                                                 <asp:Label ID="lbl_planid" runat="server" Text='<%# Bind("PricePlanId")%>' Visible="false"></asp:Label>
                                                                 <asp:LinkButton ID="LinkButton1" runat="server" CommandName="viewplan"  CommandArgument='<%# Eval("PricePlanId") %>' Text='<%# Bind("PricePlanName")%>' Style="color: #000;"  ></asp:LinkButton>
                                                            </td>
                                                        </tr>
                                                        <tr style="height:8px;border:5px solid black;">
                                                            <td>
                                                            <label>
                                                            Contact Person
                                                            </label> 
                                                            </td>
                                                            <td>
                                                                 <asp:Label ID="lblContactPerson" runat="server" Text='<%# Bind("ContactPerson")%>'></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <label>
                                                                 Company Name
                                                                 </label>  
                                                            </td>
                                                            <td>
                                                                  <asp:Label ID="lblemaildisplaynameName" runat="server" Text='<%# Bind("CompanyName")%>'></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <label>
                                                                Phone No
                                                                </label>
                                                            </td>
                                                            <td>
                                                                  <asp:Label ID="lblphone" runat="server" Text='<%# Bind("phone")%>'></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                            <label>
                                                               Email
                                                                </label>
                                                            </td>
                                                            <td>
                                                             <asp:Label ID="Email" runat="server" Text='<%# Bind("Email")%>'></asp:Label>
                                                            </td>
                                                        </tr>
                                                         <tr>
                                                            <td>
                                                                <label>
                                                                    LicenseDate
                                                                </label> 
                                                                
                                                            </td>
                                                            <td>
                                                             <asp:Label ID="lbl_LicenseDate" runat="server" Text='<%# Bind("LicenseDate")%>'></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <label>
                                                                    License Period
                                                                </label> 
                                                            </td>
                                                            <td>
                                                                          <asp:Label ID="lbl_noofday" runat="server" Text='<%# Bind("LicensePeriod")%>'></asp:Label>
                                                                           <asp:Label ID="lbllicensedathide" runat="server" Text='<%# Bind("LicenseDate")%>' Visible="false"></asp:Label>
                                                                             <asp:Label ID="lblserverid" runat="server" Text='<%# Bind("ServerId")%>' Visible="false" ></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <label>
                                                                   License End Date
                                                                </label> 
                                                            </td>
                                                            <td>
                                                                 <asp:Label ID="lbl_enddate" runat="server" Text=""></asp:Label>
                                                            </td>
                                                        </tr>
                                                       <tr>
                                                            <td colspan="2">
                                                                 <label>
                                                                 Delete
                                                                <asp:ImageButton ID="imgbtndelete" runat="server" Height="25px"  CommandArgument='<%# Eval("CompanyId") %>' ToolTip="Delete" CommandName="Delete" ImageUrl="~/images/deletegreen.png" />
                                                                <asp:ImageButton ID="imgbtnrestore" runat="server" Height="25px" CommandArgument='<%# Eval("CompanyId") %>' ToolTip="Restore" CommandName="Restore" ImageUrl="~/images/cantaccess.jpg" Visible="false" Enabled="false"  />
                                                                </label> 
                                                                <label>                                                                    
                                                                    Backup
                                                                      <asp:ImageButton ID="imgbtnbackup" runat="server" Height="25px"  CommandArgument='<%# Eval("CompanyId") %>' ToolTip="Manual backup" CommandName="backup" ImageUrl="~/images/backupgreen.png" />                                                     
                                                                        <asp:ImageButton ID="imgbcant" Enabled="false"  runat="server" Height="25px"  ToolTip="Cant Backup"  ImageUrl="~/images/cantaccess.jpg" Visible="false" />                                                    
                                                                </label>
                                                                <label>
                                                                 <asp:Panel ID="pnl_payd" runat="server" Visible="false">
                                                                     <asp:ImageButton ID="imgbtnrestoremanual" runat="server" Height="20px" CommandArgument='<%# Eval("CompanyId") %>' ToolTip="Restore" CommandName="Restore" ImageUrl="~/images/restor.jpg" Visible="false"  />
                                                                            <asp:ImageButton ID="imgbcantrest" Enabled="false"  runat="server" Height="25px"  ToolTip="Can't Restore"  ImageUrl="~/images/cantaccess.jpg"  />                                                    
                                                                            </asp:Panel>
                                                                </label>  
                                                            </td>
                                                       </tr>
                                                        <tr>
                                                            <td colspan="2" style="background:#416271">
                                                            <br />
                                                            </td>
                                                        </tr>
                                                    </table> 
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle Width="100%" />
                                            </asp:TemplateField>  
                                            
                                        </Columns>
                                      
                                       
                                    </asp:GridView>
                        </asp:Panel>
                        <input id="hdnsortExp" runat="Server" name="hdnsortExp" style="width: 1px" type="hidden" />
                        <input id="hdnsortDir" runat="Server" name="hdnsortDir" style="width: 1px" type="hidden" />
                    </td>
                </tr>
            </table>
        </asp:Panel>
    </fieldset>

       <asp:Panel ID="pnl_paydetail" runat="server" Visible="false">
             <div style="position: absolute; margin: 1550px 0px 0px 230px; width:840px; background-color: #FFF;" class="Box" >
           
              <fieldset>
        
         <asp:Panel ID="pnl_licence" runat="server">
           <%--  <div style="position: absolute; margin: -00px 0px 0px 350px; height:600px; width:840px; background-color: #A4A4A4;" class="Box" >--%>
           <div>
              <legend>
            <asp:Label ID="lbltitle" runat="server" Text=" PricePlan Detail "></asp:Label>
        </legend>
        <table style="width:100%">
        <tr>
        <td style="width:92%">
            <asp:Label ID="lblmsgn" runat="server" Text="  "></asp:Label>
        </td>            
        <td  style="width:8%">
        <asp:Button ID="Button4" runat="server" Text=" X "   style="color: #FFFFFF; background-color: #FF0000; height: 26px;"  onclick="Button4_Click" />
        </td>
        </tr>
        </table>      
        <fieldset>
        
                 <asp:Panel ID="pnlview" runat="server">                  
            <table width="100%">
               <tr>
                <td class="font" style="width:250px;">
                    <asp:Label ID="Label12" runat="server" Text="Portal Name"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lblportalname" runat="server"></asp:Label>
                </td>
                <td>
                        &nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>               
                <tr>
                    <td class="font">
                        <asp:Label ID="Label13" runat="server" Text="Priceplan Category "></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblcategory" runat="server"></asp:Label>
                    </td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                </tr>
                <tr>
                    <td class="font">
                        <asp:Label ID="Label124" runat="server" Text="Plan Name"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lbl_planname" runat="server"></asp:Label>
                    </td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                </tr>
                <tr>
                    <td class="font"valign="top" colspan="2">
                       <asp:Label ID="lbldate" runat="server" Text=" What is Included in this price plan "></asp:Label>
                    </td>
                    <td>
                        
                    </td>
                   
                    <td>
                        &nbsp;</td>
                </tr>
                <tr>
                <td colspan="4">  
                   <asp:GridView ID="gvpopup" runat="server" ShowHeader="false" CssClass="mGridcss" PagerStyle-CssClass="pgr" AutoGenerateColumns="false" DataKeyNames="Id" onrowcommand="gvpopup_RowCommand">
                                                            <Columns>
                                                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblfe1" runat="server" Text='<%# Bind("NameofRest1") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField ItemStyle-Width="82px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" HeaderText="">
                                                                    <ItemTemplate>
                                                                       <asp:Label ID="Label1" runat="server" Text='<%# Bind("RecordsAllowed") %>'></asp:Label>
                                                                         <asp:Label ID="lblid" runat="server" Text='<%# Bind("id") %>' Visible="false"></asp:Label>
                                                                         <asp:Label ID="lblcid" runat="server" Text='<%# Bind("portalid") %>' Visible="false"></asp:Label>
                                                                         
                                                                         <asp:Label ID="lblnorec" Visible="false" runat="server"> </asp:Label>
                                                                             <asp:Label ID="lblval" Visible="false" runat="server"> </asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField ItemStyle-Width="82px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" HeaderText="" Visible="false">
                                                                    <ItemTemplate>
                                                                          <asp:button id="Button2" cssclass="BTNTRANS" runat="server" text="Change" CommandName="GetRow" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>                                                             
                                                            </Columns>
                                                             <PagerStyle CssClass="pgr" />
                                                            <AlternatingRowStyle CssClass="alt" />
                                                        </asp:GridView>   
                </td>
                </tr>
            </table>
            </asp:Panel>
            </fieldset>
              
              </div>
      </asp:Panel>      

            </fieldset>
              
              </div>
      </asp:Panel>


      <asp:Panel ID="pnl_popupdeleterestore" runat="server"  Visible="false">
             <div style="position: absolute; margin: -350px 0px 0px 230px; width:840px; background-color:#ffe793;" class="Box">           
              <fieldset>   
              <legend>
            <asp:Label ID="lblmsgdeleterestore" runat="server" Text=" Are you sure to delete this company's code and database ? "></asp:Label>
             <asp:Label ID="lblcompanyid" runat="server" Text="" Visible="false"></asp:Label>
             <asp:Label ID="lblbackup" runat="server" Text="no" Visible="false"></asp:Label>
                     <asp:Label ID="lblserverid" runat="server" Text="" Visible="false"></asp:Label>
        </legend>
        <table style="width:100%">
        <tr>
        <td style="width:92%">
            
        </td>            
        <td  style="width:8%">
        <asp:Button ID="Btn_popupdeleterestore" runat="server" Text=" X "   style="color: #FFFFFF; background-color: #FF0000; height: 26px;"  onclick="Btn_popupdeleterestore_Click" />
        </td>
        </tr>
        </table>      
             <fieldset>
              <asp:Panel ID="pnldeletereson" runat="server" Visible="false">                  
            <table width="100%">
               <tr>
                     <td class="font" colspan="4">   
                     <asp:Label ID="lblpnldeleteresonmsg" runat="server" Text="" ForeColor="Red"></asp:Label>                 
                        </td>                
               </tr>
                           
                <tr>
                    <td class="font" valign="top" colspan="4" >
                        <asp:Label ID="Label11" runat="server" Text="Please give reason to delete this company "></asp:Label>
                        <br />

                         <asp:TextBox ID="txtreson" runat="server" Height="70px" Width="100%" TextMode="MultiLine"></asp:TextBox>   
                    </td>
                    
                </tr>
             
                <tr>
                    <td colspan="4">
                       <asp:Button ID="Button3" runat="server" CssClass="btnSubmit" Text="Conform " OnClick="btndelete_Click" />                       
                    </td>                   
                   
                </tr>
              
              </table>
            </asp:Panel>
             
                 <asp:Panel ID="pnlGridcodeDatabase" runat="server" Visible="false">                  
                <table width="100%">
                  <tr>
                      <td colspan="4">

                     </td>
                 </tr>
                    <tr>
                    <td colspan="4">  
                       <asp:GridView ID="gvdatabase" runat="server" ShowHeader="true" 
                           CssClass="mGrid"  PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt" 
                           AutoGenerateColumns="false" DataKeyNames="Id" onrowcommand="gvdatabase_RowCommand" OnRowDataBound="gvdatabase_RowDataBound">
                                                                <Columns>
                                                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="Database Detail" HeaderStyle-Width="80%">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblcode" runat="server" Text='<%# Bind("CodeType") %>'></asp:Label>
                                                                            "-"
                                                                           <asp:Label ID="Label1" runat="server" Text='<%# Bind("DatabaseName") %>'></asp:Label>
                                                                                <asp:ImageButton ID="imgbtnsuccess" runat="server" Height="15px"   ToolTip="Successfully"  ImageUrl="~/images/Right1.jpg" Visible="false" />
                                                                                <asp:ImageButton ID="imgbtnunsucccess" runat="server" Height="15px"  ToolTip="Restore" CommandName="Restore" ImageUrl="~/images/Wrong1.jpg" Visible="false" />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField  HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" HeaderText="DatabaseName" Visible="false">
                                                                        <ItemTemplate>
                                                                       
                                                                             <asp:Label ID="lblid" runat="server" Text='<%# Bind("Id") %>' Visible="false"></asp:Label>
                                                                             <asp:Label ID="lblcom" runat="server" Text='<%# Bind("CompanyLoginId") %>' Visible="false"></asp:Label>
                                                                         
                                                                             <asp:Label ID="lblnorec" Visible="false" runat="server"> </asp:Label>
                                                                                 <asp:Label ID="lblval" Visible="false" runat="server"> </asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                                                                          
                                                                </Columns>
                                                                 <PagerStyle CssClass="pgr" />
                                                        <AlternatingRowStyle CssClass="alt" />
                                                            </asp:GridView>   
                    </td>
                    </tr>
                    <tr>
                    <td colspan="4">
                    <asp:GridView ID="gvcode" runat="server" ShowHeader="true" 
                     CssClass="mGrid"  PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt"  AutoGenerateColumns="false" DataKeyNames="Id" onrowcommand="gvcode_RowCommand" OnRowDataBound="gvcode_RowDataBound">
                                                                <Columns>

                                                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="Code Detail" HeaderStyle-Width="80%" >
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblcode" runat="server" Text='<%# Bind("CodeType") %>'></asp:Label> -
                                                                            <asp:Label ID="Label1" runat="server" Text='<%# Bind("WebsiteURL") %>'></asp:Label>
                                                                                <asp:ImageButton ID="imgbtnsuccess" runat="server" Height="15px"  ToolTip="Success"  ImageUrl="~/images/Right1.jpg" Visible="false"   />
                                                                                <asp:ImageButton ID="imgbtnunsucccess" runat="server" Height="15px"  ToolTip="Restore" CommandName="Restore" ImageUrl="~/images/Wrong.jpg" Visible="false" />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                
                                                                    <asp:TemplateField  HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" HeaderText="WebsiteURL"  Visible="false">
                                                                        <ItemTemplate>                                                                       
                                                                             <asp:Label ID="lblid" runat="server" Text='<%# Bind("Id") %>' Visible="false"></asp:Label>
                                                                             <asp:Label ID="lblcom" runat="server" Text='<%# Bind("CompanyLoginId") %>' Visible="false"></asp:Label>
                                                                             <asp:Label ID="lblnorec" Visible="false" runat="server"> </asp:Label>
                                                                                 <asp:Label ID="lblval" Visible="false" runat="server"> </asp:Label>                                                                            
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                
                                                                </Columns>
                                                                 <PagerStyle CssClass="pgr" />
                                                                    <AlternatingRowStyle CssClass="alt" />
                                                            </asp:GridView>
                    </td>
                    </tr>
                    <tr>
                        <td>
                         
                        </td>
                    </tr>
                    <tr>
                    <td></td>
                    <td colspan="3">
                     <asp:Button ID="btnconformDelete" runat="server" CssClass="btnSubmit" Text="Yes " OnClick="btnconformDelete_Click" />
                           <asp:Button ID="btnrestore" Visible="false"  runat="server" CssClass="btnSubmit" Text="Restore " OnClick="btnrestor_Click" />  
                    </td>
                    </tr>
                </table>
            </asp:Panel>
            </fieldset>
      </fieldset>
              
              </div>
      </asp:Panel>

        <asp:Panel ID="pnlbackupstatus" runat="server"  Visible="false">
             <div style="position: absolute; margin: -750px 0px 0px 230px; width:840px; background-color: #FFF;" class="Box" >
              <fieldset>                 
                  <legend>
                    <asp:Label ID="Label9" runat="server" Text="Restore the Code and Database from backup "></asp:Label>             
                 </legend>
                 <table>
                 <tr>
                 <td align="right">
                  <asp:Button ID="Button52" runat="server" Text=" X "   style="color: #FFFFFF; background-color: #FF0000; height: 26px;"  onclick="Btn_pnlbackupstatus_Click" />
                 </td>
                 </tr>
                 <tr>
                 <td>
                        <asp:GridView ID="gvbackupstatus" runat="server" ShowHeader="true" Width="800px"  
                     CssClass="mGrid"  PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt"  AutoGenerateColumns="false" DataKeyNames="Id" onrowcommand="gvbackupstatus_RowCommand" OnRowDataBound="gvbackupstatus_RowDataBound">
                                                                <Columns>
                                                                   <asp:BoundField DataField="ServerName" HeaderText="ServerName"  />
                                                                    <asp:BoundField DataField="FTPUserId" HeaderText="FTPUserId"  />
                                                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="Company Id" >
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblcomid" runat="server" Text='<%# Bind("CompanyID") %>'></asp:Label> 
                                                                            <asp:Label ID="lblrestoid" runat="server" Visible="false"  Text='<%# Bind("ID") %>'></asp:Label> 
                                                                            <asp:Label ID="lblftpId" runat="server" Visible="false"  Text='<%# Bind("id") %>'></asp:Label> 

                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:BoundField DataField="Date" HeaderText="Start Date" DataFormatString="{0:dd-M-yyyy}" />
                                                                    <asp:BoundField DataField="Time" HeaderText="Date"  />
                                                                    <asp:BoundField DataField="BackupFinishedDate" HeaderText="End Date" DataFormatString="{0:dd-M-yyyy}" />
                                                                    <asp:BoundField DataField="BackupFinishedTime" HeaderText="End Time"  />                                                                  
                                                                  
                                                                    <asp:TemplateField  HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" HeaderText="Restore" >
                                                                        <ItemTemplate>                                                                       
                                                                            <asp:ImageButton ID="imgbtnrestore" runat="server" Height="20px" CommandArgument='<%# Eval("CompanyId") %>' ToolTip="Restore" CommandName="Restore" ImageUrl="~/images/restor.jpg"  />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                
                                                                </Columns>
                                                                 <PagerStyle CssClass="pgr" />
                                                                    <AlternatingRowStyle CssClass="alt" />
                                                            </asp:GridView>
                 </td>
                 </tr>
                 </table> 
                       
            </fieldset>
               </div>
           </asp:Panel>
         
    </td>
    </tr>
    
    </table> 
    
            
</asp:Content>
