<%@ Page Title="" Language="C#" MasterPageFile="~/Master/LicenseMaster.master"
    AutoEventWireup="true" CodeFile="StatusCategoryAddManage.aspx.cs" Inherits="Add_Inventory_Master" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script language="javascript" type="text/javascript">
        function CallPrint(strid) {
            var prtContent = document.getElementById('<%= pnlgrid.ClientID %>');
            var WinPrint = window.open('', '', 'left=0,top=0,width=1024,height=768,toolbar=1,scrollbars=1,status=0');
            WinPrint.document.write(prtContent.innerHTML, '<link rel="stylesheet" type="text/css" href="css/forms.css" media="screen" />');
            WinPrint.document.close();
            WinPrint.focus();
            WinPrint.print();
            WinPrint.close();

        }
        function mask(evt) {
            counter = document.getElementById(id);
            alert(counter);
            if (evt.keyCode == 13) {

                return false;
            }


            if (evt.keyCode == 188 || evt.keyCode == 191 || evt.keyCode == 192 || evt.keyCode == 109 || evt.keyCode == 221 || evt.keyCode == 220 || evt.keyCode == 222 || evt.keyCode == 219 || evt.keyCode == 59 || evt.keyCode == 186) {


                alert("You have entered an invalid character");
                return false;
            }

        }
        function check(txt1, regex, reg, id, max_len) {
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

        function mak(id, max_len, myele) {
            counter = document.getElementById(id);

            if (myele.value.length <= max_len) {
                remaining_characters = max_len - myele.value.length;
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
        #innertbl
        {
        }
        #subinnertbl
        {
        }
    </style>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="products_box">
                <div style="padding-left: 1%">
                    <asp:Label ID="statuslable" runat="server" Visible="False" ForeColor="Red"></asp:Label>
                </div>
                <fieldset>
                    
                    <legend>
                        <asp:Label ID="lbllegend" runat="server" 
                            ></asp:Label>
                    </legend>
                    <div style="float: right;">
                        <asp:Button ID="btnadd" runat="server" Text="Add New Category" CssClass="btnSubmit"
                            OnClick="btnadd_Click" />
                    </div>
                    <div style="clear: both;">
                    </div>
                    <asp:Panel ID="pnladd" runat="server" Visible="false">
                        <label>
                            <asp:Label ID="lblStatusCategory" runat="server" Text="Category"></asp:Label>
                            <asp:Label ID="Label6" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                            
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtdegnation"
                                ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="REG1" runat="server" ErrorMessage="Invalid Character" Display="Dynamic"
                                SetFocusOnError="True" ValidationExpression="^([_a-zA-Z0-9\s]*)" ControlToValidate="txtdegnation"
                                ValidationGroup="1"></asp:RegularExpressionValidator>
                            <asp:TextBox ID="txtdegnation" runat="server" MaxLength="20" onKeydown="return mask(event)"
                                onkeyup="return check(this,/[\\/!@#$%^'.&*()>+:;={}[]|\/]/g,/^[\a-zA-Z_0-9\s]+$/,'div1',20)"></asp:TextBox>
                           <asp:Label ID="Label60" CssClass="labelcount" runat="server" Text="Max "></asp:Label>
                           <span id="div1" class="labelcount">20</span>
                            <asp:Label ID="Label12" CssClass="labelcount" runat="server" Text="(A-Z 0-9 _)"></asp:Label>
                        </label>
                        <br />
                        <asp:CheckBox ID="CheckBox1" Checked="true" Text="Add a status for this category (Recommended)"
                            runat="server" />
                        <div style="clear: both;">
                        </div>
                        <asp:Button ID="ImageButton1" runat="server" Text="Submit" OnClick="ImageButton1_Click"
                            ValidationGroup="1" CssClass="btnSubmit" />
                            <asp:Button ID="btnupdate" runat="server" Text="Update" Visible="false" 
                            ValidationGroup="1" CssClass="btnSubmit" onclick="btnupdate_Click" />
                        <asp:Button ID="ImageButton6" runat="server" Text="Cancel" OnClick="Button2_Click"
                            CssClass="btnSubmit" />
                    </asp:Panel>
                </fieldset>
            </div>
            <div style="clear: both;">
            </div>
            <fieldset>
                <legend>
                    <asp:Label ID="lblStatusCategorylist" runat="server" Text="List of Categories" ></asp:Label></legend>
                <div style="float: right;">
                    <asp:Button ID="Button4" runat="server" Text="Printable Version" OnClick="Button4_Click"
                        CssClass="btnSubmit" />
                    <input id="Button1" runat="server" onclick="document.getElementById('mydiv').className='open';javascript:CallPrint('divPrint');document.getElementById('mydiv').className='closed';"
                        type="button" value="Print" visible="false" class="btnSubmit" />
                </div>
                <div style="clear: both;">
                </div>
                <asp:Panel ID="pnlgrid" runat="server" Width="100%">
                    <table width="100%">
                        <tr align="center">
                            <td>
                                <div id="mydiv" class="closed">
                                    <table width="100%" style="color: Black; font-weight: bold; font-style: italic; text-align: center">
                                        <tr align="center">
                                            <td>
                                                <asp:Label ID="lblCompany" Font-Size="20px" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr align="center">
                                            <td>
                                                <asp:Label ID="Label4" runat="server" Font-Size="18px" Text="List of Categories"
                                                    Visible="True"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:GridView ID="GridView1" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                                    PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt" GridLines="Both"
                                    CssClass="mGrid" DataKeyNames="StatusCategoryMasterId" OnPageIndexChanging="GridView1_PageIndexChanging"
                                     OnRowCommand="GridView1_RowCommand"
                                    OnRowDeleting="GridView1_RowDeleting" OnRowEditing="GridView1_RowEditing" 
                                    OnSorting="GridView1_Sorting" Width="100%" EmptyDataText="No Record Found." AllowPaging="True">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Categories" SortExpression="StatusCategory" HeaderStyle-HorizontalAlign="Left"
                                            ItemStyle-HorizontalAlign="Left" ItemStyle-Width="92%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblCatName" runat="server" Text='<%#Eval("StatusCategory") %>'></asp:Label>
                                                <asp:Label ID="lblCatId" runat="server" Text='<%#Eval("StatusCategoryMasterId") %>'
                                                    Visible="false"></asp:Label>
                                            </ItemTemplate>
                                           <%-- <EditItemTemplate>
                                                <asp:TextBox ID="txtdesignname" MaxLength="20" runat="server" Text='<%#Eval("StatusCategory") %>'
                                                    Width="400px">
                                                    
                                                    
                                                </asp:TextBox>
                                                <asp:RegularExpressionValidator ID="REG2" runat="server" ErrorMessage="Invalid Character" Display="Dynamic"
                                                    SetFocusOnError="True" ValidationExpression="^([_a-zA-Z0-9\s]*)" ControlToValidate="txtdesignname"
                                                    ValidationGroup="2"></asp:RegularExpressionValidator>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtdesignname"
                                                    ErrorMessage="*" ValidationGroup="2"></asp:RequiredFieldValidator>
                                                <asp:Label ID="lblCatId" runat="server" Text='<%#Eval("StatusCategoryMasterId") %>'
                                                    Visible="false"></asp:Label>
                                            </EditItemTemplate>--%>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Edit" ItemStyle-Width="3%" HeaderImageUrl="~/Account/images/edit.gif"
                                                HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="ImageButton1" runat="server" 
                                                        CommandName="Edit" ImageUrl="~/Account/images/edit.gif" ToolTip="Edit" />
                                                </ItemTemplate>
                                        </asp:TemplateField>
                                        <%--<asp:CommandField ShowEditButton="True" HeaderText="Edit" ItemStyle-HorizontalAlign="Left" ValidationGroup="2"
                                            HeaderStyle-HorizontalAlign="Left" EditImageUrl="~/Account/images/edit.gif" HeaderImageUrl="~/Account/images/edit.gif"
                                            ButtonType="Image" UpdateImageUrl="~/Account/images/UpdateGrid.JPG" CancelImageUrl="~/images/delete.gif">
                                            <ItemStyle Width="3%" />
                                        </asp:CommandField>--%>
                                        <asp:TemplateField HeaderImageUrl="~/Account/images/delete.gif" ItemStyle-HorizontalAlign="Left"
                                            HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="2%">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="ImageButton49" runat="server" OnClientClick="return confirm ('Do you wish to delete this record ?');"
                                                    CommandArgument='<%# Eval("StatusCategoryMasterId") %>' CommandName="Delete" ToolTip="Delete"
                                                    ImageUrl="~/Account/images/delete.gif" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                                <input id="hdnsortExp" runat="Server" name="hdnsortExp" style="width: 1px" type="hidden" />
                                <input id="hdnsortDir" runat="Server" name="hdnsortDir" style="width: 1px" type="hidden" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;
                                <input id="DeleteStatus" style="width: 16px" type="hidden" runat="server" value="0" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <%-- &nbsp;&nbsp;--%>
                                <%--<asp:Panel ID="Panel3" runat="server" BackColor="#CCCCCC" BorderColor="Black" BorderStyle="Solid"
                                        Width="350px" >
                                        <table cellpadding="3" cellspacing="2" style=" width: 100%">
                                            <tr>
                                                <td>
                                                   
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="Label1" runat="server"  Text="Are you sure you wish to delete this record?"></asp:Label>
                                                </td>
                                             <tr><td></td></tr>
                                            </tr>
                                            
                                            <tr>
                                                <td align="center">
                                                    <asp:Button ID="ImageButton2" runat="server" Text="Yes" OnClick="ImageButton2_Click" CssClass="btnSubmit" />
                                                   
                                                    <asp:Button ID="ImageButton5" runat="server" Text="No" OnClick="ImageButton5_Click" CssClass="btnSubmit" />
                                                    
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                    <asp:Button ID="HiddenButton222" runat="server" Style="display: none" />
                                    <cc1:ModalPopupExtender ID="ModalPopupExtender1222" runat="server" BackgroundCssClass="modalBackground"
                                        PopupControlID="Panel3" TargetControlID="HiddenButton222">
                                    </cc1:ModalPopupExtender>--%>
                               
                               
                                
                              
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
