<%@ Page Language="C#" MasterPageFile="~/Master/LicenseMaster.master"
    AutoEventWireup="true" CodeFile="DocumentFolderMaster.aspx.cs" Inherits="Account_DocumentFolderMaster"
    Title="Untitled Page" %>

<%@ Register Assembly="PagingGridView" Namespace="Fadrian.Web.Control" TagPrefix="cc11" %>
<%@ Register Src="~/ioffice/Account/UserControl/UcontrolHelpPanel.ascx" TagName="pnlhelp"
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

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div style="float: left;">
                <asp:Label ID="lblmsg" runat="server" ForeColor="Red"></asp:Label>
            </div>
            <div style="clear: both;">
            </div>
            <div class="products_box">
                <fieldset>
                    <legend>
                        <asp:Label ID="lbllegend" runat="server"></asp:Label>
                    </legend>
                    <div style="float: right;">
                        <asp:Button ID="btnadd" runat="server" Text="Add New Folder" CssClass="btnSubmit"
                            OnClick="btnadd_Click" />
                    </div>
                    <div style="clear: both;">
                    </div>
                    <asp:Panel runat="server" ID="pnladd" Width="100%" Visible="false">
                        <table cellspacing="3" width="100%">
                            <tr>
                                <td align="right">
                                    <label>
                                        <asp:Label ID="Label1" runat="server" Text="Personal Folder"></asp:Label>
                                        <asp:Label ID="Label19" runat="server" CssClass="labelstar" Text="*"></asp:Label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtFoldername"
                                            ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="REG1" runat="server" ErrorMessage="Invalid Character"
                                            Display="Dynamic" SetFocusOnError="True" ValidationExpression="^([a-zA-Z0-9_\s]*)"
                                            ControlToValidate="txtFoldername" ValidationGroup="1"></asp:RegularExpressionValidator>
                                    </label>
                                </td>
                                <td align="left">
                                    <input runat="Server" id="hdnsortExp" name="hdnsortExp" type="hidden" style="width: 1px" />
                                    <input runat="Server" name="hdnsortDir" id="hdnsortDir" type="hidden" style="width: 1px" />
                                    <label>
                                        <asp:TextBox ID="txtFoldername" runat="server" ValidationGroup="1" Width="195px"
                                            MaxLength="20" onKeydown="return mask(event)" onkeyup="return check(this,/[\\/!@#$%^'&*()>.+:;={}[]|\/]/g,/^[\a-zA-Z0-9_\s]+$/,'div1',20)"></asp:TextBox>
                                    </label>
                                    <label>
                                        <asp:Label ID="Label16" runat="server" Text="Max" CssClass="labelcount"></asp:Label>
                                        <span id="div1" class="labelcount">20</span>
                                        <asp:Label ID="lblinvstiename" runat="server" CssClass="labelcount" Text="(A-Z 0-9 _ )"></asp:Label></label>
                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                </td>
                                <td>
                                    <asp:Button ID="imgbtnsubmit" runat="server" Text="Submit" CssClass="btnSubmit" ValidationGroup="1"
                                        OnClick="imgbtnsubmit_Click" />
                                    <asp:Button ID="Button2" runat="server" Text="Update" Visible="false" CssClass="btnSubmit"
                                        ValidationGroup="1" OnClick="Button2_Click" />
                                    <asp:Button ID="btncancel" runat="server" Text="Cancel" CssClass="btnSubmit" OnClick="btncancel_Click" />
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </fieldset>
                <fieldset>
                    <legend>
                        <asp:Label ID="Label2" runat="server" Text="List of My Personal Folders"></asp:Label>
                    </legend>
                    <table width="100%">
                        <tr>
                            <td colspan="2" align="right">
                                <asp:Button ID="Button1" runat="server" Text="Printable Version" CssClass="btnSubmit"
                                    OnClick="Button1_Click" />
                                <input id="Button7" runat="server" onclick="document.getElementById('mydiv').className='open';javascript:CallPrint('divPrint');document.getElementById('mydiv').className='closed';"
                                    style="width: 51px;" type="button" value="Print" visible="false" class="btnSubmit" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:Panel ID="pnlgrid" runat="server">
                                    <table id="GridTbl" width="100%">
                                        <tr>
                                            <td>
                                                <div id="mydiv" class="closed">
                                                    <table width="100%">
                                                        <tr>
                                                            <td align="center" style="text-align: center; font-size: 20px; font-weight: bold;">
                                                                <asp:Label ID="lblcomname" runat="server" Font-Italic="true" Visible="false"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="center" style="text-align: center; font-size: 18px; font-weight: bold;">
                                                                <asp:Label ID="lblhead" runat="server" Font-Italic="true" Text="List of My Personal Folders">
                                                                </asp:Label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <cc11:PagingGridView AutoGenerateColumns="False" ID="gridDocumentFolder" runat="server"
                                                    Width="100%" OnRowCommand="gridDocumentFolder_RowCommand" OnRowEditing="gridDocumentFolder_RowEditing"
                                                    DataKeyNames="FolderID" OnRowDeleting="gridDocumentFolder_RowDeleting" OnRowUpdating="gridDocumentFolder_RowUpdating"
                                                    OnRowCancelingEdit="gridDocumentFolder_RowCancelingEdit" EmptyDataText="No Record Found."
                                                    CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt"
                                                    AllowPaging="True" OnPageIndexChanging="gridDocumentFolder_PageIndexChanging"
                                                    OnSorting="gridDocumentFolder_Sorting" AllowSorting="True">
                                                    <PagerSettings Mode="NumericFirstLast" />
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Folder Name" SortExpression="FolderName" ItemStyle-Width="50%"
                                                            HeaderStyle-HorizontalAlign="Left">
                                                            <ItemTemplate>
                                                                <a onclick="window.open('DocumentRelatedFolders.aspx?Sid=<%#DataBinder.Eval(Container.DataItem, "FolderID")%>', 'welcome','fullscreen=yes,status=yes,top=0,left=0,menubar=no,status=no')"
                                                                    style="color: #426172" href="javascript:void(0)">
                                                                    <%#DataBinder.Eval(Container.DataItem, "FolderName")%>
                                                                </a>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Folder Creation Date" SortExpression="DocumentAddedDate"
                                                            ItemStyle-Width="45%" HeaderStyle-HorizontalAlign="Left">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblDocumentAddedDate" runat="server" Text='<%# Eval("DocumentAddedDate","{0:MM/dd/yyyy-HH:mm}")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField ItemStyle-Width="3%" HeaderStyle-HorizontalAlign="Left" HeaderImageUrl="~/Account/images/edit.gif">
                                                            <ItemTemplate>
                                                                <asp:ImageButton ID="llinedit" runat="server" ToolTip="Edit" CommandArgument='<%# Eval("FolderID") %>'
                                                                    CommandName="Edit" ImageUrl="~/Account/images/edit.gif"></asp:ImageButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Delete" ItemStyle-Width="3%" HeaderStyle-HorizontalAlign="Left"
                                                            HeaderImageUrl="~/ShoppingCart/images/trash.jpg">
                                                            <ItemTemplate>
                                                                <asp:ImageButton ID="ImageButton48" ToolTip="Delete" runat="server" ImageUrl="~/Account/images/delete.gif"
                                                                    CommandName="Delete" OnClientClick="return confirm('Do you wish to delete this record?');"
                                                                    CommandArgument='<%# Eval("FolderID") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                    <PagerStyle CssClass="pgr" />
                                                    <AlternatingRowStyle CssClass="alt" />
                                                </cc11:PagingGridView>
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
