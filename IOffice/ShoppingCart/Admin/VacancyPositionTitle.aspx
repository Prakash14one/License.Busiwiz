<%@ Page Language="C#" MasterPageFile="~/ShoppingCart/Admin/Master/mp_Admin.master"
    AutoEventWireup="true" CodeFile="VacancyPositionTitle.aspx.cs" Inherits="ShoppingCart_Admin_Master_Default"
    Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">

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
        
     function checktextboxmaxlength(txt, maxLen,evt) {
            try {
                if (txt.value.length > (maxLen - 1) && evt.keyCode != 8)
                    return false;
            }
            catch (e) {

            }
        }
          function mask(evt)
        { 
         
           if(evt.keyCode==13 )
            { 
         
                  
             }
            
           
            if(evt.keyCode==192 ||evt.keyCode==109 || evt.keyCode==221 || evt.keyCode==220 ||evt.keyCode==222 ||evt.keyCode==219||evt.keyCode==59||evt.keyCode==186)
            { 
                
            
              alert("You have entered an invalid character");
                  return false;
             }     
            
        }   
         function check(txt1, regex, reg,id,max_len)
            {
             if (txt1.value.length > max_len) {

                txt1.value = txt1.value.substring(0, max_len);
            }
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
    <asp:UpdatePanel ID="update1" runat="server">
        <ContentTemplate>
            <div style="padding-left: 2%">
                <asp:Label ID="statuslable" runat="server" ForeColor="Red"></asp:Label>
            </div>
            <div style="clear: both;">
            </div>
            <fieldset>
                <legend>
                    <asp:Label ID="lbllegend" runat="server" Text=""></asp:Label>
                </legend>
                <div style="float: right;">
                    <asp:Button ID="btnadd" runat="server" CssClass="btnSubmit" Text="Add Vacancy Title"
                        OnClick="btnadd_Click" />
                </div>
                <div style="clear: both;">
                </div>
                <asp:Panel ID="Pnladdnew" runat="server" Visible="false" Width="100%">
                    <table width="100%">
                        <tr>
                            <td style="width: 30%">
                                <label>
                                    <asp:Label ID="Label3" runat="server" Text="Vacancy Position Type"></asp:Label>
                                    <asp:Label ID="Label4" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="DropDownList1"
                                        runat="server" ErrorMessage="*" ValidationGroup="1" InitialValue="0"></asp:RequiredFieldValidator>
                                </label>
                            </td>
                            <td style="width: 70%">
                                <label>
                                    <asp:DropDownList ID="DropDownList1" runat="server">
                                    </asp:DropDownList>
                                </label>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 30%">
                                <label>
                                    <asp:Label ID="Label1" runat="server" Text="Vacancy Position Title"></asp:Label>
                                    <asp:Label ID="Label5" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ControlToValidate="txtvacancytype"
                                        runat="server" ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                                </label>
                            </td>
                            <td style="width: 70%">
                                <label>
                                    <asp:TextBox ID="txtvacancytype" runat="server" onKeydown="return mask(event)" MaxLength="60"
                                        onkeyup="return check(this,/[\\/!@#$%^'&*()>+;={}[]|\/]/g,/^[\ _,?a-zA-Z.0-9\s]+$/,'div1',60)"></asp:TextBox>
                                </label>
                                <label>
                                    <asp:Label runat="server" ID="sadasd" Text="Max "></asp:Label>
                                    <span id="div1">60</span>
                                    <asp:Label ID="lblinvstiename" runat="server" Text="(A-Z 0-9 . , ? _)"></asp:Label>
                                </label>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 30%">
                                <label>
                                    <asp:Label ID="Label2" runat="server" Text="Status"></asp:Label>
                                </label>
                            </td>
                            <td style="width: 70%">
                                <label>
                                    <%--<asp:CheckBox ID="CheckBox1" runat="server" />--%>
                                    <asp:DropDownList ID="ddlstatus" runat="server">
                                        <asp:ListItem Selected="True" Text="Active" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="Inactive" Value="0"></asp:ListItem>
                                    </asp:DropDownList>
                                </label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                            <td>
                                <asp:Button ID="btnsubmit" runat="server" Text="Submit" CssClass="btnSubmit" OnClick="btnsubmit_Click"
                                    ValidationGroup="1" />
                                <asp:Button ID="btnupdate" runat="server" Text="Update" CssClass="btnSubmit" Visible="false"
                                    OnClick="btnupdate_Click" ValidationGroup="1" />
                                <asp:Button ID="btncancel" runat="server" Text="Cancel" CssClass="btnSubmit" OnClick="btncancel_Click" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                            <td>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </fieldset>
            <fieldset>
                <legend>
                    <asp:Label ID="lbllege" runat="server" Text="List of Vacancy Position Titles"></asp:Label>
                </legend>
                <div style="clear: both;">
                </div>
                <table width="100%">
                    <tr>
                        <td>
                            <div style="float: right;">
                                <asp:Button ID="btncancel0" runat="server" CssClass="btnSubmit " CausesValidation="false"
                                    OnClick="btncancel0_Click" Text="Printable Version" />
                                <input id="Button7" runat="server" visible="false" onclick="document.getElementById('mydiv').className='open';javascript:CallPrint('divPrint');document.getElementById('mydiv').className='closed';"
                                    class="btnSubmit" type="button" value="Print" />
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Panel ID="pnlgrid" runat="server" Width="100%">
                                <table width="100%">
                                    <tr align="center">
                                        <td>
                                            <div id="mydiv" class="closed">
                                                <table width="100%">
                                                    <tr>
                                                        <td align="center" style="text-align: center; font-size: 18px; font-weight: bold;">
                                                            <asp:Label ID="Label11" Font-Italic="true" runat="server" Text="List of Vacancy Position Titles"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            <asp:GridView ID="GridView1" runat="server" Width="100%" CssClass="mGrid" PagerStyle-CssClass="pgr"
                                                EmptyDataText="No Record Found." AlternatingRowStyle-CssClass="alt" AutoGenerateColumns="False"
                                                OnRowCommand="GridView1_RowCommand" OnRowDeleting="GridView1_RowDeleting" OnRowEditing="GridView1_RowEditing">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Vacancy Position Type" HeaderStyle-HorizontalAlign="Left"
                                                        HeaderStyle-Width="30%">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Labecxvxl11" runat="server" Text='<%# Eval("Name") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Left" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Vacancy Position Title" HeaderStyle-HorizontalAlign="Left"
                                                        HeaderStyle-Width="35%">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label11" runat="server" Text='<%# Eval("VacancyPositionTitle") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Left" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Status" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="15%">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label12" runat="server" Text='<%# Eval("Statuslabel") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Left" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderImageUrl="~/Account/images/edit.gif" HeaderStyle-HorizontalAlign="Left"
                                                        HeaderStyle-Width="2%">
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="ImageButton48" runat="server" CommandName="Edit" CommandArgument='<%# Eval("ID") %>'
                                                                ToolTip="Edit" ImageUrl="~/Account/images/edit.gif" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderImageUrl="~/ShoppingCart/images/trash.jpg" HeaderStyle-HorizontalAlign="Left"
                                                        HeaderStyle-Width="2%">
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="llinkbb" runat="server" CommandName="Delete" ToolTip="Delete"
                                                                CommandArgument='<%# Eval("ID") %>' ImageUrl="~/Account/images/delete.gif" OnClientClick="return confirm('Do you wish to delete this record?');">
                                                            </asp:ImageButton>
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
                        </td>
                    </tr>
                </table>
            </fieldset>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
