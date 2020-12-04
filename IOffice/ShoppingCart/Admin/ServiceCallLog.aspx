<%@ Page Language="C#" MasterPageFile="~/Master/LicenseMaster.master"
    AutoEventWireup="true" CodeFile="ServiceCallLog.aspx.cs" Inherits="ShoppingCart_Admin_Default3"
    Title="Untitled Page" %>

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
            WinPrint.document.write(prtContent.innerHTML, '<link rel="stylesheet" type="text/css" href="css/forms.css" media="screen" />');
            WinPrint.document.close();
            WinPrint.focus();
            WinPrint.print();
            WinPrint.close();

        }
        function Button4_Click() { }
        function Button7_Click() { }


        function mask(evt) {

            if (evt.keyCode == 13) {

                return true;
            }


            if (evt.keyCode == 191 || evt.keyCode == 192 || evt.keyCode == 109 || evt.keyCode == 221 || evt.keyCode == 220 || evt.keyCode == 222 || evt.keyCode == 219 || evt.keyCode == 59 || evt.keyCode == 186) {


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


        function mak(id, max_len, myele) {
            counter = document.getElementById(id);

            if (myele.value.length <= max_len) {
                remaining_characters = max_len - myele.value.length;
                counter.innerHTML = remaining_characters;
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

    <asp:UpdatePanel ID="updatepanel1" runat="server">
        <ContentTemplate>
            <div class="products_box">
                <asp:Panel ID="Panel2" runat="server" Width="100%">
                    <div style="padding-left: 1%">
                        <asp:Label ID="lblmsg" runat="server" ForeColor="Red"></asp:Label>
                    </div>
                    <fieldset>
                        <div style="float: right;">
                            <asp:Button ID="Button3" runat="server" Text="Add Service Call" CssClass="btnSubmit"
                                OnClick="Button3_Click" />
                        </div>
                        <div style="clear: both;">
                        </div>
                        <asp:Panel runat="server" Width="100%">
                            <label>
                                <asp:Label ID="Label6" runat="server" Text="Business Name"></asp:Label>
                                <asp:DropDownList ID="ddlWarehouse" runat="server" OnSelectedIndexChanged="ddlWarehouse_SelectedIndexChanged"
                                    AutoPostBack="True">
                                </asp:DropDownList>
                            </label>
                            <label>
                                <asp:Label ID="Label25" runat="server" Text="Party Name"></asp:Label>
                                <asp:DropDownList ID="ddlpartynamefilter" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlpartynamefilter_SelectedIndexChanged">
                                </asp:DropDownList>
                            </label>
                            <label>
                                <asp:Label ID="Label3" runat="server" Text="Service Complaint Status"></asp:Label>
                                <asp:DropDownList ID="ddlMainStatus" runat="server" Width="100px" AutoPostBack="True"
                                    OnSelectedIndexChanged="ddlMainStatus_SelectedIndexChanged">
                                </asp:DropDownList>
                            </label>
                            <label>
                                <asp:Label ID="Label1" runat="server" Text="From Date"></asp:Label>
                                <asp:TextBox ID="txtFromDate" runat="server" Width="80px" AutoPostBack="True" OnTextChanged="txtFromDate_TextChanged"></asp:TextBox>
                                <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtFromDate"
                                    PopupButtonID="ImageButton1">
                                </cc1:CalendarExtender>
                            </label>
                            <label>
                                <br />
                                <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/ShoppingCart/images/cal_btn.jpg">
                                </asp:ImageButton>
                            </label>
                            <label>
                                <asp:Label ID="Label2" runat="server" Text="To Date"></asp:Label>
                                <asp:TextBox ID="txtTodate" runat="server" Width="80px" AutoPostBack="True" OnTextChanged="txtTodate_TextChanged"></asp:TextBox>
                                <cc1:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtTodate"
                                    PopupButtonID="ImageButton2">
                                </cc1:CalendarExtender>
                            </label>
                            <label>
                                <br />
                                <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/ShoppingCart/images/cal_btn.jpg">
                                </asp:ImageButton>
                            </label>
                            <label>
                                <asp:Label ID="Label4" runat="server" Text="Problem ID:Problem Title"></asp:Label>
                                <asp:DropDownList ID="ddlpartytype" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlpartytype_SelectedIndexChanged">
                                </asp:DropDownList>
                            </label>
                        </asp:Panel>
                        <div style="clear: both;">
                        </div>
                    </fieldset>
                    <asp:Panel ID="Panel1" runat="server" Width="100%" Visible="false">
                        <fieldset>
                            <label>
                                <asp:Label ID="Label5" runat="server" Text="Service Complaint ID"></asp:Label>
                                <br />
                                <asp:Label ID="lblID" runat="server" Text=""></asp:Label>
                            </label>
                            <label>
                                <asp:Label ID="Label7" runat="server" Text="Service Complaint Date"></asp:Label>
                                <br />
                                <asp:Label ID="lblDate" runat="server" Text=""></asp:Label>
                            </label>
                            <label>
                                <asp:Label ID="Label8" runat="server" Text="Party Name"></asp:Label>
                                <br />
                                <asp:Label ID="lblpname" runat="server" Text=""></asp:Label>
                            </label>
                            <label>
                                <asp:Label ID="Label9" runat="server" Text="Problem Title"></asp:Label>
                                <br />
                                <asp:Label ID="lblprobtitle" runat="server" Text=""></asp:Label>
                            </label>
                            <label>
                                <asp:Label ID="Label11" runat="server" Text="Status"></asp:Label>
                                <br />
                                <asp:Label ID="lblstatus" runat="server" Text=""></asp:Label>
                            </label>
                            <div style="clear: both;">
                            </div>
                            <label>
                                <asp:Label ID="Label10" runat="server" Text="Problem Description"></asp:Label>
                                <br />
                                <asp:Label ID="lblprobdesc" runat="server" Text=""></asp:Label>
                            </label>
                        </fieldset>
                    </asp:Panel>
                    <div style="clear: both;">
                    </div>
                    <fieldset>
                        <legend>
                            <asp:Label ID="Label12" runat="server" Text="List of Services Provided"></asp:Label>
                        </legend>
                        <div style="clear: both;">
                        </div>
                        <div style="float: right;">
                            <asp:Button ID="Button4" runat="server" Text="Printable Version" CssClass="btnSubmit"
                                OnClick="Button4_Click" />
                            <input id="Button7" runat="server" onclick="document.getElementById('mydiv').className='open';javascript:CallPrint('divPrint');document.getElementById('mydiv').className='closed';"
                                type="button" class="btnSubmit" value="Print" visible="false" />
                        </div>
                        <div style="clear: both;">
                        </div>
                        <asp:Panel ID="pnlgrid" runat="server" Width="100%" ScrollBars="None">
                            <table width="100%">
                                <tr>
                                    <td>
                                        <div id="mydiv" class="closed">
                                            <table width="100%" style="color: Black; font-weight: bold; font-style: italic; text-align: center">
                                                <tr>
                                                    <td align="center">
                                                        <asp:Label ID="lblCompany" runat="server" Font-Size="20px" Visible="false"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr align="center">
                                                    <td align="center" style="text-align: center; font-size: 20px; font-weight: bold;">
                                                        <asp:Label ID="Label28" runat="server" Text="Business : " Font-Italic="true"></asp:Label>
                                                        <asp:Label ID="lblBusiness" runat="server" Font-Italic="True"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="center">
                                                        <asp:Label ID="Label18" runat="server" Text="List of Services Provided" Font-Size="18px"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="left">
                                                        <asp:Label ID="Label19" runat="server" Text="Status :" Font-Size="16px" Font-Bold="false"></asp:Label>
                                                        <asp:Label ID="Label20" runat="server" Font-Size="16px" Font-Bold="false"></asp:Label>
                                                        &nbsp;
                                                        <asp:Label ID="Label21" runat="server" Text="From Date :" Font-Size="16px" Font-Bold="false"></asp:Label>
                                                        <asp:Label ID="lblprintfromdate" runat="server" Font-Size="16px" Font-Bold="false"></asp:Label>
                                                        &nbsp;
                                                        <asp:Label ID="Label23" runat="server" Text="To Date :" Font-Size="16px" Font-Bold="false"></asp:Label>
                                                        <asp:Label ID="lblprinttodate" runat="server" Font-Size="16px" Font-Bold="false"></asp:Label>
                                                        &nbsp;
                                                        <asp:Label ID="Label24" runat="server" Text="ID:Problem Title:Party Name :" Font-Size="16px"
                                                            Font-Bold="false"></asp:Label>
                                                        <asp:Label ID="lblprblemtitalidprint" runat="server" Font-Size="16px" Font-Bold="false"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:GridView ID="GridView1" runat="server" Width="100%" PagerStyle-CssClass="pgr"
                                            AllowSorting="true" AlternatingRowStyle-CssClass="alt" CssClass="mGrid" DataKeyNames="Id"
                                            AutoGenerateColumns="False" EmptyDataText="No Record Found." OnRowCommand="GridView1_RowCommand"
                                            OnRowDeleting="GridView1_RowDeleting" OnSorting="GridView1_Sorting">
                                            <Columns>
                                                <asp:TemplateField HeaderText="ID" SortExpression="ServiceCallId" HeaderStyle-HorizontalAlign="Left"
                                                    ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblservicecalllogid" runat="server" Text='<%#Bind("ServiceCallId") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Service Provided Date" SortExpression="ServiceProvidedDate"
                                                    ItemStyle-Width="20%" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblserviceprovideddate" runat="server" Text='<%#Bind("ServiceProvidedDate") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Employee Name/User ID" SortExpression="Uname" ItemStyle-Width="20%"
                                                    HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblemployeename" runat="server" Text='<%#Bind("Uname") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Service Done Notes" SortExpression="ServiceDoneNote"
                                                    ItemStyle-Width="45%" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblservicedonenote" runat="server" Text='<%#Bind("ServiceDoneNote") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                                <%-- <asp:ButtonField HeaderImageUrl="~/Account/images/edit.gif" ImageUrl="~/Account/images/edit.gif"
                                                    CommandName="view" ItemStyle-Width="3%" ButtonType="Image" ItemStyle-ForeColor="Black"
                                                    HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                    <ItemStyle ForeColor="Black" HorizontalAlign="Left" Width="3%" />
                                                </asp:ButtonField>--%>
                                                <asp:TemplateField ItemStyle-Width="3%" HeaderStyle-HorizontalAlign="Left" HeaderImageUrl="~/Account/images/edit.gif">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="llinedit" runat="server" ToolTip="Edit" CommandArgument='<%# Eval("Id") %>'
                                                            CommandName="view" ImageUrl="~/Account/images/edit.gif"></asp:ImageButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Delete" HeaderImageUrl="~/ShoppingCart/images/trash.jpg"
                                                    ItemStyle-Width="2%" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="ImageButton48" runat="server" CommandName="Delete" Height="16px"
                                                            ImageUrl="~/Account/images/delete.gif" OnClientClick="return confirm('Do you wish to delete this record?')"
                                                            CommandArgument='<%# Eval("Id") %>' ToolTip="Delete" />
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
                    </fieldset>
                </asp:Panel>
                <asp:Panel ID="Panel3" runat="server" Width="100%" Visible="false">
                    <fieldset>
                        <table width="100%">
                            <tr>
                                <td style="width: 30%">
                                    <label>
                                        <asp:Label ID="Label13" runat="server" Text="Service Provided Date"></asp:Label>
                                    </label>
                                </td>
                                <td style="width: 70%">
                                    <label>
                                        <asp:TextBox ID="txtservicedate" runat="server" Width="80px"></asp:TextBox>
                                        <cc1:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="txtservicedate"
                                            PopupButtonID="ImageButton3">
                                        </cc1:CalendarExtender>
                                    </label>
                                    <label>
                                        <asp:ImageButton ID="ImageButton3" runat="server" ImageUrl="~/ShoppingCart/images/cal_btn.jpg">
                                        </asp:ImageButton>
                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 30%">
                                    <label>
                                        <asp:Label ID="Label15" runat="server" Text="Business Name"></asp:Label>
                                    </label>
                                </td>
                                <td style="width: 70%">
                                    <asp:DropDownList ID="ddlfilterwarehouse" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlfilterwarehouse_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 30%">
                                    <label>
                                        <asp:Label ID="Label14" runat="server" Text="Employee Name"></asp:Label>
                                    </label>
                                </td>
                                <td style="width: 70%">
                                    <asp:DropDownList ID="ddlUserMaster" runat="server">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 30%">
                                    <label>
                                        <asp:Label ID="Label16" runat="server" Text="Service Note"></asp:Label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="TextBox1"
                                            ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="REG1" runat="server" ErrorMessage="Invalid Character"
                                            SetFocusOnError="True" ValidationExpression="^([_.,a-zA-Z0-9\s]*)" ControlToValidate="TextBox1"
                                            ValidationGroup="1"></asp:RegularExpressionValidator>
                                    </label>
                                </td>
                                <td style="width: 70%">
                                    <label>
                                        <asp:TextBox ID="TextBox1" runat="server" Width="400px" onkeypress="return checktextboxmaxlength(this,1000,event)"
                                            onkeyup="return check(this,/[\\/!@#$%^'&*()>+:;={}[]|\/]/g,/^[\a-zA-Z0-9_.,\s]+$/,'Span1',1000)"
                                            TextMode="MultiLine" Height="70px"></asp:TextBox>
                                        <asp:Label ID="Label27" runat="server" Text="Max" CssClass="labelcount"></asp:Label>
                                        <span id="Span1" class="labelcount">1000</span>
                                        <asp:Label ID="Label22" runat="server" CssClass="labelcount" Text="(A-Z 0-9 _ . ,)"></asp:Label>
                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 30%">
                                    <label>
                                        <asp:Label ID="Label17" runat="server" Text="Has this problem been solved?"></asp:Label>
                                    </label>
                                </td>
                                <td style="width: 70%">
                                    <asp:CheckBox ID="CheckBox1" Text="Yes" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 30%">
                                    <label>
                                    </label>
                                </td>
                                <td style="width: 70%">
                                    <input id="hdnsortExp" runat="Server" name="hdnsortExp" style="width: 1px" type="hidden" />
                                    <input id="hdnsortDir" runat="Server" name="hdnsortDir" style="width: 1px" type="hidden" />
                                    <asp:Button ID="Button1" runat="server" ValidationGroup="1" Text="Update" CssClass="btnSubmit"
                                        OnClick="Button1_Click" />
                                    <asp:Button ID="Button2" runat="server" Text="Cancel" CssClass="btnSubmit" OnClick="Button2_Click" />
                                </td>
                            </tr>
                        </table>
                    </fieldset>
                </asp:Panel>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
