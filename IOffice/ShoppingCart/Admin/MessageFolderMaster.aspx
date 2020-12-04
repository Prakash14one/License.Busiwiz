<%@ Page Language="C#" MasterPageFile="~/Master/LicenseMaster.master"
    AutoEventWireup="true" CodeFile="MessageFolderMaster.aspx.cs" Inherits="ShoppingCart_Admin_MessageFolderMaster"
    Title="Untitled Page" %>

<%@ Register Src="~/ioffice/ShoppingCart/Admin/UserControl/UcontrolHelpPanel.ascx" TagName="pnlhelp"
    TagPrefix="pnlhelp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="Winthusiasm.HtmlEditor" Namespace="Winthusiasm.HtmlEditor"
    TagPrefix="cc2" %>
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
        
            function mask(evt)
        { 
         
           if(evt.keyCode==13 )
            { 
         
                  return false;
             }
            
           
            if(evt.keyCode==188 ||evt.keyCode==191 ||evt.keyCode==192 ||evt.keyCode==109 || evt.keyCode==221 || evt.keyCode==220 ||evt.keyCode==222 ||evt.keyCode==219 ||evt.keyCode==59||evt.keyCode==186  )
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
    <asp:UpdatePanel ID="UpdatePanelUserMng" runat="server">
        <ContentTemplate>
            <div class="products_box">
                <div style="padding-left: 1%">
                    <asp:Label ID="labelmsg" runat="server" ForeColor="Red" Text=""></asp:Label>
                </div>
                <div style="clear: both;">
                </div>
                <fieldset>
                    <legend>
                        <asp:Label ID="lbllegend" runat="server" Text=""></asp:Label>
                    </legend>
                    <div style="float: right;">
                        <asp:Button ID="btnadddd" runat="server" Text="Add New Message Folder" OnClick="btnadddd_Click"
                            Width="180px" CssClass="btnSubmit" />
                    </div>
                    <div style="clear: both;">
                    </div>
                    <asp:Panel runat="server" ID="pnladdd" Width="100%" Visible="false">
                        <table cellspacing="0" cellpadding="0" width="100%">
                            <tr>
                                <td width="300px">
                                    <label>
                                        <asp:Label ID="Label2" runat="server" Text="Business Name"></asp:Label>
                                    </label>
                                </td>
                                <td style="width: 23%">
                                    <label>
                                        <asp:DropDownList ID="ddlstore" runat="server" Width="203px">
                                        </asp:DropDownList>
                                    </label>
                                </td>
                                <td valign="bottom">
                                    <asp:ImageButton ID="imgadddept" runat="server" Height="20px" ImageUrl="~/ShoppingCart/images/AddNewRecord.jpg"
                                        ToolTip="AddNew" Width="20px" ImageAlign="Bottom" OnClick="imgadddept_Click" />
                                    <asp:ImageButton ID="imgdeptrefresh" runat="server" AlternateText="Refresh" Height="20px"
                                        ImageUrl="~/ShoppingCart/images/DataRefresh.jpg" ToolTip="Refresh" Width="20px"
                                        ImageAlign="Bottom" OnClick="imgdeptrefresh_Click" />
                                </td>
                            </tr>
                            <tr>
                                <td width="300px">
                                    <label>
                                        <asp:Label ID="Label3" runat="server" Text="Folder Name"></asp:Label>
                                        <asp:Label ID="asdsadsad" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                        <asp:RequiredFieldValidator ID="redsd" runat="server" ControlToValidate="txtmessagefolder"
                                            SetFocusOnError="true" ValidationGroup="7" ErrorMessage="*"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="REG1" runat="server" ErrorMessage="Invalid Character"
                                            Display="Dynamic" SetFocusOnError="True" ValidationExpression="^([a-zA-Z0-9_\s]*)"
                                            ControlToValidate="txtmessagefolder" ValidationGroup="7"></asp:RegularExpressionValidator>
                                    </label>
                                </td>
                                <td colspan="2">
                                    <label>
                                        <asp:TextBox ID="txtmessagefolder" runat="server" Width="198px" MaxLength="20" onKeydown="return mask(event)"
                                            onkeyup="return check(this,/[\\/!@#$%^'&*()>.+:;={}[]|\/]/g,/^[\a-zA-Z0-9_\s]+$/,'div1',20)"></asp:TextBox>
                                    </label>
                                    <label>
                                        <asp:Label runat="server" ID="sadasd" Text="Max" CssClass="labelcount"></asp:Label>
                                        <span id="div1" cssclass="labelcount">20</span>
                                        <asp:Label ID="Label6" runat="server" Text="(A-Z 0-9 _)" CssClass="labelcount"></asp:Label>
                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td width="300px">
                                </td>
                                <td colspan="2">
                                    <asp:Button ID="btninsert" Text="Submit" runat="server" OnClick="btninsert_Click"
                                        ValidationGroup="7" CssClass="btnSubmit" />
                                    <asp:Button ID="btnCancel" runat="server" OnClick="btnCancel_Click" Text="Cancel"
                                        CssClass="btnSubmit" />
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </fieldset>
                <fieldset>
                    <legend>
                        <asp:Label ID="Label4" runat="server" Text="List of Message Folders"></asp:Label>
                    </legend>
                    <table width="100%">
                        <tr>
                            <td colspan="4">
                                <div style="float: right">
                                    <asp:Button ID="Button1" runat="server" CssClass="btnSubmit" Text="Printable Version"
                                        OnClick="Button1_Click1" />
                                    <input id="Button2" runat="server" class="btnSubmit" onclick="document.getElementById('mydiv').className='open';javascript:CallPrint('divPrint');document.getElementById('mydiv').className='closed';"
                                        style="width: 51px;" type="button" value="Print" visible="false" />
                                </div>
                                <div style="clear: both;">
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4" valign="top">
                                <label>
                                    <asp:Label ID="Label5" runat="server" Text="Filter by Business"></asp:Label>
                                </label>
                                <label>
                                    <asp:DropDownList ID="ddlfilterstore" runat="server" Width="203px" AutoPostBack="True"
                                        OnSelectedIndexChanged="ddlstore_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </label>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4">
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                    <ContentTemplate>
                                        <asp:Panel ID="pnlgrid" runat="server" Width="100%">
                                            <table width="100%">
                                                <tr>
                                                    <td>
                                                        <div id="mydiv" class="closed">
                                                            <table width="850Px">
                                                                <tr align="center">
                                                                    <td align="center" style="text-align: center; font-size: 20px; font-weight: bold;">
                                                                        <asp:Label ID="lblCompany" runat="server" ForeColor="Black" Font-Italic="true" Visible="false"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr align="center">
                                                                    <td align="center" style="text-align: center; font-size: 18px; font-weight: bold;">
                                                                        <asp:Label ID="Label7" runat="server" Text="Business : " Font-Italic="true"></asp:Label>
                                                                        <asp:Label ID="lblBusiness" runat="server" ForeColor="Black" Font-Italic="true"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr align="center">
                                                                    <td align="center" style="text-align: center; font-size: 18px; font-weight: bold;">
                                                                        <asp:Label ID="Label24" runat="server" Font-Italic="true" Text="List of Message Folders"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </div>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Panel ID="Panel2" runat="server">
                                                            <asp:GridView AutoGenerateColumns="False" ID="GridEmail" runat="server" Width="100%"
                                                                OnRowCommand="GridEmail_RowCommand" CssClass="mGrid" PagerStyle-CssClass="pgr"
                                                                AlternatingRowStyle-CssClass="alt" EmptyDataText="No Record Found." DataKeyNames="ID"
                                                                OnRowEditing="GridEmail_RowEditing" OnRowDeleting="GridEmail_RowDeleting" AllowSorting="True"
                                                                OnSorting="GridEmail_Sorting">
                                                                <Columns>
                                                                    <asp:TemplateField HeaderText="Business Name" SortExpression="Name" HeaderStyle-HorizontalAlign="Left"
                                                                        ItemStyle-Width="26%">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblbusname" runat="server" Text='<%# Bind("Name")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                        <ItemStyle Width="26%" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Email ID" SortExpression="EmailId" HeaderStyle-HorizontalAlign="Left"
                                                                        ItemStyle-Width="34%">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblemailid" runat="server" Text='<%# Bind("EmailId")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                        <ItemStyle Width="34%" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Folder Name" SortExpression="Foldername" HeaderStyle-HorizontalAlign="Left"
                                                                        ItemStyle-Width="34%">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblfoldername" runat="server" Text='<%# Bind("Foldername")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                        <ItemStyle Width="34%" />
                                                                    </asp:TemplateField>
                                                                    <%--    <asp:ButtonField CommandName="Edit" ButtonType="Image" HeaderText="Edit" HeaderImageUrl="~/Account/images/edit.gif"
                                                                        HeaderStyle-HorizontalAlign="Left" Text="Edit" ImageUrl="~/Account/images/edit.gif"
                                                                        ItemStyle-Width="3%">
                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                        <ItemStyle Width="3%" />
                                                                    </asp:ButtonField>--%>
                                                                    <%-- <asp:ButtonField  CommandName="Delete1" ButtonType="Image" HeaderText="Delete" Text="Delete" ImageUrl="~/Account/images/delete.gif" ItemStyle-Width="3%" >
                                                      
                                                        
                                                       <ItemStyle Width="3%" />
                                                      
                                                        
                                                    </asp:ButtonField>--%>
                                                                    <asp:TemplateField HeaderText="Edit" HeaderImageUrl="~/Account/images/edit.gif" HeaderStyle-HorizontalAlign="Left">
                                                                        <ItemTemplate>
                                                                            <asp:ImageButton ID="ImageButton49" ImageUrl="~/Account/images/edit.gif" runat="server"
                                                                                ToolTip="Edit" CommandArgument='<%# Eval("ID") %>' CommandName="Edit" />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Delete" HeaderImageUrl="~/ShoppingCart/images/trash.jpg"
                                                                        HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="3%">
                                                                        <ItemTemplate>
                                                                            <asp:ImageButton ID="ImageButton48" runat="server" CommandArgument='<%# Eval("ID") %>'
                                                                                ToolTip="Delete" CommandName="Delete" ImageUrl="~/Account/images/delete.gif"
                                                                                OnClientClick="return confirm('Do you wish to delete this record?');" />
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                        <ItemStyle HorizontalAlign="Left" Width="3%" />
                                                                    </asp:TemplateField>
                                                                </Columns>
                                                                <PagerStyle CssClass="pgr" />
                                                                <AlternatingRowStyle CssClass="alt" />
                                                            </asp:GridView>
                                                        </asp:Panel>
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:Panel>
                                    </ContentTemplate>
                                    <%--  <Triggers>
                         
                           
                            <asp:AsyncPostBackTrigger ControlID="btninsert" EventName="Click" />
                            </Triggers>--%>
                                </asp:UpdatePanel>
                                <input style="width: 1px" id="hdnsortExp" type="hidden" name="hdnsortExp" runat="Server" />
                                <input style="width: 1px" id="hdnsortDir" type="hidden" name="hdnsortDir" runat="Server" />
                                <input id="hdnMaintypeAdd" runat="Server" name="hdnMaintypeAdd" type="hidden" style="width: 4px" />
                                <input id="Hidden1" runat="Server" name="Hidden1" type="hidden" style="width: 4px" />
                            </td>
                        </tr>
                    </table>
                </fieldset>
                <table width="100%">
                    <tr>
                        <td colspan="4">
                            <asp:Panel ID="pnlMainTypeAdd" runat="server" BorderStyle="Outset" Width="300px"
                                BackColor="#CCCCCC" BorderColor="#333333">
                                <table id="innertbl1">
                                    <tr>
                                        <td>
                                            <asp:UpdatePanel ID="UpdatePanelMainTypeAdd" runat="server" UpdateMode="Conditional">
                                                <ContentTemplate>
                                                    <table id="Table1" cellspacing="0" cellpadding="0">
                                                        <tbody>
                                                            <tr>
                                                                <td>
                                                                    Confirm Message
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:Panel ID="pnlMainTypeAdd1" runat="server" Width="100%" Height="95px">
                                                                        <table id="Table2" cellspacing="0" cellpadding="0">
                                                                            <tbody>
                                                                                <tr>
                                                                                    <td colspan="2" style="font-weight: bold; padding-left: 10px; font-size: 12px; font-family: Arial;
                                                                                        text-align: left; vertical-align: top;" width="350px">
                                                                                        <br />
                                                                                        <asp:Label ID="lbb" runat="Server" Text="Are you sure you want to delete record?"></asp:Label>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td style="padding-left: 15px;">
                                                                                        <br />
                                                                                        <asp:UpdatePanel ID="UpdatePanel16" runat="server">
                                                                                            <ContentTemplate>
                                                                                                <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Account/images/btnconfirm.jpg"
                                                                                                    AlternateText="Close" CausesValidation="False" OnClick="ImageButton1_Click">
                                                                                                </asp:ImageButton>
                                                                                            </ContentTemplate>
                                                                                        </asp:UpdatePanel>
                                                                                    </td>
                                                                                    <td>
                                                                                        <br />
                                                                                        <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/Account/images/btncancel.jpg"
                                                                                            AlternateText="Close" CausesValidation="False" OnClick="ImageButton2_Click">
                                                                                        </asp:ImageButton>
                                                                                    </td>
                                                                                </tr>
                                                                            </tbody>
                                                                        </table>
                                                                    </asp:Panel>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                            <cc1:ModalPopupExtender ID="ModalPopupExtender9" runat="server" BackgroundCssClass="modalBackground"
                                PopupControlID="pnlMainTypeAdd" TargetControlID="hdnMaintypeAdd" CancelControlID="ImageButton2"
                                X="250" Y="-200" Drag="true">
                            </cc1:ModalPopupExtender>
                        </td>
                    </tr>
                </table>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
