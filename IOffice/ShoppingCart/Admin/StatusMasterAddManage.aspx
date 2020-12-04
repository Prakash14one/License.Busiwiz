<%@ Page Title="" Language="C#" MasterPageFile="~/Master/LicenseMaster.master"
    AutoEventWireup="true" CodeFile="StatusMasterAddManage.aspx.cs" Inherits="Add_Status_Master" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
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
    </style>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="products_box">
                <div style="padding-left: 1%">
                    <asp:Label ID="statuslable" runat="server" ForeColor="Red"></asp:Label>
                </div>
                <fieldset>
                    <legend>
                        <asp:Label ID="lbllegend" runat="server" Text="" ></asp:Label>
                    </legend>
                    <div style="float: right;">
                        <asp:Button ID="btnadd" runat="server" Text="Add New Status" CssClass="btnSubmit"
                            OnClick="btnadd_Click" />
                    </div>
                    <div style="clear: both;">
                    </div>
                    <asp:Panel ID="pnladd" runat="server" Visible="false">
                        <label>
                            <asp:Label ID="Label1" runat="server" Text="Category"></asp:Label><asp:Label ID="Label8"
                                runat="server" Text="*" CssClass="labelstar"></asp:Label><asp:RequiredFieldValidator
                                    ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddldesignation"
                                    InitialValue="0" ValidationGroup="1" ErrorMessage="*"></asp:RequiredFieldValidator>
                                    <asp:DropDownList
                                        ID="ddldesignation" runat="server">
                                    </asp:DropDownList>
                        </label>
                        <label>
                            <asp:Label ID="Label2" runat="server" Text="Status"></asp:Label><asp:Label ID="Label7"
                                runat="server" Text="*" CssClass="labelstar"></asp:Label><asp:RegularExpressionValidator
                                    ID="REG1" runat="server" ErrorMessage="Invalid Character" Display="Dynamic" SetFocusOnError="True"
                                    ValidationExpression="^([_a-zA-Z0-9\s]*)" ControlToValidate="txtdegnation" ValidationGroup="1"></asp:RegularExpressionValidator><asp:RequiredFieldValidator
                                        ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtdegnation"
                                        ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator><asp:TextBox ID="txtdegnation"
                                            runat="server" MaxLength="20" onKeydown="return mask(event)" onkeyup="return check(this,/[\\/!@#$%^'.&*()>+:;={}[]|\/]/g,/^[\a-zA-Z_0-9\s]+$/,'div1',20)"></asp:TextBox>
                            <asp:Label ID="Label60" CssClass="labelcount" runat="server" Text="Max "></asp:Label>
                            <span id="div1" class="labelcount">20</span>
                            <asp:Label ID="Label11" runat="server" CssClass="labelcount" Text="(A-Z 0-9 _)"></asp:Label>
                        </label>
                        <div style="clear: both;">
                        </div>
                        <br />
                        <asp:Button ID="ImageButton1" runat="server" Text="Submit" ValidationGroup="1" OnClick="ImageButton1_Click1"
                            CssClass="btnSubmit" />
                        <asp:Button ID="btnupdate" runat="server" Text="Update" ValidationGroup="1" Visible="false" 
                            CssClass="btnSubmit" onclick="btnupdate_Click" />
                        <asp:Button ID="Button2" runat="server" Text="Cancel" OnClick="ImageButton6_Click"
                            CssClass="btnSubmit" />
                    </asp:Panel>
                </fieldset>
                <div style="clear: both;">
                </div>
                <fieldset>
                    <legend>
                        <asp:Label ID="lbl" runat="server" Text="List of Status Names"></asp:Label>
                    </legend>
                    <div style="float: right;">
                        <asp:Button ID="Button5" CssClass="btnSubmit" runat="server" Text="Printable Version"
                            OnClick="Button5_Click" />
                        <input id="Button3" class="btnSubmit" runat="server" onclick="document.getElementById('mydiv').className='open';javascript:CallPrint('divPrint');document.getElementById('mydiv').className='closed';"
                            type="button" value="Print" visible="false" />
                    </div>
                    <label>
                        <asp:Label ID="Label9" runat="server" Text="Filter by Category"></asp:Label>
                        <asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged1">
                        </asp:DropDownList>
                        <asp:DropDownList ID="DropDownList2" runat="server" Visible="false">
                        </asp:DropDownList>
                    </label>
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
                                                    <asp:Label ID="Label5" runat="server" Font-Size="18px" Text="List of Status Names"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr align="left">
                                                <td>
                                                    <asp:Label ID="Label3" Font-Size="16px" Font-Bold="false" runat="server" Text="Category :"></asp:Label>
                                                    <asp:Label ID="lblStatusCat" Font-Size="16px" Font-Bold="false" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr align="center">
                                                <td align="center" style="text-align: center; font-size: 13px;">
                                                    <asp:Label ID="Label4" runat="server" Text="Status Name :" Visible="false"></asp:Label>
                                                    <asp:Label ID="lblStatus" runat="server" Visible="false"></asp:Label>
                                                    <input id="hdnsortExp" runat="Server" name="hdnsortExp" style="width: 1px" type="hidden" />
                                                    <input id="hdnsortDir" runat="Server" name="hdnsortDir" style="width: 1px" type="hidden" />
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:GridView ID="GridView1" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                                        GridLines="Both" AllowPaging="true" CssClass="mGrid" PagerStyle-CssClass="pgr"
                                        Width="100%" AlternatingRowStyle-CssClass="alt" DataKeyNames="StatusId" OnPageIndexChanging="GridView1_PageIndexChanging"
                                         OnRowCommand="GridView1_RowCommand"
                                        OnRowDeleting="GridView1_RowDeleting" OnRowEditing="GridView1_RowEditing" 
                                        OnSorting="GridView1_Sorting">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Category" SortExpression="StatusCategory" HeaderStyle-HorizontalAlign="Left"
                                                ItemStyle-HorizontalAlign="Left" ItemStyle-Width="45%">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgrdcatname" runat="server" Text='<%# Bind("StatusCategory") %>'></asp:Label>
                                                    <asp:Label ID="lblgrdCatid" runat="server" Text='<%# Bind("StatusCategoryMasterId") %>'
                                                        Visible="false"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Status Name" SortExpression="StatusName" HeaderStyle-HorizontalAlign="Left"
                                                ItemStyle-HorizontalAlign="Left" ItemStyle-Width="50%">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgrdSubCatname" runat="server" Text='<%# Bind("StatusName") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="StatusCategoryMasterId" HeaderText="StatusCategoryMasterId"
                                                InsertVisible="False" ReadOnly="True" SortExpression="DeptID" Visible="False">
                                            </asp:BoundField>
                                            <asp:TemplateField HeaderText="Edit" ItemStyle-Width="3%" HeaderImageUrl="~/Account/images/edit.gif"
                                                HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="ImageButton1" runat="server" CommandName="Edit" ImageUrl="~/Account/images/edit.gif"
                                                        ToolTip="Edit" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <%-- <asp:CommandField ShowEditButton="True" HeaderText="Edit" ItemStyle-Width="3%" HeaderImageUrl="~/Account/images/edit.gif" ValidationGroup="2"
                                                EditImageUrl="~/Account/images/edit.gif" ButtonType="Image" UpdateImageUrl="~/Account/images/UpdateGrid.JPG"
                                                HeaderStyle-Font-Bold="true" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"
                                                CancelImageUrl="~/images/delete.gif"></asp:CommandField>--%>
                                            <asp:TemplateField HeaderText="Delete" ItemStyle-HorizontalAlign="Left" HeaderImageUrl="~/ShoppingCart/images/trash.jpg"
                                                HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="2%">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="btndelete" runat="server" CommandName="Delete" ImageUrl="~/Account/images/delete.gif"
                                                        OnClientClick="return confirm('Do you wish to delete this record?');" ToolTip="Delete">
                                                    </asp:ImageButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </fieldset>
                <div style="clear: both;">
                </div>
                <input id="DeleteStatus" style="width: 16px" type="hidden" runat="server" value="0" />
                <asp:Panel ID="Panel3" runat="server" BackColor="#CCCCCC" BorderColor="#C0C0FF" BorderStyle="Outset"
                    Width="300px">
                    <table cellpadding="0" cellspacing="3" width="100%">
                        <tr>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="Label6" runat="server" ForeColor="Black">Are you sure you wish to 
                            delete this record ?</asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td align="center">
                                <%-- <asp:ImageButton ID="ImageButton2" runat="server" AlternateText="submit" ImageUrl="~/ShoppingCart/images/Yes.png"
                                            OnClick="ImageButton2_Click" />--%>
                                <asp:Button ID="ImageButton2" runat="server" Text="Yes" OnClick="ImageButton2_Click"
                                    Width="40px" />
                                <%--  <asp:ImageButton ID="ImageButton5" runat="server"
                                                AlternateText="cancel" ImageUrl="~/ShoppingCart/images/No.png" />--%>
                                <asp:Button ID="ImageButton5" runat="server" Text="No" Width="40px" />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <asp:Button ID="HiddenButton222" runat="server" Style="display: none" />
                <cc1:ModalPopupExtender ID="ModalPopupExtender1222" runat="server" BackgroundCssClass="modalBackground"
                    PopupControlID="Panel3" TargetControlID="HiddenButton222">
                </cc1:ModalPopupExtender>
                <div style="clear: both;">
                </div>
                <asp:Panel ID="Panel6" runat="server" BackColor="#CCCCCC" BorderColor="#C0C0FF" BorderStyle="Outset"
                    Width="300px">
                    <table cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                            <td class="subinnertblfc">
                            </td>
                        </tr>
                        <tr>
                            <td class="label" style="height: 14px">
                                <asp:Label ID="lbleditmsg" runat="server" ForeColor="Black">Sorry, Edit is not 
                            allowed!</asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="center" style="height: 26px">
                                <asp:Button ID="ImageButton10" runat="server" Text="OK" OnClick="ImageButton10_Click" />
                                <%-- <asp:ImageButton ID="ImageButton10" runat="server" AlternateText="OK" ImageUrl="~/ShoppingCart/images/cancel.png"
                                OnClick="ImageButton10_Click" />--%>
                            </td>
                        </tr>
                    </table>
                    &nbsp;</asp:Panel>
                <asp:Button ID="Button1" runat="server" Style="display: none" /><cc1:ModalPopupExtender
                    ID="ModalPopupExtender3" runat="server" BackgroundCssClass="modalBackground"
                    PopupControlID="Panel6" TargetControlID="Button1">
                </cc1:ModalPopupExtender>
                <div style="clear: both;">
                </div>
                <asp:Panel ID="Panel5" runat="server" BackColor="#CCCCCC" BorderColor="#C0C0FF" BorderStyle="Outset"
                    Width="300px">
                    <table cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                            <td class="subinnertblfc">
                            </td>
                        </tr>
                        <tr>
                            <td class="label">
                                <asp:Label ID="lbldeletemsg" runat="server" ForeColor="Black">You are not 
                            allowed to DELETE this record!</asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="center">
                                <asp:Button ID="ImageButton11" runat="server" Text="OK" OnClick="ImageButton11_Click" />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <asp:Button ID="Button4" runat="server" Style="display: none" /><cc1:ModalPopupExtender
                    ID="ModalPopupExtender2" runat="server" BackgroundCssClass="modalBackground"
                    PopupControlID="Panel5" TargetControlID="Button4">
                </cc1:ModalPopupExtender>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
