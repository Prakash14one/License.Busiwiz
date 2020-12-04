<%@ Page Language="C#" MasterPageFile="~/Master/LicenseMaster.master"
    AutoEventWireup="true" CodeFile="JobMaster.aspx.cs" Inherits="ShoppingCart_Admin_JobMaster"
    Title="Untitled Page" %>

<%@ Register Assembly="PagingGridView" Namespace="Fadrian.Web.Control" TagPrefix="cc11" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

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
        function checktextboxmaxlength(txt, maxLen, evt) {
            try {
                if (txt.value.length > (maxLen - 1) && evt.keyCode != 8)
                    return false;
            }
            catch (e) {

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
            //        counter = document.getElementById(id);

            if (myele.value.length <= max_len) {
                remaining_characters = max_len - myele.value.length;
                counter.innerHTML = remaining_characters;
            }
        }
        function mask(evt, max_len) {



            if (evt.keyCode == 13) {

                return true;
            }


            if (evt.keyCode == 188 || evt.keyCode == 191 || evt.keyCode == 192 || evt.keyCode == 109 || evt.keyCode == 221 || evt.keyCode == 220 || evt.keyCode == 222 || evt.keyCode == 219 || evt.keyCode == 59 || evt.keyCode == 186) {


                alert("You have entered an invalid character");
                return false;
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
                <div style="padding-left: 1%;">
                    <asp:Label ID="Label1" runat="server" ForeColor="Red" Visible="False"></asp:Label>
                </div>
                <div style="clear: both;">
                </div>
                <fieldset>
                    <legend>
                        <asp:Label ID="lbladd" Text="" runat="server">
                         
                        </asp:Label>
                    </legend>
                    <div style="float: right;">
                        <asp:Button ID="btnaddroom" runat="server" Text="Add Work Order" CssClass="btnSubmit"
                            OnClick="btnaddroom_Click" />
                    </div>
                    <div style="clear: both;">
                    </div>
                    <asp:Panel ID="addinventoryroom" Visible="false" runat="server">
                        <label>
                            <asp:Label ID="Label8" runat="server" Text=" Business Name">
                            </asp:Label>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="ddlStoreName"
                                ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                            <input id="hdnsortExp" runat="Server" name="hdnsortExp" style="width: 1px" type="hidden" />
                            <input id="hdnsortDir" runat="Server" name="hdnsortDir" style="width: 1px" type="hidden" />
                            <asp:DropDownList ID="ddlStoreName" runat="server" AutoPostBack="True" Width="150px"
                                OnSelectedIndexChanged="ddlStoreName_SelectedIndexChanged">
                            </asp:DropDownList>
                            &nbsp;
                            <asp:Label ID="lblmain" runat="server" Visible="False"></asp:Label>
                        </label>
                        <label>
                            <asp:Label ID="Label9" runat="server" Text="Work Order Number"></asp:Label>
                            <%--<asp:Label ID="Label24" runat="server" Text="*" CssClass="labelstar"></asp:Label>--%>
                            <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtJobno"
                                ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="REG14889" runat="server" ErrorMessage="*" Display="Dynamic"
                                SetFocusOnError="True" ValidationExpression="^([a-zA-Z0-9\s]*)" ControlToValidate="txtJobno"
                                ValidationGroup="1"></asp:RegularExpressionValidator>--%>
                            <br />
                            <asp:Label ID="txtJobno" runat="server" Text=""></asp:Label>
                            <%--<asp:TextBox ID="txtJobno" runat="server" MaxLength="15"></asp:TextBox>--%>
                            <asp:Label ID="lblsubcat" runat="server" Visible="False"></asp:Label>
                        </label>
                        <label>
                            <asp:Label ID="Label10" runat="server" Text="Reference No."></asp:Label>
                            <asp:Label ID="Label19" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtRefernceNo"
                                ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Invalid Character"
                                SetFocusOnError="True" ValidationExpression="^([a-zA-Z0-9\s]*)" ControlToValidate="txtRefernceNo"
                                ValidationGroup="1"></asp:RegularExpressionValidator>
                            <div style="clear: both;">
                            </div>
                            <asp:TextBox ID="txtRefernceNo" runat="server" MaxLength="15" onKeydown="return mask(event)"
                                Width="150px" onkeyup="return check(this,/[\\/!@#$%_^'.&*()>+:;={}[]|\/]/g,/^[\a-zA-Z0-9\s]+$/,'div1',15)"></asp:TextBox>
                            <asp:Label ID="Label60" CssClass="labelcount" runat="server" Text="Max "></asp:Label>
                            <span id="div1" class="labelcount">15</span>
                            <asp:Label ID="Label5" CssClass="labelcount" runat="server" Text="(A-Z 0-9)"></asp:Label>
                            <asp:Label ID="lblsubsub" runat="server" Visible="False"></asp:Label>
                        </label>
                        <label>
                            <br />
                            <div style="clear: both;">
                            </div>
                        </label>
                        <div style="clear: both;">
                        </div>
                        <label>
                            <asp:Label ID="Label11" runat="server" Text="Work Order Name ">
                            </asp:Label>
                            <asp:Label ID="Label23" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtjobname"
                                ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ErrorMessage="Invalid Character"
                                SetFocusOnError="True" ValidationExpression="^([,._a-zA-Z0-9\s]*)" ControlToValidate="txtjobname"
                                ValidationGroup="1"></asp:RegularExpressionValidator>
                            <asp:TextBox ID="txtjobname" runat="server" MaxLength="60" Width="350px" onKeydown="return mask(event)"
                                onkeyup="return check(this,/[\\/!@#$%^'&*()>+:;={}[]|\/]/g,/^[\a-zA-Z_.,0-9\s]+$/,'Span1',60)"></asp:TextBox>
                            <asp:Label ID="lblinvname" runat="server" Visible="False"></asp:Label>
                        </label>
                        <label>
                            <br />
                            <asp:Label ID="Label4" CssClass="labelcount" runat="server" Text="Max "></asp:Label>
                            <span id="Span1" class="labelcount">60</span>
                            <asp:Label ID="Label6" CssClass="labelcount" runat="server" Text="(A-Z 0-9 _ .)"></asp:Label>
                        </label>
                        <div style="clear: both;">
                        </div>
                        <label>
                            <asp:Label ID="Label12" runat="server" Text=" Customer Name "></asp:Label>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlPartyName"
                                ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                            <asp:DropDownList ID="ddlPartyName" runat="server" Width="150px">
                            </asp:DropDownList>
                        </label>
                        <label>
                            <br />
                            <asp:ImageButton ID="imgAdd2" runat="server" AlternateText="Add New" ImageUrl="~/ShoppingCart/images/AddNewRecord.jpg"
                                Height="20px" ToolTip="Add New " Width="20px" OnClick="imgAdd2_Click" ImageAlign="Bottom" />
                        </label>
                        <label>
                            <br />
                            <asp:ImageButton ID="LinkButton97666667" runat="server" Height="20px" ToolTip="Refresh "
                                Width="20px" AlternateText="Refresh" ImageUrl="~/ShoppingCart/images/DataRefresh.jpg"
                                OnClick="LinkButton97666667_Click" ImageAlign="Bottom"></asp:ImageButton>
                        </label>
                        <label>
                            <asp:Label ID="Label13" runat="server" Text=" Status"></asp:Label>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="ddlStatus"
                                ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                            <asp:DropDownList ID="ddlStatus" runat="server" Width="100px">
                            </asp:DropDownList>
                        </label>
                        <div style="clear: both;">
                        </div>
                        <table>
                            <tr>
                                <td>
                                    <label>
                                        <asp:Label ID="Label14" runat="server" Text="Work Order Start Date">
                                        </asp:Label>
                                        <asp:Label ID="Label25" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValsdfidator4" runat="server"
                                            ErrorMessage="*" ControlToValidate="txtstartdate" ValidationGroup="1" ValidationExpression="^[0-9]{1,2}[/][0-9]{1,2}[/][0-9]{4}$">
                                        </asp:RegularExpressionValidator>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ControlToValidate="txtstartdate"
                                            ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                                    </label>
                                </td>
                                <td>
                                    <label>
                                        <asp:Label ID="Label24" runat="server" Text="Target Date">
                                        </asp:Label>
                                        <asp:Label ID="Label28" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="TargetDate"
                                            ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ErrorMessage="*"
                                            ControlToValidate="TargetDate" ValidationGroup="1" ValidationExpression="^[0-9]{1,2}[/][0-9]{1,2}[/][0-9]{4}$">
                                        </asp:RegularExpressionValidator>
                                    </label>
                                </td>
                                <td>
                                    <label>
                                        <asp:Label ID="Label15" runat="server" Text="Work Order End Date"></asp:Label>
                                        <asp:Label ID="Label26" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                                        <asp:RegularExpressionValidator ID="rghjk" runat="server" ErrorMessage="*" ControlToValidate="txtenddate"
                                            ValidationGroup="1" ValidationExpression="^[0-9]{1,2}[/][0-9]{1,2}[/][0-9]{4}$"></asp:RegularExpressionValidator>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtenddate"
                                            ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                                    </label>
                                </td>
                            </tr>
                            <tr valign="top">
                                <td>
                                    <label>
                                        <asp:TextBox ID="txtstartdate" runat="server" Width="75px" MaxLength="10"></asp:TextBox>
                                        <cc1:CalendarExtender ID="CalendarExtender2" runat="server" PopupButtonID="ImageButton2"
                                            Format="MM/dd/yyyy" TargetControlID="txtstartdate">
                                        </cc1:CalendarExtender>
                                    </label>
                                    <label>
                                        <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/ShoppingCart/images/cal_actbtn.jpg" />
                                    </label>
                                </td>
                                <td>
                                    <label>
                                        <asp:TextBox ID="TargetDate" runat="server" Width="75px" MaxLength="10"></asp:TextBox>
                                        <cc1:CalendarExtender ID="CalendarExtender3" runat="server" PopupButtonID="ImageButton1"
                                            Format="MM/dd/yyyy" TargetControlID="TargetDate">
                                        </cc1:CalendarExtender>
                                    </label>
                                    <label>
                                        <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/ShoppingCart/images/cal_actbtn.jpg" />
                                    </label>
                                </td>
                                <td>
                                    <label>
                                        <asp:TextBox ID="txtenddate" runat="server" Width="75px" MaxLength="10"></asp:TextBox>
                                        <%--<asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/ShoppingCart/images/cal_actbtn.jpg" />--%>
                                        <cc1:CalendarExtender ID="CalendarExtender1" runat="server" PopupButtonID="ImageButton4"
                                            Format="MM/dd/yyyy" TargetControlID="txtenddate">
                                        </cc1:CalendarExtender>
                                    </label>
                                    <label>
                                        <asp:ImageButton ID="ImageButton4" runat="server" ImageUrl="~/ShoppingCart/images/cal_actbtn.jpg" />
                                    </label>
                                </td>
                            </tr>
                        </table>
                        <%--<asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="The end date must be the current date, or greater than the current date"
                                ControlToValidate="txtenddate" Display="Dynamic" ControlToCompare="txtstartdate"
                                Operator="GreaterThanEqual"></asp:CompareValidator>--%>
                        <div style="clear: both;">
                        </div>
                        <label>
                            <asp:Label ID="Label16" runat="server" Text="Note"></asp:Label>
                            <asp:Label ID="Label27" runat="server" Text="*" CssClass="labelstar"></asp:Label>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txtnote"
                                ErrorMessage="*" ValidationGroup="1"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ErrorMessage="Invalid Character"
                                Display="Dynamic" SetFocusOnError="True" ValidationExpression="^([_.a-zA-Z0-9\s]*)"
                                ControlToValidate="txtnote" ValidationGroup="1"></asp:RegularExpressionValidator>
                            <%--<asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ErrorMessage="Please enter 1000 chars"
                                Display="Dynamic" SetFocusOnError="True" ValidationExpression="^([\S\s]{0,1000})$"
                                ControlToValidate="txtnote" ValidationGroup="1"></asp:RegularExpressionValidator>--%>
                            <br />
                            <asp:TextBox ID="txtnote" runat="server" TextMode="MultiLine" MaxLength="1000" Width="400px"
                                Height="100px" onkeypress="return checktextboxmaxlength(this,1000,event)" onkeyup="return check(this,/[\\/!@#$%^'&*()>+:;={}[]|\/]/g,/^[\.a-zA-Z_0-9\s]+$/,'Span3',1000)"></asp:TextBox>
                            <asp:Label ID="Label69" CssClass="labelcount" runat="server" Text="Max "></asp:Label>
                            <span id="Span3" class="labelcount">1000</span>
                            <asp:Label ID="Label7" CssClass="labelcount" runat="server" Text="(A-Z 0-9 _ .)"></asp:Label>
                        </label>
                        <div style="clear: both;">
                        </div>
                        <br />
                        <asp:Button ID="Button3" runat="server" Text="Submit" CssClass="btnSubmit" OnClick="Button3_Click"
                            ValidationGroup="1" />
                        <asp:Button ID="Button7" runat="server" OnClick="Button7_Click" Text="Update" CssClass="btnSubmit"
                            Visible="False" ValidationGroup="1" />
                        &nbsp;<asp:Button ID="Button4" runat="server" CssClass="btnSubmit" OnClick="Button4_Click"
                            Text="Cancel" />
                        <div style="clear: both;">
                        </div>
                        <label>
                            <asp:Label ID="Label20" runat="server" Text="">
                            </asp:Label>
                        </label>
                        <label>
                            <asp:Label ID="Label21" runat="server" Text=""></asp:Label>
                        </label>
                        <label>
                            <asp:Label ID="Label22" runat="server" Text=""></asp:Label>
                        </label>
                        <div style="clear: both;">
                        </div>
                    </asp:Panel>
                </fieldset>
                <div style="clear: both;">
                </div>
                <fieldset>
                    <legend>
                        <asp:Label ID="lblList" Text="List of Work Orders" runat="server">
                         
                        </asp:Label>
                    </legend>
                    <div style="float: right;">
                        <asp:Button ID="btnjv" runat="server" Text="Recalculate Work Order Cost" CssClass="btnSubmit"
                            OnClick="btnjv_Click" />
                        <asp:Button ID="Button1" runat="server" Text="Printable Version" CssClass="btnSubmit"
                            OnClick="Button1_Click" />
                        <input id="Button2" runat="server" class="btnSubmit" onclick="document.getElementById('mydiv').className='open';javascript:CallPrint('divPrint');document.getElementById('mydiv').className='closed';"
                            type="button" value="Print" visible="false" />
                    </div>
                    <div style="clear: both;">
                    </div>
                    <label>
                        <asp:Label ID="Label17" Text=" Select by Business Name" runat="server">
                        </asp:Label>
                        <asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="True" Width="150px"
                            OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged">
                        </asp:DropDownList>
                    </label>
                    <div style="clear: both;">
                    </div>
                    <asp:Panel ID="pnlgrid" runat="server" Width="100%">
                        <table width="100%">
                            <tr>
                                <td>
                                    <br />
                                </td>
                            </tr>
                            <tr align="center">
                                <td>
                                    <div id="mydiv" class="closed">
                                        <table width="100%">
                                            <tr align="center">
                                                <td align="center" style="text-align: center; font-size: 20px; font-weight: bold;">
                                                    <asp:Label ID="lblCompany" runat="server" Font-Italic="true" Visible="false"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr align="center">
                                                <td align="center" style="text-align: center; font-size: 18px; font-weight: bold;">
                                                    <asp:Label ID="Label18" runat="server" Text="Business : " Font-Italic="true"></asp:Label>
                                                    <asp:Label ID="lblBusiness" runat="server" Font-Italic="true"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr align="center">
                                                <td align="center" style="text-align: center; font-size: 18px; font-weight: bold;">
                                                    <asp:Label ID="Label3" runat="server" Font-Italic="true" Text="List of Work Orders">
                                                    </asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <cc11:PagingGridView ID="GridView1" runat="server" AllowPaging="True" AllowSorting="True"
                                        AutoGenerateColumns="False" DataKeyNames="Id" Width="100%" OnRowDeleting="GridView1_RowDeleting"
                                        OnSorting="GridView1_Sorting" PageSize="25" EmptyDataText="No Record Found."
                                        CssClass="mGrid" GridLines="Both" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt"
                                        OnPageIndexChanging="GridView1_PageIndexChanging">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Business" SortExpression="Name" ItemStyle-Width="10%"
                                                ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblBus" runat="server" Text='<%#Bind("Name") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Customer Name" SortExpression="Compname" ItemStyle-Width="14%"
                                                ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblPartyName" runat="server" Text='<%#Bind("Compname") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Work Order No." SortExpression="JobNumber" ItemStyle-Width="7%"
                                                ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <a href='JobProfile.aspx?Id=<%#Eval("Id")%>&pid=<%#Eval("PartyId") %>' target="_blank">
                                                        <asp:Label ID="lblJobno" runat="server" ForeColor="Black" Text='<%#Bind("JobNumber") %>'></asp:Label>
                                                    </a>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Ref No." SortExpression="JobReferenceNo" ItemStyle-Width="10%"
                                                ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <a href='JobProfile.aspx?Id=<%#Eval("Id")%>&pid=<%#Eval("PartyId") %>' target="_blank">
                                                        <asp:Label ID="lblReferenceNo" runat="server" ForeColor="Black" Text='<%#Bind("JobReferenceNo") %>'></asp:Label>
                                                    </a>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Work Order Name" SortExpression="JobName" ItemStyle-Width="13%"
                                                ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <a href='JobProfile.aspx?Id=<%#Eval("Id")%>&pid=<%#Eval("PartyId") %>' target="_blank">
                                                        <asp:Label ID="lblJobName" runat="server" ForeColor="Black" Text='<%#Bind("JobName") %>'></asp:Label>
                                                    </a>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Status" SortExpression="StatusName" ItemStyle-Width="8%"
                                                ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblStatus" runat="server" Text='<%#Bind("StatusName") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Work Order Cost" SortExpression="cost" ItemStyle-Width="8%"
                                                ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblnote" runat="server" Text='<%#Bind("cost") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Start Date" SortExpression="JobStartDate" ItemStyle-Width="4%"
                                                ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblStartDate" runat="server" Text='<%#Bind("JobStartDate") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Target Date" SortExpression="TargetDate" ItemStyle-Width="4%"
                                                ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbltargetDate" runat="server" Text='<%#Bind("TargetDate") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="End Date" SortExpression="JobEndDate" ItemStyle-Width="4%"
                                                ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblEndDate" runat="server" Text='<%#Bind("JobEndDate") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Edit" ItemStyle-Width="2%" HeaderImageUrl="~/Account/images/edit.gif"
                                                HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="ImageButton3" runat="server" CommandArgument='<%# Eval("Id") %>'
                                                        OnClick="LinkButton4_Click" ImageUrl="~/Account/images/edit.gif" ToolTip="Edit" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Delete" HeaderImageUrl="~/ShoppingCart/images/trash.jpg"
                                                ItemStyle-Width="2%" HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="Button1" runat="server" CommandName="Delete" ImageUrl="~/Account/images/delete.gif"
                                                        ToolTip="Delete" OnClientClick="return confirm('Do you wish to delete this record?');">
                                                    </asp:ImageButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </cc11:PagingGridView>
                                    <br />
                                    <asp:Panel ID="Panel3" runat="server" BackColor="#CCCCCC" BorderColor="#333333" Width="399px">
                                        <table>
                                            <tr>
                                                <td>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="center" style="background-color: #CCCCCC">
                                                    <asp:Label ID="Label2" runat="server" ForeColor="Black">You Sure , You want to Delete this Record?</asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="center">
                                                    &nbsp;<asp:Button ID="Button5" runat="server" Text="Yes" BackColor="#CCCCCC" Width="55px"
                                                        CssClass="btnSubmit" Height="25px" />
                                                    &nbsp;<asp:Button ID="Button6" runat="server" Text="No" BackColor="#CCCCCC" Width="55px"
                                                        CssClass="btnSubmit" Height="25px" />
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
                    <asp:Panel ID="panelhide" runat="server" Visible="false">
                        <asp:GridView ID="grdMaterialIssue" runat="server" AllowPaging="True" AllowSorting="True"
                            AutoGenerateColumns="False" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt"
                            CssClass="mGrid" GridLines="Both" EmptyDataText="No Record Found." Width="100%">
                            <Columns>
                                <asp:TemplateField HeaderText="Material Name" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-Width="30%">
                                    <ItemTemplate>
                                        <asp:Label ID="lblitemnameid" runat="server" Text='<%# Eval("InvWMasterId") %>' Visible="false"></asp:Label>
                                        <asp:Label ID="lbljobname124" runat="server" Text='<%# Eval("Name") %>'></asp:Label>
                                        <asp:Label ID="lblmaterialmasterid" runat="server" Text='<%# Eval("Id") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Issue Date" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-Width="10%">
                                    <ItemTemplate>
                                        <asp:Label ID="lbldate124" runat="server"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Quantity" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-Width="18%">
                                    <ItemTemplate>
                                        <asp:Label ID="lblqty" runat="server" Text='<%# Eval("Qty") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Rate" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-Width="17%">
                                    <ItemTemplate>
                                        <asp:Label ID="lblRate" runat="server" Text='<%# Eval("Rate") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Total Cost" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-Width="18%">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCost" runat="server"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="ViewDetail" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-Width="7%">
                                    <ItemTemplate>
                                        <a href='Materialissueform.aspx?jobid=<%# Eval("JobMasterId")%>&amp;materilInvId=<%#Eval("InvWMasterId") %>'
                                            target="_blank">
                                            <asp:Label ID="lblmateriliss" runat="server" Text="View Detail" ForeColor="#0066FF"></asp:Label>
                                        </a>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                        <asp:GridView ID="grdoverhead" runat="server" AllowPaging="True" AllowSorting="True"
                            AutoGenerateColumns="False" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt"
                            CssClass="mGrid" GridLines="Both" DataKeyNames="Id" Width="100%" EmptyDataText="No Record Found.">
                            <Columns>
                                <asp:TemplateField HeaderText="Overhead Name" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" ItemStyle-Width="20%">
                                    <ItemTemplate>
                                        <asp:Label ID="lbloverheadname" runat="server" Text='<%#Bind("Name") %>'></asp:Label>
                                        <asp:Label ID="lbloverheadmasterid" runat="server" Text='<%#Bind("Id") %>' Visible="false"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Start Date" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                    ItemStyle-Width="8%">
                                    <ItemTemplate>
                                        <asp:Label ID="lblstartdate789" runat="server" Text='<%#Bind("StartDate","{0:MM/dd/yyy}") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="End Date" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                    ItemStyle-Width="8%">
                                    <ItemTemplate>
                                        <asp:Label ID="lblenddate789" runat="server" Text='<%#Bind("EndDate","{0:MM/dd/yyy}") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Overhead By Material" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" ItemStyle-Width="15%">
                                    <ItemTemplate>
                                        <asp:Label ID="lblohbymaterial789" runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Overhead By Labour Cost" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" ItemStyle-Width="15%">
                                    <ItemTemplate>
                                        <asp:Label ID="lbldirectlabour789" runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Overhead By Project Duration " HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" ItemStyle-Width="15%">
                                    <ItemTemplate>
                                        <asp:Label ID="lblnoofdays789" runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Overhead By Equal" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" ItemStyle-Width="15%">
                                    <ItemTemplate>
                                        <asp:Label ID="ohbyequal789" runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Total" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                    ItemStyle-Width="15%">
                                    <ItemTemplate>
                                        <asp:Label ID="lblOhAllocationtotal789" runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                        <asp:GridView ID="grddailywork" runat="server" AllowPaging="True" AllowSorting="True"
                            AutoGenerateColumns="False" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt"
                            CssClass="mGrid" GridLines="Both" DataKeyNames="Id" Width="100%" EmptyDataText="No Record Found.">
                            <Columns>
                                <asp:TemplateField HeaderText="Employee Name" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" ItemStyle-Width="40%">
                                    <ItemTemplate>
                                        <asp:Label ID="lblEmployee" runat="server" Text='<%#Bind("EmployeeName") %>'></asp:Label>
                                        <asp:Label ID="lblmasterid" runat="server" Text='<%#Bind("Id") %>' Visible="false"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Hours" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                    ItemStyle-Width="15%">
                                    <ItemTemplate>
                                        <asp:Label ID="lblhours" runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Cost" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                    ItemStyle-Width="18%">
                                    <ItemTemplate>
                                        <asp:Label ID="lblcost" runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="ViewDetail" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                    ItemStyle-Width="7%">
                                    <ItemTemplate>
                                        <a href='DailyWorkSheet.aspx?EmployeeId=<%# Eval("EmployeeMasterID")%>&amp;JobId=<%#Eval("JobMasterId") %>'
                                            target="_blank">
                                            <asp:Label ID="lblentrytyp2e" runat="server" Text="View Detail" ForeColor="#0066FF"></asp:Label></a>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                        <asp:Label ID="lblTotalSum" runat="server" Text="Label"></asp:Label>
                        <asp:Label ID="lbltotaloverheadbyall" runat="server" Text=""></asp:Label>
                        <asp:Label ID="lbldailyworktotal" runat="server" Text=""></asp:Label>
                        <asp:Label ID="lblMyfinal" runat="server" Text=""></asp:Label>
                    </asp:Panel>
                    <div style="clear: both;">
                    </div>
                </fieldset>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
