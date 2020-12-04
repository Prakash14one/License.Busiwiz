<%@ Page Language="C#" MasterPageFile="~/Master/LicenseMaster.master"
    AutoEventWireup="true" CodeFile="OverHeadAllocation.aspx.cs" Inherits="ShoppingCart_Admin_OverHeadAllocation"
    Title="Untitled Page" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="PagingGridView" Namespace="Fadrian.Web.Control" TagPrefix="cc11" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

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
          function CallPrint1(strid) {
            var prtContent = document.getElementById('<%= pnlgrid1.ClientID %>');
            var WinPrint = window.open('', '', 'left=0,top=0,width=900,height=600,toolbar=1,scrollbars=1,status=0');
            WinPrint.document.write(prtContent.innerHTML, '<link rel="stylesheet" type="text/css" href="css/forms.css" media="screen" />');
            WinPrint.document.close();
            WinPrint.focus();
            WinPrint.print();
            WinPrint.close();

        }  


   function mask(evt)
        { 
         
           if(evt.keyCode==13 )
            { 
         
                  return true;
             }
            
           
            if(evt.keyCode==188 ||evt.keyCode==191 ||evt.keyCode==192 ||evt.keyCode==109 || evt.keyCode==221 || evt.keyCode==220 ||evt.keyCode==222 ||evt.keyCode==219 ||evt.keyCode==59||evt.keyCode==186  )
            { 
                
            
              alert("You have entered invalid character");
                  return false;
             }     
            
        }   
         function check(txt1, regex, reg,id,max_len)
            {
            if (txt1.value != '' && txt1.value.match(reg) == null) 
            {
                txt1.value = txt1.value.replace(regex, '');
                alert("You have entered invalid character");
            }   
        
            counter=document.getElementById(id);
            
            if(txt1.value.length <= max_len)
            {
                remaining_characters=max_len-txt1.value.length;
                counter.innerHTML=remaining_characters;
            }
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
    </style>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="products_box">
                <%-- <fieldset>--%>
                <div style="float: left;">
                    &nbsp;<asp:Label ID="lblmsg" runat="server" ForeColor="Red" Visible="False"></asp:Label>
                </div>
                <div style="clear: both;">
                </div>
                <fieldset>
                    <legend>
                        <asp:Label ID="lbladitio" runat="server"></asp:Label></legend>
                    <div style="float: right;">
                        <asp:Button ID="btnovea" runat="server" CssClass="btnSubmit" Text="Add overhead allocation"
                            OnClick="btnovea_Click" />
                    </div>
                    <asp:Panel ID="addinventoryroom" runat="server" Visible="False">
                        <table>
                            <tr>
                                <td>
                                    <label>
                                        A)
                                    </label>
                                </td>
                                <td>
                                    <label>
                                        <asp:Label ID="Label1" runat="server" Text="Business Name"></asp:Label>
                                    </label>
                                </td>
                                <td>
                                    <label>
                                        <asp:DropDownList ID="ddlstorename" runat="server" OnSelectedIndexChanged="ddlstorename_SelectedIndexChanged"
                                            AutoPostBack="True">
                                        </asp:DropDownList>
                                    </label>
                                </td>
                            </tr>
                        </table>
                        <table>
                            <tr>
                                <td>
                                    <label>
                                        B)
                                    </label>
                                </td>
                                <td>
                                    <label>
                                        <asp:Label ID="Label5" runat="server" Text="Do you want to allocate overheads since the date of last allocation ? "></asp:Label>
                                    </label>
                                </td>
                                <td>
                                    <asp:CheckBox ID="chkb" runat="server" Checked="true" AutoPostBack="True" OnCheckedChanged="chkb_CheckedChanged" />
                                </td>
                            </tr>
                        </table>
                        <table>
                            <tr>
                                <td style="width: 10%">
                                </td>
                                <td>
                                    <label>
                                        <asp:Label ID="Label6" runat="server" Text="Select the dates for collection of overheads for alloation"></asp:Label>
                                    </label>
                                </td>
                            </tr>
                        </table>
                        <table>
                            <tr>
                                <td style="width: 10%">
                                </td>
                                <td>
                                    <label>
                                        <asp:Label ID="lbldatefrom" runat="server" Text="From Date"></asp:Label>
                                    </label>
                                </td>
                                <td>
                                    <label>
                                        <asp:TextBox ID="txtdatefrom" runat="server" Width="100px"></asp:TextBox>
                                        <cc1:CalendarExtender ID="CalendarExtender3" runat="server" PopupButtonID="imgbtncal"
                                            TargetControlID="txtdatefrom">
                                        </cc1:CalendarExtender>
                                    </label>
                                </td>
                                <td>
                                    <label>
                                        <asp:ImageButton ID="imgbtncal" runat="server" ImageUrl="~/ShoppingCart/images/cal_actbtn.jpg" />
                                    </label>
                                </td>
                                <td>
                                    <label>
                                        <asp:Label ID="lbldateto" runat="server" Text="To  Date"></asp:Label>
                                    </label>
                                </td>
                                <td>
                                    <label>
                                        <asp:TextBox ID="txtdateto" runat="server" Width="100px"></asp:TextBox>
                                        <cc1:CalendarExtender ID="CalendarExtender1" runat="server" PopupButtonID="ImageButton1"
                                            TargetControlID="txtdateto">
                                        </cc1:CalendarExtender>
                                    </label>
                                </td>
                                <td>
                                    <label>
                                        <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/ShoppingCart/images/cal_actbtn.jpg" />
                                    </label>
                                    <label>
                                        <asp:CompareValidator ID="cvf" runat="server" Operator="GreaterThanEqual" Type="Date"
                                            ControlToCompare="txtdatefrom" ControlToValidate="txtdateto" Display="Dynamic"
                                            ErrorMessage="*" SetFocusOnError="true"></asp:CompareValidator>
                                    </label>
                                </td>
                            </tr>
                        </table>
                        <table>
                            <tr>
                                <td>
                                    <label>
                                        C)</label>
                                </td>
                                <td>
                                    <label>
                                        <asp:Label ID="Label7" runat="server" Text="Do you want to apply overheads  based on previously used allocation methods ?"></asp:Label>
                                    </label>
                                </td>
                                <td>
                                    <asp:CheckBox ID="CheckBox1" runat="server" Checked="true" AutoPostBack="True" OnCheckedChanged="CheckBox1_CheckedChanged" />
                                </td>
                            </tr>
                        </table>
                        <asp:Panel ID="pnlc" runat="server" Visible="false">
                            <table>
                                <tr>
                                    <td style="width: 10%">
                                    </td>
                                    <td>
                                        <label>
                                            <asp:Label ID="Label14" runat="server" Text="Select the previously used allocation methods"></asp:Label>
                                        </label>
                                    </td>
                                    <td>
                                        <label>
                                            <asp:DropDownList ID="ddlpriviallocat" runat="server">
                                            </asp:DropDownList>
                                        </label>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                        <table>
                            <tr>
                                <td>
                                    <label>
                                        D)</label>
                                </td>
                                <td>
                                    <label>
                                        <asp:Label ID="Label2" runat="server" Text=" Overhead allocation title"></asp:Label>
                                    </label>
                                </td>
                                <td>
                                    <label>
                                        <asp:TextBox ID="txtoverheadname" runat="server" MaxLength="200" Width="421px"></asp:TextBox>
                                    </label>
                                </td>
                            </tr>
                        </table>
                        <table>
                            <tr>
                                <td style="width: 20%">
                                </td>
                                <td>
                                    <asp:Button ID="Button3" runat="server" CssClass="btnSubmit" OnClick="Button3_Click"
                                        Text="Go" />
                                </td>
                            </tr>
                        </table>
                        <div style="float: right">
                            <input id="Button2a" runat="server" class="btnSubmit" onclick="document.getElementById('mydiv').className='open';javascript:CallPrint('divPrint');document.getElementById('mydiv').className='closed';"
                                style="width: 51px;" type="button" value="Print" visible="true" />
                        </div>
                        <div style="clear: both;">
                        </div>
                        <asp:Panel ID="pnlgrid" runat="server" Width="100%">
                            <table width="100%">
                                <tr>
                                    <td>
                                        <div id="mydiv" class="closed">
                                            <table width="100%">
                                                <tr align="center">
                                                    <td align="center" style="text-align: center; color: Navy; font-size: 20px; font-weight: bold;">
                                                        <asp:Label ID="lblmb" runat="server" ForeColor="Black" Font-Italic="true"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr align="center">
                                                    <td align="center" style="text-align: center; font-size: 18px; font-weight: bold;">
                                                        <asp:Label ID="lblovern" runat="server" Font-Italic="true" Text=""></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="left">
                                                        <asp:Label ID="lblbbbbbb" runat="server" Text="" Font-Size="16px" Font-Italic="true"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                        <asp:Panel ID="pnladdval" runat="server" Visible="False">
                                            <table id="Table1" style="width: 100%">
                                                <tr>
                                                    <td>
                                                        <div>
                                                            <asp:Panel ID="Panel3" runat="server" Visible="False">
                                                                <fieldset>
                                                                    <legend>
                                                                        <asp:Label ID="Label3" runat="server" Text="List of Overhead Titles"></asp:Label>
                                                                    </legend>
                                                                    <div style="clear: both;">
                                                                        <label>
                                                                            <asp:Button ID="btnaccogroup" runat="server" OnClick="btnaccogroup_Click" Text="Select Accounting Groups to be Considered for Overhead Allocation." />
                                                                        </label>
                                                                        <label>
                                                                            <asp:Button ID="btningr" runat="server" OnClick="btningr_Click" Text="Insert" Visible="false" />
                                                                        </label>
                                                                    </div>
                                                                    <div style="clear: both;">
                                                                        <asp:Panel ID="pnlgrop" runat="server" Visible="false">
                                                                            <table>
                                                                                <tr>
                                                                                    <td>
                                                                                        <asp:CheckBoxList ID="chkgroup" runat="server" RepeatColumns="4" RepeatDirection="Horizontal">
                                                                                        </asp:CheckBoxList>
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </asp:Panel>
                                                                    </div>
                                                                    <div style="clear: both;">
                                                                    </div>
                                                                    <asp:GridView ID="grdaccount" runat="server" AllowPaging="True" AllowSorting="True"
                                                                        AlternatingRowStyle-CssClass="alt" AutoGenerateColumns="False" CssClass="mGrid"
                                                                        DataKeyNames="Id" EmptyDataText="No Record Found." GridLines="Both" OnPageIndexChanging="grdaccount_PageIndexChanging"
                                                                        PagerStyle-CssClass="pgr">
                                                                        <Columns>
                                                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Select">
                                                                                <ItemTemplate>
                                                                                    <asp:CheckBox ID="chkinvMasterStatus" runat="server" Checked="true" />
                                                                                </ItemTemplate>
                                                                                <ItemStyle Width="30px" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Group Name">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblgroupid" runat="server" Text='<%#Bind("groupid") %>' Visible="false"></asp:Label>
                                                                                    <asp:Label ID="lblgroupname" runat="server" Text='<%#Bind("groupdisplayname") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Account Name">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblaccountmasterid" runat="server" Text='<%#Bind("Id") %>' Visible="false"></asp:Label>
                                                                                    <asp:Label ID="lblaccountname" runat="server" Text='<%#Bind("AccountName") %>'></asp:Label>
                                                                                    <asp:Label ID="lblaccountid" runat="server" Text='<%#Bind("AccountId") %>' Visible="false"></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Selected Period Amount">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblamount" runat="server"></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Amount to be Allocated">
                                                                                <ItemTemplate>
                                                                                    <asp:TextBox ID="txtamountallocate" runat="server" Text="0"></asp:TextBox>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Select Allocation Method">
                                                                                <ItemTemplate>
                                                                                    <asp:DropDownList ID="ddlallocation" runat="server">
                                                                                    </asp:DropDownList>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                        </Columns>
                                                                    </asp:GridView>
                                                                </fieldset></asp:Panel>
                                                        </div>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <div>
                                                            <asp:Panel ID="Panel4" runat="server" Visible="False">
                                                                <fieldset>
                                                                    <legend>
                                                                        <asp:Label ID="Label4" runat="server" Text=" List of Work Order/ Projects During Selected Period"></asp:Label>
                                                                    </legend>
                                                                    <div style="clear: both;">
                                                                        <label>
                                                                            <asp:Button ID="btnselectcol" runat="server" OnClick="btnselectcol_Click" Text="Select Columns" />
                                                                        </label>
                                                                        <label>
                                                                            <asp:Button ID="btnfillcol" runat="server" OnClick="btnfillcol_Click" Text="Refresh" />
                                                                        </label>
                                                                    </div>
                                                                    <div style="clear: both;">
                                                                        <asp:Panel ID="pnladdcol" runat="server" Visible="false">
                                                                            <table>
                                                                                <tr>
                                                                                    <td>
                                                                                        <asp:CheckBoxList ID="CheckBoxList1" runat="server" RepeatColumns="4" RepeatDirection="Horizontal"
                                                                                            Width="100%">
                                                                                            <asp:ListItem Selected="True" Value="0">Active</asp:ListItem>
                                                                                            <asp:ListItem Selected="True" Value="1">Work Order(Project)No.</asp:ListItem>
                                                                                            <asp:ListItem Selected="True" Value="2">Work Order (Project) Name</asp:ListItem>
                                                                                            <asp:ListItem Value="3">Start Date</asp:ListItem>
                                                                                            <asp:ListItem Value="4">End Date</asp:ListItem>
                                                                                            <asp:ListItem Value="5">Work Order (Project) Direct Material Cost</asp:ListItem>
                                                                                            <asp:ListItem Selected="True" Value="6">Direct Material Cost for the Period</asp:ListItem>
                                                                                            <asp:ListItem Value="7">Direct Labour Cost For Select Period</asp:ListItem>
                                                                                            <asp:ListItem Selected="True" Value="8">Direct Labour Costs for the Period</asp:ListItem>
                                                                                            <asp:ListItem Selected="True" Value="9">Active Project Days for the Period</asp:ListItem>
                                                                                            <asp:ListItem Selected="True" Value="10">Overhead by Material Cost Ratio</asp:ListItem>
                                                                                            <asp:ListItem Selected="True" Value="11">Overhead by Labour Cost Ratio</asp:ListItem>
                                                                                            <asp:ListItem Selected="True" Value="12">Overhead by Active Days Ratio</asp:ListItem>
                                                                                            <asp:ListItem Selected="True" Value="13">Overhead Allocated Equally to all 
                                                                                            Projects</asp:ListItem>
                                                                                            <asp:ListItem Selected="True" Value="14">Total Overhead</asp:ListItem>
                                                                                        </asp:CheckBoxList>
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </asp:Panel>
                                                                    </div>
                                                                    <div style="clear: both;">
                                                                    </div>
                                                                    <asp:GridView ID="grdjob" runat="server" AllowSorting="True" AlternatingRowStyle-CssClass="alt"
                                                                        AutoGenerateColumns="False" CssClass="mGrid" DataKeyNames="Id" EmptyDataText="No Record Found."
                                                                        GridLines="Both" OnPageIndexChanging="grdjob_PageIndexChanging" PagerStyle-CssClass="pgr"
                                                                        ShowFooter="True" Width="100%">
                                                                        <Columns>
                                                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Apply">
                                                                                <ItemTemplate>
                                                                                    <asp:CheckBox ID="chkjobmaster" runat="server" Checked="true" />
                                                                                </ItemTemplate>
                                                                                <HeaderStyle HorizontalAlign="Left" />
                                                                                <ItemStyle Width="30px" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left">
                                                                                <HeaderTemplate>
                                                                                    <asp:Label ID="lblheadjobno" runat="server" Text="Work Order  No." ToolTip="Work Order (Project) No."></asp:Label>
                                                                                </HeaderTemplate>
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lbljobno" runat="server" Text='<%#Bind("JobNumber") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                                <HeaderStyle HorizontalAlign="Left" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left">
                                                                                <HeaderTemplate>
                                                                                    <asp:Label ID="lblheadjobname" runat="server" Text="Work Order Name" ToolTip="Work Order (Project) Name"></asp:Label>
                                                                                </HeaderTemplate>
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lbljobname" runat="server" Text='<%#Bind("JobName") %>'></asp:Label>
                                                                                    <asp:Label ID="lbljobmasterid" runat="server" Text='<%#Bind("Id") %>' Visible="false"></asp:Label>
                                                                                    <asp:Label ID="lblinvmasterid" runat="server" Text='<%#Bind("InvWMasterId") %>' Visible="false"></asp:Label>
                                                                                </ItemTemplate>
                                                                                <HeaderStyle HorizontalAlign="Left" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Start Date">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblstartdate" runat="server" Text='<%#Bind("JobStartDate","{0:dd/MM/yyy}") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                                <HeaderStyle HorizontalAlign="Left" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="End Date">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblenddate" runat="server" Text='<%#Bind("JobEndDate","{0:dd/MM/yyy}") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                                <HeaderStyle HorizontalAlign="Left" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left">
                                                                                <HeaderTemplate>
                                                                                    <asp:Label ID="lblheadtotdmc" runat="server" Text="Total DMC" ToolTip="Work Order (Project) Direct Material Cost"></asp:Label>
                                                                                </HeaderTemplate>
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lbldirectmaterial" runat="server"></asp:Label>
                                                                                </ItemTemplate>
                                                                                <HeaderStyle HorizontalAlign="Left" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left">
                                                                                <HeaderTemplate>
                                                                                    <asp:Label ID="lblheadperioddmc" runat="server" Text="DMC For Period" ToolTip="Work Order (Project) Material Cost For Selected Period "></asp:Label>
                                                                                </HeaderTemplate>
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lbldirectmaterialperiod" runat="server"></asp:Label>
                                                                                </ItemTemplate>
                                                                                <HeaderStyle HorizontalAlign="Left" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left">
                                                                                <HeaderTemplate>
                                                                                    <asp:Label ID="lblheadtotaldlc" runat="server" Text="Total DLC" ToolTip="Direct Labour Cost For Select Period "></asp:Label>
                                                                                </HeaderTemplate>
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lbldirectlabour" runat="server"></asp:Label>
                                                                                </ItemTemplate>
                                                                                <HeaderStyle HorizontalAlign="Left" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left">
                                                                                <HeaderTemplate>
                                                                                    <asp:Label ID="lblheadPERIODdlc" runat="server" Text="DLC For Period" ToolTip="Direct Labour Cost for the Period"></asp:Label>
                                                                                </HeaderTemplate>
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lbldirectlabourperiod" runat="server"></asp:Label>
                                                                                </ItemTemplate>
                                                                                <HeaderStyle HorizontalAlign="Left" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Total Project Duration " Visible="false">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblnoofdays" runat="server" HeaderStyle-HorizontalAlign="Left"></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left">
                                                                                <HeaderTemplate>
                                                                                    <asp:Label ID="lblheadPERIODPD" runat="server" Text="Active Proj. Days" ToolTip="Active Project Days for the Period"></asp:Label>
                                                                                </HeaderTemplate>
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblnoofdaysperiod" runat="server"></asp:Label>
                                                                                </ItemTemplate>
                                                                                <HeaderStyle HorizontalAlign="Left" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left">
                                                                                <HeaderTemplate>
                                                                                    <asp:Label ID="lblheadoverheadbyperiod" runat="server" Text="Overhead by Material"
                                                                                        ToolTip="Overhead by Material Cost Ratio"></asp:Label>
                                                                                </HeaderTemplate>
                                                                                <ItemTemplate>
                                                                                    <asp:TextBox ID="txtohbymaterial" runat="server" Text="0" Width="50px"></asp:TextBox>
                                                                                </ItemTemplate>
                                                                                <HeaderStyle HorizontalAlign="Left" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left">
                                                                                <HeaderTemplate>
                                                                                    <asp:Label ID="lblheadoverheadbylabour" runat="server" Text="Overhead by Labour"
                                                                                        ToolTip="Overhead by Labour Cost Ratio"></asp:Label>
                                                                                </HeaderTemplate>
                                                                                <ItemTemplate>
                                                                                    <asp:TextBox ID="txtohbylabour" runat="server" Text="0" Width="50px"></asp:TextBox>
                                                                                </ItemTemplate>
                                                                                <HeaderStyle HorizontalAlign="Left" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left">
                                                                                <HeaderTemplate>
                                                                                    <asp:Label ID="lblheadoverheadbydays" runat="server" Text="Overhead by Active Days"
                                                                                        ToolTip="Overhead by Active Days (in selected period) Ratio"></asp:Label>
                                                                                </HeaderTemplate>
                                                                                <ItemTemplate>
                                                                                    <asp:TextBox ID="txtohbydays" runat="server" Text="0" Width="50px"></asp:TextBox>
                                                                                </ItemTemplate>
                                                                                <HeaderStyle HorizontalAlign="Left" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left">
                                                                                <HeaderTemplate>
                                                                                    <asp:Label ID="lblheadoverheadbyeq" runat="server" Text="Overhead by Equal" ToolTip="Overhead Allocated Equally to all Projects"></asp:Label>
                                                                                </HeaderTemplate>
                                                                                <ItemTemplate>
                                                                                    <asp:TextBox ID="txtohbyequal" runat="server" Text="0" Width="50px"></asp:TextBox>
                                                                                </ItemTemplate>
                                                                                <HeaderStyle HorizontalAlign="Left" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left">
                                                                                <HeaderTemplate>
                                                                                    <asp:Label ID="lblheadoverheadbytotal" runat="server" Text="Total" ToolTip="Total Overhead"></asp:Label>
                                                                                </HeaderTemplate>
                                                                                <ItemTemplate>
                                                                                    <asp:TextBox ID="txtfinaltotal" runat="server" Text="0" Width="50px"></asp:TextBox>
                                                                                </ItemTemplate>
                                                                                <FooterTemplate>
                                                                                    <asp:Label ID="footertotal" runat="server" Enabled="false" ForeColor="Black"></asp:Label>
                                                                                </FooterTemplate>
                                                                            </asp:TemplateField>
                                                                        </Columns>
                                                                        <PagerStyle CssClass="pgr" />
                                                                        <AlternatingRowStyle CssClass="alt" />
                                                                    </asp:GridView>
                                                                </fieldset></asp:Panel>
                                                        </div>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <div>
                                                            <fieldset>
                                                                <asp:Button ID="Button4" runat="server" CssClass="btnSubmit" OnClick="Button4_Click"
                                                                    Text="Calculate" Visible="False" />
                                                                <asp:Button ID="Button5" runat="server" CssClass="btnSubmit" OnClick="Button5_Click"
                                                                    Text="Submit" Visible="False" />
                                                                <asp:Button ID="btncan" runat="server" CssClass="btnSubmit" OnClick="Button8_Click"
                                                                    Text="Cancel" Visible="False" />
                                                                <div style="clear: both;">
                                                                    <asp:Label ID="lblcalms" runat="server" ForeColor="Red" Visible="true"></asp:Label>
                                                                </div>
                                                            </fieldset>
                                                        </div>
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:Panel>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Panel ID="Panel2" runat="server" Visible="False">
                                            <table id="temp1tbl" style="width: 100%">
                                                <tr>
                                                    <td>
                                                        <fieldset>
                                                            <legend>
                                                                <asp:Label ID="lblledge" runat="server" Text="List of Overhead Titles"></asp:Label>
                                                            </legend>
                                                            <table style="width: 100%">
                                                                <tr>
                                                                    <td>
                                                                        <asp:GridView ID="GridView2" runat="server" AllowPaging="True" AllowSorting="True"
                                                                            AlternatingRowStyle-CssClass="alt" AutoGenerateColumns="False" CssClass="mGrid"
                                                                            DataKeyNames="Id" EmptyDataText="No Records Found." GridLines="Both" PagerStyle-CssClass="pgr"
                                                                            Width="100%">
                                                                            <Columns>
                                                                                <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Select">
                                                                                    <ItemTemplate>
                                                                                        <asp:CheckBox ID="chkinvMasterStatus123" runat="server" Checked='<%#Bind("Active") %>' />
                                                                                    </ItemTemplate>
                                                                                    <ItemStyle Width="30px" />
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Group Name">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblgroupid123" runat="server" Text='<%#Bind("groupid") %>' Visible="false"></asp:Label>
                                                                                        <asp:Label ID="lblgroupname123" runat="server" Text='<%#Bind("groupdisplayname") %>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Account Name">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblaccountmasterid123" runat="server" Text='<%#Bind("Id") %>' Visible="false"></asp:Label>
                                                                                        <asp:Label ID="lblaccountname123" runat="server" Text='<%#Bind("AccountName") %>'></asp:Label>
                                                                                        <asp:Label ID="lblaccountid123" runat="server" Text='<%#Bind("AccountId") %>' Visible="false"></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Selected Period Amount">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblamount123" runat="server" Text='<%#Bind("AmountForPeriod") %>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Amount to be Allocated">
                                                                                    <ItemTemplate>
                                                                                        <asp:TextBox ID="txtamountallocate123" runat="server" Text='<%#Bind("AmountApplied") %>'></asp:TextBox>
                                                                                        <%--                <asp:Label ID="Label51" runat="server" Text="*" CssClass="labelstar"></asp:Label>--%>
                                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2456" runat="server" ControlToValidate="txtamountallocate123"
                                                                                            Display="Dynamic" ErrorMessage="*" ValidationGroup="5"></asp:RequiredFieldValidator>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Select Allocation Method">
                                                                                    <ItemTemplate>
                                                                                        <asp:DropDownList ID="ddlallocation123" runat="server">
                                                                                        </asp:DropDownList>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="OID" Visible="false">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lbloid123" runat="server" Text='<%#Bind("OId") %>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                            </Columns>
                                                                        </asp:GridView>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </fieldset>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <fieldset>
                                                            <legend>
                                                                <asp:Label ID="Label13" runat="server" Text="List of Work Order/ Projects During Selected Period"></asp:Label>
                                                            </legend>
                                                            <div style="clear: both;">
                                                                <label>
                                                                    <asp:Button ID="btneditsk" runat="server" OnClick="btneditsk_Click" Text="Select Columns" />
                                                                </label>
                                                                <label>
                                                                    <asp:Button ID="btrfilcoedit" runat="server" OnClick="btrfilcoedit_Click" Text="Refresh" />
                                                                </label>
                                                            </div>
                                                            <div style="clear: both;">
                                                                <asp:Panel ID="pnleditcolshow" runat="server" Visible="false">
                                                                    <table>
                                                                        <tr>
                                                                            <td>
                                                                                <asp:CheckBoxList ID="CheckBoxList2" runat="server" RepeatColumns="4" RepeatDirection="Horizontal"
                                                                                    Width="100%">
                                                                                    <asp:ListItem Selected="True" Value="0">Active</asp:ListItem>
                                                                                    <asp:ListItem Selected="True" Value="1">Work Order(Project)No.</asp:ListItem>
                                                                                    <asp:ListItem Selected="True" Value="2">Work Order (Project) Name</asp:ListItem>
                                                                                    <asp:ListItem Value="3">Start Date</asp:ListItem>
                                                                                    <asp:ListItem Value="4">End Date</asp:ListItem>
                                                                                    <asp:ListItem Value="5">Work Order (Project) Direct Material Cost</asp:ListItem>
                                                                                    <asp:ListItem Selected="True" Value="6">Direct Material Cost for the Period</asp:ListItem>
                                                                                    <asp:ListItem Value="7">Direct Labour Cost For Select Period</asp:ListItem>
                                                                                    <asp:ListItem Selected="True" Value="8">Direct Labour Costs for the Period</asp:ListItem>
                                                                                    <asp:ListItem Selected="True" Value="9">Active Project Days for the Period</asp:ListItem>
                                                                                    <asp:ListItem Selected="True" Value="10">Overhead by Material Cost Ratio</asp:ListItem>
                                                                                    <asp:ListItem Selected="True" Value="11">Overhead by Labour Cost Ratio</asp:ListItem>
                                                                                    <asp:ListItem Selected="True" Value="12">Overhead by Active Days Ratio</asp:ListItem>
                                                                                    <asp:ListItem Selected="True" Value="13">Overhead Allocated Equally to all 
                                                            Projects</asp:ListItem>
                                                                                    <asp:ListItem Selected="True" Value="14">Total Overhead</asp:ListItem>
                                                                                </asp:CheckBoxList>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </asp:Panel>
                                                            </div>
                                                            <div style="clear: both;">
                                                            </div>
                                                            <asp:GridView ID="GridView3" runat="server" AllowSorting="True" AlternatingRowStyle-CssClass="alt"
                                                                AutoGenerateColumns="False" CssClass="mGrid" DataKeyNames="Id" EmptyDataText="No Records Found."
                                                                GridLines="Both" PagerStyle-CssClass="pgr" ShowFooter="True" Width="100%">
                                                                <Columns>
                                                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Apply">
                                                                        <ItemTemplate>
                                                                            <asp:CheckBox ID="chkjobmaster123" runat="server" Checked="true" />
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                        <ItemStyle Width="30px" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Left">
                                                                        <HeaderTemplate>
                                                                            <asp:Label ID="lblheadjobno" runat="server" Text="Work Order  No." ToolTip="Work Order (Project) No."></asp:Label>
                                                                        </HeaderTemplate>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lbljobno123" runat="server" Text='<%#Bind("JobNumber") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Left">
                                                                        <HeaderTemplate>
                                                                            <asp:Label ID="lblheadjobname" runat="server" Text="Work Order Name" ToolTip="Work Order (Project) Name"></asp:Label>
                                                                        </HeaderTemplate>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lbljobname123" runat="server" Text='<%#Bind("JobName") %>'></asp:Label>
                                                                            <asp:Label ID="JobMasterId123" runat="server" Text='<%#Bind("JobMasterId") %>' Visible="false"></asp:Label>
                                                                            <asp:Label ID="lblinvmasterid123" runat="server" Text='<%#Bind("InvWMasterId") %>'
                                                                                Visible="false"></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Start Date">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblstartdate123" runat="server" Text='<%#Bind("JobStartDate","{0:dd/MM/yyy}") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="End Date">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblenddate123" runat="server" Text='<%#Bind("JobEndDate","{0:dd/MM/yyy}") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Total DMC">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lbldirectmaterial123" runat="server" Text='<%#Bind("DirectMaterialCost") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Left">
                                                                        <HeaderTemplate>
                                                                            <asp:Label ID="lblheadperioddmc" runat="server" Text="DMC For Period" ToolTip="Work Order (Project) Material Cost For Selected Period "></asp:Label>
                                                                        </HeaderTemplate>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lbldirectmaterialperiod123" runat="server" Text='<%#Bind("DirectMaterialCostOfPeriod") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Left">
                                                                        <HeaderTemplate>
                                                                            <asp:Label ID="lblheadtotaldlc" runat="server" Text="Total DLC" ToolTip="Direct Labour Cost For Select Period "></asp:Label>
                                                                        </HeaderTemplate>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lbldirectlabour123" runat="server" Text='<%#Bind("DirectMaterialCostOfPeriod") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Left">
                                                                        <HeaderTemplate>
                                                                            <asp:Label ID="lblheadPERIODdlc" runat="server" Text="DLC For Period" ToolTip="Direct Labour Cost for the Period"></asp:Label>
                                                                        </HeaderTemplate>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lbldirectlabourperiod123" runat="server" Text='<%#Bind("DirectLabourCost") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Total Project Duration "
                                                                        Visible="false">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblnoofdays123" runat="server"></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Left">
                                                                        <HeaderTemplate>
                                                                            <asp:Label ID="lblheadPERIODPD" runat="server" Text="Active Proj. Days" ToolTip="Active Project Days for the Period"></asp:Label>
                                                                        </HeaderTemplate>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblnoofdaysperiod123" runat="server" Text='<%#Bind("NoOfDaysCostForPeriod") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField>
                                                                        <HeaderTemplate>
                                                                            <asp:Label ID="lblheadoverheadbyperiod" runat="server" Text="Overhead by Material"
                                                                                ToolTip="Overhead by Material Cost Ratio"></asp:Label>
                                                                        </HeaderTemplate>
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txtohbymaterial123" runat="server" Text='<%#Bind("OhByMaterial") %>'
                                                                                Width="50px"></asp:TextBox>
                                                                            <%--       <asp:Label ID="Label52" runat="server" Text="*" CssClass="labelstar"></asp:Label>--%>
                                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator245456" runat="server" ControlToValidate="txtohbymaterial123"
                                                                                Display="Dynamic" ErrorMessage="*" ValidationGroup="5"></asp:RequiredFieldValidator>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Left">
                                                                        <HeaderTemplate>
                                                                            <asp:Label ID="lblheadoverheadbylabour" runat="server" Text="Overhead by Labour"
                                                                                ToolTip="Overhead by Labour Cost Ratio"></asp:Label>
                                                                        </HeaderTemplate>
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txtohbylabour123" runat="server" Text='<%#Bind("OhByLabour") %>'
                                                                                Width="50px"></asp:TextBox>
                                                                            <%--                                                <asp:Label ID="Label53" runat="server" Text="*" CssClass="labelstar"></asp:Label>--%>
                                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator247896" runat="server" ControlToValidate="txtohbylabour123"
                                                                                Display="Dynamic" ErrorMessage="*" ValidationGroup="5"></asp:RequiredFieldValidator>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Left">
                                                                        <HeaderTemplate>
                                                                            <asp:Label ID="lblheadoverheadbydays" runat="server" Text="Overhead by Active Days"
                                                                                ToolTip="Overhead by Active Days (in selected period) Ratio"></asp:Label>
                                                                        </HeaderTemplate>
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txtohbydays123" runat="server" Text='<%#Bind("OhByDays") %>' Width="50px"></asp:TextBox>
                                                                            <%--   <asp:Label ID="Label54" runat="server" Text="*" CssClass="labelstar"></asp:Label>--%>
                                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator78996" runat="server" ControlToValidate="txtohbydays123"
                                                                                Display="Dynamic" ErrorMessage="*" ValidationGroup="5"></asp:RequiredFieldValidator>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Left">
                                                                        <HeaderTemplate>
                                                                            <asp:Label ID="lblheadoverheadbyeq" runat="server" Text="Overhead by Equal" ToolTip="Overhead Allocated Equally to all Projects"></asp:Label>
                                                                        </HeaderTemplate>
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txtohbyequal123" runat="server" Text='<%#Bind("Ohbyequal") %>' Width="50px"></asp:TextBox>
                                                                            <%--  <asp:Label ID="Label55" runat="server" Text="*" CssClass="labelstar"></asp:Label>--%>
                                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2658" runat="server" ControlToValidate="txtohbyequal123"
                                                                                ErrorMessage="*" ValidationGroup="5"></asp:RequiredFieldValidator>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Left">
                                                                        <HeaderTemplate>
                                                                            <asp:Label ID="lblheadoverheadbytotal" runat="server" Text="Total" ToolTip="Total Overhead"></asp:Label>
                                                                        </HeaderTemplate>
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txtfinaltotal123" runat="server" Text='<%#Bind("OhAllocationtotal") %>'
                                                                                Width="50px"></asp:TextBox>
                                                                            <%--<asp:Label ID="Label56" runat="server" Text="*" CssClass="labelstar"></asp:Label>--%>
                                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2658742253" runat="server"
                                                                                ControlToValidate="txtfinaltotal123" Display="Dynamic" ErrorMessage="*" ValidationGroup="5"></asp:RequiredFieldValidator>
                                                                        </ItemTemplate>
                                                                        <FooterTemplate>
                                                                            <asp:Label ID="footertotal" runat="server" Enabled="false" ForeColor="Black"></asp:Label>
                                                                        </FooterTemplate>
                                                                    </asp:TemplateField>
                                                                </Columns>
                                                                <PagerStyle CssClass="pgr" />
                                                                <AlternatingRowStyle CssClass="alt" />
                                                            </asp:GridView>
                                                        </fieldset>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <fieldset>
                                                            <asp:Button ID="Button6" runat="server" CssClass="btnSubmit" OnClick="Button6_Click"
                                                                Text="Calculate" ValidationGroup="5" Visible="False" />
                                                            &nbsp;<asp:Button ID="Button7" runat="server" CssClass="btnSubmit" OnClick="Button7_Click"
                                                                Text="Update" ValidationGroup="5" Visible="False" />
                                                            &nbsp;<asp:Button ID="Button8" runat="server" CssClass="btnSubmit" OnClick="Button8_Click"
                                                                Text="Cancel" />
                                                            <div style="clear: both;">
                                                                <asp:Label ID="lblceditms" runat="server" ForeColor="Red" Visible="true"></asp:Label>
                                                            </div>
                                                        </fieldset>
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:Panel>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                        <div style="clear: both;">
                        </div>
                    </asp:Panel>
                </fieldset>
                <div style="clear: both;">
                </div>
                <div>
                    <fieldset>
                        <legend>
                            <asp:Label ID="Label8" runat="server" Text="List of Overhead Allocation Titles"></asp:Label>
                        </legend>
                        <div style="clear: both;">
                        </div>
                        <div style="float: right">
                            <label>
                                <asp:Button ID="Button1" runat="server" CssClass="btnSubmit" Text="Printable Version"
                                    OnClick="Button1_Click1" />
                            </label>
                            <label>
                                <input id="Button2" runat="server" class="btnSubmit" onclick="document.getElementById('mydiv1').className='open';javascript:CallPrint1('divPrint');document.getElementById('mydiv1').className='closed';"
                                    style="width: 51px;" type="button" value="Print" visible="false" />
                            </label>
                        </div>
                        <table>
                            <tr>
                                <td>
                                    <label>
                                        <asp:Label ID="Label10" runat="server" Text="Business Name"></asp:Label>
                                    </label>
                                </td>
                                <td>
                                    <label>
                                        <asp:DropDownList ID="ddlwarehouse" runat="server">
                                        </asp:DropDownList>
                                    </label>
                                </td>
                                <%--  <td>
                 <asp:RadioButtonList ID="RadioButtonList1" runat="server" AutoPostBack="True" OnSelectedIndexChanged="RadioButtonList1_SelectedIndexChanged"
                            RepeatDirection="Horizontal">
                            <asp:ListItem Value="0">Date</asp:ListItem>
                            <asp:ListItem Selected="True" Value="1">Period</asp:ListItem>
                        </asp:RadioButtonList>
                </td>--%>
                                <td>
                                    <asp:Panel ID="Panel1" runat="server" Visible="true">
                                        <label>
                                            <asp:Label ID="Label11" runat="server" Text="From Date"></asp:Label>
                                        </label>
                                        <label>
                                            <asp:TextBox ID="txtsdate" runat="server" Width="100px"></asp:TextBox>
                                            <cc1:CalendarExtender ID="CalendarExtender2" runat="server" PopupButtonID="ImageButton3"
                                                TargetControlID="txtsdate">
                                            </cc1:CalendarExtender>
                                        </label>
                                        <label>
                                            <asp:ImageButton ID="ImageButton3" runat="server" ImageUrl="~/ShoppingCart/images/cal_actbtn.jpg" />
                                        </label>
                                        <label>
                                            <asp:Label ID="Label12" runat="server" Text="To  Date"></asp:Label>
                                        </label>
                                        <label>
                                            <asp:TextBox ID="txtedate" runat="server" Width="100px"></asp:TextBox>
                                            <cc1:CalendarExtender ID="CalendarExtender4" runat="server" PopupButtonID="ImageButton4"
                                                TargetControlID="txtedate">
                                            </cc1:CalendarExtender>
                                        </label>
                                        <label>
                                            <asp:ImageButton ID="ImageButton4" runat="server" ImageUrl="~/ShoppingCart/images/cal_actbtn.jpg" />
                                        </label>
                                    </asp:Panel>
                                    <asp:Panel ID="Panel12" runat="server" Visible="False">
                                        <label>
                                            <asp:Label ID="Label9" runat="server" Text="Period"></asp:Label>
                                        </label>
                                        <label>
                                            <asp:DropDownList ID="ddlperiod" runat="server" OnSelectedIndexChanged="ddlperiod_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </label>
                                    </asp:Panel>
                                </td>
                                <td>
                                    <asp:Button ID="btng" runat="server" CssClass="btnSubmit" Text="Go" OnClick="btng_Click" />
                                </td>
                            </tr>
                        </table>
                        <div style="clear: both;">
                        </div>
                        <asp:Panel ID="pnlgrid1" runat="server" Width="100%">
                            <table width="100%">
                                <tr>
                                    <td>
                                        <div id="mydiv1" class="closed">
                                            <table width="850Px">
                                                <%--<tr align="center">
                                                    <td align="center" style="text-align: center; color: Navy; font-size: 20px;
                                                        font-weight: bold;">
                                                        <asp:Label ID="lblCompany" runat="server" ForeColor="Black" Font-Italic="true"></asp:Label>
                                                    </td>
                                                    </tr>--%>
                                                <tr align="center">
                                                    <td align="center" style="text-align: center; color: Navy; font-size: 20px; font-weight: bold;">
                                                        <asp:Label ID="lblBusiness" runat="server" ForeColor="Black" Font-Italic="true"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr align="center">
                                                    <td align="center" style="text-align: center; font-size: 18px; font-weight: bold;">
                                                        <asp:Label ID="Label24" runat="server" Font-Italic="true" Text="List of Overhead Allocation Titles"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="left">
                                                        <asp:Label ID="lblddd" runat="server" Text="" Font-Size="16px" Font-Italic="true"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <cc11:PagingGridView ID="GridView1" runat="server" AllowPaging="True" AllowSorting="True"
                                            EmptyDataText="No Records Found." AutoGenerateColumns="False" CssClass="mGrid"
                                            PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt" DataKeyNames="Id"
                                            OnRowCommand="GridView1_RowCommand" Width="100%" 
                                            OnRowDeleting="GridView1_RowDeleting" 
                                            onpageindexchanging="GridView1_PageIndexChanging">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Overhead Name" HeaderStyle-HorizontalAlign="Left"
                                                    ItemStyle-HorizontalAlign="Left" ItemStyle-Width="50%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbloverheadname123" runat="server" Text='<%#Bind("Name") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                    <ItemStyle HorizontalAlign="Left" Width="50%" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Overhead Collection Start Date" HeaderStyle-HorizontalAlign="Left"
                                                    ItemStyle-HorizontalAlign="Left" ItemStyle-Width="15%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblstartdate123" runat="server" Text='<%#Bind("StartDate","{0:dd/MM/yyy}") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                    <ItemStyle HorizontalAlign="Left" Width="10%" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Overhead Collection End Date" HeaderStyle-HorizontalAlign="Left"
                                                    ItemStyle-HorizontalAlign="Left" ItemStyle-Width="15%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblenddate123" runat="server" Text='<%#Bind("EndDate","{0:dd/MM/yyy}") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                    <ItemStyle HorizontalAlign="Left" Width="10%" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Total Overhead" HeaderStyle-HorizontalAlign="Left"
                                                    ItemStyle-HorizontalAlign="Left" ItemStyle-Width="20%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbltotaloverhead123" runat="server" Text='<%#Bind("TotalAmountOverHead") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                    <ItemStyle HorizontalAlign="Left" Width="10%" />
                                                </asp:TemplateField>
                                                <%-- <asp:ButtonField CommandName="vi" Text="View" ButtonType="Image" HeaderImageUrl="~/Account/images/edit.gif"
                                    ImageUrl="~/Account/images/edit.gif" HeaderText="Edit" HeaderStyle-HorizontalAlign="Left" 
                                    ItemStyle-HorizontalAlign="Left" ItemStyle-Width="3%">
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" Width="3%" />
                                </asp:ButtonField>--%>
                                                <asp:TemplateField HeaderText="Edit" HeaderImageUrl="~/Account/images/edit.gif" HeaderStyle-HorizontalAlign="Left"
                                                    ItemStyle-Width="3%">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="llinedit" runat="server" ToolTip="Edit" CommandArgument='<%# Eval("Id") %>'
                                                            CommandName="vi" ImageUrl="~/Account/images/edit.gif"></asp:ImageButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Delete" HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="3%"
                                                    HeaderImageUrl="~/ShoppingCart/images/trash.jpg">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="ImageButton48" runat="server" CommandName="Delete" ImageUrl="~/Account/images/delete.gif"
                                                            OnClientClick="return confirm('Do you wish to delete this record?');" ToolTip="Delete"
                                                            CommandArgument='<%# Eval("Id") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </cc11:PagingGridView>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </fieldset>
                </div>
                <div style="clear: both;">
                    <asp:Panel ID="Panel6" runat="server" CssClass="modalPopup" Width="400px">
                        <table width="100%">
                            <tr>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label>
                                        <asp:Label ID="lblm" runat="server" ForeColor="Black">You have to switch to 
                                                            previous year to view this report.</asp:Label>
                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td align="center" style="height: 26px">
                                    <asp:Label ID="lblstartdate" runat="server" Visible="false"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="center">
                                    <asp:Button ID="ImageButton2" runat="server" Text="Cancel" CssClass="btnSubmit" />
                                </td>
                            </tr>
                        </table>
                        &nbsp;</asp:Panel>
                    <asp:Button ID="btnmd" runat="server" Style="display: none" /><cc1:ModalPopupExtender
                        ID="ModalPopupExtender1" runat="server" BackgroundCssClass="modalBackground"
                        PopupControlID="Panel6" TargetControlID="btnmd" CancelControlID="ImageButton2">
                    </cc1:ModalPopupExtender>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
