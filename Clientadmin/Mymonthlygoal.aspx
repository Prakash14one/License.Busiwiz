<%@ Page Title="" Language="C#" MasterPageFile="~/Master/LicenseMaster.master" AutoEventWireup="true"
    CodeFile="Mymonthlygoal.aspx.cs" Inherits="Mymonthlygoal" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <fieldset>
                <legend>
                    <asp:Label ID="Label30" runat="server" Text="List of MonthlyGoal"></asp:Label>
                </legend>
                <table width="100%">
                    <tr>
                        <td colspan="3">
                        </td>
                    </tr>
                    <tr>
                        <%--<td colspan="3" align="right">
                                <asp:Button ID="Button3" runat="server" CssClass="btnSubmit" Text="Printable Version" />
                                <input id="Button4" runat="server" class="btnSubmit" onclick="document.getElementById('mydiv').className='open';javascript:CallPrint('divPrint');document.getElementById('mydiv').className='closed';"
                                    style="width: 51px;" type="button" value="Print" visible="false" />
                            </td>--%>
                    </tr>
                    <tr>
                        <td colspan="4">
                           <label>
                                    <asp:Label ID="Label6" runat="server" Text="Filter By Department :"></asp:Label>
                                </label>
                                <label>
                                    <asp:DropDownList ID="ddlsortdept" runat="server" Enabled="false" AutoPostBack="True" Width="180px">
                                     </asp:DropDownList>
                                </label>
                            <label>
                                <asp:Label ID="Label1" runat="server" Text="Filter By Employee "></asp:Label>:
                            </label>
                            <label>
                                <asp:DropDownList ID="ddlstatus" runat="server" AutoPostBack="True" 
                                Width="180px">
                                </asp:DropDownList>
                            </label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                           <label>
                                    <asp:Label ID="Label2" runat="server" Text="Filter By Month"></asp:Label>
                                   
                                </label>
                                 <label style="margin-left: 24px;">:</label>
                                <label>
                                    <asp:DropDownList ID="ddlsortyear" runat="server" Width="180px" OnSelectedIndexChanged="ddlsortyear_SelectedIndexChanged"
                                        AutoPostBack="true">
                                       
                                    </asp:DropDownList>
                                </label>
                            <label>
                                <asp:Label ID="Label3" runat="server" Text="Filter By Status"></asp:Label>
                            </label>
                            <label style="margin-left: 13px;">:</label>
                            <label>
                                <asp:DropDownList ID="ddlsortmonth" runat="server" Width="180px" AutoPostBack="true"
                                    OnSelectedIndexChanged="ddlsortmonth_SelectedIndexChanged">
                                    <asp:ListItem>---Select All---</asp:ListItem>
                                    <asp:ListItem>Complete</asp:ListItem>
                                    <asp:ListItem>Pending</asp:ListItem>
                                </asp:DropDownList>
                            </label>
                           <label>
                                    <asp:Button ID="btngo" runat="server" Text="Go" 
                                CausesValidation="false" onclick="btngo_Click"  />
                                </label>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            <%--     <asp:Panel ID="Panel2" runat="server" BackColor="White" BorderColor="Gray" Width="600"
                                    BorderStyle="Solid" BorderWidth="5px">
                                    <table style="width: 100%">
                                        <tr>
                                            <td style="height: 9px">
                                                &nbsp;
                                            </td>
                                            <td align="right" style="height: 9px">
                                                <asp:ImageButton ID="ImageButton1" ImageUrl="~/images/closeicon.jpeg" runat="server"
                                                    Width="16px" Height="16px" />
                                            </td>
                                        </tr>
                                        <tr> 
                                            <td colspan="2">
                                                <label>
                                                    You will need to add our Monthlygoal which allows download of Busicontroller for
                                                    your Product. Busicontroller will allow to regulate your license terms with your
                                                    customer.</label>
                                            </td>
                                        </tr>
                                        <tr align="center">
                                            <td colspan="2">
                                                <asp:Button ID="Button1" runat="server" Text="OK" CssClass="btnSubmit" OnClick="Button2_Click" />
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>--%>
                            <asp:Button ID="Button11" runat="server" Style="display: none" />
                            <%-- <cc1:ModalPopupExtender ID="ModalPopupExtender2" runat="server" BackgroundCssClass="modalBackground"
                                    PopupControlID="Panel2" TargetControlID="Button11">
                                </cc1:ModalPopupExtender>--%>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            <asp:Panel ID="pnlgrid" runat="server" Width="100%">
                                <table width="100%">
                                    <tr>
                                        <td>
                                            <%--    <div id="mydiv" class="closed">
                                                    <table width="850Px">
                                                        <tr align="center">
                                                            <td align="center" style="text-align: center; font-size: 20px; font-weight: bold;">
                                                                <asp:Label ID="lblCompany" runat="server" ForeColor="Black" Font-Italic="true"></asp:Label>
                                                            </td>
                                                        </tr>
                                               
                                                        <tr align="center">
                                                            <td align="center" style="text-align: center; font-size: 18px; font-weight: bold;">
                                                                <asp:Label ID="Label24" runat="server" Font-Italic="true" Text="List of MonthlyGoal"></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </div>--%>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:GridView ID="grdmonthlygoal" runat="server" AutoGenerateColumns="False" CssClass="mGrid"
                                                PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt" AllowSorting="True"
                                                Width="100%" EmptyDataText="No Record Found." 
                                                OnSorting="GridView1_Sorting" onrowcommand="grdmonthlygoal_RowCommand">
                                                <AlternatingRowStyle CssClass="alt" />
                                                <Columns>
                                                    <asp:BoundField DataField="YearMaster_Name" HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="5%"
                                                        HeaderText="Year" SortExpression="YearMaster_Name">
                                                        <HeaderStyle HorizontalAlign="Left" />
                                                        <ItemStyle Width="5%" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="MonthMaster_MonthName" HeaderStyle-HorizontalAlign="Left"
                                                        ItemStyle-Width="5%" HeaderText="Month" SortExpression="MonthMaster_MonthName">
                                                        <HeaderStyle HorizontalAlign="Left" />
                                                        <ItemStyle Width="5%" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="Name" HeaderStyle-HorizontalAlign="Left" HeaderText="Dept Name"
                                                        ItemStyle-Width="15%" SortExpression="Name">
                                                        <HeaderStyle HorizontalAlign="Left" />
                                                        <ItemStyle Width="15%" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="Name1" HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="15%"
                                                        HeaderText="Employee Name" SortExpression="Name1">
                                                        <HeaderStyle HorizontalAlign="Left" />
                                                        <ItemStyle Width="15%" />
                                                    </asp:BoundField>
                                                 
                                                    <%--  <asp:BoundField DataField="MonthlyGoalMaster_MonthlyGoalTitle" HeaderStyle-HorizontalAlign="Left"
                                                            HeaderText="Monthly Goal" ItemStyle-Width="30%" SortExpression="MonthlyGoalMaster_MonthlyGoalTitle">
                                                            <HeaderStyle HorizontalAlign="Left" />
                                                            <ItemStyle Width="30%" />
                                                        </asp:BoundField>--%>
                                                    <asp:TemplateField HeaderText="Monthly Goal" ShowHeader="False" ItemStyle-Width="30%">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="false" Text='<%# Eval("MonthlyGoalMaster_MonthlyGoalTitle") %>'
                                                                CommandName="monthgoal" ForeColor="Gray" CommandArgument='<%# Eval("MonthlyGoalMaster_Id") %>'></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="MonthlyGoalMaster_Status" HeaderStyle-HorizontalAlign="Left"
                                                        ItemStyle-Width="5%" HeaderText="Status" SortExpression="MonthlyGoalMaster_Status">
                                                        <HeaderStyle HorizontalAlign="Left" />
                                                        <ItemStyle Width="5%" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="MonthlyGoalMaster_Status_Description" HeaderStyle-HorizontalAlign="Left"
                                                        HeaderText="Status Description" ItemStyle-Width="30%" SortExpression="MonthlyGoalMaster_Status_Description">
                                                        <HeaderStyle HorizontalAlign="Left" />
                                                        <ItemStyle Width="30%" />
                                                    </asp:BoundField>
                                                    <%--<asp:TemplateField HeaderText="Active" SortExpression="Active" HeaderStyle-HorizontalAlign="Left">
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="CheckBox2" Checked='<%#Bind("MonthlyGoalMaster_Active") %>' runat="server" Enabled="false" />
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Left" />
                                                        </asp:TemplateField>--%>
                                                    <%-- <asp:TemplateField HeaderText="Edit" HeaderImageUrl="~/Account/images/edit.gif" HeaderStyle-HorizontalAlign="Left"
                                                            ItemStyle-Width="3%">
                                                            <ItemTemplate>
                                                                <asp:ImageButton ID="ImageButton3" runat="server" ImageUrl="~/Account/images/edit.gif"
                                                                    CommandName="Edit" CommandArgument='<%# Eval("MonthlyGoalMaster_Id") %>' />
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Left" />
                                                            <ItemStyle Width="3%" />
                                                        </asp:TemplateField>--%>
                                                    <%--<asp:TemplateField HeaderText="Delete" HeaderImageUrl="~/Account/images/delete.gif"
                                                            HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" HeaderStyle-Width="2%">
                                                            <ItemTemplate>
                                                                <asp:ImageButton ID="imgdelete" ToolTip="Delete" runat="server" ImageUrl="~/Account/images/delete.gif"
                                                                    CommandName="Delete" CommandArgument='<%# Eval("MonthlyGoalMaster_Id") %>'></asp:ImageButton>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Left" Width="2%" />
                                                            <ItemStyle HorizontalAlign="Left" />
                                                        </asp:TemplateField>--%>
                                                </Columns>
                                                <PagerStyle CssClass="pgr" />
                                            </asp:GridView>
                                        </td>
                                        <input id="hdnsortExp" runat="Server" name="hdnsortExp" style="width: 1px" type="hidden" />
                                        <input id="hdnsortDir" runat="Server" name="hdnsortDir" style="width: 1px" type="hidden" />
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
