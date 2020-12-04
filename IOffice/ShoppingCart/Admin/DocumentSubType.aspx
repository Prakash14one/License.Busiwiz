<%@ Page Language="C#" MasterPageFile="~/Master/LicenseMaster.master"
    AutoEventWireup="true" CodeFile="DocumentSubType.aspx.cs" Inherits="ShoppingCart_Admin_DocumentSubType"
    Title="Untitled Page" %>

<%@ Register Assembly="PagingGridView" Namespace="Fadrian.Web.Control" TagPrefix="cc11" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/ioffice/ShoppingCart/Admin/UserControl/UcontrolHelpPanel.ascx" TagName="pnlhelp"
    TagPrefix="pnlhelp" %>
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
            WinPrint.document.write(prtContent.innerHTML,'<link rel="stylesheet" type="text/css" href="css/forms.css" media="screen" />');
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
            
           
            if( evt.keyCode==188 ||evt.keyCode==191 ||evt.keyCode==192 ||evt.keyCode==109 || evt.keyCode==221 || evt.keyCode==220 ||evt.keyCode==222 ||evt.keyCode==219 ||evt.keyCode==59||evt.keyCode==186 ||evt.keyCode==59  )
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
            function mak(id,max_len,myele)
        {
            counter=document.getElementById(id);
            
            if(myele.value.length <= max_len)
            {
                remaining_characters=max_len-myele.value.length;
                counter.innerHTML=remaining_characters;
            }
        }       
    </script>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div style="margin-left: 1%">
                <asp:Label ID="lblmsg" runat="server" ForeColor="Red"></asp:Label>
            </div>
            <div style="clear: both;">
            </div>
            <div class="products_box">
                <fieldset>
                    <legend>
                        <asp:Label ID="Label12" runat="server" Text="Add New Document Drawer" Visible="False">
                        </asp:Label>
                    </legend>
                    <div style="float: right;">
                        <asp:Button ID="btnadd" runat="server" Text="Add New Document Drawer" CssClass="btnSubmit"
                            OnClick="btnadd_Click" />
                    </div>
                    <div style="clear: both;">
                    </div>
                    <asp:Panel ID="pnladd" runat="server" Visible="false">
                        <table width="100%">
                            <tr>
                                <td >
                                    <label>
                                        <asp:Label runat="server" ID="Label7" Text="Business Name" Visible="true"></asp:Label>
                                    </label>
                                </td>
                                <td >
                                    <label>
                                        <asp:DropDownList ID="ddlbusinessname" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlbusinessname_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td >
                                    <label>
                                        <asp:Label runat="server" ID="Label8" Text="Cabinet Name" Visible="true"></asp:Label>
                                    </label>
                                </td>
                                <td >
                                    <label>
                                        <asp:DropDownList ID="ddldocmaintype" runat="server" DataTextField="DocumentMainType"
                                            DataValueField="DocumentMainTypeId" OnSelectedIndexChanged="ddldocmaintype_SelectedIndexChanged"
                                            CausesValidation="True" AutoPostBack="True">
                                        </asp:DropDownList>
                                    </label>
                                    <label>
                                        <asp:ImageButton ID="imgAdd" runat="server" AlternateText="Add New" ImageUrl="~/ShoppingCart/images/AddNewRecord.jpg"
                                            Height="20px" ToolTip="Add New " Width="20px" OnClick="imgAdd_Click" ImageAlign="Bottom" />
                                    </label>
                                    <label>
                                        <asp:ImageButton ID="imgRefresh" runat="server" AlternateText="Refresh" ImageUrl="~/ShoppingCart/images/DataRefresh.jpg"
                                            Height="20px" Width="20px" ToolTip="Refresh" OnClick="imgRefresh_Click" ImageAlign="Bottom" />
                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td >
                                    <label>
                                        <asp:Label runat="server" ID="Label9" Text="Drawer Name " Visible="true"></asp:Label>
                                        <asp:Label runat="server" ID="Label10" Text="*" Visible="true"></asp:Label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtdocsubtypename"
                                            ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="REG1" runat="server" ErrorMessage="Invalid Character"
                                            Display="Dynamic" SetFocusOnError="True" ValidationExpression="^([_a-zA-Z0-9\s]*)"
                                            ControlToValidate="txtdocsubtypename" ValidationGroup="1"></asp:RegularExpressionValidator>
                                    </label>
                                </td>
                                <td >
                                    <label>
                                        <asp:TextBox ID="txtdocsubtypename" onKeydown="return mask(event)" onkeyup="return check(this,/[\\/!@#$%^'.&*()>+:;={}[]|\/]/g,/^[\a-zA-Z0-9_ ]+$/,'div1',20)"
                                            runat="server" ValidationGroup="1" Width="235px" MaxLength="20"></asp:TextBox>
                                    </label>
                                    <label>
                                        <asp:Label ID="Label16" runat="server" Text="Max" CssClass="labelcount"></asp:Label>
                                        <span id="div1" class="labelcount">20</span>
                                        <asp:Label ID="lblinvstiename" runat="server" CssClass="labelcount" Text="(A-Z 0-9 _)"></asp:Label></label>
                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td >
                                </td>
                                <td >
                                    <asp:CheckBox ID="chkdesright" Checked="true" runat="server" AutoPostBack="True"
                                        OnCheckedChanged="chkdesright_CheckedChanged" />
                                    <label>
                                        <asp:Label runat="server" ID="Label15" Text="Do you wish to set access rights for this drawer ?"
                                            Visible="true"></asp:Label>
                                    </label>
                                </td>
                            </tr>
                                   <tr>
                                <td>
                                </td>
                                <td colspan="1" align="left">
                                    <asp:Panel ID="pnldataaccess" runat="server" Width="80%">
                                        <table width="97%">
                                            <tr>
                                                <td style="width: 170px;">
                                                    <label>
                                                        <asp:Label runat="server" ID="Label14" Text="Filter by Department" Visible="true"></asp:Label>
                                                    </label>
                                                </td>
                                                <td>
                                                    <label>
                                                        <asp:DropDownList ID="ddldept" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddldept_SelectedIndexChanged">
                                                        </asp:DropDownList>
                                                    </label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="3">
                                                    <asp:Panel ID="pnnnnx" runat="server" ScrollBars="Vertical" Height="200px">
                                                        <asp:GridView ID="grdacc" runat="server" AllowPaging="false" AlternatingRowStyle-CssClass="alt"
                                                            PagerStyle-CssClass="pgr" AutoGenerateColumns="False" CssClass="mGrid" DataKeyNames="DesignationMasterId"
                                                            EmptyDataRowStyle-HorizontalAlign="Left" GridLines="Both" ShowHeader="true">
                                                            <Columns>
                                                                <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Designation Name"
                                                                    ItemStyle-HorizontalAlign="Left" Visible="true">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="pnlcab" runat="server" Text='<%# Bind("DesignationName") %>'></asp:Label>
                                                                        <asp:Label ID="lblcode" runat="server" Text="0" Visible="false"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="View" ItemStyle-HorizontalAlign="Left"
                                                                    ItemStyle-Width="70px">
                                                                    <HeaderTemplate>
                                                                        <asp:Label ID="lblInvi" runat="server" Text="View"></asp:Label>
                                                                        <asp:CheckBox ID="chkViewhead" runat="server" AutoPostBack="true" OnCheckedChanged="chkViewhead_chachedChanged" />
                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:CheckBox ID="chkview" runat="server" Checked="false" Visible="true" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Delete" ItemStyle-HorizontalAlign="Left"
                                                                    ItemStyle-Width="80px">
                                                                    <HeaderTemplate>
                                                                        <asp:Label ID="lblIndel" runat="server" Text="Delete"></asp:Label>
                                                                        <asp:CheckBox ID="chkdeletehead" runat="server" AutoPostBack="true" OnCheckedChanged="chkdeletehead_chachedChanged" />
                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:CheckBox ID="chkdelete" runat="server" Checked="false" Visible="true" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Save" ItemStyle-HorizontalAlign="Left"
                                                                    ItemStyle-Width="70px">
                                                                    <HeaderTemplate>
                                                                        <asp:Label ID="lblInsave" runat="server" Text="Save"></asp:Label>
                                                                        <asp:CheckBox ID="chksavehead" runat="server" AutoPostBack="true" OnCheckedChanged="chksavehead_chachedChanged" />
                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:CheckBox ID="chksave" runat="server" Checked="false" Visible="true" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Edit" ItemStyle-HorizontalAlign="Left"
                                                                    ItemStyle-Width="70px">
                                                                      <HeaderTemplate>
                                                                        <asp:Label ID="lblInedi" runat="server" Text="Edit"></asp:Label>
                                                                        <asp:CheckBox ID="chkedithead" runat="server" AutoPostBack="true" OnCheckedChanged="chkedithead_chachedChanged" />
                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:CheckBox ID="chkedit" runat="server" Checked="false" Visible="true" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Email" ItemStyle-HorizontalAlign="Left"
                                                                    ItemStyle-Width="70px">
                                                                     <HeaderTemplate>
                                                                        <asp:Label ID="lblInem" runat="server" Text="Email"></asp:Label>
                                                                        <asp:CheckBox ID="chkemailhead" runat="server" AutoPostBack="true" OnCheckedChanged="chkemailhead_chachedChanged" />
                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:CheckBox ID="chkemail" runat="server" Checked="false" Visible="true" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Message" ItemStyle-HorizontalAlign="Left"
                                                                    ItemStyle-Width="90px">
                                                                     <HeaderTemplate>
                                                                        <asp:Label ID="lblInms" runat="server" Text="Message"></asp:Label>
                                                                        <asp:CheckBox ID="chkMessagehead" runat="server" AutoPostBack="true" OnCheckedChanged="chkMessagehead_chachedChanged" />
                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:CheckBox ID="chkMessage" runat="server" Checked="false" Visible="true" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                        </asp:GridView>
                                                    </asp:Panel>
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                </td>
                            </tr>
                            
                            <tr>
                                <td >
                                </td>
                                <td >
                                    <asp:CheckBox ID="CheckBox1" runat="server" />
                                    <label>
                                        <asp:Label runat="server" ID="Label13" Text="Do you wish to add any folders to this drawer? (This is recommended) "
                                            Visible="true"></asp:Label>
                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td >
                                </td>
                                <td >
                                    <asp:Button ID="imgbtnsubmit" runat="server" CssClass="btnSubmit" OnClick="imgbtnsubmit_Click"
                                        Text="Submit" ValidationGroup="1" ToolTip="Submit" />
                                    <asp:Button ID="btnupdate" runat="server" CssClass="btnSubmit" Text="Update" Visible="false"
                                        ValidationGroup="1" OnClick="btnupdate_Click" />
                                    <asp:Button ID="btncancel0" runat="server" OnClick="btncancel_Click" CssClass="btnSubmit"
                                        Text="Cancel" ToolTip="Cancel" />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <br />
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </fieldset>
                <fieldset>
                    <legend>
                        <asp:Label ID="lbllegend" runat="server" Text="List of Drawers"></asp:Label>
                    </legend>
                    <table width="100%">
                        <tr>
                            <td colspan="2">
                                <div style="float: right;">
                                    <asp:Button ID="Button2" runat="server" CssClass="btnSubmit" Text="Printable Version"
                                        OnClick="Button2_Click" />
                                    <input id="Button1" runat="server" onclick="document.getElementById('mydiv').className='open';javascript:CallPrint('divPrint');document.getElementById('mydiv').className='closed';"
                                        class="btnSubmit" type="button" value="Print" visible="false" />
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <label>
                                    <asp:Label runat="server" ID="Label6" Text="Select by Business" Visible="true"></asp:Label>
                                </label>
                                <label>
                                    <asp:DropDownList ID="ddlfilterbusiness" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlfilterbusiness_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </label>
                                <label>
                                    <asp:Label runat="server" ID="Label11" Text="Select by Cabinet" Visible="true"></asp:Label>
                                </label>
                                <label>
                                    <asp:DropDownList ID="ddlfiltercabinet" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlfiltercabinet_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </label>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:Panel ID="pnlgrid" runat="server" Width="100%">
                                    <table style="width: 100%">
                                        <tr align="center">
                                            <td colspan="2">
                                                <div id="mydiv" class="closed">
                                                    <table width="100%">
                                                        <tr>
                                                            <td align="center" style="text-align: center; font-size: 20px; font-weight: bold;">
                                                                <asp:Label ID="lblcompany" Visible="false" runat="server" Font-Italic="True" Text=""></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="center" style="text-align: center; font-size: 20px; font-weight: bold;">
                                                                <asp:Label ID="Label2" runat="server" Font-Italic="True" Text="Business:"></asp:Label>
                                                                <asp:Label ID="Label5" runat="server" Font-Italic="True" Text=""></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="center" style="text-align: center; font-size: 18px; font-weight: bold;">
                                                                <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Italic="True" ForeColor="Black"
                                                                    Text="List of Drawers"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="left">
                                                                <asp:Label ID="Label3" runat="server" Text="Cabinet :" ForeColor="Black"></asp:Label>
                                                                <asp:Label ID="Label4" runat="server" ForeColor="Black"></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Panel ID="Panel222" runat="server">
                                                    <cc11:PagingGridView ID="gridocsubtype1" runat="server" AllowSorting="true" AutoGenerateColumns="False"
                                                        BorderWidth="1px" CssClass="mGrid" GridLines="Both" PagerStyle-CssClass="pgr"
                                                        AlternatingRowStyle-CssClass="alt" DataKeyNames="DocumentSubTypeId" EmptyDataText="No Record Found."
                                                        OnRowCancelingEdit="gridocsubtype_RowCancelingEdit" OnRowCommand="gridocsubtype_RowCommand"
                                                        OnRowDeleting="gridocsubtype_RowDeleting" OnRowEditing="gridocsubtype_RowEditing"
                                                        OnSorting="gridocsubtype_Sorting" Width="100%" 
                                                        onpageindexchanging="gridocsubtype1_PageIndexChanging" PageSize="20">
                                                        <PagerSettings Mode="NumericFirstLast" />
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="Business Name" ItemStyle-Width="25%" SortExpression="WName"
                                                                HeaderStyle-HorizontalAlign="Left">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblbusinessname456" runat="server" Text='<%# Eval("WName")%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Cabinet" ItemStyle-Width="25%" SortExpression="DocumentMainType"
                                                                HeaderStyle-HorizontalAlign="Left">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblDocumentMainType" runat="server" Text='<%# Eval("DocumentMainType")%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Drawer" ItemStyle-Width="45%" SortExpression="DocumentSubType"
                                                                HeaderStyle-HorizontalAlign="Left">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblDocumentSubType" runat="server" Text='<%# Eval("DocumentSubType")%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField ItemStyle-Width="3%" HeaderStyle-HorizontalAlign="Left" HeaderImageUrl="~/Account/images/edit.gif">
                                                                <ItemTemplate>
                                                                    <asp:ImageButton ID="llinedit" runat="server" ToolTip="Edit" CommandArgument='<%# Eval("DocumentSubTypeId") %>'
                                                                        CommandName="Edit" ImageUrl="~/Account/images/edit.gif"></asp:ImageButton>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Delete" ItemStyle-Width="2%" ItemStyle-HorizontalAlign="Left"
                                                                HeaderStyle-HorizontalAlign="Left" HeaderImageUrl="~/ShoppingCart/images/trash.jpg">
                                                                <ItemTemplate>
                                                                    <asp:ImageButton ID="ImageButton1" ToolTip="Delete" runat="server" CommandName="Delete"
                                                                        ImageUrl="~/Account/images/delete.gif" OnClientClick="return confirm('Do you wish to delete this record?');" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                        <PagerStyle CssClass="pgr" />
                                                        <AlternatingRowStyle CssClass="alt" />
                                                    </cc11:PagingGridView>
                                                </asp:Panel>
                                            </td>
                                            <tr>
                                                <td>
                                                    <input id="hdnsortExp" runat="Server" name="hdnsortExp" style="width: 1px" type="hidden" />
                                                    <input id="hdnsortDir" runat="Server" name="hdnsortDir" style="width: 1px" type="hidden" />
                                                </td>
                                            </tr>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </td>
                        </tr>
                        </tr>
                    </table>
                </fieldset>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="ddldocmaintype" />
            <asp:AsyncPostBackTrigger ControlID="ddlbusinessname" EventName="SelectedIndexChanged" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
