<%@ Page Language="C#" MasterPageFile="~/Master/LicenseMaster.master"
    AutoEventWireup="true" CodeFile="PartyTypeMaster.aspx.cs" Inherits="Add_PartyType_Master"
    Title="Party Type Master" %>

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
    <div class="products_box">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div style="padding-left: 1%">
                    <asp:Label ID="Label1" ForeColor="Red" runat="server" Visible="False"></asp:Label>
                </div>
                <fieldset>
                    <legend>
                        <asp:Label ID="lbladd" runat="server"></asp:Label>
                    </legend>
                    <div style="float: right">
                        <asp:Button ID="btnadd" runat="server" Text="Add User Sub-Category" CssClass="btnSubmit"
                            OnClick="btnadd_Click" />
                    </div>
                    <asp:Panel ID="pnladd" runat="server" Visible="false">
                        <label>
                            <asp:Label ID="Label3" runat="server" Text="User Category"></asp:Label><asp:DropDownList
                                ID="ddlptclist" runat="server">
                            </asp:DropDownList>
                        </label>
                        <label>
                            <asp:Label ID="Label2" runat="server" Text="User Sub-Category"></asp:Label><asp:Label
                                ID="Label10" runat="server" Text="*" CssClass="labelstar"></asp:Label><asp:RequiredFieldValidator
                                    ID="RequiredFieldValidator1" runat="server" ControlToValidate="tbPartyName" ErrorMessage="*"
                                    ValidationGroup="1"></asp:RequiredFieldValidator><asp:RegularExpressionValidator
                                        ID="REG1" runat="server" ErrorMessage="Invalid Character" Display="Dynamic" SetFocusOnError="True"
                                        ValidationExpression="^([_a-zA-Z0-9\s]*)" ControlToValidate="tbPartyName" ValidationGroup="1"></asp:RegularExpressionValidator><asp:TextBox
                                            ID="tbPartyName" runat="server" MaxLength="30" onKeydown="return mask(event)"
                                            onkeyup="return check(this,/[\\/!@#$%^'.&*()>+:;={}[]|\/]/g,/^[\a-zA-Z_0-9\s]+$/,'div1',30)"></asp:TextBox>
                       
                       <asp:Label ID="Label60"  CssClass="labelcount" runat="server" Text="Max "></asp:Label>
                       <span id="div1" class="labelcount">30</span>
                            <asp:Label ID="Label9" CssClass="labelcount" runat="server" Text="(A-Z 0-9 _)"></asp:Label></label>
                        
                        <div style="clear: both;">
                        </div>
                        <br />
                        <asp:Button ID="ImageButton1" runat="server" Text="Submit" OnClick="Button1_Click"
                            CssClass="btnSubmit" ValidationGroup="1" />
                        <asp:Button ID="btnupdate" runat="server" Text="Update" Visible="false" CssClass="btnSubmit"
                            ValidationGroup="1" OnClick="btnupdate_Click" />
                        <asp:Button ID="ImageButton2" runat="server" Text="Cancel" CssClass="btnSubmit" OnClick="Button2_Click" />
                    </asp:Panel>
                </fieldset>
                <div style="clear: both;">
                </div>
                <fieldset>
                    <legend>
                        <asp:Label ID="Label7" runat="server" Text="List of User Sub-Categories"></asp:Label>
                    </legend>
                    <div style="float: right;">
                        <asp:Button ID="Button4" runat="server" Text="Printable Version" OnClick="Button4_Click"
                            CssClass="btnSubmit" />
                        <input id="Button7" runat="server" onclick="javascript:CallPrint('divPrint')" class="btnSubmit"
                            type="button" value="Print" visible="false" />
                        <%--<asp:Button runat="server" CausesValidation="true" ValidationGroup="Submit" ID="Button1"
                            CssClass="btnSubmit" Text="Print Version" />--%>
                    </div>
                    <div style="clear: both;">
                    </div>
                    <label>
                        <asp:Label ID="Label4" runat="server" Text="Filter by User Category"></asp:Label>
                        <asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged">
                        </asp:DropDownList>
                        <%--<select>
                        <option>All</option>
                        <option>EPlaza Store</option>
                        <option>EPlaza Store</option>
                    </select>--%>
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
                                                    <asp:Label ID="Label5" Font-Size="18px" runat="server" Text="List of User Sub-Categories"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr align="left">
                                                <td>
                                                    <asp:Label ID="Label8" Font-Size="16px" runat="server" Font-Bold="false" ForeColor="Black"
                                                        Text="User Category :"></asp:Label>
                                                    <asp:Label ID="lblcategory" Font-Size="16px" runat="server" Font-Bold="false" ForeColor="Black"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:GridView ID="GridView1" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                                        CssClass="mGrid" PagerStyle-CssClass="pgr" Width="100%" GridLines="Both" AlternatingRowStyle-CssClass="alt"
                                        DataKeyNames="PartyTypeId" 
                                        OnRowDeleting="GridView1_RowDeleting"
                                         OnSorting="GridView1_Sorting"
                                        EmptyDataText="No Record Found." AllowPaging="True" 
                                        onrowcommand="GridView1_RowCommand" onrowediting="GridView1_RowEditing">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Party Type Id" SortExpression="PartyTypeId" ItemStyle-Width="15%"
                                                Visible="false" HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblpartytypeid" runat="server" Text='<%#Bind("PartyTypeId") %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle Width="15%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="User Category" SortExpression="Name" ItemStyle-Width="32%"
                                                ItemStyle-VerticalAlign="Top" HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblpmasterc" runat="server" Text='<%# Bind("Name") %>'></asp:Label>
                                                </ItemTemplate>
                                               
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle Width="32%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="User Sub-Category" SortExpression="PartType" ItemStyle-Width="32%"
                                                ItemStyle-VerticalAlign="Top" HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblPartType" runat="server" Text='<%# Bind("PartType") %>'></asp:Label>
                                                </ItemTemplate>
                                                
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle Width="32%" />
                                            </asp:TemplateField>
                                            <%--<asp:CommandField ShowEditButton="True" HeaderText="Edit" ItemStyle-Width="3%" HeaderStyle-HorizontalAlign="Left"
                                                ButtonType="Image" HeaderImageUrl="~/Account/images/edit.gif" EditImageUrl="~/Account/images/edit.gif"
                                                UpdateImageUrl="~/Account/images/UpdateGrid.JPG" CancelImageUrl="~/images/delete.gif">
                                                <HeaderStyle HorizontalAlign="Left" />
                                            </asp:CommandField>--%>
                                            <asp:TemplateField ItemStyle-Width="3%" HeaderStyle-HorizontalAlign="Left" HeaderImageUrl="~/Account/images/edit.gif">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="llinedit" runat="server" ToolTip="Edit" CommandArgument='<%# Eval("PartyTypeId") %>'
                                                        CommandName="Edit" ImageUrl="~/Account/images/edit.gif"></asp:ImageButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Delete" HeaderImageUrl="~/ShoppingCart/images/trash.jpg"
                                                ItemStyle-Width="3%" HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="btndelete" runat="server" CommandName="Delete" ImageUrl="~/Account/images/delete.gif"
                                                        OnClientClick="return confirm('Do you wish to delete this record?');" ToolTip="Delete">
                                                    </asp:ImageButton>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                        </Columns>
                                        <PagerStyle CssClass="pgr" />
                                        <AlternatingRowStyle CssClass="alt" />
                                    </asp:GridView>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Panel ID="Panel6" runat="server" BackColor="#CCCCCC" BorderColor="Black" BorderStyle="Solid"
                                        Width="300px">
                                        <table cellpadding="0" cellspacing="0" width="100%" bgcolor="#CCCCCC" style="width: 292px">
                                            <tr>
                                                <td>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lbleditmsg" runat="server" ForeColor="Black">Sorry, Edit is not allow!</asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="center" style="height: 26px" bgcolor="#CCCCCC">
                                                    <asp:Button ID="ImageButton10" runat="server" CssClass="btnSubmit" Text="Ok" OnClick="ImageButton10_Click" />
                                                    <%--<asp:ImageButton ID="ImageButton10" runat="server" AlternateText="OK" ImageUrl="~/ShoppingCart/images/cancel.png"
                                                OnClick="ImageButton10_Click" BackColor="#CCCCCC" />--%>
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                    <asp:Button ID="Button3" runat="server" Style="display: none" />
                                    <cc1:ModalPopupExtender ID="ModalPopupExtender3" runat="server" BackgroundCssClass="modalBackground"
                                        PopupControlID="Panel6" TargetControlID="Button3">
                                    </cc1:ModalPopupExtender>
                                    <input id="hdnsortExp" runat="Server" name="hdnsortExp" style="width: 1px" type="hidden" />
                                    <input id="hdnsortDir" runat="Server" name="hdnsortDir" style="width: 1px" type="hidden" />
                                    <asp:Panel ID="Panel5" runat="server" BackColor="#CCCCCC" BorderColor="Black" BorderStyle="Solid"
                                        Width="300px">
                                        <table cellpadding="0" cellspacing="0" width="100%" bgcolor="#CCCCCC">
                                            <tr>
                                                <td>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lbldeletemsg" runat="server" ForeColor="Black">You are not 
                            allow to DELETE any record!</asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <br />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="center" style="height: 26px">
                                                    <asp:Button ID="ImageButton8" runat="server" Text="Cancel" CssClass="btnSubmit" OnClick="ImageButton8_Click" />
                                                    <%--<asp:ImageButton ID="ImageButton8" runat="server" AlternateText="OK" ImageUrl="~/ShoppingCart/images/cancel.png"
                                                OnClick="ImageButton8_Click" />--%>
                                                </td>
                                            </tr>
                                        </table>
                                        &nbsp;</asp:Panel>
                                    <asp:Button ID="Button2" runat="server" Style="display: none" />
                                    <cc1:ModalPopupExtender ID="ModalPopupExtender2" runat="server" BackgroundCssClass="modalBackground"
                                        PopupControlID="Panel5" TargetControlID="Button2">
                                    </cc1:ModalPopupExtender>
                                    <asp:Panel ID="Panel3" runat="server" BackColor="#CCCCCC" BorderColor="Black" BorderStyle="Solid"
                                        Width="300px">
                                        <table id="Table1" cellpadding="0" cellspacing="0" width="100%">
                                            <tr>
                                                <td>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="Label6" runat="server" ForeColor="Black">Do you wish to delete this record?</asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <br />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="center">
                                                    <asp:Button ID="ImageButton5" runat="server" Text="Yes" CssClass="btnSubmit" OnClick="yes_Click" />
                                                    <%--<asp:ImageButton ID="ImageButton5" runat="server" AlternateText="submit" ImageUrl="~/ShoppingCart/images/Yes.png"
                                            OnClick="yes_Click" />--%>
                                                    <asp:Button ID="ImageButton6" runat="server" Text="No" CssClass="btnSubmit" OnClick="ImageButton6_Click" />
                                                    <%--<asp:ImageButton ID="ImageButton6" runat="server" AlternateText="cancel" ImageUrl="~/ShoppingCart/images/No.png"
                                            OnClick="ImageButton6_Click" />--%>
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                    <asp:Button ID="HiddenButton222" runat="server" Style="display: none" />
                                    <cc1:ModalPopupExtender ID="ModalPopupExtender1222" runat="server" BackgroundCssClass="modalBackground"
                                        PopupControlID="Panel3" TargetControlID="HiddenButton222">
                                    </cc1:ModalPopupExtender>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                    <div style="clear: both;">
                    </div>
                    <asp:Panel ID="Panel4" runat="server" BackColor="#CCCCCC" BorderColor="Black" BorderStyle="Outset"
                        Width="300px">
                        <table cellpadding="0" cellspacing="0" width="100%">
                            <tr>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lbladdmsg" runat="server" ForeColor="Black">You are not allow 
                                                    to ADD any record!</asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td align="center" style="height: 26px">
                                    <asp:Button ID="ImageButton7" runat="server" CssClass="btnSubmit" Text="Cancel" OnClick="ImageButton7_Click" />
                                    <%--<asp:ImageButton ID="ImageButton7" runat="server" AlternateText="OK" ImageUrl="~/ShoppingCart/images/cancel.png"
                                                        OnClick="ImageButton7_Click" />--%>
                                </td>
                            </tr>
                        </table>
                        &nbsp;</asp:Panel>
                    <asp:Button ID="Button1" runat="server" Style="display: none" />
                    <cc1:ModalPopupExtender ID="ModalPopupExtender1" runat="server" BackgroundCssClass="modalBackground"
                        PopupControlID="Panel4" TargetControlID="Button1">
                    </cc1:ModalPopupExtender>
                    <%--<asp:GridView ID="gvCustomres" runat="server" DataSourceID="customresDataSource"
                    AutoGenerateColumns="False" GridLines="None" AllowPaging="true" CssClass="mGrid"
                    PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt">
                    <Columns>
                        <asp:BoundField DataField="ContactName"  HeaderText="PartyID" />
                        <asp:BoundField DataField="PricePlan" HeaderText="Party Type" />
                        <asp:BoundField DataField="PricePlan" HeaderText="Party Master Category" />
                        <asp:HyperLinkField Text="Edit" HeaderText="Edit" NavigateUrl="#" />
                        <asp:HyperLinkField Text="Delete" HeaderText="Delete" NavigateUrl="#" />
                    </Columns>
                </asp:GridView>--%>
                    <%--<asp:XmlDataSource ID="customresDataSource" runat="server" DataFile="~/App_Data/data.xml">
                </asp:XmlDataSource>--%>
                </fieldset>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <!--end of right content-->
    <div style="clear: both;">
    </div>
</asp:Content>
