<%@ Page Language="C#" MasterPageFile="~/Master/LicenseMaster.master"
    AutoEventWireup="true" CodeFile="WizardDocumentApproveTypeMaster.aspx.cs" Inherits="WizardDocumentApproveTypeMaster"
    Title="Untitled Page" %>

<%@ Register Assembly="PagingGridView" Namespace="Fadrian.Web.Control" TagPrefix="cc11" %>
<%@ Register Src="~/ioffice/Account/UserControl/UControlWizardpanel.ascx" TagName="pnl" TagPrefix="pnl" %>
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
         
                  return true;
             }
            
           
            if( evt.keyCode==191 ||evt.keyCode==192 ||evt.keyCode==109 || evt.keyCode==221 || evt.keyCode==220 ||evt.keyCode==222 ||evt.keyCode==219 ||evt.keyCode==59||evt.keyCode==186  )
            { 
                
            
              alert("You have entered an invalid character");
                  return false;
             }
             
                        
        }
        function check(txt1, regex, reg, id, max_len) {
            if (txt1.value.length > max_len) {

                txt1.value = txt1.value.substring(0, max_len);
            }
            if (txt1.value != '' && txt1.value.match(reg) == null) {
                txt1.value = txt1.value.replace(regex, '');
                alert("You have entered an invalid character");
            }

            counter = document.getElementById(id);

            if (txt1.value.length <= max_len) {
                remaining_characters = max_len - txt1.value.length;
                counter.innerHTML = remaining_characters;
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

        function checktextboxmaxlength(txt, maxLen, evt) {
            try {
                if (txt.value.length > (maxLen - 1) && evt.keyCode != 8)
                    return false;
            }
            catch (e) {

            }
        }
        
    </script>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div style="margin-left: 1%">
                <asp:Label ID="lblmsg" runat="server" ForeColor="Red"></asp:Label></td>
            </div>
            <div style="clear: both;">
            </div>
            <div class="products_box">
                <fieldset>
                    <legend>
                        <asp:Label ID="Label8" runat="server" Text="Add New Document Approval Type" Visible="False">
                        </asp:Label>
                    </legend>
                    <div style="float: right;">
                        <asp:Button ID="btnadd" runat="server" Text="Add New Document Approval Type" CssClass="btnSubmit"
                            OnClick="btnadd_Click" />
                    </div>
                    <div style="clear: both;">
                    </div>
                    <asp:Panel ID="pnladd" runat="server" Visible="false">
                        <table style="width: 100%">
                            <tr>
                                <td style="width: 20%">
                                    <label>
                                        <asp:Label ID="Label4" runat="server" Text="Business Name"></asp:Label>
                                    </label>
                                </td>
                                <td style="width: 80%">
                                    <label>
                                        <asp:DropDownList ID="ddlbusiness" runat="server">
                                        </asp:DropDownList>
                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 20%">
                                    <label>
                                        <asp:Label ID="Label1" runat="server" Text="Document Approval Type"></asp:Label>
                                        <asp:Label ID="Label2" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtdocapprvltype"
                                            ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ErrorMessage="Invalid Character"
                                            Display="Dynamic" SetFocusOnError="True" ValidationExpression="^([_a-zA-Z0-9\s]*)"
                                            ControlToValidate="txtdocapprvltype" ValidationGroup="1"></asp:RegularExpressionValidator>
                                    </label>
                                </td>
                                <td style="width: 80%">
                                    <label>
                                        <asp:TextBox ID="txtdocapprvltype" runat="server" onKeydown="return mask(event)"
                                            onkeyup="return check(this,/[\\/!#$%^'.@&*()>+:;={}[]|\/]/g,/^[\a-zA-Z0-9_ \s]+$/,'Span1',50)"
                                            MaxLength="50"></asp:TextBox>
                                    </label>
                                    <label>
                                        <asp:Label ID="Label16" runat="server" Text="Max" CssClass="labelcount"></asp:Label>
                                        <span id="Span1" class="labelcount">50</span>
                                        <asp:Label ID="lblinvstiename" runat="server" CssClass="labelcount" Text="(A-Z 0-9 _ )"></asp:Label></label>
                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 20%">
                                    <label>
                                        <asp:Label ID="Label3" runat="server" Text="Add Description"></asp:Label>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Invalid Character"
                                            Display="Dynamic" SetFocusOnError="True" ValidationExpression="^([_.,/a-zA-Z0-9\s]*)"
                                            ControlToValidate="txtdesc" ValidationGroup="1">
                                        </asp:RegularExpressionValidator>
                                    </label>
                                </td>
                                <td style="width: 80%">
                                    <label>
                                        <asp:TextBox ID="txtdesc" runat="server" onkeypress="return checktextboxmaxlength(this,2500,event)"
                                            onkeyup="return check(this,/[\\/!@#$%^'&*()>+:;={}[]|\/]/g,/^[\a-z/A-Z0-9_.,\s]+$/,'Span2',2500)"
                                            TextMode="MultiLine" Height="100px" Width="500px"></asp:TextBox>
                                    </label>
                                    <label>
                                        <asp:Label ID="Label21" runat="server" Text="Max" CssClass="labelcount"></asp:Label>
                                        <span id="Span2" class="labelcount">2500</span>
                                        <asp:Label ID="Label22" runat="server" Text="(A-Z 0-9 _ . , /)" CssClass="labelcount"></asp:Label>
                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 20%">
                                </td>
                                <td style="width: 80%">
                                    <asp:Button ID="imgbtnsubmit" runat="server" Text="Submit" ValidationGroup="1" OnClick="imgbtnsubmit_Click"
                                        CssClass="btnSubmit" />
                                    <asp:Button ID="Button3" runat="server" Text="Update" Visible="false" ValidationGroup="1"
                                        CssClass="btnSubmit" OnClick="Button3_Click" />
                                    <asp:Button ID="Button4" runat="server" CausesValidation="false" Text="Cancel" ValidationGroup="1"
                                        CssClass="btnSubmit" OnClick="Button4_Click" />
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </fieldset>
                <fieldset>
                    <legend>
                        <asp:Label ID="Label5" runat="server" Text="List of Document Approval Type "></asp:Label>
                    </legend>
                    <table width="100%">
                        <tr>
                            <td align="right" colspan="2">
                                <div style="float: right;">
                                    <asp:Button ID="Button2" runat="server" Text="Printable Version" CssClass="btnSubmit"
                                        OnClick="Button1_Click" />
                                    <input id="Button1" runat="server" onclick="document.getElementById('mydiv').className='open';javascript:CallPrint('divPrint');document.getElementById('mydiv').className='closed';"
                                        class="btnSubmit" type="button" value="Print" visible="false" />
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 20%">
                                <label>
                                    <asp:Label ID="Label6" runat="server" Text=" Filter by business Name "></asp:Label>
                                </label>
                            </td>
                            <td style="width: 80%">
                                <label>
                                    <asp:DropDownList ID="ddlbusifil" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlbusiness_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </label>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <table id="GridTbl" width="100%" cellpadding="0" cellspacing="3">
                                    <tr>
                                        <td colspan="2">
                                            <asp:Panel ID="pnlgrid" runat="server">
                                                <table width="100%">
                                                    <tr align="center">
                                                        <td>
                                                            <div id="mydiv" class="closed">
                                                                <table width="100%">
                                                                    <tr>
                                                                        <td align="center">
                                                                            <asp:Label ID="lblcomid" runat="server" Font-Italic="true" Font-Bold="True" Font-Size="20px"
                                                                                Visible="false"></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td align="center">
                                                                            <asp:Label ID="Label7" runat="server" Font-Italic="true" Font-Bold="True" Text="Business : "
                                                                                Font-Size="20px"></asp:Label>
                                                                            <asp:Label ID="lblcomname" runat="server" Font-Italic="true" Font-Bold="True" Font-Size="20px"></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td align="center">
                                                                            <asp:Label ID="lblhead" runat="server" Font-Italic="true" Font-Bold="True" Font-Size="20px"
                                                                                Text="List of Document Approval Type"></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </div>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <cc11:PagingGridView AutoGenerateColumns="False" ID="griddocapprovaltype" runat="server"
                                                                OnRowCommand="griddocapprovaltype_RowCommand" OnRowEditing="griddocapprovaltype_RowEditing"
                                                                DataKeyNames="RuleApproveTypeId" OnRowCancelingEdit="griddocapprovaltype_RowCancelingEdit"
                                                                EmptyDataText="No Rrcord Found." CssClass="mGrid" GridLines="Both" PagerStyle-CssClass="pgr"
                                                                AlternatingRowStyle-CssClass="alt" AllowSorting="True" OnSorting="griddocapprovaltype_Sorting"
                                                                Width="100%" OnPageIndexChanged="griddocapprovaltype_PageIndexChanged" OnPageIndexChanging="griddocapprovaltype_PageIndexChanging">
                                                                <Columns>
                                                                    <asp:TemplateField HeaderText="Business Name" ItemStyle-HorizontalAlign="Left" SortExpression="Name"
                                                                        HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="15%">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblbusname" runat="server" Text='<%# Eval("Name")%>'></asp:Label>
                                                                            <asp:Label ID="lblWhid" runat="server" Text='<%# Eval("Whid")%>' Visible="false"></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Approval Type" ItemStyle-HorizontalAlign="Left" SortExpression="RuleApproveTypeName"
                                                                        HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="15%">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblDocumentApproveType" runat="server" Text='<%# Eval("RuleApproveTypeName")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Description" ItemStyle-HorizontalAlign="Left" SortExpression="Description"
                                                                        HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="65%">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblgdesc" runat="server" Text='<%# Eval("Description")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField ItemStyle-Width="3%" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                                                        HeaderImageUrl="~/Account/images/edit.gif">
                                                                        <ItemTemplate>
                                                                            <asp:ImageButton ID="llinedit" runat="server" ToolTip="Edit" CommandArgument='<%# Eval("RuleApproveTypeId") %>'
                                                                                CommandName="Edit" ImageUrl="~/Account/images/edit.gif"></asp:ImageButton>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Delete" ItemStyle-Width="2%" ItemStyle-HorizontalAlign="Left"
                                                                        HeaderStyle-HorizontalAlign="Left" HeaderImageUrl="~/ShoppingCart/images/trash.jpg">
                                                                        <ItemTemplate>
                                                                            <asp:ImageButton ID="ImageButton1" runat="server" ToolTip="Delete" CommandName="Delete1"
                                                                                CommandArgument='<%# Eval("RuleApproveTypeId") %>' ImageUrl="~/Account/images/delete.gif"
                                                                                OnClientClick="return confirm('Do you wish to delete this record?');" />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                </Columns>
                                                            </cc11:PagingGridView>
                                                            <input style="width: 1px" id="hdnsortExp" type="hidden" name="hdnsortExp" runat="Server" />
                                                            <input style="width: 1px" id="hdnsortDir" type="hidden" name="hdnsortDir" runat="Server" />
                                                            <input id="hdnDocSubTypeId" type="hidden" name="hdnDocSubTypeId" runat="Server" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </asp:Panel>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </fieldset>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
