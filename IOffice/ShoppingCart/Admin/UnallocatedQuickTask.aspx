<%@ Page Language="C#" MasterPageFile="~/Master/LicenseMaster.master"
    AutoEventWireup="true" CodeFile="UnAllocatedQuickTask.aspx.cs" Inherits="ShoppingCart_Admin_AddQuickTask"
    Title="Untitled Page" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <ContentTemplate>
            <div style="padding-left: 2%">
                <asp:Label ID="lblmsg" runat="server" Visible="False" ForeColor="Red"></asp:Label>
            </div>
            <div style="clear: both;">
            </div>
            <div class="products_box">
                <fieldset>
                    <legend>
                        <asp:Label ID="statuslable" runat="server" Text="Add General Task"></asp:Label>
                    </legend>
                    <div style="clear: both;">
                    </div>
                    <table style="width: 100%">
                        <tr>
                            <td valign="bottom">
                                <label>
                                    Business Name
                                </label>
                                <label>
                                    <asp:DropDownList ID="ddlStore" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlStore_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </label>
                            </td>
                        </tr>
                    </table>
                    <table style="width: 100%">
                        <tr>
                            <td style="width: 30%" valign="bottom">
                                <label>
                                    Tasks for (Optional)
                                </label>
                                <label>
                                    <asp:DropDownList ID="ddltaskfor" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddltaskfor_SelectedIndexChanged">
                                        <asp:ListItem Text="-Select-" Selected="True" Value="0"></asp:ListItem>
                                        <asp:ListItem Text="Department" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="Division" Value="2"></asp:ListItem>
                                    </asp:DropDownList>
                                </label>
                            </td>
                            <td>
                                <asp:Panel ID="Panel2" runat="server" Visible="false">
                                    <label>
                                        Department
                                    </label>
                                    <label>
                                        <asp:DropDownList ID="ddldepartment" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddldepartment_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </label>
                                </asp:Panel>
                                <asp:Panel ID="Panel3" runat="server" Visible="false">
                                    <label>
                                        Division
                                    </label>
                                    <label>
                                        <asp:DropDownList ID="ddldivision" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddldivision_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </label>
                                </asp:Panel>
                            </td>
                        </tr>
                    </table>
                    <table style="width: 100%">
                        <tr>
                            <td valign="bottom" style="width: 22%">
                                <asp:CheckBox ID="CheckBox1" runat="server" Text="Allocate to Employee ? (Optional)"
                                    TextAlign="Left" AutoPostBack="true" OnCheckedChanged="CheckBox1_CheckedChanged" />
                            </td>
                            <td valign="bottom" style="width: 22%">
                                <asp:CheckBox ID="CheckBox2" runat="server" Text="For Weekly Goal ? (Optional)" AutoPostBack="true"
                                    TextAlign="Left" OnCheckedChanged="CheckBox2_CheckedChanged" />
                            </td>
                            <td valign="bottom" style="width: 56%">
                                <asp:CheckBox ID="CheckBox3" runat="server" Text="For Project ? (Optional)" TextAlign="Left"
                                    AutoPostBack="true" OnCheckedChanged="CheckBox3_CheckedChanged" />
                            </td>
                        </tr>
                    </table>
                    <table style="width: 100%">
                        <tr>
                            <td style="width: 22%">
                                <asp:Panel ID="Panel5" runat="server" Visible="false">
                                    <label>
                                        <asp:DropDownList ID="ddlnewtaskempname" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlnewtaskempname_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </label>
                                </asp:Panel>
                            </td>
                            <td style="width: 22%">
                                <asp:Panel ID="Panel6" runat="server" Visible="false">
                                    <label>
                                        <asp:DropDownList ID="DropDownList1" runat="server">
                                        </asp:DropDownList>
                                    </label>
                                </asp:Panel>
                            </td>
                            <td style="width: 56%">
                                <asp:Panel ID="Panel7" runat="server" Visible="false">
                                    <label>
                                        <asp:DropDownList ID="DropDownList2" runat="server">
                                        </asp:DropDownList>
                                    </label>
                                </asp:Panel>
                            </td>
                        </tr>
                    </table>
                    <table style="width: 100%">
                        <tr>
                            <td colspan="4">
                                <table style="width: 100%">
                                    <tr>
                                        <td>
                                        </td>
                                        <td>
                                        </td>
                                        <td align="right">
                                        </td>
                                        <td align="left">
                                            <asp:Label ID="lblallocatedhours" runat="server" Visible="False"></asp:Label>
                                        </td>
                                        <td align="right">
                                        </td>
                                        <td align="left">
                                            <asp:Label ID="lblavalablehours" runat="server" Visible="False">0:0</asp:Label>
                                        </td>
                                        <td align="right">
                                        </td>
                                        <td align="left">
                                            <asp:Label ID="lblregularbatchhour" runat="server" Visible="False">0:0</asp:Label>
                                        </td>
                                        <td align="right">
                                        </td>
                                        <td align="left">
                                            <asp:Label ID="lblbatchtimeschedule" runat="server" Visible="False"></asp:Label>
                                            <asp:Label ID="lblinserttime" runat="server" Visible="False"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 25%" align="right">
                            </td>
                            <td style="width: 25%">
                            </td>
                            <td style="width: 25%" align="right">
                            </td>
                            <td style="width: 25%">
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4">
                                <%--  <asp:Panel ID="pnlhdboth" runat="server" Visible="false">
                                    <table>
                                        <tr>
                                            <td valign="bottom">
                                                <label>
                                                    <asp:Label ID="Label1" runat="server" Text="Show Columns : "></asp:Label>
                                                </label>
                                            </td>
                                            <td valign="top">
                                                <label>
                                                </label>
                                                <label>
                                                </label>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>--%>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4">
                                <table style="width: 100%">
                                    <tr>
                                        <td style="width: 2%">
                                        </td>
                                        <%--<td>
                                            <label>
                                                Weekly Goals
                                            </label>
                                        </td>
                                        <td>
                                            <label>
                                                Project
                                            </label>
                                        </td>--%>
                                        <td align="left">
                                            <label>
                                                Task
                                            </label>
                                        </td>
                                        <td align="left">
                                            <label>
                                                Bud Minute
                                            </label>
                                        </td>
                                        <td align="left">
                                            <label>
                                                Date
                                            </label>
                                        </td>
                                        <td align="left">
                                            <label>
                                                More..
                                            </label>
                                        </td>
                                        <td align="left">
                                            <label>
                                                Remove
                                            </label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 2%">
                                            1.
                                        </td>
                                        <%-- <td>
                                            <asp:DropDownList ID="ddlweeklygoal" runat="server" OnSelectedIndexChanged="ddlweeklygoal_SelectedIndexChanged"
                                                Width="250px">
                                            </asp:DropDownList>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlproject" runat="server" Width="250px">
                                            </asp:DropDownList>
                                        </td>--%>
                                        <td align="left">
                                            <label>
                                                <asp:TextBox ID="txtTaskName" runat="server" Width="820px"></asp:TextBox>
                                            </label>
                                        </td>
                                        <td align="left">
                                            <label>
                                                <asp:TextBox ID="txtbudgetminute" runat="server" Width="70px"></asp:TextBox>
                                            </label>
                                        </td>
                                        <td align="left">
                                            <label>
                                                <asp:TextBox ID="txteenddate" runat="server" Width="70px"></asp:TextBox>
                                            </label>
                                            <label>
                                                <asp:ImageButton ID="ImageButton11" runat="server" ImageUrl="~/images/cal_actbtn.jpg" />
                                                <cc1:CalendarExtender ID="Calendarextender5" runat="server" TargetControlID="txteenddate"
                                                    PopupButtonID="ImageButton11">
                                                </cc1:CalendarExtender>
                                            </label>
                                        </td>
                                        <td align="left">
                                            <asp:LinkButton ID="LinkButton1" runat="server" Text="More" OnClick="LinkButton1_Click">
                                            </asp:LinkButton>
                                        </td>
                                        <td align="left" valign="top">
                                            <%--<asp:CheckBox ID="chkbx" runat="server" />--%>
                                            <label>
                                                <asp:ImageButton ID="ImageButton1" runat="server" Height="15px" ImageUrl="~/images/closeicon.jpeg"
                                                    Width="15px" OnClick="ImageButton1_Click" />
                                            </label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            2.
                                        </td>
                                        <%-- <td>
                                            <asp:DropDownList ID="ddlweeklygoal2" runat="server" Width="250px" OnSelectedIndexChanged="ddlweeklygoal2_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlproject2" runat="server" Width="250px">
                                            </asp:DropDownList>
                                        </td>--%>
                                        <td align="left">
                                            <label>
                                                <asp:TextBox ID="txttaskname2" runat="server" Width="820px"></asp:TextBox>
                                            </label>
                                        </td>
                                        <td align="left">
                                            <label>
                                                <asp:TextBox ID="txtbudgetedminute2" runat="server" Width="70px"></asp:TextBox>
                                            </label>
                                        </td>
                                        <td align="left">
                                            <label>
                                                <asp:TextBox ID="txteenddate2" runat="server" Width="70px"></asp:TextBox>
                                            </label>
                                            <label>
                                                <asp:ImageButton ID="ImageButton12" runat="server" ImageUrl="~/images/cal_actbtn.jpg" />
                                                <cc1:CalendarExtender ID="Calendarextender1" runat="server" TargetControlID="txteenddate2"
                                                    PopupButtonID="ImageButton12">
                                                </cc1:CalendarExtender>
                                            </label>
                                        </td>
                                        <td align="left">
                                            <asp:LinkButton ID="LinkButton2" runat="server" Text="More" OnClick="LinkButton2_Click">
                                            </asp:LinkButton>
                                        </td>
                                        <td align="left" valign="top">
                                            <%--<asp:CheckBox ID="chk2" runat="server" />--%>
                                            <label>
                                                <asp:ImageButton ID="ImageButton2" runat="server" Height="15px" ImageUrl="~/images/closeicon.jpeg"
                                                    Width="15px" OnClick="ImageButton2_Click" />
                                            </label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            3.
                                        </td>
                                        <%-- <td>
                                            <asp:DropDownList ID="ddlweeklygoal3" runat="server" Width="250px" OnSelectedIndexChanged="ddlweeklygoal3_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlproject3" runat="server" Width="250px">
                                            </asp:DropDownList>
                                        </td>--%>
                                        <td align="left">
                                            <label>
                                                <asp:TextBox ID="txttaskname3" runat="server" Width="820px"></asp:TextBox>
                                            </label>
                                        </td>
                                        <td align="left">
                                            <label>
                                                <asp:TextBox ID="txtbudgetedminute3" runat="server" Width="70px"></asp:TextBox>
                                            </label>
                                        </td>
                                        <td align="left">
                                            <label>
                                                <asp:TextBox ID="txteenddate3" runat="server" Width="70px"></asp:TextBox>
                                            </label>
                                            <label>
                                                <asp:ImageButton ID="ImageButton13" runat="server" ImageUrl="~/images/cal_actbtn.jpg" />
                                                <cc1:CalendarExtender ID="Calendarextender2" runat="server" TargetControlID="txteenddate3"
                                                    PopupButtonID="ImageButton13">
                                                </cc1:CalendarExtender>
                                            </label>
                                        </td>
                                        <td align="left">
                                            <asp:LinkButton ID="LinkButton3" runat="server" Text="More" OnClick="LinkButton3_Click">
                                            </asp:LinkButton>
                                        </td>
                                        <td align="left" valign="top">
                                            <%--<asp:CheckBox ID="chk3" runat="server" />--%>
                                            <label>
                                                <asp:ImageButton ID="ImageButton3" runat="server" Height="15px" ImageUrl="~/images/closeicon.jpeg"
                                                    Width="15px" OnClick="ImageButton3_Click1" />
                                            </label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            4.
                                        </td>
                                        <%--   <td>
                                            <asp:DropDownList ID="ddlweeklygoal4" runat="server" Width="250px" OnSelectedIndexChanged="ddlweeklygoal4_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlproject4" runat="server" Width="250px">
                                            </asp:DropDownList>
                                        </td>--%>
                                        <td align="left">
                                            <label>
                                                <asp:TextBox ID="txttaskname4" runat="server" Width="820px"></asp:TextBox>
                                            </label>
                                        </td>
                                        <td align="left">
                                            <label>
                                                <asp:TextBox ID="txtbudgetedminute4" runat="server" Width="70px"></asp:TextBox>
                                            </label>
                                        </td>
                                        <td align="left">
                                            <label>
                                                <asp:TextBox ID="txteenddate4" runat="server" Width="70px"></asp:TextBox>
                                            </label>
                                            <label>
                                                <asp:ImageButton ID="ImageButton4j" runat="server" ImageUrl="~/images/cal_actbtn.jpg" />
                                                <cc1:CalendarExtender ID="Calendarextender3" runat="server" TargetControlID="txteenddate4"
                                                    PopupButtonID="ImageButton4j">
                                                </cc1:CalendarExtender>
                                            </label>
                                        </td>
                                        <td align="left">
                                            <asp:LinkButton ID="LinkButton4" runat="server" Text="More" OnClick="LinkButton4_Click">
                                            </asp:LinkButton>
                                        </td>
                                        <td align="left" valign="top">
                                            <%--<asp:CheckBox ID="chk4" runat="server" />--%>
                                            <label>
                                                <asp:ImageButton ID="ImageButton5" runat="server" Height="15px" ImageUrl="~/images/closeicon.jpeg"
                                                    Width="15px" OnClick="ImageButton5_Click" />
                                            </label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            5.
                                        </td>
                                        <%-- <td>
                                            <asp:DropDownList ID="ddlweeklygoal5" runat="server" Width="250px" OnSelectedIndexChanged="ddlweeklygoal5_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlproject5" runat="server" Width="250px">
                                            </asp:DropDownList>
                                        </td>--%>
                                        <td align="left">
                                            <label>
                                                <asp:TextBox ID="txttaskname5" runat="server" Width="820px"></asp:TextBox>
                                            </label>
                                        </td>
                                        <td align="left">
                                            <label>
                                                <asp:TextBox ID="txtbudgetedminute5" runat="server" Width="70px"></asp:TextBox>
                                            </label>
                                        </td>
                                        <td align="left">
                                            <label>
                                                <asp:TextBox ID="txteenddate5" runat="server" Width="70px"></asp:TextBox>
                                            </label>
                                            <label>
                                                <asp:ImageButton ID="ImageButton14" runat="server" ImageUrl="~/images/cal_actbtn.jpg" />
                                                <cc1:CalendarExtender ID="Calendarextender4" runat="server" TargetControlID="txteenddate5"
                                                    PopupButtonID="ImageButton14">
                                                </cc1:CalendarExtender>
                                            </label>
                                        </td>
                                        <td align="left">
                                            <asp:LinkButton ID="LinkButton5" runat="server" Text="More" OnClick="LinkButton5_Click">
                                            </asp:LinkButton>
                                        </td>
                                        <td align="left" valign="top">
                                            <%--<asp:CheckBox ID="chk5" runat="server" />--%>
                                            <label>
                                                <asp:ImageButton ID="ImageButton6" runat="server" Height="15px" ImageUrl="~/images/closeicon.jpeg"
                                                    Width="15px" OnClick="ImageButton6_Click" />
                                            </label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            6.
                                        </td>
                                        <%-- <td>
                                            <asp:DropDownList ID="ddlweeklygoal6" runat="server" Width="250px" OnSelectedIndexChanged="ddlweeklygoal6_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlproject6" runat="server" Width="250px">
                                            </asp:DropDownList>
                                        </td>--%>
                                        <td align="left">
                                            <label>
                                                <asp:TextBox ID="txttaskname6" runat="server" Width="820px"></asp:TextBox>
                                            </label>
                                        </td>
                                        <td align="left">
                                            <label>
                                                <asp:TextBox ID="txtbudgetedminute6" runat="server" Width="70px"></asp:TextBox>
                                            </label>
                                        </td>
                                        <td align="left">
                                            <label>
                                                <asp:TextBox ID="txteenddate6" runat="server" Width="70px"></asp:TextBox>
                                            </label>
                                            <label>
                                                <asp:ImageButton ID="ImageButton232" runat="server" ImageUrl="~/images/cal_actbtn.jpg" />
                                                <cc1:CalendarExtender ID="Calendarextender6" runat="server" TargetControlID="txteenddate6"
                                                    PopupButtonID="ImageButton232">
                                                </cc1:CalendarExtender>
                                            </label>
                                        </td>
                                        <td align="left">
                                            <asp:LinkButton ID="LinkButton6" runat="server" Text="More" OnClick="LinkButton6_Click">
                                            </asp:LinkButton>
                                        </td>
                                        <td align="left" valign="top">
                                            <%--<asp:CheckBox ID="chk6" runat="server" />--%>
                                            <label>
                                                <asp:ImageButton ID="ImageButton7" runat="server" Height="15px" ImageUrl="~/images/closeicon.jpeg"
                                                    Width="15px" OnClick="ImageButton7_Click" />
                                            </label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            7.
                                        </td>
                                        <%-- <td>
                                            <asp:DropDownList ID="ddlweeklygoal7" runat="server" Width="250px" OnSelectedIndexChanged="ddlweeklygoal7_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlproject7" runat="server" Width="250px">
                                            </asp:DropDownList>
                                        </td>--%>
                                        <td align="left">
                                            <label>
                                                <asp:TextBox ID="txttaskname7" runat="server" Width="820px"></asp:TextBox>
                                            </label>
                                        </td>
                                        <td align="left">
                                            <label>
                                                <asp:TextBox ID="txtbudgetedminute7" runat="server" Width="70px"></asp:TextBox>
                                            </label>
                                        </td>
                                        <td align="left">
                                            <label>
                                                <asp:TextBox ID="txteenddate7" runat="server" Width="70px"></asp:TextBox>
                                            </label>
                                            <label>
                                                <asp:ImageButton ID="ImageButton2m" runat="server" ImageUrl="~/images/cal_actbtn.jpg" />
                                                <cc1:CalendarExtender ID="Calendarextender7" runat="server" TargetControlID="txteenddate7"
                                                    PopupButtonID="ImageButton2m">
                                                </cc1:CalendarExtender>
                                            </label>
                                        </td>
                                        <td align="left">
                                            <asp:LinkButton ID="LinkButton7" runat="server" Text="More" OnClick="LinkButton7_Click">
                                            </asp:LinkButton>
                                        </td>
                                        <td align="left" valign="top">
                                            <%--<asp:CheckBox ID="chk7" runat="server" />--%>
                                            <label>
                                                <asp:ImageButton ID="ImageButton8" runat="server" Height="15px" ImageUrl="~/images/closeicon.jpeg"
                                                    Width="15px" OnClick="ImageButton8_Click" />
                                            </label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            8.
                                        </td>
                                        <%--<td>
                                            <asp:DropDownList ID="ddlweeklygoal8" runat="server" Width="250px" OnSelectedIndexChanged="ddlweeklygoal8_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlproject8" runat="server" Width="250px">
                                            </asp:DropDownList>
                                        </td>--%>
                                        <td align="left">
                                            <label>
                                                <asp:TextBox ID="txttaskname8" runat="server" Width="820px"></asp:TextBox>
                                            </label>
                                        </td>
                                        <td align="left">
                                            <label>
                                                <asp:TextBox ID="txtbudgetedminute8" runat="server" Width="70px"></asp:TextBox>
                                            </label>
                                        </td>
                                        <td align="left">
                                            <label>
                                                <asp:TextBox ID="txteenddate8" runat="server" Width="70px"></asp:TextBox>
                                            </label>
                                            <label>
                                                <asp:ImageButton ID="ImageButton15" runat="server" ImageUrl="~/images/cal_actbtn.jpg" />
                                                <cc1:CalendarExtender ID="Calendarextender8" runat="server" TargetControlID="txteenddate8"
                                                    PopupButtonID="ImageButton15">
                                                </cc1:CalendarExtender>
                                            </label>
                                        </td>
                                        <td align="left">
                                            <asp:LinkButton ID="LinkButton8" runat="server" Text="More" OnClick="LinkButton8_Click">
                                            </asp:LinkButton>
                                        </td>
                                        <td align="left" valign="top">
                                            <%--<asp:CheckBox ID="chk8" runat="server" />--%>
                                            <label>
                                                <asp:ImageButton ID="ImageButton9" runat="server" Height="15px" ImageUrl="~/images/closeicon.jpeg"
                                                    Width="15px" OnClick="ImageButton9_Click" />
                                            </label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            9.
                                        </td>
                                        <%--  <td>
                                            <asp:DropDownList ID="ddlweeklygoal9" runat="server" Width="250px" OnSelectedIndexChanged="ddlweeklygoal9_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlproject9" runat="server" Width="250px">
                                            </asp:DropDownList>
                                        </td>--%>
                                        <td align="left">
                                            <label>
                                                <asp:TextBox ID="txttaskname9" runat="server" Width="820px"></asp:TextBox>
                                            </label>
                                        </td>
                                        <td align="left">
                                            <label>
                                                <asp:TextBox ID="txtbudgetedminute9" runat="server" Width="70px"></asp:TextBox>
                                            </label>
                                        </td>
                                        <td align="left">
                                            <label>
                                                <asp:TextBox ID="txteenddate9" runat="server" Width="70px"></asp:TextBox>
                                            </label>
                                            <label>
                                                <asp:ImageButton ID="ImageButton16" runat="server" ImageUrl="~/images/cal_actbtn.jpg" />
                                                <cc1:CalendarExtender ID="Calendarextender9" runat="server" TargetControlID="txteenddate9"
                                                    PopupButtonID="ImageButton16">
                                                </cc1:CalendarExtender>
                                            </label>
                                        </td>
                                        <td align="left">
                                            <asp:LinkButton ID="LinkButton9" runat="server" Text="More" OnClick="LinkButton9_Click">
                                            </asp:LinkButton>
                                        </td>
                                        <td align="left" valign="top">
                                            <%--<asp:CheckBox ID="chk9" runat="server" />--%>
                                            <label>
                                                <asp:ImageButton ID="ImageButton10" runat="server" Height="15px" ImageUrl="~/images/closeicon.jpeg"
                                                    Width="15px" OnClick="ImageButton10_Click" />
                                            </label>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 25%" align="right">
                            </td>
                            <td style="width: 25%">
                            </td>
                            <td style="width: 25%" align="left" class="btnSubmit">
                                <asp:Button ID="Button1" runat="server" Text="Submit" OnClick="Button1_Click" CssClass="btnSubmit" />
                            </td>
                            <td style="width: 25%">
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 25%" align="right">
                            </td>
                            <td style="width: 25%">
                            </td>
                            <td style="width: 25%" align="right">
                            </td>
                            <td style="width: 25%">
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 25%" align="right">
                            </td>
                            <td style="width: 25%">
                            </td>
                            <td style="width: 25%" align="right">
                            </td>
                            <td style="width: 25%">
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4">
                                <asp:Panel ID="Panel21" runat="server" BackColor="White" BorderColor="#999999" Width="620px"
                                    Height="300px" BorderStyle="Solid" BorderWidth="10px">
                                    <table cellpadding="0" cellspacing="3" style="width: 100%">
                                        <tr>
                                            <td colspan="2">
                                                <table style="width: 100%; font-weight: bold; color: #000000;" bgcolor="#CCCCCC">
                                                    <tr>
                                                        <td>
                                                            <label>
                                                                More Task Detail
                                                            </label>
                                                        </td>
                                                        <td align="right">
                                                            <asp:ImageButton ID="ImageButton4" runat="server" Height="15px" ImageUrl="~/images/closeicon.jpeg"
                                                                OnClick="ImageButton3_Click" Width="15px" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 30%" valign="top" align="right">
                                            </td>
                                            <td style="width: 70%">
                                                <asp:Label ID="lbltempmessage" runat="server" ForeColor="Red" Visible="False"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 30%" valign="top" align="right">
                                            </td>
                                            <td style="width: 70%">
                                                <asp:Label ID="lbltablerow" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 30%" valign="top" align="right">
                                                <label>
                                                    Task Description
                                                </label>
                                            </td>
                                            <td style="width: 70%">
                                                <asp:TextBox ID="TextBox1" runat="server" TextMode="MultiLine" Width="300px" Height="100px"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 30%" align="right">
                                                <label>
                                                    Add Attchment
                                                </label>
                                            </td>
                                            <td style="width: 70%">
                                                <asp:CheckBox ID="chkattchment" runat="server" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 30%">
                                            </td>
                                            <td style="width: 70%">
                                                <asp:Button ID="btntempadd" runat="server" Text="Add" OnClick="Button2_Click" CssClass="btnSubmit" />
                                                <asp:Button ID="btntempupdate" runat="server" OnClick="btntempupdate_Click" Text="Update"
                                                    Visible="False" CssClass="btnSubmit" />
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                                <cc1:ModalPopupExtender ID="ModalPopupExtenderAddnew" runat="server" BackgroundCssClass="modalBackground"
                                    Enabled="True" PopupControlID="Panel21" TargetControlID="HiddenButton222">
                                </cc1:ModalPopupExtender>
                                <asp:Button ID="HiddenButton222" runat="server" Style="display: none" />
                            </td>
                        </tr>
                    </table>
                </fieldset>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
