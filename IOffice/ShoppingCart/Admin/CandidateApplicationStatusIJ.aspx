<%@ Page Language="C#" MasterPageFile="~/Master/LicenseMaster.master"
    AutoEventWireup="true" CodeFile="CandidateApplicationStatusIJ.aspx.cs" Inherits="ShoppingCart_Admin_CandidateApplicationStatusIJ"
    Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
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
        function mask(evt) {

            if (evt.keyCode == 13) {

            }
            if (evt.keyCode == 192 || evt.keyCode == 109 || evt.keyCode == 221 || evt.keyCode == 220 || evt.keyCode == 222 || evt.keyCode == 219 || evt.keyCode == 59 || evt.keyCode == 186) {


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
            <div style="padding-left: 2%">
                <div style="clear: both;">
                    <asp:Panel ID="Panel2" runat="server">
                    </asp:Panel>
                </div>
                <asp:Label ID="lblmsg" runat="server" ForeColor="Red"></asp:Label>
            </div>
            <div class="products_box">
                <fieldset>
                    <legend>
                        <asp:Label runat="server" ID="lbllegend" Text=""></asp:Label>
                    </legend>
                    <div style="float: right;">
                        <asp:Button ID="btnadddd" runat="server" Text="Add New Candidate Application Status"
                            Width="180px" CssClass="btnSubmit" OnClick="btnadddd_Click" />
                    </div>
                    <div style="clear: both;">
                    </div>
                    <asp:Panel ID="pnladdd" runat="server" Visible="false">
                        <table width="100%">
                            <tr>
                                <td style="width: 25%">
                                    <label>
                                        <asp:Label ID="lbllogo" runat="server" Text="Candidate Application Status"></asp:Label>
                                        <asp:Label ID="Label8" runat="server" CssClass="labelstar" Text="*"> </asp:Label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtdocumenttype"
                                            SetFocusOnError="true" runat="server" ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator10" runat="server"
                                            SetFocusOnError="true" ErrorMessage="Invalid" ValidationExpression="^([a-zA-Z0-9,.\s]*)"
                                            ControlToValidate="txtdocumenttype" ValidationGroup="1"></asp:RegularExpressionValidator>
                                    </label>
                                </td>
                                <td>
                                    <label>
                                        <asp:TextBox ID="txtdocumenttype" MaxLength="150" runat="server" Width="400px" onKeydown="return mask(event)"
                                            onkeyup="return check(this,/[\\/#?@&$%_^:!*'-<>+;=(){}[]|\/]/g,/^[\a-zA-Z0-9,.\s]+$/,'div1',150)"></asp:TextBox>
                                    </label>
                                    <label>
                                        <asp:Label runat="server" ID="Label18" Text="Max" CssClass="labelcount"></asp:Label>
                                        <span id="div1" cssclass="labelcount">150</span>
                                        <asp:Label ID="Label19" runat="server" Text="(A-Z 0-9 ,.)" CssClass="labelcount"></asp:Label>
                                    </label>
                                </td>
                            </tr>
                            
                             <tr>
                                <td style="width: 25%">
                                    <label>
                                        <asp:Label ID="Label2" runat="server" Text="Status Short Name"></asp:Label>
                                        <asp:Label ID="Label3" runat="server" CssClass="labelstar" Text="*"> </asp:Label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="txtshortname"
                                            SetFocusOnError="true" runat="server" ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server"
                                            SetFocusOnError="true" ErrorMessage="Invalid" ValidationExpression="^([a-zA-Z0-9,.\s]*)"
                                            ControlToValidate="txtshortname" ValidationGroup="1"></asp:RegularExpressionValidator>
                                    </label>
                                </td>
                                <td>
                                    <label>
                                        <asp:TextBox ID="txtshortname" MaxLength="150" runat="server"  onKeydown="return mask(event)"
                                            onkeyup="return check(this,/[\\/#?@&$%_^:!*'-<>+;=(){}[]|\/]/g,/^[\a-zA-Z0-9,.\s]+$/,'Span1',100)"></asp:TextBox>
                                    </label>
                                    <label>
                                        <asp:Label runat="server" ID="Label4" Text="Max" CssClass="labelcount"></asp:Label>
                                        <span id="Span1" cssclass="labelcount">150</span>
                                        <asp:Label ID="Label5" runat="server" Text="(A-Z 0-9 ,.)" CssClass="labelcount"></asp:Label>
                                    </label>
                                </td>
                            </tr>
                            
                            
                            
                            
                            <tr>
                                <td>
                                    <label>
                                        <asp:Label ID="Label1" runat="server" Text="Status"></asp:Label>
                                    </label>
                                </td>
                                <td>
                                    <label>
                                        <asp:DropDownList ID="ddlstactive" runat="server" Width="180px">
                                            <asp:ListItem Value="1" Text="Active"></asp:ListItem>
                                            <asp:ListItem Value="0" Text="Inactive"></asp:ListItem>
                                        </asp:DropDownList>
                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                </td>
                                <td>
                                    <asp:Button ID="btnsubmit" ToolTip="Submit" runat="server" ValidationGroup="1" Text="Submit"
                                        CssClass="btnSubmit" OnClick="btnsubmit_Click" />
                                    <asp:Button ID="btnupdate" ToolTip="Update" runat="server" ValidationGroup="1" Text="Update"
                                        Visible="false" CssClass="btnSubmit" OnClick="btnupdate_Click" />
                                    <asp:Button ID="btncancel" ToolTip="Cancel" runat="server" CausesValidation="false"
                                        Text="Cancel" CssClass="btnSubmit" OnClick="btncancel_Click" />
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </fieldset>
                <%-- Start Print Button Code--%>
                <fieldset>
                    <legend>
                        <asp:Label ID="Label44" runat="server" Text="List of Candidate Application Status"></asp:Label>
                    </legend>
                    <div style="float: right">
                        <asp:Button ID="Button1" runat="server" CssClass="btnSubmit" Text="Printable Version"
                            OnClick="Button1_Click1" />
                        <input id="Button2" runat="server" class="btnSubmit" onclick="document.getElementById('mydiv').className='open';javascript:CallPrint('divPrint');document.getElementById('mydiv').className='closed';"
                            style="width: 51px;" type="button" value="Print" visible="false" />
                    </div>
                    <table width="60%">
                        <tr>
                            <td>
                                <label>
                                    <asp:Label ID="lblasdas" Text="Filter by Status" runat="server"></asp:Label>
                                </label>
                                <label>
                                    <asp:DropDownList ID="ddlstapplicationstatus" Width="150px" runat="server" AutoPostBack="true"
                                        OnSelectedIndexChanged="ddlstapplicationstatus_SelectedIndexChanged">
                                        <asp:ListItem Value="All"></asp:ListItem>
                                        <asp:ListItem Value="Active"></asp:ListItem>
                                        <asp:ListItem Value="Inactive"></asp:ListItem>
                                    </asp:DropDownList>
                                </label>
                            </td>
                        </tr>
                    </table>
                    <asp:Panel ID="pnlgrid" runat="server" Width="100%">
                        <div id="mydiv" class="closed">
                            <table width="850Px">
                                <tr align="center">
                                    <td align="center" style="text-align: center; font-size: 18px; font-weight: bold;">
                                        <asp:Label ID="Label45" runat="server" Font-Italic="true" Text="List of Candidate Application Status"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <table width="100%">
                            <tr>
                                <td>
                                    <asp:GridView ID="grdsecquestuion" DataKeyNames="id" runat="server" AlternatingRowStyle-CssClass="alt"
                                        PagerStyle-CssClass="pgr" CssClass="mGrid" Width="100%" EmptyDataText="No Record Found."
                                        AutoGenerateColumns="False" OnRowCommand="grdsecquestuion_RowCommand" OnRowDeleting="grdsecquestuion_RowDeleting"
                                        OnRowEditing="grdsecquestuion_RowEditing" OnRowDataBound="grdsecquestuion_RowDataBound">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Candidate Application Status" HeaderStyle-Width="60%"
                                                HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblsecquestion" runat="server" Text='<%# Eval("PageStatusName") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                             <asp:TemplateField HeaderText="Status Short Name" HeaderStyle-Width="30%"
                                                HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblsecquestsdgion" runat="server" Text='<%# Eval("StatusShortName") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Status" HeaderStyle-Width="8%" HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblproductid1" Visible="false" runat="server" Text='<%# Eval("Active") %>'></asp:Label>
                                                    <asp:Label ID="lblproductid2" Visible="false" runat="server" Text="Active"></asp:Label>
                                                    <asp:Label ID="lblproductid3" Visible="false" runat="server" Text="Inactive"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderImageUrl="~/Account/images/edit.gif" HeaderStyle-Width="1%"
                                                HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="imgbtnedit" ImageUrl="~/Account/images/edit.gif" runat="server"
                                                        ToolTip="Edit" CommandArgument='<%#Eval("id")%>' CommandName="edit" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Delete" HeaderImageUrl="~/ShoppingCart/images/trash.jpg"
                                                HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" HeaderStyle-Width="1%">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="imgbtndelete" runat="server" CommandArgument='<%# Eval("id") %>'
                                                        ToolTip="Delete" CommandName="delete" ImageUrl="~/Account/images/delete.gif"
                                                        OnClientClick="return confirm('Do you wish to delete this record?');" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                    </td> </tr> </table> </label>
                </fieldset>
            </div>
        </ContentTemplate>
        <Triggers>
            <%--   <asp:PostBackTrigger ControlID="btnsubmit" />
            <asp:PostBackTrigger ControlID="btncancel" />
            <asp:PostBackTrigger ControlID="btnupdate" />
            <asp:PostBackTrigger ControlID="ddlstexport" />
            <asp:PostBackTrigger ControlID="Button1" />--%>
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
