<%@ Page Language="C#" MasterPageFile="~/Master/LicenseMaster.master"
    AutoEventWireup="true" CodeFile="DocumentMainType.aspx.cs" Inherits="ShoppingCart_Admin_DocumentMainType"
    Title="Untitled Page" %>

<%@ Register Assembly="PagingGridView" Namespace="Fadrian.Web.Control" TagPrefix="cc11" %>
<%@ Register Src="~/ioffice/ShoppingCart/Admin/UserControl/UcontrolHelpPanel.ascx" TagName="pnlhelp"
    TagPrefix="pnlhelp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script language="javascript" type="text/javascript">

        function CallPrint(strid) {
            var prtContent = document.getElementById('<%= Panel2.ClientID %>');
            var WinPrint = window.open('', '', 'left=0,top=0,width=1024px,height=768px,toolbar=1,scrollbars=0,status=0');
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
                
            
              alert("You have entered an invalid character");
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
            <div style="margin-left: 1%">
                <asp:Label ID="lblmsg" runat="server" ForeColor="Red"></asp:Label>
            </div>
            <div style="clear: both;">
            </div>
            <div class="products_box">
                <fieldset>
                    <legend>
                        <asp:Label ID="Label6" runat="server" Text="Add New Document Cabinet" Visible="False">
                        </asp:Label>
                    </legend>
                    <div style="float: right;">
                        <asp:Button ID="btnadd" runat="server" Text="Add New Document Cabinet" CssClass="btnSubmit"
                            OnClick="btnadd_Click" />
                    </div>
                    <div style="clear: both;">
                    </div>
                    <asp:Panel ID="pnladd" runat="server" Visible="false">
                        <table id="subinnertbl" width="100%" cellspacing="3">
                            <tr>
                                <td style="width: 40%">
                                    <label>
                                        <asp:Label runat="server" ID="Label2" Text="Business Name" Visible="true"></asp:Label>
                                    </label>
                                </td>
                                <td style="width: 60%">
                                    <label>
                                        <asp:DropDownList ID="ddlbusiness" runat="server">
                                        </asp:DropDownList>
                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 40%">
                                    <label>
                                        <asp:Label runat="server" ID="Label3" Text=" Cabinet Name" Visible="true"></asp:Label>
                                        <asp:Label runat="server" ID="Label4" Text="*" Visible="true"></asp:Label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtdocmaintype"
                                            ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Invalid Character"
                                            Display="Dynamic" SetFocusOnError="True" ValidationExpression="^([_a-zA-Z0-9\s]*)"
                                            ControlToValidate="txtdocmaintype" ValidationGroup="1"></asp:RegularExpressionValidator>
                                    </label>
                                </td>
                                <td style="width: 60%">
                                    <label>
                                        <asp:TextBox ID="txtdocmaintype" onKeydown="return mask(event)" onkeyup="return check(this,/[\\/!@#$%^'.&*()>+:;={}[]|\/]/g,/^[\a-zA-Z0-9_ ]+$/,'div1',30)"
                                            MaxLength="30" runat="server" ValidationGroup="1"></asp:TextBox>
                                    </label>
                                    <label>
                                        <asp:Label ID="Label16" runat="server" Text="Max" CssClass="labelcount"></asp:Label>
                                        <span id="div1" class="labelcount">30</span>
                                        <asp:Label ID="lblinvstiename" runat="server" CssClass="labelcount" Text="(A-Z 0-9 _ )"></asp:Label>
                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 40%">
                                </td>
                                <td style="width: 60%">
                                    <asp:CheckBox ID="CheckBox1" runat="server" />
                                    <label>
                                        <asp:Label runat="server" ID="Label7" Text="Do you wish to create drawers for this cabinet now? (This is recommended) "
                                            Visible="true"></asp:Label>
                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 40%">
                                </td>
                                <td style="width: 60%">
                                    <input id="Hidden1" runat="Server" name="hdnsortExp" style="width: 1px" type="hidden" />
                                    <input id="Hidden2" runat="Server" name="hdnsortDir" style="width: 1px" type="hidden" />
                                    <asp:Button ID="imgbtnsubmit" runat="server" CssClass="btnSubmit" Text="Submit" ValidationGroup="1"
                                        OnClick="imgbtnsubmit_Click" />
                                    <asp:Button ID="btnupdate" runat="server" CssClass="btnSubmit" Text="Update" Visible="false"
                                        ValidationGroup="1" OnClick="btnupdate_Click" />
                                    <asp:Button ID="imdcancel" runat="server" CssClass="btnSubmit" OnClick="imdcancel_Click"
                                        Text="Cancel" />
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
                        <asp:Label ID="lbllegend" runat="server" Text="List of Document Cabinets"></asp:Label>
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
                            <td style="width: 40%">
                                <label>
                                    <asp:Label runat="server" ID="Label5" Text="Select by Business" Visible="true"></asp:Label>
                                </label>
                                <label>
                                    <asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged"
                                        Width="205px">
                                    </asp:DropDownList>
                                </label>
                            </td>
                            <td style="width: 60%">
                            </td>
                        </tr>
                        <input id="hdnsortExp" runat="Server" name="hdnsortExp" style="width: 1px" type="hidden" />
                        <input id="hdnsortDir" runat="Server" name="hdnsortDir" style="width: 1px" type="hidden" />
                        <tr>
                            <td colspan="2">
                                <asp:Panel ID="Panel2" runat="server" Width="100%">
                                    <table width="100%" cellpadding="5" cellspacing="1" border="0" style="vertical-align: top;">
                                        <tr align="center">
                                            <td colspan="2">
                                                <div id="mydiv" class="closed">
                                                    <table width="100%">
                                                        <tr>
                                                            <td colspan="2" align="center" style="text-align: center; font-size: 20px; font-weight: bold;">
                                                                <asp:Label ID="lblBusiness0" Visible="false" runat="server" Text="" Font-Italic="True"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="2" align="center" style="text-align: center; font-size: 20px; font-weight: bold;">
                                                                <asp:Label ID="lblsomethins" runat="server" Text="Business:" Font-Italic="True"></asp:Label>
                                                                <asp:Label ID="lblBusiness" runat="server" Text="" Font-Italic="True"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="2" align="center" style="text-align: center; font-size: 18px; font-weight: bold;">
                                                                <asp:Label ID="Label1" runat="server" Font-Italic="true" Text="List of Document Cabinets"
                                                                    ForeColor="Black"></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <cc11:PagingGridView AutoGenerateColumns="False" BorderWidth="1px" ID="griddocmaintype"
                                                    runat="server" Width="100%" OnRowEditing="griddocmaintype_RowEditing" DataKeyNames="DocumentMainTypeId"
                                                    OnRowDeleting="griddocmaintype_RowDeleting" OnRowCancelingEdit="griddocmaintype_RowCancelingEdit"
                                                    EmptyDataText="No Record Found." CssClass="mGrid" GridLines="Both" PagerStyle-CssClass="pgr"
                                                    AlternatingRowStyle-CssClass="alt" OnPageIndexChanging="griddocmaintype_PageIndexChanging"
                                                    AllowSorting="true" OnSorting="griddocmaintype_Sorting" 
                                                    OnRowCommand="griddocmaintype_RowCommand" PageSize="20">
                                                    <PagerSettings Mode="NumericFirstLast" />
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Business Name" SortExpression="Name" ItemStyle-Width="45%"
                                                            HeaderStyle-HorizontalAlign="Left">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblwname" runat="server" Text='<%# Eval("Name")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Cabinet" SortExpression="DocumentMainType" HeaderStyle-HorizontalAlign="Left"
                                                            ItemStyle-Width="50%">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblDocumentMainType" runat="server" Text='<%# Eval("DocumentMainType")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField ItemStyle-Width="3%" HeaderStyle-HorizontalAlign="Left" HeaderImageUrl="~/Account/images/edit.gif">
                                                            <ItemTemplate>
                                                                <asp:ImageButton ID="llinedit" runat="server" ToolTip="Edit" CommandArgument='<%# Eval("DocumentMainTypeId") %>'
                                                                    CommandName="Edit" ImageUrl="~/Account/images/edit.gif"></asp:ImageButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Delete" ItemStyle-Width="2%" ItemStyle-HorizontalAlign="Left"
                                                            HeaderStyle-HorizontalAlign="Left" HeaderImageUrl="~/ShoppingCart/images/trash.jpg">
                                                            <ItemTemplate>
                                                                <asp:ImageButton ID="ImageButton1" runat="server" ToolTip="Delete" CommandName="Delete"
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
    </asp:UpdatePanel>
</asp:Content>
