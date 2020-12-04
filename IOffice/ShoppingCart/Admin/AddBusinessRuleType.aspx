<%@ Page Language="C#" MasterPageFile="~/Master/LicenseMaster.master"
    AutoEventWireup="true" CodeFile="AddBusinessRuleType.aspx.cs" Inherits="Account_AddBusinessRuleType"
    Title="Untitled Page" %>

<%@ Register Assembly="PagingGridView" Namespace="Fadrian.Web.Control" TagPrefix="cc11" %>
<%@ Register Src="~/ioffice/ShoppingCart/Admin/UserControl/UcontrolHelpPanel.ascx" TagName="pnlhelp"
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
            var prtContent = document.getElementById('<%= pnlgrid.ClientID %>');
            var WinPrint = window.open('', '', 'left=0,top=0,width=1024,height=768,toolbar=1,scrollbars=1,status=0');
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
                alert("You have entered an invalid character");
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

    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <ContentTemplate>
            <div style="margin-left: 1%">
                <asp:Label ID="lblmsg" runat="server" ForeColor="Red"></asp:Label>
            </div>
            <div style="clear: both;">
            </div>
            <div class="products_box">
                <fieldset>
                    <legend>
                        <asp:Label ID="Label6" runat="server" Text="Add New Document Flow Rule Type" Visible="False">
                        </asp:Label>
                    </legend>
                    <div style="float: right;">
                        <asp:Button ID="btnadd" runat="server" Text="Add New Document Flow Rule Type" CssClass="btnSubmit"
                            OnClick="btnadd_Click" />
                    </div>
                    <asp:Panel ID="pnladd" runat="server" Visible="false">
                        <table width="100%">
                            <tr>
                                <td style="width: 20%;">
                                    <label>
                                        <asp:Label ID="lbldoyou" runat="server" Text="Business Name"></asp:Label>
                                    </label>
                                </td>
                                <td style="width: 80%;">
                                    <label>
                                        <asp:DropDownList ID="ddlbusiness" runat="server" AutoPostBack="false" Width="206px">
                                        </asp:DropDownList>
                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 20%;">
                                    <label>
                                        <asp:Label ID="Label7" runat="server" Text="Add Rule Type"></asp:Label>
                                        <asp:Label ID="Label2" runat="server" Text="*"></asp:Label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtftp"
                                            ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="REG1" runat="server" ErrorMessage="Invalid Character"
                                            Display="Dynamic" SetFocusOnError="True" ValidationExpression="^([_a-zA-Z0-9\s]*)"
                                            ControlToValidate="txtftp" ValidationGroup="1"></asp:RegularExpressionValidator>
                                    </label>
                                </td>
                                <td style="width: 80%;">
                                    <label>
                                        <asp:TextBox ID="txtftp" runat="server" onKeydown="return mask(event)" onkeyup="return check(this,/[\\/!@#$%^'.&*()>+:;={}[]|\/]/g,/^[\a-zA-Z0-9_ ]+$/,'div1',30)"
                                            Width="200px" MaxLength="30" ValidationGroup="1"></asp:TextBox>
                                    </label>
                                    <label>
                                        <asp:Label ID="Label29" runat="server" Text="Max" CssClass="labelcount"></asp:Label>
                                        <span id="div1" class="labelcount">30</span>
                                        <asp:Label ID="lblinvstiename" runat="server" CssClass="labelcount" Text="(A-Z 0-9 _)"></asp:Label></label>
                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                </td>
                                <td>
                                    <asp:Button ID="imgbtnsubmit" runat="server" Text="Submit" ValidationGroup="1" CssClass="btnSubmit"
                                        OnClick="imgbtnsubmit_Click" />
                                    <asp:Button ID="Button3" runat="server" Text="Update" ValidationGroup="1" CssClass="btnSubmit"
                                        OnClick="Button3_Click" Visible="False" />
                                    <asp:Button ID="btncancel" Text="Cancel" runat="server" CssClass="btnSubmit" OnClick="btncancel_Click" />
                                    <div style="clear: both;">
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </fieldset>
                <fieldset>
                    <legend>
                        <asp:Label ID="lbllegnnd" runat="server" Text="List of Document Flow Rule Type"></asp:Label>
                    </legend>
                    <table width="100%">
                        <tr>
                            <td align="right" colspan="2">
                                <div style="float: right;">
                                    <asp:Button ID="Button2" runat="server" CssClass="btnSubmit" Text="Printable Version"
                                        OnClick="Button1_Click" />
                                    <input id="Button1" runat="server" class="btnSubmit" onclick="document.getElementById('mydiv').className='open';javascript:CallPrint('divPrint');document.getElementById('mydiv').className='closed';"
                                        type="button" value="Print" visible="false" />
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <label>
                                    <asp:Label ID="Label1" runat="server" Text="Select by Business "></asp:Label>
                                </label>
                                <label>
                                    <asp:DropDownList ID="ddlbusifil" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlbusiness_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </label>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:Panel ID="pnlgrid" runat="server" Width="100%">
                                    <table id="Table1" width="100%">
                                        <tr>
                                            <td colspan="2">
                                                <div id="mydiv" class="closed">
                                                    <table width="100%">
                                                        <tr>
                                                            <td align="center" colspan="2">
                                                                <asp:Label ID="lblcom" runat="server" Font-Italic="true" Font-Bold="True" Font-Size="20px"
                                                                    Visible="false"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="center" colspan="2">
                                                                <asp:Label ID="Label4" runat="server" Font-Bold="True" Font-Italic="true" Text="Business:"
                                                                    Font-Size="20px"></asp:Label>
                                                                <asp:Label ID="lblcomname" runat="server" Font-Italic="true" Font-Bold="True" Font-Size="20px"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="center" colspan="2">
                                                                <asp:Label ID="lblhead" runat="server" Font-Italic="true" Font-Bold="True" Font-Size="18px"
                                                                    Text="List of Document Flow Rule Type"></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                                <cc11:PagingGridView AutoGenerateColumns="False" ID="griddocapprovaltype" runat="server"
                                                    DataKeyNames="RuleTypeId" EmptyDataText="No Record Found." CssClass="mGrid" GridLines="Both"
                                                    PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt" AllowSorting="True"
                                                    OnRowCancelingEdit="griddocapprovaltype_RowCancelingEdit" OnRowCommand="griddocapprovaltype_RowCommand"
                                                    OnRowDeleting="griddocapprovaltype_RowDeleting" OnRowEditing="griddocapprovaltype_RowEditing"
                                                    OnSorting="griddocapprovaltype_Sorting" Width="100%" OnPageIndexChanging="griddocapprovaltype_PageIndexChanging">
                                                    <PagerSettings Mode="NumericFirstLast" />
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Business Name" SortExpression="Name" ItemStyle-HorizontalAlign="Left"
                                                            ItemStyle-Width="40%" HeaderStyle-HorizontalAlign="Left">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblbusname" runat="server" Text='<%# Eval("Name")%>'></asp:Label>
                                                                <asp:Label ID="lblWhid" runat="server" Text='<%# Eval("Whid")%>' Visible="false"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Rule Type Name" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="55%"
                                                            SortExpression="RuleType" HeaderStyle-HorizontalAlign="Left">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblDocumentApproveType" runat="server" Text='<%# Eval("RuleType")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField ItemStyle-Width="3%" HeaderStyle-HorizontalAlign="Left" HeaderImageUrl="~/Account/images/edit.gif">
                                                            <ItemTemplate>
                                                                <asp:ImageButton ID="llinedit" runat="server" ToolTip="Edit" CommandArgument='<%# Eval("RuleTypeId") %>'
                                                                    CommandName="Edit" ImageUrl="~/Account/images/edit.gif"></asp:ImageButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Delete" HeaderImageUrl="~/ShoppingCart/images/trash.jpg"
                                                            ItemStyle-Width="2%" HeaderStyle-HorizontalAlign="Left">
                                                            <ItemTemplate>
                                                                <asp:ImageButton ID="llinkbb" runat="server" ToolTip="Delete" CommandName="Delete"
                                                                    ImageUrl="~/Account/images/delete.gif" OnClientClick="return confirm('Do you wish to delete this record?');">
                                                                </asp:ImageButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                    <PagerStyle CssClass="pgr" />
                                                    <AlternatingRowStyle CssClass="alt" />
                                                </cc11:PagingGridView>
                                                <input style="width: 1px" id="hdnsortExp" type="hidden" name="hdnsortExp" runat="Server" />
                                                <input style="width: 1px" id="hdnsortDir" type="hidden" name="hdnsortDir" runat="Server" />
                                                <input id="hdnRuleTypeId" type="hidden" name="hdnRuleTypeId" runat="Server" />
                                                <input id="hdncnfm" type="hidden" name="hdncnfm" runat="Server" />
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
