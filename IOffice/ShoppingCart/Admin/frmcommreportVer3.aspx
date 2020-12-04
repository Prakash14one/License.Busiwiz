<%@ Page Language="C#" MasterPageFile="~/ShoppingCart/Admin/Master/mp_Admin.master"
    EnableEventValidation="false" AutoEventWireup="true" CodeFile="frmcommreport.aspx.cs"
    Inherits="Admin_frmcommreport" Title="OnlineMis" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
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
        
     function mask(evt)
        { 
         
           if(evt.keyCode==13 )
            { 
         
                  return false;
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

    <asp:UpdatePanel ID="uppanel" runat="server">
        <ContentTemplate>
            <div style="float: left;">
                <asp:Label ID="Label6" runat="server" ForeColor="Red"></asp:Label>
            </div>
            <div style="clear: both;">
            </div>
            <fieldset>
                <table style="width: 100%" cellpadding="1" cellspacing="2">
                    <tr>
                        <td colspan="3">
                            <asp:Panel ID="pnldd" runat="server" Visible="true">
                                <table width="100%">
                                    <tr>
                                        <td colspan="7">
                                            <asp:Panel ID="Panel1" runat="server" Visible="true" Width="100%">
                                                <table width="100%">
                                                    <tr>
                                                        <td style="text-align: right; width: 27%">
                                                            <input id="hdnsortExp" runat="Server" name="hdnsortExp" style="width: 1px" type="hidden" />
                                                            <input id="hdnsortDir" runat="Server" name="hdnsortDir" style="width: 1px" type="hidden" />
                                                            <label>
                                                                <asp:Label ID="Label1" runat="server" Text="Business Name"></asp:Label>
                                                            </label>
                                                            <label>
                                                                <asp:DropDownList ID="ddlstore" runat="server" OnSelectedIndexChanged="ddlstore_SelectedIndexChanged"
                                                                    AutoPostBack="True">
                                                                </asp:DropDownList>
                                                            </label>
                                                        </td>
                                                        <td style="text-align: left; width: 25%;">
                                                            <asp:RadioButtonList ID="rddatelist" runat="server" AutoPostBack="true" RepeatDirection="Horizontal"
                                                                OnSelectedIndexChanged="rddatelist_SelectedIndexChanged">
                                                                <asp:ListItem Value="1" Text="Select by Period"></asp:ListItem>
                                                                <asp:ListItem Selected="True" Value="2" Text="Select by Date"></asp:ListItem>
                                                            </asp:RadioButtonList>
                                                        </td>
                                                        <td colspan="4">
                                                            <asp:Panel ID="pnldateby" runat="server">
                                                                <table width="100%">
                                                                    <tr>
                                                                        <td>
                                                                            <label>
                                                                                <asp:Label ID="Label10" runat="server" Text="From"></asp:Label>
                                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtfromdate"
                                                                                    ErrorMessage="*" SetFocusOnError="True" ValidationGroup="1"></asp:RequiredFieldValidator>
                                                                            </label>
                                                                            <label>
                                                                                <asp:TextBox ID="txtfromdate" runat="server" Width="70px"></asp:TextBox>
                                                                            </label>
                                                                            <cc1:CalendarExtender ID="CalendarExtender3" runat="server" PopupButtonID="txtfromdate"
                                                                                TargetControlID="txtfromdate">
                                                                            </cc1:CalendarExtender>
                                                                            <cc1:MaskedEditExtender ID="MaskedEditExtender3" runat="server" Mask="99/99/9999"
                                                                                MaskType="Date" TargetControlID="txtfromdate">
                                                                            </cc1:MaskedEditExtender>
                                                                            <label>
                                                                                <asp:Label ID="Label11" runat="server" Text="To"></asp:Label>
                                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txttodate"
                                                                                    ErrorMessage="*" SetFocusOnError="True" ValidationGroup="1"></asp:RequiredFieldValidator>
                                                                            </label>
                                                                            <label>
                                                                                <asp:TextBox ID="txttodate" runat="server" Width="70px"></asp:TextBox>
                                                                            </label>
                                                                            <cc1:CalendarExtender ID="CalendarExtender4" runat="server" PopupButtonID="txttodate"
                                                                                TargetControlID="txttodate">
                                                                            </cc1:CalendarExtender>
                                                                            <cc1:MaskedEditExtender ID="MaskedEditExtender4" runat="server" Mask="99/99/9999"
                                                                                MaskType="Date" TargetControlID="txttodate">
                                                                            </cc1:MaskedEditExtender>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </asp:Panel>
                                                        </td>
                                                        <td colspan="4" style="text-align: center">
                                                            <asp:Panel ID="panperiodwise" runat="server" Width="100%" Visible="False">
                                                                <table width="100%">
                                                                    <tr>
                                                                        <td>
                                                                            <label>
                                                                                <asp:Label ID="Label12" runat="server" Text="Period"></asp:Label>
                                                                            </label>
                                                                            <label>
                                                                                <asp:DropDownList ID="ddlperiodwise" runat="server" AutoPostBack="True">
                                                                                    <asp:ListItem>Today</asp:ListItem>
                                                                                    <asp:ListItem>Yesterday</asp:ListItem>
                                                                                    <asp:ListItem>LastWeek</asp:ListItem>
                                                                                    <asp:ListItem>LastMonth</asp:ListItem>
                                                                                    <asp:ListItem>LastQuerter</asp:ListItem>
                                                                                    <asp:ListItem>LastYear</asp:ListItem>
                                                                                </asp:DropDownList>
                                                                            </label>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </asp:Panel>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </asp:Panel>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 6%">
                                            <label>
                                                Filter By
                                            </label>
                                        </td>
                                        <td style="width: 16%">
                                            <asp:CheckBox ID="chkpartytype" runat="server" Text="User Type" TextAlign="Right"
                                                AutoPostBack="True" OnCheckedChanged="chkpartytype_CheckedChanged" />
                                        </td>
                                        <td style="width: 16%">
                                            <asp:CheckBox ID="chkpartyname" runat="server" Text="User Name" TextAlign="Right"
                                                AutoPostBack="True" OnCheckedChanged="chkpartyname_CheckedChanged" />
                                        </td>
                                        <td style="width: 16%">
                                            <asp:CheckBox ID="chkdiv" runat="server" AutoPostBack="true" OnCheckedChanged="chkdiv_CheckedChanged"
                                                Text="Department - Division" />
                                        </td>
                                        <td style="width: 16%">
                                            <asp:CheckBox ID="chkweek" runat="server" AutoPostBack="true" OnCheckedChanged="chkweek_CheckedChanged"
                                                Text="Weekly Goal" />
                                        </td>
                                        <td style="width: 16%">
                                            <asp:CheckBox ID="chkproject" runat="server" AutoPostBack="true" OnCheckedChanged="chkproject_CheckedChanged"
                                                Text="Project" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 6%">
                                        </td>
                                        <td style="width: 16%">
                                            <asp:Panel ID="Panel2" runat="server" Visible="False" Width="100%">
                                                <table width="100%">
                                                    <tr>
                                                        <td>
                                                            <label>
                                                                <asp:DropDownList ID="ddlusertype" runat="server" AppendDataBoundItems="True" OnSelectedIndexChanged="ddlusertype_SelectedIndexChanged"
                                                                    AutoPostBack="True">
                                                                </asp:DropDownList>
                                                            </label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </asp:Panel>
                                        </td>
                                        <td style="width: 16%">
                                            <asp:Panel ID="Panel3" runat="server" Visible="False" Width="100%">
                                                <table width="100%">
                                                    <tr>
                                                        <td>
                                                            <label>
                                                                <asp:DropDownList ID="ddlmf" runat="server" AppendDataBoundItems="True">
                                                                </asp:DropDownList>
                                                            </label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </asp:Panel>
                                        </td>
                                        <td style="width: 16%">
                                            <asp:Panel ID="pnldivision" runat="server" Visible="False" Width="100%">
                                                <table width="100%">
                                                    <tr>
                                                        <td>
                                                            <label>
                                                                <asp:DropDownList ID="ddlrelatedbus1" runat="server" AppendDataBoundItems="True">
                                                                </asp:DropDownList>
                                                            </label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </asp:Panel>
                                        </td>
                                        <td style="width: 16%">
                                            <asp:Panel ID="pnlweekly" runat="server" Visible="False" Width="100%">
                                                <table width="100%">
                                                    <tr>
                                                        <td>
                                                            <label>
                                                                <asp:DropDownList ID="ddlrelweekgoal1" runat="server" AppendDataBoundItems="True">
                                                                </asp:DropDownList>
                                                            </label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </asp:Panel>
                                        </td>
                                        <td style="width: 16%">
                                            <asp:Panel ID="pnlproject" runat="server" Visible="False" Width="100%">
                                                <table width="100%">
                                                    <tr>
                                                        <td>
                                                            <label>
                                                                <asp:DropDownList ID="ddlproject1" runat="server" AppendDataBoundItems="True">
                                                                </asp:DropDownList>
                                                            </label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </asp:Panel>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 6%">
                                        </td>
                                        <td style="width: 16%">
                                            <asp:CheckBox ID="chktask" runat="server" AutoPostBack="true" OnCheckedChanged="chktask_CheckedChanged"
                                                Text="Task" />
                                        </td>
                                        <td style="width: 16%">
                                            <asp:CheckBox ID="chkcommtype" runat="server" AutoPostBack="true" Text="Communication Type"
                                                OnCheckedChanged="chkcommtype_CheckedChanged" />
                                        </td>
                                        <td style="width: 16%">
                                            <asp:CheckBox ID="chkcommname" runat="server" AutoPostBack="true" Text="Communication By"
                                                OnCheckedChanged="chkcommname_CheckedChanged" />
                                        </td>
                                        <td style="width: 16%">
                                            <asp:CheckBox ID="chldate" runat="server" AutoPostBack="true" Text="Reminder Date Wise"
                                                OnCheckedChanged="chldate_CheckedChanged" />
                                        </td>
                                        <td style="width: 16%">
                                            <asp:CheckBox ID="chksearch" runat="server" AutoPostBack="true" Text="Search" OnCheckedChanged="chksearch_CheckedChanged" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 4%">
                                        </td>
                                        <td style="width: 16%">
                                            <asp:Panel ID="pnltask" runat="server" Visible="False" Width="100%">
                                                <table width="100%">
                                                    <tr>
                                                        <td>
                                                            <label>
                                                                <asp:DropDownList ID="ddltask1" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddltask1_SelectedIndexChanged">
                                                                </asp:DropDownList>
                                                            </label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </asp:Panel>
                                        </td>
                                        <td style="width: 16%">
                                            <asp:Panel ID="Panel4" runat="server" Visible="False" Width="100%">
                                                <table width="100%">
                                                    <tr>
                                                        <td>
                                                            <label>
                                                                <asp:DropDownList ID="ddlsmt" runat="server" AutoPostBack="True">
                                                                </asp:DropDownList>
                                                            </label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </asp:Panel>
                                        </td>
                                        <td style="width: 16%">
                                            <asp:Panel ID="Panel5" runat="server" Visible="False" Width="100%">
                                                <table width="100%">
                                                    <tr>
                                                        <td>
                                                            <label>
                                                                <asp:DropDownList ID="ddlmfor" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlmfor_SelectedIndexChanged">
                                                                </asp:DropDownList>
                                                            </label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </asp:Panel>
                                        </td>
                                        <td style="width: 16%">
                                            <asp:Panel ID="PanelReminderDate" runat="server" Visible="False">
                                                <table width="100%">
                                                    <tr>
                                                        <td>
                                                            <label>
                                                                <asp:TextBox ID="txtfromdate1" runat="server" Width="70px"></asp:TextBox>
                                                            </label>
                                                            <cc1:CalendarExtender ID="CalendarExtender5" runat="server" PopupButtonID="txtfromdate1"
                                                                TargetControlID="txtfromdate1">
                                                            </cc1:CalendarExtender>
                                                            <cc1:MaskedEditExtender ID="MaskedEditExtender5" runat="server" Mask="99/99/9999"
                                                                MaskType="Date" TargetControlID="txtfromdate1">
                                                            </cc1:MaskedEditExtender>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </asp:Panel>
                                        </td>
                                        <td style="width: 16%">
                                            <asp:Panel ID="panelsearch" runat="server" Width="100%" Visible="False">
                                                <table width="100%">
                                                    <tr>
                                                        <td colspan="2" align="left">
                                                            <label>
                                                                <asp:TextBox ID="txtsearch" runat="server" MaxLength="30" onKeydown="return mask(event)"
                                                                    onkeyup="return check(this,/[\\/!@#$%^'&*()>.+:;={}[]|\/]/g,/^[\a-zA-Z0-9_\s]+$/,'div1',30)"></asp:TextBox>
                                                            </label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </asp:Panel>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 6%">
                                        </td>
                                        <td style="width: 16%">
                                            <asp:CheckBox ID="chkemailid" runat="server" AutoPostBack="true" Text="Email" OnCheckedChanged="chkemailid_CheckedChanged" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 6%">
                                        </td>
                                        <td style="width: 16%">
                                            <asp:Panel ID="panelemail" runat="server" Width="100%" Visible="False">
                                                <table width="100%">
                                                    <tr>
                                                        <td colspan="2" align="left">
                                                            <label>
                                                                <asp:DropDownList ID="ddlemailid" runat="server">
                                                                </asp:DropDownList>
                                                            </label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </asp:Panel>
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                            <cc1:RoundedCornersExtender ID="RoundedCornersExtender1" runat="server" BorderColor="#666666"
                                Radius="8" TargetControlID="pnldd">
                            </cc1:RoundedCornersExtender>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3" align="center">
                            <asp:Button ID="btnsearch" Text="Go" runat="server" OnClick="btnsearch_Click" CssClass="btnSubmit" />
                        </td>
                    </tr>
                </table>
            </fieldset>
            <fieldset>
                <legend>
                    <asp:Label ID="lblcrepo" runat="server" Text="Communication Report"></asp:Label>
                </legend>
                <table width="100%">
                    <tr>
                        <td colspan="2">
                            <div style="float: left;">
                                <asp:Panel Width="100%" ID="Panel6" runat="server">
                                    <div>
                                        <asp:CheckBox ID="chkwwww" runat="server" AutoPostBack="True" OnCheckedChanged="chkwwww_CheckedChanged" />
                                        <label>
                                            <asp:Label ID="Label29" runat="server" Text="Weekly Goal"></asp:Label>
                                        </label>
                                        <asp:CheckBox ID="chkppp" runat="server" AutoPostBack="True" OnCheckedChanged="chkppp_CheckedChanged" />
                                        <label>
                                            <asp:Label ID="Label30" runat="server" Text="Project"></asp:Label>
                                        </label>
                                        <asp:CheckBox ID="chkttt" runat="server" AutoPostBack="True" OnCheckedChanged="chkttt_CheckedChanged" />
                                        <label>
                                            <asp:Label ID="Label31" runat="server" Text="Task"></asp:Label>
                                        </label>
                                        <asp:CheckBox ID="chkeee" runat="server" AutoPostBack="True" OnCheckedChanged="chkeee_CheckedChanged" />
                                        <label>
                                            <asp:Label ID="Label32" runat="server" Text="Email ID"></asp:Label>
                                        </label>
                                        <asp:CheckBox ID="chkremin" runat="server" AutoPostBack="True" OnCheckedChanged="chkremin_CheckedChanged" />
                                        <label>
                                            <asp:Label ID="Label2" runat="server" Text="Reminder Date"></asp:Label>
                                        </label>
                                    </div>
                                </asp:Panel>
                            </div>
                        </td>
                    </tr>
                </table>
                <div style="clear: both;">
                </div>
                <div style="float: right">
                    <label>
                        <asp:Button ID="Button1" runat="server" Text="Print and Export" OnClick="Button1_Click"
                            CssClass="btnSubmit" />
                    </label>
                    <label>
                        <input id="Button7" runat="server" onclick="document.getElementById('mydiv').className='open';javascript:CallPrint('divPrint');document.getElementById('mydiv').className='closed';"
                            type="button" value="Print" visible="False" class="btnSubmit" />
                    </label>
                    <label>
                        <asp:DropDownList ID="ddlExport" runat="server" OnSelectedIndexChanged="ddlExport_SelectedIndexChanged"
                            AutoPostBack="True" Width="130px" Visible="False">
                        </asp:DropDownList>
                    </label>
                </div>
                <div style="clear: both;">
                </div>
                <table width="100%">
                    <tr>
                        <td colspan="6">
                            <asp:Panel ID="pnlgrid" runat="server" Width="100%" ScrollBars="Both">
                                <table width="100%">
                                    <tr align="center">
                                        <td colspan="6">
                                            <div id="mydiv" class="closed">
                                                <table width="850Px">
                                                    <tr>
                                                        <td align="center" style="text-align: center; font-size: 20px; font-weight: bold;">
                                                            <asp:Label ID="lblcompany" runat="server" Font-Italic="true" Font-Size="20px" ForeColor="Black"
                                                                Visible="false"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="center" style="font-size: 18px; font-weight: bold; color: #000000" colspan="6">
                                                            <asp:Label ID="sdf" runat="server" Text="Business : " Font-Italic="true" Font-Size="20px"></asp:Label>
                                                            <asp:Label ID="lblBusiness" runat="server" Font-Italic="true" Font-Size="20px" ForeColor="Black"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="center" style="font-size: 18px; font-weight: bold; color: #000000" colspan="6">
                                                            <asp:Label ID="lblreport" runat="server" Text="Communication Report" Font-Italic="true"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr style="vertical-align: top; height: 80%">
                                        <td colspan="6">
                                            <asp:GridView ID="griedviewperiodwise" runat="server" AutoGenerateColumns="False"
                                                CellPadding="4" OnPageIndexChanging="griedviewperiodwise_PageIndexChanging" OnRowCancelingEdit="griedviewperiodwise_RowCancelingEdit"
                                                OnRowCommand="griedviewperiodwise_RowCommand" OnRowDeleting="griedviewperiodwise_RowDeleting"
                                                OnRowEditing="griedviewperiodwise_RowEditing" OnRowUpdating="griedviewperiodwise_RowUpdating"
                                                Width="100%" DataKeyNames="CommID" AllowPaging="True" OnSelectedIndexChanged="griedviewperiodwise_SelectedIndexChanged"
                                                EmptyDataText="No Records Found" CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt"
                                                AllowSorting="True" OnSorting="griedviewperiodwise_Sorting">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Comm Date" SortExpression="Date" HeaderStyle-Width="2%"
                                                        HeaderStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblDate" runat="server" Text='<%#Bind("Date") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Left" Width="2%" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Reminder Date" Visible="false" SortExpression="ReminderDate"
                                                        HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="5%">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblreminder" runat="server" Text='<%# Bind("ReminderDate") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Left" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="RemTime" Visible="false" SortExpression="Time" HeaderStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                            <%-- <asp:Label ID="lbltime" runat="server" Text='<%# Eval("Time") %>'></asp:Label>--%>
                                                            <asp:Label ID="lbltime" runat="server"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Left" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="PartyType" SortExpression="PartyTypeName" HeaderStyle-HorizontalAlign="Left"
                                                        Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblPartyTypeName" runat="server" Text='<%#Bind("PartyTypeName") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Left" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="User Name" SortExpression="PartyName" HeaderStyle-HorizontalAlign="Left"
                                                        HeaderStyle-Width="18%">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblPartyName" runat="server" Text='<%#Bind("PartyName") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Left" Width="18%" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="CommType" SortExpression="Smallmesstype" HeaderStyle-HorizontalAlign="Left"
                                                        HeaderStyle-Width="10%">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSmallmesstype" runat="server" Text='<%#Bind("Smallmesstype") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Left" Width="10%" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Comm By" SortExpression="EmployeeName" HeaderStyle-HorizontalAlign="Left"
                                                        HeaderStyle-Width="10%">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblEmployeeName" runat="server" Text='<%#Bind("EmployeeName") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Weekly Goal" SortExpression="week" HeaderStyle-HorizontalAlign="Left"
                                                        HeaderStyle-Width="8%" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblweekgoal" runat="server" Text='<%#Bind("week") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Left" Width="8%" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Project" SortExpression="Project" HeaderStyle-HorizontalAlign="Left"
                                                        HeaderStyle-Width="8%" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblProject" runat="server" Text='<%#Bind("Project") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Left" Width="8%" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Task" SortExpression="Task" HeaderStyle-HorizontalAlign="Left"
                                                        HeaderStyle-Width="8%" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblTask" runat="server" Text='<%#Bind("Task") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Left" Width="8%" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Email" SortExpression="Email" HeaderStyle-HorizontalAlign="Left"
                                                        HeaderStyle-Width="8%" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblEmail" runat="server" Text='<%#Bind("Email") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Left" Width="8%" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Communication Description" SortExpression="Description"
                                                        HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="50%">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblDescription" runat="server" Text='<%#Bind("Description") %>' Visible="false"></asp:Label>
                                                            <asp:LinkButton ID="LinkButton2" runat="server" Text='<%#Bind("Description") %>'
                                                                CommandName="comm" CommandArgument='<%#Eval("CommID") %>' ForeColor="Black"></asp:LinkButton>
                                                            <asp:LinkButton ID="LinkButton3" runat="server" Text='<%#Bind("Description") %>'
                                                                CommandName="internal" CommandArgument='<%#Eval("MsgID") %>' ForeColor="Black"></asp:LinkButton>
                                                            <asp:LinkButton ID="LinkButton4" runat="server" Text='<%#Bind("Description") %>'
                                                                CommandName="external" CommandArgument='<%#Eval("MsgIDS") %>' ForeColor="Black"></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Phone No" SortExpression="Phoneno" HeaderStyle-HorizontalAlign="Left"
                                                        Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblPhoneno" runat="server" Text='<%#Bind("Phoneno") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Left" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblserialno" runat="server"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Docs" SortExpression="DocumentId" HeaderStyle-HorizontalAlign="Left"
                                                        HeaderStyle-Width="3%">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="LinkButton1" runat="server" Text='<%#Eval("DocumentId") %>' CommandName="Send"
                                                                CommandArgument='<%#Eval("CommID") %>' ForeColor="Black"></asp:LinkButton>
                                                            <asp:LinkButton ID="LinkButton5" runat="server" Text='<%#Eval("DocumentId") %>' CommandName="internal"
                                                                CommandArgument='<%#Eval("MsgID") %>' ForeColor="Black"></asp:LinkButton>
                                                            <asp:LinkButton ID="LinkButton6" runat="server" Text='<%#Eval("DocumentId") %>' CommandName="external"
                                                                CommandArgument='<%#Eval("MsgIDS") %>' ForeColor="Black"></asp:LinkButton>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Left" Width="3%" />
                                                    </asp:TemplateField>
                                                </Columns>
                                                <PagerStyle CssClass="pgr" />
                                                <AlternatingRowStyle CssClass="alt" />
                                            </asp:GridView>
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            <asp:Panel ID="Panel21" runat="server" BackColor="#CCCCCC" BorderColor="#666666"
                                Width="500px" BorderStyle="Solid">
                                <table bgcolor="#CCCCCC" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td align="center" bgcolor="#CCCCCC">
                                            <table bgcolor="#CCCCCC" cellpadding="0" cellspacing="0" style="width: 500Px">
                                                <tr>
                                                    <td style="text-align: left; font-weight: bolder;">
                                                        <label>
                                                            <asp:Label ID="Label13" runat="server" Text="Office Documents"></asp:Label>
                                                        </label>
                                                    </td>
                                                    <td style="text-align: right">
                                                        <asp:ImageButton ID="ImageButton4" runat="server" Height="15px" ImageUrl="~/images/closeicon.jpeg"
                                                            Width="15px" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center">
                                            <asp:Panel ID="pnlof" Height="220px" ScrollBars="Both" runat="server">
                                                <table cellpadding="0" cellspacing="0" width="480Px">
                                                    <tr>
                                                        <td align="center">
                                                            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" PageSize="10"
                                                                Width="470" CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt"
                                                                OnRowCommand="GridView1_RowCommand">
                                                                <Columns>
                                                                    <asp:TemplateField HeaderText="Doc ID" SortExpression="DocumentId" HeaderStyle-HorizontalAlign="Left"
                                                                        ItemStyle-HorizontalAlign="Left">
                                                                        <ItemTemplate>
                                                                            <asp:LinkButton ID="LinkButton1" runat="server" Text='<%#Eval("DocumentId") %>' CommandName="View"
                                                                                HeaderStyle-HorizontalAlign="Left" CommandArgument='<%#Eval("DocumentId") %>'
                                                                                ForeColor="Black"></asp:LinkButton>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:BoundField DataField="DocType" HeaderText="Cabinet-Drawer-Folder" HeaderStyle-HorizontalAlign="Left"
                                                                        ItemStyle-HorizontalAlign="Left" />
                                                                    <%-- <asp:BoundField DataField="DocumentId" HeaderText="Doc Id" />--%>
                                                                    <asp:BoundField DataField="DocumentTitle" HeaderText="Title" HeaderStyle-HorizontalAlign="Left"
                                                                        ItemStyle-HorizontalAlign="Left" />
                                                                    <asp:BoundField DataField="DocumentDate" HeaderText="Date" HeaderStyle-Width="2%"
                                                                        HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                                                                </Columns>
                                                                <EmptyDataTemplate>
                                                                    No Record Found.
                                                                </EmptyDataTemplate>
                                                            </asp:GridView>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </asp:Panel>
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                            <cc1:ModalPopupExtender ID="ModalPopupExtenderAddnew" runat="server" BackgroundCssClass="modalBackground"
                                Enabled="True" PopupControlID="Panel21" CancelControlID="ImageButton4" TargetControlID="HiddenButton222">
                            </cc1:ModalPopupExtender>
                            <asp:Button ID="HiddenButton222" runat="server" Style="display: none" />
                        </td>
                    </tr>
                </table>
            </fieldset>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="ddlExport" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
