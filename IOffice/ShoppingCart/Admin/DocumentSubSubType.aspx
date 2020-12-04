<%@ Page Language="C#" MasterPageFile="~/Master/LicenseMaster.master"
    AutoEventWireup="true" CodeFile="DocumentSubSubType.aspx.cs" Inherits="ShoppingCart_Admin_DocumentSubSubType"
    Title="Untitled Page" %>

<%@ Register Assembly="PagingGridView" Namespace="Fadrian.Web.Control" TagPrefix="cc11" %>
<%@ Register Src="~/IOffice/ShoppingCart/Admin/UserControl/UcontrolHelpPanel.ascx" TagName="pnlhelp"
    TagPrefix="pnlhelp" %>
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
            var prtContent = document.getElementById('<%= Panel2.ClientID %>');
            var WinPrint = window.open('', '', 'left=0,top=0,width=1000px,height=1000px,toolbar=1,scrollbars=0,status=0');
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

    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
        <ContentTemplate>
            <div style="margin-left: 1%">
                <asp:Label ID="lblmsg" runat="server" ForeColor="Red"></asp:Label>
            </div>
            <div style="clear: both;">
            </div>
            <div class="products_box">
                <fieldset>
                    <legend>
                        <asp:Label ID="Label17" runat="server" Text="Add New Document Folder" Visible="False">
                        </asp:Label>
                    </legend>
                    <div style="float: right;">
                        <asp:Button ID="btnadd" runat="server" Text="Add New Document Folder" CssClass="btnSubmit"
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
                                        <asp:DropDownList ID="ddlbusiness" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlbusiness_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td >
                                    <label>
                                        <asp:Label runat="server" ID="Label4" Text="Cabinet Name" Visible="true"></asp:Label>
                                        <asp:Label runat="server" ID="Label5" Text="*" Visible="true"></asp:Label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddldocmaintype"
                                            ErrorMessage="*" InitialValue="0" ValidationGroup="1"> </asp:RequiredFieldValidator>
                                    </label>
                                </td>
                                <td >
                                    <label>
                                        <asp:DropDownList ID="ddldocmaintype" runat="server" AutoPostBack="True" DataTextField="DocumentMainType"
                                            DataValueField="DocumentMainTypeId" OnSelectedIndexChanged="ddldocmaintype_SelectedIndexChanged"
                                            CausesValidation="True">
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
                                        <asp:Label runat="server" ID="Label6" Text="Drawer Name" Visible="true"></asp:Label>
                                        <asp:Label runat="server" ID="Label8" Text="*" Visible="true"></asp:Label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddldocsubtypename"
                                            ErrorMessage="*" InitialValue="0" ValidationGroup="1"></asp:RequiredFieldValidator>
                                    </label>
                                </td>
                                <td >
                                    <label>
                                        <asp:DropDownList ID="ddldocsubtypename" runat="server" DataTextField="DocumentSubType"
                                            DataValueField="DocumentSubTypeId" CausesValidation="True" 
                                        onselectedindexchanged="ddldocsubtypename_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </label>
                                    <label>
                                        <asp:ImageButton ID="imgAdd2" runat="server" AlternateText="Add New" ImageUrl="~/ShoppingCart/images/AddNewRecord.jpg"
                                            Height="20px" ToolTip="Add New " Width="20px" OnClick="imgAdd2_Click" />
                                    </label>
                                    <label>
                                        <asp:ImageButton ID="imgRefresh2" runat="server" AlternateText="Refresh" Height="20px"
                                            ImageUrl="~/ShoppingCart/images/DataRefresh.jpg" OnClick="imgRefresh2_Click"
                                            ToolTip="Refresh" Width="20px" />
                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td >
                                    <label>
                                        <asp:Label runat="server" ID="Label9" Text="Folder Name" Visible="true"></asp:Label>
                                        <asp:Label runat="server" ID="Label10" Text="*" Visible="true"></asp:Label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtdocsubsubtypename"
                                            ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="REG1" runat="server" ErrorMessage="Invalid Character"
                                            Display="Dynamic" SetFocusOnError="True" ValidationExpression="^([-_a-zA-Z0-9\s]*)"
                                            ControlToValidate="txtdocsubsubtypename" ValidationGroup="1"></asp:RegularExpressionValidator>
                                    </label>
                                </td>
                                <td >
                                    <label>
                                        <asp:TextBox ID="txtdocsubsubtypename" MaxLength="30" runat="server" onKeydown="return mask(event)"
                                            onkeyup="return check(this,/[\\/!@#$%^'.&*()>+:;={}[]|\/]/g,/^[\a-zA-Z0-9_-\s]+$/,'div1',30)"
                                            ValidationGroup="1" Width="200px"></asp:TextBox>
                                    </label>
                                    <label>
                                        <%-- <asp:Label ID="Label16" runat="server" Text="Max" CssClass="labelcount"></asp:Label>
                                        <span id="div1" class="labelcount">30</span>
                                        <asp:Label ID="lblinvstiename" runat="server" CssClass="labelcount" Text="(A-Z 0-9 _ -)"></asp:Label>--%>
                                        <asp:Label ID="Label16" runat="server" Text="Max" CssClass="labelcount"></asp:Label>
                                        <span id="div1" class="labelcount">30</span>
                                        <asp:Label ID="lblinvstiename" runat="server" CssClass="labelcount" Text="(A-Z 0-9 _ -)"></asp:Label></label>
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
                                        <asp:Label runat="server" ID="Label19" Text="Do you wish to set access rights for this folder ?"
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
                                                        <asp:Label runat="server" ID="Label18" Text="Filter by Department" Visible="true"></asp:Label>
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
                                        <asp:Label runat="server" ID="Label11" Text="Do you wish to add documents? "></asp:Label>
                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td >
                                </td>
                                <td >
                                    <input id="hdnsortExp" runat="Server" name="hdnsortExp" style="width: 1px" type="hidden" />
                                    <input id="hdnsortDir" runat="Server" name="hdnsortDir" style="width: 1px" type="hidden" />
                                    <asp:Button ID="imgbtnsubmit" runat="server" Text="Submit" ValidationGroup="1" OnClick="imgbtnsubmit_Click"
                                        CssClass="btnSubmit" />
                                    <asp:Button ID="btnupdate" runat="server" CssClass="btnSubmit" Text="Update" Visible="false"
                                        ValidationGroup="1" OnClick="btnupdate_Click" />
                                    <asp:Button ID="imgbtnsubmit0" runat="server" CssClass="btnSubmit" OnClick="imgbtnsubmit0_Click"
                                        Text="Cancel" />
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </fieldset>
                <fieldset>
                    <legend>
                        <asp:Label ID="lbllegend" runat="server" Text="List of Document Folders"></asp:Label>
                    </legend>
                    <table width="100%">
                        <tr>
                            <td>
                                <div style="float: right;">
                                    <asp:Button ID="Button2" runat="server" CssClass="btnSubmit" Text="Printable Version"
                                        OnClick="Button2_Click" />
                                    <input id="Button1" runat="server" onclick="document.getElementById('mydiv').className='open';javascript:CallPrint('divPrint');document.getElementById('mydiv').className='closed';"
                                        class="btnSubmit" type="button" value="Print" visible="false" />
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <table width="100%">
                                    <tr>
                                        <td>
                                            <label>
                                                <asp:Label ID="Label12" runat="server" Text=" Filter by Business"></asp:Label>
                                            </label>
                                            <label>
                                                <asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </label>
                                            <label>
                                                <asp:Label ID="Label13" runat="server" Text="Filter by Cabinet"></asp:Label>
                                            </label>
                                            <label>
                                                <asp:DropDownList ID="DropDownList2" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropDownList2_SelectedIndexChanged"
                                                    CausesValidation="True">
                                                </asp:DropDownList>
                                            </label>
                                            <label>
                                                <asp:Label ID="Label14" runat="server" Text="Filter by Drawer"></asp:Label>
                                            </label>
                                            <label>
                                                <asp:DropDownList ID="DropDownList3" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropDownList3_SelectedIndexChanged"
                                                    CausesValidation="True">
                                                </asp:DropDownList>
                                            </label>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <br />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Panel ID="Panel2" runat="server">
                                    <table width="100%">
                                        <tr align="center">
                                            <td>
                                                <div id="mydiv" class="closed">
                                                    <table width="100%">
                                                        <tr>
                                                            <td align="center" style="text-align: center; font-size: 20px; font-weight: bold;">
                                                                <asp:Label ID="lblBusiness0" runat="server" Text="" Font-Italic="True" Visible="false"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="center" style="text-align: center; font-size: 20px; font-weight: bold;">
                                                                <asp:Label ID="Label15" runat="server" Text="Business : " Font-Italic="True"></asp:Label>
                                                                <asp:Label ID="lblBusiness" runat="server" Font-Italic="True" Text=""></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="center" style="text-align: center; font-size: 18px; font-weight: bold;">
                                                                <asp:Label ID="Label1" runat="server" Font-Italic="True" Text="List of Document Folders"
                                                                    ForeColor="Black"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="left">
                                                                <asp:Label ID="Label2" runat="server" Text="Cabinet : "></asp:Label>
                                                                <asp:Label ID="lblCabinet" runat="server"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="Label3" runat="server" Text="Drawer : "></asp:Label>
                                                                <asp:Label ID="lblDrawer" runat="server"></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <cc11:PagingGridView ID="gridocsubsubtype" runat="server" BorderWidth="1px" Width="100%"
                                                    AutoGenerateColumns="False" DataKeyNames="DocumentTypeId" OnRowCancelingEdit="gridocsubsubtype_RowCancelingEdit"
                                                    OnRowCommand="gridocsubsubtype_RowCommand" OnRowDeleting="gridocsubsubtype_RowDeleting"
                                                    OnRowEditing="gridocsubsubtype_RowEditing" EmptyDataText="No Record Found." CssClass="mGrid"
                                                    GridLines="Both" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt"
                                                    AllowSorting="True" OnPageIndexChanging="gridocsubsubtype_PageIndexChanging"
                                                    OnSorting="gridocsubsubtype_Sorting" PageSize="20">
                                                    <PagerSettings Mode="NumericFirstLast" />
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Business Name" SortExpression="Name" HeaderStyle-HorizontalAlign="Left"
                                                            ItemStyle-Width="15%">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblwname" runat="server" Text='<%# Eval("Name")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Cabinet" SortExpression="DocumentMainType" HeaderStyle-HorizontalAlign="Left"
                                                            ItemStyle-Width="20%">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblDocumentMainType" runat="server" Text='<%# Eval("DocumentMainType")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Drawer" SortExpression="DocumentSubType" HeaderStyle-HorizontalAlign="Left"
                                                            ItemStyle-Width="20%">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblDocumentSubType" runat="server" Text='<%# Eval("DocumentSubType")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Folder" SortExpression="DocumentType" HeaderStyle-HorizontalAlign="Left"
                                                            ItemStyle-Width="35%">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblDocumentType" runat="server" Text='<%# Eval("DocumentType")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField ItemStyle-Width="3%" HeaderStyle-HorizontalAlign="Left" HeaderImageUrl="~/Account/images/edit.gif">
                                                            <ItemTemplate>
                                                                <asp:ImageButton ID="llinedit" runat="server" ToolTip="Edit" CommandArgument='<%# Eval("DocumentTypeId") %>'
                                                                    CommandName="Edit" ImageUrl="~/Account/images/edit.gif"></asp:ImageButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Delete" ItemStyle-Width="2%" HeaderStyle-HorizontalAlign="Left"
                                                            HeaderImageUrl="~/ShoppingCart/images/trash.jpg">
                                                            <ItemTemplate>
                                                                <asp:ImageButton ID="ImageButton1" runat="server" CommandName="Delete" CausesValidation="false"
                                                                    ImageUrl="~/Account/images/delete.gif" OnClientClick="return confirm('Do you wish to delete this record?');" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                    <PagerStyle CssClass="pgr" />
                                                    <AlternatingRowStyle CssClass="alt" />
                                                </cc11:PagingGridView>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </td>
                        </tr>
                    </table>
                </fieldset>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="imgAdd" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="imgAdd2" EventName="Click"></asp:AsyncPostBackTrigger>
            <asp:AsyncPostBackTrigger ControlID="imgAdd2" EventName="Click"></asp:AsyncPostBackTrigger>
            <asp:AsyncPostBackTrigger ControlID="imgAdd2" EventName="Click"></asp:AsyncPostBackTrigger>
            <asp:AsyncPostBackTrigger ControlID="imgAdd2" EventName="Click"></asp:AsyncPostBackTrigger>
            <asp:AsyncPostBackTrigger ControlID="imgAdd2" EventName="Click"></asp:AsyncPostBackTrigger>
            <asp:AsyncPostBackTrigger ControlID="imgAdd2" EventName="Click"></asp:AsyncPostBackTrigger>
            <asp:AsyncPostBackTrigger ControlID="imgAdd2" EventName="Click"></asp:AsyncPostBackTrigger>
<asp:AsyncPostBackTrigger ControlID="imgAdd2" EventName="Click"></asp:AsyncPostBackTrigger>
<asp:AsyncPostBackTrigger ControlID="imgAdd2" EventName="Click"></asp:AsyncPostBackTrigger>
<asp:AsyncPostBackTrigger ControlID="imgAdd2" EventName="Click"></asp:AsyncPostBackTrigger>
<asp:AsyncPostBackTrigger ControlID="imgAdd2" EventName="Click"></asp:AsyncPostBackTrigger>
<asp:AsyncPostBackTrigger ControlID="imgAdd2" EventName="Click"></asp:AsyncPostBackTrigger>
<asp:AsyncPostBackTrigger ControlID="imgAdd2" EventName="Click"></asp:AsyncPostBackTrigger>
<asp:AsyncPostBackTrigger ControlID="imgAdd2" EventName="Click"></asp:AsyncPostBackTrigger>
<asp:AsyncPostBackTrigger ControlID="imgAdd2" EventName="Click"></asp:AsyncPostBackTrigger>
        </Triggers>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="imgAdd2" EventName="Click" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
